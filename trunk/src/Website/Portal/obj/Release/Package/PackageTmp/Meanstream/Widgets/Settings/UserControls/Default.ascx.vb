
Partial Class Meanstream_Widgets_Settings_UserControls_Default
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.Page.Title = "Edit Module Settings"

        If Not Page.IsPostBack Then
            If Request.Params("ModuleId") <> Nothing Then
                Me.LoadModuleSettings(New Guid(Request.Params("ModuleId")))
                Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            End If
        End If
    End Sub

    Protected Sub LoadModuleSettings(ByVal VersionModuleID As Guid)
        Me.btnSave.Visible = True
        Me.SecurityGrid.Visible = True

        'get loadcontrol and set Id
        Dim Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetVersionById(VersionModuleID)

        If Widget Is Nothing Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Widget does not exist"
            Exit Sub
        End If

        Dim ModuleId As Guid = Widget.Id

        If Widget.StartDate <> Nothing Then
            Me.txtStartDate.Text = Widget.StartDate
        End If

        If Widget.EndDate <> Nothing Then
            Me.txtEndDate.Text = Widget.EndDate
        End If

        Me.txtName.Text = Widget.Title
        Me.SecurityGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllRoles()
        Me.SecurityGrid.DataBind()

        For Each GridRow As GridViewRow In SecurityGrid.Rows()
            Dim lblRoleID As Literal = CType(GridRow.FindControl("RoleName"), Literal)
            If lblRoleID.Text = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Or _
            lblRoleID.Text = Meanstream.Portal.Core.AppConstants.HOST Or _
            lblRoleID.Text = Meanstream.Portal.Core.AppConstants.CONTENT_ADMINISTRATOR Then
                'Default Administrator accounts
                Dim cbView As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxView"), CheckBox)
                Dim cbEdit As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxEdit"), CheckBox)
                cbView.Checked = True
                cbView.Enabled = False
                cbEdit.Checked = True
                cbEdit.Enabled = False
            End If

            If lblRoleID.Text = Meanstream.Portal.Core.AppConstants.ALLUSERS Then
                'All users
                Dim cbEdit As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxEdit"), CheckBox)
                cbEdit.Enabled = False
            End If
        Next

        'Enumerate the GridViewRows
        For index As Integer = 0 To SecurityGrid.Rows.Count - 1
            'Programmatically access the CheckBox from the TemplateField
            Dim cbView As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxView"), CheckBox)
            Dim cbEdit As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxEdit"), CheckBox)
            Dim lblRoleID As Literal = CType(SecurityGrid.Rows(index).FindControl("RoleID"), Literal)

            For Each WidgetPermission As Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission In Widget.Permissions
                If WidgetPermission.Role.Id = New Guid(lblRoleID.Text) Then
                    If WidgetPermission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id Then
                        cbView.Checked = True
                    End If

                    If WidgetPermission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id Then
                        cbEdit.Checked = True
                    End If
                End If
            Next
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetVersionById(New Guid(Request.Params("ModuleId").ToString))

        'make a copy of permissions for edit
        Dim listArray(widget.Permissions.Count) As Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission
        widget.Permissions.CopyTo(listArray)
        Dim list As List(Of Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission) = listArray.ToList()
        'bug fix - toCopy adds an extra item
        list.RemoveAt(list.Count - 1)

        'Enumerate the GridViewRows
        For index As Integer = 0 To SecurityGrid.Rows.Count - 1
            'Programmatically access the CheckBox from the"TemplateField
            Dim cbView As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxView"), CheckBox)
            Dim cbEdit As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxEdit"), CheckBox)
            Dim lblRoleID As Literal = CType(SecurityGrid.Rows(index).FindControl("RoleID"), Literal)
            Dim RoleID As Guid = New Guid(lblRoleID.Text)
            Dim role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(RoleID)

            'If no permission exists add one. If not try to remove.                        
            If cbView.Checked Then
                Dim exists As Boolean = False
                For Each permission As Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission In Widget.Permissions
                    If permission.Role.Id = RoleID Then
                        If permission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id Then
                            exists = True
                        End If
                    End If
                Next
                If Not exists Then
                    Dim permission As New Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission(Guid.NewGuid)
                    permission.PageVersionId = Widget.PageVersionId
                    permission.WidgetId = Widget.Id
                    permission.Role = role
                    permission.Permission = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW)
                    list.Add(permission)
                End If
            Else
                For Each permission As Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission In Widget.Permissions
                    If permission.Role.Id = RoleID Then
                        If permission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id Then
                            If list.Contains(permission) Then
                                list.Remove(permission)
                            End If
                        End If
                    End If
                Next
            End If

            If cbEdit.Checked Then
                Dim exists As Boolean = False
                For Each permission As Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission In Widget.Permissions
                    If permission.Role.Id = RoleID Then
                        If permission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id Then
                            exists = True
                        End If
                    End If
                Next
                If Not exists Then
                    Dim permission As New Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission(Guid.NewGuid)
                    permission.PageVersionId = Widget.PageVersionId
                    permission.WidgetId = Widget.Id
                    permission.Role = role
                    permission.Permission = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT)
                    list.Add(permission)
                End If
            Else
                For Each permission As Meanstream.Portal.Core.WidgetFramework.WidgetVersionPermission In Widget.Permissions
                    If permission.Role.Id = RoleID Then
                        If permission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id Then
                            If list.Contains(permission) Then
                                list.Remove(permission)
                            End If
                        End If
                    End If
                Next
            End If
        Next

        widget.Permissions.Clear()
        widget.Permissions.AddRange(list)

        If Not Date.TryParse(Me.txtStartDate.Text, New Date) Then
            Me.txtStartDate.Text = Date.Today
        End If

        If Not Date.TryParse(Me.txtEndDate.Text, New Date) Then
            Me.txtEndDate.Text = "12/31/9999"
        End If

        Widget.Title = Me.txtName.Text
        Widget.StartDate = Me.txtStartDate.Text
        Widget.EndDate = Me.txtEndDate.Text

        Try
            Dim manager As New Meanstream.Portal.Core.WidgetFramework.WidgetVersionManager(Widget)
            manager.Save()
            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
