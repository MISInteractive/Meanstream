
Namespace Meanstream.Portal.Core.Content
    Public Class PagePermission
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Content.PagePermission)

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

        Private _pageId As Guid
        Public Property PageId() As Guid
            Get
                Return _pageId
            End Get
            Set(ByVal value As Guid)
                _pageId = value
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

        Public Overloads Function Equals(ByVal other As PagePermission) As Boolean Implements System.IEquatable(Of PagePermission).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

