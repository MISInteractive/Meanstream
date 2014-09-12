
Partial Class Meanstream_Administration_UserControls_Password
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim Username As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("uid")))
            Me.LoadUserByID(Username)
            Me.btnChangePassword.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-change-password.png"
            Me.btnResetPassword.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-reset-password.png"
        End If
    End Sub

    Protected Sub LoadUserByID(ByVal ID As String)
        Dim MembershipUser As MembershipUser = System.Web.Security.Membership.GetUser(ID)
        Me.lblUsername.Text = ID
        Me.LastPasswordChangedDate.Text = MembershipUser.LastPasswordChangedDate
    End Sub

    Protected Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangePassword.Click
        Dim result As String = ""

        If Me.txtPassword.Text <> Me.txtConfirmPassword.Text Then
            Me.btnChangePassword.ThrowFailure = True
            Me.btnChangePassword.FailMessage = "Passwords do not match"
            Return
        End If
        Try
            Dim MembershipUser As MembershipUser = System.Web.Security.Membership.GetUser(Me.lblUsername.Text)
            Dim ResetPassword As String = MembershipUser.ResetPassword()
            Dim Password As String = Me.txtPassword.Text

            If MembershipUser.ChangePassword(ResetPassword, Password) = False Then
                Me.btnChangePassword.ThrowFailure = True
                Me.btnChangePassword.FailMessage = "Reset password failed"
                Return
            End If

            Me.btnChangePassword.FailMessage = "Password has been changed and sent to the user's inbox"
            Me.LoadUserByID(Me.lblUsername.Text)
        Catch ex As Exception
            Me.btnChangePassword.ThrowFailure = True
            Me.btnChangePassword.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub btnResetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetPassword.Click
        Dim result As String = ""

        Try
            Dim MembershipUser As MembershipUser = System.Web.Security.Membership.GetUser(Me.lblUsername.Text)
            Dim ResetPassword As String = MembershipUser.ResetPassword()
            Dim Password As String = Membership.GeneratePassword(5, 0)

            If MembershipUser.ChangePassword(ResetPassword, Password) = False Then
                Me.btnResetPassword.ThrowFailure = True
                Me.btnResetPassword.FailMessage = "Reset password failed"
                Return
            End If

            'Send Mail
            Dim Response As Integer = Meanstream.Portal.Core.Messaging.MessagingService.Current.SendResetPasswordEmail(MembershipUser.UserName, MembershipUser.Email, Password)

            If Response = 2 Or Response = 3 Then
                Me.btnResetPassword.FailMessage = "There was an error sending the email. Please check the smtp settings"
            Else
                Me.btnResetPassword.FailMessage = "Password has been reset and sent to the user's email"
            End If

            Me.LoadUserByID(Me.lblUsername.Text)
        Catch ex As Exception
            Me.btnResetPassword.ThrowFailure = True
            Me.btnResetPassword.FailMessage = ex.Message
        End Try
    End Sub
End Class
