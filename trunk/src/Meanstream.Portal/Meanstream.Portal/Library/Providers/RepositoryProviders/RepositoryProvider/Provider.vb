Imports Meanstream.Core.Data
Imports Meanstream.Core.Repository
Imports Meanstream.Core.Query

Namespace Meanstream.Core.Providers.RepositoryProvider

    Public Class Provider
        Inherits Meanstream.Core.Repository.RepositoryProvider


        Private dataProvider As DataProvider = DataService.Current.Provider


        Public Overrides Sub Delete(ByVal id As System.Guid)
            dataProvider.Delete(id)
        End Sub


        Public Overrides Sub DeleteField(ByVal type As String, ByVal name As String)
            dataProvider.DeleteField(type, name)
        End Sub


        Public Overloads Overrides Function Find(ByVal type As String, ByVal whereClause As String) As System.Data.DataSet
            Return dataProvider.Find(type, whereClause)
        End Function


        Public Overloads Overrides Function Find(ByVal type As String, ByVal query As IQuery) As System.Data.DataSet
            Return dataProvider.Find(type, query)
        End Function


        Public Overloads Overrides Function Find(Of T As {New, Core.EntityModel.IEntity})(ByVal whereClause As String) As System.Collections.Generic.List(Of T)
            Return dataProvider.Find(Of T)(whereClause)
        End Function


        Public Overloads Overrides Function Find(Of T As {New, Core.EntityModel.IEntity})(ByVal query As IQuery) As System.Collections.Generic.List(Of T)
            Return dataProvider.Find(Of T)(query)
        End Function


        Public Overloads Overrides Function GetById(ByVal id As System.Guid) As System.Data.DataSet
            Return dataProvider.GetById(id)
        End Function


        Public Overloads Overrides Function GetById(Of T As {New, Core.EntityModel.IEntity})(ByVal id As System.Guid) As T
            Return dataProvider.GetById(Of T)(id)
        End Function


        Public Overrides Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As System.Collections.Generic.List(Of System.Collections.Generic.Dictionary(Of String, Object))
            Return dataProvider.GetKeyValues(type, whereClause)
        End Function


        Public Overrides Function GetKeyValues(ByVal type As String, ByVal query As IQuery) As System.Collections.Generic.List(Of System.Collections.Generic.Dictionary(Of String, Object))
            Return dataProvider.GetKeyValues(type, query)
        End Function


        Public Overrides Function GetKeyValuesById(ByVal id As System.Guid) As System.Collections.Generic.List(Of System.Collections.Generic.Dictionary(Of String, Object))
            Return dataProvider.GetKeyValuesById(id)
        End Function


        Public Overloads Overrides Sub Insert(ByVal entity As System.Collections.Generic.Dictionary(Of String, Object))
            dataProvider.Insert(entity)
        End Sub


        Public Overloads Overrides Sub Insert(Of T As Core.EntityModel.IEntity)(ByVal entity As T)
            dataProvider.Insert(Of T)(entity)
        End Sub


        Public Overrides Sub InsertField(ByVal type As String, ByVal name As String, ByVal defaultValue As Object)
            dataProvider.InsertField(type, name, defaultValue)
        End Sub


        Public Overrides Sub Rename(ByVal oldType As String, ByVal newType As String)
            dataProvider.Rename(oldType, newType)
        End Sub


        Public Overrides Sub RenameField(ByVal type As String, ByVal oldName As String, ByVal newName As String)
            dataProvider.RenameField(type, oldName, newName)
        End Sub


        Public Overloads Overrides Sub Update(ByVal entity As System.Collections.Generic.Dictionary(Of String, Object))
            dataProvider.Update(entity)
        End Sub


        Public Overloads Overrides Sub Update(Of T As Core.EntityModel.IEntity)(ByVal entity As T)
            dataProvider.Update(Of T)(entity)
        End Sub

    End Class

End Namespace


