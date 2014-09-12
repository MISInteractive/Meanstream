Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Public Class CallManager
        Inherits AttributeEntityManager

        Private _entity As Calls

        Sub New(ByRef attributeEntity As Calls)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("call id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the call {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls)
            _entity.CampaignId = entity.CampaignId.GetValueOrDefault
            _entity.Participant = System.Activator.CreateInstance(Type.GetType(entity.ParticipantType), entity.ParticipantId)
            _entity.UserId = entity.UserId
            _entity.CreatedDate = entity.CreatedDate
            _entity.IsCanceled = entity.IsCanceled
            _entity.IsCompleted = entity.IsCompleted
            _entity.ScheduleFollowUp = entity.ScheduleFollowUp
            _entity.IsAppointment = entity.IsAppointment
            _entity.CallResult = entity.CallResult
            _entity.IsExactTime = entity.IsExactTime
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Notes = entity.Notes
            _entity.Occurance = entity.Occurance
            _entity.Summary = entity.Summary
            _entity.AccountId = entity.AccountId
            _entity.ScheduledDateTime = entity.ScheduledDateTime
            _entity.CampaignStepId = entity.CampaignStepId.GetValueOrDefault
            _entity.ReferenceId = entity.ReferenceId.GetValueOrDefault
            _entity.PhoneScript = entity.PhoneScript
            _entity.PhoneScriptId = entity.PhoneScriptId.GetValueOrDefault
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls = Me.GetEntityFromDatasource

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
            entity.CreatedDate = _entity.CreatedDate
            entity.IsCanceled = _entity.IsCanceled
            entity.IsCompleted = _entity.IsCompleted
            entity.ScheduleFollowUp = _entity.ScheduleFollowUp
            entity.IsAppointment = _entity.IsAppointment
            entity.CallResult = _entity.CallResult
            entity.IsExactTime = _entity.IsExactTime
            entity.LastModifiedDate = _entity.LastModifiedDate
            entity.Notes = _entity.Notes
            entity.Occurance = _entity.Occurance
            entity.Summary = _entity.Summary
            entity.AccountId = _entity.AccountId
            entity.ScheduledDateTime = _entity.ScheduledDateTime
            entity.UserId = _entity.UserId
            entity.CampaignStepId = _entity.CampaignStepId
            entity.ReferenceId = _entity.ReferenceId
            entity.PhoneScript = _entity.PhoneScript
            entity.PhoneScriptId = _entity.PhoneScriptId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Delete(_entity.Id)
        End Sub

        Public Shared Function Create(ByVal calls As Calls) As Guid
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls = New Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls

            If calls.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If calls.Occurance Is Nothing Or calls.Occurance.Trim = "" Then
                Throw New ArgumentNullException("occurance is required")
            End If
            If calls.AccountId = Nothing Then
                Throw New ArgumentNullException("account is required")
            End If
            If calls.UserId = Nothing Then
                Throw New ArgumentNullException("user is required")
            End If
            If calls.ScheduledDateTime = Nothing Then
                Throw New ArgumentNullException("scheduled date and time is required")
            End If

            Dim participant As Object = calls.Participant
            Dim type As String = participant.GetType().FullName

            entity.Id = calls.Id
            entity.UserId = calls.UserId
            entity.CampaignId = calls.CampaignId
            entity.ParticipantId = calls.Participant.Id
            entity.ParticipantType = type
            entity.CreatedDate = Date.Now
            entity.IsCanceled = False
            entity.IsCompleted = False
            entity.ScheduleFollowUp = calls.ScheduleFollowUp
            entity.IsAppointment = calls.IsAppointment
            entity.CallResult = ""
            entity.IsExactTime = calls.IsExactTime
            entity.LastModifiedDate = Date.Now
            entity.Notes = calls.Notes
            entity.Occurance = calls.Occurance
            entity.Summary = calls.Summary
            entity.AccountId = calls.AccountId
            entity.ScheduledDateTime = calls.ScheduledDateTime
            entity.CampaignStepId = calls.CampaignStepId
            entity.ReferenceId = calls.ReferenceId
            entity.PhoneScript = calls.PhoneScript
            entity.PhoneScriptId = calls.PhoneScriptId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In calls.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

