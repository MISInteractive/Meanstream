#Region "Using Directives"
Imports System.Configuration
Imports System.Text
#End Region

Namespace Meanstream.Core.Query
    ''' <summary>
    ''' Provides utility methods for generating SQL expressions.
    ''' </summary>
    <CLSCompliant(True)> _
    Public NotInheritable Class SqlUtil
        Private Sub New()
        End Sub
#Region "Declarations"

        ''' <summary>
        ''' SQL AND keyword.
        ''' </summary>
        Public Shared ReadOnly [AND] As [String] = "AND"
        ''' <summary>
        ''' SQL OR keyword.
        ''' </summary>
        Public Shared ReadOnly [OR] As [String] = "OR"
        ''' <summary>
        ''' SQL ASC keyword.
        ''' </summary>
        Public Shared ReadOnly ASC As [String] = "ASC"
        ''' <summary>
        ''' SQL DESC keyword.
        ''' </summary>
        Public Shared ReadOnly DESC As [String] = "DESC"
        ''' <summary>
        ''' SQL NULL keyword.
        ''' </summary>
        Public Shared ReadOnly NULL As [String] = "NULL"
        ''' <summary>
        ''' Used to represent quoted search terms.
        ''' </summary>
        Public Shared ReadOnly TOKEN As [String] = "@@@"
        ''' <summary>
        ''' Delimiter for quoted search terms.
        ''' </summary>
        Public Shared ReadOnly QUOTE As [String] = """"
        ''' <summary>
        ''' Used as wildcard character within search text.
        ''' </summary>
        Public Shared ReadOnly STAR As [String] = "*"
        ''' <summary>
        ''' SQL wildcard character.
        ''' </summary>
        Public Shared ReadOnly WILD As [String] = "%"
        ''' <summary>
        ''' SQL grouping open character.
        ''' </summary>
        Public Shared ReadOnly LEFT As [String] = "("
        ''' <summary>
        ''' SQL grouping close character.
        ''' </summary>
        Public Shared ReadOnly RIGHT As [String] = ")"
        ''' <summary>
        ''' Delimiter for optional search terms.
        ''' </summary>
        Public Shared ReadOnly COMMA As [String] = ","

        ''' <summary>
        ''' PageIndex Temp Table
        ''' </summary>
        Public Shared ReadOnly PAGE_INDEX As [String] = "#PageIndex"

#End Region

#Region "Equals"

        ''' <summary>
        ''' Creates an <see cref="SqlComparisonType.Equals"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overloads Shared Function Equals(column As [String], value As [String]) As [String]
            Return Equals(column, value, False)
        End Function

        ''' <summary>
        ''' Creates an <see cref="SqlComparisonType.Equals"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Overloads Shared Function Equals(column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return Equals(column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates an <see cref="SqlComparisonType.Equals"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Overloads Shared Function Equals(column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetEqualFormat(ignoreCase, surround), column, Equals(value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.Equals"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Overloads Shared Function Equals(value As [String]) As [String]
            Return [String].Format("{0}", Encode(value))
        End Function

#End Region

#Region "Contains"

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.Contains"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function Contains(column As [String], value As [String]) As [String]
            Return Contains(column, value, False)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.Contains"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Shared Function Contains(column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return Contains(column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.Contains"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function Contains(column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetLikeFormat(ignoreCase, surround), column, Contains(value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.Contains"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Shared Function Contains(value As [String]) As [String]
            Return [String].Format("%{0}%", Encode(value))
        End Function

#End Region

#Region "NotContains"

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.NotContains"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function NotContains(column As [String], value As [String]) As [String]
            Return NotContains(column, value, False)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.NotContains"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Shared Function NotContains(column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return NotContains(column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.NotContains"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function NotContains(column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetNotLikeFormat(ignoreCase, surround), column, NotContains(value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.NotContains"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Shared Function NotContains(value As [String]) As [String]
            Return [String].Format("%{0}%", Encode(value))
        End Function

#End Region

#Region "StartsWith"

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.StartsWith"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function StartsWith(column As [String], value As [String]) As [String]
            Return StartsWith(column, value, False)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.StartsWith"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Shared Function StartsWith(column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return StartsWith(column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.StartsWith"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function StartsWith(column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetLikeFormat(ignoreCase, surround), column, StartsWith(value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.StartsWith"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Shared Function StartsWith(value As [String]) As [String]
            Return [String].Format("{0}%", Encode(value))
        End Function

#End Region

#Region "EndsWith"

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.EndsWith"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function EndsWith(column As [String], value As [String]) As [String]
            Return EndsWith(column, value, False)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.EndsWith"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Shared Function EndsWith(column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return EndsWith(column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.EndsWith"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function EndsWith(column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetLikeFormat(ignoreCase, surround), column, EndsWith(value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.EndsWith"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Shared Function EndsWith(value As [String]) As [String]
            Return [String].Format("%{0}", Encode(value))
        End Function

#End Region

#Region "Like"

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.Like"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function [Like](column As [String], value As [String]) As [String]
            Return [Like](column, value, False)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.Like"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Shared Function [Like](column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return [Like](column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.Like"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function [Like](column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetLikeFormat(ignoreCase, surround), column, [Like](value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.Like"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Shared Function [Like](value As [String]) As [String]
            Return [String].Format("{0}", Encode(value))
        End Function

#End Region

#Region "NotLike"

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.NotLike"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function NotLike(column As [String], value As [String]) As [String]
            Return NotLike(column, value, False)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.NotLike"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <returns></returns>
        Public Shared Function NotLike(column As [String], value As [String], ignoreCase As Boolean) As [String]
            Return NotLike(column, value, ignoreCase, True)
        End Function

        ''' <summary>
        ''' Creates a <see cref="SqlComparisonType.NotLike"/> expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function NotLike(column As [String], value As [String], ignoreCase As Boolean, surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return IsNull(column)
            End If
            Return [String].Format(GetNotLikeFormat(ignoreCase, surround), column, [Like](value))
        End Function

        ''' <summary>
        ''' Encodes the value for a <see cref="SqlComparisonType.NotLike"/> expression.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Shared Function NotLike(value As [String]) As [String]
            Return [String].Format("{0}", Encode(value))
        End Function

#End Region

#Region "Null/Not Null"

        ''' <summary>
        ''' Creates an IS NULL expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <returns></returns>
        Public Shared Function IsNull(column As [String]) As [String]
            Return [String].Format("{0} IS NULL", column)
        End Function

        ''' <summary>
        ''' Creates an IS NOT NULL expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <returns></returns>
        Public Shared Function IsNotNull(column As [String]) As [String]
            Return [String].Format("{0} IS NOT NULL", column)
        End Function

#End Region

#Region "Encode"

        ''' <summary>
        ''' Encodes the specified value for use in SQL expressions.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function Encode(value As [String]) As [String]
            Return Encode(value, False)
        End Function

        ''' <summary>
        ''' Encodes the specified value for use in SQL expressions and
        ''' optionally surrounds the value with single-quotes.
        ''' </summary>
        ''' <param name="value"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function Encode(value As [String], surround As Boolean) As [String]
            If [String].IsNullOrEmpty(value) Then
                Return SqlUtil.NULL
            End If
            Dim format As [String] = If(surround, "'{0}'", "{0}")
            Return [String].Format(format, value.Replace("'", "''"))
        End Function

        ''' <summary>
        ''' Encodes the specified values for use in SQL expressions.
        ''' </summary>
        ''' <param name="values"></param>
        ''' <returns></returns>
        Public Shared Function Encode(values As [String]()) As [String]
            Return Encode(values, False)
        End Function

        ''' <summary>
        ''' Encodes the specified values for use in SQL expressions and
        ''' optionally surrounds the value with single-quotes.
        ''' </summary>
        ''' <param name="values"></param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function Encode(values As [String](), surround As Boolean) As [String]
            If values Is Nothing OrElse values.Length < 1 Then
                Return SqlUtil.NULL
            End If

            Dim csv As New StringBuilder()

            For Each value As [String] In values
                If Not [String].IsNullOrEmpty(value) Then
                    If csv.Length > 0 Then
                        csv.Append(",")
                    End If

                    csv.Append(SqlUtil.Encode(value.Trim(), surround))
                End If
            Next

            Return csv.ToString()
        End Function

#End Region

#Region "Format Methods"

        ''' <summary>
        ''' Gets the like format string.
        ''' </summary>
        ''' <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        ''' <returns></returns>
        Public Shared Function GetLikeFormat(ignoreCase As Boolean) As [String]
            Return GetLikeFormat(ignoreCase, True)
        End Function

        ''' <summary>
        ''' Gets the like format string.
        ''' </summary>
        ''' <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function GetLikeFormat(ignoreCase As Boolean, surround As Boolean) As [String]
            If surround Then
                Return If(ignoreCase, "UPPER({0}.value) LIKE UPPER('{1}')", "{0}.value LIKE '{1}'")
            End If

            Return If(ignoreCase, "UPPER({0}.value) LIKE UPPER({1})", "{0}.value LIKE {1}")
        End Function

        ''' <summary>
        ''' Gets the not like format string.
        ''' </summary>
        ''' <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        ''' <returns></returns>
        Public Shared Function GetNotLikeFormat(ignoreCase As Boolean) As [String]
            Return GetNotLikeFormat(ignoreCase, True)
        End Function

        ''' <summary>
        ''' Gets the not like format string.
        ''' </summary>
        ''' <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function GetNotLikeFormat(ignoreCase As Boolean, surround As Boolean) As [String]
            If surround Then
                Return If(ignoreCase, "UPPER({0}.value) NOT LIKE UPPER('{1}')", "{0}.value NOT LIKE '{1}'")
            End If

            Return If(ignoreCase, "UPPER({0}.value) NOT LIKE UPPER({1})", "{0}.value NOT LIKE {1}")
        End Function



        ''' <summary>
        ''' Gets the equal format string.
        ''' </summary>
        ''' <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        ''' <returns></returns>
        Public Shared Function GetEqualFormat(ignoreCase As Boolean) As [String]
            Return GetEqualFormat(ignoreCase, True)
        End Function

        ''' <summary>
        ''' Gets the equal format string.
        ''' </summary>
        ''' <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        ''' <param name="surround"></param>
        ''' <returns></returns>
        Public Shared Function GetEqualFormat(ignoreCase As Boolean, surround As Boolean) As [String]
            If surround Then
                Return If(ignoreCase, "UPPER({0}.value) = UPPER('{1}')", "{0}.value = '{1}'")
            End If

            Return If(ignoreCase, "UPPER({0}.value) = UPPER({1})", "{0}.value = {1}")
        End Function

#End Region
    End Class

#Region "SqlComparisonType Enum"

    ''' <summary>
    ''' Enumeration of SQL expression comparison types.
    ''' </summary>
    Public Enum SqlComparisonType
        ''' <summary>
        ''' Represents = value.
        ''' </summary>
        Equals
        ''' <summary>
        ''' Represents LIKE value%.
        ''' </summary>
        StartsWith
        ''' <summary>
        ''' Represents LIKE %value.
        ''' </summary>
        EndsWith
        ''' <summary>
        ''' Represents LIKE %value%.
        ''' </summary>
        Contains
        ''' <summary>
        ''' Represents NOT LIKE %value%.
        ''' </summary>
        NotContains
        ''' <summary>
        ''' Represents LIKE value.
        ''' </summary>
        [Like]
        ''' <summary>
        ''' Represents NOT LIKE value.
        ''' </summary>
        NotLike

    End Enum

#End Region

#Region "SqlFilterType Enum"

    ''' <summary>
    ''' Enumeration of SQL Filter Types.
    ''' </summary>
    Public Enum SqlFilterType
        ''' <summary>
        ''' Represents an Individual Word filter
        ''' </summary>
        ''' <example>(if using <see cref="SqlComparisonType.Contains"/>) CompanyName LIKE "%Acme" AND CompanyName LIKE "Company%"</example>
        Word
        ''' <summary>
        ''' Represents a Phrase filter
        ''' </summary>
        ''' <example>(if using <see cref="SqlComparisonType.Contains"/>) CompanyName LIKE "%Acme Company%"</example>
        Phrase
    End Enum

#End Region

#Region "SqlSortDirection Enum"

    ''' <summary>
    ''' Enumeration of SQL expression Sort Directions
    ''' </summary>
    Public Enum SqlSortDirection
        ''' <summary>
        ''' Database Ascending
        ''' </summary>
        ASC

        ''' <summary>
        ''' Database Descending
        ''' </summary>
        DESC
    End Enum

#End Region
End Namespace

