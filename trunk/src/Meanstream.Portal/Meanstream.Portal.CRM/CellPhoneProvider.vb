
Namespace Meanstream.Portal.CRM
    Public Class CellPhoneProvider
        Implements IEquatable(Of CellPhoneProvider)

        Sub New(ByVal provider As String)
            _provider = provider
        End Sub

#Region " Properties "
        Private _provider As String
        Public Overloads ReadOnly Property Provider() As String
            Get
                Return _provider
            End Get
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As CellPhoneProvider) As Boolean Implements System.IEquatable(Of CellPhoneProvider).Equals
            If Me.Provider = other.Provider Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

