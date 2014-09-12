Imports Meanstream.Portal.Core.Services.Scheduling

Namespace Meanstream.Portal.Providers.PortalScheduler
    Public Class SchedulingProvider
        Inherits Meanstream.Portal.Core.Services.Scheduling.SchedulingProvider

        Private Shared _scheduleTasks As List(Of ScheduleTask)
        Private Shared _tasksLock As New Object()
        Private Shared ReadOnly Property TasksInQueue() As List(Of ScheduleTask)
            Get
                If _scheduleTasks Is Nothing Then
                    LoadTasksToQueue()
                End If
                Return _scheduleTasks
            End Get
        End Property

        Private Shared Sub LoadTasksToQueue()
            'If Not Enabled() Then
            '    Exit Sub
            'End If

            SyncLock _tasksLock
                _scheduleTasks = New List(Of ScheduleTask)
                'get list of tasks 
                '_scheduleTasks.Clear()
                Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.GetAll
                For Each entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks In entities
                    'only add back if its not in the list
                    For Each _task As ScheduleTask In _scheduleTasks
                        If _task.Task.Id = entity.Id Then
                            Continue For
                        End If
                    Next

                    Dim task As Task = Nothing
                    'Fill(entity, task)

                    Dim type As Meanstream.Portal.Core.Services.Scheduling.StartupType = [Enum].Parse(GetType(Meanstream.Portal.Core.Services.Scheduling.StartupType), entity.StartupType)
                    Dim args() As Object = {entity.Id, entity.Interval, type}
                    task = System.Activator.CreateInstance(Core.Utilities.AppUtility.GetGlobalType(entity.Name), args)
                    task.Status = [Enum].Parse(GetType(Status), entity.Status)
                    task.Type = entity.Name
                    task.StartupType = type
                    task.NextRunTime = entity.NextRunTime.GetValueOrDefault
                    task.LastRunTime = entity.LastRunDate.GetValueOrDefault
                    task.LastRunResult = entity.LastRunResult
                    task.LastRunSuccessful = entity.LastRunSuccessful.GetValueOrDefault
                    task.Interval = entity.Interval

                    Dim scheduleTask As New ScheduleTask(task)
                    _scheduleTasks.Add(scheduleTask)
                Next
            End SyncLock
        End Sub

        Private Shared Sub Fill(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks, ByVal task As Task)
            Dim startupType As Meanstream.Portal.Core.Services.Scheduling.StartupType = [Enum].Parse(GetType(Meanstream.Portal.Core.Services.Scheduling.StartupType), entity.StartupType)

            Dim args() As Object = {entity.Id, entity.Interval, startupType}
            task = System.Activator.CreateInstance(Core.Utilities.AppUtility.GetGlobalType(entity.Name), args)
            task.Status = [Enum].Parse(GetType(Status), entity.Status)
            task.Type = entity.Name
            task.StartupType = startupType
            task.NextRunTime = entity.NextRunTime.GetValueOrDefault
            task.LastRunTime = entity.LastRunDate.GetValueOrDefault
            task.LastRunResult = entity.LastRunResult
            task.LastRunSuccessful = entity.LastRunSuccessful.GetValueOrDefault
            task.Interval = entity.Interval
        End Sub

        Public Overrides Sub RestartService()
            If Not Enabled() Then
                Exit Sub
            End If

            'refresh tasks ??????
            Me.StopService()
            Me.StartService()
        End Sub

        Public Overrides Sub StartService()
            LoadTasksToQueue()

            If Not Enabled() Then
                Exit Sub
            End If

            _serviceStarted = True
            For Each task As ScheduleTask In TasksInQueue
                task.Start()
            Next
        End Sub

        Public Overrides Sub StartTask(ByVal id As Guid)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            For Each task As ScheduleTask In TasksInQueue
                If task.Task.Id = id Then
                    If task.Task.Status = Status.Stopped Then
                        task.Start()
                    End If
                End If
            Next
        End Sub

        Public Overrides Sub StopService()
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            For Each task As ScheduleTask In TasksInQueue
                task.Stop()
            Next
            _serviceStarted = False
        End Sub

        Public Overrides Sub StopTask(ByVal id As Guid)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks = Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.GetById(id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the task {0} cannot be located in database.", id))
            End If

            For Each item As ScheduleTask In TasksInQueue
                If item.Task.Id = entity.Id Then
                    item.Stop()
                End If
            Next
        End Sub

        Public Overrides Sub RemoveTask(ByVal id As Guid)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks = Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.GetById(id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the task {0} cannot be located in database.", id))
            End If

            Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.Delete(id)

            'For Each item As ScheduleTask In TasksInQueue
            '    If item.Task.Id = entity.Id Then
            '        item.Stop()
            '        '_scheduleTasks.Remove(item)
            '    End If
            'Next

            'refresh
            LoadTasksToQueue()
        End Sub

        Public Overrides Sub AddSchedule(ByVal task As Core.Services.Scheduling.Task)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            If task Is Nothing Then
                Throw New ArgumentException("task cannot be nothing")
            End If
            If String.IsNullOrEmpty(task.StartupType) Then
                Throw New ArgumentException("StartupType required")
            End If

            If task.NextRunTime = Nothing Then
                Throw New ArgumentException("NextRunTime required")
            End If

            'If task.StartupType = startupType.Automatic Or task.StartupType = startupType.RunOnce Then
            '    task.NextRunTime = Date.Now.AddSeconds(2)
            'Else
            '    task.NextRunTime = Date.Now.AddSeconds(-1)
            'End If

            task.Status = Status.Stopped

            Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks
            entity.Id = task.Id
            entity.CreatedDate = Date.Now
            entity.LastModifiedDate = Date.Now
            entity.Interval = task.Interval
            entity.NextRunTime = task.NextRunTime
            entity.Name = task.GetType.FullName
            entity.LastRunResult = ""
            entity.Status = [Enum].GetName(GetType(Meanstream.Portal.Core.Services.Scheduling.Status), task.Status)
            entity.StartupType = [Enum].GetName(GetType(Meanstream.Portal.Core.Services.Scheduling.StartupType), task.StartupType)
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.Insert(entity)

            Dim _task As Task = Nothing
            'Fill(entity, _task)

            Dim type As Meanstream.Portal.Core.Services.Scheduling.StartupType = [Enum].Parse(GetType(Meanstream.Portal.Core.Services.Scheduling.StartupType), entity.StartupType)
            Dim args() As Object = {entity.Id, entity.Interval, type}
            _task = System.Activator.CreateInstance(Core.Utilities.AppUtility.GetGlobalType(entity.Name), args)
            _task.Status = [Enum].Parse(GetType(Status), entity.Status)
            _task.Type = entity.Name
            _task.StartupType = type
            _task.NextRunTime = entity.NextRunTime.GetValueOrDefault
            _task.LastRunTime = entity.LastRunDate.GetValueOrDefault
            _task.LastRunResult = entity.LastRunResult
            _task.LastRunSuccessful = entity.LastRunSuccessful.GetValueOrDefault
            _task.Interval = entity.Interval

            Dim scheduleTask As New ScheduleTask(_task)
            TasksInQueue.Add(scheduleTask)

            If scheduleTask.Task.StartupType = StartupType.Automatic Or scheduleTask.Task.StartupType = StartupType.RunOnce Then
                scheduleTask.Start()
            End If
        End Sub

        Public Overrides Function GetSchedule() As List(Of Object)
            Dim list As New List(Of Task)
            For Each scheduledTask As ScheduleTask In TasksInQueue
                list.Add(scheduledTask.Task)
            Next
            Dim listP As List(Of Object) = list.ConvertAll(Of Object)(Function(x) DirectCast(x, Object)).ToList()
            Return listP
        End Function

        Public Overrides Sub RunTaskNow(ByVal id As Guid)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            For Each _task As ScheduleTask In TasksInQueue
                If _task.Task.Id = id Then
                    _task.RunTaskNow(True)
                End If
            Next
        End Sub

        Public Overrides Sub UpdateLastRunStatus(ByVal taskId As System.Guid, ByVal success As Boolean, ByVal result As String)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks = Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.GetById(taskId)
            entity.LastRunDate = Date.Now
            entity.LastModifiedDate = Date.Now
            entity.LastRunSuccessful = success
            entity.LastRunResult = result
            If entity.NextRunTime IsNot Nothing Then
                entity.NextRunTime = entity.NextRunTime.GetValueOrDefault.AddMilliseconds(entity.Interval)
            End If
            If entity.StartupType = "RunOnce" Then
                entity.NextRunTime = entity.LastRunDate
            End If
            'update datastore
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.Update(entity)

            'update queue
            For Each task As ScheduleTask In TasksInQueue
                If task.Task.Id = entity.Id Then
                    task.Task.LastRunTime = entity.LastRunDate
                    task.Task.LastRunSuccessful = entity.LastRunSuccessful
                    task.Task.LastRunResult = result
                    If entity.NextRunTime IsNot Nothing Then
                        task.Task.NextRunTime = entity.NextRunTime
                    End If
                End If
            Next
        End Sub

        Public Overrides Sub UpdateTask(ByVal task As Core.Services.Scheduling.Task)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks = Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.GetById(task.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the schedule task {0} cannot be located in database.", task.Id))
            End If

            entity.LastModifiedDate = Date.Now
            entity.Interval = task.Interval
            entity.LastRunDate = task.LastRunTime
            entity.LastRunResult = task.LastRunResult
            entity.NextRunTime = task.NextRunTime
            'entity.Name = task.GetType.FullName
            entity.Status = [Enum].GetName(GetType(Meanstream.Portal.Core.Services.Scheduling.Status), task.Status)
            entity.StartupType = [Enum].GetName(GetType(Meanstream.Portal.Core.Services.Scheduling.StartupType), task.StartupType)
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.Update(entity)

            'update queue
            For Each scheduleTask As ScheduleTask In TasksInQueue
                If scheduleTask.Task.Id = entity.Id Then
                    'scheduleTask.Name = task.GetType.FullName
                    scheduleTask.Task.LastRunTime = entity.LastRunDate.GetValueOrDefault
                    scheduleTask.Task.LastRunSuccessful = entity.LastRunSuccessful.GetValueOrDefault
                    scheduleTask.Task.LastRunResult = entity.LastRunResult
                    scheduleTask.Task.Status = task.Status
                    entity.StartupType = task.StartupType
                    If entity.NextRunTime IsNot Nothing Then
                        scheduleTask.Task.NextRunTime = entity.NextRunTime
                    End If
                End If
            Next
        End Sub

        Public Overrides Sub UpdateStatus(ByVal id As Guid, ByVal status As Status)
            If Not Enabled() Then
                Exit Sub
            End If

            If Not Started() Then
                Exit Sub
            End If

            'update status
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamScheduledTasks = Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.GetById(id)
            entity.LastModifiedDate = Date.Now
            entity.Status = status
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamScheduledTasksProvider.Update(entity)

            For Each scheduleTask As ScheduleTask In TasksInQueue
                If scheduleTask.Task.Id = entity.Id Then
                    scheduleTask.Task.Status = status
                End If
            Next
        End Sub

    End Class

End Namespace

