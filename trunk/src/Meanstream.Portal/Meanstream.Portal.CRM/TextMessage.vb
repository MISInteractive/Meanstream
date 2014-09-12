
Namespace Meanstream.Portal.Core.Messaging
    Public Class TextMessage
        Implements IEquatable(Of TextMessage)


        Public Enum TextMessageType
            SMS = 1
            MMS = 2
            SML = 3
        End Enum

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

        Private _carrier As TextMessageCarrier
        Public Property Carrier() As TextMessageCarrier
            Get
                Return _carrier
            End Get
            Set(ByVal value As TextMessageCarrier)
                _carrier = value
            End Set
        End Property

        Private _recipient As String
        Public Property Recipient() As String
            Get
                Return _recipient
            End Get
            Set(ByVal value As String)
                _recipient = value
            End Set
        End Property

        Private _sender As String
        Public Property Sender() As String
            Get
                Return _sender
            End Get
            Set(ByVal value As String)
                _sender = value
            End Set
        End Property

        Private _subject As String
        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        Private _body As String
        Public Property Body() As String
            Get
                Return _body
            End Get
            Set(ByVal value As String)
                _body = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As TextMessage) As Boolean Implements System.IEquatable(Of TextMessage).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

