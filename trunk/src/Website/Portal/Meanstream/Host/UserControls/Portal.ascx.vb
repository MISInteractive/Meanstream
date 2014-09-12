
Partial Class Meanstream_Host_UserControls_Portal
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not IsPostBack Then
            Me.LoadHostSettings()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Private Sub LoadHostSettings()
        Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim SmtpSection As System.Net.Configuration.SmtpSection = DirectCast(Configuration.GetSection("system.net/mailSettings/smtp"), System.Net.Configuration.SmtpSection)
        Me.txtCache.Text = Configuration.AppSettings.Settings.Item("Meanstream.PageCacheExpiration").Value
        Me.txtExportsRoot.Text = Configuration.AppSettings.Settings.Item("Meanstream.ExportPath").Value
        Me.txtSupportEmail.Text = Configuration.AppSettings.Settings.Item("Meanstream.Support").Value
        Me.txtVersion.Text = Configuration.AppSettings.Settings.Item("Meanstream.Version").Value
        'Me.cbWorkflow.Checked = Configuration.AppSettings.Settings.Item("Meanstream.EnableWorkflow").Value
        Me.cbCaching.Checked = Configuration.AppSettings.Settings.Item("Meanstream.EnableCaching").Value
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtCache.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Cache required"
            Return
        End If

        Try
            Integer.Parse(Me.txtCache.Text.Trim)
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Cache must be a valid number"
            Return
        End Try

        If Me.txtExportsRoot.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Exports Root required"
            Return
        End If

        If Me.txtSupportEmail.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Support Email required"
            Return
        End If

        Try
            Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            Configuration.AppSettings.Settings.Item("Meanstream.PageCacheExpiration").Value = Me.txtCache.Text
            Configuration.AppSettings.Settings.Item("Meanstream.ExportPath").Value = Me.txtExportsRoot.Text
            Configuration.AppSettings.Settings.Item("Meanstream.Support").Value = Me.txtSupportEmail.Text
            Configuration.AppSettings.Settings.Item("Meanstream.Version").Value = Me.txtVersion.Text
            Configuration.AppSettings.Settings.Item("Meanstream.EnableWorkflow").Value = Me.cbWorkflow.Checked
            Configuration.AppSettings.Settings.Item("Meanstream.EnableCaching").Value = Me.cbCaching.Checked
            Configuration.Save()

            'Dim GlobalWorkflowSettings As Meanstream.Portal.Core.WorkflowSettings = SessionManager.GetWorkflowManager.GetSettings
            'If GlobalWorkflowSettings IsNot Nothing Then
            '    GlobalWorkflowSettings.Enabled = Me.cbWorkflow.Checked
            '    GlobalWorkflowSettings.Save(False, False, False)
            'End If

            Me.btnSave.SuccessMessage = "Save succesful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub btnRecycleApplicationPool_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecycleApplicationPool.Click
        Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Configuration.Save()
        Response.Redirect(Request.RawUrl)
    End Sub

    Protected Sub btnDeleteCache_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteCache.Click
        Meanstream.Portal.Core.Utilities.CacheUtility.ClearApplicationCache()
        Response.Redirect(Request.RawUrl)
    End Sub
End Class
