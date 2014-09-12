
Partial Class Meanstream_Administration_UserControls_AddUserToRoleMessage
    Inherits System.Web.UI.UserControl

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim BodySettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_BODY_ADD_USER_TO_ROLE)
            Me.txtBody.Text = BodySettings.Value
            Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_ADD_USER_TO_ROLE)
            Me.txtSubject.Text = SubjectSettings.Value
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Public Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim BodySettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_BODY_ADD_USER_TO_ROLE)
        BodySettings.Value = Me.txtBody.Text
        BodySettings.Save()

        Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_ADD_USER_TO_ROLE)
        SubjectSettings.Value = Me.txtSubject.Text()
        SubjectSettings.Save()

        Me.btnSave.SuccessMessage = "Save successful"
    End Sub
End Class
