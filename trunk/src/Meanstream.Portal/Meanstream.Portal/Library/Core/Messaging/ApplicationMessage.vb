
Namespace Meanstream.Portal.Core.Messaging

    Public Class ApplicationMessage
        Private _message As String
        Public Property Message() As String
            Get
                Return _message
            End Get
            Private Set(ByVal value As String)
                _message = value
            End Set
        End Property

        Private _messageType As ApplicationMessageType
        Public Property MessageType() As ApplicationMessageType
            Get
                Return _messageType
            End Get
            Private Set(ByVal value As ApplicationMessageType)
                _messageType = value
            End Set
        End Property


        Public Sub New(ByVal applicationMessage__1 As String)
            Me.New(applicationMessage__1, ApplicationMessageType.[Custom])
        End Sub

        Public Sub New(ByVal applicationMessage__1 As String, ByVal applicationMessageType As ApplicationMessageType)
            If String.IsNullOrEmpty(applicationMessage__1) Then
                Throw New ArgumentNullException("applicationMessage", "The application message is null or empty.")
            End If

            Message = applicationMessage__1
            MessageType = applicationMessageType
        End Sub

    End Class

End Namespace
