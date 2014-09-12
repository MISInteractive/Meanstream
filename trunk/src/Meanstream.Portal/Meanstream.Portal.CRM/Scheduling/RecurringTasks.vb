Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Services.Scheduling

Namespace Meanstream.Portal.CRM.Scheduling
    Public Class RecurringTasks
        Inherits Meanstream.Portal.Core.Services.Scheduling.Task

        Public Sub New(ByVal id As Guid, ByVal interval As Double, ByVal startupType As Core.Services.Scheduling.StartupType)
            MyBase.New(id, interval, startupType)
        End Sub

        Public Overrides Sub Execute()
            
           'get all task where occurance <> 'once' and scheduleDateTime has passed
            'update task date accordingly and save
            Me.scheduleCalls()
            Me.scheduleEmails()
            Me.scheduleMailing()
            Me.scheduleTasks()
        End Sub

        Private Sub scheduleCalls()
            Dim callsQuery As New Meanstream.Portal.CRM.Data.MeanstreamCrmCallsQuery
            callsQuery.AppendNotEquals(Entities.MeanstreamCrmCallsColumn.Occurance, "once")
            callsQuery.AppendEquals(Entities.MeanstreamCrmCallsColumn.IsCanceled, "False")
            callsQuery.AppendLessThanOrEqual(Entities.MeanstreamCrmCallsColumn.ScheduledDateTime, Date.Now.ToString)
            Dim calls As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Find(callsQuery.GetParameters)
            For Each callTask As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls In calls
                'callTask.UserId
                If callTask.Occurance.ToLower = "daily" Then
                    callTask.ScheduledDateTime = callTask.ScheduledDateTime.AddDays(1.0)
                End If
                If callTask.Occurance.ToLower = "weekly" Then
                    callTask.ScheduledDateTime = callTask.ScheduledDateTime.AddDays(7.0)
                End If
                If callTask.Occurance.ToLower = "monthly" Then
                    callTask.ScheduledDateTime = callTask.ScheduledDateTime.AddMonths(1.0)
                End If
                If callTask.Occurance.ToLower = "yearly" Then
                    callTask.ScheduledDateTime = callTask.ScheduledDateTime.AddYears(1.0)
                End If

                'update existing
                'callTask.IsCompleted = False
                'Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Update(callTask)

                'Or we create a new task
                'Dim newCall As New Meanstream.Portal.CRM.Calls(Guid.NewGuid)
                'newCall.AccountId = callTask.AccountId
                'newCall.CallResult = callTask.CallResult
                'newCall.CampaignId = callTask.CampaignId
                'newCall.CampaignStepId = callTask.CampaignStepId
                'newCall.CreatedDate = callTask.CreatedDate
                'newCall.IsAppointment = callTask.IsAppointment
                'newCall.IsCanceled = False
                'newCall.IsCompleted = False
                'newCall.IsExactTime = callTask.IsExactTime
                'newCall.LastModifiedDate = callTask.LastModifiedDate
                'newCall.Notes = callTask.Notes
                'newCall.Occurance = callTask.Occurance
                'newCall.Participant = System.Activator.CreateInstance(Type.GetType(callTask.ParticipantType), callTask.ParticipantId)
                'newCall.PhoneScript = callTask.PhoneScript
                'newCall.PhoneScriptId = callTask.PhoneScriptId
                'newCall.ReferenceId = callTask.ReferenceId
                'newCall.ScheduledDateTime = callTask.ScheduledDateTime
                'newCall.ScheduleFollowUp = callTask.ScheduleFollowUp
                'newCall.Summary = callTask.Summary
                'newCall.UserId = callTask.UserId
                'Meanstream.Portal.CRM.CRMService.Current.ScheduleCall(newCall)
            Next
        End Sub

        Private Sub scheduleEmails()
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmEmailsQuery
            query.AppendNotEquals(Entities.MeanstreamCrmEmailsColumn.Occurance, "once")
            query.AppendEquals(Entities.MeanstreamCrmEmailsColumn.IsCanceled, "False")
            query.AppendLessThanOrEqual(Entities.MeanstreamCrmEmailsColumn.ScheduledDateTime, Date.Now.ToString)
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails In tasks
                If task.Occurance.ToLower = "daily" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddDays(1.0)
                End If
                If task.Occurance.ToLower = "weekly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddDays(7.0)
                End If
                If task.Occurance.ToLower = "monthly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddMonths(1.0)
                End If
                If task.Occurance.ToLower = "yearly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddYears(1.0)
                End If
                'update existing
                task.IsCompleted = False
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Update(task)
            Next
        End Sub

        Private Sub scheduleMailing()
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmMailsQuery
            query.AppendNotEquals(Entities.MeanstreamCrmMailsColumn.Occurance, "once")
            query.AppendEquals(Entities.MeanstreamCrmMailsColumn.IsCanceled, "False")
            query.AppendLessThanOrEqual(Entities.MeanstreamCrmMailsColumn.ScheduledDateTime, Date.Now.ToString)
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmMails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails In tasks
                If task.Occurance.ToLower = "daily" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddDays(1.0)
                End If
                If task.Occurance.ToLower = "weekly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddDays(7.0)
                End If
                If task.Occurance.ToLower = "monthly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddMonths(1.0)
                End If
                If task.Occurance.ToLower = "yearly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddYears(1.0)
                End If
                'update existing
                task.IsEnded = False
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Update(task)
            Next
        End Sub

        Private Sub scheduleTasks()
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmTasksQuery
            query.AppendNotEquals(Entities.MeanstreamCrmTasksColumn.Occurance, "once")
            query.AppendEquals(Entities.MeanstreamCrmTasksColumn.IsCanceled, "False")
            query.AppendLessThanOrEqual(Entities.MeanstreamCrmTasksColumn.ScheduledDateTime, Date.Now.ToString)
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks In tasks
                If task.Occurance.ToLower = "daily" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddDays(1.0)
                End If
                If task.Occurance.ToLower = "weekly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddDays(7.0)
                End If
                If task.Occurance.ToLower = "monthly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddMonths(1.0)
                End If
                If task.Occurance.ToLower = "yearly" Then
                    task.ScheduledDateTime = task.ScheduledDateTime.AddYears(1.0)
                End If
                'update existing
                task.IsEnded = False
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Update(task)
            Next
        End Sub
    End Class
End Namespace
