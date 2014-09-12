
Namespace Meanstream.Portal.Subscription
    Public Class Payment
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Subscription.Payment)

        Public Enum PaymentStatus
            Received
            Declined
            Errors
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

        Private _billingId As Guid
        Public Property BillingId() As Guid
            Get
                Return _billingId
            End Get
            Set(ByVal value As Guid)
                _billingId = value
            End Set
        End Property

        Private _paymentProcessorId As Guid
        Public Property PaymentProcessorId() As Guid
            Get
                Return _paymentProcessorId
            End Get
            Set(ByVal value As Guid)
                _paymentProcessorId = value
            End Set
        End Property

        Private _status As PaymentStatus
        Public Property Status() As PaymentStatus
            Get
                Return _status
            End Get
            Set(ByVal value As PaymentStatus)
                _status = value
            End Set
        End Property

        Private _statusMessage As String = ""
        Public Property StatusMessage() As String
            Get
                Return _statusMessage
            End Get
            Set(ByVal value As String)
                _statusMessage = value
            End Set
        End Property

        Private _transactionId As String = ""
        Public Property TransactionId() As String
            Get
                Return _transactionId
            End Get
            Set(ByVal value As String)
                _transactionId = value
            End Set
        End Property

        Private _authCode As String = ""
        Public Property AuthCode() As String
            Get
                Return _authCode
            End Get
            Set(ByVal value As String)
                _authCode = value
            End Set
        End Property

        Private _paymentDate As Date
        Public Property PaymentDate() As Date
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As Date)
                _paymentDate = value
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

        Public Overloads Function Equals(ByVal other As Payment) As Boolean Implements System.IEquatable(Of Payment).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

