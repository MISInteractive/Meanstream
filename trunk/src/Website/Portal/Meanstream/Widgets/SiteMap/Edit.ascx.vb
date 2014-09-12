
Partial Class Meanstream_Widgets_SiteMap_Edit
    Inherits Meanstream.Portal.Core.Widgets.SiteMap.WidgetEditBase

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Add our content
        Me.Content = Me.LoadControl("~/Controls/Core/Widgets/SiteMap/Widget.ascx")
        'Allow widget removal
        Me.AllowDelete = True
        'Allow widget settings updates
        Me.ShowSettings = True
    End Sub
End Class
