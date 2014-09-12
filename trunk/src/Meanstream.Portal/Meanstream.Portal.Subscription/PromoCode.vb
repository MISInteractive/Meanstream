
Namespace Meanstream.Portal.Subscription
    Public Class PromoCode
        Implements IEquatable(Of Meanstream.Portal.Subscription.PromoCode)

        Public Enum DiscountType
            Dollar
            Percentage
        End Enum

        Sub New(ByVal code As String)
            _code = code
        End Sub

#Region " Properties "
        Private _code As String
        Public ReadOnly Property Code() As String
            Get
                Return _code
            End Get
        End Property

        Private _portalId As Guid
        Public Property PortalId() As Guid
            Get
                Return _portalId
            End Get
            Set(ByVal value As Guid)
                _portalId = value
            End Set
        End Property

        Private _type As DiscountType
        Public Property Type() As DiscountType
            Get
                Return _type
            End Get
            Set(ByVal value As DiscountType)
                _type = value
            End Set
        End Property

        Private _discount As String = ""
        Public Property Discount() As String
            Get
                Return _discount
            End Get
            Set(ByVal value As String)
                _discount = value
            End Set
        End Property

        Private _allocation As Integer = 0
        Public Property Allocation() As Integer
            Get
                Return _allocation
            End Get
            Set(ByVal value As Integer)
                _allocation = value
            End Set
        End Property

        Private _used As Integer = 0
        Public Property Used() As Integer
            Get
                Return _used
            End Get
            Set(ByVal value As Integer)
                _used = value
            End Set
        End Property

        Private _salesRep As String = ""
        Public Property SalesRep() As String
            Get
                Return _salesRep
            End Get
            Set(ByVal value As String)
                _salesRep = value
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

        Public Overloads Function Equals(ByVal other As PromoCode) As Boolean Implements System.IEquatable(Of PromoCode).Equals
            If Me.Code = other.Code Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

