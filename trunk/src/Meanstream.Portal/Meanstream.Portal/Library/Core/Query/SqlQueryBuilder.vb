#Region "Using Directives"
Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text
#End Region

Namespace Meanstream.Core.Query
    ''' <summary>
    ''' Represents a SQL filter builder expression enumeration.
    ''' </summary>
    <CLSCompliant(True)> _
    Public Enum StringBuilderExpression
        ''' <summary>
        ''' Append
        ''' </summary>
        Append = 1
        ''' <summary>
        ''' AppendEquals
        ''' </summary>
        AppendEquals
        ''' <summary>
        ''' AppendNotEquals
        ''' </summary>
        AppendNotEquals
        ''' <summary>
        ''' AppendIn
        ''' </summary>
        AppendIn
        ''' <summary>
        ''' AppendNotIn
        ''' </summary>
        AppendNotIn
        ''' <summary>
        ''' AppendInQuery
        ''' </summary>
        AppendInQuery
        ''' <summary>
        ''' AppendNotInQuery
        ''' </summary>
        AppendNotInQuery
        ''' <summary>
        ''' AppendRange
        ''' </summary>
        AppendRange
        ''' <summary>
        ''' AppendNotRange
        ''' </summary>
        AppendNotRange
        ''' <summary>
        ''' AppendIsNull
        ''' </summary>
        AppendIsNull
        ''' <summary>
        ''' AppendIsNotNull
        ''' </summary>
        AppendIsNotNull
        ''' <summary>
        ''' AppendGreaterThan
        ''' </summary>
        AppendGreaterThan
        ''' <summary>
        ''' AppendGreaterThanOrEqual
        ''' </summary>
        AppendGreaterThanOrEqual
        ''' <summary>
        ''' AppendLessThan
        ''' </summary>
        AppendLessThan
        ''' <summary>
        ''' AppendLessThanOrEqual
        ''' </summary>
        AppendLessThanOrEqual
        ''' <summary>
        ''' AppendStartsWith
        ''' </summary>
        AppendStartsWith
        ''' <summary>
        ''' AppendEndsWith
        ''' </summary>
        AppendEndsWith
        ''' <summary>
        ''' AppendContains
        ''' </summary>
        AppendContains
        ''' <summary>
        ''' AppendNotContains
        ''' </summary>
        AppendNotContains
        ''' <summary>
        ''' AppendLike
        ''' </summary>
        AppendLike
        ''' <summary>
        ''' AppendLike
        ''' </summary>
        AppendNotLike

    End Enum

    ''' <summary>
    ''' Represents a SQL filter expression.
    ''' </summary>
    Public Class SqlQueryBuilder
        Implements IQueryBuilder
        Implements IQuery

#Region "Declarations"

        Private sql As New StringBuilder()
        Private _groupCount As Integer = 0

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the SqlQueryBuilder class.
        ''' </summary>
        Public Sub New()
            Me.m_junction = SqlUtil.[AND]
            Me.m_ignoreCase = False
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the SqlQueryBuilder class.
        ''' </summary>
        ''' <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
        Public Sub New(ignoreCase As Boolean)
            Me.m_junction = SqlUtil.[AND]
            Me.m_ignoreCase = ignoreCase
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the SqlQueryBuilder class.
        ''' </summary>
        ''' <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
        ''' <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
        Public Sub New(ignoreCase As Boolean, useAnd As Boolean)
            Me.m_junction = If(useAnd, SqlUtil.[AND], SqlUtil.[OR])
            Me.m_ignoreCase = ignoreCase
        End Sub

#End Region

        '#Region "Append"

        '        ''' <summary>
        '        ''' Appends the specified column and value to the current filter.
        '        ''' </summary>
        '        ''' <param name="column"></param>
        '        ''' <param name="searchText"></param>
        '        ''' <returns></returns>
        '        Public Overridable Function Append(column As String, searchText As String) As SqlQueryBuilder
        '            Return Append(Me.m_junction, column, searchText, Me.m_ignoreCase)
        '        End Function

        '        ''' <summary>
        '        ''' Appends the specified column and search text to the current filter.
        '        ''' </summary>
        '        ''' <param name="column"></param>
        '        ''' <param name="searchText"></param>
        '        ''' <param name="ignoreCase"></param>
        '        ''' <returns></returns>
        '        Public Overridable Function Append(column As String, searchText As String, ignoreCase As Boolean) As SqlQueryBuilder
        '            Return Append(Me.m_junction, column, searchText, ignoreCase)
        '        End Function

        '        ''' <summary>
        '        ''' Appends the specified column and search text to the current filter.
        '        ''' </summary>
        '        ''' <param name="junction"></param>
        '        ''' <param name="column"></param>
        '        ''' <param name="searchText"></param>
        '        ''' <param name="ignoreCase"></param>
        '        ''' <returns></returns>
        '        Public Overridable Function Append(junction As String, column As String, searchText As String, ignoreCase As Boolean) As SqlQueryBuilder
        '            If Not String.IsNullOrEmpty(searchText) Then
        '                Logging.Log.Fatal(Parse(column, searchText, ignoreCase))
        '                AppendInternal(junction, Parse(column, searchText, ignoreCase))
        '            End If

        '            Return Me
        '        End Function

        '#End Region

