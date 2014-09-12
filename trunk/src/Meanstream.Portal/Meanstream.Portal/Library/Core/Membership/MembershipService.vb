Imports Meanstream.Portal.Core.Instrumentation
Imports System.Configuration

Namespace Meanstream.Portal.Core.Membership
    Public Class MembershipService
        Implements IDisposable

        Public Const MEANSTREAMUSERTHEME As String = "MEANSTREAMUSERTHEME"
        Public Const MEANSTREAMUSERTHEMEPATH As String = "MEANSTREAMUSERTHEMEPATH"

#Region " Singleton "
        Private Shared _privateServiceInstance As MembershipService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As MembershipService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName
                            _privateServiceInstance = New MembershipService(machineName, appFriendlyName)
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

#Region " Methods "
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The membership service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Membership Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Membership Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function DeleteUser(ByVal username As String) As Boolean
            Dim UserRoles As String() = System.Web.Security.Roles.GetRolesForUser(UserName)
            If UserRoles.Length > 0 Then
                System.Web.Security.Roles.RemoveUserFromRoles(username, UserRoles)
            End If
            System.Web.Profile.ProfileManager.DeleteProfile(username)
            Return System.Web.Security.Membership.DeleteUser(UserName)
        End Function

        Public Function GetUserGuid(ByVal username As String) As Guid
            Dim userid As Guid = Nothing
            Try
                If System.Web.HttpContext.Current.Session("USERGUID_" & username) Is Nothing Then
                    userid = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.Find("UserName=" & username).Item(0).UserId()
                    System.Web.HttpContext.Current.Session("USERGUID_" & username) = userid
                Else
                    userid = System.Web.HttpContext.Current.Session("USERGUID_" & username)
                End If
            Catch ex As Exception
                'No Session exists for Web Service
                userid = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.Find("UserName=" & username).Item(0).UserId()
            End Try

            Return Userid
        End Function

        Public Function GetUsername(ByVal userId As Guid) As String
            Dim username As String = Nothing
            If System.Web.HttpContext.Current.Session("PORTAL_SESSION_USERNAME_" & userId.ToString) Is Nothing Then
                Try
                    username = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.GetByUserId(userId).UserName
                    System.Web.HttpContext.Current.Session("PORTAL_SESSION_USERNAME_" & userId.ToString) = username
                Catch ex As Exception
                End Try
            Else
                username = System.Web.HttpContext.Current.Session("PORTAL_SESSION_USERNAME_" & userId.ToString)
            End If
            Return username
        End Function

        Public Function GetNumberOfProfiles() As Integer
            Return System.Web.Profile.ProfileManager.GetNumberOfProfiles(System.Web.Profile.ProfileAuthenticationOption.All)
        End Function

        Public Function GetNumberOfUsersOnline() As Integer
            Return System.Web.Security.Membership.GetNumberOfUsersOnline
        End Function

        Public Function GetAllUsersDataSet() As System.Data.DataSet
            Dim ds As New System.Data.DataSet
            Dim dt As New System.Data.DataTable
            dt = ds.Tables.Add("Users")

            Dim muc As System.Web.Security.MembershipUserCollection
            muc = System.Web.Security.Membership.GetAllUsers
            dt.Columns.Add("UserName", Type.GetType("System.String"))
            For Each mu As System.Web.Security.MembershipUser In muc
                Dim dr As System.Data.DataRow
                dr = dt.NewRow
                dr("Username") = mu.UserName
                dt.Rows.Add(dr)
            Next

            Return ds
        End Function

        Public Function GetUsers() As System.Data.DataSet
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.GetAll().ToDataSet(True)
        End Function

        Public Function GetNewUsersByDateRange(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.AppendRange(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.CreateDate, FromDate.ToString, ToDate.ToString)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function GetNewUsersByLastActivityDateRange(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.AppendRange(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.LastActivityDate, FromDate.ToString, ToDate.ToString)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function GetUsersByLastLoginDateRange(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.AppendRange(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.LastLoginDate, FromDate.ToString, ToDate.ToString)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function GetUsersByLastPasswordChangeDateRange(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.AppendRange(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.LastPasswordChangedDate, FromDate.ToString, ToDate.ToString)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function GetUsersByLastLockoutDateRange(ByVal FromDate As Date, ByVal ToDate As Date) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.AppendRange(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.LastLockoutDate, FromDate.ToString, ToDate.ToString)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function SearchForUser(ByVal Username As String) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.Append(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.UserName, "%" & Username & "%", True)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function SearchForUser(ByVal UserName As String, ByVal Email As String) As System.Data.DataSet
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.Append(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.UserName, "%" & UserName & "%", True)
            Query.Append("OR", Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.Email, "%" & Email & "%", True)
            Return Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters).ToDataSet(True)
        End Function

        Public Function GetProfilesInCSV() As String
            Dim MembershipUserCollection As System.Web.Security.MembershipUserCollection = System.Web.Security.Membership.GetAllUsers()
            Dim x As String = ""
            ' Get record count
            Dim n As Integer = MembershipUserCollection.Count
            n = n - 1

            Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            Dim ProfileSection As System.Web.Configuration.ProfileSection = DirectCast(Configuration.GetSection("system.web/profile"), System.Web.Configuration.ProfileSection)
            Dim PropertySettings As System.Web.Configuration.RootProfilePropertySettingsCollection = ProfileSection.PropertySettings

            For Each PropertySetting As System.Web.Configuration.ProfilePropertySettings In PropertySettings
                x = x & PropertySetting.Name & ","
            Next
            x = x & vbCrLf
            'check ProfileBase is really ProfileCommon
            Dim Enumerator As IEnumerator = MembershipUserCollection.GetEnumerator
            While Enumerator.MoveNext()
                Dim ProfileCommon As System.Web.Profile.ProfileBase = CType(System.Web.Profile.ProfileBase.Create(Enumerator.Current.Username, True), System.Web.Profile.ProfileBase)
                For Each PropertySetting As System.Web.Configuration.ProfilePropertySettings In PropertySettings
                    x = x & ProfileCommon.GetPropertyValue(PropertySetting.Name) & ","
                Next
                x = x & vbCrLf
            End While

            Return x
        End Function

        Public Function GetUsersinRoleDataSet(ByVal roleId As Guid) As System.Data.DataSet
            Dim ds As New System.Data.DataSet
            Dim dt As New System.Data.DataTable
            dt = ds.Tables.Add("Users")

            dt.Columns.Add("UserName", Type.GetType("System.String"))
            dt.Columns.Add("UserId", Type.GetType("System.Guid"))
            dt.Columns.Add("IsAnonymous", Type.GetType("System.Boolean"))
            dt.Columns.Add("LastActivityDate", Type.GetType("System.DateTime"))

            Dim i As Integer = 0
            Dim users As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.AspnetUsers) = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.GetByRoleIdFromAspnetUsersInRoles(RoleId)
            Dim s As String = ""
            For Each user As Meanstream.Portal.Core.Entities.AspnetUsers In users
                Dim dr As System.Data.DataRow
                dr = dt.NewRow
                dr("Username") = user.UserName
                dr("UserId") = user.UserId
                dr("IsAnonymous") = user.IsAnonymous
                dr("LastActivityDate") = user.LastActivityDate
                dt.Rows.Add(dr)
            Next

            Return ds
        End Function

        Public Function CreateUser(ByVal username As String, ByVal email As String, ByVal password As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, _
                                   ByVal roles() As String, ByVal isApproved As Boolean, ByVal generatePassword As Boolean, ByVal sendEmailConfirmation As Boolean) As System.Web.Security.MembershipCreateStatus
            If generatePassword Then
                password = "temp001z"
            End If

            'Create Membership User
            Dim status As New System.Web.Security.MembershipCreateStatus()
            Dim MembershipUser As System.Web.Security.MembershipUser = System.Web.Security.Membership.CreateUser(username.Trim, password, email.Trim, passwordQuestion, passwordAnswer, isApproved, status)
            If status = System.Web.Security.MembershipCreateStatus.Success Then

                If generatePassword Then
                    Dim ResetPassword As String = MembershipUser.ResetPassword()
                    password = System.Web.Security.Membership.GeneratePassword(5, 0)
                    If MembershipUser.ChangePassword(ResetPassword, password) = False Then
                        Throw New ApplicationException("Reset password failed.")
                    End If
                End If

                'Default roles
                Meanstream.Portal.Core.Membership.MembershipService.Current.SetAutoAssignmentRolesForUser(username)
                For Each role As String In roles
                    Meanstream.Portal.Core.Membership.MembershipService.Current.AddUserToRole(username, role)
                Next
                If sendEmailConfirmation Then
                    Dim Response As Integer = Meanstream.Portal.Core.Messaging.MessagingService.Current.SendRegistrationConfirmation(MembershipUser.UserName, MembershipUser.Email, password)
                    If Response = 2 Or Response = 3 Then
                        Throw New ApplicationException("There was an error sending the confirmation email. Please check the smtp settings. The user was successfully created.")
                    End If
                End If
            End If
            Return status
        End Function

        Public Function CreateUser(ByVal username As String, ByVal email As String, ByVal password As String, ByVal roles() As String, ByVal isApproved As Boolean, ByVal generatePassword As Boolean, ByVal sendEmailConfirmation As Boolean) As System.Web.Security.MembershipCreateStatus
            Dim status As System.Web.Security.MembershipCreateStatus = Nothing

            If generatePassword Then
                password = "temp001z"
            End If

            Try
                'Create Membership User
                Dim MembershipUser As System.Web.Security.MembershipUser = System.Web.Security.Membership.CreateUser(username.Trim, password, email.Trim)
                MembershipUser.IsApproved = isApproved

                If generatePassword Then
                    Dim ResetPassword As String = MembershipUser.ResetPassword()
                    password = System.Web.Security.Membership.GeneratePassword(5, 0)
                    If MembershipUser.ChangePassword(ResetPassword, password) = False Then
                        Throw New ApplicationException("Reset password failed.")
                    End If
                End If

                'Default roles
                Meanstream.Portal.Core.Membership.MembershipService.Current.SetAutoAssignmentRolesForUser(username)
                For Each role As String In roles
                    Meanstream.Portal.Core.Membership.MembershipService.Current.AddUserToRole(username, role)
                Next
                If sendEmailConfirmation Then
                    Dim Response As Integer = Meanstream.Portal.Core.Messaging.MessagingService.Current.SendRegistrationConfirmation(MembershipUser.UserName, MembershipUser.Email, password)
                    If Response = 2 Or Response = 3 Then
                        Throw New ApplicationException("There was an error sending the confirmation email. Please check the smtp settings. The user was successfully created.")
                    End If
                End If
                status = System.Web.Security.MembershipCreateStatus.Success
            Catch ex As System.Web.Security.MembershipCreateUserException
                status = ex.StatusCode
            End Try

            Return status
        End Function

        Public Function CreateRole(ByVal name As String, ByVal Description As String, ByVal isPublic As Boolean, ByVal autoAssignment As Boolean) As Guid
            PortalTrace.WriteLine([String].Concat("CreateRole() ", AppFriendlyName, " #", ApplicationId, " name=", name))
            System.Web.Security.Roles.CreateRole(name)
            Dim exists As Role = Me.GetRoleByName(name)
            If exists IsNot Nothing Then
                Return Nothing
            End If
            Dim role As Meanstream.Portal.Core.Entities.MeanstreamRoles = New Meanstream.Portal.Core.Entities.MeanstreamRoles
            role.Id = Meanstream.Portal.Core.Data.DataRepository.AspnetRolesProvider.Find("RoleName=" & name)(0).RoleId
            role.IsPublic = isPublic
            role.RoleName = name
            role.AutoAssignment = autoAssignment
            role.Description = Description
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Insert(role)
            Return role.Id
        End Function

        Public Function GetUserRoles(ByVal username As String) As String()
            PortalTrace.WriteLine([String].Concat("GetUserRoles() ", AppFriendlyName, " #", ApplicationId, " username=", username))
            'get cached user roles
            Dim UserRoles() As String = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.MEANSTREAM_MENU_CACHE & "_" & username)
            If UserRoles Is Nothing Then
                UserRoles = System.Web.Security.Roles.GetRolesForUser(username)
                System.Web.HttpRuntime.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.MEANSTREAM_MENU_CACHE & "_" & username, UserRoles, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Cache")), TimeSpan.Zero)
            End If
            Return UserRoles
        End Function

        Public Function GetRolesForUser(ByVal username As String) As List(Of Role)
            PortalTrace.WriteLine([String].Concat("GetRolesForUser() ", AppFriendlyName, " #", ApplicationId, " username=", username))
            Dim userRoles() As String = Me.GetUserRoles(username)
            Dim roles As List(Of Role) = New List(Of Role)
            Dim query As Meanstream.Portal.Core.Data.MeanstreamRolesQuery = New Meanstream.Portal.Core.Data.MeanstreamRolesQuery
            query.AppendIn(Core.Entities.MeanstreamRolesColumn.RoleName, userRoles)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamRoles In entities
                Dim role As New Role(entity.Id)
                Dim manager As New RoleManager(role)
                manager.Bind(entity)
                roles.Add(role)
            Next
            Return roles
        End Function

        Public Function IsUserInRole(ByVal username As String, ByVal roleName As String) As Boolean
            Return System.Web.Security.Roles.IsUserInRole(Username, RoleName)
        End Function

        Public Function IsUserInRole(ByVal username As String, ByVal roleId As Guid) As Boolean
            Dim UserID As Guid = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.Find("UserName=" & Username).Item(0).UserId()
            Dim Count As Integer = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.Find("UserId=" & UserID.ToString & " And RoleID=" & RoleID.ToString).Count
            If Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Function GetAllRoles() As List(Of Role)
            Dim roles As List(Of Role) = New List(Of Role)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.GetAll()
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamRoles In entities
                Dim role As New Role(entity.Id)
                Dim manager As New RoleManager(role)
                manager.Bind(entity)
                roles.Add(role)
            Next
            Return roles
        End Function

        Public Function GetRoleById(ByVal roleId As Guid) As Role
            Dim roles As Role = New Membership.Role(roleId)
            Dim manager As New Meanstream.Portal.Core.Membership.RoleManager(roles)
            manager.LoadFromDatasource()
            Return roles
        End Function

        Public Function GetRoleByName(ByVal name As String) As Role
            Dim roles As Role = Nothing
            'hit Roles table
            Dim RoleList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & name)
            For Each role As Meanstream.Portal.Core.Entities.MeanstreamRoles In RoleList
                roles = New Membership.Role(role.Id)
                Dim manager As New Meanstream.Portal.Core.Membership.RoleManager(roles)
                manager.Bind(role)
            Next
            Return roles
        End Function

        Public Function GetAllUsersRole() As Role
            Dim Roles As Role = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.AppConstants.ALLUSERS)
            If Roles Is Nothing Then
                Roles = Me.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS)
                'cache the Permissions
                Meanstream.Portal.Core.Utilities.CacheUtility.Add(Meanstream.Portal.Core.AppConstants.ALLUSERS, Roles)
            End If
            Return Roles
        End Function

        Public Sub SetAutoAssignmentRolesForUser(ByVal username As String)
            PortalTrace.WriteLine([String].Concat("SetAutoAssignmentRolesForUser() ", AppFriendlyName, " #", ApplicationId, " username=", username))
            Dim roles As List(Of Role) = Me.GetAllRoles()
            For Each role As Role In roles
                If role.AutoAssignment Then
                    Me.AddUserToRole(UserName, role.Name)
                End If
            Next
        End Sub

        Public Sub AddUserToRole(ByVal username As String, ByVal roleName As String)
            PortalTrace.WriteLine([String].Concat("GetUserRoles() ", AppFriendlyName, " #", ApplicationId, " username=", username, " roleName=", roleName))
            System.Web.Security.Roles.AddUserToRole(Username, RoleName)
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES & "_" & username)
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES & "_ENTITIES_" & username)
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.MEANSTREAM_MENU_CACHE)
        End Sub

        Public Sub AddUserToRole(ByVal username As String, ByVal roleName As String, ByVal Notify As Boolean)
            If MembershipService.Current.IsUserInRole(username, roleName) Then
                Exit Sub
            End If
            Me.AddUserToRole(username, roleName)
            If Notify Then
                'Send Mail
                Core.Messaging.MessagingService.Current.SendAddRoleToUserEmail(username, System.Web.Security.Membership.GetUser(username).Email, roleName)
            End If
        End Sub

        Public Sub RemoveUserFromRole(ByVal username As String, ByVal roleName As String)
            If MembershipService.Current.IsUserInRole(username, roleName) Then
                System.Web.Security.Roles.RemoveUserFromRole(username, roleName)
            End If
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES & "_" & username)
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES & "_ENTITIES_" & username)
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.MEANSTREAM_MENU_CACHE & "_" & username)
        End Sub

        Public Function GetPreference(ByVal name As String) As Meanstream.Portal.Core.Membership.Preference
            Dim Preferences As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPreferenceProvider.Find("Name=" & name.Trim)
            If Preferences.Count = 0 Then
                Return Nothing
            End If
            Dim preference As New Meanstream.Portal.Core.Membership.Preference(Preferences(0).Id)
            Dim manager As New PreferenceManager(preference)
            manager.Bind(Preferences(0))
            Return preference
        End Function

        Public Function GetPreference(ByVal PreferenceId As Guid) As Preference  
            Dim Preferences As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPreferenceProvider.Find("PreferenceId=" & PreferenceId.ToString)
            If Preferences.Count = 0 Then
                Return Nothing
            End If
            Dim preference As New Meanstream.Portal.Core.Membership.Preference(Preferences(0).Id)
            Dim manager As New PreferenceManager(preference)
            manager.Bind(Preferences(0))
            Return preference
        End Function

        Public Function GetPreferences() As List(Of Preference)
            Dim enitities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPreferenceProvider.GetAll
            If enitities.Count = 0 Then
                Return Nothing
            End If
            Dim preferences As New List(Of Preference)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPreference In enitities
                Dim preference As New Meanstream.Portal.Core.Membership.Preference(entity.Id)
                Dim manager As New PreferenceManager(preference)
                manager.Bind(entity)
                preferences.Add(preference)
            Next
            Return preferences
        End Function

        Public Function CreatePreference(ByVal preference As String) As Guid
            PortalTrace.WriteLine([String].Concat("CreatePreference() ", AppFriendlyName, " #", ApplicationId, " preference=", preference))
            If Me.GetPreference(preference) IsNot Nothing Then
                Return Nothing
            End If
            Dim Preference1 As Meanstream.Portal.Core.Entities.MeanstreamPreference = New Meanstream.Portal.Core.Entities.MeanstreamPreference
            Preference1.Id = Guid.NewGuid
            Preference1.PreferenceId = Guid.NewGuid
            Preference1.Name = preference
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPreferenceProvider.Insert(Preference1)
            Return Preference1.Id
        End Function

        Public Function AddUserPreference(ByVal username As String, ByVal preferenceId As Guid, ByVal value As Object) As Guid
            Return Me.AddUserPreference(Me.GetUserGuid(Username), preferenceId, value)
        End Function

        Public Function AddUserPreference(ByVal userId As Guid, ByVal preferenceId As Guid, ByVal value As Object) As Guid
            PortalTrace.WriteLine([String].Concat("AddUserPreference() ", AppFriendlyName, " #", ApplicationId, " userId=", userId, " preferenceId=", preferenceId, " value=", value))
            Dim UserPreference As Meanstream.Portal.Core.Entities.MeanstreamUserPreference = New Meanstream.Portal.Core.Entities.MeanstreamUserPreference
            UserPreference.Id = Guid.NewGuid
            UserPreference.ParamValue = value
            UserPreference.PreferenceId = preferenceId
            UserPreference.UserId = userId
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserPreferenceProvider.Insert(UserPreference)
            Return UserPreference.Id
        End Function

        Public Sub UpdateUserPreference(ByVal userId As Guid, ByVal preferenceId As Guid, ByVal value As Object)
            Dim UserPreferences As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamUserPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserPreferenceProvider.Find("PreferenceId=" & PreferenceId.ToString & " AND UserId=" & UserId.ToString)
            If UserPreferences.Count = 0 Then
                Exit Sub
            End If
            Dim UserPreference As Meanstream.Portal.Core.Entities.MeanstreamUserPreference = UserPreferences(0)
            UserPreference.ParamValue = Value
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserPreferenceProvider.Update(UserPreference)
        End Sub

        Public Sub UpdateUserPreference(ByVal username As String, ByVal preferenceId As Guid, ByVal value As Object)
            Me.UpdateUserPreference(Me.GetUserGuid(Username), PreferenceId, Value)
        End Sub

        ''create a view for this
        Public Function GetUserPreference(ByVal userId As Guid, ByVal preference As String) As UserPreference
            Dim Preferences As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPreferenceProvider.Find("Name=" & preference.Trim)
            If Preferences.Count = 0 Then
                Return Nothing
            End If
            Dim UserPreferences As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamUserPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserPreferenceProvider.Find("PreferenceId=" & Preferences(0).PreferenceId.ToString & " AND UserId=" & userId.ToString)
            If UserPreferences.Count = 0 Then
                Return Nothing
            End If
            Dim userPreference As New UserPreference(UserPreferences(0).Id)
            userPreference.UserId = UserPreferences(0).UserId
            userPreference.PreferenceId = UserPreferences(0).PreferenceId
            userPreference.Value = UserPreferences(0).ParamValue
            Return userPreference
        End Function

        ''create a view for this
        Public Function GetUserPreference(ByVal Username As String, ByVal Preference As String) As UserPreference
            Return Me.GetUserPreference(Me.GetUserGuid(Username), Preference)
        End Function

        Public Function GetUserPreferences(ByVal username As String) As List(Of UserPreference)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamUserPreference) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserPreferenceProvider.Find("UserId=" & Me.GetUserGuid(username).ToString)
            If entities.Count = 0 Then
                Return Nothing
            End If
            Dim preferences As List(Of UserPreference) = New List(Of UserPreference)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamUserPreference In entities
                Dim preference As New UserPreference(entity.Id)
                preference.UserId = entity.UserId
                preference.Value = entity.ParamValue
                preferences.Add(preference)
            Next
            Return preferences
        End Function

        Public Function GetMeanstreamThemeForUser(ByVal Username As String) As String
            Dim Theme As String = ""
            If System.Web.HttpContext.Current.Session("MEANSTREAMUSERTHEME_" & Username) Is Nothing Then
                Dim UserId As Guid = Me.GetUserGuid(Username)
                Dim UserPreference As UserPreference = Me.GetUserPreference(UserId, MEANSTREAMUSERTHEME)
                If UserPreference Is Nothing Then
                    'add user to preference
                    Dim Preference As Preference = Me.GetPreference(MEANSTREAMUSERTHEME)
                    If Preference Is Nothing Then
                        Me.CreatePreference(MEANSTREAMUSERTHEME)
                        Preference = Me.GetPreference(MEANSTREAMUSERTHEME)
                    End If
                    Me.AddUserPreference(UserId, Preference.PreferenceId, "Meanstream.2011")
                    UserPreference = Me.GetUserPreference(UserId, MEANSTREAMUSERTHEME)
                End If
                Theme = UserPreference.Value
                System.Web.HttpContext.Current.Session("MEANSTREAMUSERTHEME_" & Username) = Theme

                Dim ThemePath As String = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "App_Themes/" & Theme
                System.Web.HttpContext.Current.Session("MEANSTREAMUSERTHEMEPATH") = ThemePath
            Else
                Theme = System.Web.HttpContext.Current.Session("MEANSTREAMUSERTHEME_" & Username)
            End If
            Return Theme
        End Function

        Public Sub UpdateMeanstreamThemeForUser(ByVal username As String, ByVal theme As String)
            PortalTrace.WriteLine([String].Concat("UpdateMeanstreamThemeForUser() ", AppFriendlyName, " #", ApplicationId, " username=", username, " theme=", theme))
            Dim UserId As Guid = Me.GetUserGuid(Username)
            Dim UserPreference As UserPreference = Me.GetUserPreference(UserId, MEANSTREAMUSERTHEME)
            Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamUserPreference
            entity.Id = UserPreference.Id
            entity.OriginalId = UserPreference.Id
            entity.PreferenceId = UserPreference.PreferenceId
            entity.UserId = UserPreference.UserId
            entity.ParamValue = theme
            If Not Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserPreferenceProvider.Update(entity) Then
                Throw New ApplicationException("theme update failed user preference ID = " & entity.Id.ToString & " userId = " & entity.UserId.ToString & " PreferenceId = " & entity.PreferenceId.ToString)
            End If
            System.Web.HttpContext.Current.Session("MEANSTREAMUSERTHEME_" & Username) = Theme
            Dim ThemePath As String = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "App_Themes/" & theme
            System.Web.HttpContext.Current.Session("MEANSTREAMUSERTHEMEPATH") = ThemePath
        End Sub

        Public Function GetPermissions() As List(Of Permission)
            Dim list As List(Of Permission) = Utilities.CacheUtility.GetCachedObject(Utilities.CacheUtility.PERMISSIONS)
            If list Is Nothing Then
                list = New List(Of Permission)
                Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPermissionProvider.GetAll
                For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPermission In entities
                    Dim permission As New Permission(entity.Id)
                    Dim manager As New PermissionManager(permission)
                    manager.Bind(entity)
                    list.Add(permission)
                Next
                System.Web.HttpContext.Current.Cache.Insert(Utilities.CacheUtility.PERMISSIONS, list, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.Cache")), TimeSpan.Zero)
            End If
            Return list
        End Function

        Public Function GetPermission(ByVal type As Permission.PermissionType) As Permission
            Dim list As List(Of Permission) = Me.GetPermissions
            For Each permission As Permission In list
                'PortalTrace.Fail([Enum].GetName(GetType(Permission.PermissionType), type), DisplayMethodInfo.DoNotDisplay)
                If permission.Code = "SYSTEM_MODULE_DEFINITION" Then
                    permission.Code = "SYSTEM_MODULE"
                End If
                If permission.Code & "_" & permission.Key = [Enum].GetName(GetType(Permission.PermissionType), type) Then
                    Return permission
                End If
            Next
            Return Nothing
        End Function

        Public Function GetUsersInRole(ByVal roleId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.AspnetUsers)
            Return Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.GetByRoleIdFromAspnetUsersInRoles(roleiD)
        End Function

        'Public Function GetEmailsInCSV() As String
        '    Log.Debug("Meanstream.Portal.Core.UserProfileManager.GetEmailsInCSV()")

        '    Dim x As String = ""
        '    Dim MembershipUserCollection As MembershipUserCollection = Membership.GetAllUsers()

        '    ' Get record count
        '    Dim n As Integer = MembershipUserCollection.Count
        '    Dim k As Integer = 0
        '    n = n - 1

        '    Dim Enumerator As IEnumerator = MembershipUserCollection.GetEnumerator
        '    While Enumerator.MoveNext()
        '        x = x & Enumerator.Current.Email & vbCrLf
        '    End While

        '    Dim Emails As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.Email) = Meanstream.Portal.Core.Data.DataRepository.EmailProvider.GetAll
        '    For Each Email As Meanstream.Portal.Core.Entities.Email In Emails
        '        x = x & Email.Email & vbCrLf
        '    Next

        '    Return x
        'End Function
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

