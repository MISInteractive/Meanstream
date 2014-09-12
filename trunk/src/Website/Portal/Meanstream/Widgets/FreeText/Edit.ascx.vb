Imports Meanstream.Portal.Core.Instrumentation

Partial Class Meanstream_Widgets_FreeText_Edit
    Inherits Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PortalTrace.WriteLine("Meanstream_Widgets_FreeText_Edit:Page_Load()")

        Try
            Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Me.GetIModuleDTO()
            If FreeText.Content.Text.Trim = "" Then
                FreeText.Content.Text = "<br><br><br>"
            End If
            Me.Content = New LiteralControl(FreeText.Content.Text)
        Catch ex As Exception
            Me.Controls.Add(New LiteralControl(ex.Message))
        End Try

        'Add our custom edit menu option
        Dim EditMenuItem As Meanstream.Portal.Web.UI.WidgetEditMenuItem = New Meanstream.Portal.Web.UI.WidgetEditMenuItem
        EditMenuItem.Text = "Edit"
        EditMenuItem.NavigateUrl = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "Meanstream/Widgets/FreeText/Admin.aspx?WidgetId=" & Me.WidgetId.ToString
        EditMenuItem.Title = "Free Text"
        EditMenuItem.Width = "1000"
        EditMenuItem.Height = "605"
        EditMenuItem.ShowUrl = True
        EditMenuItem.ShowLoader = True
        'EditMenuItem.SkinID = "Window"
        'Call OnServerClose when Window Closes
        AddHandler EditMenuItem.ServerClose, AddressOf EditMenuItem_ServerClose
        Me.EditMenuItems.Add(EditMenuItem)
        'Allow widget removal
        Me.AllowDelete = True
        'Allow widget settings updates
        Me.ShowSettings = True
        'End If
    End Sub

    ''' <summary>
    ''' Raises the <see cref="Meanstream.Portal.Web.UI.WidgetEditMenuItem.ServerClose"/> event. Refreshes page after postback on edit.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Public Sub EditMenuItem_ServerClose(ByVal sender As Object, ByVal e As EventArgs)
        PortalTrace.WriteLine("Meanstream_Widgets_FreeText_Edit:EditMenuItem_ServerClose()")
    End Sub
End Class
