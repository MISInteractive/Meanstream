
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetVersionPermission
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of WidgetVersionPermission)

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

        Private _widgetId As Guid
        Public Property WidgetId() As Guid
            Get
                Return _widgetId
            End Get
            Set(ByVal value As Guid)
                _widgetId = value
            End Set
        End Property

        Private _pageVersionId As Guid
        Public Property PageVersionId() As Guid
            Get
                Return _pageVersionId
            End Get
            Set(ByVal value As Guid)
                _pageVersionId = value
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

        Public Overloads Function Equals(ByVal other As WidgetVersionPermission) As Boolean Implements System.IEquatable(Of WidgetVersionPermission).Equals
            If Me.Permission.Id = other.Permission.Id And Me.Role.Id = other.Role.Id Or Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

