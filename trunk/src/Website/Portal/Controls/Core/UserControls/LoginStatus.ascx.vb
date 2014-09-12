
Partial Class Controls_Core_UserControls_LoginStatus
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Request.IsAuthenticated Then
            Me.btnStatus.Text = "sign out"
        End If
    End Sub

    Protected Sub btnStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatus.Click
        If Request.IsAuthenticated Then
            FormsAuthentication.SignOut()
            Response.Redirect(FormsAuthentication.DefaultUrl)
        Else
            FormsAuthentication.RedirectToLoginPage()
        End If
    End Sub
End Class
