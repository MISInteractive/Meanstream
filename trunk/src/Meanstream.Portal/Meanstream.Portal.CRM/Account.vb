
Namespace Meanstream.Portal.CRM
    Public Class Account
        Inherits Meanstream.Portal.CRM.Extensibility.AttributeEntity
        Implements IEquatable(Of Account)

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

        Private _billingAddress As String = ""
        Public Property BillingAddress() As String
            Get
                Return _billingAddress
            End Get
            Set(ByVal value As String)
                _billingAddress = value
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

        Private _name As String = ""
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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

        Private _preferredDeliveryMethod As String = ""
        Public Property PreferredDeliveryMethod() As String
            Get
                Return _preferredDeliveryMethod
            End Get
            Set(ByVal value As String)
                _preferredDeliveryMethod = value
            End Set
        End Property

        Private _preferredPaymentMethod As String = ""
        Public Property PreferredPaymentMethod() As String
            Get
                Return _preferredPaymentMethod
            End Get
            Set(ByVal value As String)
                _preferredPaymentMethod = value
            End Set
        End Property

        Private _shippingAddress As String = ""
        Public Property ShippingAddress() As String
            Get
                Return _shippingAddress
            End Get
            Set(ByVal value As String)
                _shippingAddress = value
            End Set
        End Property

        Private _taxResaleNum As String = ""
        Public Property TaxResaleNum() As String
            Get
                Return _taxResaleNum
            End Get
            Set(ByVal value As String)
                _taxResaleNum = value
            End Set
        End Property

        Private _terms As String = ""
        Public Property Terms() As String
            Get
                Return _terms
            End Get
            Set(ByVal value As String)
                _terms = value
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
#End Region

        Public Overloads Function Equals(ByVal other As Account) As Boolean Implements System.IEquatable(Of Account).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

