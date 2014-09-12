
Partial Class Meanstream_Administration_UserControls_CreateRole
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtName.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Name required"
            Exit Sub
        End If

        Try
            Meanstream.Portal.Core.Membership.MembershipService.Current.CreateRole(Me.txtName.Text, Me.txtDescription.Text, Me.chkAddPublic.Checked, Me.chkAddAuto.Checked)
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
            Exit Sub
        End Try
        Me.litRoleName.Value = Me.txtName.Text

        Me.btnSave.SuccessMessage = "Role created successfully"
    End Sub
End Class
