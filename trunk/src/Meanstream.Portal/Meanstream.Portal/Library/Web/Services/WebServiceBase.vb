Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Web.Services

    Public Class WebServiceBase
        Inherits System.Web.Services.WebService
        Protected Profile As System.Web.Profile.ProfileBase = System.Web.HttpContext.Current.Profile
    End Class
End Namespace
