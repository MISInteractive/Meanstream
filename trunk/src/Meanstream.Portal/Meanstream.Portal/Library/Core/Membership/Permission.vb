
Namespace Meanstream.Portal.Core.Membership
    Public Class Permission
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Membership.Permission)

        Public Enum PermissionType
            SYSTEM_PAGE_VIEW = 3
            SYSTEM_PAGE_EDIT = 4
            SYSTEM_MODULE_VIEW = 1
            SYSTEM_MODULE_EDIT = 2
        End Enum

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

        Private _code As String
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property

        Private _key As String
        Public Property Key() As String
            Get
                Return _key
            End Get
            Set(ByVal value As String)
                _key = value
            End Set
        End Property

        Private _value As String
        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Permission) As Boolean Implements System.IEquatable(Of Permission).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

