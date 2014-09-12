Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Web

Namespace Meanstream.Portal.Core
    Public Class PortalContext
        Implements IDisposable

        Private _Application As NameObjectCollectionBase
        Private _SessionWrapper As Session
        Private _Items As IDictionary
        Private _Disposed As Boolean = False        ' To detect redundant calls
        Private _Portal As Portal

        Private Const HTTP_CONTEXT_KEY As String = "Meanstream.Portal.Core.PortalContext"

       
        Private Sub New(ByRef context As HttpContext, ByVal siteUrl As String, ByVal siteRelativePath As String)
            _SessionWrapper = New Session(context.Session)
            _Application = context.Application
            _ownerHttpContext = context
            _originalUri = New Uri(context.Request.Url.ToString())
            _siteRelativePath = siteRelativePath
            _siteUrl = siteUrl

            Initialize()
        End Sub

        Private Sub Initialize()
            'find portal
            _Portal = PortalContext.GetPortalByDomain(SiteUrl)
            _portalId = _Portal.Id
            'Try
            '    _Portal = PortalContext.GetPortalByDomain(Me.SiteUrl)
            'Catch ex As Exception
            '    _Portal = PortalContext.GetPortalByDomain(OriginalUri.Host)
            'End Try
            'Try
            '    _portalId = PortalContext.GetPortalByDomain(SiteUrl).Id
            'Catch ex As Exception
            '    _portalId = PortalContext.GetPortalByDomain(OriginalUri.Host).Id
            'End Try
        End Sub


        Public Shared Function GetPortalByDomain(ByVal domain As String) As Portal
            If Portals.Values.Count = 0 Then
                ReloadPortals()
            End If

            For Each portal As Portal In Portals.Values
                If portal.Domain.Contains(domain) Then
                    Return portal
                End If
            Next
            Return Nothing
        End Function


        Public Shared Function GetPortalById(ByVal id As Guid) As Portal
            If Portals.Values.Count = 0 Then
                ReloadPortals()
            End If

            For Each portal As Portal In Portals.Values
                If id = portal.Id Then
                    Return portal
                End If
            Next
            Return Nothing
        End Function


        Friend Shared Function Create(ByVal context As HttpContext) As PortalContext
            ' STEP 1: Search for a matching Site URL based on the Request URL
            Dim matchingSiteUrl As String = Nothing

            ' Drop the scheme (http://) and the querystring parts of the URL
            ' Example: http://localhost:1315/public/folder1?
            Dim nakedRequestUrl As String = context.Request.Url.GetComponents(UriComponents.Host Or UriComponents.Port Or UriComponents.Path, UriFormat.Unescaped).ToLower

            If nakedRequestUrl.EndsWith("/") Then
                nakedRequestUrl = nakedRequestUrl.Substring(0, nakedRequestUrl.Length - 1)
            End If
            
            ' Get the matching site url (if any)
            For Each url As String In Portals.Keys
                url = url.ToLower

                'handle mulituple domains
                If url.Contains(",") Then
                    Dim urls() As String = url.Split(",")
                    For Each durl As String In urls
                        'handle ports
                        If nakedRequestUrl.Contains(":") Then
                            If Not url.Contains(":") Then
                                Dim s As String = nakedRequestUrl.Substring(nakedRequestUrl.IndexOf(":"))
                                Dim e As String = s.Substring(0, s.IndexOf("/"))
                                durl = durl & e
                            End If
                        End If

                        If nakedRequestUrl.StartsWith(durl) AndAlso (nakedRequestUrl.Length = durl.Length OrElse nakedRequestUrl(durl.Length) = "/"c OrElse nakedRequestUrl(durl.Length) = "?"c) Then
                            matchingSiteUrl = durl
                            Exit For
                        End If
                    Next
                Else
                    'handle ports
                    If nakedRequestUrl.Contains(":") Then
                        If Not url.Contains(":") Then
                            Dim s As String = nakedRequestUrl.Substring(nakedRequestUrl.IndexOf(":"))
                            Dim e As String = s.Substring(0, s.IndexOf("/"))
                            url = url & e
                        End If
                    End If

                    If nakedRequestUrl.StartsWith(url) AndAlso (nakedRequestUrl.Length = url.Length OrElse nakedRequestUrl(url.Length) = "/"c OrElse nakedRequestUrl(url.Length) = "?"c) Then
                        matchingSiteUrl = url
                        Exit For
                    End If
                End If
            Next

            ' Create Path
            Dim pfsPath As String = ""
            Dim siteRelativePath As String = ""
            If matchingSiteUrl IsNot Nothing Then
                siteRelativePath = nakedRequestUrl.Substring(matchingSiteUrl.Length)
                'ensure virtual path
                Dim relativeWebRoot As String = Meanstream.Portal.Core.Utilities.AppUtility.AbsoluteWebRoot.AbsolutePath.ToLower

                If siteRelativePath.StartsWith(relativeWebRoot) Then
                    siteRelativePath = siteRelativePath.Substring(1)
                End If

                If siteRelativePath.StartsWith("/") Then
                    siteRelativePath = siteRelativePath.Substring(1)
                End If
            Else
                ' The request does not belong to a site (eg. "http://localhost/Root/System/Skins/Test.css")
                'pfsPath = HttpUtility.UrlDecode(context.Request.Url.AbsolutePath)

                ''TODO: check this
                'Dim appPath As String = context.Request.ApplicationPath
                'If appPath IsNot Nothing AndAlso appPath.Length > 1 Then
                '    pfsPath = pfsPath.Substring(appPath.Length - 1)
                'End If

                'matchingSiteUrl = pfsPath
            End If

            'Create a new PortalContext instance and return it.
            Dim pc As New PortalContext(context, matchingSiteUrl, siteRelativePath)
            context.Items.Add(HTTP_CONTEXT_KEY, pc)
            Return pc
        End Function


        Private Shared _portalUrls As Dictionary(Of Guid, Dictionary(Of String, String))
        Private Shared _portalUrlsLock As New Object()
        Friend Shared ReadOnly Property PortalUrls() As Dictionary(Of Guid, Dictionary(Of String, String))
            Get
                If _portalUrls Is Nothing Then
                    ReloadPortalUrlList()
                End If
                Return _portalUrls
            End Get
        End Property


        Public Shared Sub ReloadPortalUrlList()
            SyncLock _portalUrlsLock
                _portalUrls = New Dictionary(Of Guid, Dictionary(Of String, String))
                For Each portal As Portal In Portals.Values
                    Dim urls As New Dictionary(Of String, String)
                    Dim pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Content.ContentService.Current.GetAllPages(portal.Id)
                    For Each page As Meanstream.Portal.Core.Content.Page In pages
                        urls.Add(page.Url.ToLowerInvariant(), page.Url.ToLowerInvariant())
                    Next
                    _portalUrls.Add(portal.Id, urls)
                Next
            End SyncLock
        End Sub


        Private Shared _portals As Dictionary(Of String, Portal)
        Private Shared _portalsLock As New Object()
        Private Shared ReadOnly Property Portals() As Dictionary(Of String, Portal)
            Get
                If _portals Is Nothing Then
                    ReloadPortals()
                End If
                Return _portals
            End Get
        End Property


        Public Shared Sub ReloadPortals()
            SyncLock _portalsLock
                _portals = New Dictionary(Of String, Portal)
                'get list of portals by their domain name key
                Dim Portals As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPortals) = _
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamPortalsProvider.GetAll()

                For Each Portal As Meanstream.Portal.Core.Entities.MeanstreamPortals In Portals
                    Dim _portal As Portal = New Portal(Portal.Id, Portal.ApplicationId, Portal.Name, Portal.Domain, _
                                                       Portal.Root, Portal.LoginPageUrl, Portal.HomePageUrl, Portal.Theme)
                    _portals.Add(_portal.Domain, _portal)
                Next
            End SyncLock
        End Sub

