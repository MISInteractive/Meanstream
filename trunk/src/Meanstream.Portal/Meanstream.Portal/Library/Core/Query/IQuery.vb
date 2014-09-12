
Namespace Meanstream.Core.Query

    Public Interface IQuery

        Function AppendEquals(column As String, value As String) As IQueryBuilder
        Function AppendEquals(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendNotEquals(column As String, value As String) As IQueryBuilder
        Function AppendNotEquals(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendIn(column As String, values As String()) As IQueryBuilder
        Function AppendIn(column As String, values As String(), encode As Boolean) As IQueryBuilder
        Function AppendIn(junction As String, column As String, values As String()) As IQueryBuilder
        Function AppendIn(junction As String, column As String, values As String(), encode As Boolean) As IQueryBuilder
        Function AppendNotIn(column As String, values As String()) As IQueryBuilder
        Function AppendNotIn(column As String, values As String(), encode As Boolean) As IQueryBuilder
        Function AppendNotIn(junction As String, column As String, values As String()) As IQueryBuilder
        Function AppendNotIn(junction As String, column As String, values As String(), encode As Boolean) As IQueryBuilder
        Function AppendInQuery(column As String, query As String) As IQueryBuilder
        Function AppendInQuery(junction As String, column As String, query As String) As IQueryBuilder
        Function AppendNotInQuery(column As String, query As String) As IQueryBuilder
        Function AppendNotInQuery(junction As String, column As String, query As String) As IQueryBuilder
        Function AppendRange(column As String, from As String, [to] As String) As IQueryBuilder
        Function AppendRange(junction As String, column As String, from As String, [to] As String) As IQueryBuilder
        Function AppendNotRange(column As String, from As String, [to] As String) As IQueryBuilder
        Function AppendNotRange(junction As String, column As String, from As String, [to] As String) As IQueryBuilder
        Function AppendIsNull(column As String) As IQueryBuilder
        Function AppendIsNull(junction As String, column As String) As IQueryBuilder
        Function AppendIsNotNull(column As String) As IQueryBuilder
        Function AppendIsNotNull(junction As String, column As String) As IQueryBuilder
        Function AppendGreaterThan(column As String, value As String) As IQueryBuilder
        Function AppendGreaterThan(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendGreaterThanOrEqual(column As String, value As String) As IQueryBuilder
        Function AppendGreaterThanOrEqual(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendLessThan(column As String, value As String) As IQueryBuilder
        Function AppendLessThan(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendLessThanOrEqual(column As String, value As String) As IQueryBuilder
        Function AppendLessThanOrEqual(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendStartsWith(column As String, value As String) As IQueryBuilder
        Function AppendStartsWith(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendEndsWith(column As String, value As String) As IQueryBuilder
        Function AppendEndsWith(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendContains(column As String, value As String) As IQueryBuilder
        Function AppendContains(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendNotContains(column As String, value As String) As IQueryBuilder
        Function AppendNotContains(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendLike(column As String, value As String) As IQueryBuilder
        Function AppendLike(junction As String, column As String, value As String) As IQueryBuilder
        Function AppendNotLike(column As String, value As String) As IQueryBuilder
        Function AppendNotLike(junction As String, column As String, value As String) As IQueryBuilder

        Function ToString() As String
        Function ToString(junction As String) As String
        Sub Clear()
        Function GetInQueryValues(values As String, encode As Boolean) As [String]
        Function GetInQueryValues(values As String(), encode As Boolean) As [String]
        Sub BeginGroup()
        Sub BeginGroup(junction As String)
        Sub EndGroup()

        Property Junction() As [String]
        Property IgnoreCase() As Boolean
        Property Length() As Integer

    End Interface

End Namespace

