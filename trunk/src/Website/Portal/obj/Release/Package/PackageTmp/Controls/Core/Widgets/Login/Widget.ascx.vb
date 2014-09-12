Imports Meanstream.Portal.Core.Instrumentation

Partial Class Controls_Core_Widgets_Login_Widget
    Inherits Meanstream.Portal.Core.Widgets.Login.WidgetBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Form.DefaultButton = Me.btnLogin.UniqueID
        
        If Not IsPostBack Then
            Me.PasswordRecovery1.Visible = False
            Me.txtUsername.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Click"/> event. Validates user credentials and authenticates.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Membership.ValidateUser(txtUsername.Text, txtPassword.Text) Then
            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, False)
        Else
            lblInvalidLogin.Text = "Invalid Credentials..."
            lblInvalidLogin.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Click"/> event. Retrieves user password and sends to their email.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub btnForgotPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnForgotPassword.Click
        Me.tblLogin.Visible = False
        Me.PasswordRecovery1.Visible = True
        Me.PasswordRecovery1.MailDefinition.Priority = Net.Mail.MailPriority.High

        Dim SubjectSettings As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.MESSAGE_SUBJECT_FORGOT_PASSWORD)
        Me.PasswordRecovery1.MailDefinition.Subject = SubjectSettings.Value

        Dim From As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SMTP_FROM)
        Me.PasswordRecovery1.MailDefinition.From = From.Value
        Me.PasswordRecovery1.DataBind()
    End Sub
End Class
