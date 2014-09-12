Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Public Class ContactManager
        Inherits AttributeEntityManager

        Private _entity As Contact

        Sub New(ByRef attributeEntity As Contact)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("contact id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmContactsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the contact {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts)
            _entity.Address = entity.Address
            _entity.Address2 = entity.Address2
            _entity.City = entity.City
            _entity.CreatedDate = entity.CreatedDate
            _entity.Email = entity.Email
            _entity.FirstName = entity.FirstName
            _entity.Flag = entity.Flag
            _entity.LastModifiedBy = entity.LastModifiedBy
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.LastName = entity.LastName
            _entity.Mobile = entity.Mobile
            _entity.Notes = entity.Notes
            _entity.Phone = entity.Phone
            _entity.State = entity.State
            _entity.Zip = entity.Zip
            _entity.UserId = entity.UserId
            _entity.BirthDate = entity.BirthDate.GetValueOrDefault
            _entity.Interest = entity.Interest
            _entity.Nickname = entity.Nickname
            _entity.Spouse = entity.Spouse
            _entity.Website = entity.Website
            _entity.Source = entity.Source
            _entity.SourceName = entity.SourceName
            _entity.SourceNotes = entity.SourceNotes
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.VwMeanstreamCrmGroupContacts)
            _entity.Address = entity.Address
            _entity.Address2 = entity.Address2
            _entity.City = entity.City
            _entity.CreatedDate = entity.CreatedDate
            _entity.Email = entity.Email
            _entity.FirstName = entity.FirstName
            _entity.Flag = entity.Flag
            _entity.LastModifiedBy = entity.LastModifiedBy
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.LastName = entity.LastName
            _entity.Mobile = entity.Mobile
            _entity.Notes = entity.Notes
            _entity.Phone = entity.Phone
            _entity.State = entity.State
            _entity.Zip = entity.Zip
            _entity.UserId = entity.UserId
            _entity.BirthDate = entity.BirthDate.GetValueOrDefault
            _entity.Interest = entity.Interest
            _entity.Nickname = entity.Nickname
            _entity.Spouse = entity.Spouse
            _entity.Website = entity.Website
            _entity.Source = entity.Source
            _entity.SourceName = entity.SourceName
            _entity.SourceNotes = entity.SourceNotes
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'delete all group contacts here
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts In entities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Delete(entity.Id)
            Next
            'delete all segment contacts here
            Dim sEntities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts In sEntities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Delete(entity.Id)
            Next
            'delete main contact
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmContactsProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts = Me.GetEntityFromDatasource()

            If _entity.FirstName Is Nothing Or _entity.FirstName.Trim = "" Then
                Throw New ArgumentNullException("first name is required")
            End If

            If _entity.UserId = Nothing Then
                Throw New ArgumentNullException("userId is required")
            End If

            entity.Address = _entity.Address
            entity.Address2 = _entity.Address2
            entity.City = _entity.City
            'entity.CreatedDate = _entity.CreatedDate
            entity.Email = _entity.Email
            entity.FirstName = _entity.FirstName
            entity.Flag = _entity.Flag
            entity.LastModifiedBy = _entity.UserId
            entity.LastModifiedDate = Date.Now
            entity.LastName = _entity.LastName
            entity.Mobile = _entity.Mobile
            entity.Notes = _entity.Notes
            entity.Phone = _entity.Phone
            entity.State = _entity.State
            entity.Zip = _entity.Zip
            entity.UserId = _entity.UserId
            Try
                entity.BirthDate = _entity.BirthDate
            Catch ex As Exception
            End Try
            entity.Interest = _entity.Interest
            entity.Nickname = _entity.Nickname
            entity.Spouse = _entity.Spouse
            entity.Website = _entity.Website
            entity.Source = _entity.Source
            entity.SourceName = _entity.SourceName
            entity.SourceNotes = _entity.SourceNotes
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmContactsProvider.Update(entity)
        End Sub

        Public Function IsContactInGroup(ByVal groupId As Guid) As Boolean
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & _entity.Id.ToString & " AND GroupId=" & groupId.ToString)
            If entities.Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Sub AddContactToGroup(ByVal groupId As Guid)
            If Not IsContactInGroup(groupId) Then
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts
                entity.Id = Guid.NewGuid
                entity.ContactId = _entity.Id
                entity.CreatedDate = Date.Now
                entity.GroupId = groupId
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Insert(entity)
            End If
        End Sub

        Public Sub RemoveContactFromGroup(ByVal groupId As Guid)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & _entity.Id.ToString & " AND GroupId=" & groupId.ToString)
            If entities.Count > 0 Then
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Delete(entities(0))
            End If
        End Sub

        Public Sub RemoveContactFromAllGroups()
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & _entity.Id.ToString)
            For Each Group As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts In entities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Delete(Group)
            Next
        End Sub

        Public Function IsContactInSegment(ByVal segmentId As Guid) As Boolean
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & _entity.Id.ToString & " AND SegmentId=" & segmentId.ToString)
            If entities.Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Sub RemoveContactFromSegment(ByVal segmentId As Guid)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & _entity.Id.ToString & " AND SegmentId=" & segmentId.ToString)
            If entities.Count > 0 Then
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Delete(entities(0))
            End If
        End Sub

        Public Sub RemoveContactFromAllSegments()
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & _entity.Id.ToString)
            For Each Segment As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts In entities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Delete(Segment)
            Next
        End Sub

        Public Sub AddContactToSegment(ByVal segmentId As Guid)
            If Not IsContactInSegment(segmentId) Then
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts
                entity.Id = Guid.NewGuid
                entity.ContactId = segmentId
                entity.CreatedDate = Date.Now
                entity.SegmentId = _entity.Id
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Insert(entity)
            End If
        End Sub

        Protected Friend Shared Function Create(ByVal contact As Contact) As Guid
            If contact.FirstName Is Nothing Or contact.FirstName.Trim = "" Then
                Throw New ArgumentNullException("first name is required")
            End If

            If contact.UserId = Nothing Then
                Throw New ArgumentNullException("userId is required")
            End If

            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts
            entity.Id = contact.Id
            entity.Address = contact.Address
            entity.Address2 = contact.Address2
            entity.City = contact.City
            entity.CreatedDate = Date.Now
            entity.Email = contact.Email
            entity.FirstName = contact.FirstName
            entity.Flag = contact.Flag
            entity.LastModifiedBy = contact.UserId
            entity.LastModifiedDate = Date.Now
            entity.LastName = contact.LastName
            entity.Mobile = contact.Mobile
            entity.Notes = contact.Notes
            entity.Phone = contact.Phone
            entity.State = contact.State
            entity.Zip = contact.Zip
            entity.UserId = contact.UserId
            Try
                entity.BirthDate = contact.BirthDate
            Catch ex As Exception
            End Try
            entity.Interest = contact.Interest
            entity.Nickname = contact.Nickname
            entity.Spouse = contact.Spouse
            entity.Website = contact.Website
            entity.Source = contact.Source
            entity.SourceName = contact.SourceName
            entity.SourceNotes = contact.SourceNotes
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmContactsProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In contact.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

