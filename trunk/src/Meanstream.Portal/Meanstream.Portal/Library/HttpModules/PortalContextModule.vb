Imports System.Web
Imports Meanstream.Portal.Core
Imports Meanstream.Portal.Core.Messaging

Namespace Meanstream.Portal.HttpModules
    Public Class PortalContextModule
        Implements IHttpModule

        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
        End Sub

        Public Sub Init(ByVal application As HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler application.BeginRequest, AddressOf Me.ProcessRequest
            If ApplicationMessagingManager.Enabled Then
                AddHandler ApplicationMessagingManager.Current.PortalContextChangedMessageEvent, AddressOf Me.OnPortalContextChangedMessageReceived
            End If
        End Sub

        Private Sub OnPortalContextChangedMessageReceived(ByVal sender As Object, ByVal e As ApplicationMessageEventArgs)
            Select Case e.Message.MessageType
                Case ApplicationMessageType.PageSaved
                    If Not e.Handled Then
                        PortalContext.ReloadPortalUrlList()
                        e.Handled = True
                    End If
                    Exit Select
                Case ApplicationMessageType.PagePublished
                    If Not e.Handled Then
                        PortalContext.ReloadPortalUrlList()
                        PortalContext.ReloadPortals()
                        e.Handled = True
                    End If
                    Exit Select
                Case ApplicationMessageType.PortalSaved
                    If Not e.Handled Then
                        PortalContext.ReloadPortals()
                        e.Handled = True
                    End If
                    Exit Select
                Case ApplicationMessageType.WidgetSaved
                    If Not e.Handled Then

                        e.Handled = True
                    End If
                    Exit Select
            End Select
        End Sub

        Private Function GetSmartUrl(ByVal portalContext As PortalContext) As String
            If portalContext Is Nothing Then
                Throw New ArgumentNullException("portalContext")
            End If

            If portalContext.SiteRelativePath Is Nothing Then
                Return Nothing
            End If

            Dim smartUrlTargetPath As String = ""

            'retrieve current portal url
            Dim urls As New Dictionary(Of String, String)
            If portalContext.PortalUrls.TryGetValue(portalContext.PortalId, urls) Then
                'find the requested page in our list or urls
                urls.TryGetValue(portalContext.SiteRelativePath.ToLowerInvariant(), smartUrlTargetPath)
            Else
                Throw New ApplicationException("No website found matching the host name: " & portalContext.SiteUrl)
            End If

            If smartUrlTargetPath Is Nothing Then
                Return Nothing
            End If

            Dim resolvedSmartUrl As String = String.Concat(portalContext.OriginalUri.Scheme, "://", portalContext.SiteUrl, "/" & smartUrlTargetPath)

            Return resolvedSmartUrl
        End Function

        Private Sub ProcessRequest(ByVal sender As Object, ByVal e As EventArgs)
            Dim httpContext As HttpContext = TryCast(sender, HttpApplication).Context

            ' Create a new PortalContext instance and attach it to the current HttpContext
            Dim portalContext As PortalContext = portalContext.Create(httpContext)

            Dim smartUrl As String = Me.GetSmartUrl(portalContext)

            Core.Instrumentation.PortalTrace.WriteLine("ProcessRequest() url=" & httpContext.Request.RawUrl & " SiteUrl=" & portalContext.SiteUrl & " SiteRelativePath=" & portalContext.SiteRelativePath & " smartUrl=" & smartUrl, Instrumentation.DisplayMethodInfo.DoNotDisplay)

            Dim isMVC As Boolean = System.Configuration.ConfigurationManager.AppSettings("Meanstream.IsMVC")

            If Not isMVC Then
                'we've found a system url so let's rewrite to our handler
                If smartUrl IsNot Nothing Then
                    Dim q As String = portalContext.OriginalUri.Query
                    If q.StartsWith("?") Then
                        q = q.Replace("?", "")
                    End If
                    Dim controller As String = System.Configuration.ConfigurationManager.AppSettings("Meanstream.Controller")
                    httpContext.RewritePath(controller, smartUrl, q)
                End If
            End If

        End Sub

    End Class
End Namespace
