
Namespace Meanstream.Portal.Core.Content
    Public Class PageVersionPermissionManager
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntityManager

        Private _entity As PageVersionPermission

        Sub New(ByRef attributeEntity As PageVersionPermission)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("page version permission id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page permission version {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion)
            _entity.VersionId = entity.VersionId
            Dim permission As New Meanstream.Portal.Core.Membership.Permission(entity.PermissionId)
            Dim manager As New Membership.PermissionManager(permission)
            manager.LoadFromDatasource()
            _entity.Permission = permission
            _entity.Role = New Meanstream.Portal.Core.Membership.Role(entity.RoleId)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'custom code here
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.GetById(_entity.Id)

            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page version permission {0} cannot be located in database.", _entity.Id))
            End If

            entity.VersionId = _entity.VersionId
            entity.PermissionId = _entity.Permission.Id
            entity.RoleId = _entity.Role.Id
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Update(entity)

            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONSVERSION)
        End Sub

        Public Shared Sub SavePermissions(ByVal list As List(Of PageVersionPermission))
            If list.Count > 0 Then
                Dim existing As List(Of PageVersionPermission) = ContentService.Current.GetPageVersionPermissions(list(0).VersionId)
                'delete if it doesn't exist in new list
                For Each permission As PageVersionPermission In existing
                    If Not list.Contains(permission) Then
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Delete(permission.Id)
                    End If
                Next
                'add if it doesn't exist in old list
                For Each permission As PageVersionPermission In list
                    Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamPagePermissionVersion
                    entity.VersionId = permission.VersionId
                    entity.PermissionId = permission.Permission.Id
                    entity.RoleId = permission.Role.Id
                    entity.Id = permission.Id
                    If Not existing.Contains(permission) Then
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Insert(entity)
                    Else
                        Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Update(entity)
                    End If
                Next
            End If
        End Sub

        Public Shared Sub DeletePermissions(ByVal pageVersionId As Guid)
            Dim permissions As List(Of PageVersionPermission) = ContentService.Current.GetPageVersionPermissions(pageVersionId)
            For Each permission As PageVersionPermission In permissions
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Delete(permission.Id)
            Next
        End Sub
    End Class
End Namespace

