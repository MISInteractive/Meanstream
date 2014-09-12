Imports System.Text.RegularExpressions

Namespace Meanstream.Portal.Subscription
    Public Class CreditCard

        Public Enum CreditCardTypes
            Invalid = -1
            Visa
            Mastercard
            Discover
            Amex
        End Enum

        Private Shared CreditCardPatterns As New List(Of String) _
                      (New String() { _
                      "^(4\d{12})|(4\d{15})$", _
                      "^5[1-5]\d{14}$", _
                      "(^(6011)\d{12}$)|(^(65)\d{14}$)", _
                      "^3[47]\d{13}$"})

        Private Shared _cardNumber As String

        Public Shared Function Validate(ByVal cardNumber As String) As CreditCardTypes
            Dim CardType As CreditCardTypes = CreditCardTypes.Invalid
            _cardNumber = Regex.Replace(cardNumber, "[^\d]", String.Empty)
            CardType = CType(CreditCardPatterns.FindIndex(AddressOf FindPattern), CreditCardTypes)
            If CardType = CreditCardTypes.Invalid Then
                _cardNumber = String.Empty
                Return CreditCardTypes.Invalid
            End If
            Dim Digits As Char() = _cardNumber.ToCharArray
            _cardNumber = String.Empty
            Dim Digit As Integer
            Dim Sum As Integer = 0
            Dim Alt As Boolean = False
            Array.Reverse(Digits)
            For Each Value As Char In Digits
                Digit = Integer.Parse(Value)
                If Alt Then
                    Digit *= 2
                    If Digit > 9 Then
                        Digit -= 9
                    End If
                End If
                Sum += Digit
                Alt = Not Alt
            Next
            If Sum Mod 10 = 0 Then
                Return CardType
            Else
                Return CreditCardTypes.Invalid
            End If
        End Function

        Private Shared Function FindPattern(ByVal value As String) As Boolean
            If Regex.IsMatch(_cardNumber, value) Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class
End Namespace