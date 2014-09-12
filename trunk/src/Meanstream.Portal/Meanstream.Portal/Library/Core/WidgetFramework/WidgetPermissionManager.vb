Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.WidgetFramework
    Friend Class WidgetPermissionManager
        Inherits AttributeEntityManager

        Private _entity As WidgetPermission

        Sub New(ByRef attributeEntity As WidgetPermission)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("widget permission id cannot be null.")
            End If
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamModulePermission)
            _entity.WidgetId = entity.ModuleId
            Dim permission As New Meanstream.Portal.Core.Membership.Permission(entity.PermissionId)
            Dim manager As New Membership.PermissionManager(permission)
            manager.LoadFromDatasource()
            _entity.Permission = permission
            _entity.Role = New Meanstream.Portal.Core.Membership.Role(entity.RoleId)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'custom code here
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.GetById(_entity.Id)

            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the widget permission {0} cannot be located in database.", _entity.Id))
            End If

            entity.ModuleId = _entity.WidgetId
            entity.PermissionId = _entity.Permission.Id
            entity.RoleId = _entity.Role.Id
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Update(entity)
        End Sub
    End Class
End Namespace

