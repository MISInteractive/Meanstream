Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Public Class GroupManager
        Inherits AttributeEntityManager

        Private _entity As Group

        Sub New(ByRef attributeEntity As Group)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("group id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the group {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup)
            _entity.Description = entity.Description
            _entity.Name = entity.Name
            _entity.UserId = entity.UserId
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'delete all group contacts here
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("GroupId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts In entities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Delete(entity.Id)
            Next
            'delete main group
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup = Me.GetEntityFromDatasource()

            If _entity.Name Is Nothing Or _entity.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            If _entity.UserId = Nothing Then
                Throw New ArgumentNullException("userId is required")
            End If

            entity.Description = _entity.Description
            entity.Name = _entity.Name
            entity.UserId = _entity.UserId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.Update(entity)
        End Sub

        Public Function IsContactInGroup(ByVal contactId As Guid) As Boolean
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & contactId.ToString & " AND GroupId=" & _entity.Id.ToString)
            If entities.Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Sub AddContactToGroup(ByVal contactId As Guid)
            If Not IsContactInGroup(contactId) Then
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts
                entity.Id = Guid.NewGuid
                entity.ContactId = contactId
                entity.CreatedDate = Date.Now
                entity.GroupId = _entity.Id
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Insert(entity)
            End If
        End Sub

        Public Sub RemoveContactFromGroup(ByVal contactId As Guid)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & contactId.ToString & " AND GroupId=" & _entity.Id.ToString)
            If entities.Count > 0 Then
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Delete(entities(0))
            End If
        End Sub

        Public Sub RemoveAllContactsFromGroup()
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("GroupId=" & _entity.Id.ToString)
            If entities.Count > 0 Then
                For Each contact As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts In entities
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Delete(contact)
                Next
            End If
        End Sub

        Protected Friend Shared Function Create(ByVal group As Group) As Guid
            If group.Name Is Nothing Or group.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            If group.UserId = Nothing Then
                Throw New ArgumentNullException("userId is required")
            End If

            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup
            entity.Id = group.Id
            entity.Description = group.Description
            entity.Name = group.Name
            entity.UserId = group.UserId
            entity.CreatedDate = Date.Now
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In group.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

