Imports System.Net
Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Core.EntityModel
Imports Meanstream.Core.Repository

Partial Class Login
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Form.DefaultButton = Me.btnSignin.UniqueID

        If Not IsPostBack Then
            Dim singleSignOn As Boolean = System.Configuration.ConfigurationManager.AppSettings("Meanstream.SingleSignOn")
            Dim action As [String] = Request.QueryString("action")

            If Not [String].IsNullOrEmpty(action) AndAlso action.ToLower.Equals("logout") Then
                If singleSignOn Then
                    Me.SingleSignOnSignOut()
                Else
                    'sign out
                    System.Web.Security.FormsAuthentication.SignOut()
                End If

                Dim returnUrl As [String] = Request.QueryString("returnUrl")
                If Not [String].IsNullOrEmpty(returnUrl) Then
                    Response.Redirect(returnUrl)
                Else
                    Response.Redirect(FormsAuthentication.DefaultUrl)
                End If
            Else
                If singleSignOn Then
                    Me.SingleSignOnSignInRequest()
                End If
            End If

            txtUsername.Focus()
        End If
    End Sub

    Protected Sub btnSignin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSignin.Click
        If Membership.ValidateUser(txtUsername.Text, txtPassword.Text) Then
            Dim singleSignOn As Boolean = System.Configuration.ConfigurationManager.AppSettings("Meanstream.SingleSignOn")
            If singleSignOn Then
                Me.SingleSignOnSignIn()
            Else
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, False)
            End If
        Else
            lblInvalidLogin.Text = "Invalid Credentials"
            lblInvalidLogin.Visible = True
        End If
    End Sub

    Private Sub SingleSignOnSignIn()
        'create auth User
        Dim user As New AuthUser
        user.Id = Guid.NewGuid
        user.Username = txtUsername.Text
        'register sso user
        AuthUtil.SignIn(user)
        'get domain list
        Dim strDomains As String = AuthUtil.GetDomains(Request.Url.Host)
        'set our auth cookie
        FormsAuthentication.SetAuthCookie(txtUsername.Text, False)
        'split to find next domain
        Dim domain() As String = strDomains.Split(",")
        'redirect to current domain
        Dim url As String = String.Concat(Request.Url.Scheme, "://", domain(0), System.Web.Security.FormsAuthentication.GetRedirectUrl(user.Username, False).Replace("~/", "/"))
        If url.Contains("?") Then
            url = url & "&"
        Else
            url = url & "?"
        End If
        url = url & "token=" & user.Id.ToString & "&username=" & user.Username & "&source=" & Request.Url.Host & "&domains=" & strDomains
        Response.Redirect(url)
    End Sub

    Private Sub SingleSignOnSignInRequest()
        Dim domains As [String] = Request.QueryString("domains")
        Dim source As [String] = Request.QueryString("source")
        Dim token As [String] = Request.QueryString("token")
        Dim username As [String] = Request.QueryString("username")
        Dim currentDomain As String = Request.Url.Host

        If token <> Nothing And username <> Nothing And source <> Nothing And domains <> Nothing Then
            Dim user As New AuthUser
            user.Id = New Guid(token)
            user.Username = username

            'if token and username match...
            If AuthUtil.IsValidUser(user) Then
                'set our auth cookie
                FormsAuthentication.SetAuthCookie(username, False)
                'remove current domain from list and send to next one
                domains = domains.Replace(currentDomain & ",", "").Replace(currentDomain, "").Trim
                domains = domains.TrimStart(",")
                domains = domains.TrimEnd(",")
                'if no more domains are in list then send back to source
                If domains.Length = 0 Then
                    Dim rUrl As String = String.Concat(Request.Url.Scheme, "://", source, System.Web.Security.FormsAuthentication.GetRedirectUrl(username, False).Replace("~/", "/"))
                    'remove signon params
                    If rUrl.IndexOf("?token") > -1 Then
                        rUrl = rUrl.Substring(0, rUrl.IndexOf("?token"))
                    ElseIf rUrl.IndexOf("&token") > -1 Then
                        rUrl = rUrl.Substring(0, rUrl.IndexOf("&token"))
                    End If
                    Response.Redirect(rUrl)
                End If
                'split to find next domain
                Dim domain() As String = domains.Split(",")
                'redirect to the next domain
                Dim url As String = String.Concat(Request.Url.Scheme, "://", domain(0), System.Web.Security.FormsAuthentication.GetRedirectUrl(username, False).Replace("~/", "/"))
                If url.Contains("?") Then
                    url = url & "&"
                Else
                    url = url & "?"
                End If
                url = url & "token=" & user.Id.ToString & "&username=" & username & "&source=" & source & "&domains=" & domains
                Response.Redirect(url)
            End If
        End If
    End Sub

    Private Sub SingleSignOnSignOut()
        Dim domains As [String] = Request.QueryString("domains")
        Dim source As [String] = Request.QueryString("source")
        Dim currentDomain As String = Request.Url.Host

        If domains <> Nothing And source <> Nothing Then
            'sign out of current domain
            System.Web.Security.FormsAuthentication.SignOut()
            'remove current domain from list and send to next one 
            domains = domains.Replace(currentDomain & ",", "").Replace(currentDomain, "").Trim
            domains = domains.TrimStart(",")
            domains = domains.TrimEnd(",")
            'if no more domains are in list then send back to source default url
            If domains.Trim.Length = 0 Then
                AuthUtil.SignOut(Profile.UserName)
                Dim defaultUrl As String = String.Concat(Request.Url.Scheme, "://", source, System.Web.Security.FormsAuthentication.DefaultUrl.Replace("~/", "/"))
                Response.Redirect(defaultUrl)
            End If
        Else
            'sign out of current domain
            System.Web.Security.FormsAuthentication.SignOut()
            'if no domains are present start our requests to sign out
            Dim strDomains As String = AuthUtil.GetDomains(Request.Url.Host)
            'split to find next domain
            Dim domain() As String = strDomains.Split(",")
            'redirect to current domain
            Dim loginUrl As String = String.Concat(Request.Url.Scheme, "://", domain(0), System.Web.Security.FormsAuthentication.LoginUrl.Replace("~/", "/"), "?action=logout&source=" & currentDomain & "&domains=" & strDomains)
            Response.Redirect(loginUrl)
        End If
    End Sub

    Protected Sub btnForgotPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnForgotPassword.Click
        Me.tblLogin.Visible = False
        Me.PasswordRecovery1.Visible = True
        Me.PasswordRecovery1.MailDefinition.Priority = Net.Mail.MailPriority.High

        Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_FORGOT_PASSWORD)
        Me.PasswordRecovery1.MailDefinition.Subject = SubjectSettings.Value

        Dim From As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SMTP_FROM)
        Me.PasswordRecovery1.MailDefinition.From = From.Value
        Me.PasswordRecovery1.DataBind()
    End Sub
