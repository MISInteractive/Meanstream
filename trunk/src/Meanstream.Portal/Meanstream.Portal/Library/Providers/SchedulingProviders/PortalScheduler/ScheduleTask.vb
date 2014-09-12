Imports System.Threading
Imports System.Timers
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Services.Scheduling

Namespace Meanstream.Portal.Providers.PortalScheduler
    Public Class ScheduleTask

        Sub New(ByVal task As Task)
            _task = task
        End Sub

#Region "Fields"

        Private startTimer As System.Timers.Timer = Nothing
        Private timer As System.Timers.Timer = Nothing
        Private _task As Task

#End Region

#Region "Properties"

        Public ReadOnly Property Task() As Task
            Get
                Return _task
            End Get
        End Property

#End Region

#Region "Methods"

        Public Sub Start()

            If Not SchedulingProvider.Started Then
                Exit Sub
            End If

            If _task.StartupType = StartupType.Disabled Then
                Exit Sub
            End If

            _task.Status = Status.Started
            'update status
            SchedulingProvider.Current.UpdateStatus(_task.Id, _task.Status)

            If _task.NextRunTime <> Nothing Then
                'get initial start date then get the milliseconds between now and then
                Dim dt1 As DateTime = DateTime.Now
                If dt1 > _task.NextRunTime Then
                    'set next run to now if we've passed our next run date
                    _task.NextRunTime = DateTime.Now.AddSeconds(2)
                    SchedulingProvider.Current.UpdateTask(_task)
                End If

                If _task.StartupType = StartupType.RunOnce Then
                    'kick off at next run time - non recurring
                    Dim ts As TimeSpan = _task.NextRunTime.Subtract(Date.Now)
                    startTimer = New System.Timers.Timer(ts.TotalMilliseconds)
                    AddHandler startTimer.Elapsed, New ElapsedEventHandler(AddressOf startTimerOnce_Elapsed)
                    startTimer.Enabled = True
                ElseIf _task.StartupType = StartupType.Disabled Then
                    'do nothing
                    Exit Sub
                Else
                    'kick off at next run time - recurring
                    Dim ts As TimeSpan = _task.NextRunTime.Subtract(dt1)
                    'Dim ts As TimeSpan = _task.NextRunTime.TimeOfDay
                    startTimer = New System.Timers.Timer(ts.TotalMilliseconds)
                    AddHandler startTimer.Elapsed, New ElapsedEventHandler(AddressOf startTimer_Elapsed)
                    startTimer.Enabled = True
                End If
            End If

            AddHandler SchedulingService.Current.TaskExecutedMessageReceived, AddressOf Me.OnTaskExecutedMessageReceived

        End Sub

        Public Sub [Stop]()

            _task.Status = Status.Stopped

            Try
                timer.Stop()
                timer.Enabled = False
                RemoveHandler timer.Elapsed, New ElapsedEventHandler(AddressOf timer_Elapsed)
            Catch ex As Exception

            End Try

            'update status
            SchedulingProvider.Current.UpdateStatus(_task.Id, _task.Status)

        End Sub

#End Region

