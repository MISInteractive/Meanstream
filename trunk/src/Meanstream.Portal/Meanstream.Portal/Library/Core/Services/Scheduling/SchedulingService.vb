Imports Meanstream.Portal.Core.Instrumentation
Imports System.Configuration
Imports Meanstream.Portal.Core.Messaging

Namespace Meanstream.Portal.Core.Services.Scheduling
    Public Class SchedulingService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As SchedulingService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As SchedulingService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateServiceInstance = New SchedulingService(machineName, appFriendlyName)
                            _privateServiceInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privateServiceInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

#Region "Fields"
        'Private tasks As New List(Of ITask)
        'Private Shared serviceStarted As Boolean = False
#End Region

#Region "Methods"
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The scheduling service has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Scheduling Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Scheduling Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Shared Function Started() As Boolean
            Return SchedulingProvider.Started
        End Function

        Public Shared Function Enabled() As Boolean
            Return SchedulingProvider.Enabled
        End Function

        Public Sub StartService()
            SchedulingProvider.Current.StartService()
        End Sub

        Public Sub StopService()
            SchedulingProvider.Current.StopService()
        End Sub

        Public Sub AddToSchedule(ByVal task As Task)
            SchedulingProvider.Current.AddSchedule(task)
        End Sub

        Public Sub RemoveFromSchedule(ByVal id As Guid)
            SchedulingProvider.Current.RemoveTask(id)
        End Sub

        Public Function GetScheduledTasks() As List(Of Object)
            Return SchedulingProvider.Current.GetSchedule
        End Function

        Public Sub StartTask(ByVal id As Guid)
            SchedulingProvider.Current.StartTask(id)
        End Sub

        Public Sub StopTask(ByVal id As Guid)
            SchedulingProvider.Current.StopTask(id)
        End Sub

        Public Sub RunTaskNow(ByVal id As Guid)
            SchedulingProvider.Current.RunTaskNow(id)
        End Sub
#End Region

#Region "Events/Fire Methods "
        Public Event TaskExecutedMessageReceived As EventHandler(Of TaskExecuteEventArgs)
        Public Sub FireTaskExecutedMessageReceivedEvent(ByVal message As ApplicationMessage)
            RaiseEvent TaskExecutedMessageReceived(Me, New TaskExecuteEventArgs(message))
        End Sub
#End Region

#Region " Properties "
        Private _appFriendlyName As String
        Public Property AppFriendlyName() As String
            Get
                Return _appFriendlyName
            End Get
            Private Set(ByVal value As String)
                _appFriendlyName = value
            End Set
        End Property

        Private _machineName As String
        Public Property MachineName() As String
            Get
                Return _machineName
            End Get
            Private Set(ByVal value As String)
                _machineName = value
            End Set
        End Property

        Private _applicationId As Guid
        Public Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
            Private Set(ByVal value As Guid)
                _applicationId = value
            End Set
        End Property
#End Region

#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Deinitialize()
        End Sub
#End Region
    End Class
End Namespace

