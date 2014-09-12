
Partial Class Meanstream_Administration_UserControls_Roles
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'BindGrid()
        Me.btnCreateRole.TargetControlId = Me.CreateRoleTarget.ClientID
        Me.btnCreateUser.TargetControlId = Me.CreateUserTarget.ClientID
    End Sub

    'Private Sub BindGrid()
    '    Me.RoleGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllRoles()
    '    Me.RoleGrid.DataBind()
    'End Sub

    'Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
    '    If TrueOrFalse Then
    '        Return "yes"
    '    End If
    '    Return "no"
    'End Function

    'Private Function CanDelete(ByVal RoleName As String) As Boolean
    '    If RoleName = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR _
    '    Or RoleName = Meanstream.Portal.Core.AppConstants.ALLUSERS _
    '    Or RoleName = Meanstream.Portal.Core.AppConstants.REGISTERED_USERS _
    '    Or RoleName = Meanstream.Portal.Core.AppConstants.CONTENT_ADMINISTRATOR _
    '    Or RoleName = Meanstream.Portal.Core.AppConstants.SECURITY_ADMINISTRATOR _
    '    Or RoleName = Meanstream.Portal.Core.AppConstants.ECOMMERCE_ADMINISTRATOR _
    '    Or RoleName = Meanstream.Portal.Core.AppConstants.HOST Then
    '        Return False
    '    End If
    '    Return True
    'End Function

    'Protected Sub RoleGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles RoleGrid.RowDeleting

    'End Sub

    'Protected Sub RoleGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RoleGrid.RowCommand

    '    Select Case e.CommandName
    '        Case "Delete"
    '            Dim rowIndex As Integer = CInt(e.CommandArgument)
    '            Dim RoleName As String = DirectCast(RoleGrid.Rows(rowIndex).FindControl("RoleName"), Label).Text

    '            Try
    '                Dim role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName(RoleName)
    '                Dim manager As New Meanstream.Portal.Core.Membership.RoleManager(role)
    '                manager.Delete()

    '                Me.RoleGrid.EditIndex = -1
    '                BindGrid()
    '                Me.lblStatus.Text = "The role has been deleted"
    '            Catch ex As Exception
    '                Me.lblStatus.Text = ex.Message
    '            End Try
    '    End Select

    'End Sub

    'Protected Sub RoleGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RoleGrid.PageIndexChanging
    '    RoleGrid.PageIndex = e.NewPageIndex
    '    RoleGrid.EditIndex = -1
    '    BindGrid()
    'End Sub

    'Protected Sub RoleGrid_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RoleGrid.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim RoleName As String = DataBinder.Eval(e.Row.DataItem, "Name")
    '        Dim RoleID As String = DataBinder.Eval(e.Row.DataItem, "Id").ToString

    '        Dim Target As LinkButton = New LinkButton
    '        Target.ID = "Target"
    '        Target.Text = RoleName

    '        e.Row.Cells(RoleGrid.Columns.Count - 1).Controls.Item(0).FindControl("phRole").Controls.Add(Target)

    '        Dim Window As Meanstream.Web.UI.Window = New Meanstream.Web.UI.Window
    '        Window.SkinID = "Window"
    '        Window.Width = "900"
    '        Window.Height = "500"
    '        Window.ShowLoader = "true"
    '        Window.ShowUrl = "true"
    '        Window.Title = "Manage Role"
    '        Window.NavigateUrl = "Module.aspx?ctl=ManageRole&RoleID=" & RoleID
    '        Window.OnClientClose = "windowClose()"
    '        Window.TargetControlId = Target.ClientID

    '        e.Row.Cells(RoleGrid.Columns.Count - 1).Controls.Add(Window)

    '        Dim DeleteButton As ImageButton = e.Row.Cells(1).Controls(0)
    '        If Not Me.CanDelete(RoleName) Then
    '            DeleteButton.Visible = False
    '        End If

    '    End If
    'End Sub
End Class
