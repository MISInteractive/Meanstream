
Namespace Meanstream.Portal.Subscription
    Public Class Subscriber
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Subscription.Subscriber)

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

        Private _portalId As Guid
        Public Property PortalId() As Guid
            Get
                Return _portalId
            End Get
            Set(ByVal value As Guid)
                _portalId = value
            End Set
        End Property

        Private _userId As Guid
        Public Property UserId() As Guid
            Get
                Return _userId
            End Get
            Set(ByVal value As Guid)
                _userId = value
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

        Private _displayName As String = ""
        Public Property DisplayName() As String
            Get
                Return _displayName
            End Get
            Set(ByVal value As String)
                _displayName = value
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

        Private _status As Meanstream.Portal.PaymentGateway.SubscriptionStatus
        Public Property Status() As Meanstream.Portal.PaymentGateway.SubscriptionStatus
            Get
                Return _status
            End Get
            Set(ByVal value As Meanstream.Portal.PaymentGateway.SubscriptionStatus)
                _status = value
            End Set
        End Property

        Private _reasonForCanceling As String = ""
        Public Property ReasonForCanceling() As String
            Get
                Return _reasonForCanceling
            End Get
            Set(ByVal value As String)
                _reasonForCanceling = value
            End Set
        End Property

        Private _promoCode As String = ""
        Public Property PromoCode() As String
            Get
                Return _promoCode
            End Get
            Set(ByVal value As String)
                _promoCode = value
            End Set
        End Property

        Private _gatewaySubscriptionId As String = ""
        Public Property GatewaySubscriptionId() As String
            Get
                Return _gatewaySubscriptionId
            End Get
            Set(ByVal value As String)
                _gatewaySubscriptionId = value
            End Set
        End Property

        Private _paymentPlan As Meanstream.Portal.PaymentGateway.PaymentSchedule
        Public Property PaymentPlan() As Meanstream.Portal.PaymentGateway.PaymentSchedule
            Get
                Return _paymentPlan
            End Get
            Set(ByVal value As Meanstream.Portal.PaymentGateway.PaymentSchedule)
                _paymentPlan = value
            End Set
        End Property

        Private _signupDate As Date
        Public Property SignupDate() As Date
            Get
                Return _signupDate
            End Get
            Set(ByVal value As Date)
                _signupDate = value
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

        Public Overloads Function Equals(ByVal other As Subscriber) As Boolean Implements System.IEquatable(Of Subscriber).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

