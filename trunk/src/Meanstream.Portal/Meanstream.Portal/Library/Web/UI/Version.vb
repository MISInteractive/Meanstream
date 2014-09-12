Imports Microsoft.VisualBasic
Imports System.Web.UI
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.Security

<Assembly: System.Web.UI.WebResource("pane-bar-bg.jpg", "image/gif")> 
<Assembly: System.Web.UI.WebResource("drop-bg.jpg", "image/gif")> 

Namespace Meanstream.Portal.Web.UI

    Public Class Version
        Inherits System.Web.UI.Page

        Dim PageVersionBase As Meanstream.Portal.Core.Content.PageVersion = Nothing

        Public Overrides Property StyleSheetTheme() As String
            Get
                Dim MeanstreamTheme As String = Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(HttpContext.Current.Profile.UserName)
                Return MeanstreamTheme
            End Get
            Set(ByVal value As String)
                MyBase.StyleSheetTheme = value
            End Set
        End Property

        Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) _
            Handles Me.PreInit
            PageVersionBase = New Meanstream.Portal.Core.Content.PageVersion(New Guid(Request.Params(Meanstream.Portal.Core.AppConstants.VERSIONID).ToString))
            Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(PageVersionBase)
            manager.LoadFromDatasource()
            Me.Page.MasterPageFile = PageVersionBase.Skin.Path
            Me.Page.Theme = Meanstream.Portal.Core.PortalContext.GetPortalById(PageVersionBase.Skin.PortalId).Theme
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            
            Dim MeanstreamTheme As String = Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(HttpContext.Current.Profile.UserName)

            'Check to see if ScriptManager exists
            Dim ScriptManagerFlag As Boolean = False
            For Each Control As Control In Me.Form.Controls
                If TypeOf (Control) Is Meanstream.Web.UI.ScriptManager Then
                    Dim ScriptManager As ScriptManager = CType(Control, Meanstream.Web.UI.ScriptManager)
                    ScriptManager.ScriptMode = ScriptMode.Release
                    'add our widget service
                    Dim WidgetService As ServiceReference = New ServiceReference
                    WidgetService.InlineScript = "false"
                    WidgetService.Path = "~/Meanstream/Widgets/WidgetService.asmx"
                    ScriptManager.Services.Add(WidgetService)
                    'add our widget js
                    Dim WidgetJS As ScriptReference = New ScriptReference
                    WidgetJS.Assembly = "Meanstream.Portal"
                    WidgetJS.Name = "Meanstream.Portal.Web.UI.widget.js"
                    ScriptManager.Scripts.Add(WidgetJS)
                    'add our page editor js
                    Dim PageEditorJS As ScriptReference = New ScriptReference
                    PageEditorJS.Assembly = "Meanstream.Portal"
                    PageEditorJS.Name = "Meanstream.Portal.Web.UI.pageEditor.js"
                    ScriptManager.Scripts.Add(PageEditorJS)
                    'add our page editor js
                    'Dim JQuery As ScriptReference = New ScriptReference
                    'JQuery.Path = "~/Scripts/jquery-1.4.2.min.js"
                    'JQuery.Assembly = "Meanstream.Portal"
                    'JQuery.Name = "jquery-1.4.2.min.js"
                    'ScriptManager.Scripts.Add(JQuery)

                    ScriptManagerFlag = True
                    Exit For
                End If
            Next

            If Not ScriptManagerFlag Then
                Throw New Exception("Meanstream.Web.UI.ScriptManager is Required.")
            End If

            'Add style
            Me.Page.Header.Controls.AddAt(0, New LiteralControl("<style type='text/css'>.paneheader { z-index: 1; position: relative; text-align: left; width: 100%; background-image: url(" & Page.ClientScript.GetWebResourceUrl(GetType(Meanstream.Portal.Web.UI.Version), "pane-bar-bg.jpg") & "); font-family: Arial, Verdana, sans-serif; font-size: 12px; font-style: normal; color: #737373; font-weight: bold; TEXT-DECORATION: none; } .meanstream_web_ui_dragdrophandle { cursor: move; z-index: 9998; position: relative; } .meanstream_web_ui_dragdropcontent { z-index: 9998; position: relative; } .meanstream_web_ui_dragdrop { z-index: 9998; position: relative; } .meanstream_web_ui_dragdropcue { z-index: 9998; position: relative; background: transparent; min-width: 5px; min-height: 20px; height: auto !important; border:1px dashed #CCC; } .meanstream_web_ui_dragdropcue  .placeholder { background: #ccc; z-index: 1; position: relative; } .controlfunctions { z-index: 1; position: relative; float: left; align: left; width: 100%; height: 100%; background-color: #000000; opacity: .60; filter: alpha(opacity=70); MozOpacity=.60; } .controlmenuitem a{ float: left; align: left; font-family: Arial, Verdana, sans-serif; font-size: 12px; font-style: normal; color: #d5d5d5; font-weight: bold; TEXT-DECORATION: none; } .controlmenuitem a:visited{ float: left; align: left; font-family: Arial, Verdana, sans-serif; font-size: 12px; font-style: normal; color: #d5d5d5; font-weight: bold; TEXT-DECORATION: none; } .controlmenuitem a:hover{ float: left; align: left; font-family: Arial, Verdana, sans-serif; font-size: 12px; font-style: normal; color: #ffffff; font-weight: bold; TEXT-DECORATION: underline; } </style>"))

            ' instead of a literal control maybe it should lode the content control or login control
            Dim ViewFlag As Boolean = False
            Dim EditFlag As Boolean = False

            'IMPORTANT!!! - Sets the Header ID (VersionID) for the page 
            Me.Page.Header.ID = PageVersionBase.Id.ToString
            '''''''''''''''''''''''''''''''''''''''''''''''

            Dim Roles As List(Of Meanstream.Portal.Core.Membership.Role) = Nothing
            Dim AllUsersRole As Meanstream.Portal.Core.Membership.Role = Core.Membership.MembershipService.Current.GetRoleByName(Meanstream.Portal.Core.AppConstants.ALLUSERS)

            If Request.IsAuthenticated Then
                Roles = Core.Membership.MembershipService.Current.GetRolesForUser(HttpContext.Current.Profile.UserName)

                For Each Role As Meanstream.Portal.Core.Membership.Role In Roles
                    If Me.HasViewPagePermissionsVersion(PageVersionBase.Id, Role.Id) Then
                        ViewFlag = True
                    End If

                    If Me.HasEditPagePermissionsVersion(PageVersionBase.Id, Role.Id) Or Core.Membership.MembershipService.Current.IsUserInRole(HttpContext.Current.Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR) Then
                        EditFlag = True
                    End If
                Next
            Else
                ViewFlag = False
                EditFlag = False
            End If

            If ViewFlag = False Then
                'go to error page
                FormsAuthentication.RedirectToLoginPage()
            End If

            Me.Title = PageVersionBase.MetaTitle

            Dim Keywords As System.Web.UI.HtmlControls.HtmlMeta = Page.Master.FindControl("Keywords")
            If Keywords IsNot Nothing Then
                Keywords.Attributes("content") = PageVersionBase.MetaKeywords
            End If

            Dim Description As System.Web.UI.HtmlControls.HtmlMeta = Page.Master.FindControl("Description")
            If Description IsNot Nothing Then
                Description.Attributes("content") = PageVersionBase.MetaDescription
            End If

            Dim DragDropManager As Meanstream.Web.UI.DragDropManager = New Meanstream.Web.UI.DragDropManager
            DragDropManager.ID = "DragDropManager"
            DragDropManager.OnClientDrop = "OnDrop"
            'DragDropManager.Theme = "Black" 'MeanstreamTheme.Replace("Meanstream.", "")
            Me.Page.Form.Controls.AddAt(1, DragDropManager)

            Dim ZoneArray As ArrayList = New ArrayList

            Dim PageModules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Find("IsDeleted=False AND PageVersionId=" & PageVersionBase.Id.ToString)
            PageModules.Sort("DisplayOrder")

            'for each pane
            For Each SkinPane As Meanstream.Portal.Core.Content.SkinZone In PageVersionBase.Skin.Zones
                Dim ContentPlaceHolder As ContentPlaceHolder = Nothing
                Dim LiteralControl As LiteralControl = New LiteralControl
                ContentPlaceHolder = Master.FindControl(SkinPane.Pane)

                ContentPlaceHolder.Controls.AddAt(0, New LiteralControl("<div pane='paneheader' class='paneheader'>&nbsp;&nbsp;" & SkinPane.Pane & "</div>"))


                Dim Zone As Meanstream.Web.UI.DragDropZone = New Meanstream.Web.UI.DragDropZone
                Zone.ID = SkinPane.Id.ToString.Replace("-", "___")
                Zone.DragDropManagerID = DragDropManager.ID

                ContentPlaceHolder.Controls.Add(Zone)
                ZoneArray.Add(Zone.ClientID)

                'New - used for sorting modules on each pane
                Dim SkinPaneModuleList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleVersion) = PageModules.FindAll("SkinPaneId", SkinPane.Id)
                SkinPaneModuleList.Sort("DisplayOrder")

                'recurse the the sorted module list for the page
                For Each PageModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion In SkinPaneModuleList

                    ViewFlag = False
                    EditFlag = False

                    If Request.IsAuthenticated Then
                        For Each Role As Meanstream.Portal.Core.Membership.Role In Roles
                            If Me.HasViewModulePermissionsVersion(PageModule.Id, Role.Id) Then
                                ViewFlag = True
                            End If

                            If Me.HasEditModulePermissionsVersion(PageModule.Id, Role.Id) Or Core.Membership.MembershipService.Current.IsUserInRole(HttpContext.Current.Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR) Then
                                EditFlag = True
                            End If
                        Next
                    Else
                        If Me.HasViewModulePermissionsVersion(PageModule.Id, AllUsersRole.Id) Then
                            ViewFlag = True
                        End If
                    End If

                    If ViewFlag Then

                        If SkinPane.Id = PageModule.SkinPaneId Then

                            Dim Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = Nothing
                            For Each WidgetBase As Meanstream.Portal.Core.WidgetFramework.WidgetVersion In PageVersionBase.Widgets
                                If WidgetBase.Id = PageModule.Id Then
                                    Widget = WidgetBase
                                    Exit For
                                End If
                            Next

                            If EditFlag Then
                                Dim WidgetContainer As Panel = New Panel
                                WidgetContainer.Attributes.Add("style", "z-index: 9998; position: relative; border:1px dashed #CCC;")

                                Dim DragDropItem As Meanstream.Web.UI.DragDropItem = New Meanstream.Web.UI.DragDropItem
                                DragDropItem.ID = PageModule.Id.ToString.Replace("-", "___")
                                DragDropItem.HandleTemplate = New Meanstream.Web.UI.DragDropHandleTemplate(WidgetContainer)
                                DragDropItem.ContentTemplate = New Meanstream.Web.UI.DragDropContentTemplate(New LiteralControl(""))
                                Zone.Items.Add(DragDropItem)

                                WidgetContainer.Controls.Add(Widget.UserControl)
                            Else
                                Zone.Controls.Add(Widget.UserControl)

                            End If
                        End If
                    End If

                Next

            Next

            Me.OnDrop(DragDropManager.ID)
            'Call the gray out only at the end of control rendering
            Dim CallGrayOut As LiteralControl = New LiteralControl
            CallGrayOut.ID = "CallGrayOut"
            CallGrayOut.Text = "<script language='javascript' type='text/javascript'>grayOut(true, {'zindex':'0'}); </script>"
            Me.Page.Master.Controls.AddAt(Page.Master.Controls.Count, CallGrayOut)
        End Sub

        Private Sub OnDrop(ByVal DragDropManagerID As String)
            Dim Script As String = ""
            Script = "<script language='javascript' type='text/javascript'>" + Constants.vbLf
            Script = Script & "$(document).ready(function() {" + Constants.vbLf
            Script = Script & "function OnDrop(event, ui) {" + Constants.vbCrLf
            Script = Script & "$('div[manager=" & DragDropManagerID & "]').each(function() {" + Constants.vbCrLf
            Script = Script & "var itemorder = $(this).sortable('toArray');" + Constants.vbCrLf
            Script = Script & "var zone = $(this).attr('id');" + Constants.vbCrLf
            Script = Script & "var zoneId = zone.split('_')[zone.split('_').length - 2];" + Constants.vbCrLf
            Script = Script & "var itemsInZone = itemorder.toString().split(',');" + Constants.vbCrLf
            Script = Script & "for (i = 0; i < itemsInZone.length; i++) {" + Constants.vbCrLf
            Script = Script & "var widget = itemsInZone[i];" + Constants.vbCrLf
            Script = Script & "var widgetId = widget.split('_')[widget.split('_').length - 2];" + Constants.vbCrLf
            Script = Script & "var params = new Object();" + Constants.vbCrLf
            Script = Script & "params.FormControlId = widgetId;" + Constants.vbCrLf
            Script = Script & "params.SkinPaneId = zoneId;" + Constants.vbCrLf
            Script = Script & "params.DisplayOrder = i;" + Constants.vbCrLf
            Script = Script & "if(params.FormControlId!=null) {" + Constants.vbCrLf
            'Script = Script & "alert('zone(' + zone + ') ' + params.FormControlId + ',' + params.SkinPaneId + ',' + params.DisplayOrder);" + Constants.vbCrLf
            Script = Script & "Meanstream.Portal.Web.Services.WidgetService.Move(params.FormControlId, params.SkinPaneId, params.DisplayOrder);" + Constants.vbCrLf
            Script = Script & "}" + Constants.vbCrLf
            Script = Script & "}" + Constants.vbCrLf

            Script = Script & "});" + Constants.vbCrLf
            Script = Script & " }" + Constants.vbCrLf
            Script = Script & "});" + Constants.vbLf
            Script = Script & "</script>" + Constants.vbLf
            Me.Page.Master.Controls.AddAt(Page.Master.Controls.Count, New LiteralControl(Script))
        End Sub

        Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            Dim WindowContainer As Panel = New Panel
            WindowContainer.CssClass = "windowContainer"
            Page.Form.Controls.Add(WindowContainer)
            Dim s As String = ""
            s = s + "<script>" + Constants.vbLf
            s = s + "$(document).ready(function() {" + Constants.vbLf
            's = s + "$(parent.document.body).append(""<script> $(document).ready(function () { var iframe = $('#editFrame'); $('#editFrame').load(function () { var h = $(window).height(); var w = $(window).width(); var cssObj = { 'height': h + 'px', 'width': w + 'px' }; iframe.css(cssObj); }); $(window).bind('resize', resizeIframe); function resizeIframe() { var h = $(window).height(); var w = $(window).width(); var cssObj = { 'height': h + 'px', 'width': w + 'px' }; iframe.css(cssObj); } }); </scr""+""ipt>"");" + Constants.vbLf
            's = s + "$(parent.document.body).append(""<script>alert('here')</scr""+""ipt>"");" + Constants.vbLf
            s = s + "$('.windowContainer').append($('.Black'));" + Constants.vbLf
            s = s + "});" + Constants.vbLf
            s = s + "</script>" + Constants.vbLf
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "initDom", s, False)
            Me.Page.RegisterStartupScript("initDom", s)


            's = s + "var parentWindow = $(parent.document);" + Constants.vbLf
            's = s + "var iframe = parent.$('#editFrame');" + Constants.vbLf
            's = s + "parent.$('#editFrame').load(function () {" + Constants.vbLf
            's = s + "     var h = parentWindow.height();" + Constants.vbLf
            's = s + "     var w = parentWindow.width();" + Constants.vbLf
            's = s + "     var cssObj = { 'height': '777px', 'width': w + 'px' };" + Constants.vbLf ''width': w + 'px'
            's = s + "     iframe.css(cssObj);" + Constants.vbLf
            's = s + " });" + Constants.vbLf
            's = s + " $(parent.window).bind('resize', parent.frames.editFrame.resizeIframe);" + Constants.vbLf
            's = s + " function resizeIframe() {" + Constants.vbLf
            's = s + "alert(parentWindow.height());" + Constants.vbLf
            's = s + "     var h = parentWindow.height();" + Constants.vbLf
            's = s + "     var w = parentWindow.width();" + Constants.vbLf
            's = s + "     //resize editFrame" + Constants.vbLf
            's = s + "     var cssObj = { 'height': '777px', 'width': w + 'px' };" + Constants.vbLf
            's = s + "     iframe.css(cssObj);" + Constants.vbLf
            's = s + " }" + Constants.vbLf

        End Sub

        Private Function HasEditPagePermissionsVersion(ByVal VersionID As Guid, ByVal RoleId As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" & VersionID.ToString & " AND RoleId=" & RoleId.ToString & " AND PermissionId=" & Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_EDIT).Id.ToString).Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Private Function HasViewPagePermissionsVersion(ByVal VersionID As Guid, ByVal RoleId As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionVersionProvider.Find("VersionId=" & VersionID.ToString & " AND RoleId=" & RoleId.ToString & " AND PermissionId=" & Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_PAGE_VIEW).Id.ToString).Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Private Function HasEditModulePermissionsVersion(ByVal ModuleID As Guid, ByVal ID As Guid) As Boolean
            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionPermissionProvider.Find("RoleId=" & ID.ToString & " AND PermissionId=" & Core.Membership.MembershipService.Current.GetPermission(Meanstream.Portal.Core.Membership.Permission.PermissionType.SYSTEM_MODULE_EDIT).Id.ToString & " AND ModuleId=" & ModuleID.ToString).Count = 0 Then
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


