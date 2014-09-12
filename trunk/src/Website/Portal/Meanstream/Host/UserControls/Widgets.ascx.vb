Imports Meanstream.Portal.Core.Instrumentation

Partial Class Meanstream_Host_UserControls_Widgets
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
            Me.btnCreateWidget.TargetControlId = Me.CreateWidgetTarget.ClientID
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
        End If
    End Sub

    Private Sub BindGrid()
        Me.WidgetsGrid.DataSource = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetModuleDefinitions.ToDataSet(True)
        Me.WidgetsGrid.DataBind()
    End Sub

    Protected Sub WidgetsGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles WidgetsGrid.PageIndexChanging
        Me.WidgetsGrid.PageIndex = e.NewPageIndex
        Me.WidgetsGrid.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            For Each GridViewRow As Web.UI.WebControls.GridViewRow In WidgetsGrid.Rows
                Dim ModuleDefId As String = CType(GridViewRow.FindControl("lblID"), Label).Text

                Dim Enabled As Boolean = True
                If CType(GridViewRow.FindControl("Enabled"), CheckBox).Checked Then
                    Enabled = True
                Else
                    Enabled = False
                End If

                Dim ModuleDef As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetModuleDefinitions.Find("Id", New Guid(ModuleDefId))
                ModuleDef.Enabled = Enabled
                Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.UpdateWidgetDefinition(ModuleDef)
            Next

            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
