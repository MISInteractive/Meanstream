
Namespace Meanstream.Portal.CRM
    Public Class Contact
        Inherits Participant
        Implements IEquatable(Of Contact)

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

        Private _address As String = ""
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        Private _address2 As String = ""
        Public Property Address2() As String
            Get
                Return _address2
            End Get
            Set(ByVal value As String)
                _address2 = value
            End Set
        End Property

        Private _city As String = ""
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property

        Private _email As String = ""
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        Private _firstName As String = ""
        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property

        Private _lastName As String = ""
        Public Property LastName() As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property

        Private _flag As String = ""
        Public Property Flag() As String
            Get
                Return _flag
            End Get
            Set(ByVal value As String)
                _flag = value
            End Set
        End Property

        Private _mobile As String = ""
        Public Property Mobile() As String
            Get
                Return _mobile
            End Get
            Set(ByVal value As String)
                _mobile = value
            End Set
        End Property

        Private _notes As String = ""
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property

        Private _phone As String = ""
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property

        Private _state As String = ""
        Public Property State() As String
            Get
                Return _state
            End Get
            Set(ByVal value As String)
                _state = value
            End Set
        End Property

        Private _zip As String = ""
        Public Property Zip() As String
            Get
                Return _zip
            End Get
            Set(ByVal value As String)
                _zip = value
            End Set
        End Property

        Private _birthDate As String = ""
        Public Property BirthDate() As String
            Get
                Return _birthDate
            End Get
            Set(ByVal value As String)
                _birthDate = value
            End Set
        End Property

        Private _interest As String = ""
        Public Property Interest() As String
            Get
                Return _interest
            End Get
            Set(ByVal value As String)
                _interest = value
            End Set
        End Property

        Private _nickname As String = ""
        Public Property Nickname() As String
            Get
                Return _nickname
            End Get
            Set(ByVal value As String)
                _nickname = value
            End Set
        End Property

        Private _spouse As String = ""
        Public Property Spouse() As String
            Get
                Return _spouse
            End Get
            Set(ByVal value As String)
                _spouse = value
            End Set
        End Property

        Private _website As String = ""
        Public Property Website() As String
            Get
                Return _website
            End Get
            Set(ByVal value As String)
                _website = value
            End Set
        End Property

        Private _lastModifiedBy As Guid
        Public Property LastModifiedBy() As Guid
            Get
                Return _lastModifiedBy
            End Get
            Set(ByVal value As Guid)
                _lastModifiedBy = value
            End Set
        End Property

        Private _lastModifiedDate As DateTime
        Public Property LastModifiedDate() As DateTime
            Get
                Return _lastModifiedDate
            End Get
            Set(ByVal value As DateTime)
                _lastModifiedDate = value
            End Set
        End Property

        Private _createdDate As DateTime
        Public Property CreatedDate() As DateTime
            Get
                Return _createdDate
            End Get
            Set(ByVal value As DateTime)
                _createdDate = value
            End Set
        End Property

        Private _source As String = ""
        Public Property Source() As String
            Get
                Return _source
            End Get
            Set(ByVal value As String)
                _source = value
            End Set
        End Property

        Private _sourceName As String = ""
        Public Property SourceName() As String
            Get
                Return _sourceName
            End Get
            Set(ByVal value As String)
                _sourceName = value
            End Set
        End Property

        Private _sourceNotes As String = ""
        Public Property SourceNotes() As String
            Get
                Return _sourceNotes
            End Get
            Set(ByVal value As String)
                _sourceNotes = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Contact) As Boolean Implements System.IEquatable(Of Contact).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

