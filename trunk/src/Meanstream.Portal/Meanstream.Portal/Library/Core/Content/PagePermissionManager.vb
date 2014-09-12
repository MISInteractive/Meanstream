Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.Content
    Friend Class PagePermissionManager
        Inherits AttributeEntityManager

        Private _entity As PagePermission

        Sub New(ByRef attributeEntity As PagePermission)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("page permission id cannot be null.")
            End If
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermission)
            _entity.PageId = entity.PageId
            Dim permission As New Meanstream.Portal.Core.Membership.Permission(entity.PermissionId)
            Dim manager As New Membership.PermissionManager(permission)
            manager.LoadFromDatasource()
            _entity.Permission = permission
            _entity.Role = New Meanstream.Portal.Core.Membership.Role(entity.RoleId)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'custom code here
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.GetById(_entity.Id)

            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page permission {0} cannot be located in database.", _entity.Id))
            End If

            entity.PageId = _entity.PageId
            entity.PermissionId = _entity.Permission.Id
            entity.RoleId = _entity.Role.Id
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Update(entity)

            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS)
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONSVERSION)
        End Sub

    End Class
End Namespace

