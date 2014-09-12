Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core
    Public Class PortalService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privatePortalServicerInstance As PortalService
        Private Shared _portalServiceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As PortalService
            Get
                If _privatePortalServicerInstance Is Nothing Then
                    SyncLock _portalServiceSingletonLockObject
                        If _privatePortalServicerInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privatePortalServicerInstance = New PortalService(machineName, appFriendlyName)
                            _privatePortalServicerInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privatePortalServicerInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

#Region " Methods "
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The portal service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Portal Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Portal Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function Create() As Guid

        End Function

        Public Function GetPortal(ByVal id As Guid) As Portal
            Return Nothing
        End Function

        Public Function GetPortals() As List(Of Portal)
            Return Nothing
        End Function
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

