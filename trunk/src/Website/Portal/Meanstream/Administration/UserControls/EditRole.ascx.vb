
Partial Class Meanstream_Administration_UserControls_EditRole
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim RoleID As String = Request.Params("RoleID")
            If RoleID <> Nothing And RoleID <> "" Then
                Me.LoadRoleForEdit(RoleID)
                Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            End If
        End If
    End Sub

    Public Sub LoadRoleForEdit(ByVal RoleID As String)
        Dim ID As Guid = New Guid(RoleID)
        Dim Role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(ID)
        Me.txtEditName.Text = Role.Name
        Me.txtEditDescription.Text = Role.Description
        Me.chkEditAuto.Checked = Role.AutoAssignment
        Me.chkEditPublic.Checked = Role.IsPublic

        If Role.Name = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR _
        Or Role.Name = Meanstream.Portal.Core.AppConstants.ALLUSERS _
        Or Role.Name = Meanstream.Portal.Core.AppConstants.REGISTERED_USERS _
        Or Role.Name = Meanstream.Portal.Core.AppConstants.CONTENT_ADMINISTRATOR _
        Or Role.Name = Meanstream.Portal.Core.AppConstants.SECURITY_ADMINISTRATOR _
        Or Role.Name = Meanstream.Portal.Core.AppConstants.ECOMMERCE_ADMINISTRATOR _
        Or Role.Name = Meanstream.Portal.Core.AppConstants.HOST Then
            Me.txtEditName.Enabled = False
            Me.chkEditAuto.Enabled = False
            Me.chkEditPublic.Enabled = False
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim result As String = ""
        Dim Role As Meanstream.Portal.Core.Membership.Role = New Meanstream.Portal.Core.Membership.Role(New Guid(Request.Params("RoleID")))
        Dim manager As New Meanstream.Portal.Core.Membership.RoleManager(Role)
        manager.LoadFromDatasource()
        Role.Name = Me.txtEditName.Text
        Role.Description = Me.txtEditDescription.Text
        Role.AutoAssignment = Me.chkEditAuto.Checked
        Role.IsPublic = Me.chkEditPublic.Checked
        Try
            manager.Save()
            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
