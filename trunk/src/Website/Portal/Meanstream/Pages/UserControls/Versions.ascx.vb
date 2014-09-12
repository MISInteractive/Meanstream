
Partial Class Meanstream_Pages_UserControls_Versions
    Inherits System.Web.UI.UserControl


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub


    Dim Versions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPageVersion) = Nothing
    Private Sub BindGrid()
        Versions = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersionsByPageID(New Guid(Request.Params(Meanstream.Portal.Core.AppConstants.PAGEID)))
        Versions.Sort("Name DESC")
        If Versions.Count > 0 Then
            Dim ds As System.Data.DataSet = Versions.ToDataSet(True)
            Me.PagesGrid.DataSource = ds.Tables(0)
            Me.PagesGrid.DataBind()
        End If
    End Sub


    Protected Sub PagesGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles PagesGrid.PageIndexChanging
        PagesGrid.PageIndex = e.NewPageIndex
        PagesGrid.EditIndex = -1
        BindGrid()
    End Sub


    Public Function CanEdit(ByVal IsPublishedVersion As Boolean) As String
        If IsPublishedVersion Then
            Return "Edit"
        End If
        Return "Edit"
    End Function


    Public Function GetSkinName(ByVal SkinID As Guid) As String
        Try
            Return Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(SkinID).Name
        Catch ex As Exception

        End Try
        Return ""
    End Function


    Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
        If TrueOrFalse Then
            Return "yes"
        End If
        Return "no"
    End Function


    Protected Sub PagesGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles PagesGrid.RowDeleting
    End Sub


    Protected Sub PagesGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles PagesGrid.RowCommand
        Dim rowIndex As Integer = CInt(e.CommandArgument)
        Dim VersionId As String = DirectCast(PagesGrid.Rows(rowIndex).FindControl("lblID"), Label).Text

        Select Case e.CommandName
            Case "Delete"
                Try
                    Dim Page As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(New Guid(VersionId))
                    If Page.IsPublishedVersion Then
                        Me.lblStatus.Text = "The published version of a page cannot be deleted."
                        Exit Sub
                    End If
                    Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(Page)
                    manager.Delete()
                    Me.PagesGrid.EditIndex = -1
                    BindGrid()
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
            Case "Edit"
                Try
                    Response.Redirect("../Edit.aspx?VersionID=" & VersionId)
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
        End Select
    End Sub


    Protected Sub PagesGrid_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles PagesGrid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim VersionId As String = DataBinder.Eval(e.Row.DataItem, "Id").ToString

            If DataBinder.Eval(e.Row.DataItem, "IsPublishedVersion") = "True" Or _
            Versions.Count = 1 Or Me.Page.Header.Attributes.Item("Id") = VersionId Then
                Dim DeleteButton As ImageButton = e.Row.Cells(3).Controls(0)
                DeleteButton.Visible = False
            End If

            If DataBinder.Eval(e.Row.DataItem, "IsPublishedVersion") = "True" Then
                Dim EditButton As HyperLink = e.Row.Cells(1).Controls(0)
                EditButton.Visible = False
            End If
        End If
    End Sub


    Public Function CanDelete(ByVal IsPublished As Boolean) As Boolean
        If IsPublished Then
            Return False
        End If
        Return True
    End Function
End Class
