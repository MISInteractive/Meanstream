Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Public Class CampaignManager
        Inherits AttributeEntityManager

        Private _entity As Campaign

        Sub New(ByRef attributeEntity As Campaign)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("campaign id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the campaign {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns)
            _entity.IsActive = entity.IsActive
            '_entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Summary = entity.Summary
            _entity.Description = entity.Description
            _entity.Name = entity.Name
            _entity.UserId = entity.UserId
            _entity.ReminderDays = entity.ReminderDays
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns = Me.GetEntityFromDatasource
            entity.IsActive = _entity.IsActive
            entity.LastModifiedDate = Date.Now
            entity.Summary = _entity.Summary
            entity.Description = _entity.Description
            entity.Name = _entity.Name
            entity.UserId = _entity.UserId
            entity.ReminderDays = _entity.ReminderDays
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignsProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()

            Dim steps As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignSteps) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignStepsProvider.Find("CampaignId=" & _entity.Id.ToString)
            For Each CampaignStep As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignSteps In steps
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignStepsProvider.Delete(CampaignStep.Id)
            Next

            Dim participants As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignParticipants) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignParticipantsProvider.Find("CampaignId=" & _entity.Id.ToString)
            For Each participant As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignParticipants In participants
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignParticipantsProvider.Delete(participant.Id)
            Next

            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignsProvider.Delete(_entity.Id)
        End Sub

        Public Function IsParticipantInCampaign(ByVal participantId As Guid) As Boolean
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignParticipants) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignParticipantsProvider.Find("ParticipantId=" & participantId.ToString & " AND CampaignId=" & _entity.Id.ToString)
            If entities.Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Sub AddParticipantToSegment(ByVal participant As Participant)
            If Not IsParticipantInCampaign(participant.Id) Then
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignParticipants
                entity.Id = Guid.NewGuid
                entity.ParticipantId = participant.Id
                entity.ParticipantType = participant.GetType.FullName
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignParticipantsProvider.Insert(entity)
            End If
        End Sub

        Protected Friend Shared Function Create(ByVal campaign As Campaign) As Guid
            If campaign.Name Is Nothing Or campaign.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns
            entity.Id = campaign.Id
            entity.IsActive = campaign.IsActive
            entity.LastModifiedDate = Date.Now
            entity.CreatedDate = Date.Now
            entity.Summary = campaign.Summary
            entity.Description = campaign.Description
            entity.Name = campaign.Name
            entity.UserId = campaign.UserId
            entity.ReminderDays = campaign.ReminderDays

            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignsProvider.Insert(entity)

            'If campaign.Steps IsNot Nothing Then
            '    For Each campaignStep As Meanstream.Portal.CRM.CampaignStep In campaign.Steps
            '        Dim entityStep As New Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaignSteps
            '        entityStep.CampaignId = campaign.Id
            '        entityStep.CreatedDate = Date.Now
            '        entityStep.DisplayOrder = campaignStep.DisplayOrder
            '        entityStep.Id = campaignStep.Id
            '        entityStep.CampaignTaskType = campaignStep.Task.GetType.FullName

            '        If campaignStep.Task.GetType.FullName = "Meanstream.Portal.CRM.Appointment" Then
            '            Dim task As Appointment = System.Activator.CreateInstance(campaignStep.Task.GetType, campaignStep.Task.Id)
            '            task.CampaignId = campaign.Id
            '            CRMService.Current.ScheduleAppointment(task)
            '            entityStep.IsComplete = task.IsCompleted
            '            entityStep.CampaignTaskId = task.Id
            '        ElseIf campaignStep.Task.GetType.FullName = "Meanstream.Portal.CRM.Calls" Then
            '            Dim task As Calls = System.Activator.CreateInstance(campaignStep.Task.GetType, campaignStep.Task.Id)
            '            task.CampaignId = campaign.Id
            '            CRMService.Current.ScheduleCall(task)
            '            entityStep.IsComplete = task.IsCompleted
            '            entityStep.CampaignTaskId = task.Id
            '        ElseIf campaignStep.Task.GetType.FullName = "Meanstream.Portal.CRM.Email" Then
            '            Dim task As Email = System.Activator.CreateInstance(campaignStep.Task.GetType, campaignStep.Task.Id)
            '            task.CampaignId = campaign.Id
            '            CRMService.Current.ScheduleEmail(task)
            '            entityStep.IsComplete = task.IsCompleted
            '            entityStep.CampaignTaskId = task.Id
            '        ElseIf campaignStep.Task.GetType.FullName = "Meanstream.Portal.CRM.Mail" Then
            '            Dim task As Mail = System.Activator.CreateInstance(campaignStep.Task.GetType, campaignStep.Task.Id)
            '            task.CampaignId = campaign.Id
            '            CRMService.Current.ScheduleMail(task)
            '            entityStep.IsComplete = task.IsCompleted
            '            entityStep.CampaignTaskId = task.Id
            '        ElseIf campaignStep.Task.GetType.FullName = "Meanstream.Portal.CRM.Task" Then
            '            Dim task As Task = System.Activator.CreateInstance(campaignStep.Task.GetType, campaignStep.Task.Id)
            '            task.CampaignId = campaign.Id
            '            CRMService.Current.ScheduleTask(task)
            '            entityStep.IsComplete = task.IsCompleted
            '            entityStep.CampaignTaskId = task.Id
            '        End If

            '        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignStepsProvider.Insert(entityStep)
            '    Next
            'End If

            Return campaign.Id
        End Function
    End Class
End Namespace

