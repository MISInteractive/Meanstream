
Namespace Meanstream.Portal.CRM
    Public Class StoredPhoneScript
        Implements IEquatable(Of StoredPhoneScript)

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

        Private _name As String = ""
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _script As String = ""
        Public Property Script() As String
            Get
                Return _script
            End Get
            Set(ByVal value As String)
                _script = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As StoredPhoneScript) As Boolean Implements System.IEquatable(Of StoredPhoneScript).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

