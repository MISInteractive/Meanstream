
Partial Class Meanstream_Administration_UserControls_Smtp
    Inherits System.Web.UI.UserControl

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SMTP_FROM)
            Me.txtFrom.Text = SubjectSettings.Value
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Public Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SMTP_FROM)
        SubjectSettings.Value = Me.txtFrom.Text()
        SubjectSettings.Save()

        Me.btnSave.SuccessMessage = "Save successful"
    End Sub
End Class
