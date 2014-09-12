
Partial Class Meanstream_Pages_UserControls_SkinPageList
    Inherits System.Web.UI.UserControl

    Public Sub Process()
        Me.rPageList.DataSource = Meanstream.Portal.Core.Content.ContentService.Current.GetPagesBySkinId(New Guid(Me.ID))
        Me.rPageList.DataBind()
    End Sub
End Class
