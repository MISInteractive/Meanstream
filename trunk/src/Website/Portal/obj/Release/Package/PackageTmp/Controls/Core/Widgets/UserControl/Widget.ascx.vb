Imports Meanstream.Portal.Core.Instrumentation

Partial Class Controls_Core_Widgets_UserControl_Widget
    Inherits Meanstream.Portal.Core.Widgets.UserControl.WidgetBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PortalTrace.WriteLine("Controls_Core_Widgets_UserControl_Widget:Page_Load()")
        Try
            Dim Content As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Me.GetIModuleDTO()
            Me.Controls.Add(LoadControl(Content.VirtualPath))
        Catch ex As Exception
            Me.Controls.Add(New LiteralControl(ex.Message))
        End Try
    End Sub
End Class
