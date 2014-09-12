Imports Meanstream.Portal.ComponentModel
Imports System.Data
Imports Meanstream.Core.EntityModel
Imports Meanstream.Core.Query

Namespace Meanstream.Core.Repository

    Public MustInherit Class RepositoryProvider
        Implements IRepositoryProvider


        Public Sub New()

        End Sub

        Public Shared Function Current() As RepositoryProvider
            'initialize provider
            Return ComponentFactory.GetComponent(Of RepositoryProvider)()
        End Function

        Public ReadOnly Property Settings() As IProviderSettings Implements IRepositoryProvider.Settings
            Get
                If _settings Is Nothing Then
                    Dim componentSettings = ComponentFactory.GetComponentSettings(Me.GetType.FullName)
                    _settings = New ProviderSettings
                    For Each key As String In componentSettings.Keys
                        _settings.Settings.Add(key, componentSettings(key))
                    Next
                End If

                Return _settings
            End Get
        End Property
        Private _settings As IProviderSettings

        ''' <summary>
        ''' Get the provider name 
        ''' </summary>
        Public Overridable ReadOnly Property Name() As String Implements IRepositoryProvider.Name
            Get
                Return Me.GetType.FullName
            End Get
        End Property
        
        Public MustOverride Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As T Implements IRepositoryProvider.GetById
        Public MustOverride Function GetById(ByVal id As Guid) As DataSet Implements IRepositoryProvider.GetById
        Public MustOverride Function GetKeyValuesById(ByVal id As Guid) As List(Of Dictionary(Of String, Object)) Implements IRepositoryProvider.GetKeyValuesById
        Public MustOverride Function Find(Of T As {IEntity, New})(ByVal whereClause As String) As List(Of T) Implements IRepositoryProvider.Find
        Public MustOverride Function Find(Of T As {IEntity, New})(ByVal query As IQuery) As List(Of T) Implements IRepositoryProvider.Find
        Public MustOverride Function Find(ByVal type As String, ByVal whereClause As String) As DataSet Implements IRepositoryProvider.Find
        Public MustOverride Function Find(ByVal type As String, ByVal query As IQuery) As DataSet Implements IRepositoryProvider.Find
        Public MustOverride Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object)) Implements IRepositoryProvider.GetKeyValues
        Public MustOverride Function GetKeyValues(ByVal type As String, ByVal query As IQuery) As List(Of Dictionary(Of String, Object)) Implements IRepositoryProvider.GetKeyValues
        Public MustOverride Sub Insert(ByVal entity As Dictionary(Of String, Object)) Implements IRepositoryProvider.Insert
        Public MustOverride Sub Insert(Of T As IEntity)(ByVal entity As T) Implements IRepositoryProvider.Insert
        Public MustOverride Sub InsertField(ByVal type As String, ByVal name As String, ByVal defaultValue As Object) Implements IRepositoryProvider.InsertField
        Public MustOverride Sub Update(ByVal entity As Dictionary(Of String, Object)) Implements IRepositoryProvider.Update
        Public MustOverride Sub Update(Of T As IEntity)(ByVal entity As T) Implements IRepositoryProvider.Update
        Public MustOverride Sub Delete(ByVal id As Guid) Implements IRepositoryProvider.Delete
        Public MustOverride Sub DeleteField(ByVal type As String, ByVal name As String) Implements IRepositoryProvider.DeleteField
        Public MustOverride Sub Rename(ByVal oldType As String, ByVal newType As String) Implements IRepositoryProvider.Rename
        Public MustOverride Sub RenameField(ByVal type As String, ByVal oldName As String, ByVal newName As String) Implements IRepositoryProvider.RenameField

    End Class

End Namespace


