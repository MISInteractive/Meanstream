Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Content
    Public Class PageVersionManager
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntityManager

        Private _entity As PageVersion

        Sub New(ByRef attributeEntity As PageVersion)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("page version id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Core.Entities.MeanstreamPageVersion
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page version {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal page As Meanstream.Portal.Core.Entities.MeanstreamPageVersion)
            _entity.MetaTitle = page.Title
            _entity.Name = page.Name
            _entity.MetaDescription = page.Description
            _entity.MetaKeywords = page.KeyWords
            _entity.PageId = page.PageId.GetValueOrDefault
            _entity.ParentId = page.ParentId.GetValueOrDefault
            _entity.DisableLink = page.DisableLink
            _entity.IsDeleted = page.IsDeleted
            _entity.DisplayOrder = page.DisplayOrder.GetValueOrDefault
            _entity.IsVisible = page.IsVisible
            _entity.Url = page.Url.Trim
            _entity.IsPublished = page.IsPublished
            _entity.Approved = page.Approved.GetValueOrDefault
            _entity.Author = page.Author
            _entity.PortalId = page.PortalId.GetValueOrDefault
            _entity.StartDate = page.StartDate.GetValueOrDefault
            _entity.EndDate = page.EndDate.GetValueOrDefault
            _entity.EnableCaching = page.EnableCaching.GetValueOrDefault
            _entity.EnableViewState = page.EnableViewState.GetValueOrDefault
            _entity.Index = page.Index.GetValueOrDefault
            _entity.LastSavedDate = Date.Now
            _entity.CreatedDate = page.CreatedDate.GetValueOrDefault
            _entity.IsPublishedVersion = page.IsPublishedVersion
            _entity.Type = page.Type
            '_entity.Type = [Enum].Parse(GetType(Meanstream.Portal.Core.Content.Page.PageType), page.Type.GetValueOrDefault)

        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            If _entity.Type = Page.PageType.INTERNAL Then
                If _entity.Url.Trim = "" Then
                    Throw New InvalidOperationException("Please give this page a filename. Filenames must be unique.")
                End If

                Dim ParentList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId= " & _entity.PortalId.ToString & " AND Url=" & _entity.Url.Trim)
                If ParentList.Count > 0 Then
                    If ParentList(0).Id <> _entity.PageId Then
                        Throw New InvalidOperationException("Filename exists. Please supply a unique Filename.")
                    End If
                End If

                If _entity.Url.Contains("/") Then
                    Throw New InvalidOperationException("Filename Cannot Contain the '/' character.")
                End If

                'build full url path
                _entity.Url = Meanstream.Portal.Core.Utilities.AppUtility.BuildUrlForPage(_entity.PortalId, _entity.Url.Trim, _entity.ParentId)
            End If


            Dim existingEntity As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = Me.GetEntityFromDatasource

            'home page cannot have a parent
            'If _entity.Id.ToString = "1" Then
            '    Throw New Exception("The Home Page Cannot have a Parent Page.")
            'End If

            If _entity.ParentId.ToString <> "" Then
                'validate parentid so that both page id cannot be each other's parent
                Dim ParentPage As Meanstream.Portal.Core.Entities.MeanstreamPage = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.GetById(_entity.ParentId)
                If ParentPage IsNot Nothing Then
                    If ParentPage.ParentId = _entity.PageId Then
                        Throw New InvalidOperationException("No two pages can be the parent of each.")
                    End If

                    If _entity.ParentId = _entity.PageId Then
                        Throw New InvalidOperationException("This page cannot be a parent of itself.")
                    End If
                End If      
            End If

            Dim ChangeSkin As Boolean = False
            If _entity.Skin.Id <> existingEntity.SkinId Then
                ChangeSkin = True
            End If

            Try
                existingEntity.Name = _entity.Name
                existingEntity.Author = System.Web.HttpContext.Current.Profile.UserName
                existingEntity.Description = _entity.MetaDescription
                existingEntity.EndDate = _entity.EndDate
                existingEntity.KeyWords = _entity.MetaKeywords
                existingEntity.LastSavedDate = Date.Now
                existingEntity.ParentId = _entity.ParentId
                existingEntity.SkinId = _entity.Skin.Id
                existingEntity.StartDate = _entity.StartDate
                existingEntity.Title = _entity.MetaTitle
                existingEntity.Url = _entity.Url
                existingEntity.DisplayOrder = _entity.DisplayOrder
                existingEntity.IsVisible = _entity.IsVisible
                existingEntity.DisableLink = _entity.DisableLink
                existingEntity.IsDeleted = False
                existingEntity.Type = _entity.Type
                existingEntity.EnableCaching = _entity.EnableCaching
                existingEntity.EnableViewState = _entity.EnableViewState
                existingEntity.Index = _entity.Index
                'PageVersion.CreatedDate
                'PageVersion.Approved
                'PageVersion.VersionId
                'PageVersion.IsPublished
                'PageVersion.IsPublishedVersion
                'PageVersion.PortalId
            Catch ex As Exception
                Throw New ApplicationException("Values are null.")
            End Try
            
            If Not Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Update(existingEntity) Then
                Throw New ApplicationException("Update failed.")
            End If

            If _entity.Permissions Is Nothing Then
                Throw New ApplicationException("Permissions cannot be null.")
            End If

            'save permissions
            PageVersionPermissionManager.SavePermissions(_entity.Permissions)
            'save widgets

            If ChangeSkin Then
                'Get all pagemudels
                Dim PageModules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("PageVersionId=" & _entity.Id.ToString)
                'get new skinpanes
                Dim SkinPanes As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Find("SkinId=" & _entity.Skin.Id.ToString)

                For Each PageModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In PageModules
                    Dim SearchSkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane = SkinPanes.Find("Pane", Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.GetById(PageModule.SkinPaneId).Pane)

                    'update pagemodules with new skin pane id
                    If SearchSkinPane IsNot Nothing Then
                        PageModule.SkinPaneId = SearchSkinPane.Id
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Update(PageModule)
                    Else
                        PageModule.SkinPaneId = SkinPanes(0).Id
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Update(PageModule)
                    End If
                Next
            End If

        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()

            'delete widgets
            For Each widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion In _entity.Widgets
                Dim manager As New WidgetFramework.WidgetVersionManager(widget)
                manager.Delete()
            Next

            'Delete Workflows
            'Dim Workflows As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.Workflow) = Meanstream.Portal.Core.Data.DataRepository.WorkflowProvider.Find("PageId=" & Me.Properties.TabId & " AND DocumentId=" & Me.Properties.VersionId)
            'For Each Workflow As Meanstream.Portal.Core.Entities.Workflow In Workflows

            '    Dim Tasks As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.WorkflowTask) = Meanstream.Portal.Core.Data.DataRepository.WorkflowTaskProvider.Find("WorkflowId=" & Workflow.Id)
            '    For Each Task In Tasks
            '        Meanstream.Portal.Core.Data.DataRepository.WorkflowTaskProvider.Delete(Task)
            '    Next
            '    Meanstream.Portal.Core.Data.DataRepository.WorkflowProvider.Delete(Workflow)
            'Next

            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Delete(_entity.Id)
            PageVersionPermissionManager.DeletePermissions(_entity.Id)
        End Sub

        Public Sub Publish()
            PortalTrace.WriteLine("Publish(): Id=" & _entity.Id.ToString)
            'Remove Page and Menu Cache
            Meanstream.Portal.Core.Utilities.CacheUtility.RemoveEachPage()

            Dim PageVersionProperties As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = Me.GetEntityFromDatasource

            Try
                Dim IsPublish As Boolean = True
                Dim Author As String = System.Web.HttpContext.Current.Profile.UserName

                '************ SAVE VERSION SETTINGS|PERMISSIONS|PAGEMODULES ************
                'Add a new version
                Dim NewPageVersion As Meanstream.Portal.Core.Content.PageVersion = Me.CreateNewPageVersionBaseFromVersionID(_entity.Id)
                '************ END SAVE VERSION SETTINGS|PERMISSIONS|PAGEMODULES ************
                PortalTrace.WriteLine("Publish(): End CreateNewPageVersionBaseFromVersionID")

                Dim PageContent As Meanstream.Portal.Core.Content.Page = New Meanstream.Portal.Core.Content.Page(_entity.PageId) 'update
                Dim manager As New PageManager(PageContent)
                manager.LoadFromDatasource()

                '************************ unpublish existing versions ************************
                If IsPublish Then
                    Dim PageVersions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPageVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Find("PageId=" & PageContent.Id.ToString)
                    For Each PageVersionT As Meanstream.Portal.Core.Entities.MeanstreamPageVersion In PageVersions
                        PageVersionT.IsPublishedVersion = False
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Update(PageVersionT)
                    Next
                End If
                '************************ end unpublish ************************
                PortalTrace.WriteLine("Publish(): End Unpublish")

                '************************ create new published page and move over version properties ************************
                Dim PageMain As Meanstream.Portal.Core.Entities.MeanstreamPage = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.GetById(_entity.PageId)
                PageMain.Description = _entity.MetaDescription
                PageMain.DisableLink = _entity.DisableLink
                PageMain.EndDate = _entity.EndDate
                PageMain.IsDeleted = False

                If _entity.PageId.ToString = "1" Then
                    PageMain.IsHome = True
                Else
                    PageMain.IsHome = PageContent.IsHomePage
                End If

                PageMain.IsPublished = IsPublish
                PageMain.IsVisible = _entity.IsVisible
                PageMain.KeyWords = _entity.MetaKeywords
                PageMain.ParentId = _entity.ParentId
                PageMain.PortalId = _entity.PortalId
                PageMain.SkinId = _entity.Skin.Id
                PageMain.StartDate = _entity.StartDate
                PageMain.DisplayOrder = _entity.DisplayOrder
                PageMain.Title = _entity.MetaTitle
                PageMain.Url = _entity.Url
                PageMain.Name = _entity.Name
                PageMain.Type = _entity.Type
                PageMain.EnableCaching = _entity.EnableCaching
                PageMain.EnableViewState = _entity.EnableViewState
                PageMain.Index = _entity.Index

                'PageMain.Id = PageContent.Id

                PageMain.VersionId = NewPageVersion.Id 'updated

                '************************ assign the relationship to both page version/published ************************
                If IsPublish Then
                    PageMain.PublishedDate = Date.Now
                    PageMain.Author = Author
                    PageVersionProperties.IsPublished = True 'updated
                    PageVersionProperties.IsPublishedVersion = True 'updated
                    PageVersionProperties.LastSavedDate = Date.Now 'updated
                    PageVersionProperties.Author = Author 'updated
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Update(PageVersionProperties) 'updated
                End If
                'update page setting
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Update(PageMain)
                '************************ end assignment ************************
                PortalTrace.WriteLine("Publish(): End Assignment")

                '************************ Clear out all old modules and leave the shared modules alone ************************
                For Each Widget As Meanstream.Portal.Core.WidgetFramework.Widget In PageContent.Widgets
                    If Widget Is Nothing Then
                        Continue For
                    End If

                    'if global module then do nothing
                    If Widget.AllPages = False Then
                        Dim widgetManager As New WidgetFramework.WidgetManager(Widget)
                        widgetManager.Delete()

                        'For Each ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission In Widget.Permissions
                        '    'delete module permissions
                        '    Meanstream.Portal.Core.Data.DataRepository.ModulePermissionProvider.Delete(ModulePermission)
                        'Next

                        'For Each ModuleSetting As Meanstream.Portal.Core.Entities.ModuleSettings In Widget.Settings
                        '    Meanstream.Portal.Core.Data.DataRepository.ModuleSettingsProvider.Delete(ModuleSetting)
                        'Next

                        'Delete widget
                        'Widget.OnWidgetDelete()

                        'Delete Module
                        'Meanstream.Portal.Core.Data.DataRepository.ModulesProvider.Delete(Widget.Properties)
                    End If
                Next
                'delete page permissions
                For Each PagePermission As Meanstream.Portal.Core.Content.PagePermission In PageContent.Permissions
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Delete(PagePermission.Id)
                Next
                '************************************************ End Clear ************************************************
                PortalTrace.WriteLine("Publish(): End Clear Out Old Modules")

                '**************************** Now add new page modules, modules, and permissions ***************************
                For Each Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion In _entity.Widgets
                    Dim ModuleVersion As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Widget
                    Dim ModuleSettingsVersions As List(Of Meanstream.Portal.Core.Extensibility.Attribute) = Widget.Attributes

                    If ModuleVersion.AllPages Then
                        Dim GlobalContent As Meanstream.Portal.Core.Entities.MeanstreamModule = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.GetById(ModuleVersion.SharedId)

                        'if its new the add. If not then update
                        If GlobalContent Is Nothing Then
                            Dim GlobalModules As Meanstream.Portal.Core.Entities.MeanstreamModule = New Meanstream.Portal.Core.Entities.MeanstreamModule
                            GlobalModules.AllPages = ModuleVersion.AllPages
                            GlobalModules.IsDeleted = ModuleVersion.IsDeleted
                            GlobalModules.ModuleDefId = ModuleVersion.ModuleDefId
                            GlobalModules.Title = ModuleVersion.Title
                            GlobalModules.SkinPaneId = ModuleVersion.SkinPaneId
                            GlobalModules.DisplayOrder = ModuleVersion.DisplayOrder
                            GlobalModules.StartDate = ModuleVersion.StartDate
                            GlobalModules.EndDate = ModuleVersion.EndDate
                            GlobalModules.Id = Guid.NewGuid
                            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Insert(GlobalModules)

                            'Add Settings
                            For Each ModuleSettingsVersion As Meanstream.Portal.Core.Extensibility.Attribute In ModuleSettingsVersions
                                Dim ModuleSettings As Meanstream.Portal.Core.Extensibility.Attribute = New Meanstream.Portal.Core.Extensibility.Attribute
                                ModuleSettings.Key = ModuleSettingsVersion.Key
                                ModuleSettings.Value = ModuleSettingsVersion.Value
                                ModuleSettings.DataType = ModuleSettingsVersion.DataType
                                ModuleSettings.ComponentId = GlobalModules.Id
                                'copy attributes
                                Extensibility.AttributeService.Current.Create(ModuleSettings.ComponentId, ModuleSettings.Key, ModuleSettings.Value, ModuleSettings.DataType)
                                'ModuleVersion.Attributes.Add(ModuleSettings)
                                'Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleSettingsProvider.Insert(ModuleSettings)
                            Next

                            ModuleVersion.SharedId = GlobalModules.Id
                            'Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Update(ModuleVersion)
                            Dim managerVersion As New WidgetFramework.WidgetVersionManager(ModuleVersion)
                            managerVersion.Save()

                            'add permissions
                            Dim ModulePermissionVersionList1 As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("ModuleId=" & ModuleVersion.Id.ToString)
                            For Each ModulePermissionVersion As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission In ModulePermissionVersionList1
                                Dim ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = New Meanstream.Portal.Core.Entities.MeanstreamModulePermission
                                ModulePermission.ModuleId = GlobalModules.Id
                                ModulePermission.PermissionId = ModulePermissionVersion.PermissionId
                                ModulePermission.RoleId = ModulePermissionVersion.RoleId
                                ModulePermission.Id = Guid.NewGuid
                                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Insert(ModulePermission)
                            Next
                        Else
                            'Only update existing global content if we're ready to publish
                            'assign and update
                            GlobalContent.AllPages = ModuleVersion.AllPages
                            GlobalContent.ModuleDefId = ModuleVersion.ModuleDefId
                            GlobalContent.Title = ModuleVersion.Title
                            GlobalContent.SkinPaneId = ModuleVersion.SkinPaneId
                            GlobalContent.DisplayOrder = ModuleVersion.DisplayOrder
                            GlobalContent.StartDate = ModuleVersion.StartDate
                            GlobalContent.EndDate = ModuleVersion.EndDate
                            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Update(GlobalContent)

                            'add settings
                            For Each ModuleSettingsVersion As Meanstream.Portal.Core.Extensibility.Attribute In ModuleSettingsVersions
                                Dim ModuleSettings As Meanstream.Portal.Core.Extensibility.Attribute = New Meanstream.Portal.Core.Extensibility.Attribute
                                ModuleSettings.Key = ModuleSettingsVersion.Key
                                ModuleSettings.Value = ModuleSettingsVersion.Value
                                ModuleSettings.DataType = ModuleSettingsVersion.DataType
                                ModuleSettings.ComponentId = GlobalContent.Id
                                'copy attributes
                                Extensibility.AttributeService.Current.Create(ModuleSettings.ComponentId, ModuleSettings.Key, ModuleSettings.Value, ModuleSettings.DataType)
                                'ModuleVersion.Attributes.Add(ModuleSettings)
                                'Meanstream.Portal.Core.Data.DataRepository.ModuleSettingsProvider.Insert(ModuleSettings)
                            Next

                            '******** add shared copy method here **********
                            'widget.copy
                            '***********************************************
                        End If

                        'delete permissions
                        Dim ModulePermissionList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModulePermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Find("ModuleId=" & GlobalContent.Id.ToString)
                        For Each ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission In ModulePermissionList
                            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Delete(ModulePermission)
                        Next

                        Dim ModulePermissionVersionList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("ModuleId=" & ModuleVersion.Id.ToString)
                        For Each ModulePermissionVersion As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission In ModulePermissionVersionList
                            Dim ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = New Meanstream.Portal.Core.Entities.MeanstreamModulePermission
                            ModulePermission.ModuleId = GlobalContent.Id
                            ModulePermission.PermissionId = ModulePermissionVersion.PermissionId
                            ModulePermission.RoleId = ModulePermissionVersion.RoleId
                            ModulePermission.Id = Guid.NewGuid
                            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Insert(ModulePermission)
                        Next
                    Else

                        'Add new standard module
                        Dim Modules As Meanstream.Portal.Core.Entities.MeanstreamModule = New Meanstream.Portal.Core.Entities.MeanstreamModule
                        Modules.AllPages = ModuleVersion.AllPages
                        Modules.IsDeleted = ModuleVersion.IsDeleted
                        Modules.DisplayOrder = ModuleVersion.DisplayOrder
                        Modules.ModuleDefId = ModuleVersion.ModuleDefId
                        Modules.Title = ModuleVersion.Title
                        Modules.SkinPaneId = ModuleVersion.SkinPaneId
                        Modules.StartDate = ModuleVersion.StartDate
                        Modules.EndDate = ModuleVersion.EndDate
                        Modules.PageId = PageMain.Id
                        Modules.CreatedBy = ModuleVersion.CreatedBy
                        'Modules.sharedid = ModuleVersion.SharedId
                        Modules.Id = Guid.NewGuid
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Insert(Modules)

                        '**************************** do custom method **********************************
                        PortalTrace.WriteLine("Publish(): Start Custom Widget")
                        Try

                            Dim Widget1 As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = WidgetFramework.WidgetService.Current.GetWidgetVersionById(ModuleVersion.Id)
                            If Widget1 IsNot Nothing Then
                                Widget1.UserControl.OnPublish(Modules.Id)
                            End If
                        Catch ex As Exception
                            PortalTrace.Fail("Publish(): " & ex.Message, DisplayMethodInfo.FullSignature)
                        End Try
                        '**************************** end custom method **********************************
                        PortalTrace.WriteLine("Publish(): End Custom Widget")

                        'add settings
                        For Each ModuleSettingsVersion As Meanstream.Portal.Core.Extensibility.Attribute In ModuleSettingsVersions
                            Dim ModuleSettings As Meanstream.Portal.Core.Extensibility.Attribute = New Meanstream.Portal.Core.Extensibility.Attribute
                            ModuleSettings.Key = ModuleSettingsVersion.Key
                            ModuleSettings.Value = ModuleSettingsVersion.Value
                            ModuleSettings.DataType = ModuleSettingsVersion.DataType
                            ModuleSettings.ComponentId = Modules.Id

                            'copy attributes
                            Extensibility.AttributeService.Current.Create(ModuleSettings.ComponentId, ModuleSettings.Key, ModuleSettings.Value, ModuleSettings.DataType)
                            'ModuleVersion.Attributes.Add(ModuleSettings)
                            'Meanstream.Portal.Core.Data.DataRepository.ModuleSettingsProvider.Insert(ModuleSettings)
                        Next

                        PortalTrace.WriteLine("Publish(): End Widget Settings")

                        'Add Module Permissions
                        Dim ModulePermissionVersionList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("ModuleId=" & ModuleVersion.Id.ToString)
                        For Each ModulePermissionVersion As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission In ModulePermissionVersionList
                            Dim ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = New Meanstream.Portal.Core.Entities.MeanstreamModulePermission
                            ModulePermission.ModuleId = Modules.Id
                            ModulePermission.PermissionId = ModulePermissionVersion.PermissionId
                            ModulePermission.RoleId = ModulePermissionVersion.RoleId
                            ModulePermission.Id = Guid.NewGuid
                            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Insert(ModulePermission)
                        Next
                        PortalTrace.WriteLine("Publish(): End Widget Permissions")

                    End If

                Next

                'Add new PagePermissions
                For Each PagePermissionVersion As PageVersionPermission In _entity.Permissions
                    Dim PagePermission As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = New Meanstream.Portal.Core.Entities.MeanstreamPagePermission
                    PagePermission.PageId = PageMain.Id
                    PagePermission.PermissionId = PagePermissionVersion.Permission.Id
                    PagePermission.RoleId = PagePermissionVersion.Role.Id
                    PagePermission.Id = Guid.NewGuid
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Insert(PagePermission)
                Next

                PortalTrace.WriteLine("Publish(): End Page Permissions")

                'always create a workflow - uncomment workflow!
                'PageManager.CreateVersionWorkflow(PageVersionProperties.VersionId, NewPageVersion.Properties.VersionId, PageMain.TabId)

                'PortalTrace.WriteLine("Publish(): End CreateVersionWorkflow")

                'Handle Workflow Tasks - uncomment workflow!
                'Dim Workflows As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.Workflow) = Meanstream.Portal.Core.Data.DataRepository.WorkflowProvider.Find("WorkflowType=1 AND DocumentId=" & PageVersionProperties.VersionId)
                'For Each Workflow As Meanstream.Portal.Core.Entities.Workflow In Workflows
                '    Me.PublishWorkflow(Workflow)
                'Next

                'PortalTrace.WriteLine("Publish(): Handle Workflow Tasks")

                'uncomment workflow!
                'Me.PublishWorkflow()

                'Index page for search
                'If PageMain.Index Then
                '    ContentService.Current.Index(PageMain.Id)
                'End If

                PortalTrace.WriteLine("Publish(): End PublishWorkflow")

                Meanstream.Portal.Core.Utilities.CacheUtility.RefreshPages()
                Meanstream.Portal.Core.Utilities.CacheUtility.RefreshMenu()

                Meanstream.Portal.Core.PortalContext.ReloadPortalUrlList()

            Catch ex As Exception
                PortalTrace.Fail("Publish(): " & ex.Message, DisplayMethodInfo.FullSignature)
                Throw New Exception(ex.Message)
            End Try
        End Sub


        '''<summary>
        ''' Creates a new PageVersionBase from an existing version.
        ''' </summary>
        ''' <param name="ID">The VersionId of he page to be copied</param>
        Private Function CreateNewPageVersionBaseFromVersionID(ByVal Id As Guid) As Meanstream.Portal.Core.Content.PageVersion
            PortalTrace.WriteLine("CreateNewPageVersionBaseFromVersionID(): Id=" & Id.ToString)

            'Dim SessionManager As Meanstream.Portal.Core.SessionManager = Meanstream.Portal.Core.SessionManager.GetInstance()
            'Dim WidgetManager As Meanstream.Portal.Core.WidgetManager = Meanstream.Portal.Core.WidgetManager.GetInstance
            'Dim RoleManager As Meanstream.Portal.Core.RoleManager = Meanstream.Portal.Core.RoleManager.GetInstance

            'Get Updated Page Content
            Dim PageContent As PageVersion = New PageVersion(Id)
            Dim pageVersionManager As New PageVersionManager(PageContent)
            pageVersionManager.LoadFromDatasource()

            '**************************** Copy and Add Page Version ****************************
            Dim PageVersion As Meanstream.Portal.Core.Entities.MeanstreamPageVersion = New Meanstream.Portal.Core.Entities.MeanstreamPageVersion
            PageVersion.Title = PageContent.MetaTitle
            PageVersion.Name = PageContent.Name
            PageVersion.Author = PageContent.Author
            PageVersion.Description = PageContent.MetaDescription
            PageVersion.KeyWords = PageContent.MetaKeywords
            PageVersion.SkinId = PageContent.Skin.Id
            PageVersion.ParentId = PageContent.ParentId
            PageVersion.DisableLink = PageContent.DisableLink
            PageVersion.IsDeleted = PageContent.IsDeleted
            PageVersion.DisplayOrder = PageContent.DisplayOrder
            PageVersion.IsVisible = PageContent.IsVisible
            PageVersion.Url = PageContent.Url
            PageVersion.IsPublished = PageContent.IsPublished
            PageVersion.PageId = PageContent.PageId
            PageVersion.CreatedDate = Date.Now
            PageVersion.LastSavedDate = Date.Now
            PageVersion.Approved = False
            PageVersion.PortalId = PageContent.PortalId
            PageVersion.StartDate = PageContent.StartDate
            PageVersion.EndDate = PageContent.EndDate
            PageVersion.Type = PageContent.Type
            PageVersion.EnableCaching = PageContent.EnableCaching
            PageVersion.EnableViewState = PageContent.EnableViewState
            PageVersion.Index = PageContent.Index
            PageVersion.Id = Guid.NewGuid
            '**************************** end copy and add pageversion ****************************

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Insert(PageVersion) Then
                PageVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.GetById(PageVersion.Id)

                'If PageVersion.Id <> Nothing And PageVersion.VersionId > -1 Then
                If PageVersion.Id <> Nothing Then
                    '****************************  Enumerate the existing page permissions, copy and add permissions **************************** 
                    Dim PagePermissionList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" & PageContent.Id.ToString)
                    For Each PagePermissionVersion As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion In PagePermissionList
                        Dim NewPagePermissionVersion As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = New Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
                        NewPagePermissionVersion.PermissionId = PagePermissionVersion.PermissionId
                        NewPagePermissionVersion.RoleId = PagePermissionVersion.RoleId
                        NewPagePermissionVersion.VersionId = PageVersion.Id
                        NewPagePermissionVersion.Id = Guid.NewGuid
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Insert(NewPagePermissionVersion)
                    Next
                    '**************************** end permissions **************************** 

                    For Each Modules As WidgetFramework.WidgetVersion In PageContent.Widgets
                        Dim AddModule As Boolean = False
                        'If its html module then create new rows and copy text
                        'if exsisting is alltabs the market accordingly
                        Dim NewModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersion

                        'alltabs then use exsisting module
                        If Modules.AllPages Then
                            NewModule = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.GetById(Modules.Id)
                            'Do not add new module, use existing
                            AddModule = True
                        Else
                            NewModule.AllPages = Modules.AllPages
                            NewModule.SharedId = Nothing
                            NewModule.IsDeleted = Modules.IsDeleted
                            NewModule.ModuleDefId = Modules.ModuleDefId
                            NewModule.Title = Modules.Title
                            'NewModule.PortalId = Modules.PortalId
                            NewModule.PageVersionId = PageVersion.Id
                            NewModule.SharedId = Modules.SharedId
                            NewModule.DisplayOrder = Modules.DisplayOrder
                            NewModule.SkinPaneId = Modules.SkinPaneId
                            NewModule.EndDate = Modules.EndDate
                            NewModule.StartDate = Modules.StartDate
                            NewModule.CreatedBy = System.Web.HttpContext.Current.Profile.UserName
                            NewModule.LastModifiedBy = System.Web.HttpContext.Current.Profile.UserName
                            NewModule.LastModifiedDate = Date.Now
                            NewModule.Id = Guid.NewGuid
                            AddModule = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Insert(NewModule)
                        End If

                        If AddModule Then
                            PortalTrace.WriteLine("CreateNewPageVersionBaseFromVersionID: Start Custom")
                            '**************************** Do Custom *************************************
                            Try
                                Dim Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = WidgetFramework.WidgetService.Current.GetWidgetVersionById(Modules.Id)
                                If Widget IsNot Nothing Then
                                    Widget.UserControl.OnCreateVersionFromVersion(NewModule.Id)
                                End If
                            Catch ex As Exception
                                PortalTrace.WriteLine("CreateNewPageVersionBaseFromVersionID: " & ex.Message)
                            End Try
                            '**************************** End Custom *************************************
                            PortalTrace.WriteLine("CreateNewPageVersionBaseFromVersionID: End Custom")
                            '**************************** Copy and Add settings **************************** 
                            'Dim ModuleSettingsVersions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.ModuleSettingsVersion) = WidgetManager.GetWidgetVersionBaseByModuleId(Modules.ModuleId).Settings
                            For Each ModuleSetting As Meanstream.Portal.Core.Extensibility.Attribute In Modules.Attributes
                                Dim ModuleSettingsVersion As Meanstream.Portal.Core.Extensibility.Attribute = New Meanstream.Portal.Core.Extensibility.Attribute
                                ModuleSettingsVersion.Key = ModuleSetting.Key
                                ModuleSettingsVersion.Value = ModuleSetting.Value
                                ModuleSettingsVersion.DataType = ModuleSetting.DataType
                                ModuleSettingsVersion.ComponentId = NewModule.Id
                                'copy attributes
                                'ModuleVersion.Attributes.Add(ModuleSettings)
                                Extensibility.AttributeService.Current.Create(ModuleSettingsVersion.ComponentId, ModuleSettingsVersion.Key, ModuleSettingsVersion.Value, ModuleSettingsVersion.DataType)
                                'Meanstream.Portal.Core.Data.DataRepository.ModuleSettingsVersionProvider.Insert(ModuleSettingsVersion)
                            Next
                            '**************************** end settings **************************** 
                            PortalTrace.WriteLine("CreateNewPageVersionBaseFromVersionID: End Settings")
                            '**************************** only add module permissions if not shared ****************************
                            If Modules.AllPages = False Then
                                'Existing Page Module Permissions
                                Dim ModulePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("ModuleId=" & Modules.Id.ToString)
                                For Each ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission In ModulePermissions
                                    Dim NewModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                                    NewModulePermission.ModuleId = NewModule.Id
                                    NewModulePermission.PermissionId = ModulePermission.PermissionId
                                    NewModulePermission.RoleId = ModulePermission.RoleId
                                    NewModulePermission.PageVersionId = PageVersion.Id
                                    NewModulePermission.Id = Guid.NewGuid
                                    'Add new Permission
                                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(NewModulePermission)
                                Next
                            End If
                            ''**************************** end permissions ****************************
                            PortalTrace.WriteLine("CreateNewPageVersionBaseFromVersionID: End permissions")
                        End If
                    Next

                End If
            End If

            Dim result As New Meanstream.Portal.Core.Content.PageVersion(PageVersion.Id)
            pageVersionManager = New PageVersionManager(result)
            pageVersionManager.LoadFromDatasource()
            Return result
        End Function

        'Private Sub PublishWorkflow()
        '    Log.Debug("Meanstream.Portal.Core.PageVersionBase.PublishWorkflow()")

        '    Dim MessagingManager As Meanstream.Portal.Core.MessagingManager = Meanstream.Portal.Core.MessagingManager.GetInstance

        '    'Check if Workflow is enabled
        '    Dim IsWorkflow As Boolean = False

        '    Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")

        '    IsWorkflow = Configuration.AppSettings.Settings.Item("Meanstream.Workflow").Value

        '    Dim WorkflowUsers As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.GlobalWorkflowSettings) = Meanstream.Portal.Core.Data.DataRepository.GlobalWorkflowSettingsProvider.GetAll

        '    If IsWorkflow And WorkflowUsers.Count > 0 Then

        '        IsWorkflow = WorkflowUsers(0).Enabled

        '    Else

        '        IsWorkflow = False

        '    End If

        '    If IsWorkflow Then

        '        'send message that the Page has been published
        '        Dim Workflows As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.Workflow) = Meanstream.Portal.Core.Data.DataRepository.WorkflowProvider.Find("WorkflowType=1 AND DocumentId=" & Me.Properties.VersionId)

        '        If Workflows.Count > 0 Then

        '            Dim Subject As String = Me.Properties.TabName & " was Published"
        '            Dim Message As String = Me.Properties.TabName & " was Successfully Published. The Workflow titled " & Workflows(0).Title & "  has been completed.<br>"

        '            'send message to author
        '            Meanstream.Portal.Core.Message.SendMessage(Workflows(0).Author, "", Subject, Message, Net.Mail.MailPriority.Normal, Meanstream.Portal.Core.Message.Type.MESSAGE_TYPE_WORKFLOW, Workflows(0).Id, True)

        '            'send message to reviewer
        '            Meanstream.Portal.Core.Message.SendMessage(Workflows(0).Reviewer, "", Subject, Message, Net.Mail.MailPriority.Normal, Meanstream.Portal.Core.Message.Type.MESSAGE_TYPE_WORKFLOW, Workflows(0).Id, True)

        '            'send message to Approver
        '            Meanstream.Portal.Core.Message.SendMessage(Workflows(0).Approver, "", Subject, Message, Net.Mail.MailPriority.Normal, Meanstream.Portal.Core.Message.Type.MESSAGE_TYPE_WORKFLOW, Workflows(0).Id, True)

        '        End If
        '    End If

        'End Sub

        'Private Sub PublishWorkflow(ByVal Workflow As Meanstream.Portal.Core.Entities.Workflow)
        '    Log.Debug("Meanstream.Portal.Core.PageVersionBase.PublishWorkflow(): WorkflowId=" & Workflow.Id)
        '    Dim Tasks As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.WorkflowTask) = Meanstream.Portal.Core.Data.DataRepository.WorkflowTaskProvider.Find("WorkflowId=" & Workflow.Id)
        '    For Each Task As Meanstream.Portal.Core.Entities.WorkflowTask In Tasks
        '        If Task.Status <> "Completed" Then
        '            Task.Status = "Completed"
        '            Task.PercentCompleted = 100
        '            Meanstream.Portal.Core.Data.DataRepository.WorkflowTaskProvider.Update(Task)
        '        End If
        '    Next
        '    Workflow.Status = "Published"
        '    Meanstream.Portal.Core.Data.DataRepository.WorkflowProvider.Update(Workflow)
        'End Sub
    End Class
End Namespace

