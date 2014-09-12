
Partial Class Meanstream_Profile_UserControls_ChangePassword
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-change-password.png"
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtPassword.Text.Trim = "" Or Me.txtConfirmPassword.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Password required"
            Return
        End If

        If Me.txtPassword.Text <> Me.txtConfirmPassword.Text Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Passwords do not match"
            Return
        End If

        Dim MembershipUser As MembershipUser = System.Web.Security.Membership.GetUser(Profile.UserName)
        Dim ResetPassword As String = MembershipUser.ResetPassword()

        If MembershipUser.ChangePassword(ResetPassword, Me.txtPassword.Text) = False Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Reset Password failed"
            Return
        End If

        Me.btnSave.SuccessMessage = "Password changed succesfully"
    End Sub
End Class
