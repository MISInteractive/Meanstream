
Namespace Meanstream.Portal.PaymentGateway
    Public Class SubscriptionRequest

        Sub New(ByVal id As String)
            _id = id
        End Sub

        Public Sub New()
        End Sub

#Region " Properties "
        Private _id As String = ""
        Public Property Id() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

        Private _billingAddressId As String = ""
        Public Property BillingAddressId() As String
            Get
                Return _billingAddressId
            End Get
            Set(ByVal value As String)
                _billingAddressId = value
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

        Private _company As String = ""
        Public Property Company() As String
            Get
                Return _company
            End Get
            Set(ByVal value As String)
                _company = value
            End Set
        End Property

        Private _creditCardexpirationDate As Date
        Public Property CreditCardExpirationDate() As Date
            Get
                Return _creditCardexpirationDate
            End Get
            Set(ByVal value As Date)
                _creditCardexpirationDate = value
            End Set
        End Property

        Private _startsOn As Date
        Public Property StartsOn() As Date
            Get
                Return _startsOn
            End Get
            Set(ByVal value As Date)
                _startsOn = value
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

        Private _name As String = ""
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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

        Private _email As String = ""
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        Private _paymentSchedule As Meanstream.Portal.PaymentGateway.PaymentSchedule
        Public Property PaymentSchedule() As Meanstream.Portal.PaymentGateway.PaymentSchedule
            Get
                Return _paymentSchedule
            End Get
            Set(ByVal value As Meanstream.Portal.PaymentGateway.PaymentSchedule)
                _paymentSchedule = value
            End Set
        End Property

        Private _amount As Decimal = 0.0
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property
#End Region
    End Class
End Namespace

