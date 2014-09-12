Namespace Meanstream.Portal.Core.Utilities
    ''' <summary>
    ''' Summary description for IsUtility.
    ''' </summary>
    Public Class IsUtility
        ''' <summary> Class cannot be instantiated</summary>
        Private Sub New()
        End Sub

        ''' <summary> Empty String tests a String to see if it is null or empty.
        ''' </summary>
        ''' <param name="value">String to be tested.
        ''' </param>
        ''' <returns> boolean true if empty.
        ''' </returns>
        Public Shared Function EmptyString(ByVal value As String) As Boolean
            Return (value Is Nothing OrElse value.Trim().Length = 0)
        End Function

        ' ''' <summary> Empty HttpCookie tests a HttpCookie to see if it is null or empty.
        ' ''' </summary>
        ' ''' <param name="HttpCookie">HttpCookie to be tested.
        ' ''' </param>
        ' ''' <returns> boolean true if empty.
        ' ''' </returns>
        'Public Shared Function EmptyHttpCookie(ByVal value As System.Web.HttpCookie) As Boolean
        '    Return (value Is Nothing OrElse value.Value Is Nothing OrElse value.Value.Trim().Length = 0)
        'End Function

    End Class
End Namespace