#Region "AppendEquals"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendEquals(column As String, value As String) As IQueryBuilder Implements IQuery.AppendEquals
            Return AppendEquals(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendEquals(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendEquals
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, column, "=", SqlUtil.Encode(value, True))
            End If

            Return Me
        End Function

#End Region

#Region "AppendNotEquals"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' as a NOT EQUALS expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotEquals(column As String, value As String) As IQueryBuilder Implements IQuery.AppendNotEquals
            Return AppendNotEquals(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter
        ''' as a NOT EQUALS expression.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotEquals(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendNotEquals
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, column, "<>", SqlUtil.Encode(value, True))
            End If

            Return Me
        End Function

#End Region

#Region "AppendIn"

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIn(column As String, values As String()) As IQueryBuilder Implements IQuery.AppendIn
            Return AppendIn(Me.m_junction, column, values, True)
        End Function

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <param name="encode"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIn(column As String, values As String(), encode As Boolean) As IQueryBuilder Implements IQuery.AppendIn
            Return AppendIn(Me.m_junction, column, values, encode)
        End Function

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIn(junction As String, column As String, values As String()) As IQueryBuilder Implements IQuery.AppendIn
            Return AppendIn(junction, column, values, True)
        End Function

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <param name="encode"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIn(junction As String, column As String, values As String(), encode As Boolean) As IQueryBuilder Implements IQuery.AppendIn
            If values IsNot Nothing AndAlso values.Length > 0 Then
                Dim sqlString As String = GetInQueryValues(values, encode)

                If Not String.IsNullOrEmpty(sqlString) Then
                    AppendInQuery(junction, column, sqlString)
                End If
            End If

            Return Me
        End Function

#End Region

#Region "AppendNotIn"

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' as a NOT IN expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotIn(column As String, values As String()) As IQueryBuilder Implements IQuery.AppendNotIn
            Return AppendNotIn(Me.m_junction, column, values, True)
        End Function

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' as a NOT IN expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <param name="encode"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotIn(column As String, values As String(), encode As Boolean) As IQueryBuilder Implements IQuery.AppendNotIn
            Return AppendNotIn(Me.m_junction, column, values, encode)
        End Function

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter.
        ''' as a NOT IN expression.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotIn(junction As String, column As String, values As String()) As IQueryBuilder Implements IQuery.AppendNotIn
            Return AppendNotIn(junction, column, values, True)
        End Function

        ''' <summary>
        ''' Appends the specified column and list of values to the current filter
        ''' as a NOT IN expression.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="values"></param>
        ''' <param name="encode"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotIn(junction As String, column As String, values As String(), encode As Boolean) As IQueryBuilder Implements IQuery.AppendNotIn
            If values IsNot Nothing AndAlso values.Length > 0 Then
                Dim sqlString As String = GetInQueryValues(values, encode)

                If Not String.IsNullOrEmpty(sqlString) Then
                    AppendNotInQuery(junction, column, sqlString)
                End If
            End If

            Return Me
        End Function

#End Region

#Region "AppendInQuery"

        ''' <summary>
        ''' Appends the specified sub-query to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="query"></param>
        ''' <returns></returns>
        Public Overridable Function AppendInQuery(column As String, query As String) As IQueryBuilder Implements IQuery.AppendInQuery
            Return AppendInQuery(Me.m_junction, column, query)
        End Function

        ''' <summary>
        ''' Appends the specified sub-query to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="query"></param>
        ''' <returns></returns>
        Public Overridable Function AppendInQuery(junction As String, column As String, query As String) As IQueryBuilder Implements IQuery.AppendInQuery
            If Not String.IsNullOrEmpty(query) Then
                AppendInternal(junction, column, "IN", "(" & query & ")")
            End If

            Return Me
        End Function

#End Region

#Region "AppendNotInQuery"

        ''' <summary>
        ''' Appends the specified sub-query to the current filter
        ''' as a NOT IN expression.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="query"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotInQuery(column As String, query As String) As IQueryBuilder Implements IQuery.AppendNotInQuery
            Return AppendNotInQuery(Me.m_junction, column, query)
        End Function

        ''' <summary>
        ''' Appends the specified sub-query to the current filter
        ''' as a NOT IN expression.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="query"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotInQuery(junction As String, column As String, query As String) As IQueryBuilder Implements IQuery.AppendNotInQuery
            If Not String.IsNullOrEmpty(query) Then
                AppendInternal(junction, column, "NOT IN", "(" & query & ")")
            End If

            Return Me
        End Function

#End Region

#Region "AppendRange"

        ''' <summary>
        ''' Appends the specified column and value range to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="from"></param>
        ''' <param name="to"></param>
        ''' <returns></returns>
        Public Overridable Function AppendRange(column As String, from As String, [to] As String) As IQueryBuilder Implements IQuery.AppendRange
            Return AppendRange(Me.m_junction, column, from, [to])
        End Function

        ''' <summary>
        ''' Appends the specified column and value range to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="from"></param>
        ''' <param name="to"></param>
        ''' <returns></returns>
        Public Overridable Function AppendRange(junction As String, column As String, from As String, [to] As String) As IQueryBuilder Implements IQuery.AppendRange
            If Not String.IsNullOrEmpty(from) OrElse Not String.IsNullOrEmpty([to]) Then
                Dim sb As New StringBuilder()

                If Not String.IsNullOrEmpty(from) Then
                    sb.AppendFormat("{0}.value >= {1}", column, SqlUtil.Encode(from, False))
                End If
                If Not String.IsNullOrEmpty(from) AndAlso Not String.IsNullOrEmpty([to]) Then
                    sb.AppendFormat(" {0} ", SqlUtil.[AND])
                End If
                If Not String.IsNullOrEmpty([to]) Then
                    sb.AppendFormat("{0}.value <= {1}", column, SqlUtil.Encode([to], False))
                End If

                AppendInternal(junction, sb.ToString())
            End If

            Return Me
        End Function

#End Region

#Region "AppendNotRange"

        ''' <summary>
        ''' Appends the specified column and value not in range to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="from"></param>
        ''' <param name="to"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotRange(column As String, from As String, [to] As String) As IQueryBuilder Implements IQuery.AppendNotRange
            Return AppendNotRange(Me.m_junction, column, from, [to])
        End Function

        ''' <summary>
        ''' Appends the specified column and value not in range to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="from"></param>
        ''' <param name="to"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotRange(junction As String, column As String, from As String, [to] As String) As IQueryBuilder Implements IQuery.AppendNotRange
            If Not String.IsNullOrEmpty(from) OrElse Not String.IsNullOrEmpty([to]) Then
                Dim sb As New StringBuilder()

                sb.Append("NOT (")
                If Not String.IsNullOrEmpty(from) Then
                    sb.AppendFormat("{0}.value >= {1}", column, SqlUtil.Encode(from, False))
                End If
                If Not String.IsNullOrEmpty(from) AndAlso Not String.IsNullOrEmpty([to]) Then
                    sb.AppendFormat(" {0} ", SqlUtil.[AND])
                End If
                If Not String.IsNullOrEmpty([to]) Then
                    sb.AppendFormat("{0}.value <= {1}", column, SqlUtil.Encode([to], False))
                End If
                sb.Append(")")

                AppendInternal(junction, sb.ToString())
            End If

            Return Me
        End Function

#End Region

#Region "AppendIsNull"

        ''' <summary>
        ''' Appends an IS NULL expression to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIsNull(column As String) As IQueryBuilder Implements IQuery.AppendIsNull
            Return AppendIsNull(Me.m_junction, column)
        End Function

        ''' <summary>
        ''' Appends an IS NULL expression to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIsNull(junction As String, column As String) As IQueryBuilder Implements IQuery.AppendIsNull
            AppendInternal(junction, SqlUtil.IsNull(column))
            Return Me
        End Function

#End Region

#Region "AppendIsNotNull"

        ''' <summary>
        ''' Appends an IS NOT NULL expression to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIsNotNull(column As String) As IQueryBuilder Implements IQuery.AppendIsNotNull
            Return AppendIsNotNull(Me.m_junction, column)
        End Function

        ''' <summary>
        ''' Appends an IS NOT NULL expression to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <returns></returns>
        Public Overridable Function AppendIsNotNull(junction As String, column As String) As IQueryBuilder Implements IQuery.AppendIsNotNull
            AppendInternal(junction, SqlUtil.IsNotNull(column))
            Return Me
        End Function

#End Region

#Region "AppendGreaterThan"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendGreaterThan(column As String, value As String) As IQueryBuilder Implements IQuery.AppendGreaterThan
            Return AppendGreaterThan(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendGreaterThan(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendGreaterThan
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, column, ">", SqlUtil.Encode(value, False))
            End If

            Return Me
        End Function

#End Region

#Region "AppendGreaterThanOrEqual"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendGreaterThanOrEqual(column As String, value As String) As IQueryBuilder Implements IQuery.AppendGreaterThanOrEqual
            Return AppendGreaterThanOrEqual(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendGreaterThanOrEqual(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendGreaterThanOrEqual
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, column, ">=", SqlUtil.Encode(value, False))
            End If

            Return Me
        End Function

#End Region

#Region "AppendLessThan"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendLessThan(column As String, value As String) As IQueryBuilder Implements IQuery.AppendLessThan
            Return AppendLessThan(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendLessThan(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendLessThan
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, column, "<", SqlUtil.Encode(value, False))
            End If

            Return Me
        End Function

#End Region

#Region "AppendLessThanOrEqual"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendLessThanOrEqual(column As String, value As String) As IQueryBuilder Implements IQuery.AppendLessThanOrEqual
            Return AppendLessThanOrEqual(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendLessThanOrEqual(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendLessThanOrEqual
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, column, "<=", SqlUtil.Encode(value, False))
            End If

            Return Me
        End Function

#End Region

#Region "AppendStartsWith"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendStartsWith(column As String, value As String) As IQueryBuilder Implements IQuery.AppendStartsWith
            Return AppendStartsWith(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendStartsWith(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendStartsWith
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, SqlUtil.StartsWith(column, value))
            End If

            Return Me
        End Function

#End Region

#Region "AppendEndsWith"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendEndsWith(column As String, value As String) As IQueryBuilder Implements IQuery.AppendEndsWith
            Return AppendEndsWith(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendEndsWith(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendEndsWith
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, SqlUtil.EndsWith(column, value))
            End If

            Return Me
        End Function

#End Region

#Region "AppendContains"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendContains(column As String, value As String) As IQueryBuilder Implements IQuery.AppendContains
            Return AppendContains(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendContains(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendContains
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, SqlUtil.Contains(column, value))
            End If

            Return Me
        End Function

#End Region

#Region "AppendNotContains"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotContains(column As String, value As String) As IQueryBuilder Implements IQuery.AppendNotContains
            Return AppendNotContains(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotContains(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendNotContains
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, SqlUtil.Contains(column, value))
            End If
            Return Me
        End Function

#End Region

#Region "AppendLike"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendLike(column As String, value As String) As IQueryBuilder Implements IQuery.AppendLike
            Return AppendLike(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendLike(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendLike
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, SqlUtil.[Like](column, value))
            End If

            Return Me
        End Function

#End Region

#Region "AppendNotLike"

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotLike(column As String, value As String) As IQueryBuilder Implements IQuery.AppendNotLike
            Return AppendNotLike(Me.m_junction, column, value)
        End Function

        ''' <summary>
        ''' Appends the specified column and value to the current filter.
        ''' </summary>
        ''' <param name="junction"></param>
        ''' <param name="column"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Overridable Function AppendNotLike(junction As String, column As String, value As String) As IQueryBuilder Implements IQuery.AppendNotLike
            If Not String.IsNullOrEmpty(value) Then
                AppendInternal(junction, SqlUtil.NotLike(column, value))
            End If

            Return Me
        End Function
#End Region

#Region "AppendInternal"

        ''' <summary>
        ''' Appends the SQL expression to the internal <see cref="StringBuilder"/>.
        ''' </summary>
        ''' <param name="junction">The junction.</param>
        ''' <param name="column">The column.</param>
        ''' <param name="compare">The compare.</param>
        ''' <param name="value">The value.</param>
        Protected Overridable Sub AppendInternal(junction As String, column As String, compare As String, value As String)
            AppendInternal(junction, String.Format("{0}.value {1} {2}", column, compare, value))
        End Sub

        ''' <summary>
        ''' Appends the SQL expression to the internal <see cref="StringBuilder"/>.
        ''' </summary>
        ''' <param name="junction">The junction.</param>
        ''' <param name="query">The query.</param>
        Protected Overridable Sub AppendInternal(junction As String, query As String)
            If Not String.IsNullOrEmpty(query) Then
#If DEBUG Then
                Dim [end] As [String] = System.Environment.NewLine
#Else
				Dim [end] As [String] = [String].Empty
#End If
                Dim format As [String] = If((sql.Length > 0), " {0} ({1}){2}", " ({1}){2}")
                sql.AppendFormat(format, junction, query, [end])
            End If
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Clears the internal string buffer.
        ''' </summary>
        Public Overridable Sub Clear() Implements IQuery.Clear
            sql.Length = 0
            _groupCount = 0
        End Sub

        ''' <summary>
        ''' Converts the value of this instance to a System.String.
        ''' </summary>
        Public Overrides Function ToString() As String Implements IQuery.ToString
            Return "(" & sql.ToString().TrimEnd() & ")"
        End Function

        ''' <summary>
        ''' Converts the value of this instance to a System.String and
        ''' prepends the specified junction if the expression is not empty.
        ''' </summary>
        Public Overridable Overloads Function ToString(junction As String) As String Implements IQuery.ToString
            If sql.Length > 0 Then
                Return New StringBuilder(" ").Append(junction).Append(" ").Append(ToString()).ToString()
            End If

            Return [String].Empty
        End Function

        'fix
        ' ''' <summary>
        ' ''' Parses the specified searchText to create a SQL filter expression.
        ' ''' </summary>
        ' ''' <param name="column"></param>
        ' ''' <param name="searchText"></param>
        ' ''' <param name="ignoreCase"></param>
        ' ''' <returns></returns>
        'Public Overridable Function Parse(column As String, searchText As String, ignoreCase As Boolean) As String
        '    Return New SqlExpressionParser(column, ignoreCase).Parse(searchText)
        'End Function

        ''' <summary>
        ''' Gets an encoded list of values for use with an IN clause.
        ''' </summary>
        ''' <param name="values">Comma string with the in parameters already in one string</param>
        ''' <param name="encode">tells whether or not to enclose the parameters with ' and to replace ' with '' within the parameters.</param>
        ''' <returns></returns>
        Public Overridable Function GetInQueryValues(values As String, encode As Boolean) As [String] Implements IQuery.GetInQueryValues
            If encode Then
                Dim split As [String]() = values.Split(","c)
                values = SqlUtil.Encode(split, encode)
            End If

            Return values
        End Function

        ''' <summary>
        ''' Gets an encoded list of values for use with an IN clause.
        ''' </summary>
        ''' <param name="values">String Array with the in parameters</param>
        ''' <param name="encode">tells whether or not to enclose the parameters with ' and to replace ' with '' within the parameters.</param>
        ''' <returns></returns>
        Public Overridable Function GetInQueryValues(values As String(), encode As Boolean) As [String] Implements IQuery.GetInQueryValues
            Dim inQuery As String = String.Empty

            inQuery = SqlUtil.Encode(values, encode)

            Return inQuery
        End Function

        ''' <summary>
        ''' Begins a new group of parameters by adding an open parenthesis "("
        ''' </summary>
        Public Overridable Sub BeginGroup() Implements IQuery.BeginGroup
            BeginGroup(Junction)
        End Sub

        ''' <summary>
        ''' Begins a new groups of parameters using the specified junction operator
        ''' </summary>
        ''' <param name="junction">The junction operator to be used</param>
        Public Overridable Sub BeginGroup(junction As String) Implements IQuery.BeginGroup
            If sql.Length > 0 Then
                sql.AppendFormat("{0} (", junction)
            Else
                sql.AppendFormat("(", junction)
            End If
            _groupCount += 1
        End Sub

        ''' <summary>
        ''' Ends a group of parameters by add a closing parenthesis ")"
        ''' </summary>
        Public Overridable Sub EndGroup() Implements IQuery.EndGroup
            If _groupCount > 0 Then
                sql.Append(")")
                _groupCount -= 1
            End If
        End Sub

        ''' <summary>
        ''' Makes sure that all groups have been ended (each call to BeginGroup has a corresponding EndGroup)
        ''' </summary>
        Friend Overridable Sub EnsureGroups()
            'while (_groupCount > 0)
            '            {
            '               EndGroup();
            '            }

        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' The Junction member variable.
        ''' </summary>
        Private m_junction As [String]

        ''' <summary>
        ''' Gets or sets the Junction property.
        ''' </summary>
        Public Overridable Property Junction() As [String] Implements IQuery.Junction
            Get
                Return m_junction
            End Get
            Set(value As [String])
                m_junction = value
            End Set
        End Property

        ''' <summary>
        ''' The IgnoreCase member variable.
        ''' </summary>
        Private m_ignoreCase As Boolean

        ''' <summary>
        ''' Gets or sets the IgnoreCase property.
        ''' </summary>
        Public Overridable Property IgnoreCase() As Boolean Implements IQuery.IgnoreCase
            Get
                Return m_ignoreCase
            End Get
            Set(value As Boolean)
                m_ignoreCase = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the length of the internal StringBuilder object.
        ''' </summary>
        ''' <value>The length.</value>
        Public Overridable Property Length() As Integer Implements IQuery.Length
            Get
                Return sql.Length
            End Get
            Set(value As Integer)
                sql.Length = value
            End Set
        End Property

#End Region

    End Class

End Namespace

