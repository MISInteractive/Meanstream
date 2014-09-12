Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Friend Class MailManager
        Inherits AttributeEntityManager

        Private _entity As Mail

        Sub New(ByRef attributeEntity As Mail)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("mail id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the mail {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails)
            _entity.CampaignId = entity.CampaignId.GetValueOrDefault
            _entity.Participant = System.Activator.CreateInstance(Type.GetType(entity.ParticipantType), entity.ParticipantId)
            _entity.UserId = entity.UserId
            _entity.CreatedDate = entity.CreatedDate
            _entity.IsCanceled = entity.IsCanceled
            _entity.IsCompleted = entity.IsEnded
            _entity.IsExactTime = entity.IsExactTime
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Notes = entity.Notes
            _entity.Occurance = entity.Occurance
            _entity.Summary = entity.Summary
            _entity.AccountId = entity.AccountId
            _entity.ScheduledDateTime = entity.ScheduledDateTime
            _entity.CampaignStepId = entity.CampaignStepId.GetValueOrDefault
            _entity.ReferenceId = entity.ReferenceId.GetValueOrDefault
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails = Me.GetEntityFromDatasource

            If _entity.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If _entity.Occurance Is Nothing Or _entity.Occurance.Trim = "" Then
                Throw New ArgumentNullException("occurance is required")
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
            'entity.CreatedDate = _entity.CreatedDate
            entity.IsCanceled = _entity.IsCanceled
            entity.IsEnded = _entity.IsCompleted
            entity.IsExactTime = _entity.IsExactTime
            entity.LastModifiedDate = Date.Now
            entity.Notes = _entity.Notes
            entity.Occurance = _entity.Occurance
            entity.Summary = _entity.Summary
            entity.AccountId = _entity.AccountId
            entity.UserId = _entity.UserId
            entity.ScheduledDateTime = _entity.ScheduledDateTime
            entity.CampaignStepId = _entity.CampaignStepId
            entity.ReferenceId = _entity.ReferenceId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Delete(_entity.Id)
        End Sub

        Public Shared Function Create(ByVal mail As Mail) As Guid
            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmMails

            If mail.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If mail.Occurance Is Nothing Or mail.Occurance.Trim = "" Then
                Throw New ArgumentNullException("occurance is required")
            End If
            If mail.AccountId = Nothing Then
                Throw New ArgumentNullException("account is required")
            End If
            If mail.UserId = Nothing Then
                Throw New ArgumentNullException("user is required")
            End If
            If mail.ScheduledDateTime = Nothing Then
                Throw New ArgumentNullException("scheduled date and time is required")
            End If

            Dim participant As Object = mail.Participant
            Dim type As String = participant.GetType().FullName

            entity.Id = mail.Id
            entity.CampaignId = mail.CampaignId
            entity.ParticipantId = mail.Participant.Id
            entity.ParticipantType = type
            entity.CreatedDate = Date.Now
            entity.IsCanceled = False
            entity.IsEnded = False
            entity.IsExactTime = mail.IsExactTime
            entity.LastModifiedDate = Date.Now
            entity.Notes = mail.Notes
            entity.Occurance = mail.Occurance
            entity.Summary = mail.Summary
            entity.AccountId = mail.AccountId
            entity.UserId = mail.UserId
            entity.ScheduledDateTime = mail.ScheduledDateTime
            entity.CampaignStepId = mail.CampaignStepId
            entity.ReferenceId = mail.ReferenceId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In mail.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

