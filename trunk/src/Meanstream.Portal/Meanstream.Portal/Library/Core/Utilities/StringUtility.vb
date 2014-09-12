Namespace Meanstream.Portal.Core.Utilities
    ''' <summary>
    ''' Summary description for StrUtil.
    ''' </summary>
    Public Class StringUtility
        ''' <summary> Class cannot be instantiated</summary>
        Private Sub New()
        End Sub

#Region "Left"
        ''' <summary>
        ''' Gets the leftmost n characters of a String. If n characters are not
        ''' available, or the String is <code>null</code>, the String will be
        ''' returned without an exception.
        ''' </summary>
        ''' <param name="str">The String to get the leftmost characters from</param>
        ''' <param name="len">The length of the required String</param>
        ''' <returns>The leftmost characters</returns>
        Public Shared Function Left(ByVal str As String, ByVal len As Integer) As String
            If len < 0 OrElse str Is Nothing OrElse str.Length <= len Then
                Return str
            Else
                Return str.Substring(0, len)
            End If
        End Function
#End Region

#Region "LeftIndexOf"
        ''' <summary>
        ''' Gets the left most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the left most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <returns>The left most characters</returns>
        Public Shared Function LeftIndexOf(ByVal str As String, ByVal search As String) As String
            Return LeftIndexOf(str, search, 0, False)
        End Function

        ''' <summary>
        ''' Gets the left most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the left most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <param name="includeSearchString">If true will include the search string in the result, if false then the search string is excluded</param>
        ''' <returns>The left most characters</returns>
        Public Shared Function LeftIndexOf(ByVal str As String, ByVal search As String, ByVal includeSearchString As Boolean) As String
            Return LeftIndexOf(str, search, 0, includeSearchString)
        End Function

        ''' <summary>
        ''' Gets the left most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the left most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <param name="fromIndex">The index to start the search from</param>
        ''' <returns>The left most characters</returns>
        Public Shared Function LeftIndexOf(ByVal str As String, ByVal search As String, ByVal fromIndex As Integer) As String
            Return LeftIndexOf(str, search, fromIndex, False)
        End Function

        ''' <summary>
        ''' Gets the left most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the left most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <param name="fromIndex">The index to start the search from</param>
        ''' <param name="includeSearchString">If true will include the search string in the result, if false then the search string is excluded</param>
        ''' <returns>The left most characters</returns>
        Public Shared Function LeftIndexOf(ByVal str As String, ByVal search As String, ByVal fromIndex As Integer, ByVal includeSearchString As Boolean) As String
            If IsUtility.EmptyString(str) OrElse IsUtility.EmptyString(search) Then
                Return str
            Else
                If fromIndex > str.Length Then
                    Return str
                End If

                Dim pos As Integer = str.IndexOf(search, fromIndex)

                If pos = -1 Then
                    Return str
                Else
                    Return str.Substring(0, pos + (If(includeSearchString, search.Length, 0)))
                End If
            End If
        End Function
#End Region

#Region "LeftOf"
        ''' <summary>
        ''' Gets the characters to the left of <var>pos</var>.
        ''' </summary>
        ''' <param name="str">The String to get the left most characters from</param>
        ''' <param name="pos">The position to cut from</param>
        ''' <returns>The left most characters</returns>
        Public Shared Function LeftOf(ByVal str As String, ByVal pos As Integer) As String
            If str Is Nothing Then
                Return Nothing
            Else
                If pos = -1 Then
                    Return ""
                End If
                If pos > str.Length Then
                    Return str
                End If

                Return Substring(str, 0, pos)
            End If
        End Function
#End Region

#Region "Right"
        ''' <summary>
        ''' Gets the right most n characters of a String. If n characters are not
        ''' available, or the String is <code>null</code>, the String will be
        ''' returned without an exception.
        ''' </summary>
        ''' <param name="str">The String to get the right most characters from</param>
        ''' <param name="len">The length of the required String</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function Right(ByVal str As String, ByVal len As Integer) As String
            If len < 0 OrElse str Is Nothing OrElse str.Length <= len Then
                Return str
            Else
                Return str.Substring(str.Length - len)
            End If
        End Function
#End Region

#Region "RightIndexOf"
        ''' <summary>
        ''' Gets the right most characters starting from the index of <var>search</var>.
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the right most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function RightIndexOf(ByVal str As String, ByVal search As String) As String
            Return RightIndexOf(str, search, 0, False)
        End Function

        ''' <summary>
        ''' Gets the right most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the right most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <param name="includeSearchString">If true will include the search string in the result, if false then the search string is excluded</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function RightIndexOf(ByVal str As String, ByVal search As String, ByVal includeSearchString As Boolean) As String
            Return RightIndexOf(str, search, 0, includeSearchString)
        End Function

        ''' <summary>
        ''' Gets the right most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the right most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <param name="fromIndex">The index to start the search from</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function RightIndexOf(ByVal str As String, ByVal search As String, ByVal fromIndex As Integer) As String
            Return RightIndexOf(str, search, fromIndex, False)
        End Function

        ''' <summary>
        ''' Gets the right most characters starting from the index of <var>search</var>
        ''' <br/>
        ''' If <var>str</var> is <code>null</code>, then <var>str</var> will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the right most characters off</param>
        ''' <param name="search">The string to search for</param>
        ''' <param name="fromIndex">The index to start the search from</param>
        ''' <param name="includeSearchString">If true will include the search string in the result, if false then the search string is excluded</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function RightIndexOf(ByVal str As String, ByVal search As String, ByVal fromIndex As Integer, ByVal includeSearchString As Boolean) As String
            If IsUtility.EmptyString(str) OrElse IsUtility.EmptyString(search) OrElse fromIndex > str.Length Then
                Return str
            Else
                Dim pos As Integer = str.IndexOf(search, fromIndex)

                If pos = -1 Then
                    Return str
                Else
                    Return str.Substring(pos + (If(includeSearchString, 0, search.Length)))
                End If
            End If
        End Function
