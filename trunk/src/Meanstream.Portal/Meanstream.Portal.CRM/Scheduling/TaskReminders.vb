Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Scheduling
Imports System.Configuration

Namespace Meanstream.Portal.CRM.Scheduling
    Public Class TaskReminders
        Inherits Meanstream.Portal.Core.Services.Scheduling.Task

        Public Sub New(ByVal id As Guid, ByVal interval As Double, ByVal startupType As Core.Services.Scheduling.StartupType)
            MyBase.New(id, interval, startupType)
        End Sub

        Public Overrides Sub Execute()

            Me.SendCallReminders()
            Me.SendAppointmentReminders()
            Me.SendMailingReminders()
            Me.SendTaskReminders()
        End Sub

        Private Sub SendCallReminders()
            Dim userSentList As New ArrayList

            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmCallsQuery
            query.AppendEquals(Entities.MeanstreamCrmCallsColumn.IsCompleted, "False")
            query.AppendEquals(Entities.MeanstreamCrmCallsColumn.IsCanceled, "False")
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls In tasks
                If userSentList.Contains(task.UserId) Then
                    Continue For
                End If
                'get reminder delivery prefernce email/sms
                Dim preference As Meanstream.Portal.CRM.ReminderDeliveryPreference = Meanstream.Portal.CRM.CRMService.Current.GetReminderDeliveryPreference(task.UserId)
                If preference Is Nothing Then
                    Continue For
                End If

                'get reminder task preference
                Dim reminders As List(Of Meanstream.Portal.CRM.Reminder) = Meanstream.Portal.CRM.CRMService.Current.GetReminders(task.UserId)
                For Each Reminder As Meanstream.Portal.CRM.Reminder In reminders
                    If Reminder.Type.Type = "Calls" Then
                        'send if not none
                        If Reminder.Preference = "None" Then
                            Exit For
                        End If

                        If Reminder.Preference = "Email" Then
                            Try
                                'send email
                                Dim fromAddress As String = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                Dim toAddress As String = Meanstream.Portal.Core.Data.DataRepository.AspnetMembershipProvider.GetByUserId(task.UserId).Email
                                Dim subject As String = "Call Reminder from Izytrack"
                                Dim message As String = "This is a reminder that you have a call(s) pending."
                                Meanstream.Portal.Core.Messaging.MessagingService.Current.SmtpSend(toAddress, fromAddress, subject, message, Net.Mail.MailPriority.Normal)
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending call reminder email for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try

                        End If

                        If Reminder.Preference = "Phone" Then
                            Try
                                'send txt
                                Dim textMessage As New Meanstream.Portal.Core.Messaging.TextMessage(Guid.NewGuid)
                                textMessage.Carrier = Core.Messaging.TextMessageManager.GetCarrier(preference.Provider)
                                textMessage.Recipient = preference.DeliveryAddress
                                textMessage.Sender = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                textMessage.Subject = "Call Reminder from Izytrack"
                                textMessage.Body = "This is a reminder that you have Call(s) pending."
                                Dim manager As New Core.Messaging.TextMessageManager(textMessage)
                                manager.Send()
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending call reminder sms for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try
                        End If
                    End If
                Next
            Next
        End Sub

        Private Sub SendAppointmentReminders()
            Dim userSentList As New ArrayList

            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmAppointmentsQuery
            query.AppendEquals(Entities.MeanstreamCrmAppointmentsColumn.IsCompleted, "False")
            query.AppendEquals(Entities.MeanstreamCrmAppointmentsColumn.IsCanceled, "False")
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAppointmentsProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments In tasks
                If userSentList.Contains(task.UserId) Then
                    Continue For
                End If
                'get reminder delivery prefernce email/sms
                Dim preference As Meanstream.Portal.CRM.ReminderDeliveryPreference = Meanstream.Portal.CRM.CRMService.Current.GetReminderDeliveryPreference(task.UserId)
                If preference Is Nothing Then
                    Continue For
                End If

                'get reminder task preference
                Dim reminders As List(Of Meanstream.Portal.CRM.Reminder) = Meanstream.Portal.CRM.CRMService.Current.GetReminders(task.UserId)
                For Each Reminder As Meanstream.Portal.CRM.Reminder In reminders
                    If Reminder.Type.Type = "Appointments" Then
                        'send if not none
                        If Reminder.Preference = "None" Then
                            Exit For
                        End If

                        If Reminder.Preference = "Email" Then
                            Try
                                'send email
                                Dim fromAddress As String = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                Dim toAddress As String = Meanstream.Portal.Core.Data.DataRepository.AspnetMembershipProvider.GetByUserId(task.UserId).Email
                                Dim subject As String = "Appointment Reminder from Izytrack"
                                Dim message As String = "This is a reminder that you have Appointment(s) pending."
                                Meanstream.Portal.Core.Messaging.MessagingService.Current.SmtpSend(toAddress, fromAddress, subject, message, Net.Mail.MailPriority.Normal)
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending Appointments reminder email for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try

                        End If

                        If Reminder.Preference = "Phone" Then
                            Try
                                'send txt
                                Dim textMessage As New Meanstream.Portal.Core.Messaging.TextMessage(Guid.NewGuid)
                                textMessage.Carrier = Core.Messaging.TextMessageManager.GetCarrier(preference.Provider)
                                textMessage.Recipient = preference.DeliveryAddress
                                textMessage.Sender = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                textMessage.Subject = "Appointment Reminder from Izytrack"
                                textMessage.Body = "This is a reminder that you have a Appointments(s) pending."
                                Dim manager As New Core.Messaging.TextMessageManager(textMessage)
                                manager.Send()
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending Appointments reminder sms for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try
                        End If
                    End If
                Next
            Next
        End Sub

        Private Sub SendMailingReminders()
            Dim userSentList As New ArrayList

            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmMailsQuery
            query.AppendEquals(Entities.MeanstreamCrmMailsColumn.IsCanceled, "False")
            query.AppendEquals(Entities.MeanstreamCrmMailsColumn.IsEnded, "False")
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmMails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails In tasks
                If userSentList.Contains(task.UserId) Then
                    Continue For
                End If
                'get reminder delivery prefernce email/sms
                Dim preference As Meanstream.Portal.CRM.ReminderDeliveryPreference = Meanstream.Portal.CRM.CRMService.Current.GetReminderDeliveryPreference(task.UserId)
                If preference Is Nothing Then
                    Continue For
                End If

                'get reminder task preference
                Dim reminders As List(Of Meanstream.Portal.CRM.Reminder) = Meanstream.Portal.CRM.CRMService.Current.GetReminders(task.UserId)
                For Each Reminder As Meanstream.Portal.CRM.Reminder In reminders
                    If Reminder.Type.Type = "Snail Mail" Or Reminder.Type.Type = "Mail" Then
                        'send if not none
                        If Reminder.Preference = "None" Then
                            Exit For
                        End If

                        If Reminder.Preference = "Email" Then
                            Try
                                'send email
                                Dim fromAddress As String = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                Dim toAddress As String = Meanstream.Portal.Core.Data.DataRepository.AspnetMembershipProvider.GetByUserId(task.UserId).Email
                                Dim subject As String = "Snail Mail Reminder from Izytrack"
                                Dim message As String = "This is a reminder that you have Snail Mail pending."
                                Meanstream.Portal.Core.Messaging.MessagingService.Current.SmtpSend(toAddress, fromAddress, subject, message, Net.Mail.MailPriority.Normal)
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending Snail Mail reminder email for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try

                        End If

                        If Reminder.Preference = "Phone" Then
                            Try
                                'send txt
                                Dim textMessage As New Meanstream.Portal.Core.Messaging.TextMessage(Guid.NewGuid)
                                textMessage.Carrier = Core.Messaging.TextMessageManager.GetCarrier(preference.Provider)
                                textMessage.Recipient = preference.DeliveryAddress
                                textMessage.Sender = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                textMessage.Subject = "Snail Mail Reminder from Izytrack"
                                textMessage.Body = "This is a reminder that you have Snail Mail pending."
                                Dim manager As New Core.Messaging.TextMessageManager(textMessage)
                                manager.Send()
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending Appointments reminder sms for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try
                        End If
                    End If
                Next
            Next
        End Sub

        Private Sub SendTaskReminders()
            Dim userSentList As New ArrayList

            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmTasksQuery
            query.AppendEquals(Entities.MeanstreamCrmTasksColumn.IsCanceled, "False")
            query.AppendEquals(Entities.MeanstreamCrmTasksColumn.IsEnded, "False")
            Dim tasks As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Find(query.GetParameters)
            For Each task As Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks In tasks
                If userSentList.Contains(task.UserId) Then
                    Continue For
                End If
                'get reminder delivery prefernce email/sms
                Dim preference As Meanstream.Portal.CRM.ReminderDeliveryPreference = Meanstream.Portal.CRM.CRMService.Current.GetReminderDeliveryPreference(task.UserId)
                If preference Is Nothing Then
                    Continue For
                End If

                'get reminder task preference
                Dim reminders As List(Of Meanstream.Portal.CRM.Reminder) = Meanstream.Portal.CRM.CRMService.Current.GetReminders(task.UserId)
                For Each Reminder As Meanstream.Portal.CRM.Reminder In reminders
                    If Reminder.Type.Type = "Tasks" Then
                        'send if not none
                        If Reminder.Preference = "None" Then
                            Exit For
                        End If

                        If Reminder.Preference = "Email" Then
                            Try
                                'send email
                                Dim fromAddress As String = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                Dim toAddress As String = Meanstream.Portal.Core.Data.DataRepository.AspnetMembershipProvider.GetByUserId(task.UserId).Email
                                Dim subject As String = "Task Reminder from Izytrack"
                                Dim message As String = "This is a reminder that you have Task(s) pending."
                                Meanstream.Portal.Core.Messaging.MessagingService.Current.SmtpSend(toAddress, fromAddress, subject, message, Net.Mail.MailPriority.Normal)
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending Tasks reminder email for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try

                        End If

                        If Reminder.Preference = "Phone" Then
                            Try
                                'send txt
                                Dim textMessage As New Meanstream.Portal.Core.Messaging.TextMessage(Guid.NewGuid)
                                textMessage.Carrier = Core.Messaging.TextMessageManager.GetCarrier(preference.Provider)
                                textMessage.Recipient = preference.DeliveryAddress
                                textMessage.Sender = ConfigurationManager.AppSettings("Meanstream.CRM.TaskReminder.FromAddress")
                                textMessage.Subject = "Task Reminder from Izytrack"
                                textMessage.Body = "This is a reminder that you have Task(s) pending."
                                Dim manager As New Core.Messaging.TextMessageManager(textMessage)
                                manager.Send()
                                userSentList.Add(task.UserId)
                            Catch ex As Exception
                                Core.Instrumentation.PortalTrace.Fail("Error sending Tasks reminder sms for user: " & task.UserId.ToString & " : " & ex.Message, Core.Instrumentation.DisplayMethodInfo.NameOnly)
                            End Try
                        End If
                    End If
                Next
            Next
        End Sub
    End Class
End Namespace

