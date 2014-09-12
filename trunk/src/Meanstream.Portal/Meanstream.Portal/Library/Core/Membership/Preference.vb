
Namespace Meanstream.Portal.Core.Membership
    Public Class Preference
        Implements IEquatable(Of Meanstream.Portal.Core.Membership.Preference)

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

        Private _preferenceId As Guid
        Public Property PreferenceId() As Guid
            Get
                Return _preferenceId
            End Get
            Set(ByVal value As Guid)
                _preferenceId = value
            End Set
        End Property

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Preference) As Boolean Implements System.IEquatable(Of Preference).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

