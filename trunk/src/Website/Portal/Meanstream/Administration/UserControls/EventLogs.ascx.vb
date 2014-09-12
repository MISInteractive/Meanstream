
Partial Class Meanstream_Administration_UserControls_EventLogs
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Me.BindGrid()
        End If
    End Sub

    'Private Sub BindGrid()
    '    Me.Grid.DataSource = Meanstream.Portal.Core.Messaging.ApplicationMessagingManager.Current.GetApplicationMessages()
    '    Me.Grid.DataBind()
    '    If Me.Grid.Rows.Count = 0 Then
    '        Me.Container.Visible = False
    '    End If
    'End Sub

    'Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grid.RowDeleting
    'End Sub

    'Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
    'End Sub
End Class
