
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Interface IModuleVersion
        'Called to retrieve the Version IModule Control
        Function GetIModuleDTO() As IModuleDTO
        'Method is called when a control is marked as the default control when Creating a new page.
        Sub OnAddToPageCreate(ByVal widgetId As Guid)
        'Method is called when a control is added to the page
        Sub OnAddToPage()
        'Method is called when Copy and Create Page
        Sub OnCreateVersionFromVersion(ByVal newWidgetId As Guid)
        'Method is called when Copy and Create Page
        Sub OnCopyAndAddFromVersion(ByVal newWidgetId As Guid, ByVal newPublishedId As Guid)
        'Called when controls/modules settings are saved.
        Sub OnWidgetUpdate()
        'Method is called when a control is deleted from the page
        Sub OnWidgetDelete()
        'Called when page is published
        Sub OnPublish(ByVal newWidgetId As Guid)
    End Interface
End Namespace

