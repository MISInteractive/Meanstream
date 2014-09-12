Imports Meanstream.Portal.ComponentModel

Namespace Meanstream.Portal.Core.Services.Search

    Public MustInherit Class IndexingProvider
        Public Shared Function Current() As IndexingProvider
            Return ComponentFactory.GetComponent(Of IndexingProvider)()
        End Function

        Public MustOverride Function Crawl(ByVal url As String) As PageState()
    End Class

End Namespace

