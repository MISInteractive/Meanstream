Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Portal.Core.ExceptionHandling

Namespace Meanstream.Portal.Core.Messaging

    Public Class ApplicationMessagingManager
        Implements IDisposable


#Region " Singleton "
        Private Shared _privateMessagingMessengerInstance As ApplicationMessagingManager
        Private Shared _messagingManagerSingletonLockObject As New Object()
        Private Shared _heartBeatTimer As System.Threading.Timer

        Public Shared ReadOnly Property Current() As ApplicationMessagingManager
            Get
                If _privateMessagingMessengerInstance Is Nothing Then
                    SyncLock _messagingManagerSingletonLockObject
                        If _privateMessagingMessengerInstance Is Nothing Then
                            Dim appDomainFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateMessagingMessengerInstance = New ApplicationMessagingManager(machineName, appDomainFriendlyName)
                            _privateMessagingMessengerInstance.Initialize()

                            _heartBeatTimer = New System.Threading.Timer(New System.Threading.TimerCallback(AddressOf HeartBeatTimerElapsed), _privateMessagingMessengerInstance, 1000 * 60 * 3, 1000 * 60 * 3)
                        End If
                    End SyncLock
                End If
                Return _privateMessagingMessengerInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.MachineName = machineName
            Me.AppFriendlyName = appFriendlyName
        End Sub
#End Region


        Private Shared Sub HeartBeatTimerElapsed(ByVal state As Object)
            Dim myMessagingManager As ApplicationMessagingManager = TryCast(state, ApplicationMessagingManager)

            If myMessagingManager Is Nothing Then
                If _heartBeatTimer IsNot Nothing Then
                    _heartBeatTimer.Dispose()
                End If
                Return
            End If

            If myMessagingManager.ApplicationId <> Nothing Then
                myMessagingManager.SendApplicationMessage(myMessagingManager.ApplicationId, Nothing, New ApplicationMessage("HeartBeat", ApplicationMessageType.HeartBeat))
            End If
        End Sub


        Private Shared _messagingEnabled As Boolean = True
        Private Shared _messagingStatusChecked As Boolean = True

        ''' <summary>
        ''' Gets enabled/disabled status of messaging.
        ''' It is a read only property, the application messaging can be set up in the web.config file.
        ''' </summary>
        Public Shared ReadOnly Property Enabled() As Boolean
            Get
                If Not _messagingStatusChecked Then
                    Dim enableMessaging As String = System.Configuration.ConfigurationManager.AppSettings("Meanstream.EnableMessaging")
                    If enableMessaging Is Nothing Or enableMessaging.ToLower = "false" Then
                        _messagingEnabled = False
                    ElseIf enableMessaging.ToLower = "true" Then
                        _messagingEnabled = True
                    Else
                        Throw New ConfigurationException(String.Format("The EnableMessaging property has been set to '{0}' in the appSettings section, which is invalid. The valid values are 'true' and 'false', or you can completly omit this element, in that case its defaults to false.", enableMessaging))
                    End If
                    _messagingStatusChecked = True
                End If
                Return _messagingEnabled
            End Get
        End Property


#Region " Application Messaging Events/Fire Methods "
        Public Event CustomMessageReceived As EventHandler(Of ApplicationMessageEventArgs)
        Friend Sub FireCustomMessageReceivedEvent(ByVal message As ApplicationMessage)
            RaiseEvent CustomMessageReceived(Me, New ApplicationMessageEventArgs(message))
        End Sub

        Public Event PortalContextChangedMessageEvent As EventHandler(Of ApplicationMessageEventArgs)
        Friend Sub FirePortalContextChangedMessageEvent(ByVal message As ApplicationMessage)
            RaiseEvent PortalContextChangedMessageEvent(Me, New ApplicationMessageEventArgs(message))
        End Sub
#End Region


#Region " Properties "
        Private _portalId As Guid
        Public Property PortalId() As Guid
            Get
                Return _portalId
            End Get
            Private Set(ByVal value As Guid)
                _portalId = value
            End Set
        End Property

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


