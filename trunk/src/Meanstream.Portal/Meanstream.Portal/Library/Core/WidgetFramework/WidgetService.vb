Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As WidgetService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As WidgetService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName
                            _privateServiceInstance = New WidgetService(machineName, appFriendlyName)
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

                Throw New InvalidOperationException(String.Format("The widget service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Widget Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Widget Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function GetWidgetById(ByVal id As Guid) As Widget
            Dim widget As New Widget(id)
            Dim manager As New WidgetManager(widget)
            manager.LoadFromDatasource()
            Return widget
        End Function

        Public Function GetWidgetsByPageId(ByVal pageId As Guid) As List(Of Widget)
            Dim widgets As List(Of Widget) = New List(Of Widget)

            Dim Query As Meanstream.Portal.Core.Data.MeanstreamModuleQuery = New Meanstream.Portal.Core.Data.MeanstreamModuleQuery
            Query.AppendEquals(Entities.MeanstreamModuleColumn.PageId, PageId.ToString)
            Query.AppendLessThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamModuleColumn.StartDate, Date.Now.ToString)
            Query.AppendGreaterThanOrEqual("AND", Entities.MeanstreamModuleColumn.EndDate, Date.Now.ToString)

            Dim Modules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModule) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Find(Query.Parameters)
            Modules.Sort("DisplayOrder")
            For Each PageModule In Modules
                widgets.Add(Me.GetWidgetById(PageModule.Id))
            Next

            Return widgets
        End Function

        Public Function GetWidgetVersionsByPageVersionId(ByVal pageVersionId As Guid) As List(Of WidgetVersion)
            PortalTrace.WriteLine("GetWidgetsByVersionId(): PageVersionId=" & pageVersionId.ToString)

            Dim Widgets As List(Of WidgetVersion) = New List(Of WidgetVersion)
            Dim PageModules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("IsDeleted=False AND PageVersionId=" & pageVersionId.ToString)
            PageModules.Sort("DisplayOrder")

            For Each PageModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In PageModules
                Dim widget As New WidgetVersion(PageModule.Id)
                Dim manager As New WidgetVersionManager(widget)
                manager.Bind(PageModule)
                Widgets.Add(widget)
            Next

            Return Widgets
        End Function

        Public Function GetPermissions(ByVal widgetId As Guid) As List(Of WidgetPermission)
            Dim permissions As List(Of WidgetPermission) = New List(Of WidgetPermission)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModulePermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Find("ModuleId=" & widgetId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamModulePermission In entities
                Dim permission As WidgetPermission = New WidgetPermission(entity.Id)
                Dim manager As New WidgetPermissionManager(permission)
                manager.Bind(entity)
                permissions.Add(permission)
            Next
            Return permissions
        End Function

        Public Function GetVersionPermissions(ByVal widgetVersionId As Guid) As List(Of WidgetVersionPermission)
            Dim permissions As List(Of WidgetVersionPermission) = New List(Of WidgetVersionPermission)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("ModuleId=" & widgetVersionId.ToString)
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission In entities
                Dim permission As WidgetVersionPermission = New WidgetVersionPermission(entity.Id)
                Dim manager As New WidgetVersionPermissionManager(permission)
                manager.Bind(entity)
                permissions.Add(permission)
            Next
            Return permissions
        End Function

        Public Sub DeleteAllFromRecycleBin()
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("IsDeleted=True")
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In entities
                Dim version As New WidgetVersion(entity.Id)
                Dim manager As New WidgetVersionManager(version)
                manager.Bind(entity)
                manager.Delete()
            Next
        End Sub

        Public Function GetRecycleBin() As List(Of WidgetVersion)
            Dim list As New List(Of WidgetVersion)
            Dim entities As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("IsDeleted=True")
            For Each entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In entities
                Dim version As New WidgetVersion(entity.Id)
                Dim manager As New WidgetVersionManager(version)
                manager.Bind(entity)
                list.Add(version)
            Next
            Return list
        End Function

        '**************** REFACTOR *****************
        Public Function GetWidgetType(ByVal moduleDefId As Guid) As String
            Dim ModuleDefinitions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions) = Me.GetModuleDefinitions
            Return ModuleDefinitions.Find("Id", moduleDefId).FriendlyName
        End Function

        Public Function GetWidgetDefinitions() As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions)
            Dim ModuleDefinitions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions) = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.MODULEDEFINITIONS)
            If ModuleDefinitions Is Nothing Then
                ModuleDefinitions = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.Find("Enabled=True")
                Meanstream.Portal.Core.Utilities.CacheUtility.Add(Meanstream.Portal.Core.Utilities.CacheUtility.MODULEDEFINITIONS, ModuleDefinitions)
            End If
            Return ModuleDefinitions
        End Function

        Public Function GetModuleDefinitions() As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions)
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.GetAll()
        End Function

        Public Sub UpdateWidgetDefinition(ByVal moduleDefinition As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions)
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.MODULEDEFINITIONS)
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.Update(moduleDefinition)
        End Sub

        Private Function GetWidgetDefinitionByModuleDefId(ByVal moduleDefId As Guid) As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.GetById(moduleDefId)
        End Function

        Private Function GetModuleByID(ByVal Id As Guid) As Meanstream.Portal.Core.Entities.MeanstreamModule
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.GetById(Id)
        End Function
        '**************** END REFACTOR *****************

        Public Function GetWidgetVersionById(ByVal id As Guid) As WidgetVersion
            PortalTrace.WriteLine([String].Concat("GetWidgetVersionById() moduleId=" & id.ToString))
            Dim version As New WidgetVersion(id)
            Dim manager As New WidgetVersionManager(version)
            manager.LoadFromDatasource()
            Return version
        End Function

        Public Sub AddWidgetToPage(ByVal pageVersionId As Guid, ByVal skinPaneId As Guid, ByVal moduleDefId As Guid)
            PortalTrace.WriteLine([String].Concat("AddWidgetToPage() pageVersionId=" & pageVersionId.ToString, ", skinPaneId=" & skinPaneId.ToString, ", moduleDefId=" & moduleDefId.ToString))

            Dim WidgetList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("SkinPaneId=" & skinPaneId.ToString & " AND PageVersionId=" & pageVersionId.ToString)

            Dim Modules As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersion
            Modules.AllPages = False
            Modules.IsDeleted = False
            Modules.ModuleDefId = moduleDefId
            Modules.Title = Me.GetWidgetDefinitionByModuleDefId(moduleDefId).FriendlyName
            Modules.PageVersionId = pageVersionId
            Modules.SkinPaneId = skinPaneId
            Modules.DisplayOrder = WidgetList.Count + 1
            'Modules.SharedId =
            Modules.CreatedBy = System.Web.HttpContext.Current.Profile.UserName
            Modules.StartDate = CType("1/1/1753 12:00:00 AM", Date)
            Modules.EndDate = CType("12/31/9999 11:59:00 PM", Date)
            Modules.LastModifiedDate = Date.Now
            Modules.LastModifiedBy = System.Web.HttpContext.Current.Profile.UserName
            Modules.Id = Guid.NewGuid

            Modules.LastModifiedDate = Date.Now
            Modules.LastModifiedBy = System.Web.HttpContext.Current.Profile.UserName
            Modules.DeletedDate = ""

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Insert(Modules) Then
                'Call OnAddToPage in IModule
                Try
                    Dim Widget As WidgetVersion = Me.GetWidgetVersionById(Modules.Id)
                    If Widget IsNot Nothing Then
                        Widget.UserControl.OnAddToPage()
                    Else
                        PortalTrace.Warning("AddWidgetToPage() Widget Is Nothing", DisplayMethodInfo.DoNotDisplay)
                    End If
                Catch ex As Exception
                    PortalTrace.Fail("AddWidgetToPage() " & ex.Message, DisplayMethodInfo.DoNotDisplay)
                End Try

                Me.AddPageModule(Modules)
            End If
        End Sub

        Private Function AddPageModule(ByVal modules As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) As Boolean
            PortalTrace.WriteLine("AddPageModule(): VersionId=" & Modules.PageVersionId.ToString)

            If modules.AllPages Then
                Dim SharedModule As Meanstream.Portal.Core.Entities.MeanstreamModule = Me.GetModuleByID(modules.SharedId)
                Me.CopyModulePermissionsToVersion(SharedModule, modules)
            End If

            If modules.AllPages = False Then
                'add permissions for admin and host edit only

                Dim Administrator As Membership.Role = Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ADMINISTRATOR)
                Dim ModulePermissionView As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                'get page id
                ModulePermissionView.ModuleId = modules.Id
                ModulePermissionView.RoleId = Administrator.Id
                ModulePermissionView.PageVersionId = modules.PageVersionId
                'view permission
                ModulePermissionView.PermissionId = Membership.MembershipService.Current.GetPermission(Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id
                ModulePermissionView.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionView)
                'edit permission
                ModulePermissionView.PermissionId = Membership.MembershipService.Current.GetPermission(Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id
                ModulePermissionView.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionView)
                ''''''''''''''''''''''''''''''''''
                'Host
                Dim Host As Membership.Role = Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.HOST)
                Dim ModulePermissionViewHost As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                'get page id
                ModulePermissionViewHost.ModuleId = modules.Id
                ModulePermissionViewHost.RoleId = Host.Id
                ModulePermissionViewHost.PageVersionId = modules.PageVersionId
                'view permission
                ModulePermissionViewHost.PermissionId = Membership.MembershipService.Current.GetPermission(Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id
                ModulePermissionViewHost.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionViewHost)
                'edit permission
                ModulePermissionViewHost.PermissionId = Membership.MembershipService.Current.GetPermission(Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id
                ModulePermissionViewHost.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionViewHost)

                'AllUsers default view
                Dim AllUsers As Membership.Role = Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS)
                Dim ModulePermissionViewAllUsers As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                'get page id
                ModulePermissionViewAllUsers.ModuleId = modules.Id
                ModulePermissionViewAllUsers.RoleId = AllUsers.Id
                ModulePermissionViewAllUsers.PageVersionId = modules.PageVersionId
                'view permission
                ModulePermissionViewAllUsers.PermissionId = Membership.MembershipService.Current.GetPermission(Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id
                ModulePermissionViewAllUsers.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionViewAllUsers)
            End If

            Return True
        End Function

        Private Function CopyModulePermissionsToVersion(ByVal moduleToCopy As Meanstream.Portal.Core.Entities.MeanstreamModule, ByVal moduleToSave As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) As Boolean
            Dim ModulePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModulePermission) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Find("ModuleId=" & ModuleToCopy.Id.ToString)
            For Each ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission In ModulePermissions
                Dim ModulePermissionVersion As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                ModulePermissionVersion.ModuleId = ModuleToSave.Id
                ModulePermissionVersion.PermissionId = ModulePermission.PermissionId
                ModulePermissionVersion.RoleId = ModulePermission.RoleId
                ModulePermissionVersion.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(ModulePermissionVersion)
            Next
            Return True
        End Function

        Public Sub AddSharedWidgetToPage(ByVal pageVersionId As Guid, ByVal skinPaneId As Guid, ByVal sharedWidgetId As Guid)
            PortalTrace.WriteLine("AddSharedWidgetToPage(): pageVersionId=" & pageVersionId.ToString)
            'Get Global Module
            Dim SharedModule As Meanstream.Portal.Core.Entities.MeanstreamModule = Me.GetModuleByID(sharedWidgetId)
            'Add a version for now. Then, when published, it will carry the changes over to the global content...
            Dim Modules As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = New Meanstream.Portal.Core.Entities.MeanstreamModuleVersion
            Modules.AllPages = SharedModule.AllPages
            Modules.IsDeleted = SharedModule.IsDeleted
            Modules.SharedId = SharedModule.Id
            Modules.ModuleDefId = SharedModule.ModuleDefId
            Modules.Title = SharedModule.Title
            Modules.StartDate = SharedModule.StartDate
            Modules.EndDate = SharedModule.EndDate
            Modules.PageVersionId = pageVersionId
            Modules.DisplayOrder = 0
            Modules.CreatedBy = System.Web.HttpContext.Current.Profile.UserName
            Modules.Id = Guid.NewGuid
            Modules.LastModifiedDate = Date.Now
            Modules.LastModifiedBy = System.Web.HttpContext.Current.Profile.UserName
            Modules.DeletedDate = ""

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Insert(Modules) Then
                Me.AddPageModule(Modules)
            End If
        End Sub

        Public Sub Create(ByVal name As String, ByVal description As String, ByVal widgetVirtualPath As String, ByVal widgetEditVirtualPath As String, ByVal isDefault As Boolean, ByVal enabled As Boolean)
            Dim ModuleDefId As Guid = Guid.NewGuid

            If Not System.IO.File.Exists(System.Web.HttpContext.Current.Request.MapPath(widgetVirtualPath)) Then
                Throw New System.IO.FileNotFoundException("File Not Found for widgetVirtualPath")
            End If

            If Not System.IO.File.Exists(System.Web.HttpContext.Current.Request.MapPath(widgetEditVirtualPath)) Then
                Throw New System.IO.FileNotFoundException("File Not Found for widgetEditVirtualPath")
            End If

            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.MODULEDEFINITIONS)

            Dim ModuleDefinitions As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions = New Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions
            ModuleDefinitions.Id = ModuleDefId
            ModuleDefinitions.FriendlyName = name
            ModuleDefinitions.IsDefault = isDefault
            ModuleDefinitions.Enabled = enabled

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.Insert(ModuleDefinitions) Then
                ModuleDefId = ModuleDefinitions.Id

                Dim ModuleControlsEdit As Meanstream.Portal.Core.Entities.MeanstreamModuleControls = New Meanstream.Portal.Core.Entities.MeanstreamModuleControls
                ModuleControlsEdit.ControlKey = "Version"
                ModuleControlsEdit.ControlPath = widgetEditVirtualPath
                ModuleControlsEdit.ControlTitle = name
                ModuleControlsEdit.ModuleDefId = ModuleDefinitions.Id
                ModuleControlsEdit.Id = Guid.NewGuid

                If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleControlsProvider.Insert(ModuleControlsEdit) Then
                    Dim ModuleControls As Meanstream.Portal.Core.Entities.MeanstreamModuleControls = New Meanstream.Portal.Core.Entities.MeanstreamModuleControls
                    ModuleControls.ControlKey = "Published"
                    ModuleControls.ControlPath = widgetVirtualPath
                    ModuleControls.ControlTitle = name
                    ModuleControls.ModuleDefId = ModuleDefinitions.Id
                    ModuleControls.Id = Guid.NewGuid

                    If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleControlsProvider.Insert(ModuleControls) Then

                    Else
                        Throw New ApplicationException("There was an issue creating ModuleControlsEdit")
                    End If

                Else
                    Throw New ApplicationException("There was an issue creating ModuleControlsEdit")
                End If

            Else
                Throw New ApplicationException("There was an issue creating ModuleDefinition")
            End If

            'update default widget
            If isDefault Then
                Dim ModuleDefault As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.GetAll
                For Each ModuleDefinition As Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions In ModuleDefault
                    If ModuleDefinition.IsDefault And ModuleDefinition.Id <> ModuleDefId Then
                        ModuleDefinition.IsDefault = False
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleDefinitionsProvider.Update(ModuleDefinition)
                    End If
                Next
            End If
        End Sub

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

