
Namespace Meanstream.Portal.CRM
    Public Class List
        Implements IEquatable(Of List)

        Sub New(ByVal id As Guid)
            _id = id
        End Sub

#Region " Properties "
        Private Shared _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private Shared _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private Shared _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private Shared _lastModifiedDate As DateTime
        Public Property LastModifiedDate() As DateTime
            Get
                Return _lastModifiedDate
            End Get
            Set(ByVal value As DateTime)
                _lastModifiedDate = value
            End Set
        End Property

        Private Shared _createdDate As DateTime
        Public Property CreatedDate() As DateTime
            Get
                Return _createdDate
            End Get
            Set(ByVal value As DateTime)
                _createdDate = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As List) As Boolean Implements System.IEquatable(Of List).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