#Region "Private Methods"

        Protected Friend Sub RunTaskNow(ByVal fromRunTaskNow As Boolean)

            Dim status As Status = _task.Status

            If _task.Status = Status.Stopped And Not fromRunTaskNow Then
                Return
            End If

            If _task.StartupType <> StartupType.Disabled And _task.Status <> Status.Running Then
                _task.Status = Status.Running
                'update status
                SchedulingProvider.Current.UpdateStatus(_task.Id, _task.Status)

                _task.LastRunTime = DateTime.Now
                _task.LastRunSuccessful = True
                _task.LastRunResult = ""

                Try
                    'run task....
                    Dim t As New Thread(New ThreadStart(AddressOf _task.Execute))
                    t.IsBackground = True
                    t.Start()

                    If _task.LastRunSuccessful Then
                        Me.OnTaskExecutedMessageReceived(_task, New TaskExecuteEventArgs(New ApplicationMessage("task " & _task.Type.ToLower & " execution complete", ApplicationMessageType.TaskExecuted)))
                        If Not fromRunTaskNow Then
                            'update task run
                            SchedulingProvider.Current.UpdateLastRunStatus(_task.Id, True, _task.LastRunResult)
                        Else
                            'update task
                            _task.LastRunSuccessful = True
                            _task.Status = status
                            _task.LastRunTime = Date.Now
                            SchedulingProvider.Current.UpdateTask(_task)
                        End If
                    Else
                        Me.OnTaskExecutedMessageReceived(_task, New TaskExecuteEventArgs(New ApplicationMessage("task " & _task.Type.ToLower & " execution failed", ApplicationMessageType.TaskFailed)))
                        If Not fromRunTaskNow Then
                            'update task run
                            SchedulingProvider.Current.UpdateLastRunStatus(_task.Id, False, _task.LastRunResult)
                        Else
                            'update task
                            _task.LastRunSuccessful = False
                            _task.Status = status
                            _task.LastRunTime = Date.Now
                            SchedulingProvider.Current.UpdateTask(_task)
                        End If
                    End If

                Catch ex As Exception
                    _task.LastRunSuccessful = False
                    _task.LastRunResult = ex.Message
                    Me.OnTaskExecutedMessageReceived(_task, New TaskExecuteEventArgs(New ApplicationMessage("task " & _task.Type.ToLower & " execution failed", ApplicationMessageType.TaskFailed)))
                Finally
                    If Not fromRunTaskNow Then
                        _task.Status = status.Started
                        SchedulingProvider.Current.UpdateStatus(_task.Id, _task.Status)
                    End If
                End Try

            End If

        End Sub

        'start at specified run date - run once
        Private Sub startTimerOnce_Elapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            RemoveHandler startTimer.Elapsed, New ElapsedEventHandler(AddressOf startTimer_Elapsed)
            startTimer.Stop()
            startTimer.Enabled = False
            Me.RunTaskNow(False)
            _task.Status = Status.Stopped
            'update status
            SchedulingProvider.Current.UpdateStatus(_task.Id, _task.Status)

        End Sub

        Private Sub startTimer_Elapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            'remove the initial handler to keep this method from firing
            RemoveHandler startTimer.Elapsed, New ElapsedEventHandler(AddressOf startTimer_Elapsed)
            startTimer.Stop()
            startTimer.Enabled = False
            timer = New System.Timers.Timer(_task.Interval)
            AddHandler timer.Elapsed, New ElapsedEventHandler(AddressOf timer_Elapsed)
            timer.Enabled = True
            Me.RunTaskNow(False)

        End Sub

        Private Sub timer_Elapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            Me.RunTaskNow(False)

        End Sub

        Protected Friend Sub OnTaskExecutedMessageReceived(ByVal sender As Object, ByVal e As TaskExecuteEventArgs)

            Select Case e.Message.MessageType
                Case ApplicationMessageType.TaskFailed
                    If Not e.Handled Then
                        If ApplicationMessagingManager.Enabled Then
                            ApplicationMessagingManager.Current.SendApplicationMessage(SchedulingService.Current.ApplicationId, Nothing, New ApplicationMessage(e.Message.Message, ApplicationMessageType.TaskFailed))
                            'send message to admin....?
                        End If
                        e.Handled = True
                    End If
                    Exit Select
                Case ApplicationMessageType.TaskExecuted
                    If Not e.Handled Then
                        If ApplicationMessagingManager.Enabled Then
                            ApplicationMessagingManager.Current.SendApplicationMessage(SchedulingService.Current.ApplicationId, Nothing, New ApplicationMessage(e.Message.Message, ApplicationMessageType.TaskExecuted))
                        End If
                        e.Handled = True
                    End If
                    Exit Select
            End Select

        End Sub

#End Region

    End Class

End Namespace

