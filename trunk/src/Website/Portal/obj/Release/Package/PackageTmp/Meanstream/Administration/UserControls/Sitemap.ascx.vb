
Partial Class Meanstream_Administration_UserControls_Sitemap
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim sitemap As String = Meanstream.Portal.Core.Utilities.AppUtility.GetCurrentSiteUrl & "sitemap"
            Me.SiteMapURL.NavigateUrl = sitemap
            Me.SiteMapURL.Text = sitemap
            Me.SitemapEnabled.Checked = System.Web.SiteMap.Enabled
            'Me.BindGrid()
        End If
    End Sub

    'Private Sub BindGrid()
    '    Me.Grid.DataSource = System.Web.SiteMap.Providers
    '    Me.Grid.DataBind()
    '    If Me.Grid.Rows.Count = 0 Then
    '        Me.Container.Visible = False
    '    End If
    'End Sub
End Class
