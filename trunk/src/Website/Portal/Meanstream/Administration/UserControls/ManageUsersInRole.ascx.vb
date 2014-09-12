
Partial Class Meanstream_Administration_UserControls_ManageUsersInRole
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim Action As String = Request.Params("Action")
            If Action <> Nothing And Action <> "" Then
                If Action = "Remove" Then
                    Dim RoleID As String = Request.Params("RoleID")
                    Dim UserName As String = Request.Params("UserName")
                    Me.RemoveUserFromRole(UserName, New Guid(RoleID))
                End If
            End If

            'Me.BindGrid()
            Me.btnAddUserToGroup.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-add-group-user.png"
        End If
    End Sub

    'Protected Sub BindGrid()
    '    Me.Container.Visible = True
    '    Me.lblRoleName.Text = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(New Guid(Request.Params("RoleID").ToString())).Name
    '    Me.ddlAvailableUsers.DataTextField = "Username"
    '    Me.ddlAvailableUsers.DataValueField = "Username"
    '    Me.ddlAvailableUsers.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllUsersDataSet
    '    Me.ddlAvailableUsers.DataBind()

    '    Dim UsersDataSet As System.Data.DataSet = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsersinRoleDataSet(New Guid(Request.Params("RoleID").ToString))
    '    Dim DataView As System.Data.DataView = New System.Data.DataView(UsersDataSet.Tables(0))

    '    If DataView.Count = 0 Then
    '        Me.Container.Visible = False
    '        Exit Sub
    '    End If

    '    Me.UsersInRoleGrid.DataSource = UsersDataSet
    '    Me.UsersInRoleGrid.DataBind()
    'End Sub

    'Protected Sub UsersInRoleGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles UsersInRoleGrid.PageIndexChanging
    '    UsersInRoleGrid.PageIndex = e.NewPageIndex
    '    UsersInRoleGrid.EditIndex = -1
    '    BindGrid()
    'End Sub

    Protected Sub RemoveUserFromRole(ByVal UserName As String, ByVal RoleID As Guid)
        If UserName = Nothing Or UserName = "" Then
            Return
        End If

        If RoleID = Nothing Then
            Return
        End If

        Dim RoleName As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(RoleID).Name
        Meanstream.Portal.Core.Membership.MembershipService.Current.RemoveUserFromRole(UserName, RoleName)

        Response.Redirect("Module.aspx?ctl=ManageUsersInRole&Action=ManageUsers&RoleID=" & Request.Params("RoleID"))
    End Sub

    'Protected Sub UsersInGroupGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles UsersInRoleGrid.RowDeleting

    'End Sub

    'Protected Sub UsersInGroupGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles UsersInRoleGrid.RowCommand
    '    Dim result As String = ""
    '    Me.lblStatus.Text = ""
    '    Dim rowIndex As Integer = CInt(e.CommandArgument)
    '    Dim Id As String = DirectCast(UsersInRoleGrid.Rows(rowIndex).FindControl("lblID"), Label).Text

    '    Select Case e.CommandName
    '        Case "Remove"
    '            RemoveUserFromRole(Id, New Guid(Request.Params("RoleID").ToString))
    '    End Select
    'End Sub

    Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
        If TrueOrFalse Then
            Return "yes"
        End If
        Return "no"
    End Function

    Protected Sub btnAddUserToGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUserToGroup.Click
        Dim result As String = ""
        Me.lblStatus.Text = ""

        If Me.ddlAvailableUsers.SelectedValue = Nothing Then
            Me.btnAddUserToGroup.ThrowFailure = True
            Me.btnAddUserToGroup.FailMessage = "Please select a user"
            Exit Sub
        End If

        If Meanstream.Portal.Core.Membership.MembershipService.Current.IsUserInRole(Me.ddlAvailableUsers.SelectedValue, Me.lblRoleName.Text) Then
            Me.btnAddUserToGroup.ThrowFailure = True
            Me.btnAddUserToGroup.FailMessage = "User exists in role"
            Exit Sub
        End If

        Meanstream.Portal.Core.Membership.MembershipService.Current.AddUserToRole(Me.ddlAvailableUsers.SelectedValue, Me.lblRoleName.Text)

        If Me.chkSendNotification.Checked Then
            'Send Mail
            Meanstream.Portal.Core.Messaging.MessagingService.Current.SendAddUserToRoleEmail(Me.ddlAvailableUsers.SelectedValue, Membership.GetUser(Me.ddlAvailableUsers.SelectedValue).Email, Me.lblRoleName.Text)
        End If

        Me.btnAddUserToGroup.SuccessMessage = "User successfully added to role"
        Response.Redirect("Module.aspx?ctl=ManageUsersInRole&Action=ManageUsers&RoleID=" & Request.Params("RoleID"))
    End Sub

    Public Function CanRemove(ByVal Username As String) As String
        If Me.lblRoleName.Text = "Host" Then
            If Username = "host" Then
                Return ""
            End If
        End If

        Return "Remove"
    End Function
End Class
