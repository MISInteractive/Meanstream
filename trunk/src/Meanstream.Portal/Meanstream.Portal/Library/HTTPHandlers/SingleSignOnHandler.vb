Imports System.Web
Imports Meanstream.Portal.Core.Instrumentation
Imports System.Web.Security

Namespace Meanstream.Portal.HttpHandlers

    Public Class SingleSignOnHandler
        Implements IHttpHandler

        Public ReadOnly Property IsReusable As Boolean Implements System.Web.IHttpHandler.IsReusable
            Get
                Return True
            End Get
        End Property

        Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
            'System.Web.HttpContext.Current.Response.Clear()
            'System.Web.HttpContext.Current.Response.ContentType = "text/xml"

            Try
                FormsAuthentication.SetAuthCookie("host", True)
                'context.Response.Redirect(System.Web.Security.FormsAuthentication.GetRedirectUrl("host", False))
                'PortalTrace.Fail("authenticating..." & context.Request.Url.AbsoluteUri, DisplayMethodInfo.DoNotDisplay)
            Catch ex As Exception
                PortalTrace.Fail(ex.Message & " : " & Me.GetType.FullName, DisplayMethodInfo.DoNotDisplay)
            End Try

            System.Web.HttpContext.Current.Response.Flush()
            System.Web.HttpContext.Current.Response.[End]()
        End Sub
    End Class

End Namespace

