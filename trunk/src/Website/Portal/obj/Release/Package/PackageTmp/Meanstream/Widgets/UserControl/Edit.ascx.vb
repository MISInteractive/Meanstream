Imports Meanstream.Portal.Core.Instrumentation

Partial Class Meanstream_Widgets_UserControl_Edit
    Inherits Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PortalTrace.WriteLine("Meanstream_Widgets_UserControl_Edit:Page_Load()")

        Try
            Dim Custom As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Me.GetIModuleDTO()
            'Add our content
            If Custom.VirtualPath = "" Then
                Me.Content = New LiteralControl("No User Control Exits<br><br><br><br>")
            Else
                Me.Content = LoadControl(Custom.VirtualPath)
            End If
        Catch ex As Exception
            PortalTrace.WriteLine("Meanstream_Widgets_UserControl_Edit:Page_Load() " & ex.Message)
            Me.Content = New LiteralControl(ex.Message)
        End Try

        'Add our custom edit menu option
        Dim EditMenuItem As Meanstream.Portal.Web.UI.WidgetEditMenuItem = New Meanstream.Portal.Web.UI.WidgetEditMenuItem
        EditMenuItem.Text = "Edit"
        EditMenuItem.NavigateUrl = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "Meanstream/Widgets/UserControl/Admin.aspx?WidgetId=" & Me.WidgetId.ToString
        EditMenuItem.Title = "Custom UserControl"
        EditMenuItem.Width = "550"
        EditMenuItem.Height = "225"
        EditMenuItem.ShowUrl = True
        EditMenuItem.ShowLoader = True
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
        'Response.Redirect(Request.RawUrl)
    End Sub
End Class
