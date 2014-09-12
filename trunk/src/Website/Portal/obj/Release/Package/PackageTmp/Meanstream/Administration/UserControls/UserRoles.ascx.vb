
Partial Class Meanstream_Administration_UserControls_UserRoles
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim Action As String = Request.Params("Action")
            If Action <> Nothing And Action <> "" Then
                If Action = "Remove" Then
                    Dim RoleName As String = Request.Params("RoleName")
                    Dim UserName As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("uid")))
                    Me.RemoveUserFromRole(UserName, RoleName)
                End If
            End If
            Me.btnAddGroupToUser.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-add-group-user.png"
            Me.LoadDefault()
        End If
    End Sub

    Protected Sub LoadDefault()
        Dim Username As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("uid")))
        Me.lblUsername.Text = Username
        Me.lblUsername2.Text = Username
        Me.lblUsername3.Text = Username
        Me.AvailableRoles.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllRoles()
        Me.AvailableRoles.DataTextField = "Name"
        Me.AvailableRoles.DataValueField = "Name"
        Me.AvailableRoles.DataBind()
        Me.UserGroupList.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRolesForUser(Username)
        Me.UserGroupList.DataBind()
    End Sub

    Protected Sub RemoveUserFromRole(ByVal Username As String, ByVal RoleName As String)
        Dim result As String = ""
        If Username Is Nothing Or Username = "" Then
            Return
        End If
        If RoleName = Nothing Or RoleName = "" Then
            Return
        End If
        Meanstream.Portal.Core.Membership.MembershipService.Current.RemoveUserFromRole(Username, RoleName)
        Me.LoadDefault()
    End Sub

    Protected Sub btnAddGroupToUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddGroupToUser.Click
        Dim result As String = ""
        If Me.AvailableRoles.SelectedValue = Nothing Then
            Me.btnAddGroupToUser.ThrowFailure = True
            Me.btnAddGroupToUser.FailMessage = "Please select a role"
            Return
        End If
        Meanstream.Portal.Core.Membership.MembershipService.Current.AddUserToRole(Me.lblUsername.Text, Me.AvailableRoles.SelectedValue)
        If Me.chkSendNotification.Checked Then
            'Send Mail
            Meanstream.Portal.Core.Messaging.MessagingService.Current.SendAddUserToRoleEmail(Me.lblUsername.Text, Membership.GetUser(Me.lblUsername.Text).Email, Me.AvailableRoles.SelectedValue)
        End If
        Me.LoadDefault()
    End Sub

    Public Function DisplayRemove(ByVal Rolename As String) As String
        If Rolename = Meanstream.Portal.Core.AppConstants.HOST Then
            Return ""
        End If

        If Rolename = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Then
            If Me.lblUsername.Text = "admin" Or Me.lblUsername.Text = "host" Then
                Return ""
            End If
        End If

        Return "Remove"
    End Function

    Public Function DisplayRemoveImage(ByVal Rolename As String) As String
        If Rolename = Meanstream.Portal.Core.AppConstants.HOST Then
            Return ""
        End If

        If Rolename = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Then
            If Me.lblUsername.Text = "admin" Or Me.lblUsername.Text = "host" Then
                Return ""
            End If
        End If

        Return "<div class='icon-remove'></div>"
    End Function
End Class
