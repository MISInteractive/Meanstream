Imports Meanstream.Portal.Core.Instrumentation
Imports System.Configuration

Namespace Meanstream.Portal.CRM
    Public Class CRMService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As CRMService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As CRMService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateServiceInstance = New CRMService(machineName, appFriendlyName)
                            _privateServiceInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privateServiceInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

#Region " Methods "
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(Meanstream.Portal.Core.AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The crm service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("CRM Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize CRM Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function GetContacts(ByVal userId As Guid) As List(Of Contact)
            Dim contacts As New List(Of Contact)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmContactsProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts In entities
                Dim contact As New Contact(entity.Id)
                Dim manager As New ContactManager(contact)
                manager.Bind(entity)
                contacts.Add(contact)
            Next
            Return contacts
        End Function

        Public Function GetContact(ByVal accountId As Guid) As Contact
            Dim contact As New Contact(accountId)
            Dim manager As New ContactManager(contact)
            manager.LoadFromDatasource()
            Return contact
        End Function

        Public Function CreateContact(ByVal contact As Contact) As Guid
            Return ContactManager.Create(contact)
        End Function

        Public Sub SaveContact(ByVal contact As Contact)
            Dim manager As New ContactManager(contact)
            manager.Save()
        End Sub

        Public Function GetAccount(ByVal accountId As Guid) As Account
            Dim account As New Account(accountId)
            Dim manager As New AccountManager(account)
            manager.LoadFromDatasource()
            Return account
        End Function

        Public Function GetAccounts(ByVal userId As Guid) As List(Of Account)
            Dim accounts As New List(Of Account)

            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserAccounts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserAccountsProvider.Find("UserId=" & userId.ToString)
            Dim idList As List(Of String) = New List(Of String)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserAccounts In entities
                idList.Add(entity.AccountId.ToString)
            Next

            If idList.Count > 0 Then
                Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmAccountsQuery
                query.AppendIn(CRM.Entities.MeanstreamCrmAccountsColumn.Id, idList.ToArray)
                Dim accountEntities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAccounts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAccountsProvider.Find(query.GetParameters)
                For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAccounts In accountEntities
                    Dim account As New Account(entity.Id)
                    Dim manager As New AccountManager(account)
                    manager.Bind(entity)
                    accounts.Add(account)
                Next
            End If
            
            Return accounts
        End Function

        Public Function CreateAccount(ByVal userid As Guid, ByVal account As Account) As Guid
            Dim id As Guid = AccountManager.Create(account)
            Me.AddUserToAccount(userid, id)
            Return id
        End Function

        Public Sub SaveAccount(ByVal account As Account)
            Dim manager As New AccountManager(account)
            manager.Save()
        End Sub

        Public Function IsUserInAccount(ByVal userId As Guid, ByVal accountId As Guid) As Boolean
            Dim account As New Account(accountId)
            Dim manager As New AccountManager(account)
            Return manager.IsUserInAccount(userId)
        End Function

        Public Sub AddUserToAccount(ByVal userId As Guid, ByVal accountId As Guid)
            Dim account As New Account(accountId)
            Dim manager As New AccountManager(account)
             manager.AddUserToAccount(userId)
        End Sub

        Public Function GetSegments(ByVal userId As Guid) As List(Of Segment)
            Dim segments As New List(Of Segment)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment In entities
                Dim segment As New Segment(entity.Id)
                Dim manager As New SegmentManager(segment)
                manager.Bind(entity)
                segments.Add(segment)
            Next
            Return segments
        End Function

        Public Function GetSegment(ByVal segmentId As Guid) As Segment
            Dim segment As New Meanstream.Portal.CRM.Segment(segmentId)
            Dim manager As New Meanstream.Portal.CRM.SegmentManager(segment)
            manager.LoadFromDatasource()
            Return segment
        End Function

        Public Function GetSegmentsForContact(ByVal contactId As Guid) As List(Of Segment)
            Dim segments As New List(Of Segment)

            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("ContactId=" & contactId.ToString)
            Dim idList As List(Of String) = New List(Of String)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts In entities
                idList.Add(entity.SegmentId.ToString)
            Next
            If idList.Count > 0 Then
                Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmSegmentQuery
                query.AppendIn(CRM.Entities.MeanstreamCrmSegmentColumn.Id, idList.ToArray)
                Dim segmentEntities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentProvider.Find(query.GetParameters)
                For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegment In segmentEntities
                    Dim segment As New Segment(entity.Id)
                    Dim manager As New SegmentManager(segment)
                    manager.Bind(entity)
                    segments.Add(segment)
                Next
            End If
            
            Return segments
        End Function

        Public Function GetContactsInSegment(ByVal segmentId As Guid) As List(Of Contact)
            Dim contacts As New List(Of Contact)

            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSegmentContactsProvider.Find("SegmentId=" & segmentId.ToString)
            Dim idList As List(Of String) = New List(Of String)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSegmentContacts In entities
                idList.Add(entity.ContactId.ToString)
            Next

            If idList.Count > 0 Then
                Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmContactsQuery
                query.AppendIn(CRM.Entities.MeanstreamCrmContactsColumn.Id, idList.ToArray)
                Dim contactEntities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmContactsProvider.Find(query.GetParameters)
                For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmContacts In contactEntities
                    Dim contact As New Contact(entity.Id)
                    Dim manager As New ContactManager(contact)
                    manager.Bind(entity)
                    contacts.Add(contact)
                Next
            End If
            Return contacts
        End Function

        Public Function CreateSegment(ByVal segment As Segment) As Guid
            Return SegmentManager.Create(segment)
        End Function

        Public Sub AddContactToSegment(ByVal contactId As Guid, ByVal segmentId As Guid)
            Dim segment As New Segment(segmentId)
            Dim manager As New SegmentManager(segment)
            manager.AddContactToSegment(contactId)
        End Sub

        Public Function IsContactInSegment(ByVal contactId As Guid, ByVal segmentId As Guid) As Boolean
            Dim segment As New Segment(segmentId)
            Dim manager As New SegmentManager(segment)
            Return manager.IsContactInSegment(contactId)
        End Function

        Public Function GetGroups(ByVal userId As Guid) As List(Of Group)
            Dim groups As New List(Of Group)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup In entities
                Dim group As New Group(entity.Id)
                Dim manager As New GroupManager(group)
                manager.Bind(entity)
                groups.Add(group)
            Next
            Return groups
        End Function

        Public Function GetGroupsForContact(ByVal contactId As Guid) As List(Of Group)
            Dim groups As New List(Of Group)

            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupContactsProvider.Find("ContactId=" & contactId.ToString)
            Dim idList As List(Of String) = New List(Of String)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroupContacts In entities
                idList.Add(entity.GroupId.ToString)
            Next

            If idList.Count > 0 Then
                Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmGroupQuery
                query.AppendIn(CRM.Entities.MeanstreamCrmGroupColumn.Id, idList.ToArray)
                Dim segmentEntities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.Find(query.GetParameters)
                For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup In segmentEntities
                    Dim group As New Group(entity.Id)
                    Dim manager As New GroupManager(group)
                    manager.Bind(entity)
                    groups.Add(group)
                Next
            End If
            Return groups
        End Function

        Public Function GetGroups() As List(Of Group)
            Dim groups As New List(Of Group)
            Dim segmentEntities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.GetAll
            segmentEntities.Sort("Name ASC")
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup In segmentEntities
                Dim group As New Group(entity.Id)
                Dim manager As New GroupManager(group)
                manager.Bind(entity)
                groups.Add(group)
            Next
            Return groups
        End Function

        Public Function GetGroup(ByVal name As String) As Group
            Dim groups As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmGroup) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmGroupProvider.Find("Name=" & name)
            If groups.Count = 0 Then
                Return Nothing
            End If
            Dim group As New Group(groups(0).Id)
            Dim manager As New GroupManager(group)
            manager.Bind(groups(0))
            Return group
        End Function

        Public Function GetContactsInGroup(ByVal name As String, ByVal userId As Guid) As List(Of Contact)
            Dim contacts As New List(Of Contact)
            Dim query As New Meanstream.Portal.CRM.Data.VwMeanstreamCrmGroupContactsQuery
            query.AppendEquals(Entities.VwMeanstreamCrmGroupContactsColumn.GroupName, name)
            query.AppendEquals(Entities.VwMeanstreamCrmGroupContactsColumn.UserId, userId.ToString)
            Dim groups As Meanstream.Portal.CRM.Entities.VList(Of Meanstream.Portal.CRM.Entities.VwMeanstreamCrmGroupContacts) = Meanstream.Portal.CRM.Data.DataRepository.VwMeanstreamCrmGroupContactsProvider.Find(query.GetParameters)
            groups.Sort("FirstName ASC")

            For Each groupContact As Meanstream.Portal.CRM.Entities.VwMeanstreamCrmGroupContacts In groups
                Dim contact As New Contact(groupContact.ContactId)
                Dim manager As New ContactManager(contact)
                manager.Bind(groupContact)
                contacts.Add(contact)
            Next
            Return contacts
        End Function

        Public Function CreateGroup(ByVal group As Group) As Guid
            Return GroupManager.Create(group)
        End Function

        Public Sub AddContactToGroup(ByVal contactId As Guid, ByVal groupId As Guid)
            Dim group As New Group(groupId)
            Dim manager As New GroupManager(group)
            manager.AddContactToGroup(contactId)
        End Sub

        Public Function IsContactInGroup(ByVal contactId As Guid, ByVal groupId As Guid) As Boolean
            Dim group As New Group(groupId)
            Dim manager As New GroupManager(group)
            Return manager.IsContactInGroup(contactId)
        End Function

        Public Function GetCampaigns(ByVal userId As Guid) As List(Of Campaign)
            Dim list As New List(Of Campaign)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCampaignsProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCampaigns In entities
                Dim campaign As New Meanstream.Portal.CRM.Campaign(entity.Id)
                campaign.IsActive = entity.IsActive
                campaign.Summary = entity.Summary
                campaign.Description = entity.Description
                campaign.Name = entity.Name
                list.Add(campaign)
            Next     
            Return list
        End Function

        Public Sub DeleteCampaign(ByVal campaignId As Guid)
            Dim campaign As New Campaign(campaignId)
            Dim manager As New CampaignManager(campaign)
            manager.Delete()
        End Sub

        Public Function CreateCampaign(ByVal campaign As Campaign) As Guid
            Return CampaignManager.Create(campaign)
        End Function

        Public Function GetAppointments(ByVal accountId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Appointment)
            Dim appointments As New List(Of Appointment)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmAppointmentsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.AccountId, accountId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.IsCompleted, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAppointmentsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments In entities
                Dim appointment As New Appointment(entity.Id)
                Dim manager As New AppointmentManager(appointment)
                manager.Bind(entity)
                appointments.Add(appointment)
            Next
            Return appointments
        End Function

        Public Function GetAppointmentsForParticipant(ByVal participantId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Appointment)
            Dim appointments As New List(Of Appointment)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmAppointmentsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.ParticipantId, participantId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.IsCompleted, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointmentsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAppointmentsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAppointments In entities
                Dim appointment As New Appointment(entity.Id)
                Dim manager As New AppointmentManager(appointment)
                manager.Bind(entity)
                appointments.Add(appointment)
            Next
            Return appointments
        End Function

        Public Function GetAppointment(ByVal appointmentId As Guid) As Appointment
            Dim appointment As New Appointment(appointmentId)
            Dim manager As New AppointmentManager(appointment)
            manager.LoadFromDatasource()
            Return appointment
        End Function

        Public Sub SaveAppointment(ByVal appointment As Appointment)
            Dim manager As New AppointmentManager(appointment)
            manager.Save()
        End Sub

        Public Function ScheduleAppointment(ByVal appointment As Appointment) As Guid
            Return AppointmentManager.Create(appointment)
        End Function

        Public Sub EndAppointment(ByVal appointmentId As Guid, ByVal canceled As Boolean)
            Dim appointment As New Appointment(appointmentId)
            Dim manager As New AppointmentManager(appointment)
            manager.LoadFromDatasource()
            appointment.IsCompleted = True
            appointment.IsCanceled = canceled
            manager.Save()
        End Sub

        Public Function GetCall(ByVal callId As Guid) As Calls
            Dim sCall As New Calls(callId)
            Dim manager As New CallManager(sCall)
            manager.LoadFromDatasource()
            Return sCall
        End Function

        Public Function GetMail(ByVal mailId As Guid) As Mail
            Dim task As New Mail(mailId)
            Dim manager As New MailManager(task)
            manager.LoadFromDatasource()
            Return task
        End Function

        Public Function GetEmail(ByVal emailId As Guid) As Email
            Dim task As New Email(emailId)
            Dim manager As New EmailManager(task)
            manager.LoadFromDatasource()
            Return task
        End Function

        Public Function GetTask(ByVal taskId As Guid) As Task
            Dim task As New Task(taskId)
            Dim manager As New TaskManager(task)
            manager.LoadFromDatasource()
            Return task
        End Function

        Public Function GetCalls(ByVal accountId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Calls)
            Dim calls As New List(Of Calls)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmCallsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.AccountId, accountId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.IsCompleted, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls In entities
                Dim sCall As New Calls(entity.Id)
                Dim manager As New CallManager(sCall)
                manager.Bind(entity)
                calls.Add(sCall)
            Next
            Return calls
        End Function

        Public Function GetCallsForParticipant(ByVal participantId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Calls)
            Dim calls As New List(Of Calls)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmCallsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.ParticipantId, participantId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.IsCompleted, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmCallsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCallsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCalls In entities
                Dim sCall As New Calls(entity.Id)
                Dim manager As New CallManager(sCall)
                manager.Bind(entity)
                calls.Add(sCall)
            Next
            Return calls
        End Function

        Public Sub SaveCall(ByVal calls As Calls)
            Dim manager As New CallManager(calls)
            manager.Save()
        End Sub

        Public Function ScheduleCall(ByVal calls As Calls) As Guid
            Return CallManager.Create(calls)
        End Function

        Public Sub EndCall(ByVal callId As Guid, ByVal scheduleFollowUp As String, ByVal callResult As String, ByVal canceled As Boolean)
            Me.EndCall(callId, scheduleFollowUp, callResult, False, canceled)
        End Sub

        Public Sub EndCall(ByVal callId As Guid, ByVal scheduleFollowUp As String, ByVal callResult As String, ByVal isAppointment As Boolean, ByVal canceled As Boolean)
            If scheduleFollowUp = Nothing Then
                Throw New ArgumentNullException("schedule followUp is required")
            End If
            If callResult = Nothing Then
                Throw New ArgumentNullException("call result is required")
            End If

            Dim sCall As New Calls(callId)
            Dim manager As New CallManager(sCall)
            manager.LoadFromDatasource()
            sCall.IsAppointment = isAppointment
            sCall.IsCompleted = True
            sCall.IsCanceled = canceled
            sCall.ScheduleFollowUp = scheduleFollowUp
            sCall.CallResult = callResult
            manager.Save()
        End Sub

        Public Function GetEmails(ByVal accountId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Email)
            Dim emails As New List(Of Email)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmEmailsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.AccountId, accountId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.IsCompleted, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails In entities
                Dim sEmail As New Email(entity.Id)
                Dim manager As New EmailManager(sEmail)
                manager.Bind(entity)
                emails.Add(sEmail)
            Next
            Return emails
        End Function

        Public Function GetEmailsForParticipant(ByVal participantId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Email)
            Dim emails As New List(Of Email)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmEmailsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.ParticipantId, participantId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.IsCompleted, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmEmailsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmEmailsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmEmails In entities
                Dim sEmail As New Email(entity.Id)
                Dim manager As New EmailManager(sEmail)
                manager.Bind(entity)
                emails.Add(sEmail)
            Next
            Return emails
        End Function

        Public Sub SaveEmail(ByVal email As Email)
            Dim manager As New EmailManager(email)
            manager.Save()
        End Sub

        Public Function ScheduleEmail(ByVal email As Email) As Guid
            Return EmailManager.Create(email)
        End Function

        Public Sub EndEmail(ByVal emailId As Guid, ByVal canceled As Boolean)
            Dim email As New Email(emailId)
            Dim manager As New EmailManager(email)
            manager.LoadFromDatasource()
            email.IsCompleted = True
            email.IsCanceled = canceled
            manager.Save()
        End Sub

        Public Function GetMail(ByVal accountId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Mail)
            Dim mail As New List(Of Mail)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmMailsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.AccountId, accountId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.IsEnded, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmMails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails In entities
                Dim sMail As New Mail(entity.Id)
                Dim manager As New MailManager(sMail)
                manager.Bind(entity)
                mail.Add(sMail)
            Next
            Return mail
        End Function

        Public Function GetMailForParticipant(ByVal participantId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Mail)
            Dim mail As New List(Of Mail)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmMailsQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.ParticipantId, participantId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.IsEnded, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmMailsColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmMails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmMailsProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmMails In entities
                Dim sMail As New Mail(entity.Id)
                Dim manager As New MailManager(sMail)
                manager.Bind(entity)
                mail.Add(sMail)
            Next
            Return mail
        End Function

        Public Sub SaveMail(ByVal mail As Mail)
            Dim manager As New MailManager(mail)
            manager.Save()
        End Sub

        Public Function ScheduleMail(ByVal mail As Mail) As Guid
            Return MailManager.Create(mail)
        End Function

        Public Sub EndMail(ByVal mailId As Guid, ByVal canceled As Boolean)
            Dim mail As New Mail(mailId)
            Dim manager As New MailManager(mail)
            manager.LoadFromDatasource()
            mail.IsCompleted = True
            mail.IsCanceled = canceled
            manager.Save()
        End Sub

        Public Function GetTask(ByVal accountId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Task)
            Dim task As New List(Of Task)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmTasksQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.AccountId, accountId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.IsEnded, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks In entities
                Dim sTask As New Task(entity.Id)
                Dim manager As New TaskManager(sTask)
                manager.Bind(entity)
                task.Add(sTask)
            Next
            Return task
        End Function

        Public Function GetTaskForParticipant(ByVal participantId As Guid, ByVal range1 As DateTime, ByVal range2 As DateTime, ByVal completed As Boolean) As List(Of Task)
            Dim task As New List(Of Task)
            Dim query As New Meanstream.Portal.CRM.Data.MeanstreamCrmTasksQuery
            query.Append(Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.ParticipantId, participantId.ToString)
            If range1 <> Nothing And range2 <> Nothing Then
                query.AppendRange(Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.ScheduledDateTime, range1.ToString, range2.ToString)
            End If
            If completed <> Nothing Then
                query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.IsEnded, completed.ToString)
            End If
            query.AppendEquals("AND", Meanstream.Portal.CRM.Entities.MeanstreamCrmTasksColumn.IsCanceled, "False")
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmTasksProvider.Find(query.GetParameters)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmTasks In entities
                Dim sTask As New Task(entity.Id)
                Dim manager As New TaskManager(sTask)
                manager.Bind(entity)
                task.Add(sTask)
            Next
            Return task
        End Function

        Public Sub SaveTask(ByVal task As Task)
            Dim manager As New TaskManager(task)
            manager.Save()
        End Sub

        Public Function ScheduleTask(ByVal task As Task) As Guid
            Return TaskManager.Create(task)
        End Function

        Public Sub EndTask(ByVal taskId As Guid, ByVal canceled As Boolean)
            Dim task As New Task(taskId)
            Dim manager As New TaskManager(task)
            manager.LoadFromDatasource()
            task.IsCompleted = True
            task.IsCanceled = canceled
            manager.Save()
        End Sub

        Public Function GetEmailPreference(ByVal userId As Guid) As EmailPreference
            Dim entity As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserEmailPreference) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserEmailPreferenceProvider.Find("UserId=" & userId.ToString)
            If entity.Count = 0 Then
                Return Nothing
            End If
            Dim preference As New EmailPreference(entity(0).Id)
            preference.CompositionMode = entity(0).CompositionMode
            preference.DefaultSendingAddress = entity(0).DefaultSendingAddress
            preference.Signature = entity(0).Signature
            preference.UserId = entity(0).UserId
            Return preference
        End Function

        Public Function SaveEmailPreference(ByVal preference As EmailPreference) As Guid
            If preference.Id = Nothing Then
                Throw New ArgumentException("id is required")
            End If
            If preference.UserId = Nothing Then
                Throw New ArgumentException("userId is required")
            End If
            If preference.DefaultSendingAddress = Nothing Or preference.DefaultSendingAddress.Trim = "" Then
                Throw New ArgumentException("valid DefaultSendingAddress is required")
            End If
            If preference.CompositionMode = Nothing Or preference.CompositionMode.Trim = "" Then
                Throw New ArgumentException("CompositionMode is required")
            End If
            Dim preferenceEntity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserEmailPreference = New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserEmailPreference
            Dim entity As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserEmailPreference) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserEmailPreferenceProvider.Find("UserId=" & preference.UserId.ToString)

            If entity.Count = 0 Then
                preferenceEntity.Id = Guid.NewGuid
                preferenceEntity.CompositionMode = preference.CompositionMode
                preferenceEntity.DefaultSendingAddress = preference.DefaultSendingAddress
                preferenceEntity.Signature = preference.Signature
                preferenceEntity.UserId = preference.UserId
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserEmailPreferenceProvider.Insert(preferenceEntity)
            Else
                entity(0).CompositionMode = preference.CompositionMode
                entity(0).DefaultSendingAddress = preference.DefaultSendingAddress
                entity(0).Signature = preference.Signature
                entity(0).UserId = preference.UserId
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserEmailPreferenceProvider.Update(entity(0))
            End If
            Return preferenceEntity.Id
        End Function

        Public Function GetCellPhoneProviders() As List(Of CellPhoneProvider)
            Dim providers As New List(Of CellPhoneProvider)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmCellPhoneProviders) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCellPhoneProvidersProvider.GetAll
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmCellPhoneProviders In entities
                Dim provider As New CellPhoneProvider(entity.Provider)
                providers.Add(provider)
            Next
            Return providers
        End Function

        Public Sub SaveCellPhoneProviders(ByVal providers As List(Of CellPhoneProvider))
            If providers.Count > 0 Then
                Dim existing As List(Of CellPhoneProvider) = Me.GetCellPhoneProviders
                'delete if it doesn't exist in new list
                For Each provider As CellPhoneProvider In existing
                    If Not providers.Contains(provider) Then
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCellPhoneProvidersProvider.Delete(provider.Provider)
                    End If
                Next
                'add if it doesn't exist in old list
                For Each provider As CellPhoneProvider In providers
                    If Not existing.Contains(provider) Then
                        Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmCellPhoneProviders
                        entity.Provider = provider.Provider
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmCellPhoneProvidersProvider.Insert(entity)
                    End If
                Next
            End If
        End Sub

        Public Function GetStoredEmails(ByVal userId As Guid) As List(Of StoredEmail)
            Dim emails As New List(Of StoredEmail)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserStoredEmails) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredEmailsProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserStoredEmails In entities
                Dim email As New StoredEmail(entity.Id)
                email.Body = entity.Body
                email.Name = entity.Name
                email.Subject = entity.Subject
                email.UserId = entity.UserId
                emails.Add(email)
            Next
            Return emails
        End Function

        Public Sub SaveStoredEmails(ByVal userId As Guid, ByVal list As List(Of StoredEmail))
            'If list.Count > 0 Then
            Dim existing As List(Of StoredEmail) = Me.GetStoredEmails(userId)
            'delete if it doesn't exist in new list
            For Each email As StoredEmail In existing
                If Not list.Contains(email) Then
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredEmailsProvider.Delete(email.Id)
                End If
            Next
            'add if it doesn't exist in old list
            For Each email As StoredEmail In list
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserStoredEmails
                entity.Body = email.Body
                entity.Name = email.Name
                entity.Subject = email.Subject
                entity.UserId = email.UserId
                If Not existing.Contains(email) Then
                    entity.Id = Guid.NewGuid
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredEmailsProvider.Insert(entity)
                Else
                    entity.Id = email.Id
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredEmailsProvider.Update(entity)
                End If
            Next
            'End If
        End Sub

        Public Function GetStoredPhoneScripts(ByVal userId As Guid) As List(Of StoredPhoneScript)
            Dim scripts As New List(Of StoredPhoneScript)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserStoredPhoneScripts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredPhoneScriptsProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserStoredPhoneScripts In entities
                Dim script As New StoredPhoneScript(entity.Id)
                script.Name = entity.Name
                script.Script = entity.Script
                script.UserId = entity.UserId
                scripts.Add(script)
            Next
            Return scripts
        End Function

        Public Sub SaveUserStoredPhoneScripts(ByVal userId As Guid, ByVal list As List(Of StoredPhoneScript))
            'If list.Count > 0 Then
            Dim existing As List(Of StoredPhoneScript) = Me.GetStoredPhoneScripts(userId)
            'delete if it doesn't exist in new list
            For Each script As StoredPhoneScript In existing
                If Not list.Contains(script) Then
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredPhoneScriptsProvider.Delete(script.Id)
                End If
            Next
            'add if it doesn't exist in old list
            For Each script As StoredPhoneScript In list
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserStoredPhoneScripts
                entity.Script = script.Script
                entity.Name = script.Name
                entity.UserId = script.UserId
                entity.Id = script.Id
                If Not existing.Contains(script) Then
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredPhoneScriptsProvider.Insert(entity)
                Else
                    Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserStoredPhoneScriptsProvider.Update(entity)
                End If
            Next
            'End If
        End Sub

        Public Function GetReminders(ByVal userId As Guid) As List(Of Reminder)
            Dim reminders As New List(Of Reminder)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminders) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserRemindersProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminders In entities
                Dim reminder As New Reminder(entity.Id)
                reminder.Type = New ReminderType(entity.ReminderType)
                reminder.UserId = entity.UserId
                reminder.Preference = entity.Value
                reminders.Add(reminder)
            Next
            Return reminders
        End Function

        Public Sub SaveUserReminders(ByVal list As List(Of Reminder))
            If list.Count > 0 Then
                Dim existing As List(Of Reminder) = Me.GetReminders(list(0).UserId)
                'delete if it doesn't exist in new list
                For Each reminder As Reminder In existing
                    If Not list.Contains(reminder) Then
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserRemindersProvider.Delete(reminder.Id)
                    End If
                Next
                'add if it doesn't exist in old list
                For Each reminder As Reminder In list
                    If Not existing.Contains(reminder) Then
                        Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminders
                        entity.ReminderType = reminder.Type.Type
                        entity.UserId = reminder.UserId
                        entity.Value = reminder.Preference
                        entity.Id = reminder.Id
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserRemindersProvider.Insert(entity)

                    Else
                        Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminders = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserRemindersProvider.GetById(reminder.Id)
                        entity.ReminderType = reminder.Type.Type
                        entity.UserId = reminder.UserId
                        entity.Value = reminder.Preference
                        
                        If Not Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserRemindersProvider.Update(entity) Then
                            PortalTrace.Fail("fail " & entity.Value, DisplayMethodInfo.DoNotDisplay)
                        End If

                    End If
                Next
            End If
        End Sub

        Public Function GetReminderTypes() As List(Of ReminderType)
            Dim reminderTypes As New List(Of ReminderType)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmReminderType) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmReminderTypeProvider.GetAll
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmReminderType In entities
                Dim reminderType As New ReminderType(entity.Type)
                reminderTypes.Add(reminderType)
            Next
            Return reminderTypes
        End Function

        Public Sub SaveReminderTypes(ByVal list As List(Of ReminderType))
            If list.Count > 0 Then
                Dim existing As List(Of ReminderType) = Me.GetReminderTypes()
                'delete if it doesn't exist in new list
                For Each reminder As ReminderType In existing
                    If Not list.Contains(reminder) Then
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmReminderTypeProvider.Delete(reminder.Type)
                    End If
                Next
                'add if it doesn't exist in old list
                For Each reminder As ReminderType In list
                    Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmReminderType
                    entity.Type = reminder.Type
                    If Not existing.Contains(reminder) Then
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmReminderTypeProvider.Insert(entity)
                    Else
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmReminderTypeProvider.Update(entity)
                    End If
                Next
            End If
        End Sub

        Public Function GetReminderDeliveryPreference(ByVal userId As Guid) As ReminderDeliveryPreference
            Dim entity As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDelivery) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryProvider.Find("UserId=" & userId.ToString)
            If entity.Count = 0 Then
                Return Nothing
            End If
            Dim preference As New ReminderDeliveryPreference(entity(0).Id)
            preference.DeliveryAddress = entity(0).DeliveryAddress
            preference.Enabled = entity(0).Enabled
            preference.Provider = entity(0).Provider
            preference.Type = New ReminderDeliveryType(entity(0).UserReminderDeliveryType)
            preference.UserId = entity(0).UserId
            Return preference
        End Function

        Public Function SaveReminderDeliveryPreference(ByVal preference As ReminderDeliveryPreference)
            If preference.Id = Nothing Then
                Throw New ArgumentException("id is required")
            End If
            If preference.UserId = Nothing Then
                Throw New ArgumentException("userId is required")
            End If
            If preference.DeliveryAddress = Nothing Or preference.DeliveryAddress.Trim = "" Then
                Throw New ArgumentException("valid deliveryAddress is required")
            End If
            'If preference.Enabled = Nothing Then
            '    Throw New ArgumentException("enabled is required")
            'End If
            If preference.Type Is Nothing Then
                Throw New ArgumentException("type is required")
            Else
                If preference.Type.Type = "SMS" And (preference.Provider = Nothing Or preference.Provider.Trim = "") Then
                    Throw New ArgumentException("SMS requires a valid provider")
                End If
            End If

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDelivery = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryProvider.GetById(preference.Id)
            
            If entity Is Nothing Then
                Dim preferenceEntity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDelivery = New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDelivery
                preferenceEntity.DeliveryAddress = preference.DeliveryAddress
                preferenceEntity.Enabled = preference.Enabled
                preferenceEntity.Provider = preference.Provider
                preferenceEntity.UserId = preference.UserId
                preferenceEntity.Id = Guid.NewGuid
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryProvider.Insert(preferenceEntity)
                Return preferenceEntity.Id
            Else
                entity.DeliveryAddress = preference.DeliveryAddress
                entity.Enabled = preference.Enabled
                entity.Provider = preference.Provider
                entity.UserId = preference.UserId
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryProvider.Update(entity)
                Return entity.Id
            End If

        End Function

        Public Function GetReminderDeliveryTypes() As List(Of ReminderDeliveryType)
            Dim reminderTypes As New List(Of ReminderDeliveryType)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDeliveryType) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryTypeProvider.GetAll
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDeliveryType In entities
                Dim reminderType As New ReminderDeliveryType(entity.DeliveryType)
                reminderTypes.Add(reminderType)
            Next
            Return reminderTypes
        End Function

        Public Sub SaveReminderDeliveryTypes(ByVal list As List(Of ReminderDeliveryType))
            If list.Count > 0 Then
                Dim existing As List(Of ReminderDeliveryType) = Me.GetReminderDeliveryTypes()
                'delete if it doesn't exist in new list
                For Each reminder As ReminderDeliveryType In existing
                    If Not list.Contains(reminder) Then
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryTypeProvider.Delete(reminder.Type)
                    End If
                Next
                'add if it doesn't exist in old list
                For Each reminder As ReminderDeliveryType In list
                    Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserReminderDeliveryType
                    entity.DeliveryType = reminder.Type
                    If Not existing.Contains(reminder) Then
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryTypeProvider.Insert(entity)
                    Else
                        Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserReminderDeliveryTypeProvider.Update(entity)
                    End If
                Next
            End If
        End Sub

        Public Function GetLists(ByVal userId As Guid) As List(Of List)
            Dim lists As New List(Of List)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmLists) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmListsProvider.Find("UserId=" & userId.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmLists In entities
                Dim list As New List(entity.Id)
                list.Description = entity.Description
                list.Name = entity.Name
                list.LastModifiedDate = entity.LastModifiedDate
                list.CreatedDate = entity.CreatedDate
                lists.Add(list)
            Next
            Return lists
        End Function

        Public Function CreateList(ByVal list As List) As Guid
            If list.Name = Nothing Or list.Name.Trim = "" Then
                Throw New ArgumentException("list name required")
            End If
            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmLists
            entity.Id = Guid.NewGuid
            entity.Name = list.Name
            entity.Description = list.Description
            entity.CreatedDate = Date.Now
            entity.LastModifiedDate = Date.Now
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmListsProvider.Insert(entity)
            Return entity.Id
        End Function

        Public Sub DeleteList(ByVal listId As Guid)
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmListsProvider.Delete(listId)
        End Sub

        Public Sub SaveList(ByVal list As List)
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmLists = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmListsProvider.GetById(list.Id)
            entity.Id = list.Id
            entity.Name = list.Name
            entity.Description = list.Description
            entity.LastModifiedDate = Date.Now
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmListsProvider.Update(entity)
        End Sub
#End Region

#Region " Properties "
        Private _appFriendlyName As String
        Public Property AppFriendlyName() As String
            Get
                Return _appFriendlyName
            End Get
            Private Set(ByVal value As String)
                _appFriendlyName = value
            End Set
        End Property

        Private _machineName As String
        Public Property MachineName() As String
            Get
                Return _machineName
            End Get
            Private Set(ByVal value As String)
                _machineName = value
            End Set
        End Property

        Private _applicationId As Guid
        Public Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
            Private Set(ByVal value As Guid)
                _applicationId = value
            End Set
        End Property
#End Region


#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Deinitialize()
        End Sub
#End Region
    End Class
End Namespace

