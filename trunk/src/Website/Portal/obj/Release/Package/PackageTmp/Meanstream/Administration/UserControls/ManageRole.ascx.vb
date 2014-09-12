
Partial Class Meanstream_Administration_UserControls_ManageRole
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim RoleID As String = Request.Params("RoleID")
            If RoleID <> Nothing And RoleID <> "" Then
                Dim ID As Guid = New Guid(RoleID)
                Dim Role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(ID)
                Me.litRole.Text = Role.Name

                If Request.Params("Action") <> Nothing Then
                    If Request.Params("Action") = "ManageUsers" Then
                        Me.TabContainer1.ActiveTab = Me.ManageUsersTab
                    End If
                End If
            End If
        End If
    End Sub
End Class
