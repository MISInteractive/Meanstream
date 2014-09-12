
Partial Class Meanstream_Pages_UserControls_TreeView
    Inherits System.Web.UI.UserControl


    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
    Public ThemePath As String


    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ThemePath = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "App_Themes/" & Page.Theme
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Treeview As Meanstream.Web.UI.TreeView = Me.GetMenuTreeView()
        If Request.Params("PageID") <> Nothing Then
            Me.SelectNode(Treeview.Nodes, Request.Params("PageID"))
        End If
        Me.phTreeview.Controls.Add(Treeview)
    End Sub


    Private Sub SelectNode(ByVal TreeNode As TreeNodeCollection, ByVal TreeValue As String)
        For Each node As TreeNode In TreeNode
            If node.Value = TreeValue Then
                node.Select()
                Exit Sub
            ElseIf node.ChildNodes.Count > 0 Then
                SelectNode(node.ChildNodes, TreeValue)
            End If
        Next
    End Sub


    Private Sub ExpandNodes(ByVal Node As Meanstream.Web.UI.TreeNode)
        If Node.Parent Is Nothing Then
            Node.ExpandAll()
        Else
            ExpandNodes(Node.Parent)
        End If
    End Sub


    Private Function GetMenuTreeView() As Meanstream.Web.UI.TreeView
        Dim Treeview As Meanstream.Web.UI.TreeView = New Meanstream.Web.UI.TreeView
        Treeview.CssClass = "tree"
        'Treeview.ExpandDepth = "2"
        Treeview.PathSeparator = "-"
        Treeview.NodeStyle.ChildNodesPadding = System.Web.UI.WebControls.Unit.Pixel(1)
        'Treeview.ExpandImageUrl = ThemePath & "/images/icon-foldermax-blackbg.gif"
        'Treeview.CollapseImageUrl = ThemePath & "/images/icon-foldermin-blackbg.gif"
        Treeview.PopulateNodesFromClient = "false"
        Treeview.NodeStyle.CssClass = "treelinks"
        'Treeview.NodeStyle.ImageUrl = ThemePath & "/images/icon-foldermin-blackbg.gif"
        Treeview.SelectedNodeStyle.CssClass = "treelinks"

        Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)
        Dim TopLevelPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", New Guid("00000000-0000-0000-0000-000000000000"))
        TopLevelPages.Sort("DisplayOrder")

        For Each ParentPage As Meanstream.Portal.Core.Entities.MeanstreamPage In TopLevelPages
            Dim ParentPageLink As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
            ParentPageLink.Value = ParentPage.Id.ToString
            ParentPageLink.NavigateUrl = "~/Meanstream/Pages/Default.aspx?ctl=ViewPage&PageID=" & ParentPage.Id.ToString
            ParentPageLink.Text = ParentPage.Name

            If ParentPage.IsHome Then
                ParentPageLink.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/icon-home.png"
            End If

            If Request.Params("PageID") <> Nothing Then
                If ParentPage.Id.ToString = Request.Params("PageID").ToString Then
                    Me.ExpandNodes(ParentPageLink)
                End If
            End If

            Treeview.Nodes.Add(ParentPageLink)

            'get all child pages
            Dim ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ParentPage.Id)
            ChildPages.Sort("DisplayOrder")

            If ChildPages.Count > 0 Then
                Me.RecurseForChildren(ParentPage.Id, ParentPageLink, ChildPages, PageList)
            End If
        Next

        Return Treeview
    End Function


    Private Function RecurseForChildren(ByVal ParentID As Guid, ByVal ParentNode As System.Web.UI.WebControls.TreeNode, ByVal ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage), ByVal PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)) As Meanstream.Web.UI.TreeNode

        For Each ChildPage As Meanstream.Portal.Core.Entities.MeanstreamPage In ChildPages
            Dim ChildPageLink As Meanstream.Web.UI.TreeNode = New Meanstream.Web.UI.TreeNode
            ChildPageLink.Text = ChildPage.Name

            If ChildPage.IsHome Then
                ChildPageLink.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/icon-home.png"
            End If

            ChildPageLink.Value = ChildPage.Id.ToString
            ChildPageLink.NavigateUrl = "~/Meanstream/Pages/Default.aspx?ctl=ViewPage&PageID=" & ChildPage.Id.ToString

            If Request.Params("PageID") <> Nothing Then
                If ChildPage.Id.ToString = Request.Params("PageID").ToString Then
                    ChildPageLink.Expanded = True
                    ChildPageLink.ExpandAll()

                    If ParentNode IsNot Nothing Then
                        Me.ExpandNodes(ParentNode)
                    End If
                End If
            End If

            ParentNode.ChildNodes.Add(ChildPageLink)

            'get all child pages
            Dim SubChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ChildPage.Id)
            SubChildPages.Sort("DisplayOrder")

            If SubChildPages.Count > 0 Then
                Me.RecurseForChildren(ChildPage.Id, ChildPageLink, SubChildPages, PageList)
            End If
        Next

        Return ParentNode
    End Function
End Class
