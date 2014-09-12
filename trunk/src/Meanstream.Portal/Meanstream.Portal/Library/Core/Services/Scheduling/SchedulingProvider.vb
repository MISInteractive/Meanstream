Imports Meanstream.Portal.ComponentModel

Namespace Meanstream.Portal.Core.Services.Scheduling

    Public Enum Status
        Started
        Running
        Stopped
    End Enum

    Public Enum StartupType
        Automatic
        RunOnce
        Disabled
    End Enum

    Public MustInherit Class SchedulingProvider

#Region "Fields"
        Friend Shared _serviceStarted As Boolean = False
        Friend Shared _enabled As Boolean = False
#End Region

        Public Sub New()
            If Not String.IsNullOrEmpty(Settings("enabled")) Then
                _enabled = Convert.ToBoolean(Settings("enabled"))
                'start service
                'Me.StartService()
            Else
                Core.Instrumentation.PortalTrace.Fail(String.Format("Could not load enabled attribute {0}", Me.GetType), Instrumentation.DisplayMethodInfo.FullSignature)
            End If
        End Sub

        Public Shared Function Current() As SchedulingProvider
            'initialize provider
            Return ComponentFactory.GetComponent(Of SchedulingProvider)()
        End Function

        Public ReadOnly Property Settings() As Dictionary(Of String, String)
            Get
                Return ComponentFactory.GetComponentSettings(Me.GetType.FullName)
            End Get
        End Property

        Public Shared Function Started() As Boolean
            Return _serviceStarted
        End Function

        Public Shared Function Enabled() As Boolean
            Return _enabled
        End Function

        Public MustOverride Sub StartService()
        Public MustOverride Sub StopService()
        Public MustOverride Sub RestartService()
        Public MustOverride Sub StartTask(ByVal id As Guid)
        Public MustOverride Sub StopTask(ByVal id As Guid)
        Public MustOverride Sub UpdateLastRunStatus(ByVal id As Guid, ByVal success As Boolean, ByVal result As String)
        Public MustOverride Sub AddSchedule(ByVal task As Task)
        Public MustOverride Sub RemoveTask(ByVal id As Guid)
        Public MustOverride Function GetSchedule() As List(Of Object)
        Public MustOverride Sub UpdateTask(ByVal task As Task)
        Public MustOverride Sub RunTaskNow(ByVal id As Guid)
        Public MustOverride Sub UpdateStatus(ByVal id As Guid, ByVal status As Status)

    End Class
End Namespace

