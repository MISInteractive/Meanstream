Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Web.Services
    <WebService(Namespace:="Meanstream.Portal.Web.Services")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <System.Web.Script.Services.ScriptService()> _
    Public Class WidgetService
        Inherits Meanstream.Portal.Web.Services.WebServiceBase

        <WebMethod()> _
        Public Sub Move(ByVal widgetId As Guid, ByVal skinPaneId As Guid, ByVal displayOrder As Integer)
            PortalTrace.WriteLine("Meanstream.Portal.Web.Move(): WidgetId=" & widgetId.ToString & ", SkinPaneId=" & skinPaneId.ToString & ", DisplayOrder=" & displayOrder)
            Dim widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Core.WidgetFramework.WidgetService.Current.GetWidgetVersionById(widgetId)
            Dim manager As New Core.WidgetFramework.WidgetVersionManager(widget)
            manager.Move(skinPaneId, displayOrder)
        End Sub

        <WebMethod()> _
        Public Sub SendToRecycleBin(ByVal widgetId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Web.SendToRecycleBin(): WidgetId=" & widgetId.ToString)
            Dim widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Core.WidgetFramework.WidgetService.Current.GetWidgetVersionById(widgetId)
            Dim manager As New Core.WidgetFramework.WidgetVersionManager(widget)
            manager.SendToRecycleBin()
        End Sub
    End Class
End Namespace

