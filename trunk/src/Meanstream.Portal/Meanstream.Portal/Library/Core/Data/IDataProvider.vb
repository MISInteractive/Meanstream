Imports Meanstream.Core.EntityModel
Imports System.Data
Imports Meanstream.Core.Query

Namespace Meanstream.Core.Data

    Public Interface IDataProvider

        ReadOnly Property Name As String
        ReadOnly Property Settings() As IProviderSettings

        '****** Repository methods ******'
        Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As T
        Function GetById(ByVal id As Guid) As DataSet
        Function GetKeyValuesById(ByVal id As Guid) As List(Of Dictionary(Of String, Object))
        Function Find(Of T As {IEntity, New})(ByVal whereClause As String) As List(Of T)
        Function Find(ByVal type As String, ByVal whereClause As String) As DataSet
        Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object))

        Function Find(Of T As {IEntity, New})(ByVal query As IQuery) As List(Of T)
        Function Find(ByVal type As String, ByVal query As IQuery) As DataSet
        Function GetKeyValues(ByVal type As String, ByVal query As IQuery) As List(Of Dictionary(Of String, Object))

        Sub Insert(ByVal entity As Dictionary(Of String, Object))
        Sub Insert(Of T As IEntity)(ByVal entity As T)
        Sub InsertField(ByVal type As String, ByVal name As String, ByVal defaultValue As Object)
        Sub Update(ByVal entity As Dictionary(Of String, Object))
        Sub Update(Of T As IEntity)(ByVal entity As T)
        Sub Delete(ByVal id As Guid)
        Sub DeleteField(ByVal type As String, ByVal name As String)
        Sub Rename(ByVal oldType As String, ByVal newType As String)
        Sub RenameField(ByVal type As String, ByVal oldName As String, ByVal newName As String)

    End Interface

End Namespace

