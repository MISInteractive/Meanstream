Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.WidgetFramework
    Friend Class WidgetVersionPermissionManager
        Inherits AttributeEntityManager

        Private _entity As WidgetVersionPermission

        Sub New(ByRef attributeEntity As WidgetVersionPermission)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("widget permission id cannot be null.")
            End If
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission)
            _entity.WidgetId = entity.ModuleId
            _entity.PageVersionId = entity.PageVersionId
            Dim permission As New Meanstream.Portal.Core.Membership.Permission(entity.PermissionId)
            Dim manager As New Membership.PermissionManager(permission)
            manager.LoadFromDatasource()
            _entity.Permission = permission
            _entity.Role = New Meanstream.Portal.Core.Membership.Role(entity.RoleId)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'custom code here
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.GetById(_entity.Id)

            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the widget version permission {0} cannot be located in database.", _entity.Id))
            End If

            entity.ModuleId = _entity.WidgetId
            entity.PermissionId = _entity.Permission.Id
            entity.RoleId = _entity.Role.Id
            entity.PageVersionId = _entity.PageVersionId
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Update(entity)
        End Sub

        Public Shared Sub SavePermissions(ByVal list As List(Of WidgetVersionPermission))
            If list.Count > 0 Then
                Dim existing As List(Of WidgetVersionPermission) = WidgetService.Current.GetVersionPermissions(list(0).WidgetId)
                'delete if it doesn't exist in new list
                For Each permission As WidgetVersionPermission In existing
                    If Not list.Contains(permission) Then
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Delete(permission.Id)
                    End If
                Next
                'add if it doesn't exist in old list
                For Each permission As WidgetVersionPermission In list
                    Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamModuleVersionPermission
                    entity.ModuleId = permission.WidgetId
                    entity.PermissionId = permission.Permission.Id
                    entity.RoleId = permission.Role.Id
                    entity.PageVersionId = permission.PageVersionId
                    entity.Id = permission.Id
                    If Not existing.Contains(permission) Then
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Insert(entity)
                    Else
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Update(entity)
                    End If
                Next
            End If
        End Sub

        Public Shared Sub DeletePermissions(ByVal widgetVersionId As Guid)
            Dim permissions As List(Of WidgetVersionPermission) = WidgetService.Current.GetVersionPermissions(widgetVersionId)
            For Each permission As WidgetVersionPermission In permissions
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Delete(permission.Id)
            Next
        End Sub

    End Class
End Namespace

