Imports Microsoft.VisualBasic
Imports System.Configuration

Namespace Meanstream.Portal.Core.Utilities

    Public Class CacheUtility

        Public Const ALL_PAGES As String = "ALL_PAGES"
        Public Const PUBLISHED_PAGES As String = "PUBLISHED_PAGES"
        Public Const ALL_PAGES_IN_CACHE As String = "ALL_PAGES_IN_CACHE"
        Public Const MODULEDEFINITIONS As String = "ModuleDefinitions"
        Public Const PERMISSIONS As String = "PERMISSIONS"
        Public Const VIEWPAGEPERMISSIONS As String = "VIEWPAGEPERMISSIONS"
        Public Const VIEWPAGEPERMISSIONSVERSION As String = "VIEWPAGEPERMISSIONSVERSION"
        Public Const PAGE_CACHE As String = "PageID_"
        Public Const MEANSTREAM_MENU_CACHE As String = "MEANSTREAM_MENU_CACHE"
        Public Const USER_ROLE_CACHE As String = "USER_ROLE_CACHE"
        Public Const MENU As String = "MENU"
        Public Const USERROLES As String = "USERROLES"

        Public Shared Function GetCachedObject(ByVal Key As String) As Object
            Return System.Web.HttpRuntime.Cache(Key)
        End Function

        Public Shared Sub Add(ByVal Key As String, ByVal Value As Object)
            System.Web.HttpRuntime.Cache(Key) = Value
        End Sub

        Public Shared Sub RemovePage(ByVal Page As Meanstream.Portal.Core.Content.Page)
            Dim portalId As Guid = PortalContext.Current.PortalId

            'Remove the page content from cache
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_ENTITY_" & Page.Url & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_PAGE_PERMISSION_ENTITY_" & Page.Id.ToString & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ZONE_ENTITY_" & Page.Skin.Id.ToString & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ENTITY_" & Page.Skin.Id.ToString & "_PORTAL_" & portalId.ToString)

            System.Web.HttpRuntime.Cache.Remove(PAGE_CACHE & Page.Url & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(PAGE_CACHE & Page.Id.ToString & "_PORTAL_" & portalId.ToString)
            'Remove Menu Cache
            RemoveMenu()
            'Remove Permissions Cache
            System.Web.HttpRuntime.Cache.Remove(VIEWPAGEPERMISSIONS & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(VIEWPAGEPERMISSIONSVERSION & "_PORTAL_" & portalId.ToString)
        End Sub

        Public Shared Sub RemoveMenu()
            Dim portalId As Guid = PortalContext.Current.PortalId
            System.Web.HttpRuntime.Cache.Remove(MENU & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(MENU & "_ENTITIES" & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(ALL_PAGES & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(ALL_PAGES_IN_CACHE & "_PORTAL_" & portalId.ToString)
        End Sub

        Public Shared Sub RemoveEachPage()
            Dim portalId As Guid = PortalContext.Current.PortalId
            Dim pageList As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Content.ContentService.Current.GetAllPages(portalId)
            ' iterate through all pages
            For Each Page As Meanstream.Portal.Core.Content.Page In pageList
                System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_ENTITY_" & Page.Url & "_PORTAL_" & portalId.ToString)
                System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_PAGE_PERMISSION_ENTITY_" & Page.Id.ToString & "_PORTAL_" & portalId.ToString)
                System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ZONE_ENTITY_" & Page.Skin.Id.ToString & "_PORTAL_" & portalId.ToString)
                System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ENTITY_" & Page.Skin.Id.ToString & "_PORTAL_" & portalId.ToString)

                System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & Page.Url & "_PORTAL_" & portalId.ToString)
                System.Web.HttpRuntime.Cache(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & Page.Url & "_PORTAL_" & portalId.ToString) = New Meanstream.Portal.Core.Content.Page(Page.Id)
            Next
        End Sub

        Public Shared Sub RemoveAllPages()
            Dim portalId As Guid = PortalContext.Current.PortalId
            'Remove Menu Cache
            System.Web.HttpRuntime.Cache.Remove(MENU & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(MENU & "_ENTITIES" & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(ALL_PAGES & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(ALL_PAGES_IN_CACHE & "_PORTAL_" & portalId.ToString)
            'Remove Permissions Cache
            System.Web.HttpRuntime.Cache.Remove(VIEWPAGEPERMISSIONS & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(VIEWPAGEPERMISSIONSVERSION & "_PORTAL_" & portalId.ToString)
            CacheUtility.RemoveEachPage()
        End Sub

        Public Shared Sub Remove(ByVal Key As String)
            System.Web.HttpRuntime.Cache.Remove(Key)
        End Sub

        Public Shared Sub RefreshMenu()
            Dim portalId As Guid = PortalContext.Current.PortalId
            System.Web.HttpRuntime.Cache.Remove(MENU & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(MENU & "_ENTITIES" & "_PORTAL_" & portalId.ToString)

            Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Content.ContentService.Current.GetPageNavigation(portalId)
            'cache the menu
            System.Web.HttpRuntime.Cache(MENU & "_PORTAL_" & portalId.ToString) = pages
        End Sub

        Public Shared Sub RefreshPages()
            Dim portalId As Guid = PortalContext.Current.PortalId

            System.Web.HttpRuntime.Cache.Remove(ALL_PAGES & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(ALL_PAGES_IN_CACHE & "_PORTAL_" & portalId.ToString)
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_ENTITIES" & "_PORTAL_" & portalId.ToString)

            Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Content.ContentService.Current.GetAllPages(portalId)
            'cache page list
            System.Web.HttpRuntime.Cache(ALL_PAGES) = pages
            'System.Web.HttpRuntime.Cache(ALL_PAGES_IN_CACHE) = pages.FindAll("EnableCaching", True)
            ' iterate through all pages
            For Each Page As Meanstream.Portal.Core.Content.Page In pages
                If Page.EnableCaching Then
                    System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_ENTITY_" & Page.Url & "_PORTAL_" & portalId.ToString)
                    System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_PAGE_PERMISSION_ENTITY_" & Page.Id.ToString & "_PORTAL_" & portalId.ToString)
                    System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ZONE_ENTITY_" & Page.Skin.Id.ToString & "_PORTAL_" & portalId.ToString)
                    System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ENTITY_" & Page.Skin.Id.ToString & "_PORTAL_" & portalId.ToString)

                    System.Web.HttpRuntime.Cache.Remove(PAGE_CACHE & Page.Url & "_PORTAL_" & portalId.ToString)
                    System.Web.HttpRuntime.Cache(PAGE_CACHE & Page.Url) = New Meanstream.Portal.Core.Content.Page(Page.Id)
                End If
            Next
        End Sub

        Public Shared Sub RefreshSecurity()
            Dim portalId As Guid = PortalContext.Current.PortalId
            Dim PagePermissions As List(Of Meanstream.Portal.Core.Content.PagePermission) = Nothing
            System.Web.HttpRuntime.Cache.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS & "_PORTAL_" & portalId.ToString)
            PagePermissions = Meanstream.Portal.Core.Content.ContentService.Current.GetViewPagePermissions(portalId)
            'cache the Permissions
            System.Web.HttpRuntime.Cache(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS & "_PORTAL_" & portalId.ToString) = PagePermissions
        End Sub

        Public Shared Sub ClearApplicationCache()
            Dim keys As New List(Of String)()
            ' retrieve application Cache enumerator
            Dim enumerator As IDictionaryEnumerator = System.Web.HttpRuntime.Cache.GetEnumerator()
            ' copy all keys that currently exist in Cache
            While enumerator.MoveNext()
                keys.Add(enumerator.Key.ToString())
            End While
            ' delete every key from cache
            For i As Integer = 0 To keys.Count - 1
                System.Web.HttpRuntime.Cache.Remove(keys(i))
            Next
        End Sub
    End Class
End Namespace

