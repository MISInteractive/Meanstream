Imports System.Diagnostics

Partial Class Meanstream_Administration_UserControls_Tracing
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            Dim cs As ConfigurationSection = Configuration.GetSection("system.diagnostics")
            Dim switches As System.Collections.IEnumerator = DirectCast(cs.ElementInformation.Properties("switches").Value, System.Configuration.ConfigurationElementCollection).GetEnumerator()
            While switches.MoveNext()
                Dim element As ConfigurationElement = switches.Current
                If "PortalSwitch" = element.ElementInformation.Properties.Item("name").Value Then
                    Me.cbEnabled.Checked = element.ElementInformation.Properties.Item("value").Value
                End If
                If "PortalTraceLevel" = element.ElementInformation.Properties.Item("name").Value Then
                    Me.ddlLevel.SelectedValue = element.ElementInformation.Properties.Item("value").Value
                    Me.ddlLevel.SelectedText = Me.ddlLevel.SelectedValue
                End If
            End While

            Me.ddlRequests.SelectedValue = ConfigurationManager.AppSettings("Meanstream.Tracing.MaximumRequests")
            Me.ddlRequests.SelectedText = Me.ddlRequests.SelectedValue

            'Me.BindGrid()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub
    Private Shared Function GetSomePropertyFromReflection(ByVal o As Object, ByVal [property] As String) As String
        Return DirectCast(o.[GetType]().InvokeMember([property], System.Reflection.BindingFlags.GetProperty, Nothing, o, Nothing), String)
    End Function

    'Private Sub BindGrid()
    '    Dim ds As System.Data.DataSet = Meanstream.Portal.Core.Instrumentation.PortalTrace.GetTracing
    '    If ds IsNot Nothing Then
    '        Me.Grid.DataSource = ds
    '        Me.Grid.DataBind()
    '    End If
    'End Sub

    'Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grid.RowDeleting
    'End Sub

    'Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
    'End Sub

    'Protected Sub btnClearTrace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearTrace.Click
    '    Meanstream.Portal.Core.Instrumentation.PortalTrace.CleanTracing()
    '    Me.BindGrid()
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim cs As ConfigurationSection = Configuration.GetSection("system.diagnostics")
        Dim switches As System.Collections.IEnumerator = DirectCast(cs.ElementInformation.Properties("switches").Value, System.Configuration.ConfigurationElementCollection).GetEnumerator()
        While switches.MoveNext()
            Dim element As ConfigurationElement = switches.Current
            If "PortalSwitch" = element.ElementInformation.Properties.Item("name").Value Then
                element.ElementInformation.Properties.Item("value").Value = Me.cbEnabled.Checked
            End If
            If "PortalTraceLevel" = element.ElementInformation.Properties.Item("name").Value Then
                element.ElementInformation.Properties.Item("value").Value = Me.ddlLevel.SelectedValue
            End If
        End While
        Try
            ConfigurationManager.AppSettings("Meanstream.Tracing.MaximumRequests") = Me.ddlRequests.SelectedValue
            Configuration.Save()
            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
