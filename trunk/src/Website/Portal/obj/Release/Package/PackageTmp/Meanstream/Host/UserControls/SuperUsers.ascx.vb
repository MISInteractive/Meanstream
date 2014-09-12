
Partial Class Meanstream_Host_UserControls_SuperUsers
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim Action As String = Request.Params("Action")
            If Action <> Nothing And Action <> "" Then
                If Action = "Remove" Then
                    Dim UserName As String = Request.Params("UserName")
                    Me.RemoveUserFromRole(UserName)
                End If
            End If

            Me.btnAddUserToGroup.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-add-group-user.png"
            Me.BindGrid()
        End If
    End Sub

    Protected Sub BindGrid()
        Dim Role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName("Host")
        Me.lblRoleName.Text = Role.Name
        Me.ddlAvailableUsers.DataTextField = "Username"
        Me.ddlAvailableUsers.DataValueField = "Username"
        Me.ddlAvailableUsers.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllUsersDataSet
        Me.ddlAvailableUsers.DataBind()
        Me.UsersInGroupGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsersinRoleDataSet(Role.Id)
        Me.UsersInGroupGrid.DataBind()
    End Sub

    Protected Sub RemoveUserFromRole(ByVal UserName As String)
        If UserName = Nothing Or UserName = "" Then
            Return
        End If

        Dim Role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName("Host")
        Dim RoleName As String = Role.Name
        Meanstream.Portal.Core.Membership.MembershipService.Current.RemoveUserFromRole(UserName, RoleName)

        Me.BindGrid()
    End Sub

    Protected Sub UsersInGroupGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles UsersInGroupGrid.RowDeleting

    End Sub

    Protected Sub UsersInGroupGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles UsersInGroupGrid.RowCommand
        Dim rowIndex As Integer = CInt(e.CommandArgument)
        Dim Id As String = DirectCast(UsersInGroupGrid.Rows(rowIndex).FindControl("lblID"), Label).Text

        Select Case e.CommandName
            Case "Remove"
                Try
                    RemoveUserFromRole(Id)
                    Me.lblStatus.Text = "User has been removed"
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
        End Select
    End Sub

    Protected Sub btnAddUserToGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUserToGroup.Click
        If Me.ddlAvailableUsers.SelectedText = "Select User" Then
            Me.btnAddUserToGroup.ThrowFailure = True
            Me.btnAddUserToGroup.FailMessage = "Select a user"
            Return
        End If

        Dim RoleManager As Meanstream.Portal.Core.Membership.MembershipService = Meanstream.Portal.Core.Membership.MembershipService.Current
        If RoleManager.IsUserInRole(Me.ddlAvailableUsers.SelectedValue, Me.lblRoleName.Text) Then
            Return
        End If
        RoleManager.AddUserToRole(Me.ddlAvailableUsers.SelectedValue, Me.lblRoleName.Text)
        If Me.chkSendNotification.Checked Then
            'Send Mail
            Meanstream.Portal.Core.Messaging.MessagingService.Current.SendAddUserToRoleEmail(Me.ddlAvailableUsers.SelectedValue, Membership.GetUser(Me.ddlAvailableUsers.SelectedValue).Email, Me.lblRoleName.Text)
        End If
        Me.BindGrid()
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