#Region " Application Messaging Methods "
        Private Sub Initialize()
            If Not Enabled Then
                Throw New InvalidOperationException("Messaging is disabled. Check the EnableMessaging item in the configuration file (web.config).")
            End If

            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The messaging infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            Dim Instance As Meanstream.Portal.Core.Entities.MeanstreamApplicationMessagingInstance = New Meanstream.Portal.Core.Entities.MeanstreamApplicationMessagingInstance
            Instance.ApplicationId = Me.ApplicationId
            Instance.DomainName = Me.AppFriendlyName
            Instance.MachineName = Me.MachineName
            Instance.LastUpdateDate = Date.Now
            Instance.RegisteredDate = Date.Now

            Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingInstanceProvider.Insert(Instance)

            PortalTrace.WriteLine([String].Concat("Application Messaging initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Application Messaging: ", AppFriendlyName, " #", ApplicationId))
            Dim params() As Object = {Me.ApplicationId}
            Meanstream.Portal.Core.Data.DataRepository.Provider.ExecuteNonQuery("meanstream_DeinitializeMessaging", params)
            Me.ApplicationId = Nothing
        End Sub

        Public Sub SendApplicationMessage(ByVal applicationMessage As ApplicationMessage)
            Me.SendApplicationMessage(ApplicationId, applicationMessage)
        End Sub

        Public Sub SendApplicationMessage(ByVal ApplicationId As Guid, ByVal applicationMessage As ApplicationMessage)
            Me.SendApplicationMessage(ApplicationId, Nothing, applicationMessage)
        End Sub

        Public Sub SendApplicationMessage(ByVal ApplicationId As Guid, ByVal TargetApplicationId As Guid, ByVal applicationMessage As ApplicationMessage)
            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                Throw New InvalidOperationException(String.Format("The messaging infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            'insert message
            Dim Message As Meanstream.Portal.Core.Entities.MeanstreamApplicationMessaging = New Meanstream.Portal.Core.Entities.MeanstreamApplicationMessaging
            Message.MessageType = applicationMessage.MessageType
            Message.Message = applicationMessage.Message

            If TargetApplicationId <> Nothing Then
                Message.SenderAppicationId = ApplicationId
                Message.TargetApplicationId = TargetApplicationId
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingProvider.Insert(Message)

                'update last activity date
                Dim Instances As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamApplicationMessagingInstance) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingInstanceProvider.Find("ApplicationId=" & TargetApplicationId.ToString)
                Instances(0).LastUpdateDate = Date.Now
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingInstanceProvider.Update(Instances(0))

            Else 'if no target was passed send message to all applications
                Dim Instances As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamApplicationMessagingInstance) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingInstanceProvider.GetAll
                For Each Instance As Meanstream.Portal.Core.Entities.MeanstreamApplicationMessagingInstance In Instances
                    Message.SenderAppicationId = ApplicationId
                    Message.TargetApplicationId = Instance.ApplicationId
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingProvider.Insert(Message)

                    'update last activity date
                    Instance.LastUpdateDate = Date.Now
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingInstanceProvider.Update(Instance)
                Next
            End If
        End Sub

        Public Function GetAndDeleteApplicationMessages() As List(Of Meanstream.Portal.Core.Messaging.ApplicationMessage)
            Dim ApplicationMessages As New List(Of Meanstream.Portal.Core.Messaging.ApplicationMessage)

            Dim Messages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamApplicationMessaging) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingProvider.Find("TargetApplicationId=" & Me.ApplicationId.ToString)
            For Each Message As Meanstream.Portal.Core.Entities.MeanstreamApplicationMessaging In Messages
                'add
                ApplicationMessages.Add(New Meanstream.Portal.Core.Messaging.ApplicationMessage(Message.Message, Message.MessageType))
                'delete
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingProvider.Delete(Message.Id)
            Next

            Return ApplicationMessages
        End Function

        Public Function GetApplicationMessages() As List(Of Meanstream.Portal.Core.Messaging.ApplicationMessage)
            Dim ApplicationMessages As New List(Of Meanstream.Portal.Core.Messaging.ApplicationMessage)

            Dim Messages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamApplicationMessaging) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationMessagingProvider.Find("TargetApplicationId=" & Me.ApplicationId.ToString)
            For Each Message As Meanstream.Portal.Core.Entities.MeanstreamApplicationMessaging In Messages
                'add
                ApplicationMessages.Add(New Meanstream.Portal.Core.Messaging.ApplicationMessage(Message.Message, Message.MessageType))
            Next

            Return ApplicationMessages
        End Function
#End Region


#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Enabled Then
                Deinitialize()
            End If
        End Sub
#End Region

    End Class
End Namespace
