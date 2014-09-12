
Partial Class Meanstream_Host_UserControls_Smtp
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.LoadHostSettings()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Private Sub LoadHostSettings()
        Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim SmtpSection As System.Net.Configuration.SmtpSection = DirectCast(Configuration.GetSection("system.net/mailSettings/smtp"), System.Net.Configuration.SmtpSection)
        Me.txtFrom.Text = SmtpSection.From
        Me.txtHost.Text = SmtpSection.Network.Host
        Me.txtUsername.Text = SmtpSection.Network.UserName
        Me.txtPassword.Text = SmtpSection.Network.Password
        Me.txtPort.Text = SmtpSection.Network.Port
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtFrom.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Default From Address required"
            Return
        End If

        If Me.txtHost.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Host required"
            Return
        End If

        If Me.txtUsername.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Username required"
            Return
        End If

        If Me.txtPassword.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Password required"
            Return
        End If

        If Me.txtPort.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Port required"
            Return
        End If

        Try
            Integer.Parse(Me.txtPort.Text.Trim)
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Port must be a valid number"
            Return
        End Try

        Try
            Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            Dim SmtpSection As System.Net.Configuration.SmtpSection = DirectCast(Configuration.GetSection("system.net/mailSettings/smtp"), System.Net.Configuration.SmtpSection)
            SmtpSection.From = Me.txtFrom.Text
            SmtpSection.Network.Host = Me.txtHost.Text
            SmtpSection.Network.UserName = Me.txtUsername.Text
            SmtpSection.Network.Password = Me.txtPassword.Text
            SmtpSection.Network.Port = Me.txtPort.Text
            Configuration.Save()

            Me.btnSave.FailMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
