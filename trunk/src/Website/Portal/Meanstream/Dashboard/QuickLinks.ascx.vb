
Partial Class Meanstream_Dashboard_QuickLinks
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.btnCreatePage.TargetControlId = Me.CreatePageTarget.ClientID
            Me.btnCreateUser.TargetControlId = Me.CreateUserTarget.ClientID
        End If
    End Sub
End Class
