
Namespace Meanstream.Portal.Core.Membership
    Public Class Role
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Membership.Role)
        
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

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _isPublic As Boolean
        Public Property IsPublic() As Boolean
            Get
                Return _isPublic
            End Get
            Set(ByVal value As Boolean)
                _isPublic = value
            End Set
        End Property

        Private _autoAssignment As Boolean
        Public Property AutoAssignment() As Boolean
            Get
                Return _autoAssignment
            End Get
            Set(ByVal value As Boolean)
                _autoAssignment = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Meanstream.Portal.Core.Membership.Role) As Boolean Implements System.IEquatable(Of Meanstream.Portal.Core.Membership.Role).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

