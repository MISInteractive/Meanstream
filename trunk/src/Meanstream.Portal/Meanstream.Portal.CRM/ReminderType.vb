
Namespace Meanstream.Portal.CRM
    Public Class ReminderType
        Implements IEquatable(Of ReminderType)

        Sub New(ByVal type As String)
            _type = type
        End Sub

#Region " Properties "
        Private _type As String
        Public Overloads ReadOnly Property Type() As String
            Get
                Return _type
            End Get
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As ReminderType) As Boolean Implements System.IEquatable(Of ReminderType).Equals
            If Me.Type = other.Type Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

