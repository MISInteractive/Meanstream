Namespace Meanstream.Portal.Core.Messaging
    Public Class MessageEventArgs
        Inherits EventArgs
        Private _message As Message
        Public ReadOnly Property Message() As Message
            Get
                Return _message
            End Get
        End Property

        Private _handled As Boolean
        Public Property Handled() As Boolean
            Get
                Return _handled
            End Get
            Set(ByVal value As Boolean)
                _handled = value
            End Set
        End Property

        Public Sub New(ByVal message As Message)
            _message = message
        End Sub
    End Class
End Namespace
