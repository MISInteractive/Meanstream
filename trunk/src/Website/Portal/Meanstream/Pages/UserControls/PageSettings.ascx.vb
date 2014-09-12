Imports Meanstream.Portal.Core.Instrumentation

Partial Class Meanstream_Pages_UserControls_PageSettings
    Inherits System.Web.UI.UserControl

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId

    Dim PageTreeView As Meanstream.Web.UI.TreeView = New Meanstream.Web.UI.TreeView
    Dim InternalPageTreeView As Meanstream.Web.UI.TreeView = New Meanstream.Web.UI.TreeView

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.ddlPage.ItemTemplate = New CompiledTemplateBuilder(New BuildTemplateMethod(AddressOf BuildNodeCollection))
        Me.ddlInternalPage.ItemTemplate = New CompiledTemplateBuilder(New BuildTemplateMethod(AddressOf PageLinkBuildNodeCollection))
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            If Request.Params("Action") <> Nothing And Request.Params("Action") = "Add" Then
                Me.ddlPage.Visible = True
                Me.btnAdd.Visible = True
                Me.btnSave.Visible = False
                Me.btnCopyAdd.Visible = False
                Me.lblTitle.Text = "Create Page"
                Me.SetDefaultPageSettings()
            End If

            If Request.Params("Action") <> Nothing And Request.Params("Action") = "Edit" Then
                Me.ddlPage.Visible = True
                If Request.Params("VersionID") <> Nothing And Request.Params("VersionID") <> "" Then
                    Me.LoadPageSettings(New Guid(Request.Params("VersionID").ToString), True)
                    Me.btnAdd.Visible = False
                    Me.btnCopyAdd.Visible = False
                    Me.btnSave.Visible = True
                    Me.lblTitle.Text = "Settings"
                End If
            End If

            If Request.Params("Action") <> Nothing And Request.Params("Action") = "CopyAdd" Then
                If Request.Params("VersionID") <> Nothing And Request.Params("VersionID") <> "" Then
                    Me.LoadPageSettings(New Guid(Request.Params("VersionID").ToString), False)
                    Me.btnAdd.Visible = False
                    Me.btnSave.Visible = False
                    Me.btnCopyAdd.Visible = True
                    Me.lblTitle.Text = "Copy and Create Page"
                End If
            End If

            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            Me.btnAdd.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-create-page.png"
            Me.btnCopyAdd.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-create-page.png"

        End If
    End Sub

    Protected Sub SetDefaultPageSettings()
        'Me.SkinDropDown.DataSource = Meanstream.Portal.Core.Content.ContentService.Current.GetSkins
        Me.SkinDropDown.DataSource = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Find("PortalId=" & portalId.ToString).ToDataSet(True)
        Me.SkinDropDown.DataTextField = "SkinRoot"
        Me.SkinDropDown.DataValueField = "Id"
        Me.SkinDropDown.DataBind()
        Dim ListItem As Meanstream.Web.UI.ComboBoxItem = New Meanstream.Web.UI.ComboBoxItem
        ListItem.Text = "--Select--"
        ListItem.Value = "--Select--"
        Me.SkinDropDown.Items.Insert(0, ListItem)
        Me.SkinDropDown.DefaultDisplayValue = ListItem.Value
        Me.SkinDropDown.DefaultDisplayText = ListItem.Text

        Me.txtStartDate.Text = Date.Today
        Me.txtEndDate.Text = "12/31/2020"

        Me.chkDisplay.Checked = True
        Me.chkDisable.Checked = False
        Me.SecurityGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllRoles
        Me.SecurityGrid.DataBind()

        For Each GridRow As GridViewRow In SecurityGrid.Rows
            Dim lblRoleID As Literal = CType(GridRow.FindControl("RoleName"), Literal)

            If lblRoleID.Text = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Then
                'Default Administrator account
                Dim cbView As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxView"), CheckBox)
                Dim cbEdit As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxEdit"), CheckBox)
                cbView.Checked = True
                cbView.Enabled = False
                cbEdit.Checked = True
                cbEdit.Enabled = False
            End If

            If lblRoleID.Text = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Or _
            lblRoleID.Text = Meanstream.Portal.Core.AppConstants.CONTENT_ADMINISTRATOR Or _
            lblRoleID.Text = Meanstream.Portal.Core.AppConstants.HOST Then
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
                Dim cbView As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxView"), CheckBox)
                cbView.Checked = True
            End If
        Next
    End Sub

    Protected Sub LoadPageSettings(ByVal VersionID As Guid, ByVal IsNotCopy As Boolean)
        Dim Page As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(VersionID)
        Me.txtTitle.Text = Page.MetaTitle
        Me.txtDescription.Text = Page.MetaDescription
        Me.txtKeywords.Text = Page.MetaKeywords
        Me.txtMenuName.Text = Page.Name
        Me.txtMenuOrder.Text = Page.DisplayOrder

        'parse folders from url
        Dim Url As String = Page.Url
        If Url.Contains("/") Then
            Url = Url.Substring(Url.LastIndexOf("/") + 1)
        End If

        Me.txtUrlMapping.Text = Url

        Me.chkDisable.Checked = Page.DisableLink
        Me.chkDisplay.Checked = Page.IsVisible
        Me.litParentId.Value = Page.ParentId.ToString
        Me.cbEnableCaching.Checked = Page.EnableCaching
        Me.cbEnableViewState.Checked = Page.EnableViewState
        Me.cbIndex.Checked = Page.Index

        If Page.StartDate <> Nothing Then
            Me.txtStartDate.Text = Page.StartDate
        End If

        If Page.EndDate <> Nothing Then
            Me.txtEndDate.Text = Page.EndDate
        End If

        Me.SkinDropDown.DataSource = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Find("PortalId=" & portalId.ToString).ToDataSet(True)
        Me.SkinDropDown.DataTextField = "SkinRoot"
        Me.SkinDropDown.DataValueField = "Id"
        Me.SkinDropDown.DataBind()
        Me.SkinDropDown.SelectedValue = Page.Skin.Id.ToString

        Dim ListItem As Meanstream.Web.UI.ComboBoxItem = New Meanstream.Web.UI.ComboBoxItem
        ListItem.Text = "--Select--"
        ListItem.Value = "--Select--"
        Me.SkinDropDown.Items.Add(ListItem)
        Me.SkinTable.Visible = IsNotCopy
        Me.SecurityTab.Visible = IsNotCopy

        Me.SecurityGrid.DataSource = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllRoles
        Me.SecurityGrid.DataBind()

        For Each GridRow As GridViewRow In SecurityGrid.Rows
            Dim lblRoleID As Literal = CType(GridRow.FindControl("RoleName"), Literal)

            If lblRoleID.Text = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Or _
            lblRoleID.Text = Meanstream.Portal.Core.AppConstants.HOST Or _
            lblRoleID.Text = Meanstream.Portal.Core.AppConstants.CONTENT_ADMINISTRATOR Then
                Dim cbView As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxView"), CheckBox)
                Dim cbEdit As CheckBox = CType(GridRow.FindControl("RowLevelCheckBoxEdit"), CheckBox)
                cbView.Checked = True
                cbView.Enabled = False
                cbEdit.Checked = True
                cbEdit.Enabled = False
            End If

            If lblRoleID.Text = Meanstream.Portal.Core.AppConstants.ALLUSERS Then
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
            Dim RoleID As Guid = New Guid(lblRoleID.Text)

            For Each PagePermission As Meanstream.Portal.Core.Content.PageVersionPermission In Page.Permissions
                If PagePermission.Role.Id = RoleID Then
                    If PagePermission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id Then
                        cbView.Checked = True
                    End If

                    If PagePermission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id Then
                        cbEdit.Checked = True
                    End If
                End If
            Next
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PortalTrace.WriteLine("Meanstream_Pages_Settings.btnSave_Click()")

        Me.lblMessage.Text = ""

        If Me.txtMenuName.Text.Trim = "" Then
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = "Menu name is required."
            Return
        End If

        If Me.SkinDropDown.SelectedValue.Trim = "" Then
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = "Please select a Skin."
            Return
        End If

        If Me.txtMenuOrder.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Please specify the order in which this page will show up in the menu."
            Return
        End If

        Try
            Integer.Parse(Me.txtMenuOrder.Text)
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
            Return
        End Try

        If Not Date.TryParse(Me.txtStartDate.Text, New Date) Then
            Me.txtStartDate.Text = Date.Today
        End If

        If Not Date.TryParse(Me.txtEndDate.Text, New Date) Then
            Me.txtEndDate.Text = "12/31/9999"
        End If

        If Me.rbPageType.SelectedValue = 2 Then
            If Me.ddlInternalPage.SelectedValue = 0 Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Please select an existing page"
                Return
            End If
        End If

        Dim Url As String = ""
        If Me.rbPageType.SelectedValue = 1 Then
            Url = Me.txtUrlMapping.Text.Trim
        ElseIf Me.rbPageType.SelectedValue = 2 Then
            'find existing page url
            Url = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(Me.ddlInternalPage.SelectedValue)).Url
        ElseIf Me.rbPageType.SelectedValue = 3 Then
            Url = Me.txtExternalUrl.Text.Trim
        End If

	Dim parentid As Guid = New Guid("00000000-0000-0000-0000-000000000000")
        If Not Me.ddlPage.SelectedValue = "00000000-0000-0000-0000-000000000000" and Not Me.ddlPage.SelectedValue = "0" and Not Me.ddlPage.SelectedValue = "" Then
            parentid = New Guid(Me.ddlPage.SelectedValue)
        End If

        Dim Page As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(New Guid(Request.Params("VersionID").ToString))
        Page.MetaTitle = Me.txtTitle.Text
        Page.MetaKeywords = Me.txtKeywords.Text
        Page.MetaDescription = Me.txtDescription.Text
        Page.Url = Url
        Page.Type = Me.rbPageType.SelectedItem.Value
        Page.ParentId = parentid
        Page.Skin = Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(New Guid(Me.SkinDropDown.SelectedValue.Trim))
        Page.StartDate = Me.txtStartDate.Text
        Page.EndDate = Me.txtEndDate.Text
        Page.DisplayOrder = Me.txtMenuOrder.Text
        Page.IsVisible = Me.chkDisplay.Checked
        Page.DisableLink = Me.chkDisable.Checked
        Page.Name = Me.txtMenuName.Text
        Page.EnableCaching = Me.cbEnableCaching.Checked
        Page.EnableViewState = Me.cbEnableViewState.Checked
        Page.Index = Me.cbIndex.Checked


        'make a copy of permissions for edit
        Dim listArray(Page.Permissions.Count) As Meanstream.Portal.Core.Content.PageVersionPermission
        Page.Permissions.CopyTo(listArray)
        Dim list As List(Of Meanstream.Portal.Core.Content.PageVersionPermission) = listArray.ToList()
        'bug fix - toCopy adds an extra item
        list.RemoveAt(list.Count - 1)

        'Enumerate the GridViewRows
        For index As Integer = 0 To SecurityGrid.Rows.Count - 1
            'Programmatically access the CheckBox from the TemplateField
            Dim cbView As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxView"), CheckBox)
            Dim cbEdit As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxEdit"), CheckBox)
            Dim lblRoleID As Literal = CType(SecurityGrid.Rows(index).FindControl("RoleID"), Literal)
            Dim RoleID As Guid = New Guid(lblRoleID.Text)
            Dim role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(RoleID)

            'If no permission exists add one!                        
            If cbView.Checked Then
                Dim exists As Boolean = False
                For Each PagePermission As Meanstream.Portal.Core.Content.PageVersionPermission In Page.Permissions
                    If PagePermission.Role.Id = RoleID Then
                        If PagePermission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id Then
                            exists = True
                        End If
                    End If
                Next
                If Not exists Then
                    Dim permission As New Meanstream.Portal.Core.Content.PageVersionPermission(Guid.NewGuid)
                    permission.Role = role
                    permission.VersionId = New Guid(Request.Params("VersionID").ToString)
                    permission.Permission = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW)
                    list.Add(permission)
                End If
            Else
                For Each permission As Meanstream.Portal.Core.Content.PageVersionPermission In Page.Permissions
                    If permission.Role.Id = RoleID Then
                        If permission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id Then
                            If list.Contains(permission) Then
                                list.Remove(permission)
                            End If
                        End If
                    End If
                Next
            End If

            If cbEdit.Checked Then
                Dim exists As Boolean = False
                For Each PagePermission As Meanstream.Portal.Core.Content.PageVersionPermission In Page.Permissions
                    If PagePermission.Role.Id = RoleID Then
                        If PagePermission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id Then
                            exists = True
                        End If
                    End If
                Next
                If Not exists Then
                    Dim permission As New Meanstream.Portal.Core.Content.PageVersionPermission(Guid.NewGuid)
                    permission.Role = role
                    permission.VersionId = New Guid(Request.Params("VersionID").ToString)
                    permission.Permission = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT)
                    list.Add(permission)
                End If
            Else
                For Each permission As Meanstream.Portal.Core.Content.PageVersionPermission In Page.Permissions
                    If permission.Role.Id = RoleID Then
                        If permission.Permission.Id = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id Then
                            If list.Contains(permission) Then
                                list.Remove(permission)
                            End If
                        End If
                    End If
                Next
            End If
        Next

        Page.Permissions.Clear()
        Page.Permissions.AddRange(list)

        Try
            Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(Page)
            manager.Save()
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
        
        Me.LoadPageSettings(New Guid(Request.Params("VersionID").ToString), True)
        Me.btnSave.SuccessMessage = "Settings saved sucessfully"
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        PortalTrace.WriteLine("Meanstream_Pages_Settings.btnAdd_Click()")

        Me.lblMessage.Text = ""

        If Me.txtMenuName.Text.Trim = "" Then
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = "Menu name is required."
            Return
        End If

        If Me.SkinDropDown.SelectedValue.Trim = "" Then
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = "Please select a Skin."
            Return
        End If

        If Me.txtMenuOrder.Text.Trim = "" Then
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = "Please specify the order in which this page will show up in the menu."
            Return
        End If

        Try
            Integer.Parse(Me.txtMenuOrder.Text)
        Catch ex As Exception
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = ex.Message
            Return
        End Try

        PortalTrace.WriteLine("Meanstream_Pages_Settings.btnAdd_Click(): Iterate Security Grid")

        Dim ViewRoles As New List(Of String)
        Dim EditRoles As New List(Of String)

        'Enumerate the GridViewRows
        For index As Integer = 0 To SecurityGrid.Rows.Count - 1
            'Programmatically access the CheckBox from the TemplateField
            Dim cbView As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxView"), CheckBox)
            Dim litRoleName As Literal = CType(SecurityGrid.Rows(index).FindControl("RoleName"), Literal)
            Dim RoleName As String = litRoleName.Text

            'If View Access is checked
            If cbView.Checked Then
                ViewRoles.Add(RoleName)
            End If

            Dim cbEdit As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxEdit"), CheckBox)

            'If it's checked
            If cbEdit.Checked Then
                EditRoles.Add(RoleName)
            End If
        Next

        If Not Date.TryParse(Me.txtStartDate.Text, New Date) Then
            Me.txtStartDate.Text = Date.Now
        End If

        If Not Date.TryParse(Me.txtEndDate.Text, New Date) Then
            Me.txtEndDate.Text = "12/31/9999"
        End If

        If Me.rbPageType.SelectedValue = 2 Then
            If Me.ddlInternalPage.SelectedValue = "00000000-0000-0000-0000-000000000000" And Me.ddlInternalPage.SelectedValue = "0" Then
                Me.btnAdd.ThrowFailure = True
                Me.btnAdd.FailMessage = "Please select an existing page"
                Return
            End If
        End If

        Dim Url As String = ""
        If Me.rbPageType.SelectedValue = 1 Then
            Url = Me.txtUrlMapping.Text.Trim
        ElseIf Me.rbPageType.SelectedValue = 2 Then
            'find existing page url
            Url = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(Me.ddlInternalPage.SelectedValue)).Url
        ElseIf Me.rbPageType.SelectedValue = 3 Then
            Url = Me.txtExternalUrl.Text.Trim
        End If

        Dim parentid As Guid = New Guid("00000000-0000-0000-0000-000000000000")
        If Me.ddlPage.SelectedValue <> "00000000-0000-0000-0000-000000000000" And Me.ddlPage.SelectedValue <> "0" Then
            parentid = New Guid(Me.ddlPage.SelectedValue)
        End If

        Try
            Dim PageVersionBase As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.Create(portalId, Me.txtTitle.Text, Me.txtKeywords.Text, Me.txtDescription.Text, Url, _
            Me.rbPageType.SelectedItem.Value, parentid, New Guid(Me.SkinDropDown.SelectedValue), Me.txtStartDate.Text, Me.txtEndDate.Text, Me.txtMenuOrder.Text, Me.chkDisplay.Checked, _
            Me.chkDisable.Checked, Me.txtMenuName.Text, ViewRoles.ToArray, EditRoles.ToArray, Me.cbEnableCaching.Checked, Me.cbEnableViewState.Checked, Me.cbIndex.Checked)

            Me.btnAdd.SuccessMessage = "Page Created Successfully."
            Me.litVersionId.Value = PageVersionBase.Id.ToString
            Me.btnAdd.Enabled = False
        Catch ex As Exception
            Me.btnAdd.ThrowFailure = True
            Me.btnAdd.FailMessage = ex.Message
            Meanstream.Portal.Core.Instrumentation.PortalTrace.Fail(ex.Message, DisplayMethodInfo.DoNotDisplay)
        End Try
    End Sub

    Protected Sub btnCopyAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyAdd.Click
        Me.lblMessage.Text = ""

        If Me.SkinDropDown.SelectedValue = "--Select--" Then
            Me.lblMessage.Text = "Please select a Design Skin..."
            Return
        End If

        If Me.txtMenuOrder.Text.Trim = "" Then
            Me.lblMessage.Text = "Please specify the order in which this page will show up in the menu."
            Return
        End If

        Try
            Integer.Parse(Me.txtMenuOrder.Text)
        Catch ex As Exception
            Me.lblMessage.Text = "Menu Order must be a number."
            Return
        End Try

        Try
            Dim ViewRoles As New List(Of String)
            Dim EditRoles As New List(Of String)

            'Enumerate the GridViewRows
            For index As Integer = 0 To SecurityGrid.Rows.Count - 1
                'Programmatically access the CheckBox from the TemplateField
                Dim cbView As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxView"), CheckBox)
                Dim litRoleName As Literal = CType(SecurityGrid.Rows(index).FindControl("RoleName"), Literal)
                Dim RoleName As String = litRoleName.Text

                'If View Access is checked
                If cbView.Checked Then
                    ViewRoles.Add(RoleName)
                End If

                Dim cbEdit As CheckBox = CType(SecurityGrid.Rows(index).FindControl("RowLevelCheckBoxEdit"), CheckBox)

                'If it's checked
                If cbEdit.Checked Then
                    EditRoles.Add(RoleName)
                End If
            Next

            If Not Date.TryParse(Me.txtStartDate.Text, New Date) Then
                Me.txtStartDate.Text = Date.Now
            End If

            If Not Date.TryParse(Me.txtEndDate.Text, New Date) Then
                Me.txtEndDate.Text = "12/31/9999"
            End If

            If Me.rbPageType.SelectedValue = 2 Then
                If Me.ddlInternalPage.SelectedValue = "00000000-0000-0000-0000-000000000000" And Me.ddlInternalPage.SelectedValue = "0" Then
                    Me.btnAdd.ThrowFailure = True
                    Me.btnAdd.FailMessage = "Please select an existing page"
                    Return
                End If
            End If

            Dim Url As String = ""
            If Me.rbPageType.SelectedValue = 1 Then
                Url = Me.txtUrlMapping.Text.Trim
            ElseIf Me.rbPageType.SelectedValue = 2 Then
                'find existing page url
                Url = Meanstream.Portal.Core.Content.ContentService.Current.GetPage(New Guid(Me.ddlInternalPage.SelectedValue)).Url
            ElseIf Me.rbPageType.SelectedValue = 3 Then
                Url = Me.txtExternalUrl.Text.Trim
            End If

            Dim parentid As Guid = New Guid("00000000-0000-0000-0000-000000000000")
            If Not Me.ddlPage.SelectedValue = "00000000-0000-0000-0000-000000000000" Then
                parentid = New Guid(Me.ddlPage.SelectedValue)
            End If

            Dim PageVersionBase As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.CopyAndCreate(portalId, New Guid(Request.Params("VersionID").ToString), Me.txtTitle.Text, Me.txtKeywords.Text, Me.txtDescription.Text, Url, _
            Me.rbPageType.SelectedValue, parentid, New Guid(Me.SkinDropDown.SelectedValue), Me.txtStartDate.Text, Me.txtEndDate.Text, Me.txtMenuOrder.Text, Me.chkDisplay.Checked, _
            Me.chkDisable.Checked, Me.txtMenuName.Text, ViewRoles.ToArray, EditRoles.ToArray, Me.cbEnableCaching.Checked, Me.cbEnableViewState.Checked, Me.cbIndex.Checked)

            Me.litVersionId.Value = PageVersionBase.Id.ToString
            Me.btnCopyAdd.Enabled = False
            Me.btnCopyAdd.SuccessMessage = "Page Created Successfully"
        Catch ex As Exception
            Me.btnCopyAdd.ThrowFailure = True
            Me.btnCopyAdd.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub SkinDropDown_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SkinDropDown.SelectedValueChanged
        If Request.Params("VersionID") <> Nothing Then
            Dim PageID As String = Request.Params("VersionID").ToString
        End If
    End Sub

    Protected Sub rbPageType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPageType.SelectedIndexChanged
        If Me.rbPageType.SelectedItem.Value = 1 Then
            Me.InternalPagePanel.Visible = False
            Me.UrlPanel.Visible = True
            Me.InternalUrl.Visible = True
            Me.ExternalUrl.Visible = False
        ElseIf Me.rbPageType.SelectedItem.Value = 2 Then
            Me.InternalPagePanel.Visible = True
            Me.UrlPanel.Visible = False
        Else
            Me.InternalPagePanel.Visible = False
            Me.UrlPanel.Visible = True
            Me.InternalUrl.Visible = False
            Me.ExternalUrl.Visible = True
        End If
    End Sub

    Protected Sub TreeView_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TreeView As TreeView = DirectCast(sender, TreeView)
        Me.ddlPage.SelectedValue = TreeView.SelectedNode.Value
        Me.ddlPage.SelectedText = TreeView.SelectedNode.Text
        Me.litParentId.Value = TreeView.SelectedNode.Value
    End Sub

    Private Sub BuildNodeCollection(ByVal container As Control)
        PageTreeView = New Meanstream.Web.UI.TreeView

        PageTreeView.LineImagesFolder = "~/App_Themes/" & Page.Theme & "/Treeview/images/ComboTreeLineImages"

        AddHandler PageTreeView.SelectedNodeChanged, AddressOf Me.TreeView_SelectedNodeChanged

        Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)

        Dim TopLevelPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", New Guid("00000000-0000-0000-0000-000000000000"))

        TopLevelPages.Sort("DisplayOrder")

        Dim DefaultNode As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
        DefaultNode.Text = "This Page has no Parent"

        DefaultNode.Value = "00000000-0000-0000-0000-000000000000"
        DefaultNode.NavigateUrl = click(DefaultNode.Text, DefaultNode.Value)
        PageTreeView.Nodes.Add(DefaultNode)

        For Each ParentPage As Meanstream.Portal.Core.Entities.MeanstreamPage In TopLevelPages

            Dim ParentPageLink As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
            ParentPageLink.Text = ParentPage.Name
            ParentPageLink.Value = ParentPage.Id.ToString
            ParentPageLink.NavigateUrl = click(ParentPageLink.Text, ParentPageLink.Value)

            PageTreeView.Nodes.Add(ParentPageLink)

            'get all child pages
            Dim ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ParentPage.Id)

            ChildPages.Sort("DisplayOrder")

            If ChildPages.Count > 0 Then

                Me.RecurseForChildren(ParentPage.Id, ParentPageLink, ChildPages, PageList)

            End If

        Next

        container.Controls.Add(PageTreeView)

        If Request.Params("Action") <> Nothing And Request.Params("Action") = "Add" Then
            'Populate Page Tree View
            Me.litParentId.Value = 0
            Me.ddlPage.SelectedValue = 0
            Me.ddlPage.SelectedText = "This Page has no Parent"
            Me.UrlPanel.Visible = True
            Me.InternalUrl.Visible = True
            Me.ExternalUrl.Visible = False
            Me.rbPageType.SelectedValue = 1
        Else

            Dim Page As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(New Guid(Request.Params("VersionID").ToString))
            'Populate Page Tree View

            If Page.ParentId <> Nothing Then
                Me.litParentId.Value = Page.ParentId.ToString
                Dim Node As Meanstream.Web.UI.TreeNode = Nothing
                For Each TreeNode As Meanstream.Web.UI.TreeNode In PageTreeView.Nodes
                    RecurseForSelectNode(TreeNode, Page)
                Next
            Else
                Me.litParentId.Value = ""
                Me.ddlPage.SelectedValue = ""
                Me.ddlPage.SelectedText = "This Page has no Parent"
            End If
            'end
        End If

    End Sub

    Private Sub RecurseForSelectNode(ByVal TreeNode As Meanstream.Web.UI.TreeNode, ByVal Page As Meanstream.Portal.Core.Content.PageVersion)

        If TreeNode.Value = Page.ParentId.ToString Then
            TreeNode.Expand()
            TreeNode.Selected = True
            Me.ddlPage.SelectedValue = TreeNode.Value
            Me.ddlPage.SelectedText = TreeNode.Text
            ExpandNodes(TreeNode)

        Else
            'children
            For Each ChildNode As Meanstream.Web.UI.TreeNode In TreeNode.ChildNodes
                RecurseForSelectNode(ChildNode, Page)
            Next
        End If

    End Sub

    Private Sub ExpandNodes(ByVal Node As Meanstream.Web.UI.TreeNode)
        If Node.Parent Is Nothing Then
            Node.ExpandAll()
        Else
            ExpandNodes(Node.Parent)
        End If
    End Sub

    Private Function RecurseForChildren(ByVal ParentID As Guid, ByVal ParentNode As Meanstream.Web.UI.TreeNode, ByVal ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage), ByVal PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)) As Meanstream.Web.UI.TreeNode

        For Each ChildPage As Meanstream.Portal.Core.Entities.MeanstreamPage In ChildPages

            Dim ChildPageLink As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
            ChildPageLink.Text = ChildPage.Name
            ChildPageLink.Value = ChildPage.Id.ToString
            ChildPageLink.NavigateUrl = click(ChildPageLink.Text, ChildPageLink.Value)
            ParentNode.ChildNodes.Add(ChildPageLink)

            'get all child pages
            Dim SubChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ChildPage.Id)

            SubChildPages.Sort("DisplayOrder")

            If SubChildPages.Count > 0 Then

                Me.RecurseForChildren(ChildPage.Id, ChildPageLink, SubChildPages, PageList)

            End If

        Next

        Return ParentNode

    End Function

    Protected Sub PageLinkTreeView_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TreeView As TreeView = DirectCast(sender, TreeView)
        Me.ddlInternalPage.SelectedValue = TreeView.SelectedNode.Value
        Me.ddlInternalPage.SelectedText = TreeView.SelectedNode.Text
    End Sub

    Private Sub PageLinkBuildNodeCollection(ByVal container As Control)
        InternalPageTreeView = New Meanstream.Web.UI.TreeView
        InternalPageTreeView.LineImagesFolder = "~/App_Themes/" & Page.Theme & "/Treeview/images/ComboTreeLineImages"

        AddHandler InternalPageTreeView.SelectedNodeChanged, AddressOf Me.PageLinkTreeView_SelectedNodeChanged

        Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)
        PageList = PageList.FindAll("Type", 1)

        Dim TopLevelPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", New Guid("00000000-0000-0000-0000-000000000000"))
        TopLevelPages.Sort("DisplayOrder")

        Dim DefaultNode As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
        DefaultNode.Text = "Select a page to link to"

        DefaultNode.Value = "00000000-0000-0000-0000-000000000000"
        DefaultNode.NavigateUrl = PageLinkClick(DefaultNode.Text, DefaultNode.Value)
        InternalPageTreeView.Nodes.Add(DefaultNode)

        For Each ParentPage As Meanstream.Portal.Core.Entities.MeanstreamPage In TopLevelPages

            Dim ParentPageLink As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
            ParentPageLink.Text = ParentPage.Name
            ParentPageLink.Value = ParentPage.Id.ToString
            ParentPageLink.NavigateUrl = PageLinkClick(ParentPageLink.Text, ParentPageLink.Value)

            InternalPageTreeView.Nodes.Add(ParentPageLink)

            'get all child pages
            Dim ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ParentPage.Id)

            ChildPages.Sort("DisplayOrder")

            If ChildPages.Count > 0 Then

                Me.PageLinkRecurseForChildren(ParentPage.Id, ParentPageLink, ChildPages, PageList)

            End If

        Next
        container.Controls.Add(InternalPageTreeView)

        If Request.Params("Action") <> Nothing And Request.Params("Action") = "Add" Then
            Me.ddlInternalPage.SelectedValue = 0
            Me.ddlInternalPage.SelectedText = "Select a page to link to"
            Me.InternalPagePanel.Visible = False
        Else
            Dim Page As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(New Guid(Request.Params("VersionID").ToString))
            'handle page tyep
            Me.rbPageType.SelectedValue = Page.Type
            Select Case Page.Type
                Case 1
                    'basic page
                    Me.InternalPagePanel.Visible = False
                    Me.UrlPanel.Visible = True
                    Me.InternalUrl.Visible = True
                    Me.ExternalUrl.Visible = False
                    'Me.txtUrlMapping.Text = Page.Properties.Url

                Case 2
                    'direct to internal page
                    'Page Type Treeview
                    Me.InternalPagePanel.Visible = True
                    Me.UrlPanel.Visible = False

                    Dim PageTypeNode As Meanstream.Web.UI.TreeNode = Nothing
                    Dim SelectedPage As Meanstream.Portal.Core.Content.Page = Meanstream.Portal.Core.Content.ContentService.Current.GetPageByUrl(portalId, Page.Url)
                    'PAGE is not of type 1
                    If SelectedPage Is Nothing Then
                        Exit Select
                    End If

                    For Each TreeNode As Meanstream.Web.UI.TreeNode In InternalPageTreeView.Nodes
                        If TreeNode.Value = SelectedPage.Id.ToString Then
                            PageTypeNode = TreeNode
                            PageTypeNode.Expand()
                            PageTypeNode.Selected = True
                            Me.ddlInternalPage.SelectedValue = PageTypeNode.Value
                            Me.ddlInternalPage.SelectedText = PageTypeNode.Text
                        End If
                    Next
                    'end

                Case 3
                    'redirect to external page or file
                    Me.InternalPagePanel.Visible = False
                    Me.UrlPanel.Visible = True
                    Me.InternalUrl.Visible = False
                    Me.ExternalUrl.Visible = True
                    Me.txtExternalUrl.Text = Page.Url
            End Select
        End If

    End Sub

    Private Function PageLinkRecurseForChildren(ByVal ParentID As Guid, ByVal ParentNode As Meanstream.Web.UI.TreeNode, ByVal ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage), ByVal PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)) As Meanstream.Web.UI.TreeNode

        For Each ChildPage As Meanstream.Portal.Core.Entities.MeanstreamPage In ChildPages
            Dim ChildPageLink As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
            ChildPageLink.Text = ChildPage.Name
            ChildPageLink.Value = ChildPage.Id.ToString
            ChildPageLink.NavigateUrl = PageLinkClick(ChildPageLink.Text, ChildPageLink.Value)
            ParentNode.ChildNodes.Add(ChildPageLink)

            'get all child pages
            Dim SubChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ChildPage.Id)
            SubChildPages.Sort("DisplayOrder")

            If SubChildPages.Count > 0 Then
                Me.PageLinkRecurseForChildren(ChildPage.Id, ChildPageLink, SubChildPages, PageList)
            End If
        Next

        Return ParentNode
    End Function

    Private Function PageLinkClick(ByVal text As String, ByVal value As String) As String
        Dim s As String = ""
        Dim clientId As String = Me.ddlInternalPage.ClientID
        s = "javascript:MSComboBoxObject('" + text + "','" + value + "','" + clientId + "');"

        Return s
    End Function

    Private Function click(ByVal text As String, ByVal value As String) As String
        Dim s As String = ""
        Dim clientId As String = Me.ddlPage.ClientID
        s = "javascript:MSComboBoxObject('" + text + "','" + value + "','" + clientId + "');"

        Return s
    End Function
End Class
