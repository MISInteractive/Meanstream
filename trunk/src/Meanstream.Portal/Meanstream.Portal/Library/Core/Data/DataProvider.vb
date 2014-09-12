Imports Meanstream.Portal.ComponentModel
Imports System.Data
Imports Meanstream.Core.EntityModel
Imports Meanstream.Core.Query

Namespace Meanstream.Core.Data

    Public MustInherit Class DataProvider
        Implements IDataProvider


        Public Sub New()

        End Sub

        Public Shared Function Current() As DataProvider
            'initialize provider
            Return ComponentFactory.GetComponent(Of DataProvider)()
        End Function

        Public ReadOnly Property Settings() As IProviderSettings Implements IDataProvider.Settings
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
        Public Overridable ReadOnly Property Name() As String Implements IDataProvider.Name
            Get
                Return Me.GetType.FullName
            End Get
        End Property


        '****** Repository methods ******'
        Public MustOverride Function GetById(Of T As {IEntity, New})(ByVal id As Guid) As T Implements IDataProvider.GetById
        Public MustOverride Function GetById(ByVal id As Guid) As DataSet Implements IDataProvider.GetById
        Public MustOverride Function GetKeyValuesById(ByVal id As Guid) As List(Of Dictionary(Of String, Object)) Implements IDataProvider.GetKeyValuesById
        Public MustOverride Function Find(Of T As {IEntity, New})(ByVal whereClause As String) As List(Of T) Implements IDataProvider.Find
        Public MustOverride Function Find(ByVal type As String, ByVal whereClause As String) As DataSet Implements IDataProvider.Find
        Public MustOverride Function GetKeyValues(ByVal type As String, ByVal whereClause As String) As List(Of Dictionary(Of String, Object)) Implements IDataProvider.GetKeyValues

        Public MustOverride Function Find(Of T As {IEntity, New})(ByVal query As IQuery) As List(Of T) Implements IDataProvider.Find
        Public MustOverride Function Find(ByVal type As String, ByVal query As IQuery) As DataSet Implements IDataProvider.Find
        Public MustOverride Function GetKeyValues(ByVal type As String, ByVal query As IQuery) As List(Of Dictionary(Of String, Object)) Implements IDataProvider.GetKeyValues

        Public MustOverride Sub Insert(ByVal entity As Dictionary(Of String, Object)) Implements IDataProvider.Insert
        Public MustOverride Sub Insert(Of T As IEntity)(ByVal entity As T) Implements IDataProvider.Insert
        Public MustOverride Sub InsertField(ByVal type As String, ByVal name As String, ByVal defaultValue As Object) Implements IDataProvider.InsertField
        Public MustOverride Sub Update(ByVal entity As Dictionary(Of String, Object)) Implements IDataProvider.Update
        Public MustOverride Sub Update(Of T As IEntity)(ByVal entity As T) Implements IDataProvider.Update
        Public MustOverride Sub Delete(ByVal id As Guid) Implements IDataProvider.Delete
        Public MustOverride Sub DeleteField(ByVal type As String, ByVal name As String) Implements IDataProvider.DeleteField
        Public MustOverride Sub Rename(ByVal oldType As String, ByVal newType As String) Implements IDataProvider.Rename
        Public MustOverride Sub RenameField(ByVal type As String, ByVal oldName As String, ByVal newName As String) Implements IDataProvider.RenameField

    End Class

End Namespace


