Imports Meanstream.Portal.Core.Services.Search

Namespace Meanstream.Portal.Providers.PortalIndexer

    Public Interface IPageProcessor
        Function Process(ByVal state As PageState) As Boolean

        Property HandleContent() As PageContentDelegate
    End Interface

    Public Delegate Sub PageContentDelegate(ByVal state As PageState)

End Namespace