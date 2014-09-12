Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Meanstream.Portal.Core.Services.Search


Namespace Meanstream.Portal.Providers.PortalIndexer

    Public Class PageProcessor
        Implements IPageProcessor

        Public Function Process(ByVal state As PageState) As Boolean Implements IPageProcessor.Process
            state.ProcessStarted = True
            state.ProcessSuccessfull = False

            Try
                
                'Console.WriteLine("Process Uri: {0}", state.Uri.AbsoluteUri)

                Dim req As WebRequest = WebRequest.Create(state.Uri)
                Dim res As WebResponse = Nothing

                Try
                    res = req.GetResponse()

                    If TypeOf res Is HttpWebResponse Then
                        state.StatusCode = CType(res, HttpWebResponse).StatusCode.ToString()
                        state.StatusDescription = CType(res, HttpWebResponse).StatusDescription
                    End If

                    If TypeOf res Is FileWebResponse Then
                        state.StatusCode = "OK"
                        state.StatusDescription = "OK"
                    End If

                    If state.StatusCode.Equals("OK") Then
                        Dim sr As New StreamReader(res.GetResponseStream())

                        state.Content = sr.ReadToEnd()

                        If Not (ContentHandler Is Nothing) Then
                            Dim doIt As PageContentDelegate = ContentHandler
                            doIt(state)
                        End If
                    End If

                    state.ProcessSuccessfull = True
                Catch ex As Exception
                    HandleException(ex, state)
                Finally
                    If Not (res Is Nothing) Then
                        res.Close()
                    End If
                End Try
            Catch ex As Exception
                Throw New ApplicationException(ex.ToString)
                'Console.WriteLine(ex.ToString())
            End Try

            'Console.WriteLine("Successfull: {0}", state.ProcessSuccessfull)

            Return state.ProcessSuccessfull
        End Function 'Process

#Region "local interface"

        Private Sub HandleException(ByVal ex As Exception, ByRef state As PageState) '
            If ex.ToString().IndexOf("(404)") <> -1 Then
                state.StatusCode = "404"
                state.StatusDescription = "(404) Not Found"
            ElseIf ex.ToString().IndexOf("(403)") <> -1 Then
                state.StatusCode = "403"
                state.StatusDescription = "(403) Forbidden"
            ElseIf ex.ToString().IndexOf("(502)") <> -1 Then
                state.StatusCode = "502"
                state.StatusDescription = "(502) Bad Gateway"
            ElseIf ex.ToString().IndexOf("(503)") <> -1 Then
                state.StatusCode = "503"
                state.StatusDescription = "(503) Server Unavailable"
            ElseIf ex.ToString().IndexOf("(504)") <> -1 Then
                state.StatusCode = "504"
                state.StatusDescription = "(504) Gateway Timeout"
            ElseIf ex.ToString().IndexOf("(500)") <> -1 Then
                state.StatusCode = "500"
                state.StatusDescription = "(500) Internal Server Error"
            ElseIf Not (ex.InnerException Is Nothing) And TypeOf ex.InnerException Is FileNotFoundException Then
                state.StatusCode = "FileNotFound"
                state.StatusDescription = ex.InnerException.Message
            Else
                state.StatusDescription = ex.ToString()
            End If
        End Sub 'HandleException
#End Region

#Region "properties"
        Private m_contentHandler As PageContentDelegate = Nothing

        Public Property ContentHandler() As PageContentDelegate Implements IPageProcessor.HandleContent
            Get
                Return m_contentHandler
            End Get
            Set(ByVal Value As PageContentDelegate)
                m_contentHandler = Value
            End Set
        End Property

#End Region

    End Class

End Namespace
