
Namespace Meanstream.Portal.Core.Membership
    Public Class UserPreference
        Implements IEquatable(Of Meanstream.Portal.Core.Membership.UserPreference)

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

        Private _userId As Guid = Nothing
        Public Property UserId() As Guid
            Get
                Return _userId
            End Get
            Set(ByVal value As Guid)
                _userId = value
            End Set
        End Property

        Private _preferenceId As Guid
        Public Property PreferenceId() As Guid
            Get
                Return _preferenceId
            End Get
            Set(ByVal value As Guid)
                _preferenceId = value
            End Set
        End Property

        Private _value As String
        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As UserPreference) As Boolean Implements System.IEquatable(Of UserPreference).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

