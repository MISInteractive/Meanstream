Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.Membership
    Public Class PermissionManager
        Inherits AttributeEntityManager

        Private _entity As Permission

        Sub New(ByRef attributeEntity As Permission)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("permission id cannot be null.")
            End If
        End Sub

        Protected Friend Sub LoadFromDatasource()
            Dim permission As Meanstream.Portal.Core.Entities.MeanstreamPermission = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPermissionProvider.GetById(_entity.Id)
            If permission Is Nothing Then
                Throw New InvalidOperationException(String.Format("the permission {0} cannot be located in database.", _entity.Id))
            End If
            Me.Bind(permission)
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamPermission)
            _entity.Code = entity.PermissionCode
            _entity.Key = entity.PermissionKey
            _entity.Value = entity.PermissionName
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPermissionProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()
            Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamPermission
            entity.Id = _entity.Id
            entity.PermissionCode = _entity.Code
            entity.PermissionKey = _entity.Key
            entity.PermissionName = _entity.Value
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPermissionProvider.Update(entity)
        End Sub
    End Class
End Namespace
