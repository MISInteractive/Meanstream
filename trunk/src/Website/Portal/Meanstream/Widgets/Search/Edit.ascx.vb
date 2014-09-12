Imports Meanstream.Portal.Core.Instrumentation

Partial Class Meanstream_Widgets_Search_Edit
    Inherits Meanstream.Portal.Core.Widgets.Search.WidgetEditBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PortalTrace.WriteLine("Meanstream_Widgets_Search_Edit:Page_Load()")

        'Add our content
        Me.Content = Me.LoadControl("~/Controls/Core/Widgets/Search/Widget.ascx")
        'Allow widget removal
        Me.AllowDelete = True
        'Allow widget settings updates
        Me.ShowSettings = True

    End Sub

End Class
