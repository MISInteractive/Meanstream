
Namespace Meanstream.Portal.Subscription
    Public Class Billing
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Subscription.Billing)

        Public Enum PaymentMethod
            CreditCard
            Cash
            Check
            BillMeLater
            Paypal
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

        Private _subscriberId As Guid
        Public Property SubscriberId() As Guid
            Get
                Return _subscriberId
            End Get
            Set(ByVal value As Guid)
                _subscriberId = value
            End Set
        End Property

        Private _method As PaymentMethod
        Public Property Method() As PaymentMethod
            Get
                Return _method
            End Get
            Set(ByVal value As PaymentMethod)
                _method = value
            End Set
        End Property

        Private _creditCard As CreditCard.CreditCardTypes
        Public Property CreditCardType() As CreditCard.CreditCardTypes
            Get
                Return _method
            End Get
            Set(ByVal value As CreditCard.CreditCardTypes)
                _method = value
            End Set
        End Property

        Private _creditCardNumber As String = ""
        Public Property CreditCardNumber() As String
            Get
                Return _creditCardNumber
            End Get
            Set(ByVal value As String)
                _creditCardNumber = value
            End Set
        End Property

        Private _expirationDate As Date
        Public Property ExpirationDate() As Date
            Get
                Return _expirationDate
            End Get
            Set(ByVal value As Date)
                _expirationDate = value
            End Set
        End Property

        Private _ccv As Integer = 0
        Public Property CCV() As Integer
            Get
                Return _ccv
            End Get
            Set(ByVal value As Integer)
                _ccv = value
            End Set
        End Property

        Private _cardHolderName As String = ""
        Public Property CardHolderName() As String
            Get
                Return _cardHolderName
            End Get
            Set(ByVal value As String)
                _cardHolderName = value
            End Set
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

        Private _stateOrProvince As String = ""
        Public Property StateOrProvince() As String
            Get
                Return _stateOrProvince
            End Get
            Set(ByVal value As String)
                _stateOrProvince = value
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

        Private _country As String = ""
        Public Property Country() As String
            Get
                Return _country
            End Get
            Set(ByVal value As String)
                _country = value
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

        Private _fax As String = ""
        Public Property Fax() As String
            Get
                Return _fax
            End Get
            Set(ByVal value As String)
                _fax = value
            End Set
        End Property

        Private _first As String = ""
        Public Property First() As String
            Get
                Return _first
            End Get
            Set(ByVal value As String)
                _first = value
            End Set
        End Property

        Private _last As String = ""
        Public Property Last() As String
            Get
                Return _last
            End Get
            Set(ByVal value As String)
                _last = value
            End Set
        End Property

        Private _isDefault As Boolean = False
        Public Property IsDefault() As Boolean
            Get
                Return _isDefault
            End Get
            Set(ByVal value As Boolean)
                _isDefault = value
            End Set
        End Property

        Private _createdDate As Date
        Public Property CreatedDate() As Date
            Get
                Return _createdDate
            End Get
            Set(ByVal value As Date)
                _createdDate = value
            End Set
        End Property

        Private _lastModifiedDate As Date
        Public Property LastModifiedDate() As Date
            Get
                Return _lastModifiedDate
            End Get
            Set(ByVal value As Date)
                _lastModifiedDate = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Billing) As Boolean Implements System.IEquatable(Of Billing).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

