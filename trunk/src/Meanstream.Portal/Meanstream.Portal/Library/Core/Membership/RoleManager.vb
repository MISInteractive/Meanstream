Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.Membership
    Public Class RoleManager
        Inherits AttributeEntityManager

        Private _entity As Role

        Sub New(ByRef attributeEntity As Meanstream.Portal.Core.Membership.Role)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("role id cannot be null.")
            End If
        End Sub

        Public Sub LoadFromDatasource()
            Dim role As Meanstream.Portal.Core.Entities.MeanstreamRoles = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.GetById(_entity.Id)
            If role Is Nothing Then
                Throw New InvalidOperationException(String.Format("the role {0} cannot be located in database.", _entity.Id))
            End If
            Me.Bind(role)
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamRoles)
            _entity.Name = entity.RoleName
            _entity.Description = entity.Description
            _entity.IsPublic = entity.IsPublic
            _entity.AutoAssignment = entity.AutoAssignment
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()

            'DO NOT DELETE THESE 2 GROUPS
            If _entity.Name = Meanstream.Portal.Core.AppConstants.ADMINISTRATOR Or _entity.Name = Meanstream.Portal.Core.AppConstants.ALLUSERS Then
                Throw New InvalidOperationException(String.Format("the role {0} cannot be deleted.", _entity.Name))
            End If

            Dim RoleList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamRoles) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Find("RoleName=" & _entity.Name)
            For Each Role As Meanstream.Portal.Core.Entities.MeanstreamRoles In RoleList
                If System.Web.Security.Roles.GetUsersInRole(_entity.Name).Length > 0 Then
                    'Bulk removal of all users in role
                    System.Web.Security.Roles.RemoveUsersFromRole(System.Web.Security.Roles.GetUsersInRole(_entity.Name), _entity.Name)
                End If
                If Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Delete(Role.Id) Then
                    System.Web.Security.Roles.DeleteRole(Role.RoleName)
                End If
            Next
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim role As Meanstream.Portal.Core.Entities.MeanstreamRoles = Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.GetById(_entity.Id)
            If role Is Nothing Then
                Throw New InvalidOperationException(String.Format("the role {0} cannot be located in database.", _entity.Id))
            End If

            role.IsPublic = _entity.IsPublic
            'role.RoleName = _entity.Name
            role.AutoAssignment = _entity.AutoAssignment
            role.Description = _entity.Description
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamRolesProvider.Update(role)
        End Sub

        Public Sub AddUserToRole(ByVal username As String, ByVal notify As Boolean)
            MembershipService.Current.AddUserToRole(username, _entity.Name, notify)
        End Sub

        Public Sub RemoveUserFromRole(ByVal username As String)
            MembershipService.Current.RemoveUserFromRole(username, _entity.Name)
        End Sub
    End Class
End Namespace

