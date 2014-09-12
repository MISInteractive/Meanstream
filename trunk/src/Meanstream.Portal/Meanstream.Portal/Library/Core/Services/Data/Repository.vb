
Namespace Meanstream.Portal.Core.Services.Data

    Public Enum Status
        Active
        Locked
        Deleted
    End Enum

    Public Class Repository
        Implements IEntity
        Implements IEquatable(Of Repository)


        Public Sub New()

        End Sub

#Region "Properties"
        Private _id As Guid
        Public Property Id As System.Guid Implements IEntity.Id
            Get
                Return _id
            End Get
            Set(ByVal value As Guid)
                _id = value
            End Set
        End Property

        Private _name As String = ""
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _description As String = ""
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _tags As String = ""
        Public Property Tags As String
            Get
                Return _tags
            End Get
            Set(ByVal value As String)
                _tags = value
            End Set
        End Property

        Private _status As Status = Status.Active
        Public Property Status As Status
            Get
                Return _status
            End Get
            Set(ByVal value As Status)
                _status = value
            End Set
        End Property

        Protected Friend _createdDate As Date
        Public ReadOnly Property CreatedDate As Date
            Get
                Return _createdDate
            End Get
        End Property

        Protected Friend _lastModifiedDate As Date
        Public ReadOnly Property lastModifiedDate As Date
            Get
                Return _lastModifiedDate
            End Get
        End Property

        Protected Friend _createdBy As String = ""
        Public ReadOnly Property CreatedBy As String
            Get
                Return _createdBy
            End Get
        End Property

        Protected Friend _lastModifiedBy As String = ""
        Public ReadOnly Property LastModifiedBy As String
            Get
                Return _lastModifiedBy
            End Get
        End Property

        Private _fields As New List(Of Field)
        Public Property Fields As List(Of Field)
            Get
                Return _fields
            End Get
            Set(ByVal value As List(Of Field))
                _fields = value
            End Set
        End Property
#End Region

        Public Sub Fill(ByVal reader As System.Data.IDataReader) Implements IEntity.Fill

            _id = reader.Item("id")
            _description = reader.Item("description")
            _name = reader.Item("name")
            _tags = reader.Item("tags")
            _createdDate = reader.Item("createdDate")
            _lastModifiedDate = reader.Item("lastModifiedDate")
            _createdBy = reader.Item("createdBy")
            _lastModifiedBy = reader.Item("lastModifiedBy")
            _status = reader.Item("status")

            Dim serializer As New Serializer(Of List(Of Field))
            Dim serializedFields As String = reader.Item("fields")
            _fields = serializer.Deserialize(serializedFields)

        End Sub

        Public Overloads Function Equals(ByVal other As Repository) As Boolean Implements System.IEquatable(Of Repository).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace

