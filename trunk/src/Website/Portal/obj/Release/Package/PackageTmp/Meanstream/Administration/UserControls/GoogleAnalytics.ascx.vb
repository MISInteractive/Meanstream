
Partial Class Meanstream_Administration_UserControls_GoogleAnalytics
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.LoadSettings()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Private Sub LoadSettings()
        Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim script As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetGoogleAnalyticsScript(portalId)
        Me.txtScript.Text = script.Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        If Me.txtScript.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "URL Parameter required"
            Exit Sub
        End If

        Try
            Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId

            Dim script As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetGoogleAnalyticsScript(portalId)
            script.Value = Me.txtScript.Text
            script.Save()

            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
