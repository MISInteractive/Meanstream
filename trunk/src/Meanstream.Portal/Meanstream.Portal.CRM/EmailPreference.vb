
Namespace Meanstream.Portal.CRM
    Public Class EmailPreference
        Implements IEquatable(Of EmailPreference)

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

        Private _userId As Guid
        Public Property UserId() As Guid
            Get
                Return _userId
            End Get
            Set(ByVal value As Guid)
                _userId = value
            End Set
        End Property

        Private _defaultSendingAddress As String = ""
        Public Property DefaultSendingAddress() As String
            Get
                Return _defaultSendingAddress
            End Get
            Set(ByVal value As String)
                _defaultSendingAddress = value
            End Set
        End Property

        Private _compositionMode As String = ""
        Public Property CompositionMode() As String
            Get
                Return _compositionMode
            End Get
            Set(ByVal value As String)
                _compositionMode = value
            End Set
        End Property

        Private _signature As String = ""
        Public Property Signature() As String
            Get
                Return _signature
            End Get
            Set(ByVal value As String)
                _signature = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As EmailPreference) As Boolean Implements System.IEquatable(Of EmailPreference).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
End Class
End Namespace