#Region "Properties"
        Public Shared ReadOnly Property Current() As PortalContext
            Get
                If HttpContext.Current Is Nothing Then
                    ' offline mode 
                    ' There will be only one instance of AppContext per thread 
                    Return Nothing
                Else
                    ' Web Mode 

                    Return TryCast(HttpContext.Current.Items(HTTP_CONTEXT_KEY), PortalContext)
                End If
            End Get
        End Property


        Private _ownerHttpContext As HttpContext
        Public ReadOnly Property OwnerHttpContext() As HttpContext
            Get
                Return _ownerHttpContext
            End Get
        End Property


        Private _originalUri As Uri
        Public ReadOnly Property OriginalUri() As Uri
            Get
                Return _originalUri
            End Get
        End Property


        Private _siteUrl As String
        Public ReadOnly Property SiteUrl() As String
            Get
                Return _siteUrl
            End Get
        End Property


        Private _portalId As Guid
        Public ReadOnly Property PortalId() As Guid
            Get
                Return _portalId
            End Get
        End Property


        Public ReadOnly Property Portal() As Portal
            Get
                Return _Portal
            End Get
        End Property


        Private _siteRelativePath As String
        Public ReadOnly Property SiteRelativePath() As String
            Get
                Return _siteRelativePath
            End Get
        End Property


        Public ReadOnly Property Application() As NameObjectCollectionBase
            Get
                Return Me._Application
            End Get
        End Property


        Public ReadOnly Property Session() As Session
            Get
                Return Me._SessionWrapper
            End Get
        End Property
#End Region


#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements System.IDisposable.Dispose
            _Disposed = True
        End Sub

        Protected Overrides Sub Finalize()
            Try
                If Not _Disposed Then
                    Me.Dispose()
                End If
            Finally
                MyBase.Finalize()
            End Try
        End Sub

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me._Disposed Then
                If disposing Then
                    Me.Dispose()
                End If
            End If
            Me._Disposed = True
        End Sub
#End Region
    End Class
End Namespace

