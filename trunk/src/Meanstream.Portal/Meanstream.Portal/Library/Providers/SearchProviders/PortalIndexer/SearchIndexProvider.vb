Imports Meanstream.Portal.Core.Services.Search

Namespace Meanstream.Portal.Providers.PortalIndexer
    Public Class SearchIndexProvider
        Inherits Meanstream.Portal.Core.Services.Search.IndexingProvider

        Public Overrides Function Crawl(ByVal url As String) As PageState()
            Dim _spider = New Spider(url, -1)
            _spider.KeepWebContent = True
            _spider.Execute()
            Return _spider.Pages
        End Function

    End Class
End Namespace
