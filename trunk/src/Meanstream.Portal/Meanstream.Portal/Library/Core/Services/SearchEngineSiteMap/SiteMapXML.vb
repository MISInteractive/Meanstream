
Namespace Meanstream.Portal.Core.Services.SearchEngineSiteMap

    <System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.sitemaps.org/schemas/sitemap/0.9", IsNullable:=False, ElementName:="urlset")> _
    Public Class SiteMapXML
        Private Nodes As List(Of SiteMapXMLNode)

#Region "urls"
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
        <System.Xml.Serialization.XmlElementAttribute("url")> _
        Public Property __Nodes() As SiteMapXMLNode()
            Get
                Return Nodes.ToArray()
            End Get
            Set(ByVal value As SiteMapXMLNode())
                Nodes.Clear()
                Nodes.AddRange(value)
            End Set
        End Property
#End Region

        Public Sub New()
            Nodes = New List(Of SiteMapXMLNode)()
        End Sub

        Public Sub New(ByVal rootNode As System.Web.SiteMapNode)
            Me.New()
            Add(rootNode)
        End Sub

        Public Sub Add(ByVal rootNode As System.Web.SiteMapNode)
            Nodes.Add(New SiteMapXMLNode(rootNode))

            If rootNode.HasChildNodes Then
                For Each node As System.Web.SiteMapNode In rootNode.ChildNodes
                    If node.IsAccessibleToUser(System.Web.HttpContext.Current) Then
                        Add(node)
                    End If
                Next
            End If
        End Sub
    End Class

End Namespace