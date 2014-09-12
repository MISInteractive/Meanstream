Imports System
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net
Imports Meanstream.Portal.Core.Utilities
Imports Meanstream.Portal.Core.Services.Search

Namespace Meanstream.Portal.Providers.PortalIndexer

    Public Class Spider
        Private m_startUri As Uri
        Private m_baseUri As Uri
        Private m_uriProcessedCountMax As Integer
        Private m_uriProcessedCount As Integer
        Private m_keepWebContent As Boolean

        Private m_webPagesPending As Queue
        Private m_webPagesHash As Hashtable
        'Private m_webPages As WebPageState()
        Private m_webPageProcessor As IPageProcessor


        Public Sub New(ByVal startUri As String)
            MyClass.New(startUri, -1)
        End Sub 'New

        Public Sub New(ByVal startUri As String, ByVal uriProcessedCountMax As Integer)
            MyClass.New(startUri, "", uriProcessedCountMax, False, New PageProcessor)
        End Sub 'New

        Public Sub New(ByVal startUri As String, ByVal baseUri As String, ByVal uriProcessedCountMax As Integer)
            MyClass.New(startUri, baseUri, uriProcessedCountMax, False, New PageProcessor)
        End Sub 'New

        Public Sub New(ByVal l_startUri As String, ByVal l_baseUri As String, ByVal l_uriProcessedCountMax As Integer, ByVal l_keepWebContent As Boolean, ByVal l_webPageProcessor As IPageProcessor)

            StartUri = New Uri(l_startUri)

            ' In future this could be null and will process cross-site, but for now must exist
            If (l_baseUri Is Nothing Or l_baseUri.Trim().Length() = 0) Then
                BaseUri = New Uri(StartUri.GetLeftPart(UriPartial.Authority))
            Else
                BaseUri = New Uri(l_baseUri)
            End If

            UriProcessedCountMax = l_uriProcessedCountMax
            KeepWebContent = l_keepWebContent

            m_webPagesPending = New Queue
            m_webPagesHash = New Hashtable

            m_webPageProcessor = l_webPageProcessor

            PageProcessor.HandleContent = AddressOf Me.HandleLinks

        End Sub 'New

        Public Sub Execute()
            UriProcessedCount = 0

            Dim startTime As DateTime = DateTime.Now

            'Console.WriteLine("======================================================================================================")
            'Console.WriteLine(("Proccess URI: " + m_startUri.AbsoluteUri))
            'Console.WriteLine(("Start At    : " + startTime))
            'Console.WriteLine("------------------------------------------------------------------------------------------------------")

            AddWebPage(StartUri, StartUri.AbsoluteUri)

            Try
                While PagesPending.Count > 0 And (UriProcessedCountMax = -1 Or UriProcessedCount < UriProcessedCountMax)
                    'Console.WriteLine("Max URI's: {0}, Processed URI's: {1}, Pending URI's: {2}", UriProcessedCountMax, UriProcessedCount, WebPagesPending.Count)

                    Dim state As PageState = CType(m_webPagesPending.Dequeue(), PageState)

                    m_webPageProcessor.Process(state)

                    If Not KeepWebContent Then
                        state.Content = Nothing
                    End If

                    UriProcessedCount += 1
                End While
            Catch ex As Exception
                Throw New ApplicationException("Failure while running spider: " + ex.ToString)
                'Console.WriteLine(("Failure while running web spider: " + ex.ToString()))
            End Try

            Dim endTime As DateTime = DateTime.Now
            Dim elasped As Single = (endTime.Ticks - startTime.Ticks) / 10000000

            'Console.WriteLine("------------------------------------------------------------------------------------------------------")
            'Console.WriteLine("URI Finished   : " + m_startUri.AbsoluteUri)
            'Console.WriteLine("Pages Processed: " + UriProcessedCount.ToString())
            'Console.WriteLine("Pages Pending  : " + WebPagesPending.Count.ToString())
            'Console.WriteLine("End At         : " + endTime)
            'Console.WriteLine("Elasped Time   : {0} seconds", elasped)
            'Console.WriteLine("======================================================================================================")
        End Sub 'Execute


        Public Sub HandleLinks(ByVal state As PageState)
            'Console.WriteLine("Delegate is called")
            If Not state.ProcessInstructions.IndexOf("Handle Links") = -1 Then
                'Console.WriteLine("Delegate is called - Full")
                Dim counter As Integer = 0
                Dim m As Match = RegExUtility.GetMatchRegEx(RegularExpression.UrlExtractor, state.Content)

                While m.Success
                    If AddWebPage(state.Uri, m.Groups("url").ToString()) Then
                        counter += 1
                    End If

                    m = m.NextMatch()
                End While

                'Console.WriteLine("           : {0} new links were added", counter)
            End If
        End Sub 'HandleLinks


        Private Function AddWebPage(ByVal l_baseUri As Uri, ByVal newUri As String) As Boolean
            ' Remove any anchors
            Dim url As String = StringUtility.LeftIndexOf(newUri, "#")

            ' Construct a Uri, using the current page Uri as a base reference

            Dim uri As New Uri(l_baseUri, url)

            If Not ValidPage(uri.LocalPath) Or m_webPagesHash.Contains(uri) Then
                Return False
            End If
            Dim state As New PageState(uri)

            ' Only do a full process for pages within the same site

            If (uri.AbsoluteUri.StartsWith(BaseUri.AbsoluteUri)) Then
                state.ProcessInstructions += "Handle Links"
            End If

            '         _.P( "URI: {0}, Status: {1}", state.Uri, state.WebPageStatus );
            m_webPagesPending.Enqueue(state)
            m_webPagesHash.Add(uri, state)

            Return True
        End Function 'AddWebPage

        Private Shared m_validPageExtensions() As String = {"html", "php", "asp", "htm", "jsp", "shtml", "php3", "aspx", "pl", "cfm", "xhtml"}

        Private Function ValidPage(ByVal path As String) As Boolean
            Dim pos As Integer = path.IndexOf(".")

            If pos = -1 Or path.Chars(path.Length - 1) = "/" Then   '.ToString( ).Equals( "/" )
                Return True
            End If

            Dim uriExt As String = StringUtility.RightOf(path, pos).ToLower()

            ' Uri ends in an extension
            Dim ext As String
            For Each ext In m_validPageExtensions
                If uriExt.Equals(ext) Then
                    Return True
                End If
            Next ext

            Return False
        End Function 'ValidPageExtension

        Public Property PageProcessor() As IPageProcessor
            Get
                Return m_webPageProcessor
            End Get
            Set(ByVal Value As IPageProcessor)
                m_webPageProcessor = Value
            End Set
        End Property

        Public Property StartUri() As Uri
            Get
                Return m_startUri
            End Get
            Set(ByVal Value As Uri)
                m_startUri = Value
            End Set
        End Property

        Public Property BaseUri() As Uri
            Get
                Return m_baseUri
            End Get
            Set(ByVal Value As Uri)
                m_baseUri = Value
            End Set
        End Property

        Private Property UriProcessedCount() As Integer
            Get
                Return m_uriProcessedCount
            End Get
            Set(ByVal Value As Integer)
                m_uriProcessedCount = Value
            End Set
        End Property

        Public Property UriProcessedCountMax() As Integer
            Get
                Return m_uriProcessedCountMax
            End Get
            Set(ByVal Value As Integer)
                m_uriProcessedCountMax = Value
            End Set
        End Property

        Public Property KeepWebContent() As Boolean
            Get
                Return m_keepWebContent
            End Get
            Set(ByVal Value As Boolean)
                m_keepWebContent = Value
            End Set
        End Property

        Private Property WebPagesHash() As Hashtable
            Get
                Return m_webPagesHash
            End Get
            Set(ByVal Value As Hashtable)
                m_webPagesHash = Value
            End Set
        End Property

        Public ReadOnly Property Pages() As PageState()
            Get
                Dim WebPagesColl As ICollection = Me.WebPagesHash.Values
                Dim WebPages As PageState() = New PageState(WebPagesColl.Count - 1) {}
                Dim index As Integer = 0
                For Each webPage As PageState In WebPagesColl
                    WebPages(index) = webPage
                    index = index + 1
                Next
                Return WebPages
            End Get
        End Property

        Private Property PagesPending() As Queue
            Get
                Return m_webPagesPending
            End Get
            Set(ByVal Value As Queue)
                m_webPagesPending = Value
            End Set
        End Property

    End Class

End Namespace
