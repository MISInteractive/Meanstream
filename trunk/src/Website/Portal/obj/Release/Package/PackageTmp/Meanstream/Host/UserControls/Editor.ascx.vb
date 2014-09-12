
Partial Class Meanstream_Host_UserControls_Editor
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.LoadEditorSettings()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Private Sub LoadEditorSettings()
        Me.txtCssStylesheetPath.Text = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_STYLESHEET_PATH).Value
        Me.txtDocumentsPath.Text = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_DOCUMENTS_PATH).Value
        Me.txtFlashGalleryPath.Text = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_FLASH_GALLERY_PATH).Value
        Me.txtImageGalleryPath.Text = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_IMAGE_GALLERY_PATH).Value
        Me.txtTemplateGalleryPath.Text = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_TEMPLATE_GALLERY_PATH).Value
        Me.txtVideoGalleryPath.Text = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_VIDEO_GALLERY_PATH).Value
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim Setting As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_STYLESHEET_PATH)
            Setting.Value = Me.txtCssStylesheetPath.Text
            Setting.Save()

            Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_DOCUMENTS_PATH)
            Setting.Value = Me.txtDocumentsPath.Text
            Setting.Save()

            Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_FLASH_GALLERY_PATH)
            Setting.Value = Me.txtFlashGalleryPath.Text
            Setting.Save()

            Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_IMAGE_GALLERY_PATH)
            Setting.Value = Me.txtImageGalleryPath.Text
            Setting.Save()

            Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_TEMPLATE_GALLERY_PATH)
            Setting.Value = Me.txtTemplateGalleryPath.Text
            Setting.Save()

            Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.EDITOR_VIDEO_GALLERY_PATH)
            Setting.Value = Me.txtVideoGalleryPath.Text
            Setting.Save()

            Me.btnSave.SuccessMessage = "Save succesful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
