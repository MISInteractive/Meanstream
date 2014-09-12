Imports System.Web
Imports Meanstream.Portal.Core.Services.SearchEngineSiteMap
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.HttpHandlers
    Public Class PortalSiteMapHandler
        Implements IHttpHandler

        Public ReadOnly Property IsReusable As Boolean Implements System.Web.IHttpHandler.IsReusable
            Get
                Return True
            End Get
        End Property

        Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.ContentType = "text/xml"

            Try
                Dim siteMapXML As New SiteMapXML(System.Web.SiteMap.RootNode)

                Dim xmlSerializer As New System.Xml.Serialization.XmlSerializer(GetType(SiteMapXML))
                xmlSerializer.Serialize(System.Web.HttpContext.Current.Response.OutputStream, siteMapXML)
                xmlSerializer = Nothing
            Catch ex As Exception
                PortalTrace.Fail(ex.Message & " : " & Me.GetType.FullName, DisplayMethodInfo.DoNotDisplay)
            End Try

            System.Web.HttpContext.Current.Response.Flush()
            System.Web.HttpContext.Current.Response.[End]()
        End Sub
    End Class
End Namespace