#End Region

#Region "RightLastIndexOf"
        ''' <summary>
        ''' Gets the right most characters starting from the last index of <var>search</var>.
        ''' <br/>
        ''' If the String is <code>null</code>, the String will be returned without an exception.
        ''' <br/><br/>
        ''' If <var>search</var> is not found then <var>str</var> will be returned without an exception.
        ''' </summary>
        ''' <param name="str">The string to get the right most characters off</param>
        ''' <param name="search">The last occurence of search to start from</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function RightLastIndexOf(ByVal str As String, ByVal search As String) As String
            If (str Is Nothing) OrElse (search Is Nothing) Then
                Return str
            Else
                Dim pos As Integer = str.LastIndexOf(search)

                If pos = -1 Then
                    Return str
                Else
                    Return str.Substring(pos + search.Length)
                End If
            End If

        End Function
#End Region

#Region "RightOf"
        ''' <summary>
        ''' Gets the characters to the right of <var>pos</var>.
        ''' </summary>
        ''' <param name="str">The String to get the right most characters from</param>
        ''' <param name="pos">The position to cut from</param>
        ''' <returns>The right most characters</returns>
        Public Shared Function RightOf(ByVal str As String, ByVal pos As Integer) As String
            If str Is Nothing Then
                Return Nothing
            Else
                If pos = -1 Then
                    Return str
                End If
                Return Substring(str, pos + 1)
            End If
        End Function
#End Region

#Region "Substring"
        ''' <summary>
        ''' Gets a substring from the specified string avoiding exceptions from invalid start indexes or null strings.
        ''' </summary>
        ''' <param name="str">The String to get the substring from</param>
        ''' <param name="start">The index of the start of the substring</param>
        ''' <returns>substring from start position</returns>
        Public Shared Function Substring(ByVal str As String, ByVal start As Integer) As String
            If str Is Nothing Then
                Return Nothing
            End If

            If start < 0 Then
                start = 0
            End If
            ' Start must be 0 or greater
            If start > str.Length Then
                Return ""
            End If
            ' Start to big
            Return str.Substring(start)
        End Function

        ''' <summary>
        ''' Gets a substring from the specified string avoiding exceptions from invalid start indexes, lengths or null strings.
        ''' </summary>
        ''' <param name="str">The String to get the substring from</param>
        ''' <param name="start">The index of the start of the substring</param>
        ''' <param name="length">The number of characters in the substring. </param>
        ''' <returns>substring from start position for <var>length</var> characters</returns>
        Public Shared Function Substring(ByVal str As String, ByVal start As Integer, ByVal length As Integer) As String
            If str Is Nothing Then
                Return Nothing
            End If

            If start < 0 Then
                ' Start must be 0 or greater
                start = 0
            End If
            If length > str.Length - start Then
                length = str.Length - start
            End If

            Return str.Substring(start, length)
        End Function
#End Region

#Region "StartsWith"
        ''' <summary>
        ''' Similer to String.StartsWith except that it is case-insensitive.
        ''' </summary>
        ''' <param name="str">The string to test</param>
        ''' <param name="StartsWith__1">The string to seek</param>
        ''' <returns><b>true</b> if value matches the beginning of this string or is Empty; otherwise <b>false</b></returns>
        Public Shared Function StartsWith(ByVal str As String, ByVal startsWith__1 As String) As Boolean
            If str Is Nothing OrElse startsWith__1 Is Nothing OrElse startsWith__1.Length > str.Length Then
                Return False
            End If

            ' REFACTOR: Would be quicker to test each character in a loop
            Return str.ToLower().StartsWith(startsWith__1.ToLower())
        End Function
#End Region

    End Class
End Namespace
