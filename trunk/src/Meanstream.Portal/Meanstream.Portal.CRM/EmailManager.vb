Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Friend Class EmailManager
        Inherits AttributeEntityManager

        Private _entity As Email

        Sub New(ByRef attributeEntity As Email)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("email id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the email {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails)
            _entity.CampaignId = entity.CampaignId.GetValueOrDefault
            _entity.Participant = System.Activator.CreateInstance(Type.GetType(entity.ParticipantType), entity.ParticipantId)
            _entity.UserId = entity.UserId
            _entity.CreatedDate = entity.CreatedDate
            _entity.LastSentDate = entity.LastSentDate.GetValueOrDefault
            _entity.IsCanceled = entity.IsCanceled
            _entity.IsCompleted = entity.IsCompleted
            _entity.IsExactTime = entity.IsExactTime
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Notes = entity.Notes
            _entity.Occurance = entity.Occurance
            _entity.Purpose = entity.Purpose
            _entity.AccountId = entity.AccountId
            _entity.ScheduledDateTime = entity.ScheduledDateTime
            _entity.Body = entity.Body
            _entity.CampaignStepId = entity.CampaignStepId.GetValueOrDefault
            _entity.ReferenceId = entity.ReferenceId.GetValueOrDefault
            _entity.Signature = entity.Signature
            _entity.StoredEmailId = entity.StoredEmailId.GetValueOrDefault
            _entity.StoredSignatureId = entity.StoredSignatureId.GetValueOrDefault
            _entity.Subject = entity.Subject
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails = Me.GetEntityFromDatasource

            If _entity.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If _entity.Occurance Is Nothing Or _entity.Occurance.Trim = "" Then
                Throw New ArgumentNullException("occurance is required")
            End If
            If _entity.Purpose Is Nothing Or _entity.Purpose.Trim = "" Then
                Throw New ArgumentNullException("purpose is required")
            End If
            If _entity.AccountId = Nothing Then
                Throw New ArgumentNullException("account is required")
            End If
            If _entity.UserId = Nothing Then
                Throw New ArgumentNullException("user is required")
            End If
            If _entity.ScheduledDateTime = Nothing Then
                Throw New ArgumentNullException("scheduled date and time is required")
            End If

            Dim participant As Object = _entity.Participant
            Dim type As String = participant.GetType().FullName

            entity.CampaignId = _entity.CampaignId
            entity.ParticipantId = _entity.Participant.Id
            entity.ParticipantType = type
            If _entity.LastSentDate <> Nothing Then
                entity.LastSentDate = _entity.LastSentDate
            End If
            entity.IsCanceled = _entity.IsCanceled
            entity.IsCompleted = _entity.IsCompleted
            entity.IsExactTime = _entity.IsExactTime
            entity.LastModifiedDate = Date.Now
            entity.Notes = _entity.Notes
            entity.Occurance = _entity.Occurance
            entity.Purpose = _entity.Purpose
            entity.AccountId = _entity.AccountId
            entity.UserId = _entity.UserId
            entity.ScheduledDateTime = _entity.ScheduledDateTime
            entity.Body = _entity.Body
            entity.CampaignStepId = _entity.CampaignStepId
            entity.ReferenceId = _entity.ReferenceId
            entity.Signature = _entity.Signature
            entity.StoredEmailId = _entity.StoredEmailId
            entity.StoredSignatureId = _entity.StoredSignatureId
            entity.Subject = _entity.Subject
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Delete(_entity.Id)
        End Sub

        Public Shared Function Create(ByVal email As Email) As Guid
            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails

            If email.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If email.Occurance Is Nothing Or email.Occurance.Trim = "" Then
                Throw New ArgumentNullException("occurance is required")
            End If
            If email.Purpose Is Nothing Or email.Purpose.Trim = "" Then
                Throw New ArgumentNullException("purpose is required")
            End If
            If email.AccountId = Nothing Then
                Throw New ArgumentNullException("account is required")
            End If
            If email.UserId = Nothing Then
                Throw New ArgumentNullException("user is required")
            End If
            If email.ScheduledDateTime = Nothing Then
                Throw New ArgumentNullException("scheduled date and time is required")
            End If

            Dim participant As Object = email.Participant
            Dim type As String = participant.GetType().FullName

            entity.Id = email.Id
            entity.CampaignId = email.CampaignId
            entity.CreatedDate = Date.Now
            entity.ParticipantId = participant.id
            entity.ParticipantType = type
            'entity.LastSentDate = email.LastSentDate
            entity.IsCanceled = False
            entity.IsCompleted = False
            entity.IsExactTime = email.IsExactTime
            entity.LastModifiedDate = Date.Now
            entity.Notes = email.Notes
            entity.Occurance = email.Occurance
            entity.Purpose = email.Purpose
            entity.AccountId = email.AccountId
            entity.UserId = email.UserId
            entity.ScheduledDateTime = email.ScheduledDateTime
            entity.Body = email.Body
            entity.CampaignStepId = email.CampaignStepId
            entity.ReferenceId = email.ReferenceId
            entity.Signature = email.Signature
            entity.StoredEmailId = email.StoredEmailId
            entity.StoredSignatureId = email.StoredSignatureId
            entity.Subject = email.Subject
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In email.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

