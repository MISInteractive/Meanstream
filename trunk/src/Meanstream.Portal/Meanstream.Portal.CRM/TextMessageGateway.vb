
Namespace Meanstream.Portal.Core.Messaging
    Public Class TextMessageCarrier
        Implements IEquatable(Of TextMessageCarrier)

        Sub New(ByVal id As Guid)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _carrier As String
        Public Property Carrier() As String
            Get
                Return _carrier
            End Get
            Set(ByVal value As String)
                _carrier = value
            End Set
        End Property

        Private _gateway As String
        Public Property Gateway() As String
            Get
                Return _gateway
            End Get
            Set(ByVal value As String)
                _gateway = value
            End Set
        End Property

        Private _gatewayNumber As String
        Public Property GatewayNumber() As String
            Get
                Return _gatewayNumber
            End Get
            Set(ByVal value As String)
                _gatewayNumber = value
            End Set
        End Property

        Private _gatewayFormat As String
        Public Property GatewayFormat() As String
            Get
                Return _gatewayFormat
            End Get
            Set(ByVal value As String)
                _gatewayFormat = value
            End Set
        End Property

        Private _type As Core.Messaging.TextMessage.TextMessageType
        Public Property Type() As Core.Messaging.TextMessage.TextMessageType
            Get
                Return _type
            End Get
            Set(ByVal value As Core.Messaging.TextMessage.TextMessageType)
                _type = value
            End Set
        End Property

#End Region

        Public Overloads Function Equals(ByVal other As TextMessageCarrier) As Boolean Implements System.IEquatable(Of TextMessageCarrier).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

