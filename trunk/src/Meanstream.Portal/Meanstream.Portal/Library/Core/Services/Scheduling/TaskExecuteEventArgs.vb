Imports Meanstream.Portal.Core.Messaging

Namespace Meanstream.Portal.Core.Services.Scheduling
    Public Class TaskExecuteEventArgs
        Inherits EventArgs

        Private _applicationMessage As ApplicationMessage
        Public ReadOnly Property Message() As ApplicationMessage
            Get
                Return _applicationMessage
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

        Public Sub New(ByVal applicationMessage As ApplicationMessage)
            _applicationMessage = applicationMessage
        End Sub
    End Class
End Namespace


