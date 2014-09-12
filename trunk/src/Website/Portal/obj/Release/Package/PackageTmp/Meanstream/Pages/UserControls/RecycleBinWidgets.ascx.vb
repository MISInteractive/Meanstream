
Partial Class Meanstream_Pages_UserControls_RecycleBinWidgets
    Inherits System.Web.UI.UserControl


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub


    Private Sub BindGrid()
        Dim widgets As List(Of Meanstream.Portal.Core.WidgetFramework.WidgetVersion) = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetRecycleBin
        If widgets.Count > 0 Then
            Me.WidgetsGrid.DataSource = widgets
            Me.WidgetsGrid.DataBind()
            btnDeleteAll.Visible = True
        Else
            Me.Container.Visible = False
            Me.lblMessage.Text = "No Records Found"
            Me.btnDeleteAll.Visible = False
        End If
    End Sub


    Protected Sub WidgetsGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles WidgetsGrid.PageIndexChanging
        WidgetsGrid.PageIndex = e.NewPageIndex
        WidgetsGrid.EditIndex = -1
        BindGrid()
    End Sub


    Public Function IsGlobal(ByVal Id As Integer) As String
        If Id = "1" Then
            Return "yes"
        End If
        Return "no"
    End Function


    Public Function GetModuleType(ByVal ModuleDefId As Guid) As String
        Return Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetType(ModuleDefId)
    End Function


    Protected Sub WidgetsGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles WidgetsGrid.RowDeleting
    End Sub


    Protected Sub WidgetsGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles WidgetsGrid.RowCommand
        Dim rowIndex As Integer = CInt(e.CommandArgument)
        Dim ModuleId As String = DirectCast(WidgetsGrid.Rows(rowIndex).FindControl("lblID"), Label).Text

        Select Case e.CommandName
            Case "Delete"
                Dim widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetVersionById(New Guid(ModuleId))
                Dim manager As New Meanstream.Portal.Core.WidgetFramework.WidgetVersionManager(widget)
                manager.Delete()
                Me.WidgetsGrid.EditIndex = -1
                BindGrid()
                Me.lblStatus.Text = "Widget has been successfully deleted."
            Case "Restore"
                Dim widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetVersionById(New Guid(ModuleId))
                Dim manager As New Meanstream.Portal.Core.WidgetFramework.WidgetVersionManager(widget)
                manager.Restore()
                Me.WidgetsGrid.EditIndex = -1
                BindGrid()
        End Select
    End Sub


    Protected Sub DeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Try
            Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.DeleteAllFromRecycleBin()
            Me.BindGrid()
            Me.lblStatus.Text = "Widgets have been successfully deleted."
        Catch ex As Exception
            Me.lblStatus.Text = ex.Message
        End Try
    End Sub
End Class
