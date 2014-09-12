Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Web.Security
Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Portal.Core

Namespace Meanstream.Portal.Web.UI

    Public Class Page
        Inherits System.Web.UI.Page

        Dim pageBase As Meanstream.Portal.Core.Entities.MeanstreamPage = Nothing

        Private Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Me.PreInit
            Dim PageID As String = Meanstream.Portal.Core.PortalContext.Current.SiteRelativePath 'Request.Params(Meanstream.Portal.Core.AppConstants.PAGEID)
            PortalTrace.Fail("Requesting.... " & PageID, DisplayMethodInfo.FullSignature)
            Try
                pageBase = Me.GetPageCache(PageID)
                'undo the hardcoding
                Me.Page.MasterPageFile = Me.GetSkinCache(pageBase.SkinId).SkinSrc
                Me.Page.Theme = Meanstream.Portal.Core.PortalContext.Current.Portal.Theme
                Me.Page.EnableViewState = pageBase.EnableViewState
            Catch ex As Exception
                PortalTrace.Fail(ex.Message, DisplayMethodInfo.FullSignature)
                HttpContext.Current.Response.StatusCode = 404
                HttpContext.Current.Response.End()
            End Try
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ViewFlag As Boolean = False
            Dim EditFlag As Boolean = False

            'IMPORTANT!!! - Sets the Header ID (PageID) for the page 
            Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
            Me.Page.Header.ID = pageBase.Id.ToString
            '''''''''''''''''''''''''''''''''''''''''''''''
            If pageBase.IsDeleted Then
                FormsAuthentication.RedirectToLoginPage()
            End If

            Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.AspnetRoles) = Nothing
            Dim pagePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission) = Me.GetPagePermissionCache(pageBase.Id)

            'get cached role
            Dim AllUsersRole As Meanstream.Portal.Core.Membership.Role = Me.GetAllUsersRoleCache
            If Request.IsAuthenticated Then
                'get cached roles for the user
                Roles = Me.GetUserRoleCache(System.Web.HttpContext.Current.Profile.UserName)
                For Each Role As Meanstream.Portal.Core.Entities.AspnetRoles In Roles
                    If Me.HasViewPagePermissions(pagePermissions, Role.RoleId) Then
                        ViewFlag = True
                        Exit For
                    End If
                Next
            Else
                If Me.HasViewPagePermissions(pagePermissions, AllUsersRole.Id) Then
                    ViewFlag = True
                End If
            End If

            'If PageBase.Id = 1 Then
            '    ViewFlag = True
            'End If

            If ViewFlag = False Then
                FormsAuthentication.RedirectToLoginPage()
            End If

            'If Not Page.IsPostBack Then
            Me.Title = pageBase.Title

            Dim Keywords As System.Web.UI.HtmlControls.HtmlMeta = Page.Master.FindControl("Keywords")
            If Keywords IsNot Nothing Then
                Keywords.Content = pageBase.KeyWords
            End If

            Dim Description As System.Web.UI.HtmlControls.HtmlMeta = Page.Master.FindControl("Description")
            If Description IsNot Nothing Then
                Description.Content = pageBase.Description
            End If

            If Request.Params("meanstream_disable_link_action") <> Nothing Then
                Dim DisableLinks As LiteralControl = New LiteralControl
                DisableLinks.ID = "DisableLinks"
                DisableLinks.Text = "<script type='text/javascript' language='javascript'>setTimeout('disableLinks()',3000); function disableLinks() { var c = document.links; for (var i=0; i<c.length; i++) { c[i].title = c[i].href; c[i].href = '#'; } disableSubmitButtons(document.getElementsByTagName('INPUT')); disableSubmitButtons(document.getElementsByTagName('BUTTON')); } function disableSubmitButtons(c) { for (var i=0; i<c.length; i++) { if (c[i].type == 'submit') c[i].disabled = true; if (c[i].type == 'image') c[i].disabled = true; } } </script>"
                Me.Header.Controls.Add(DisableLinks)
            End If

            Dim modules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModule) = Me.GetModules(pageBase.Id)
            Dim widgets As List(Of Meanstream.Portal.Core.WidgetFramework.Widget) = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetsByPageId(pageBase.Id)

            'for each pane
            For Each SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane In Me.GetZoneCache(pageBase.SkinId)
                Dim ContentPlaceHolder As ContentPlaceHolder = Nothing
                Dim LiteralControl As LiteralControl = New LiteralControl
                ContentPlaceHolder = Master.FindControl(SkinPane.Pane)

                'Used for sorting modules on each pane
                Dim SkinPaneModuleList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModule) = modules.FindAll("SkinPaneId", SkinPane.Id)
                SkinPaneModuleList.Sort("DisplayOrder")

                'PortalTrace.WriteLine("Meanstream.Portal.Core.Web.Page(): modules=" & SkinPaneModuleList.Count)

                'recurse the the sorted module list for the page
                For Each PageModule As Meanstream.Portal.Core.Entities.MeanstreamModule In SkinPaneModuleList

                    If SkinPane.Id = PageModule.SkinPaneId Then

                        ViewFlag = False
                        EditFlag = False

                        'issue with extra queries here; however, this will be cached after the initial load
                        Dim widget As Meanstream.Portal.Core.WidgetFramework.Widget = Nothing
                        For Each widgetBase As Meanstream.Portal.Core.WidgetFramework.Widget In widgets
                            If widgetBase.Id = PageModule.Id Then
                                widget = widgetBase
                                Exit For
                            End If
                        Next

                        Dim widgetPermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModulePermission) = Me.GetWidgetPermissions(widget.Id)

                        If Request.IsAuthenticated Then
                            For Each Role As Meanstream.Portal.Core.Entities.AspnetRoles In Roles
                                If Me.HasViewModulePermissions(widgetPermissions, widget.Id, Role.RoleId) Then
                                    ViewFlag = True
                                End If
                            Next
                        Else
                            If Me.HasViewModulePermissions(widgetPermissions, widget.Id, AllUsersRole.Id) Then
                                ViewFlag = True
                            End If
                        End If

                        'PortalTrace.WriteLine("Meanstream.Portal.Core.Web.Page(): Widget=" & widget.Id.ToString & " View=" & ViewFlag)

                        If ViewFlag Then
                            If SkinPane.Id = PageModule.SkinPaneId Then
                                Dim PanelControl As Panel = New Panel
                                PanelControl.Controls.Add(widget.UserControl)
                                If ContentPlaceHolder IsNot Nothing Then
                                    ContentPlaceHolder.Controls.Add(PanelControl)
                                End If
                            End If
                        End If

                    End If
                Next
            Next

            Dim gaSetting As Core.Extensibility.Setting = Meanstream.Portal.Core.Extensibility.Setting.GetGoogleAnalyticsScript(portalId)
            If gaSetting IsNot Nothing Then
                gaSetting.Value = "<script type='text/javascript'>" & gaSetting.Value & "</script>"
                Me.Page.Master.Controls.Add(New LiteralControl(gaSetting.Value))
            End If
        End Sub

        Private Function GetUserRoleCache(ByVal Username As String) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.AspnetRoles)
            Dim Roles As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.AspnetRoles) = Nothing
            If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                Roles = Cache(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES & "_ENTITIES_" & Username)
            End If
            If Roles Is Nothing Then
                Dim UserID As Guid = Meanstream.Portal.Core.Data.DataRepository.AspnetUsersProvider.Find("UserName=" & Username).Item(0).UserId()
                Roles = Meanstream.Portal.Core.Data.DataRepository.AspnetRolesProvider.GetByUserIdFromAspnetUsersInRoles(UserID)
                If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                    Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.USERROLES & "_ENTITIES_" & Username, Roles, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
            End If
            Return Roles
        End Function

        Private Function GetAllUsersRoleCache() As Meanstream.Portal.Core.Membership.Role
            Dim Roles As Meanstream.Portal.Core.Membership.Role = Nothing
            If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                Roles = Cache(Meanstream.Portal.Core.AppConstants.ALLUSERS)
            End If
            If Roles Is Nothing Then
                Roles = Core.Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS)
                If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                    Cache.Insert(Meanstream.Portal.Core.AppConstants.ALLUSERS, Roles, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
            End If
            Return Roles
        End Function

        Private Function GetSkinCache(ByVal skinId As Guid) As Meanstream.Portal.Core.Entities.MeanstreamSkins
            Dim portalId As Guid = PortalContext.Current.PortalId
            Dim skin As Meanstream.Portal.Core.Entities.MeanstreamSkins = Nothing
            If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                skin = Cache(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ENTITY_" & skinId.ToString & "_PORTAL_" & portalId.ToString)
            End If
            If skin Is Nothing Then
                skin = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.GetById(skinId)
                If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                    Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ENTITY_" & skinId.ToString & "_PORTAL_" & portalId.ToString, skin, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
            End If
            Return skin
        End Function

        Private Function GetZoneCache(ByVal skinId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane)
            Dim portalId As Guid = PortalContext.Current.PortalId
            Dim zones As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = Nothing
            If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                zones = Cache(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ZONE_ENTITY_" & skinId.ToString & "_PORTAL_" & portalId.ToString)
            End If
            If zones Is Nothing Then
                zones = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Find("SkinId=" & skinId.ToString)
                If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                    Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_SKIN_ZONE_ENTITY_" & skinId.ToString & "_PORTAL_" & portalId.ToString, zones, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
            End If
            Return zones
        End Function

        Private Function GetPagePermissionCache(ByVal pageId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission)
            Dim portalId As Guid = PortalContext.Current.PortalId
            Dim permissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission) = Nothing
            If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                permissions = Cache(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_PAGE_PERMISSION_ENTITY_" & pageId.ToString & "_PORTAL_" & portalId.ToString)
            End If
            If permissions Is Nothing Then
                permissions = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.Find("PageId=" & pageId.ToString)
                If pageBase.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                    Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_PAGE_PERMISSION_ENTITY_" & pageId.ToString & "_PORTAL_" & portalId.ToString, permissions, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
            End If
            Return permissions
        End Function

        Private Function GetModules(ByVal pageId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModule)
            Dim Query As Meanstream.Portal.Core.Data.MeanstreamModuleQuery = New Meanstream.Portal.Core.Data.MeanstreamModuleQuery
            Query.AppendEquals(Core.Entities.MeanstreamModuleColumn.PageId, pageId.ToString)
            Query.AppendLessThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamModuleColumn.StartDate, Date.Now.ToString)
            Query.AppendGreaterThanOrEqual("AND", Core.Entities.MeanstreamModuleColumn.EndDate, Date.Now.ToString)
            Dim Modules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModule) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Find(Query.Parameters)
            Modules.Sort("DisplayOrder")
            Return Modules
        End Function

        Private Function GetPageCache(ByVal url As String) As Meanstream.Portal.Core.Entities.MeanstreamPage
            Dim portalId As Guid = PortalContext.Current.PortalId
            Dim page As Meanstream.Portal.Core.Entities.MeanstreamPage = Nothing
            'If ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
            '    page = Cache(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_ENTITY_" & url & "_PORTAL_" & portalId.ToString)
            'End If

            If page IsNot Nothing Then
                Return page
            End If

            If url Is Nothing Or url = "index" Or url = "default.aspx" Then
                Try
                    Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" & portalId.ToString & " AND IsHome=True")(0)
                Catch ex As Exception
                    PortalTrace.Fail([String].Concat("GetPageCache() ", "Home page has not been assigned url=" & url & " Exception: " & ex.Message), DisplayMethodInfo.FullSignature)
                End Try
            End If
            'handle returnURL issue (.net returns ',' with paramaters) 
            If url.Contains(",") Then
                url = url.Split(",")(0)
            End If
            Dim Query As Meanstream.Portal.Core.Data.MeanstreamPageQuery = New Meanstream.Portal.Core.Data.MeanstreamPageQuery
            Query.AppendEquals(Core.Entities.MeanstreamPageColumn.Url, url.Trim)
            Query.AppendEquals("AND", Core.Entities.MeanstreamPageColumn.Type, "1")
            Query.AppendEquals("AND", Core.Entities.MeanstreamPageColumn.PortalId, portalId.ToString)
            Try

                page = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(Query.GetParameters)(0)
                If page.EnableCaching And ConfigurationManager.AppSettings("Meanstream.EnableCaching") Then
                    PortalTrace.WriteLine("Caching.... " & url)
                    Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & "_ENTITY_" & url & "_PORTAL_" & portalId.ToString, page, Nothing, DateTime.Now.AddHours(ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
                Return page
            Catch ex As Exception
                PortalTrace.Fail([String].Concat("GetPageCache() ", " PAGE NOT FOUND " & url), DisplayMethodInfo.DoNotDisplay)
            End Try
            Return page
        End Function

        Private Function GetWidgetPermissions(ByVal widgetId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModulePermission)
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Find("ModuleId=" & widgetId.ToString)
        End Function

        Private Function HasViewPagePermissions(ByVal PagePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission), ByVal RoleId As Guid) As Boolean
            PagePermissions = PagePermissions.FindAll("PermissionId", Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id)
            If PagePermissions.Count = 0 Then
                Return False
            End If
            Dim PagePermission As Meanstream.Portal.Core.Entities.MeanstreamPagePermission = PagePermissions.Find("RoleId", RoleId)
            If PagePermission Is Nothing Then
                Return False
            End If
            Return True
        End Function

        Private Function HasViewModulePermissions(ByVal ModulePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModulePermission), ByVal ModuleId As Guid, ByVal RoleId As Guid) As Boolean
            ModulePermissions = ModulePermissions.FindAll("ModuleId", ModuleId)
            If ModulePermissions Is Nothing Then
                Return False
            End If
            ModulePermissions = ModulePermissions.FindAll("PermissionId", Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id)
            If ModulePermissions Is Nothing Then
                Return False
            End If
            Dim ModulePermission As Meanstream.Portal.Core.Entities.MeanstreamModulePermission = ModulePermissions.Find("RoleId", RoleId)
            If ModulePermission Is Nothing Then
                Return False
            End If
            Return True
        End Function
    End Class
End Namespace

