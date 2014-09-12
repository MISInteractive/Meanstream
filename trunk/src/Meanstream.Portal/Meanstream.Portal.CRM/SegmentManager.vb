Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Public Class SegmentManager
        Inherits AttributeEntityManager

        Private _entity As Segment

        Sub New(ByRef attributeEntity As Segment)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("segment id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the segment {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment)
            _entity.Description = entity.Description
            _entity.Name = entity.Name
            _entity.UserId = entity.UserId
            _entity.Summary = entity.Summary
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'delete all segment contacts here
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("SegmentId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts In entities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Delete(entity.Id)
            Next
            'delete main segment
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment = Me.GetEntityFromDatasource()
            
            If _entity.Name Is Nothing Or _entity.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            If _entity.UserId = Nothing Then
                Throw New ArgumentNullException("userId is required")
            End If

            entity.Description = _entity.Description
            entity.Name = _entity.Name
            entity.UserId = _entity.UserId
            entity.Summary = _entity.Summary
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentProvider.Update(entity)
        End Sub

        Public Function IsContactInSegment(ByVal contactId As Guid) As Boolean
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & contactId.ToString & " AND SegmentId=" & _entity.Id.ToString)
            If entities.Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Sub AddContactToSegment(ByVal contactId As Guid)
            If Not IsContactInSegment(contactId) Then
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts
                entity.Id = Guid.NewGuid
                entity.ContactId = contactId
                entity.CreatedDate = Date.Now
                entity.SegmentId = _entity.Id
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Insert(entity)
            End If
        End Sub

        Public Sub RemoveContactFromSegment(ByVal contactId As Guid)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & contactId.ToString & " AND SegmentId=" & _entity.Id.ToString)
            If entities.Count > 0 Then
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Delete(entities(0))
            End If
        End Sub

        Public Sub RemoveAllContactsFromSegment()
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("SegmentId=" & _entity.Id.ToString)
            If entities.Count > 0 Then
                For Each contact As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts In entities
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Delete(contact)
                Next
            End If
        End Sub

        Protected Friend Shared Function Create(ByVal segment As Segment) As Guid
            If segment.Name Is Nothing Or segment.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            If segment.UserId = Nothing Then
                Throw New ArgumentNullException("userId is required")
            End If

            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment
            entity.Id = segment.Id
            entity.Description = segment.Description
            entity.Name = segment.Name
            entity.UserId = segment.UserId
            entity.CreatedDate = Date.Now
            entity.Summary = segment.Summary
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In segment.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

