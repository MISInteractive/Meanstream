
Partial Class Meanstream_Administration_UserControls_Users
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            'Page.Form.DefaultButton = Me.btnSearch.UniqueID
            Me.txtSearch.Focus()
            Me.btnCreateRole.TargetControlId = Me.CreateRoleTarget.ClientID
            Me.btnCreateUser.TargetControlId = Me.CreateUserTarget.ClientID
            'Me.btnSearch.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-search.gif"

            If Request.Params("Action") <> Nothing Then
                'Me.BindActions(Request.Params("Action"))
            Else
                'BindGrid()
            End If
        End If
    End Sub

    'Private Sub BindGrid()
    '    Dim Users As System.Data.DataSet = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsers

    '    If Users.Tables(0).Rows.Count > 0 Then
    '        Me.UsersGrid.DataSource = Users
    '        Me.UsersGrid.DataBind()
    '    Else
    '        Me.Container.Visible = False
    '    End If
    'End Sub

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    If Me.txtSearch.Text = "" Then
    '        Exit Sub
    '    End If

    '    Me.UsersGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.SearchForUser(Me.txtSearch.Text, Me.txtSearch.Text)
    '    Me.UsersGrid.DataBind()
    'End Sub

    'Private Sub BindActions(ByVal Action As String)
    '    Dim Users As System.Data.DataSet = Nothing
    '    If Action = "Online" Then
    '        Users = Meanstream.Portal.Core.Membership.MembershipService.Current.GetNewUsersByLastActivityDateRange(Date.Now.AddHours(-1), Date.Now)
    '    End If
    '    If Action = "New" Then
    '        Users = Meanstream.Portal.Core.Membership.MembershipService.Current.GetNewUsersByDateRange(Date.Now.AddDays(-30), Date.Now)
    '    End If

    '    If Users.Tables(0).Rows.Count > 0 Then
    '        Me.UsersGrid.DataSource = Users
    '        Me.UsersGrid.DataBind()
    '    Else
    '        Me.Container.Visible = False
    '    End If
    'End Sub

    'Protected Sub UsersGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles UsersGrid.PageIndexChanging
    '    UsersGrid.PageIndex = e.NewPageIndex
    '    UsersGrid.EditIndex = -1
    '    BindGrid()
    'End Sub

    Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
        If TrueOrFalse Then
            Return "yes"
        End If
        Return "no"
    End Function

    'Protected Sub UsersGrid_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles UsersGrid.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim UserId As String = DataBinder.Eval(e.Row.DataItem, "UserId").ToString
    '        Dim Username As String = DataBinder.Eval(e.Row.DataItem, "Username")

    '        Dim Target As LinkButton = New LinkButton
    '        Target.ID = "Target"
    '        Target.Text = Username

    '        e.Row.Cells(UsersGrid.Columns.Count - 1).Controls.Item(0).FindControl("phUser").Controls.Add(Target)

    '        Dim Window As Meanstream.Web.UI.Window = New Meanstream.Web.UI.Window
    '        Window.SkinID = "Window"
    '        Window.Width = "900"
    '        Window.Height = "500"
    '        Window.ShowLoader = "true"
    '        Window.ShowUrl = "true"
    '        Window.Title = "Manage User"
    '        Window.NavigateUrl = "Module.aspx?ctl=ManageUser&uid=" & UserId
    '        Window.OnClientClose = "windowClose()"
    '        Window.TargetControlId = Target.ClientID

    '        e.Row.Cells(UsersGrid.Columns.Count - 1).Controls.Add(Window)

    '        If Username = "admin" Or Username = "host" Then
    '            Dim DeleteButton As ImageButton = e.Row.Cells(1).Controls(0)
    '            DeleteButton.Visible = False
    '        End If

    '    End If
    'End Sub

    'Protected Sub UsersGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles UsersGrid.RowDeleting

    'End Sub

    'Protected Sub UsersGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles UsersGrid.RowCommand

    '    Select Case e.CommandName
    '        Case "Delete"
    '            Dim rowIndex As Integer = CInt(e.CommandArgument)
    '            Dim UserName As String = DirectCast(UsersGrid.Rows(rowIndex).FindControl("UserName"), Label).Text

    '            Try
    '                Meanstream.Portal.Core.Membership.MembershipService.Current.DeleteUser(UserName)

    '                Me.UsersGrid.EditIndex = -1
    '                BindGrid()
    '                Me.lblStatus.Text = "The User has been deleted"
    '            Catch ex As Exception
    '                Me.lblStatus.Text = ex.Message
    '            End Try
    '    End Select

    'End Sub
End Class
