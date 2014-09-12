Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Friend Class AppointmentManager
        Inherits AttributeEntityManager

        Private _entity As Appointment

        Sub New(ByRef attributeEntity As Appointment)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("appointment id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAppointmentsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the appointment {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments)
            _entity.CampaignId = entity.CampaignId.GetValueOrDefault
            _entity.AccountId = entity.AccountId
            'If entity.ParticipantId <> Nothing Then
            '    _entity.Participant = System.Activator.CreateInstance(Type.GetType(entity.ParticipantType), entity.ParticipantId)
            'End If
            _entity.Participant = System.Activator.CreateInstance(Type.GetType(entity.ParticipantType), entity.ParticipantId)
            _entity.CreatedDate = entity.CreatedDate
            _entity.IsCanceled = entity.IsCanceled
            _entity.Address1 = entity.Address1
            _entity.Address2 = entity.Address2
            _entity.City = entity.City
            _entity.State = entity.State
            _entity.Zip = entity.Zip
            _entity.IsCompleted = entity.IsCompleted
            _entity.Location = entity.Location
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Notes = entity.Notes
            _entity.Summary = entity.Summary
            _entity.UserId = entity.UserId
            _entity.CampaignStepId = entity.CampaignStepId.GetValueOrDefault
            _entity.ReferenceId = entity.ReferenceId.GetValueOrDefault
            _entity.ScheduledDateTime = entity.ScheduledDateTime.GetValueOrDefault
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments = Me.GetEntityFromDatasource
            
            If _entity.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If _entity.Location Is Nothing Or _entity.Location.Trim = "" Then
                Throw New ArgumentNullException("location is required")
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
            entity.Address1 = _entity.Address1
            entity.Address2 = _entity.Address2
            entity.City = _entity.City
            entity.State = _entity.State
            entity.Zip = _entity.Zip
            entity.IsCompleted = _entity.IsCompleted
            entity.Location = _entity.Location
            entity.IsCanceled = _entity.IsCanceled
            entity.LastModifiedDate = Date.Now
            entity.Notes = _entity.Notes
            entity.Summary = _entity.Summary
            entity.AccountId = _entity.AccountId
            entity.UserId = _entity.UserId
            entity.ScheduledDateTime = _entity.ScheduledDateTime
            entity.CampaignStepId = _entity.CampaignStepId
            entity.ReferenceId = _entity.ReferenceId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAppointmentsProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Delete(_entity.Id)
        End Sub

        Public Shared Function Create(ByVal appointment As Appointment) As Guid
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments = New Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments

            If appointment.Participant Is Nothing Then
                Throw New ArgumentNullException("participant is required")
            End If
            If appointment.Location Is Nothing Or appointment.Location.Trim = "" Then
                Throw New ArgumentNullException("location is required")
            End If
            If appointment.AccountId = Nothing Then
                Throw New ArgumentNullException("account is required")
            End If
            If appointment.UserId = Nothing Then
                Throw New ArgumentNullException("user is required")
            End If
            If appointment.ScheduledDateTime = Nothing Then
                Throw New ArgumentNullException("scheduled date and time is required")
            End If

            Dim participant As Object = appointment.Participant
            Dim type As String = participant.GetType().FullName

            entity.Id = appointment.Id
            entity.UserId = appointment.UserId
            entity.CampaignId = appointment.CampaignId
            entity.ParticipantId = appointment.Participant.Id
            entity.ParticipantType = type
            entity.CreatedDate = Date.Now
            entity.Address1 = appointment.Address1
            entity.Address2 = appointment.Address2
            entity.City = appointment.City
            entity.State = appointment.State
            entity.Zip = appointment.Zip
            entity.IsCompleted = False
            entity.Location = appointment.Location
            entity.IsCanceled = False
            entity.LastModifiedDate = Date.Now
            entity.Notes = appointment.Notes
            entity.Summary = appointment.Summary
            entity.AccountId = appointment.AccountId
            entity.ScheduledDateTime = appointment.ScheduledDateTime
            entity.CampaignStepId = appointment.CampaignStepId
            entity.ReferenceId = appointment.ReferenceId
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAppointmentsProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In appointment.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

