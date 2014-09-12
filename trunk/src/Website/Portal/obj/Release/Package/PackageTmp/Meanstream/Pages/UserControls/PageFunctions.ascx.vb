
Partial Class Meanstream_Pages_UserControls_PageFunctions
    Inherits System.Web.UI.UserControl

    Dim PageContentMain As Meanstream.Portal.Core.Content.Page = Nothing

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Request.Params("PageID") = Nothing Then
            Exit Sub
        End If

        'If Not IsPostBack Then
        If Request.Params("PageID") <> Nothing Then
            Dim Page As Meanstream.Portal.Core.Content.Page = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(Request.Params("PageID").ToString))
            Me.btnEdit.NavigateUrl = "../Edit.aspx?VersionID=" & Page.VersionId.ToString

            Dim Url As String = ""

            If Page.IsHomePage Then
                Me.pIsHomePage.Visible = False
            End If

            Select Case Page.Type
                Case "1"
                    Url = "src='../../../../" & Page.Url & "?meanstream_disable_link_action=True'"
                Case "2"
                    Url = "src='../../../../" & Page.Url & "?meanstream_disable_link_action=True'"
                Case "3"
                    Url = "src='" & Page.Url & "'"
            End Select
            Me.litFrame.Text = "<iframe name='pageFrame' style='background: transparent;' allowTransparency='true' height='769' width='100%' frameborder='0' " & Url & "></iframe>"
        End If

        PageContentMain = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(Request.Params("PageID").ToString))
        Dim VersionID As String = PageContentMain.VersionId.ToString
        Dim PageID As String = PageContentMain.Id.ToString

        'Store VersionId in the header for history
        Me.Page.Header.Attributes.Add("VersionId", VersionID)
        Me.btnCopyCreatePage.NavigateUrl = "Module.aspx?ctl=PageSettings&Action=CopyAdd&VersionID=" & VersionID
        Me.btnCopyCreatePage.TargetControlId = Me.CopyCreatePageTarget.ClientID
        Me.btnCreatePage.TargetControlId = Me.CreatePageTarget.ClientID

        If Request.Params(Meanstream.Portal.Core.AppConstants.ACTION) IsNot Nothing And Request.Params(Meanstream.Portal.Core.AppConstants.MODULEID) Is Nothing Then
            If Request.Params(Meanstream.Portal.Core.AppConstants.ACTION).ToString = "Delete" Then
                'Send to Recycle Bin
                Dim PageBase As Meanstream.Portal.Core.Content.Page = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(PageID))
                Dim manager As New Meanstream.Portal.Core.Content.PageManager(PageBase)
                manager.SendToRecycleBin()
                Response.Redirect("Default.aspx?ctl=Pages")
            End If
        End If
        'End If
    End Sub

    Protected Sub btnSetHomePage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetHomePage.Click
        PageContentMain = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(Request.Params("PageID").ToString))
        Dim manager As New Meanstream.Portal.Core.Content.PageManager(PageContentMain)
        manager.SetAsHomePage()
        Response.Redirect("Default.aspx?ctl=ViewPage&PageID=" & Request.Params("PageID").ToString)
    End Sub
End Class
