Imports System.Web
Imports Meanstream.Portal.Core.ExceptionHandling
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.HttpModules
    Public Class PortalErrorHandlerModule
        Implements IHttpModule

        Private configSection As PortalExceptionSection

        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
        End Sub

        Public Sub Init(ByVal application As HttpApplication) Implements System.Web.IHttpModule.Init

            Dim section As PortalExceptionSection = CType(System.Configuration.ConfigurationManager.GetSection("portalException"), PortalExceptionSection)
            If section Is Nothing Then
                Throw New ConfigurationException("portalException configuration section does not exist")
            End If

            Me.configSection = section

            AddHandler application.Error, AddressOf Me.Application_Error
        End Sub

        Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
            Dim httpContext As HttpContext = TryCast(sender, HttpApplication).Context
            Dim response As HttpResponse = httpContext.Response

            Dim ex As Exception = httpContext.Current.Server.GetLastError()
            Dim lastError As HttpException = TryCast(ex, HttpException)

            If TypeOf ex Is HttpUnhandledException AndAlso ex.InnerException IsNot Nothing Then
                ex = ex.InnerException
            Else
                Return
            End If

            If ex IsNot Nothing Then
                Dim errorMessageDetails As String = ""

                'check to see if logging is enabled
                If configSection.Logging.Enabled Then
                    Try
                        Dim exceptionLogger As New ExceptionTraceLogger(ex)
                        errorMessageDetails = exceptionLogger.GetMessage()

                        'log error
                        exceptionLogger.LogException()

                    Catch logex As Exception
                        'error writing to database
                    End Try
                End If

                If configSection.Notification.Enabled Then
                    Try
                        'notify administrators
                        Dim Expression As New System.Text.RegularExpressions.Regex("\S+@\S+\.\S+")

                        'Send Mail
                        Dim objMail As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
                        objMail.Priority = configSection.Notification.Priority
                        objMail.Subject = configSection.Notification.Subject
                        objMail.Body = errorMessageDetails
                        objMail.IsBodyHtml = False
                        objMail.From = New System.Net.Mail.MailAddress(Expression.IsMatch(configSection.Notification.FromAddress))

                        'Parse the email list
                        Dim EmailList As String() = configSection.Notification.ToAddresses.Split(",")
                        For Each Email As String In EmailList
                            If Expression.IsMatch(Email) Then
                                objMail.To.Add(Email)
                            End If
                        Next

                        Dim client As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient
                        client.Send(objMail)
                    Catch smtpex As Exception
                        'error sending email
                        PortalTrace.WriteLine("error sending email.", TraceLevel.Fail)
                    End Try
                End If

                If configSection.ErrorPage.Type = ErrorPageElement.ErrorPageType.TransferToPage Then
                    'transfer request to a friendly error page
                    httpContext.Current.Server.Transfer(configSection.ErrorPage.Path)

                ElseIf configSection.ErrorPage.Type = ErrorPageElement.ErrorPageType.BuiltIn Then
                    'built-in html error
                    Dim errorHtml As String = "<html><head><title>[type]</title></head><body><br /><br />[type]</div><br /><br />[message]<br /><br />Exception source: [source]<br/><br/>Exception string:<br /><br />[tostring]<br /><br /></body></html>"
                    errorHtml = errorHtml.Replace("[type]", ex.GetType().ToString())
                    errorHtml = errorHtml.Replace("[message]", ex.Message.Replace("\n", "<br />"))
                    errorHtml = errorHtml.Replace("[tostring]", ex.ToString().Replace("\n", "<br />"))
                    errorHtml = errorHtml.Replace("[source]", ex.Source)

                    If configSection.ErrorPage.ShowHTMLErrorMessage Then
                        'Display Yellow Screen of Death for this error 
                        Dim yellowScreen As String = lastError.GetHtmlErrorMessage()
                        If Not String.IsNullOrEmpty(yellowScreen) Then
                            errorHtml = errorHtml & yellowScreen
                        End If
                    End If

                    response.Clear()
                    response.Write(errorHtml)
                    response.End()
                End If
            End If

            httpContext.Current.Server.ClearError()
        End Sub

    End Class
End Namespace

