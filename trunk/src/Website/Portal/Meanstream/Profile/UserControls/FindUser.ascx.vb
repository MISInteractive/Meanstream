
Partial Class Meanstream_Profile_UserControls_FindUser
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Request.Params("Username") <> Nothing And Request.Params("Username") <> "Find User" And Request.Params("Username") <> "undefined" Then
                Me.txtUsername.Text = Request.Params("Username")
                Me.litUsername.Value = Request.Params("Username")
            End If
            Me.BindGrid()
            Me.btnSearch.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-search.gif"
        End If
    End Sub

    Protected Sub BindGrid()
        Me.UsersGrid.DataSource = Meanstream.Portal.Core.Utilities.AppUtility.AlphabeticSort(Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsers.Tables(0), "UserName", 0)
        Me.UsersGrid.DataBind()
    End Sub

    Protected Sub UsersInRoleGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles UsersGrid.PageIndexChanging
        UsersGrid.PageIndex = e.NewPageIndex
        UsersGrid.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Me.txtUsername.Text = "" Then
            Me.UsersGrid.DataSource = Meanstream.Portal.Core.Utilities.AppUtility.AlphabeticSort(Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsers.Tables(0), "UserName", 1)
            Me.UsersGrid.DataBind()
            Exit Sub
        End If

        Me.UsersGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.SearchForUser(Me.txtUsername.Text)
        Me.UsersGrid.DataBind()
    End Sub


    Protected Sub UsersGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles UsersGrid.RowCommand
        Me.lblStatus.Text = ""
        Dim Username As String = e.CommandArgument

        Select Case e.CommandName
            Case "Select"
                Me.litUsername.Value = Username
                Me.lblStatus.Text = "You have selected " & Username
        End Select
    End Sub
End Class
