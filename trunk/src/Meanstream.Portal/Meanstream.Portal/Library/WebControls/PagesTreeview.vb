Imports System.Web
Imports System.Web.UI.WebControls

Namespace Meanstream.Portal.WebControls
    Public Class PagesTreeview
        Inherits System.Web.UI.UserControl

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Dim Treeview As System.Web.UI.WebControls.TreeView = Me.GetMenuTreeView()

                If Request.Params("PageID") <> Nothing Then
                    Me.SelectNode(Treeview.Nodes, Request.Params("PageID"))
                End If

                Me.Controls.Add(Treeview)
            End If
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

        Private Function GetMenuTreeView() As System.Web.UI.WebControls.TreeView
            Dim Treeview As System.Web.UI.WebControls.TreeView = New System.Web.UI.WebControls.TreeView
            Treeview.CssClass = "tree"
            Treeview.ExpandDepth = "1"
            Treeview.ExpandImageUrl = "~/Meanstream/img/plus.gif"
            Treeview.CollapseImageUrl = "~/Meanstream/img/minus.gif"
            Treeview.PopulateNodesFromClient = "false"
            Treeview.NodeStyle.CssClass = "node"
            Treeview.NodeStyle.ImageUrl = "img/folder.gif"
            Treeview.SelectedNodeStyle.CssClass = "selected-node"

            Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Core.Utilities.AppUtility.GetAllPageEntities
            Dim TopLevelPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", Nothing)
            TopLevelPages.Sort("TabOrder")

            For Each ParentPage As Meanstream.Portal.Core.Entities.MeanstreamPage In TopLevelPages
                Dim ParentPageLink As System.Web.UI.WebControls.TreeNode = New System.Web.UI.WebControls.TreeNode
                ParentPageLink.Text = ParentPage.Name
                ParentPageLink.Value = ParentPage.Id.ToString
                ParentPageLink.NavigateUrl = "~/Meanstream/Pages/Default.aspx?ctl=ViewPage&PageID=" & ParentPage.Id.ToString

                If Request.Params("PageID") <> Nothing Then
                    If ParentPage.Id = New Guid(Request.Params("PageID")) Then
                        ParentPageLink.Expanded = True
                        ParentPageLink.ExpandAll()
                    End If
                End If

                Treeview.Nodes.Add(ParentPageLink)

                'get all child pages
                Dim ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = PageList.FindAll("ParentId", ParentPage.Id)
                ChildPages.Sort("TabOrder")

                If ChildPages.Count > 0 Then
                    Me.RecurseForChildren(ParentPage.Id, ParentPageLink, ChildPages, PageList)
                End If
            Next

            Return Treeview
        End Function

        Private Function RecurseForChildren(ByVal ParentID As Guid, ByVal ParentNode As System.Web.UI.WebControls.TreeNode, ByVal ChildPages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage), ByVal PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)) As System.Web.UI.WebControls.TreeNode

            For Each ChildPage As Meanstream.Portal.Core.Entities.MeanstreamPage In ChildPages
                Dim ChildPageLink As System.Web.UI.WebControls.TreeNode = New System.Web.UI.WebControls.TreeNode
                ChildPageLink.Text = ChildPage.Name
                ChildPageLink.Value = ChildPage.Id.ToString
                ChildPageLink.NavigateUrl = "~/Meanstream/Pages/Default.aspx?ctl=ViewPage&PageID=" & ChildPage.Id.ToString

                If Request.Params("PageID") <> Nothing Then
                    If ChildPage.Id = New Guid(Request.Params("PageID")) Then
                        ChildPageLink.Expanded = True
                        ChildPageLink.ExpandAll()

                        If ParentNode IsNot Nothing Then
                            ParentNode.Expanded = True
                            ParentNode.ExpandAll()
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
End Namespace

