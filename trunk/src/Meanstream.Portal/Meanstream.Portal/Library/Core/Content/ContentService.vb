Imports Meanstream.Portal.Core.Instrumentation
Imports System.Configuration
Imports System.Web
Imports System.Web.UI

Namespace Meanstream.Portal.Core.Content
    Public Class ContentService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As ContentService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As ContentService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateServiceInstance = New ContentService(machineName, appFriendlyName)
                            _privateServiceInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privateServiceInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

#Region " Methods "
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The content service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Content Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Content Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function Create(ByVal portalId As Guid, ByVal MetaTitle As String, ByVal MetaKeywords As String, ByVal MetaDescription As String, _
                                      ByVal Url As String, ByVal Type As Page.PageType, ByVal ParentPageId As Guid, ByVal SkinId As Guid, _
                                      ByVal StartDate As Date, ByVal EndDate As Date, ByVal Order As Integer, _
                                      ByVal DisplayInMenu As Boolean, ByVal DisableLink As Boolean, ByVal MenuTitle As String, _
                                      ByVal ViewRoles() As String, ByVal EditRoles() As String, ByVal EnableCaching As Boolean, ByVal EnableViewState As Boolean, ByVal Index As Boolean) As Meanstream.Portal.Core.Content.PageVersion

            PortalTrace.WriteLine("ContentService.Create()")

            If Type = Core.Content.Page.PageType.INTERNAL Then
                If Url.Trim = "" Then
                    Throw New ArgumentException("Please give this page a filename. Filenames must be unique.")
                End If

                If Url.Contains("/") Then
                    Throw New ArgumentException("Filename Cannot Contain the '/' character.")
                End If

                'build full url path
                Url = Meanstream.Portal.Core.Utilities.AppUtility.BuildUrlForPage(portalId, Url.Trim, ParentPageId)

                Dim PageCount As Integer = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" & portalId.ToString & " AND Url=" & Url.Trim).Count
                If PageCount > 0 Then
                    Throw New ArgumentException("Filename exists. Please supply a unique Filename.")
                End If
            End If

            If ViewRoles.Count = 0 Then
                Throw New ArgumentException("ViewRoles required")
            End If

            If EditRoles.Count = 0 Then
                Throw New ArgumentException("EditRoles required")
            End If

            Dim MainPage As Meanstream.Portal.Core.Entities.MeanstreamPage = New Meanstream.Portal.Core.Entities.MeanstreamPage
            MainPage.Title = MetaTitle
            MainPage.Name = MenuTitle
            MainPage.Description = MetaDescription
            MainPage.KeyWords = MetaKeywords
            MainPage.SkinId = SkinId
            MainPage.Type = Type
            MainPage.ParentId = ParentPageId
            MainPage.DisableLink = DisableLink
            MainPage.IsDeleted = False
            MainPage.DisplayOrder = Order
            MainPage.IsVisible = DisplayInMenu
            MainPage.Url = Url
            MainPage.IsPublished = False
            MainPage.IsHome = False
            MainPage.Author = HttpContext.Current.Profile.UserName
            MainPage.StartDate = StartDate
            MainPage.EndDate = EndDate
            MainPage.EnableCaching = EnableCaching
            MainPage.EnableViewState = EnableViewState
            MainPage.Index = Index
            'Create new Page
            MainPage.PortalId = portalId
            MainPage.Id = Guid.NewGuid
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Insert(MainPage)

            'Add the Version
            Dim Page As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = New Meanstream.Portal.Core.Entities.MeanstreamPageVersion
            Page.Title = MetaTitle
            Page.Name = MenuTitle
            Page.Description = MetaDescription
            Page.KeyWords = MetaKeywords
            Page.SkinId = SkinId
            Page.ParentId = ParentPageId
            Page.DisableLink = DisableLink
            Page.IsDeleted = False
            Page.DisplayOrder = Order
            Page.IsVisible = DisplayInMenu
            Page.Url = Url.Trim
            Page.Type = Type
            Page.IsPublished = False
            Page.PageId = MainPage.Id
            Page.CreatedDate = Date.Now
            Page.LastSavedDate = Date.Now
            Page.Approved = False
            Page.Author = HttpContext.Current.Profile.UserName
            Page.PortalId = portalId
            Page.StartDate = StartDate
            Page.EndDate = EndDate
            Page.EnableCaching = EnableCaching
            Page.EnableViewState = EnableViewState
            Page.Index = Index
            Page.Id = Guid.NewGuid
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Insert(Page)

            Page = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.GetById(Page.Id)
            MainPage.VersionId = Page.Id
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Update(MainPage)

            'add workflow - UNCOMMENT
            'Me.CreateVersionWorkflow(0, Page.VersionId, MainPage.Id)

            'Page Permissions
            For Each Role As String In ViewRoles
                Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & Role.Trim)
                If Roles.Count = 0 Then
                    Continue For
                End If
                Dim PagePermissionView As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = New Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
                Dim PagePermissionView2 As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = New Meanstream.Portal.Core.Entities.MeanstreamPagePermission
                'get page id
                PagePermissionView.VersionId = Page.Id
                PagePermissionView2.PageId = Page.PageId

                PagePermissionView.RoleId = Roles(0).Id
                PagePermissionView2.RoleId = Roles(0).Id

                PagePermissionView.Id = Guid.NewGuid
                PagePermissionView2.Id = Guid.NewGuid
                'view permission
                PagePermissionView.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id
                PagePermissionView2.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id

                If Not Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Insert(PagePermissionView) Then
                    Throw New ApplicationException("MeanstreamPagePermissionVersionProvider.Insert failure - PagePermissionView")
                End If
                If Not Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Insert(PagePermissionView2) Then
                    Throw New ApplicationException("MeanstreamPagePermissionVersionProvider.Insert failure - PagePermissionView2")
                End If
            Next

            For Each Role As String In EditRoles

                Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & Role.Trim)
                If Roles.Count = 0 Then
                    Continue For
                End If
                Dim PagePermissionEdit As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = New Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
                Dim PagePermissionEdit2 As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = New Meanstream.Portal.Core.Entities.MeanstreamPagePermission
                PagePermissionEdit.VersionId = Page.Id
                PagePermissionEdit2.PageId = Page.PageId
                PagePermissionEdit.RoleId = Roles(0).Id
                PagePermissionEdit2.RoleId = Roles(0).Id

                PagePermissionEdit.Id = Guid.NewGuid
                PagePermissionEdit.OriginalId = PagePermissionEdit.Id
                PagePermissionEdit2.Id = Guid.NewGuid
                PagePermissionEdit2.OriginalId = PagePermissionEdit2.Id

                'edit permission
                PagePermissionEdit.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id
                PagePermissionEdit2.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id
                If Not Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Insert(PagePermissionEdit) Then
                    Throw New ApplicationException("MeanstreamPagePermissionVersionProvider.Insert failure - PagePermissionEdit")
                End If
                If Not Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Insert(PagePermissionEdit2) Then
                    Throw New ApplicationException("MeanstreamPagePermissionVersionProvider.Insert failure - PagePermissionEdit2")
                End If
            Next
            'End Permissions

            'When a new page is added then add the default widget to the page here
            'Look up skin
            Dim Skin As Meanstream.Portal.Core.Content.Skin = ContentService.Current.GetSkin(Page.SkinId)
            'Get Default Widget
            Dim DefaultModuleDef As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions = WidgetFramework.WidgetService.Current.GetModuleDefinitions.Find("IsDefault", True)
            Dim ModuleControls As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleControls) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleControlsProvider.Find("ModuleDefId=" & DefaultModuleDef.Id.ToString)
            Dim ModuleControlsVersion As Meanstream.Portal.Core.Entities.MeanstreamModuleControls = ModuleControls.Find("ControlKey", "Version")

            'For each skinpane add a new default widget
            For Each SkinPane As Meanstream.Portal.Core.Content.SkinZone In Skin.Zones

                Dim Modules As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersion
                Modules.AllPages = False
                Modules.IsDeleted = False
                Modules.PageVersionId = Page.Id
                Modules.CreatedBy = HttpContext.Current.Profile.UserName ' rename to CreatedBy
                'look up html
                Modules.ModuleDefId = DefaultModuleDef.Id ' assign default id here
                Modules.Title = Page.Name & " > " & SkinPane.Pane
                Modules.SkinPaneId = SkinPane.Id
                Modules.DisplayOrder = 0
                Modules.StartDate = StartDate
                Modules.EndDate = EndDate
                Modules.Id = Guid.NewGuid
                Modules.LastModifiedDate = Date.Now
                Modules.LastModifiedBy = HttpContext.Current.Profile.UserName
                Modules.DeletedDate = ""
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Insert(Modules)

                Dim Modules2 As Meanstream.Portal.Core.Entities.MeanstreamModule = New Meanstream.Portal.Core.Entities.MeanstreamModule
                Modules2.AllPages = False
                Modules2.IsDeleted = False
                'look up html
                Modules2.ModuleDefId = DefaultModuleDef.Id ' assign default id here
                Modules2.Title = Page.Name & " > " & SkinPane.Pane
                Modules2.CreatedBy = HttpContext.Current.Profile.UserName ' rename to CreatedBy
                Modules2.DisplayOrder = 0
                Modules2.SkinPaneId = SkinPane.Id
                Modules2.PageId = MainPage.Id
                Modules2.StartDate = StartDate
                Modules2.EndDate = EndDate
                Modules2.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Insert(Modules2)

                'Widget Permissions
                For Each Role As String In ViewRoles
                    Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & Role.Trim)
                    If Roles.Count = 0 Then
                        Continue For
                    End If
                    Dim ModulePermissionView As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                    Dim ModulePermissionView2 As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = New Meanstream.Portal.Core.Entities.MeanstreamModulePermission
                    ModulePermissionView.ModuleId = Modules.Id
                    ModulePermissionView2.ModuleId = Modules2.Id
                    ModulePermissionView.PageVersionId = Page.Id
                    ModulePermissionView.RoleId = Roles(0).Id
                    ModulePermissionView2.RoleId = Roles(0).Id

                    ModulePermissionView.Id = Guid.NewGuid
                    ModulePermissionView.OriginalId = ModulePermissionView.Id
                    ModulePermissionView2.Id = Guid.NewGuid
                    ModulePermissionView2.OriginalId = ModulePermissionView2.Id
                    'view permission
                    ModulePermissionView.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id
                    ModulePermissionView2.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Insert(ModulePermissionView2)
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionView)
                Next

                For Each Role As String In EditRoles
                    Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & Role.Trim)
                    If Roles.Count = 0 Then
                        Continue For
                    End If
                    Dim ModulePermissionEdit As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                    Dim ModulePermissionEdit2 As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = New Meanstream.Portal.Core.Entities.MeanstreamModulePermission
                    ModulePermissionEdit.ModuleId = Modules.Id
                    ModulePermissionEdit2.ModuleId = Modules2.Id
                    ModulePermissionEdit.PageVersionId = Page.Id
                    ModulePermissionEdit.RoleId = Roles(0).Id
                    ModulePermissionEdit2.RoleId = Roles(0).Id

                    ModulePermissionEdit.Id = Guid.NewGuid
                    ModulePermissionEdit.OriginalId = ModulePermissionEdit.Id
                    ModulePermissionEdit2.Id = Guid.NewGuid
                    ModulePermissionEdit2.OriginalId = ModulePermissionEdit2.Id

                    'view permission
                    ModulePermissionEdit.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id
                    ModulePermissionEdit2.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Insert(ModulePermissionEdit2)
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionEdit)
                Next

                'Call Custom Event Methods
                Dim Version As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = WidgetFramework.WidgetService.Current.GetWidgetVersionById(Modules.Id)
                Version.UserControl.OnAddToPageCreate(Modules2.Id)
                Version.UserControl.OnAddToPage()
            Next

            'Remove All Pages Cache
            'Meanstream.Portal.Core.Utilities.CacheUtility.RemoveAllPages()
            'Meanstream.Portal.Core.Utilities.CacheUtility.RefreshMenu()
            'Meanstream.Portal.Core.Utilities.CacheUtility.RefreshPages()
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_ENTITIES" & "_PORTAL_" & portalId.ToString)

            Dim pageVersion As New Meanstream.Portal.Core.Content.PageVersion(Page.Id)
            Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(pageVersion)
            manager.LoadFromDatasource()
            Return pageVersion
        End Function

        Public Function CopyAndCreate(ByVal portalId As Guid, ByVal VersionId As Guid, ByVal MetaTitle As String, ByVal MetaKeywords As String, ByVal MetaDescription As String, _
                                      ByVal Url As String, ByVal Type As Core.Content.Page.PageType, ByVal ParentPageId As Guid, ByVal SkinId As Guid, _
                                      ByVal StartDate As Date, ByVal EndDate As Date, ByVal Order As Integer, _
                                      ByVal DisplayInMenu As Boolean, ByVal DisableLink As Boolean, ByVal MenuTitle As String, _
                                      ByVal ViewRoles() As String, ByVal EditRoles() As String, ByVal EnableCaching As Boolean, ByVal EnableViewState As Boolean, ByVal Index As Boolean) As Meanstream.Portal.Core.Content.PageVersion

            PortalTrace.WriteLine("ContentService.CopyAndCreate()")

            If Type = Core.Content.Page.PageType.INTERNAL Then
                If Url.Trim = "" Then
                    Throw New ArgumentException("Please give this page a filename. Filenames must be unique.")
                End If

                If Url.Contains("/") Then
                    Throw New ArgumentException("Filename Cannot Contain the '/' character.")
                End If

                'build full url path
                Url = Meanstream.Portal.Core.Utilities.AppUtility.BuildUrlForPage(portalId, Url.Trim, ParentPageId)

                Dim PageCount As Integer = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" & portalId.ToString & " AND Url=" & Url.Trim).Count
                If PageCount > 0 Then
                    Throw New ArgumentException("Filename exists. Please supply a unique Filename.")
                End If
            End If

            If ViewRoles.Count = 0 Then
                Throw New ArgumentException("ViewRoles required")
            End If

            If EditRoles.Count = 0 Then
                Throw New ArgumentException("EditRoles required")
            End If

            Dim MainPage As Meanstream.Portal.Core.Entities.MeanstreamPage = New Meanstream.Portal.Core.Entities.MeanstreamPage
            MainPage.Title = MetaTitle
            MainPage.Name = MenuTitle
            MainPage.Description = MetaDescription
            MainPage.KeyWords = MetaKeywords
            MainPage.SkinId = SkinId
            MainPage.ParentId = ParentPageId
            MainPage.DisableLink = DisableLink
            MainPage.IsDeleted = False
            MainPage.DisplayOrder = Order
            MainPage.IsVisible = DisplayInMenu
            MainPage.Url = Url.Trim
            MainPage.Type = Type
            MainPage.IsPublished = False
            MainPage.IsHome = False
            MainPage.Author = HttpContext.Current.Profile.UserName
            'Create new Page
            MainPage.PortalId = portalId
            MainPage.StartDate = StartDate
            MainPage.EndDate = EndDate
            MainPage.EnableCaching = EnableCaching
            MainPage.EnableViewState = EnableViewState
            MainPage.Index = Index
            MainPage.Id = Guid.NewGuid
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Insert(MainPage)

            'Add the Version
            Dim Page As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = New Meanstream.Portal.Core.Entities.MeanstreamPageVersion
            Page.Title = MetaTitle
            Page.Name = MenuTitle
            Page.Description = MetaDescription
            Page.KeyWords = MetaKeywords
            Page.SkinId = SkinId
            Page.ParentId = ParentPageId
            Page.DisableLink = DisableLink
            Page.IsDeleted = False
            Page.DisplayOrder = Order
            Page.IsVisible = DisplayInMenu
            Page.Url = Url.Trim
            Page.Type = Type
            Page.IsPublished = False
            Page.PageId = MainPage.Id
            Page.CreatedDate = Date.Now
            Page.LastSavedDate = Date.Now
            Page.Approved = False
            Page.Author = HttpContext.Current.Profile.UserName
            Page.PortalId = portalId
            Page.StartDate = StartDate
            Page.EndDate = EndDate
            Page.EnableCaching = EnableCaching
            Page.EnableViewState = EnableViewState
            Page.Index = Index
            Page.Id = Guid.NewGuid
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Insert(Page)

            Page = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.GetById(Page.Id)
            MainPage.VersionId = Page.Id
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Update(MainPage)

            'add workflow - uncomments
            'Me.CreateVersionWorkflow(VersionId, Page.Id, MainPage.Id)

            'Page Permissions
            For Each Role As String In ViewRoles
                Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & Role.Trim)
                If Roles.Count = 0 Then
                    Continue For
                End If
                Dim PagePermissionView As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = New Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
                Dim PagePermissionView2 As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = New Meanstream.Portal.Core.Entities.MeanstreamPagePermission
                'get page id
                PagePermissionView.VersionId = Page.Id
                PagePermissionView2.PageId = Page.PageId

                PagePermissionView.RoleId = Roles(0).Id
                PagePermissionView2.RoleId = Roles(0).Id

                PagePermissionView.Id = Guid.NewGuid
                PagePermissionView.OriginalId = PagePermissionView.Id
                PagePermissionView2.Id = Guid.NewGuid
                PagePermissionView2.OriginalId = PagePermissionView2.Id

                'view permission
                PagePermissionView.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id
                PagePermissionView2.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Insert(PagePermissionView)
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Insert(PagePermissionView2)
            Next

            For Each Role As String In EditRoles
                Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & Role.Trim)
                If Roles.Count = 0 Then
                    Continue For
                End If

                Dim PagePermissionEdit As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = New Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
                Dim PagePermissionEdit2 As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = New Meanstream.Portal.Core.Entities.MeanstreamPagePermission
                PagePermissionEdit.VersionId = Page.Id
                PagePermissionEdit2.PageId = Page.PageId
                PagePermissionEdit.RoleId = Roles(0).Id
                PagePermissionEdit2.RoleId = Roles(0).Id

                PagePermissionEdit.Id = Guid.NewGuid
                PagePermissionEdit.OriginalId = PagePermissionEdit.Id
                PagePermissionEdit2.Id = Guid.NewGuid
                PagePermissionEdit2.OriginalId = PagePermissionEdit2.Id

                'edit permission
                PagePermissionEdit.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id
                PagePermissionEdit2.PermissionId = Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Insert(PagePermissionEdit)
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Insert(PagePermissionEdit2)
            Next
            'End Permissions

            Dim DefaultModuleDef As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions = WidgetFramework.WidgetService.Current.GetModuleDefinitions.Find("IsDefault", True)
            Dim ModuleControls As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleControls) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleControlsProvider.Find("ModuleDefID=" & DefaultModuleDef.Id.ToString)
            Dim ModuleControlsVersion As Meanstream.Portal.Core.Entities.MeanstreamModuleControls = ModuleControls.Find("ControlKey", "Version")

            Dim PageModules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("PageVersionId=" & VersionId.ToString)
            For Each PageModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In PageModules
                Dim AddModule As Boolean = False
                Dim Modules As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.GetById(PageModule.Id)

                Dim NewModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersion
                Dim NewModule2 As Meanstream.Portal.Core.Entities.MeanstreamModule = New Meanstream.Portal.Core.Entities.MeanstreamModule
                NewModule.CreatedBy = HttpContext.Current.Profile.UserName
                NewModule2.CreatedBy = HttpContext.Current.Profile.UserName

                'alltabs then use exsisting module
                If Modules.AllPages Then
                    NewModule = Modules
                    NewModule2.AllPages = Modules.AllPages
                    NewModule2.IsDeleted = Modules.IsDeleted
                    NewModule2.ModuleDefId = Modules.ModuleDefId
                    NewModule2.Title = Page.Name & " > " & Modules.Title
                    NewModule2.DisplayOrder = Modules.DisplayOrder
                    NewModule2.SkinPaneId = Modules.SkinPaneId
                    NewModule2.EndDate = Modules.EndDate
                    NewModule2.StartDate = Modules.StartDate
                    NewModule2.PageId = MainPage.Id

                    If Modules.SharedId <> Nothing Then
                        NewModule2.Id = Modules.SharedId
                    Else
                        'if its a new global module add
                        NewModule2.Id = Guid.NewGuid
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Insert(NewModule2)
                    End If

                    'NewModule2.SharedId = Modules.SharedId
                    'Do not add new module, use existing
                    AddModule = True
                Else
                    NewModule.AllPages = Modules.AllPages
                    NewModule.IsDeleted = Modules.IsDeleted
                    NewModule.ModuleDefId = Modules.ModuleDefId
                    NewModule.Title = Page.Name & " > " & Modules.Title
                    NewModule.SharedId = Modules.SharedId
                    NewModule.PageVersionId = Page.Id
                    NewModule.DisplayOrder = Modules.DisplayOrder
                    NewModule.SkinPaneId = Modules.SkinPaneId
                    NewModule.EndDate = Modules.EndDate
                    NewModule.StartDate = Modules.StartDate
                    NewModule.LastModifiedDate = Date.Now
                    NewModule.LastModifiedBy = HttpContext.Current.Profile.UserName
                    NewModule.Id = Guid.NewGuid
                    Modules.LastModifiedDate = Date.Now
                    Modules.LastModifiedBy = HttpContext.Current.Profile.UserName
                    Modules.DeletedDate = ""
                    AddModule = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Insert(NewModule)

                    NewModule2.AllPages = Modules.AllPages
                    NewModule2.IsDeleted = Modules.IsDeleted
                    NewModule2.ModuleDefId = Modules.ModuleDefId
                    NewModule2.Title = Page.Name & " > " & Modules.Title
                    NewModule2.DisplayOrder = Modules.DisplayOrder
                    NewModule2.SkinPaneId = Modules.SkinPaneId
                    NewModule2.EndDate = Modules.EndDate
                    NewModule2.StartDate = Modules.StartDate
                    NewModule2.PageId = MainPage.Id
                    NewModule2.Id = Guid.NewGuid
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Insert(NewModule2)
                End If

                Dim attributes As List(Of Meanstream.Portal.Core.Extensibility.Attribute) = Meanstream.Portal.Core.Extensibility.AttributeService.Current.GetAttributes(Modules.Id)

                For Each ModuleSetting As Meanstream.Portal.Core.Extensibility.Attribute In attributes
                    Meanstream.Portal.Core.Extensibility.AttributeService.Current.Create(NewModule.Id, ModuleSetting.Key, ModuleSetting.Value, ModuleSetting.DataType)
                Next

                ' new module means new permissions
                If Modules.AllPages = False Then
                    'Existing Page Module Permissions
                    Dim ModulePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("ModuleId=" & PageModule.Id.ToString)

                    For Each ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission In ModulePermissions
                        Dim NewModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                        NewModulePermission.ModuleId = NewModule.Id
                        NewModulePermission.PermissionId = ModulePermission.PermissionId
                        NewModulePermission.RoleId = ModulePermission.RoleId
                        NewModulePermission.PageVersionId = Page.Id

                        NewModulePermission.Id = Guid.NewGuid
                        NewModulePermission.OriginalId = NewModulePermission.Id

                        'Add new Permission
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(NewModulePermission)

                        Dim NewModulePermission2 As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = New Meanstream.Portal.Core.Entities.MeanstreamModulePermission
                        NewModulePermission2.ModuleId = NewModule2.Id
                        NewModulePermission2.PermissionId = ModulePermission.PermissionId
                        NewModulePermission2.RoleId = ModulePermission.RoleId

                        NewModulePermission2.Id = Guid.NewGuid
                        NewModulePermission2.OriginalId = NewModulePermission2.Id

                        'Add new Permission
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Insert(NewModulePermission2)
                    Next
                End If

                'Call Custom Event Methods
                Dim Version As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = WidgetFramework.WidgetService.Current.GetWidgetVersionById(Modules.Id)
                Version.UserControl.OnCopyAndAddFromVersion(NewModule.Id, NewModule2.Id)
            Next

            'Remove All Pages Cache
            'Meanstream.Portal.Core.Utilities.CacheUtility.RemoveAllPages()
            'Meanstream.Portal.Core.Utilities.CacheUtility.RefreshMenu()
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_ENTITIES" & "_PORTAL_" & portalId.ToString)

            Dim pageVersion As New Meanstream.Portal.Core.Content.PageVersion(Page.Id)
            Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(pageVersion)
            manager.LoadFromDatasource()
            Return pageVersion
        End Function

        Public Function GetPage(ByVal id As Guid) As Meanstream.Portal.Core.Content.Page
            PortalTrace.WriteLine([String].Concat("GetPage() ", AppFriendlyName, " #", ApplicationId, " d=", id))
            Dim page As Page = New Page(id)
            Dim manager As PageManager = New PageManager(page)
            manager.LoadFromDatasource()
            Return page
        End Function

        Public Function GetPageVersion(ByVal versionId As Guid) As Meanstream.Portal.Core.Content.PageVersion
            PortalTrace.WriteLine([String].Concat("GetPageVersion() ", AppFriendlyName, " #", ApplicationId, " d=", versionId.ToString))
            Dim page As PageVersion = New PageVersion(versionId)
            Dim manager As PageVersionManager = New PageVersionManager(page)
            manager.LoadFromDatasource()
            Return page
        End Function

        Public Function GetPageByUrl(ByVal portalId As Guid, ByVal Url As String) As Meanstream.Portal.Core.Content.Page
            PortalTrace.WriteLine([String].Concat("GetPageBaseByUrl() ", AppFriendlyName, " #", ApplicationId, " Url=", Url))

            If Url Is Nothing Or Url = "index" Then
                Try
                    Return New Meanstream.Portal.Core.Content.Page(Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" & portalId.ToString & " AND IsHome=True")(0).Id)
                Catch ex As Exception
                    PortalTrace.Fail([String].Concat("GetPageBaseByUrl() ", AppFriendlyName, " #", ApplicationId, " ERROR: Could Not Find Home Page " & ex.Message), DisplayMethodInfo.FullSignature)
                End Try
            End If

            'handle returnURL issue (.net returns ',' with paramaters) 
            If Url.Contains(",") Then
                Url = Url.Split(",")(0)
            End If

            Dim Query As Meanstream.Portal.Core.Data.MeanstreamPageQuery = New Meanstream.Portal.Core.Data.MeanstreamPageQuery
            Query.AppendEquals(Core.Entities.MeanstreamPageColumn.Url, Url)
            Query.AppendEquals("AND", Core.Entities.MeanstreamPageColumn.Type, "1")
            Query.AppendEquals("AND", Core.Entities.MeanstreamPageColumn.PortalId, portalId.ToString)
            Try
                Return Me.GetPage(Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(Query.GetParameters)(0).Id)
            Catch ex As Exception
                PortalTrace.Fail([String].Concat("GetPageBaseByUrl() ", AppFriendlyName, " #", ApplicationId, " PAGE NOT FOUND " & Url), DisplayMethodInfo.DoNotDisplay)
            End Try
            Return Nothing
        End Function

        Public Function GetAllPages(ByVal portalId As Guid) As List(Of Meanstream.Portal.Core.Content.Page)
            PortalTrace.WriteLine([String].Concat("GetAllPages() ", AppFriendlyName, " #", ApplicationId))

            Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_PORTAL_" & portalId.ToString)
            If pages Is Nothing Then
                'get the clean page list
                pages = New List(Of Meanstream.Portal.Core.Content.Page)()
                Dim pageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("IsDeleted=False AND PortalId=" & portalId.ToString)
                For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPage In pageList
                    Dim page As Page = New Page(entity.Id)
                    Dim manager As PageManager = New PageManager(page)
                    manager.Bind(entity)
                    pages.Add(page)
                Next
                'cache the pages
                Meanstream.Portal.Core.Utilities.CacheUtility.Add(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_PORTAL_" & portalId.ToString, pages)
            End If
            Return pages
        End Function

        Public Function GetRecycleBin(ByVal portalId As Guid) As List(Of Meanstream.Portal.Core.Content.Page)
            Dim pages As New List(Of Meanstream.Portal.Core.Content.Page)
            Dim pageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("IsDeleted=True AND PortalId=" & portalId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPage In pageList
                Dim page As Page = New Page(entity.Id)
                Dim manager As PageManager = New PageManager(page)
                manager.Bind(entity)
                pages.Add(page)
            Next
            Return pages
        End Function

        Public Function GetPagesBySkinId(ByVal skinId As Guid) As List(Of Meanstream.Portal.Core.Content.Page)
            PortalTrace.WriteLine([String].Concat("GetPagesBySkinId() ", AppFriendlyName, " #", ApplicationId))
            Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = New List(Of Meanstream.Portal.Core.Content.Page)
            'get the clean page list
            Dim pageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("SkinId=" & skinId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPage In pageList
                Dim page As Page = New Page(entity.Id)
                Dim manager As PageManager = New PageManager(page)
                manager.Bind(entity)
                pages.Add(page)
            Next
            Return pages
        End Function

        Public Function GetPublishedPages(ByVal portalId As Guid) As List(Of Meanstream.Portal.Core.Content.Page)
            PortalTrace.WriteLine([String].Concat("GetPublishedPages() ", AppFriendlyName, " #", ApplicationId))
            
            Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.PUBLISHED_PAGES)

            If pages Is Nothing Then
                pages = New List(Of Meanstream.Portal.Core.Content.Page)

                Dim Query As Meanstream.Portal.Core.Data.MeanstreamPageQuery = New Meanstream.Portal.Core.Data.MeanstreamPageQuery
                'Query.AppendEquals(Entities.MeanstreamPageColumn.IsVisible, "True")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.IsDeleted, "False")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.IsPublished, "True")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.PortalId, portalId.ToString)
                Query.AppendLessThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamPageColumn.StartDate, Date.Now.ToString)
                Query.AppendGreaterThanOrEqual("AND", Entities.MeanstreamPageColumn.EndDate, Date.Now.ToString)

                Dim pageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(Query.Parameters)
                For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPage In pageList
                    Dim page As Page = New Page(entity.Id)
                    Dim manager As PageManager = New PageManager(page)
                    manager.Bind(entity)
                    pages.Add(page)
                Next
                System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PUBLISHED_PAGES & "_PORTAL_" & portalId.ToString, pages, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Cache")), TimeSpan.Zero)
            End If

            Return pages
        End Function

        Public Function GetPageNavigation(ByVal portalId As Guid) As List(Of Meanstream.Portal.Core.Content.Page)
            PortalTrace.WriteLine([String].Concat("GetPageNavigation() ", AppFriendlyName, " #", ApplicationId))
            
            Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.MENU & "_PORTAL_" & portalId.ToString)

            If pages Is Nothing Then
                pages = New List(Of Meanstream.Portal.Core.Content.Page)

                Dim Query As Meanstream.Portal.Core.Data.MeanstreamPageQuery = New Meanstream.Portal.Core.Data.MeanstreamPageQuery
                Query.AppendEquals(Entities.MeanstreamPageColumn.IsVisible, "True")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.IsDeleted, "False")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.IsPublished, "True")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.PortalId, portalId.ToString)
                Query.AppendLessThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamPageColumn.StartDate, Date.Now.ToString)
                Query.AppendGreaterThanOrEqual("AND", Entities.MeanstreamPageColumn.EndDate, Date.Now.ToString)

                Dim pageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(Query.Parameters)
                For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPage In pageList
                    Dim page As Page = New Page(entity.Id)
                    Dim manager As PageManager = New PageManager(page)
                    manager.Bind(entity)
                    pages.Add(page)
                Next
                System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.MENU & "_PORTAL_" & portalId.ToString, pages, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Cache")), TimeSpan.Zero)
            End If

            Return pages
        End Function

        Public Function GetViewPagePermissions(ByVal portalId As Guid) As List(Of PagePermission)
            PortalTrace.WriteLine([String].Concat("GetViewPagePermissions() ", AppFriendlyName, " #", ApplicationId))
            
            Dim permissions As List(Of Meanstream.Portal.Core.Content.PagePermission)
            permissions = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS & "_PORTAL_" & portalId.ToString)

            If permissions Is Nothing Then
                permissions = New List(Of Meanstream.Portal.Core.Content.PagePermission)
                Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Find("PermissionId=" & Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id.ToString)
                For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermission In entities
                    Dim permission As Meanstream.Portal.Core.Content.PagePermission = New Meanstream.Portal.Core.Content.PagePermission(entity.Id)
                    'get manager and load each
                    Dim manager As New PagePermissionManager(permission)
                    manager.Bind(entity)
                    permissions.Add(permission)
                Next
                System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS & "_PORTAL_" & portalId.ToString, permissions, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Cache")), TimeSpan.Zero)
            End If

            Return permissions
        End Function

        Protected Friend Function GetPagePermissions(ByVal pageId As Guid) As List(Of PagePermission)
            PortalTrace.WriteLine([String].Concat("GetPagePermissions() ", AppFriendlyName, " #", ApplicationId, " pageId=", pageId))

            Dim permissions As List(Of Meanstream.Portal.Core.Content.PagePermission) = New List(Of Meanstream.Portal.Core.Content.PagePermission)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Find("PageId=" & pageId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermission In entities
                Dim permission As Meanstream.Portal.Core.Content.PagePermission = New Meanstream.Portal.Core.Content.PagePermission(entity.Id)
                'get manager and load each
                Dim manager As New PagePermissionManager(permission)
                manager.Bind(entity)
                permissions.Add(permission)
            Next
            Return permissions
        End Function

        Protected Friend Function GetPageVersionPermissions(ByVal pageVersionId As Guid) As List(Of PageVersionPermission)
            PortalTrace.WriteLine([String].Concat("GetPageVersionPermissions() ", AppFriendlyName, " #", ApplicationId, " pageVersionId=", pageVersionId))

            Dim permissions As List(Of Meanstream.Portal.Core.Content.PageVersionPermission) = New List(Of Meanstream.Portal.Core.Content.PageVersionPermission)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" & pageVersionId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion In entities
                Dim permission As Meanstream.Portal.Core.Content.PageVersionPermission = New Meanstream.Portal.Core.Content.PageVersionPermission(entity.Id)
                'get manager and load each
                Dim manager As New PageVersionPermissionManager(permission)
                manager.Bind(entity)
                permissions.Add(permission)
            Next
            Return permissions
        End Function

        Public Function HasViewPagePermissions(ByVal pageId As Guid, ByVal RoleId As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Find("PageId=" & pageId.ToString & " AND RoleId=" & RoleId.ToString & " AND PermissionId=" & Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id.ToString).Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Function HasEditPagePermissions(ByVal pageId As Guid, ByVal RoleId As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Find("PageId=" & pageId.ToString & " AND RoleId=" & RoleId.ToString & " AND PermissionId=" & Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id.ToString).Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Function GetPagesForUser(ByVal portalId As Guid, ByVal username As String) As List(Of Page)
            PortalTrace.WriteLine([String].Concat("GetPagesForUser() ", AppFriendlyName, " #", ApplicationId, " username=", username))
            Dim UserPages As List(Of Page) = New List(Of Page)
            'Get the list of pages
            Dim PageList As List(Of Page) = Me.GetAllPages(portalId)
            Dim Roles As List(Of Meanstream.Portal.Core.Membership.Role) = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRolesForUser(username)
            For Each Page As Page In PageList
                For Each Role As Meanstream.Portal.Core.Membership.Role In Roles
                    If Me.HasViewPagePermissions(Page.Id, Role.Id) Then
                        If Not UserPages.Contains(Page) Then
                            UserPages.Add(Page)
                        End If
                    End If
                Next
            Next
            Return UserPages
        End Function

        Public Function GetSkin(ByVal skinId As Guid) As Skin
            PortalTrace.WriteLine([String].Concat("GetSkin() ", AppFriendlyName, " #", ApplicationId, " skinId=", skinId))
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamSkins = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.GetById(skinId)
            Dim skin As New Skin(entity.Id)
            Dim manager As New SkinManager(skin)
            manager.Bind(entity)
            Return skin
        End Function

        Public Function GetSkins(ByVal portalId As Guid) As List(Of Meanstream.Portal.Core.Content.Skin)
            PortalTrace.WriteLine([String].Concat("GetSkins() ", AppFriendlyName, " #", ApplicationId))
            
            Dim skins As List(Of Meanstream.Portal.Core.Content.Skin) = New List(Of Meanstream.Portal.Core.Content.Skin)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkins) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Find("PortalId=" & portalId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamSkins In entities
                Dim skin As New Skin(entity.Id)
                Dim manager As New SkinManager(skin)
                manager.Bind(entity)
                skins.Add(skin)
            Next
            Return skins
        End Function

        Public Function GetSkinForPage(ByVal pageId As Guid) As Skin
            PortalTrace.WriteLine([String].Concat("GetSkinForPage() ", AppFriendlyName, " #", ApplicationId, " pageId=", pageId))
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPage = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.GetById(pageId)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page {0} cannot be located in database.", pageId))
            End If
            Return Me.GetSkin(entity.SkinId)
        End Function

        Public Function GetSkinForPageVerion(ByVal pageVersionId As Guid) As Skin
            PortalTrace.WriteLine([String].Concat("GetSkinForPageVerion() ", AppFriendlyName, " #", ApplicationId, " pageVersionId=", pageVersionId.ToString))
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.GetById(pageVersionId)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page version {0} cannot be located in database.", pageVersionId.ToString))
            End If
            Return Me.GetSkin(entity.SkinId)
        End Function

        Public Function GetSkinByPath(ByVal path As String) As Skin
            PortalTrace.WriteLine([String].Concat("GetSkinByPath() ", AppFriendlyName, " #", ApplicationId, " path=", path))
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkins) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Find("SkinSrc=" & path)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamSkins In entities
                Return Me.GetSkin(entity.Id)
            Next
            Return Nothing
        End Function

        Public Function CreateSkin(ByVal portalId As Guid, ByVal name As String, ByVal path As String) As Guid
            PortalTrace.WriteLine([String].Concat("CreateSkin() ", AppFriendlyName, " #", ApplicationId, " name=", name, " path=", path))

            If portalId = Nothing Then
                Throw New ArgumentException("portalId is null")
            End If

            Dim Skin As Skin = Me.GetSkinByPath(path)

            If Skin IsNot Nothing Then
                Throw New ApplicationException("skin already exists")
            End If

            Dim Page As System.Web.UI.Page = CType(HttpContext.Current.Handler, System.Web.UI.Page)

            Dim MasterPage As Control = Page.LoadControl(path)

            If MasterPage Is Nothing Then
                Throw New ApplicationException("Cannot find MasterPage")
            End If

            Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamSkins
            entity.SkinRoot = name
            entity.SkinSrc = path
            entity.PortalId = portalId
            entity.Id = Guid.NewGuid

            Dim SkinPaneList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = New Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane)

            For Each Control As Control In MasterPage.Controls
                'iterate through to find contentplaceholders
                If TypeOf (Control) Is System.Web.UI.HtmlControls.HtmlForm Then
                    For Each FormControl As Control In Control.Controls
                        If TypeOf (FormControl) Is System.Web.UI.WebControls.ContentPlaceHolder Then
                            Dim ContentPlaceHolder As System.Web.UI.WebControls.ContentPlaceHolder = FormControl
                            'add to list
                            Dim SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane = New Meanstream.Portal.Core.Entities.MeanstreamSkinPane
                            SkinPane.Pane = ContentPlaceHolder.ID
                            SkinPaneList.Add(SkinPane)
                        End If
                    Next
                End If
            Next

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Insert(entity) Then
                'add skin panes
                For Each SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane In SkinPaneList
                    SkinPane.SkinId = entity.Id
                    SkinPane.Id = Guid.NewGuid
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Insert(SkinPane)
                Next
                Return entity.Id
            Else
                Throw New ApplicationException("skin insert failed")
            End If
            Return Nothing
        End Function

        'Public Sub IndexAll()
        '    'delete index
        '    Dim Directory As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SITE_SEARCH_INDEX_DIRECTORY_STORE)
        '    Dim Reader As Meanstream.Portal.Core.Content.Search.IndexReader = New Meanstream.Portal.Core.Content.Search.IndexReader
        '    Reader.Directory = Directory.Value
        '    Reader.DeleteAllDocuments()
        '    're-index
        '    Dim Pages As List(Of Meanstream.Portal.Core.Content.Page) = Me.GetAllPages()
        '    For Each Page As Meanstream.Portal.Core.Content.Page In Pages
        '        If Page.Index Then
        '            Me.Index(Page.Id)
        '        End If
        '    Next
        'End Sub

        'Public Sub Index(ByVal pageId As Guid)
        '    Dim PageBase As Meanstream.Portal.Core.Content.Page = New Meanstream.Portal.Core.Content.Page(pageId)
        '    Dim manager As New PageManager(PageBase)
        '    manager.LoadFromDatasource()

        '    If Not PageBase.Index Then
        '        Exit Sub
        '    End If

        '    Dim Document As Meanstream.Portal.Core.Content.Search.Documents.Document = New Meanstream.Portal.Core.Content.Search.Documents.Document()
        '    Document.Add(New Meanstream.Portal.Core.Content.Search.Documents.Field("page_id", PageBase.Id.ToString, True, False))

        '    Dim Content As String = PageBase.MetaTitle
        '    Content = Content & " " & PageBase.MetaDescription
        '    Content = Content & " " & PageBase.MetaKeywords
        '    Content = Content & " " & PageBase.Url
        '    Content = Content & " " & PageBase.Name

        '    'For Each Widget As Meanstream.Portal.Core.WidgetFramework.Widget In PageBase.Widgets
        '    '    If GetType(Meanstream.Portal.Core.Content.ISearchable).IsAssignableFrom(Widget.GetType) Then
        '    '        Try
        '    '            Content = Content & " " & CType(Widget, Meanstream.Portal.Core.Content.ISearchable).Content.Text
        '    '        Catch ex As Exception
        '    '            PortalTrace.WriteLine("Index() " & ex.Message)
        '    '        End Try
        '    '    End If
        '    'Next

        '    'scrape page
        '    'Dim objWebClient As New System.Net.WebClient()
        '    'Dim portalContext As Meanstream.Portal.Core.PortalContext = Meanstream.Portal.Core.PortalContext.Current
        '    'Dim strURL As String = String.Concat(portalContext.OriginalUri.Scheme, "://", portalContext.SiteUrl, PageBase.Url)
        '    'Dim aRequestedHTML() As Byte
        '    'aRequestedHTML = objWebClient.DownloadData(strURL)
        '    'Dim objUTF8 As New System.Text.UTF8Encoding()
        '    'Dim strRequestedHTML As String
        '    'strRequestedHTML = objUTF8.GetString(aRequestedHTML)

        '    'Content = Content & " " & strRequestedHTML

        '    Document.Add(New Meanstream.Portal.Core.Content.Search.Documents.Field("content", Content.ToString, True, True))

        '    Dim Directory As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SITE_SEARCH_INDEX_DIRECTORY_STORE)

        '    Dim IndexWriter As Meanstream.Portal.Core.Content.Search.IndexWriter = New Meanstream.Portal.Core.Content.Search.IndexWriter
        '    IndexWriter.Directory = Directory.Value
        '    IndexWriter.Index(Document)
        'End Sub

        'Public Function Search(ByVal keyword As String) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)
        '    Dim Pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = New Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)
        '    Dim Directory As Meanstream.Portal.Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetSettingByName(Meanstream.Portal.Core.AppConstants.SITE_SEARCH_INDEX_DIRECTORY_STORE)

        '    Dim IndexReader As Meanstream.Portal.Core.Content.Search.IndexReader = New Meanstream.Portal.Core.Content.Search.IndexReader
        '    IndexReader.Directory = Directory.Value

        '    Dim Documents As List(Of Meanstream.Portal.Core.Content.Search.Documents.Document) = IndexReader.Search("content", Keyword)

        '    '0 hits return empty list
        '    If Documents.Count = 0 Then
        '        Return Pages
        '    End If

        '    Dim PageIds() As String = {}
        '    Dim Ids As String = ""
        '    Dim Index As Integer = 0

        '    For Each Document As Meanstream.Portal.Core.Content.Search.Documents.Document In Documents
        '        For Each Field As Meanstream.Portal.Core.Content.Search.Documents.Field In Document.Fields
        '            If Field.Name = "page_id" Then
        '                If Index = 0 Then
        '                    Ids = Field.Value
        '                Else
        '                    Ids = Ids & "," & Field.Value
        '                End If
        '                Index = Index + 1
        '            End If
        '        Next
        '    Next

        '    PageIds = Ids.Split(",")

        '    'search via ids
        '    Dim PageQuery As Meanstream.Portal.Core.Data.MeanstreamPageQuery = New Meanstream.Portal.Core.Data.MeanstreamPageQuery
        '    PageQuery.AppendIn(Meanstream.Portal.Core.Entities.MeanstreamPageColumn.Id, PageIds)
        '    Pages = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(PageQuery.GetParameters)

        '    Return Pages
        'End Function

        Public Function GetMostRecentPublished(ByVal portalId As Guid, ByVal returnCount As Integer) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)
            Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("IsDeleted=False AND IsPublished=True AND PortalId=" & portalId.ToString)
            PageList.Sort("PublishedDate DESC")
            If PageList.Count > returnCount Then
                Return PageList.GetRange(0, returnCount)
            End If
            Return PageList
        End Function

        Public Function GetMostRecentEdits(ByVal portalId As Guid, ByVal returnCount As Integer) As Meanstream.Portal.Core.Entities.VList(Of Meanstream.Portal.Core.Entities.VwMeanstreamRecentEdits)
            Dim VwRecentEdits As Meanstream.Portal.Core.Entities.VList(Of Meanstream.Portal.Core.Entities.VwMeanstreamRecentEdits) = Meanstream.Portal.Core.Data.DataRepository.VwMeanstreamRecentEditsProvider.Get("PortalId='" & portalId.ToString & "' AND LastModifiedBy='" & HttpContext.Current.Profile.UserName & "'", "LastModifiedDate DESC", 0, returnCount, returnCount)
            Return VwRecentEdits
        End Function

        Public Function GetPageVersionsByPageId(ByVal PageId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPageVersion)
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Find("PageId=" & PageId.ToString)
        End Function

#End Region

#Region " Properties "
        Private _appFriendlyName As String
        Public Property AppFriendlyName() As String
            Get
                Return _appFriendlyName
            End Get
            Private Set(ByVal value As String)
                _appFriendlyName = value
            End Set
        End Property

        Private _machineName As String
        Public Property MachineName() As String
            Get
                Return _machineName
            End Get
            Private Set(ByVal value As String)
                _machineName = value
            End Set
        End Property

        Private _applicationId As Guid
        Public Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
            Private Set(ByVal value As Guid)
                _applicationId = value
            End Set
        End Property
#End Region


#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Deinitialize()
        End Sub
#End Region
    End Class

End Namespace
