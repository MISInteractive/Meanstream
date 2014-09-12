Imports Meanstream.Core.EntityModel
Imports Meanstream.Core.Query


Namespace Meanstream.Core.Repository

    ''' <summary>
    ''' Used to log unhandled exceptions 
    ''' </summary>
    Public Class Repository

        Private Shared service As RepositoryService = RepositoryService.Current


        Private Sub New()
        End Sub


#Region "Provider Property Methods"
        ''' <summary>
        ''' Get the default provider.
        ''' </summary>
        Public Shared ReadOnly Property DefaultProvider As IRepositoryProvider
            Get
                Return service.DefaultProvider
            End Get
        End Property


        ''' <summary>
        ''' Get the current provider.
        ''' </summary>
        Public Shared ReadOnly Property Provider As IRepositoryProvider
            Get
                Return service.Provider
            End Get
        End Property


        ''' <summary>
        ''' Get all providers.
        ''' </summary>
        Public Shared ReadOnly Property Providers As IDictionary(Of String, IRepositoryProvider)
            Get
                Return service.Providers
            End Get
        End Property
#End Region


#Region " Methods "
        Public Shared Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As T
            Return Provider.GetById(Of T)(id)
        End Function


        Public Shared Function GetById(ByVal id As Guid) As DataSet
            Return Provider.GetById(id)
        End Function


        Public Shared Function GetKeyValuesById(ByVal id As Guid) As List(Of Dictionary(Of String, Object))
            Return Provider.GetKeyValuesById(id)
        End Function


        Public Shared Function Find(Of T As {IEntity, New})(ByVal query As IQuery) As List(Of T)
            Return Provider.Find(Of T)(query)
        End Function


        Public Shared Function Find(ByVal type As String, ByVal query As IQuery) As DataSet
            Return Provider.Find(type, query)
        End Function


        Public Shared Function GetKeyValues(ByVal type As String, ByVal query As IQuery) As List(Of Dictionary(Of String, Object))
            Return Provider.GetKeyValues(type, query)
        End Function


        Public Shared Function Find(Of T As {IEntity, New})(ByVal whereClause As String) As List(Of T)
            Return Provider.Find(Of T)(whereClause)
        End Function


        Public Shared Function Find(ByVal type As String, ByVal whereClause As String) As DataSet
            Return Provider.Find(type, whereClause)
        End Function


        Public Shared Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object))
            Return Provider.GetKeyValues(type, whereClause)
        End Function


        Public Shared Sub Insert(ByVal entity As Dictionary(Of String, Object))
            Provider.Insert(entity)
        End Sub


        Public Shared Sub Insert(Of T As IEntity)(ByVal entity As T)
            Provider.Insert(Of T)(entity)
        End Sub


        Public Shared Sub Update(ByVal entity As Dictionary(Of String, Object))
            Provider.Update(entity)
        End Sub


        Public Shared Sub Update(Of T As IEntity)(ByVal entity As T)
            Provider.Update(Of T)(entity)
        End Sub


        Public Shared Sub Delete(ByVal id As Guid)
            Provider.Delete(id)
        End Sub

        Public Shared Sub DeleteField(ByVal type As String, ByVal name As String)
            Provider.DeleteField(type, name)
        End Sub

        Public Shared Sub AddField(ByVal type As String, ByVal name As String, ByVal defaultValue As Object)
            Provider.InsertField(type, name, defaultValue)
        End Sub

        Public Shared Sub RenameField(ByVal type As String, ByVal oldName As String, ByVal newName As String)
            Provider.RenameField(type, oldName, newName)
        End Sub

        Public Shared Sub Rename(ByVal oldType As String, ByVal newType As String)
            Provider.Rename(oldType, newType)
        End Sub
#End Region


    End Class
End Namespace
