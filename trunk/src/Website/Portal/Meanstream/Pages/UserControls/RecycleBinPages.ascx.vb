
Partial Class Meanstream_Pages_UserControls_RecycleBinPages
    Inherits System.Web.UI.UserControl

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Dim PageList As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Content.ContentService.Current.GetRecycleBin(portalId)
        If PageList.Count > 0 Then
            Me.PagesGrid.DataSource = PageList
            Me.PagesGrid.DataBind()
        Else
            Me.Container.Visible = False
            Me.lblMessage.Text = "No Records Found"
        End If
    End Sub

    Protected Sub PagesGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles PagesGrid.PageIndexChanging
        PagesGrid.PageIndex = e.NewPageIndex
        PagesGrid.EditIndex = -1
        BindGrid()
    End Sub

    Public Function CanEdit(ByVal PageID As Guid) As String
        Return "Edit"
    End Function

    Public Function GetSkinName(ByVal SkinID As Guid) As String
        Return Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(SkinID).Name
    End Function

    Protected Sub PagesGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles PagesGrid.RowDeleting
    End Sub

    Protected Sub PagesGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles PagesGrid.RowCommand
        Dim rowIndex As Integer = CInt(e.CommandArgument)
        Dim pageId As String = DirectCast(PagesGrid.Rows(rowIndex).FindControl("lblID"), Label).Text

        Select Case e.CommandName
            Case "Delete"
                Try
                    Dim page As Meanstream.Portal.Core.Content.Page = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(pageId))
                    Dim manager As New Meanstream.Portal.Core.Content.PageManager(page)
                    manager.Delete()
                    Me.PagesGrid.EditIndex = -1
                    BindGrid()
                    Me.lblStatus.Text = "Page has been successfully deleted."
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
            Case "Restore"
                Try
                    Dim page As Meanstream.Portal.Core.Content.Page = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(pageId))
                    Dim manager As New Meanstream.Portal.Core.Content.PageManager(page)
                    manager.Restore()
                    Response.Redirect("Default.aspx?ctl=ViewPage&PageID=" & pageId)
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
        End Select
    End Sub
End Class