End Class

'Public Class AuthUser
'    Inherits Meanstream.Core.EntityModel.EntityBase

'    <EntityField("username", System.Data.SqlDbType.NVarChar)> _
'    Public Property Username() As String
'        Get
'            Return m_username
'        End Get
'        Set(value As String)
'            m_username = value
'        End Set
'    End Property
'    Private m_username As String

'End Class

'Public Class AuthUtil

'    Public Shared Sub SignIn(ByVal user As AuthUser)
'        If user.Id = Nothing Then
'            user.Id = Guid.NewGuid
'        End If
'        If String.IsNullOrEmpty(user.Username) Then
'            Throw New ArgumentException("Username")
'        End If
'        'clear all sessions if needed
'        SignOut(user.Username)
'        'create new session
'        Repository.Insert(user)
'    End Sub

'    Public Shared Function IsValidUser(ByVal user As AuthUser) As Boolean
'        If Repository.GetById(Of AuthUser)(user.Id) IsNot Nothing Then
'            Return True
'        End If
'        Return False
'    End Function

'    Public Shared Sub SignOut(ByVal username As String)
'        Dim query As New Query
'        query.AppendEquals("Username", username)
'        Dim list As List(Of AuthUser) = Repository.Find(Of AuthUser)(query)
'        If list IsNot Nothing And list.Count > 0 Then
'            For Each item As AuthUser In list
'                Repository.Delete(item.Id)
'            Next
'        End If
'    End Sub

'    Public Shared Function GetDomains(ByVal currentDomain As String) As String
'        Dim portals As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPortals) = _
'                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamPortalsProvider.GetAll()

'        Dim strDomains As String = ""

'        For Each portal As Meanstream.Portal.Core.Entities.MeanstreamPortals In portals
'            If portals.IndexOf(portal) = 0 Then
'                strDomains = strDomains & portal.Domain
'            Else
'                strDomains = strDomains & "," & portal.Domain
'            End If
'        Next

'        'remove calling domain
'        strDomains = strDomains.Replace(currentDomain & ",", "").Replace(currentDomain, "")
'        'trim first and last ","
'        strDomains = strDomains.TrimStart(",")
'        strDomains = strDomains.TrimEnd(",")

'        Return strDomains.Trim
'    End Function
'End Class