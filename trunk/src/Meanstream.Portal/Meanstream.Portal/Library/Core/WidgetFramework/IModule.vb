
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Interface IModule
        'Called to retrieve the Published IModule Control
        Function GetIModuleDTO() As IModuleDTO
        'Method is called when a control is deleted from the page
        Sub OnWidgetDelete()
    End Interface
End Namespace
