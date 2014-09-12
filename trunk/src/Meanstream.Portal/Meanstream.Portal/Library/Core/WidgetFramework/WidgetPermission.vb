
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetPermission
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of WidgetPermission)

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

        Public Overloads Function Equals(ByVal other As WidgetPermission) As Boolean Implements System.IEquatable(Of WidgetPermission).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

