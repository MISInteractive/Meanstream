Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace Meanstream.Portal.Web.UI

    <ParseChildren(True)> _
    Public Class WidgetEditBase
        Inherits Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl

        Sub New()
            _EditMenuItems = New WidgetEditMenuItemCollection
            _AllowDelete = True
            _ShowSettings = True
        End Sub

        Private _AllowDelete As Boolean
        Public Property AllowDelete() As Boolean
            Get
                Return _AllowDelete
            End Get
            Set(ByVal value As Boolean)
                _AllowDelete = value
            End Set
        End Property

        Private _ShowSettings As Boolean
        Public Property ShowSettings() As Boolean
            Get
                Return _ShowSettings
            End Get
            Set(ByVal value As Boolean)
                _ShowSettings = value
            End Set
        End Property

        Private _previewMode As Boolean = False
        Public Property PreviewMode() As Boolean
            Get
                Return _previewMode
            End Get
            Set(ByVal value As Boolean)
                _previewMode = value
            End Set
        End Property

        Dim _EditMenuItems As WidgetEditMenuItemCollection
        <PersistenceMode(PersistenceMode.InnerProperty)> _
        Public Property EditMenuItems() As WidgetEditMenuItemCollection
            Get
                Return _EditMenuItems
            End Get
            Set(ByVal value As WidgetEditMenuItemCollection)
                _EditMenuItems = value
            End Set
        End Property

        Protected Overrides Sub CreateChildControls()
            MyBase.Controls.Clear()

            If Request.Params("PreviewMode") <> Nothing Then
                If Request.Params("PreviewMode") = "true" Then
                    PreviewMode = True
                End If
            End If

            If PreviewMode Then
                Controls.Add(Me.Content)
                Exit Sub
            End If

            If Not ChildControlsCreated Then
                Dim MeanstreamTheme As String = Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(HttpContext.Current.Profile.UserName)

                Dim MenuPanel As Panel = New Panel
                MenuPanel.ID = "MenuPanel_" & WidgetId.ToString.Replace("-", "___")
                MenuPanel.CssClass = "controlfunctions"
                MenuPanel.HorizontalAlign = HorizontalAlign.Left
                'add panel to base control
                Controls.Add(MenuPanel)

                Dim ContentPanel As Panel = New Panel
                ContentPanel.ID = "ContentPanel_" & WidgetId.ToString.Replace("-", "___")
                ContentPanel.HorizontalAlign = HorizontalAlign.Left
                'add panel to base control
                Controls.Add(ContentPanel)

                Dim Table As Table = New Table
                Table.HorizontalAlign = HorizontalAlign.Left
                MenuPanel.Controls.Add(Table)

                Dim TableRow As TableRow = New TableRow
                Table.Rows.Add(TableRow)

                'add items to panel
                For Each WidgetEditMenuItem As WidgetEditMenuItem In Me.EditMenuItems

                    'missing
                    'WidgetEditMenuItem.Theme = MeanstreamTheme.Replace("Meanstream.", "")
                    
                    'stylesheet
                    Dim _root As String = Context.Request.ApplicationPath.ToString
                    If _root = "/" Then
                        _root = ""
                    End If

                    WidgetEditMenuItem.CssClass = MeanstreamTheme
                    'WidgetEditMenuItem.StyleSheetPath = _root + "/App_Themes/" + WidgetEditMenuItem.Theme + "/web.ui.css"

                    Dim TableCell As TableCell = New TableCell
                    TableCell.CssClass = "controlmenuitem"

                    WidgetEditMenuItem.ID = "WidgetEditMenuItem_" & WidgetId.ToString.Replace("-", "___")

                    Dim LinkButton As LinkButton = New LinkButton
                    LinkButton.ID = WidgetId.ToString.Replace("-", "___") & "_" & WidgetEditMenuItem.ID
                    LinkButton.Text = WidgetEditMenuItem.Text

                    TableCell.Controls.Add(LinkButton)

                    TableCell.Controls.Add(WidgetEditMenuItem)

                    TableRow.Cells.Add(TableCell)

                    If EditMenuItems.IndexOf(WidgetEditMenuItem) = EditMenuItems.Count - 1 And Not ShowSettings Then
                        Exit For
                    End If

                    Dim Divider As TableCell = New TableCell
                    Divider.Text = "&nbsp;|&nbsp;"
                    TableRow.Cells.Add(Divider)

                    WidgetEditMenuItem.TargetControlId = LinkButton.ClientID
                Next

                If ShowSettings Then
                    Dim TableCell As TableCell = New TableCell
                    TableCell.CssClass = "controlmenuitem"
                    ''add settings
                    Dim Settings As WidgetEditMenuItem = New WidgetEditMenuItem
                    Settings.ID = "btnSettings_" & WidgetId.ToString.Replace("-", "___")
                    'missing
                    'Settings.Theme = MeanstreamTheme.Replace("Meanstream.", "")

                    Settings.Text = "Settings"
                    Settings.Width = "530"
                    Settings.Height = "460"
                    Settings.Title = "Settings"
                    Settings.ShowLoader = True
                    Settings.ShowUrl = True
                    Settings.NavigateUrl = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "Meanstream/Widgets/Settings/Module.aspx?ctl=Default&ModuleId=" & WidgetId.ToString

                    'stylesheet
                    Dim _root As String = Context.Request.ApplicationPath.ToString
                    If _root = "/" Then
                        _root = ""
                    End If

                    'Settings.StyleSheetPath = _root + "/App_Themes/" + Settings.Theme + "/web.ui.css"

                    Dim LinkButton As LinkButton = New LinkButton
                    LinkButton.ID = WidgetId.ToString.Replace("-", "___") & "_" & Settings.ID
                    LinkButton.Text = Settings.Text

                    TableCell.Controls.Add(LinkButton)

                    TableCell.Controls.Add(Settings)

                    TableRow.Cells.Add(TableCell)

                    Settings.TargetControlId = LinkButton.ClientID
                End If

                If AllowDelete Then
                    Dim Divider As TableCell = New TableCell
                    Divider.Text = "&nbsp;|&nbsp;"
                    TableRow.Cells.Add(Divider)

                    Dim TableCell As TableCell = New TableCell
                    TableCell.CssClass = "controlmenuitem"
                    'add Delete
                    Dim Delete As HyperLink = New HyperLink
                    Delete.ID = "btnDelete"
                    Delete.Text = "Delete"
                    'Delete.ImageUrl = Meanstream.Portal.Core.Utilities.ApplicationUtil.RelativeWebRoot & "App_Themes/" & MeanstreamTheme & "/Images/icon-trash.png"
                    Delete.ToolTip = "Send to Recycle Bin"
                    Delete.NavigateUrl = "javascript:deleteWidget('" & WidgetId.ToString & "');"
                    TableCell.Controls.Add(Delete)

                    TableRow.Cells.Add(TableCell)
                End If

                ContentPanel.Controls.Add(Content)

                Dim HoverMenuExtender As Meanstream.Web.UI.HoverMenuExtender = New Meanstream.Web.UI.HoverMenuExtender
                HoverMenuExtender.ID = "HoverMenuExtender"
                
                'HoverMenuExtender.HoverCssClass = "controlfunctions"
                HoverMenuExtender.TargetControlID = ContentPanel.ID
                HoverMenuExtender.PopupControlID = MenuPanel.ClientID
                HoverMenuExtender.PopupPosition = Meanstream.Web.UI.HoverMenuPopupPosition.Center
                HoverMenuExtender.OffsetX = "0"
                HoverMenuExtender.OffsetY = "0"
                HoverMenuExtender.PopDelay = "0"
                Controls.Add(HoverMenuExtender)

                MenuPanel.Attributes.Add("HoverMenuExtender", HoverMenuExtender.ClientID)

                'Content GrayOut Script
                Dim GrayOut As String = "<script type='text/javascript' language='javascript'> function " & ContentPanel.ClientID.Replace("-", "___") & "_grayOut(vis, options) { var options = options || {};   var zindex = options.zindex || -1;  var opacity = options.opacity || 90;  var opaque = (opacity / 100);  var bgcolor = options.bgcolor || '#000000';  var dark=document.getElementById('" & ContentPanel.ClientID & "'); dark.style.width='100%'; dark.style.zIndex=zindex; dark.style.position='relative'; if (vis) { dark.style.opacity=.30; dark.style.MozOpacity=.30; dark.style.filter='alpha(opacity='+opacity+')'; dark.style.backgroundColor=bgcolor; dark.style.display='block'; } else { dark.style.display='none';}} </script>"
                Dim GrayOutJScript As LiteralControl = New LiteralControl
                GrayOutJScript.Text = GrayOut
                Dim GrayOutJScriptCall As LiteralControl = New LiteralControl
                Dim GrayOutCall As String = "<script type='text/javascript' language='javascript'> " & ContentPanel.ClientID.Replace("-", "___") & "_grayOut(true); </script>"
                GrayOutJScriptCall.Text = GrayOutCall
                ContentPanel.Controls.Add(GrayOutJScript)
                ContentPanel.Controls.Add(GrayOutJScriptCall)
            End If
        End Sub
    End Class

    Public Class WidgetEditMenuItemCollection
        Inherits List(Of WidgetEditMenuItem)
    End Class

    Public Class WidgetEditMenuItem
        Inherits Meanstream.Web.UI.Window

        Sub New()
            MyBase.New()
            _Text = ""
        End Sub

        Dim _Text As String
        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property
    End Class
End Namespace