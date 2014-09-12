Imports Meanstream.Portal.ComponentModel

Namespace Meanstream.Portal.Core.Services.Data

    Public MustInherit Class DataProvider
        Public Sub New()

        End Sub

        Public Shared Function Current() As DataProvider
            'initialize provider
            Return ComponentFactory.GetComponent(Of DataProvider)()
        End Function

        Public ReadOnly Property Settings() As Dictionary(Of String, String)
            Get
                Return ComponentFactory.GetComponentSettings(Me.GetType.FullName)
            End Get
        End Property

        Public MustOverride Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As List(Of T)
        Public MustOverride Function GetById(ByVal id As Guid) As DataSet
        Public MustOverride Function GetKeyValuesById(ByVal id As Guid) As List(Of Dictionary(Of String, Object))
        Public MustOverride Function Find(Of T As {IEntity, New})(ByVal type As String, ByVal whereClause As String) As List(Of T)
        Public MustOverride Function Find(ByVal type As String, ByVal whereClause As String) As DataSet
        Public MustOverride Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object))
        Public MustOverride Sub Insert(ByVal type As String, ByVal entity As Dictionary(Of String, Object))
        Public MustOverride Sub Insert(Of T As IEntity)(ByVal type As String, ByVal entity As T)
        Public MustOverride Sub InsertColumn(ByVal type As String, ByVal name As String, ByVal defaultValue As String)
        Public MustOverride Sub Update(ByVal entity As Dictionary(Of String, Object))
        Public MustOverride Sub Update(Of T As IEntity)(ByVal entity As T)
        Public MustOverride Sub Delete(ByVal id As Guid)
        Public MustOverride Sub DeleteColumn(ByVal type As String, ByVal name As String)
        Public MustOverride Sub Rename(ByVal oldType As String, ByVal newType As String)
        Public MustOverride Sub RenameColumn(ByVal type As String, ByVal oldName As String, ByVal newName As String)
    End Class
End Namespace


