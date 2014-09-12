﻿
Namespace Meanstream.Portal.CRM
    Public Class ReminderDeliveryType
        Implements IEquatable(Of ReminderDeliveryType)

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

        Public Overloads Function Equals(ByVal other As ReminderDeliveryType) As Boolean Implements System.IEquatable(Of ReminderDeliveryType).Equals
            If Me.Type = other.Type Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

