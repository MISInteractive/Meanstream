Imports System.Text

Namespace Meanstream.Portal.Core.Instrumentation
    Public Class ExceptionTraceLogger

        Private _exceptionToLog As Exception

        Public Sub New(ByVal exceptionToLog As Exception)
            If exceptionToLog Is Nothing Then
                Throw New ArgumentNullException()
            End If

            _exceptionToLog = exceptionToLog
        End Sub

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

            PortalTrace.WriteLine(GetMessage(), TraceLevel.Fail)
        End Sub

        Public Function GetMessage() As String
            Dim messageBuilder As System.Text.StringBuilder = New StringBuilder()

            messageBuilder.AppendLine("An Error has Occured!")
            messageBuilder.Append("Exception Type: ")
            messageBuilder.AppendLine(_exceptionToLog.[GetType]().FullName)
            messageBuilder.Append("AppDomain: ")
            messageBuilder.AppendLine(AppDomain.CurrentDomain.FriendlyName)
            messageBuilder.AppendLine()

            Dim currentPortalContext As PortalContext = PortalContext.Current
            If currentPortalContext IsNot Nothing Then

                messageBuilder.AppendLine("Portal Context")
                If currentPortalContext Is Nothing Then
                    messageBuilder.AppendLine("The PortalContext is null")
                Else
                    messageBuilder.Append("Host: ")
                    messageBuilder.AppendLine(If(currentPortalContext.SiteUrl IsNot Nothing, currentPortalContext.SiteUrl, "[null]"))
                    messageBuilder.Append("OriginalUri: ")
                    messageBuilder.AppendLine(If(currentPortalContext.OriginalUri IsNot Nothing, currentPortalContext.OriginalUri.ToString(), "[null]"))
                    messageBuilder.Append("UrlReferrer: ")
                    messageBuilder.AppendLine(If(System.Web.HttpContext.Current.Request.UrlReferrer IsNot Nothing, System.Web.HttpContext.Current.Request.UrlReferrer.ToString(), "[null]"))
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

            messageBuilder.AppendLine()

            messageBuilder.AppendLine("Exception Details and StackTrace")


            For Each o As Object In _exceptionToLog.Data.Keys
                messageBuilder.Append(o.ToString())
                messageBuilder.Append(": ")
                messageBuilder.AppendLine(_exceptionToLog.Data(o).ToString())
            Next

            messageBuilder.AppendLine()
            messageBuilder.AppendLine("Call Stack: ")
            messageBuilder.AppendLine(Environment.StackTrace.ToString)
            messageBuilder.AppendLine()
            messageBuilder.AppendLine(_exceptionToLog.ToString())


            Return messageBuilder.ToString()
        End Function
    End Class
End Namespace