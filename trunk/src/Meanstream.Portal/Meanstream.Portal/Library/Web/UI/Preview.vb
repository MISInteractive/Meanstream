Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.Security

Namespace Meanstream.Portal.Web.UI

    Public Class Preview
        Inherits System.Web.UI.Page

        Dim PageContent As Meanstream.Portal.Core.Content.PageVersion = Nothing

        Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) _
            Handles Me.PreInit

            'Initialize and load master skin from database
            PageContent = New Meanstream.Portal.Core.Content.PageVersion(New Guid(Request.Params(Meanstream.Portal.Core.AppConstants.VERSIONID)))
            Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(PageContent)
            manager.LoadFromDatasource()

            If PageContent.Type = 2 Then
                Response.Redirect("~/" & PageContent.Url)
            ElseIf PageContent.Type = 3 Then
                Response.Redirect(PageContent.Url)
            End If

            Me.Page.MasterPageFile = PageContent.Skin.Path
            Me.Page.Theme = Meanstream.Portal.Core.PortalContext.GetPortalById(PageContent.Skin.PortalId).Theme
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' instead of a literal control maybe it should lode the content control or login control

            If Not Page.IsPostBack Then
                Me.Title = PageContent.MetaTitle
                Dim Keywords As System.Web.UI.HtmlControls.HtmlMeta = Page.Master.FindControl("Keywords")
                Keywords.Attributes("content") = PageContent.MetaKeywords
                Dim Description As System.Web.UI.HtmlControls.HtmlMeta = Page.Master.FindControl("Description")
                Description.Attributes("content") = PageContent.MetaDescription

                'PageContent.PageModuleList.Sort("ModuleOrder")

                'IMPORTANT!!! - Sets the Header ID (PageID) for the page 
                Me.Page.Header.ID = PageContent.Id.ToString
                '''''''''''''''''''''''''''''''''''''''''''''''

                Dim View As Boolean = False
                Dim Roles As List(Of Meanstream.Portal.Core.Membership.Role) = Nothing
                Dim AllUsersRole As Meanstream.Portal.Core.Membership.Role = Core.Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS)

                If Request.IsAuthenticated Then
                    Roles = Core.Membership.MembershipService.Current.GetRolesForUser(HttpContext.Current.Profile.UserName)

                    For Each Role As Meanstream.Portal.Core.Membership.Role In Roles
                        If Me.HasViewPagePermissionsVersion(PageContent.Id, Role.Id) Then
                            View = True
                        End If
                    Next
                Else
                    View = False
                End If

                If View = False Then
                    'go to error page
                    FormsAuthentication.RedirectToLoginPage()
                End If

                Dim PageModules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("IsDeleted=False AND PageVersionId=" & PageContent.Id.ToString)
                PageModules.Sort("DisplayOrder")

                'new
                'for each pane
                For Each SkinPane As Meanstream.Portal.Core.Content.SkinZone In PageContent.Skin.Zones
                    Dim ContentPlaceHolder As ContentPlaceHolder = Nothing
                    ContentPlaceHolder = Master.FindControl(SkinPane.Pane)

                    'New - used for sorting modules on each pane
                    Dim SkinPaneModuleList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = PageModules.FindAll("SkinPaneId", SkinPane.Id)
                    SkinPaneModuleList.Sort("DisplayOrder")

                    'recurse the the sorted module list for the page
                    For Each PageModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In SkinPaneModuleList

                        If SkinPane.Id = PageModule.SkinPaneId Then

                            If Request.IsAuthenticated Then
                                For Each Role As Meanstream.Portal.Core.Membership.Role In Roles
                                    If Me.HasViewModulePermissionsVersion(PageModule.Id, Role.Id) Then
                                        View = True
                                    End If
                                Next
                            Else
                                If Me.HasViewModulePermissionsVersion(PageModule.Id, AllUsersRole.Id) Then
                                    View = True
                                End If
                            End If

                            If View Then
                                Dim Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Nothing
                                For Each WidgetBase As Meanstream.Portal.Core.WidgetFramework.WidgetVersion In PageContent.Widgets
                                    If WidgetBase.Id = PageModule.Id Then
                                        Widget = WidgetBase
                                        Exit For
                                    End If
                                Next

                                ContentPlaceHolder.Controls.Add(Widget.UserControl)
                            End If
                        End If
                    Next

                Next

            End If

        End Sub

        Private Function HasViewPagePermissionsVersion(ByVal VersionID As Guid, ByVal RoleId As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" & VersionID.ToString & " AND RoleId=" & RoleId.ToString & " AND PermissionId=" & Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id.ToString).Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Private Function HasViewModulePermissionsVersion(ByVal ModuleID As Guid, ByVal RoleId As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("RoleId=" & RoleId.ToString & " AND PermissionId=" & Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_VIEW).Id.ToString & " AND ModuleId=" & ModuleID.ToString).Count = 0 Then
                Return False
            End If
            Return True
        End Function
    End Class

End Namespace

