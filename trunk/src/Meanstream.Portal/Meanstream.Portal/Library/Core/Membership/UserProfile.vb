
Namespace Meanstream.Portal.Core.Membership
    Public Class UserProfile
        Inherits System.Web.Profile.ProfileBase

#Region " Properties "
        Private _UserId As Guid = Nothing
        Public ReadOnly Property UserId() As Guid
            Get
                If _UserId = Nothing Then
                    _UserId = Me.User.ProviderUserKey
                End If
                Return _UserId
            End Get
        End Property

        Private _Email As String = Nothing
        Public ReadOnly Property Email() As String
            Get
                If _Email = Nothing Then
                    _Email = Me.User.Email
                End If
                Return _Email
            End Get
        End Property

        Private _profileFolderPath As String = Nothing
        Public ReadOnly Property ProfileFolderPath() As String
            Get
                If _profileFolderPath = Nothing Then
                    _profileFolderPath = [String].Concat(PortalContext.Current.Portal.ProfilesPath, Me.UserId.ToString, "/")
                End If
                Return _profileFolderPath
            End Get
        End Property

        Private _MembershipUser As System.Web.Security.MembershipUser = Nothing
        Public ReadOnly Property User() As System.Web.Security.MembershipUser
            Get
                If _MembershipUser Is Nothing Then
                    _MembershipUser = System.Web.Security.Membership.GetUser(Me.UserName)
                End If
                Return _MembershipUser
            End Get
        End Property
#End Region

        Public Overloads Shared Function Create(ByVal Username As String) As Meanstream.Portal.Core.Membership.UserProfile
            Dim UserProfile As Meanstream.Portal.Core.Membership.UserProfile = New Meanstream.Portal.Core.Membership.UserProfile
            UserProfile.Initialize(Username, False)
            Return UserProfile
        End Function

        Public Overloads Shared Function Create(ByVal Username As String, ByVal IsAuthenticated As Boolean) As Meanstream.Portal.Core.Membership.UserProfile
            Dim UserProfile As Meanstream.Portal.Core.Membership.UserProfile = New Meanstream.Portal.Core.Membership.UserProfile
            UserProfile.Initialize(Username, IsAuthenticated)
            Return UserProfile
        End Function

        Public Shared Function CreateNewUser(ByVal Username As String, ByVal Password As String, ByVal Email As String) As Meanstream.Portal.Core.Membership.UserProfile
            Dim MembershipUser As System.Web.Security.MembershipUser = System.Web.Security.Membership.CreateUser(Username, Password, Email)
            MembershipUser.IsApproved = True
            System.Web.Security.Membership.UpdateUser(MembershipUser)
            Return Meanstream.Portal.Core.Membership.UserProfile.Create(MembershipUser.UserName)
        End Function

        Public Sub Delete()
            Dim UserRoles As String() = System.Web.Security.Roles.GetRolesForUser(UserName)
            If UserRoles.Length > 0 Then
                System.Web.Security.Roles.RemoveUserFromRoles(UserName, UserRoles)
            End If
            System.Web.Profile.ProfileManager.DeleteProfile(UserName)
            System.Web.Security.Membership.DeleteUser(UserName)
        End Sub

        Public Function GetUserPreference(ByVal preference As String) As UserPreference
            Return MembershipService.Current.GetUserPreference(Me.UserId, preference)
        End Function

        Public Function AddUserPreference(ByVal preferenceId As Guid, ByVal value As Object) As Guid
            Return MembershipService.Current.AddUserPreference(Me.UserId, preferenceId, value)
        End Function

        Public Sub UpdateUserPreference(ByVal preferenceId As Guid, ByVal value As Object)
            MembershipService.Current.UpdateUserPreference(Me.UserId, preferenceId, value)
        End Sub

        Public Function GetUserPreferences() As List(Of UserPreference)
            Return MembershipService.Current.GetUserPreferences(Me.UserName)
        End Function

        Public Function GetUnreadMessagesCount() As Integer
            Return Messaging.MessagingService.Current.GetUnreadCount(Me.UserName)
        End Function

        Public Function GetAllRecievedMessages() As List(Of Messaging.Message)
            Return Messaging.MessagingService.Current.GetAllRecieved(Me.UserName)
        End Function

        Public Function GetAllSentMessages() As List(Of Messaging.Message)
            Return Messaging.MessagingService.Current.GetAllSent(Me.UserName)
        End Function

        Public Function GetUnreadMessages() As List(Of Messaging.Message)
            Return Messaging.MessagingService.Current.GetUnopened(Me.UserName)
        End Function
    End Class
End Namespace

