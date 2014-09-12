Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Services.Data
    Public Class RepositoryService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As RepositoryService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As RepositoryService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateServiceInstance = New RepositoryService(machineName, appFriendlyName)
                            _privateServiceInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privateServiceInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

#Region "Methods"
        Private Sub Initialize()

            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The repository service has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Repository Service initialized: ", AppFriendlyName, " #", ApplicationId))

        End Sub

        Public Sub Deinitialize()

            PortalTrace.WriteLine([String].Concat("Deinitialize Repository Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing

        End Sub

        'tested and works
        Public Sub Create(ByVal repository As Repository, ByVal items As List(Of Dictionary(Of String, Object)))

            If repository.Name.Trim = "" Then
                Throw New DataProviderException("repository name required")
            End If
            If repository.Fields.Count = 0 Then
                Throw New DataProviderException("repository fields required")
            End If

            Dim type As String = "Repository"
            repository._lastModifiedDate = Date.Now
            repository._createdDate = Date.Now
            repository._createdBy = "" 'context.Profile.UserName
            repository._lastModifiedBy = "" 'context.Profile.UserName

            Dim m_Repository As New Dictionary(Of String, Object)
            m_Repository.Add("Id", repository.Id)
            m_Repository.Add("name", repository.Name.Trim)
            'add meta
            m_Repository.Add("status", repository.Status)
            m_Repository.Add("description", repository.Description)
            m_Repository.Add("tags", repository.Tags)
            m_Repository.Add("createdDate", repository.CreatedDate)
            m_Repository.Add("lastModifiedDate", repository.lastModifiedDate)
            m_Repository.Add("createdBy", repository.CreatedBy)
            m_Repository.Add("lastModifiedBy", repository.LastModifiedBy)

            'field names
            Dim r_Fields As New Dictionary(Of String, Object)

            For Each Field As Field In repository.Fields
                r_Fields.Add(Field.Name, Field.Name)
            Next

            'insert items
            For Each item As Dictionary(Of String, Object) In items
                'create new row
                If Not item.ContainsKey("Id") Then
                    Throw New DataProviderException("id required for all items")
                End If

                'create row data
                Dim row As New Dictionary(Of String, Object)
                row.Add("Id", item("Id"))

                'new
                Dim index As Integer = 0
                For Each field As String In r_Fields.Values
                    index = Array.IndexOf(r_Fields.Values.ToArray, field)
                    row.Add(field, item(field))
                Next
                DataProvider.Current.Insert(repository.Id.ToString, row)
                'new
            Next

            'serialize fields
            Dim serializer As New Serializer(Of List(Of Field))
            Dim serializedFields As String = serializer.Serialize(repository.Fields)
            m_Repository.Add("fields", serializedFields)
            'create repository
            DataProvider.Current.Insert(type, m_Repository)

        End Sub

        Public Sub AddItems(ByVal id As Guid, ByVal items As List(Of Dictionary(Of String, Object)))

            Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
            'check exists
            If list.Count = 0 Then
                Throw New DataProviderException("repository does not exist")
            End If

            'field names
            Dim r_Fields As New Dictionary(Of String, Object)

            Dim fields As String = ""
            For Each Field As Field In list(0).Fields
                r_Fields.Add(Field.Name, Field.Name)
            Next

            'insert items
            For Each item As Dictionary(Of String, Object) In items
                'create new row
                If Not item.ContainsKey("Id") Then
                    Throw New DataProviderException("id required for all items")
                End If

                'create row data
                Dim row As New Dictionary(Of String, Object)
                row.Add("Id", item("Id"))

                'new
                Dim index As Integer = 0
                For Each field As String In r_Fields.Values
                    index = Array.IndexOf(r_Fields.Values.ToArray, field)
                    row.Add(field, item(field))
                Next
                DataProvider.Current.Insert(list(0).Id.ToString, row)
                'new
            Next

            'update properties
            list(0)._lastModifiedDate = Date.Now
            list(0)._lastModifiedBy = ""
            Me.Update(list(0))

        End Sub

        Public Sub DeleteItems(ByVal id As Guid, ByVal ids As List(Of Guid))

            Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
            'check exists
            If list.Count = 0 Then
                Throw New DataProviderException("repository does not exist")
            End If

            For Each itemId As Guid In ids
                DataProvider.Current.Delete(itemId)
            Next

            'update properties
            list(0)._lastModifiedDate = Date.Now
            list(0)._lastModifiedBy = ""
            Me.Update(list(0))

        End Sub

        Public Sub UpdateItems(ByVal id As Guid, ByVal items As List(Of Dictionary(Of String, Object)))

            Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
            'check exists
            If list.Count = 0 Then
                Throw New DataProviderException("repository does not exist")
            End If

            For Each item As Dictionary(Of String, Object) In items
                If item.ContainsKey("id") And Not String.IsNullOrEmpty(item("id")) Then
                    Meanstream.Portal.Core.Services.Data.DataProvider.Current.Update(item)
                End If
            Next

            'update properties
            list(0)._lastModifiedDate = Date.Now
            list(0)._lastModifiedBy = ""
            Me.Update(list(0))

        End Sub

        Public Sub Update(ByVal repository As Repository)

            Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(repository.Id)
            'check exists
            If list.Count = 0 Then
                Throw New DataProviderException("repository does not exist")
            End If

            Dim updateItems As Boolean = False

            'get items
            Dim items As List(Of Dictionary(Of String, Object)) = DataProvider.Current.GetKeyValues(repository.Id.ToString, "")
            If items.Count > 0 Then
                updateItems = True
            End If

            ''get old fields from rep
            Dim oldFields As List(Of Field) = list(0).Fields

            ''get new fields from rep
            Dim newFields As List(Of Field) = repository.Fields

            ''delete from items and rep field column only if id doesn't exist any longer
            For Each column As Field In oldFields
                If Not newFields.Contains(column) Then
                    'delete from items
                    If updateItems Then
                        DataProvider.Current.DeleteColumn(repository.Id.ToString, column.Name)
                    End If
                End If
            Next

            'add to items and rep field column only if its new
            For Each column As Field In newFields
                If Not oldFields.Contains(column) Then
                    'add to items
                    If updateItems Then
                        DataProvider.Current.InsertColumn(repository.Id.ToString, column.Name, "")
                    End If
                End If
            Next

            For Each column As Field In newFields
                If oldFields.Contains(column) Then
                    Dim oldName As String = oldFields.Item(oldFields.IndexOf(column)).Name
                    If oldName <> column.Name Then
                        DataProvider.Current.RenameColumn(repository.Id.ToString, oldName, column.Name)
                    End If
                End If
            Next

            'update properties
            repository._lastModifiedDate = Date.Now
            repository._lastModifiedBy = ""

            Dim m_Repository As New Dictionary(Of String, Object)
            m_Repository.Add("Id", repository.Id)
            m_Repository.Add("name", repository.Name.Trim)
            'update meta
            m_Repository.Add("status", repository.Status)
            m_Repository.Add("description", repository.Description)
            m_Repository.Add("tags", repository.Tags)
            m_Repository.Add("createdDate", repository.CreatedDate)
            m_Repository.Add("lastModifiedDate", repository.lastModifiedDate)
            m_Repository.Add("createdBy", repository.CreatedBy)
            m_Repository.Add("lastModifiedBy", repository.LastModifiedBy)
            'serialize fields
            Dim serializer As New Serializer(Of List(Of Field))
            Dim serializedFields As String = serializer.Serialize(repository.Fields)
            m_Repository.Add("fields", serializedFields)
            'update repository meta
            DataProvider.Current.Update(m_Repository)

        End Sub

        'tested and works
        Public Sub Delete(ByVal id As Guid)

            Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
            'check exists
            If list.Count = 0 Then
                Throw New DataProviderException("repository does not exist")
            End If

            'check status
            If list(0).Status = Status.Locked Then
                Throw New DataProviderException("this repository cannot be deleted because it is locked")
            End If

            'delete items
            Dim ds As DataSet = DataProvider.Current.Find(id.ToString, "")
            For Each row As DataRow In ds.Tables(0).Rows
                DataProvider.Current.Delete(row("Id"))
            Next

            'delete repository
            DataProvider.Current.Delete(id)

            'future release - delete all backups

        End Sub

        Public Function GetItems(ByVal id As Guid, ByVal whereClause As String) As DataSet
            Return Meanstream.Portal.Core.Services.Data.DataProvider.Current.Find(id.ToString, whereClause)
        End Function

        Public Function GetItemsKeyValues(ByVal id As Guid, ByVal whereClause As String) As List(Of Dictionary(Of String, Object))
            Return Meanstream.Portal.Core.Services.Data.DataProvider.Current.GetKeyValues(id.ToString, whereClause)
        End Function

        Public Function GetById(ByVal id As Guid) As Repository
            Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
            If list.Count = 0 Then
                Return Nothing
            End If
            Return list(0)
        End Function

        Public Function GetByName(ByVal name As String) As Repository
            Dim list As List(Of Repository) = DataProvider.Current.Find(Of Repository)("Repository", "Name.value = '" & name & "'")
            If list.Count = 0 Then
                Return Nothing
            End If
            Return list(0)
        End Function

        Public Function Find(ByVal whereClause As String) As List(Of Repository)
            Return DataProvider.Current.Find(Of Repository)("Repository", whereClause)
        End Function

        'Public Sub SendToRecycleBin(ByVal id As Guid)

        '    'check exists
        '    Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
        '    If list.Count = 0 Then
        '        Throw New DataProviderException("repository does not exist")
        '    End If

        '    Dim repository As Repository = list(0)
        '    repository.Status = Status.Delete
        '    repository._lastModifiedDate = Date.Now
        '    repository._lastModifiedBy = ""
        '    DataProvider.Current.Update(repository)

        'End Sub

        'Public Sub RestoreFromRecycleBin(ByVal id As Guid)

        '    'check exists
        '    Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
        '    If list.Count = 0 Then
        '        Throw New DataProviderException("repository does not exist")
        '    End If

        '    Dim repository As Repository = list(0)
        '    repository.Status = Status.Active
        '    repository._lastModifiedDate = Date.Now
        '    repository._lastModifiedBy = ""
        '    DataProvider.Current.Update(repository)

        'End Sub

        'Public Function Copy(ByVal id As Guid) As Guid

        '    Dim updateItems As Boolean = True
        '    'make a copy of repository
        '    Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
        '    If list.Count = 0 Then
        '        Throw New DataProviderException("repository does not exist")
        '    End If

        '    'new guid and backup status
        '    Dim repository As Repository = list(0)
        '    repository.Id = Guid.NewGuid
        '    repository.Name = repository.Name & " - " & Date.Now

        '    'get items
        '    Dim items As List(Of Dictionary(Of String, Object)) = DataProvider.Current.GetKeyValues(id.ToString, "")
        '    'create
        '    Me.Create(repository, items)

        '    Return repository.Id

        'End Function

        'Public Sub RestoreFromBackup(ByVal id As Guid)

        '    Dim list As List(Of Repository) = DataProvider.Current.GetById(Of Repository)(id)
        '    If list.Count = 0 Then
        '        Throw New DataProviderException("repository to restore does not exist")
        '    End If

        '    Dim backup As Repository = list(0)

        '    If backup.Name.IndexOf("{") = -1 Or backup.Name.IndexOf("}") = -1 Then
        '        Throw New DataProviderException("invalid backup")
        '    End If

        '    'get guid
        '    Dim repId As Guid = New Guid(backup.Name.Substring(backup.Name.IndexOf("{") + 1, backup.Name.IndexOf("}") - 1))

        '    'make a copy of existing repository.
        '    list = DataProvider.Current.GetById(Of Repository)(repId)
        '    If list.Count = 0 Then
        '        Throw New DataProviderException("repository to backup does not exist")
        '    End If

        '    'rename backup repository

        'End Sub

#End Region

#Region " Properties "
        Private _appFriendlyName As String
        Public Property AppFriendlyName() As String
            Get
                Return _appFriendlyName
            End Get
            Private Set(ByVal value As String)
                _appFriendlyName = value
            End Set
        End Property

        Private _machineName As String
        Public Property MachineName() As String
            Get
                Return _machineName
            End Get
            Private Set(ByVal value As String)
                _machineName = value
            End Set
        End Property

        Private _applicationId As Guid
        Public Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
            Private Set(ByVal value As Guid)
                _applicationId = value
            End Set
        End Property
#End Region

#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Deinitialize()
        End Sub
#End Region

    End Class
End Namespace

