
Partial Class Meanstream_Administration_UserControls_ManageUser
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim Username As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("uid")))
            If Username <> Nothing And Username <> "" Then
                Me.litUser.Text = Username
            End If
            If Request.Params("Action") <> Nothing Then
                If Request.Params("Action") = "Remove" Then
                    Me.TabContainer1.ActiveTab = Me.CredentialsTab
                End If
            End If
        End If
    End Sub
End Class
