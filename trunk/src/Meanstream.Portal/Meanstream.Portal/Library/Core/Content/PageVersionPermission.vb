
Namespace Meanstream.Portal.Core.Content
    Public Class PageVersionPermission
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Content.PageVersionPermission)

        Sub New(ByVal id As Guid)
            MyBase.New(id)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _versionId As Guid
        Public Property VersionId() As Guid
            Get
                Return _versionId
            End Get
            Set(ByVal value As Guid)
                _versionId = value
            End Set
        End Property

        Private _permission As Core.Membership.Permission
        Public Property Permission() As Core.Membership.Permission
            Get
                Return _permission
            End Get
            Set(ByVal value As Core.Membership.Permission)
                _permission = value
            End Set
        End Property

        Private _role As Core.Membership.Role
        Public Property Role() As Core.Membership.Role
            Get
                Return _role
            End Get
            Set(ByVal value As Core.Membership.Role)
                _role = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As PageVersionPermission) As Boolean Implements System.IEquatable(Of PageVersionPermission).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

