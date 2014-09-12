Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Portal.Core.ExceptionHandling

Namespace Meanstream.Portal.Core.Messaging
    Public Class MessagingService
        Implements IDisposable


#Region " Singleton "
        Private Shared _privateMessagingMessengerInstance As MessagingService
        Private Shared _messagingManagerSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As MessagingService
            Get
                If _privateMessagingMessengerInstance Is Nothing Then
                    SyncLock _messagingManagerSingletonLockObject
                        If _privateMessagingMessengerInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateMessagingMessengerInstance = New MessagingService(machineName, appFriendlyName)
                            _privateMessagingMessengerInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privateMessagingMessengerInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region



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


#Region " Messaging Events/Fire Methods "
        Public Event CustomMessageReceived As EventHandler(Of MessageEventArgs)
        Friend Sub FireCustomMessageReceivedEvent(ByVal message As Message)
            RaiseEvent CustomMessageReceived(Me, New MessageEventArgs(message))
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


#Region " Messaging Methods "

        Private Sub Initialize()
            If Not Enabled Then
                Throw New InvalidOperationException("Messaging is disabled. Check the EnableMessaging item in the appSettings section of the configuration file.")
            End If

            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The messaging infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Messaging initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Messaging: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function GetMessage(ByVal id As Guid) As Meanstream.Portal.Core.Messaging.Message
            Dim message As Meanstream.Portal.Core.Messaging.Message = New Meanstream.Portal.Core.Messaging.Message(id)
            Dim manager As New MessageManager(message)
            manager.LoadFromDatasource()
            Return message
        End Function

        Public Function SendMessage(ByVal Message As Message) As Integer
            If Message.MessageType = Nothing Then
                Throw New ArgumentNullException("Meanstream.Portal.Core.Messaging.MessageType Expected")
            End If

            If Message.Priority = Nothing Then
                Throw New ArgumentNullException("System.Net.Mail.MailPriority Expected")
            End If

            'Dim UserProfileManager As Meanstream.Portal.Core.UserProfileManager = Meanstream.Portal.Core.UserProfileManager.GetInstance
            Dim SendTo As System.Web.Security.MembershipUser = System.Web.Security.Membership.GetUser(Message.SentTo)
            If SendTo Is Nothing Then
                Throw New ArgumentNullException("SentTo User does not exist")
            End If

            If Message.SentFrom <> Nothing Then
                Dim SendFrom As System.Web.Security.MembershipUser = System.Web.Security.Membership.GetUser(Message.SentFrom)
                If SendFrom Is Nothing Then
                    Throw New ArgumentNullException("SentFrom User does not exist")
                End If
            End If

            Message.DateRecieved = Date.Now
            Message.DateSent = Date.Now
            Message.Opened = False
            Message.ReceivedStatus = True
            Message.SentStatus = True
            Message.IsQueued = False

            Dim Messaging As Meanstream.Portal.Core.Entities.MeanstreamMessaging = New Meanstream.Portal.Core.Entities.MeanstreamMessaging
            Messaging.Body = Message.Body
            Messaging.ReceivedOn = Date.Now
            Messaging.SentOn = Date.Now
            Messaging.MessageType = Message.MessageType
            Messaging.Opened = False
            Messaging.Status = True
            Messaging.Id = Guid.NewGuid 'Message.ReferenceId
            Messaging.Status = True
            Messaging.Recipient = Message.SentTo
            Messaging.IsQueued = False

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Insert(Messaging) Then
                If Message.SendEmail Then
                    Return Me.SmtpSend(SendTo.Email, Me.GetSmtpFrom, Message.Subject, Message.Body, Message.Priority)
                End If

            Else
                Return 3
            End If

            Return 1
        End Function

        Public Function SendMessage(ByVal SendToUsername As String, _
        ByVal SendFromUsername As String, _
        ByVal Subject As String, _
        ByVal Message As String, _
        ByVal Priority As System.Net.Mail.MailPriority, _
        ByVal MessageType As MessageType, _
        ByVal SendEmail As Boolean) As Integer

            Dim MembershipUser As System.Web.Security.MembershipUser = System.Web.Security.Membership.GetUser(SendToUsername)

            'Add Messaging
            Dim Messaging As Meanstream.Portal.Core.Entities.MeanstreamMessaging = New Meanstream.Portal.Core.Entities.MeanstreamMessaging
            Messaging.Id = Guid.NewGuid
            Messaging.Body = Message
            Messaging.ReceivedOn = Date.Now
            Messaging.SentOn = Date.Now
            Messaging.MessageType = MessageType
            Messaging.Opened = False
            Messaging.Status = True

            Messaging.Status = True
            Messaging.Recipient = MembershipUser.ProviderUserKey

            If SendFromUsername.Trim <> "" Then
                Messaging.Sender = Membership.MembershipService.Current.GetUserGuid(SendFromUsername)
            End If

            Messaging.Subject = Subject
            Messaging.IsQueued = False

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Insert(Messaging) Then
                If SendEmail Then
                    Return Me.SmtpSend(MembershipUser.Email, Me.GetSmtpFrom, Subject, Message, Priority)
                End If

            Else
                Return 3
            End If

            Return 1
        End Function

        Public Function SmtpSend(ByVal SendToEmailAddress As String, ByVal SendFromEmailAddress As String, ByVal Subject As String, ByVal Message As String, ByVal Priority As System.Net.Mail.MailPriority) As Integer
            'Dim Log As Meanstream.Portal.Core.Utilities.Logger = New Meanstream.Portal.Core.Utilities.Logger
            'Log.Debug("Meanstream.Portal.Core.Message.SendEmail(): Sending Message to " & SendToEmailAddress & " from " & SendFromEmailAddress)
            Try
                'Send Mail
                Dim objMail As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage(Me.GetSmtpFrom, SendToEmailAddress)
                objMail.Priority = Priority
                objMail.Subject = Subject
                objMail.Body = Message
                objMail.IsBodyHtml = True

                Dim client As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient
                client.Send(objMail)
            Catch ex As Exception
                'Log.OnError("Meanstream.Portal.Core.Message.SendEmail() " & ex.Message)
                Return 2
            End Try
            Return 1
        End Function

        Public Function GetSmtpFrom() As String
            Dim Settings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SMTP_FROM)
            Return Settings.Value
        End Function

        Public Sub MarkOpened(ByVal message As Message)
            message.Opened = True
            message.DateOpened = Date.Now
            Me.UpdateMessage(message)
        End Sub

        Public Sub UpdateMessage(ByVal message As Message)
            Dim manager As New MessageManager(message)
            manager.Save()
        End Sub

        Public Sub DeleteMessage(ByVal id As Guid)
            Dim message As New Message(id)
            Dim manager As New MessageManager(message)
            manager.Delete()
        End Sub

        Public Function GetAllRecieved(ByVal username As String) As List(Of Message)
            Dim messages As New List(Of Message)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamMessaging) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Find("Recipient=" & Membership.MembershipService.Current.GetUserGuid(username).ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamMessaging In entities
                Dim message As New Message(entity.Id)
                Dim manager As New MessageManager(message)
                manager.Bind(entity)
                messages.Add(message)
            Next
            Return messages
        End Function

        Public Function GetAllSent(ByVal username As String) As List(Of Message)
            Dim messages As New List(Of Message)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamMessaging) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Find("Sender=" & Membership.MembershipService.Current.GetUserGuid(username).ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamMessaging In entities
                Dim message As New Message(entity.Id)
                Dim manager As New MessageManager(message)
                manager.Bind(entity)
                messages.Add(message)
            Next
            Return messages
        End Function

        Public Function GetUnopened(ByVal username As String) As List(Of Message)
            Dim messages As New List(Of Message)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamMessaging) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Find("Opened=False AND Recipient=" & Membership.MembershipService.Current.GetUserGuid(username).ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamMessaging In entities
                Dim message As New Message(entity.Id)
                Dim manager As New MessageManager(message)
                manager.Bind(entity)
                messages.Add(message)
            Next
            Return messages
        End Function

        Public Function GetUnreadCount(ByVal username As String) As Integer
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Find("Opened=False AND Recipient=" & Membership.MembershipService.Current.GetUserGuid(username).ToString).Count
        End Function

        Public Function SendRegistrationConfirmation(ByVal UserName As String, ByVal Email As String, ByVal Password As String) As Integer
            'Log.Debug("Meanstream.Portal.Core.Meanstream.Portal.Core.Message.SendMessageRegistrationConfirmation(): " & UserName)

            Dim Message As String = ""

            Dim Subject As String = ""

            Dim BodySettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_BODY_REGISTRATION)

            Message = BodySettings.Value

            Message = Message.Replace("[USERNAME]", UserName)

            Message = Message.Replace("[PASSWORD]", Password)

            Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_REGISTER)

            Subject = SubjectSettings.Value

            Return Me.SmtpSend(Email, Me.GetSmtpFrom, Subject, Message, Net.Mail.MailPriority.High)

        End Function

        Public Function SendResetPasswordEmail(ByVal UserName As String, ByVal Email As String, ByVal Password As String) As Integer
            'Log.Debug("Meanstream.Portal.Core.Meanstream.Portal.Core.Message.SendMessageResetPasswordEmail(): " & UserName)

            Dim Message As String = ""

            Dim Subject As String = ""

            Dim BodySettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_BODY_RESET_PASSWORD)

            Message = BodySettings.Value

            Message = Message.Replace("[USERNAME]", UserName)

            Message = Message.Replace("[PASSWORD]", Password)

            Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_RESET_PASSWORD)

            Subject = SubjectSettings.Value

            Return Me.SmtpSend(Email, Me.GetSmtpFrom, Subject, Message, Net.Mail.MailPriority.High)

        End Function

        Public Function SendAddRoleToUserEmail(ByVal UserName As String, ByVal Email As String, ByVal Role As String) As Integer
            'Log.Debug("Meanstream.Portal.Core.Meanstream.Portal.Core.Message.SendMessageAddRoleToUserEmail(): " & UserName)

            Dim Message As String = ""

            Dim Subject As String = ""

            Dim BodySettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_BODY_ADD_ROLE_TO_USER)

            Message = BodySettings.Value

            Message = Message.Replace("[USERNAME]", UserName)

            Message = Message.Replace("[ROLE]", Role)

            Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_ADD_ROLE_TO_USER)

            Subject = SubjectSettings.Value

            Return Me.SmtpSend(Email, Me.GetSmtpFrom, Subject, Message, Net.Mail.MailPriority.High)

        End Function

        Public Function SendAddUserToRoleEmail(ByVal UserName As String, ByVal Email As String, ByVal Role As String) As Integer
            'Log.Debug("Meanstream.Portal.Core.Meanstream.Portal.Core.Message.SendMessageAddUserToRoleEmail(): " & UserName)

            Dim Message As String = ""

            Dim Subject As String = ""

            Dim BodySettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_BODY_ADD_USER_TO_ROLE)

            Message = BodySettings.Value

            Message = Message.Replace("[USERNAME]", UserName)

            Message = Message.Replace("[ROLE]", Role)

            Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_ADD_USER_TO_ROLE)

            Subject = SubjectSettings.Value

            Return Me.SmtpSend(Email, Me.GetSmtpFrom, Subject, Message, Net.Mail.MailPriority.High)

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
