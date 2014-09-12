Imports System.Text

Namespace Meanstream.Portal.Core.Instrumentation
    Public Class ExceptionEventLogger

        Private Shared ReadOnly LOG_SIZE As Long = 10
        Private Shared ReadOnly DEFAULT_SOURCE As String = "Portal"
        Private Shared ReadOnly LOG_NAME As String = "Meanstream"

        Private _exceptionToLog As Exception

        Public Sub New(ByVal exceptionToLog As Exception)
            If exceptionToLog Is Nothing Then
                Throw New ArgumentNullException()
            End If

            _exceptionToLog = exceptionToLog
        End Sub

        Private Function TryGetLogName(ByVal source As String) As String
            Dim logNameBySource As String
            Try
                logNameBySource = EventLog.LogNameFromSourceName(_exceptionToLog.Source, ".")
            Catch
                logNameBySource = Nothing
            End Try

            If String.IsNullOrEmpty(logNameBySource) Then
                Return Nothing
            Else
                Return logNameBySource
            End If
        End Function

        Public Sub LogException()
            Dim referrerExists As Boolean
            Try
                referrerExists = System.Web.HttpContext.Current.Request.UrlReferrer IsNot Nothing
            Catch
                referrerExists = False
            End Try

            If referrerExists Then
                Return
            End If

            Dim source As String = DEFAULT_SOURCE '_exceptionToLog.Source
            Dim logName As String = TryGetLogName(source)

            If logName Is Nothing OrElse logName <> LOG_NAME Then
                source = DEFAULT_SOURCE
                logName = TryGetLogName(source)
            End If

            If logName <> LOG_NAME Then
                PortalTrace.WriteLine(String.Format("EventLog: Could not write to the event log. Neither {0} nor {1} has been registered as event source.", _exceptionToLog.Source, DEFAULT_SOURCE))
                Return
            End If

            EventLog.WriteEntry(source, GetMessage(), EventLogEntryType.[Error])
        End Sub

        Public Function GetMessage() As String
            Dim messageBuilder As System.Text.StringBuilder = New StringBuilder()

            messageBuilder.AppendLine("--- EXCEPTION SUMMARY ---")

            messageBuilder.Append("Message: ")
            messageBuilder.AppendLine(_exceptionToLog.Message)

            messageBuilder.Append("Source: ")
            messageBuilder.AppendLine(_exceptionToLog.Source)

            messageBuilder.Append("Exception Type: ")
            messageBuilder.AppendLine(_exceptionToLog.[GetType]().FullName)

            messageBuilder.Append("AppDomain: ")
            messageBuilder.AppendLine(AppDomain.CurrentDomain.FriendlyName)

            messageBuilder.AppendLine()


            Dim currentPortalContext As PortalContext = PortalContext.Current
            If currentPortalContext IsNot Nothing Then

                messageBuilder.AppendLine("--- PORTAL CONTEXT DETAILS ---")
                If currentPortalContext Is Nothing Then
                    messageBuilder.AppendLine("Since the PortalContext.Current is null there are no details.")
                Else
                    messageBuilder.Append("OriginalUri: ")
                    messageBuilder.AppendLine(If(currentPortalContext.OriginalUri IsNot Nothing, currentPortalContext.OriginalUri.ToString(), "[null]"))
                    messageBuilder.Append("Referrer: ")
                    messageBuilder.AppendLine(If(System.Web.HttpContext.Current.Request.UrlReferrer IsNot Nothing, System.Web.HttpContext.Current.Request.UrlReferrer.ToString(), "[null]"))
                    'messageBuilder.Append("PfsPath: ")
                    'messageBuilder.AppendLine(If(currentPortalContext.PfsPath IsNot Nothing, currentPortalContext.PfsPath, "[null]"))
                    'messageBuilder.Append("IsRequestedResourceExistInPfs: ")
                    'messageBuilder.AppendLine(currentPortalContext.IsRequestedResourceExistInPfs.ToString())
                    messageBuilder.Append("SiteUrl: ")
                    messageBuilder.AppendLine(If(currentPortalContext.SiteUrl IsNot Nothing, currentPortalContext.SiteUrl, "[null]"))
                    'messageBuilder.Append("AuthenticationMode: ")
                    'messageBuilder.AppendLine(If(currentPortalContext.AuthenticationMode IsNot Nothing, currentPortalContext.AuthenticationMode, "[null]"))
                End If
                messageBuilder.AppendLine()
            End If

            'Dim currentUser As IUser = AccessProvider.Current.GetCurrentUser()
            'messageBuilder.AppendLine("--- PORTAL USER DETAILS ---")
            'If currentUser Is Nothing Then
            '    messageBuilder.AppendLine("Since the AccessProvider.Current.GetCurrentUser() returned null, there are no details.")
            'Else
            '    messageBuilder.Append("Name: ")
            '    messageBuilder.AppendLine(If(currentUser.Name IsNot Nothing, currentUser.Name, "[null]"))
            '    messageBuilder.Append("Id: ")
            '    messageBuilder.AppendLine(currentUser.Id.ToString())
            '    messageBuilder.Append("AuthenticationType: ")
            '    messageBuilder.AppendLine(If(currentUser.AuthenticationType IsNot Nothing, currentUser.AuthenticationType, "[null]"))
            'End If
            'messageBuilder.AppendLine()

            messageBuilder.AppendLine("--- EXCEPTION DETAILS (KEYS AND STACK TRACE) ---")
            For Each o As Object In _exceptionToLog.Data.Keys
                messageBuilder.Append(o.ToString())
                messageBuilder.Append(": ")
                messageBuilder.AppendLine(_exceptionToLog.Data(o).ToString())
            Next

            messageBuilder.AppendLine()
            messageBuilder.AppendLine(_exceptionToLog.ToString())


            Return messageBuilder.ToString()
        End Function
    End Class
End Namespace
