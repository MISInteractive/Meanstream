
Partial Class Controls_Core_Widgets_SiteMap_Widget
    Inherits Meanstream.Portal.Core.Widgets.SiteMap.WidgetBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim xslFileName = Server.MapPath("~/controls/core/widgets/sitemap/sitemap.xslt")
        Dim xml As String = "<siteMap xmlns='http://schemas.microsoft.com/AspNet/SiteMap-File-1.0'>" & _
                            "<siteMapNode url='~/home' title='Home'  description=''>" & _
                            "<siteMapNode url='~/test1' title='test1' description='yadda' />" & _
                            "<siteMapNode url='~/test2' title='test2'  description='yadda yadda' />" & _
                            "</siteMapNode>" & _
                            "</siteMap>"

        Dim xmlStringReader As New System.IO.StringReader(xml)
        Dim xmlReader As System.Xml.XmlReader = New System.Xml.XmlTextReader(xmlStringReader)
        Dim transform As System.Xml.Xsl.XslCompiledTransform = New System.Xml.Xsl.XslCompiledTransform
        Dim result As System.IO.StringWriter = New System.IO.StringWriter

        transform.Load(xslFileName)
        transform.Transform(xmlReader, Nothing, result)

        Me.litTest.Text = result.ToString

        xmlReader.Close()
        xmlStringReader.Close()
    End Sub

    Protected Sub rptTree_ItemDataBound(ByVal sender As Object, ByVal e As DataListItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim cat As SiteMapNode = DirectCast(e.Item.DataItem, SiteMapNode)
            If cat.ChildNodes.Count > 0 Then
                Dim hylNode As HtmlAnchor = DirectCast(e.Item.FindControl("hylNode"), HtmlAnchor)

                Dim phSubTree As PlaceHolder = DirectCast(e.Item.FindControl("phSubTree"), PlaceHolder)

                Dim rpt As New DataList()
                rpt.RepeatLayout = RepeatLayout.Table
                rpt.RepeatDirection = RepeatDirection.Horizontal
                rpt.RepeatColumns = "3"
                rpt.ItemStyle.CssClass = "sub-items"
                rpt.DataSource = cat.ChildNodes
                rpt.ItemTemplate = rptTree.ItemTemplate
                AddHandler rpt.ItemDataBound, AddressOf rptTree_ItemDataBound

                phSubTree.Controls.Add(rpt)
                rpt.DataBind()
            End If
        End If
    End Sub
End Class
