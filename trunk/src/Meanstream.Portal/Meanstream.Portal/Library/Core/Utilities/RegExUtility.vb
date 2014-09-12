Imports System.Text.RegularExpressions

Namespace Meanstream.Portal.Core.Utilities
    ''' <summary>
    ''' Summary description for RegExUtility.
    ''' </summary>
    Public Class RegExUtility
#Region "fields"
        Private Shared m_standardRegularExpressions As Regex()
#End Region


        ''' <summary> Class cannot be instantiated</summary>
        Private Sub New()
        End Sub

        ''' <summary> 
        ''' Get a predefined regular expression
        ''' </summary>
        ''' <param name="regularExpressionId">Id of the regular expression to return
        ''' </param>
        ''' <returns>RegEx</returns>
        Public Shared Function GetRegEx(ByVal regularExpressionId As RegularExpression) As Regex
            If m_standardRegularExpressions Is Nothing Then
                m_standardRegularExpressions = New Regex([Enum].GetNames(GetType(RegularExpression)).Length - 1) {}
            End If

            Dim index As Integer = CInt(regularExpressionId)

            If m_standardRegularExpressions(index) Is Nothing Then
                m_standardRegularExpressions(index) = StandardRegularExpression(regularExpressionId)
            End If

            Return m_standardRegularExpressions(index)
        End Function

        ''' <summary> 
        ''' Get a match object based on a predefined regular expression
        ''' </summary>
        ''' <param name="regularExpressionId">Id of the regular expression to return</param>
        ''' <param name="text">Text to match on</param>
        ''' <returns>Match</returns>
        Public Shared Function GetMatchRegEx(ByVal regularExpressionId As RegularExpression, ByVal text As String) As Match
            Return GetRegEx(regularExpressionId).Match(text)
        End Function

        Private Shared Function StandardRegularExpression(ByVal regularExpressionId As RegularExpression) As Regex

            Select Case regularExpressionId
                Case RegularExpression.UrlExtractor
                    If True Then
                        Return New Regex("(?:href\s*=)(?:[\s""']*)(?!#|mailto|location.|javascript|.*css|.*this\.)(?<url>.*?)(?:[\s>""'])", RegexOptions.IgnoreCase)
                    End If
                Case RegularExpression.SrcExtractor
                    If True Then
                        Return New Regex("(?:src\s*=)(?:[\s""']*)(?<url>.*?)(?:[\s>""'])", RegexOptions.IgnoreCase)
                    End If
            End Select

            Return Nothing
        End Function

    End Class

    Public Enum RegularExpression
        UrlExtractor
        SrcExtractor
    End Enum

End Namespace
