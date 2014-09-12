Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration.Provider
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports System.Security.Permissions
Imports System.Web
Imports System.Web.Caching
Imports System.Web.Configuration
Imports System.Text.RegularExpressions

Namespace Meanstream.Portal.Providers.PortalSiteMapProvider
    ''' <summary>
    ''' IndexedSiteMapProvider
    ''' This class is a site map provider implementation for a site crawler.
    ''' This provider provides caching feature so that it will return root SiteMapNode if available in cache.
    ''' </summary>
    ''' 
    <SqlClientPermission(SecurityAction.Demand, Unrestricted:=True)> _
    Public Class IndexedSiteMapProvider
        Inherits StaticSiteMapProvider
        Private Const ErrMsg1 As String = "Missing node ID"
        Private Const ErrMsg2 As String = "Duplicate node ID"
        Private Const ErrMsg3 As String = "Missing parent ID"
        Private Const ErrMsg4 As String = "Invalid parent ID"
        Private Const ErrMsg5 As String = "Empty or missing connectionStringName"
        Private Const ErrMsg6 As String = "Missing connection string"
        Private Const ErrMsg7 As String = "Empty connection string"

        Public Const CacheDependencyName As String = "__SiteMapCacheDependency"
        Private m_CacheKey As String = "IndexedSiteMapProvider_Nodes"
        Private m_CacheEnabled As Boolean = False
        Private m_IndexID As Integer, m_IndexTitle As Integer, m_IndexUrl As Integer, m_IndexDesc As Integer, m_IndexRoles As Integer, m_IndexParent, m_IndexType, m_IndexIsVisible, m_IndexDisableLink As Integer
        Private m_NodesDictionary As New Dictionary(Of String, SiteMapNode)
        Private ReadOnly m_Lock As New Object()
        Private m_RootNode As SiteMapNode


        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            ' Verify that config isn't null
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If

            ' Assign the provider a default name if it doesn't have one
            If [String].IsNullOrEmpty(name) Then
                name = "IndexedSiteMapProvider"
            End If

            ' Add a default "description" attribute to config if the
            ' attribute doesn’t exist or is empty
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "Spider site map provider")
            End If

            ' Call the base class's Initialize method
            MyBase.Initialize(name, config)


            ' Initialize Cache Enabled/Disabled
            Dim cacheEnabled As String = config("cacheEnabled")
            If Not [String].IsNullOrEmpty(cacheEnabled) Then
                m_CacheEnabled = Convert.ToBoolean(cacheEnabled)
            End If
            config.Remove("cacheEnabled")

            ' Initialize Cache Key
            Dim cacheKey As String = config("cacheKey")
            If Not [String].IsNullOrEmpty(cacheKey) Then
                m_CacheKey = cacheKey
            End If
            config.Remove("cacheKey")

            ' SiteMapProvider processes the securityTrimmingEnabled
            ' attribute but fails to remove it. Remove it now so we can
            ' check for unrecognized configuration attributes.

            If config("securityTrimmingEnabled") IsNot Nothing Then
                config.Remove("securityTrimmingEnabled")
            End If

            ' Throw an exception if unrecognized attributes remain
            If config.Count > 0 Then
                Dim attr As String = config.GetKey(0)
                If Not [String].IsNullOrEmpty(attr) Then
                    Throw New ProviderException("Unrecognized attribute: " & attr)
                End If
            End If

        End Sub

        <MethodImpl(MethodImplOptions.Synchronized)> _
        Public Overloads Overrides Function BuildSiteMap() As SiteMapNode
            SyncLock m_Lock

                If m_CacheEnabled Then
                    Dim rootNode As SiteMapNode = DirectCast(HttpRuntime.Cache.[Get](m_CacheKey), SiteMapNode)
                    If rootNode IsNot Nothing Then
                        'check dependency
                        Dim count As Integer = 0

                        Dim siteMapNodeCount As Integer = Convert.ToInt32(HttpRuntime.Cache.[Get](CacheDependencyName))

                        If count <> siteMapNodeCount Then
                            'remove the cache item so that OnSiteMapChanged event is triggered
                            HttpRuntime.Cache.Remove(CacheDependencyName)
                        Else
                            Return DirectCast(HttpRuntime.Cache.[Get](m_CacheKey), SiteMapNode)
                        End If
                    End If
                End If

                ' Make sure site map is cleared if it exists before continue 
                If m_RootNode IsNot Nothing Then
                    'return m_RootNode;
                    ClearSiteMap()
                End If

                Dim pages As List(Of Meanstream.Portal.Core.Services.Search.Document) = Meanstream.Portal.Core.Services.Search.SearchEngineService.Current.GetAll()

                ' Create the root SiteMapNode and add it to the site map
                m_RootNode = CreateRootNode()
                AddNode(m_RootNode, Nothing)

                'Dim FirstNode As SiteMapNode = CreateSiteMapNodeFromPageState(reader)
                Dim counter As Integer = 1

                For Each page As Meanstream.Portal.Core.Services.Search.Document In pages
                    ' Build a tree of SiteMapNodes underneath the root node
                    ' Create another site map node and add it to the site map
                    CreateSiteMapNodeFromPageState(page)
                    counter += 1
                Next

                If m_CacheEnabled AndAlso (HttpRuntime.Cache.[Get](m_CacheKey) Is Nothing) Then
                    'Set a value for the cache entry that will serve as the 
                    'key for the dependency to be created on
                    HttpRuntime.Cache(CacheDependencyName) = counter

                    'Create the array of cache key item names
                    Dim keys As String() = New [String](0) {}
                    keys(0) = CacheDependencyName

                    'Create a dependency object referencing the array of cachekeys (keys)
                    Dim dependency As New CacheDependency(Nothing, keys)

                    HttpRuntime.Cache.Insert(m_CacheKey, m_RootNode, dependency, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, _
                    New CacheItemRemovedCallback(AddressOf OnSiteMapChanged))

                End If

                ' Return the root SiteMapNode
                Return m_RootNode
            End SyncLock
        End Function

        Private Sub ClearSiteMap()
            Clear()
            m_NodesDictionary.Clear()
            m_RootNode = Nothing
        End Sub

        Protected Overloads Overrides Function GetRootNodeCore() As SiteMapNode
            SyncLock m_Lock
                BuildSiteMap()
                Return m_RootNode
            End SyncLock
        End Function

        ' Helper methods
        Private Function CreateSiteMapNodeFromPageState(ByVal page As Meanstream.Portal.Core.Services.Search.Document) As SiteMapNode
            ' Make sure the node is present
            If page Is Nothing Then
                Throw New ProviderException(ErrMsg1)
            End If

            ' --- Parse the id 
            Dim HeadMatch As Match = Regex.Match(page.Fields(2).Value, "<head id=""([^<]*)"">", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
            Dim id As String = HeadMatch.Groups(1).Value
            ' --- Parse the title 
            Dim TitleMatch As Match = Regex.Match(page.Fields(2).Value, "<title>([^<]*)</title>", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
            Dim title As String = TitleMatch.Groups(1).Value
            ' --- Parse the meta description 
            Dim DescriptionMatch As Match = Regex.Match(page.Fields(2).Value, "<meta name=""description"" content=""([^<]*)"">", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
            Dim description As String = DescriptionMatch.Groups(1).Value

            Dim virtualPath As String = page.Fields(0).Value.Replace(Core.Utilities.AppUtility.GetCurrentSiteUrl(), "~/")
            Dim url As String = page.Fields(0).Value

            ' Make sure the node ID is unique
            If id <> "0" And m_NodesDictionary.ContainsKey(url) Then
                Return Nothing
            End If

            ' If roles were specified, turn the list into a string array
            Dim rolelist As String() = Nothing
            ' Create a SiteMapNode
            Dim node As New SiteMapNode(Me, url, virtualPath, title, description, rolelist, _
            Nothing, Nothing, Nothing)
            ' Record the node in the m_NodesDictionary dictionary
            m_NodesDictionary.Add(url, node)
            ' Return the node
            AddNode(node, m_RootNode)

            Return node
        End Function

        Private Function CreateRootNode() As SiteMapNode
            ' Create a SiteMapNode
            Dim node As New SiteMapNode(Me, "0", "ROOT", "ROOT", "ROOT", Nothing, _
            Nothing, Nothing, Nothing)
            ' Record the node in the m_NodesDictionary dictionary
            m_NodesDictionary.Add(0, node)
            Return node
        End Function

        Public Sub OnSiteMapChanged(ByVal key As String, ByVal item As Object, ByVal reason As CacheItemRemovedReason)
            SyncLock m_Lock
                If key = CacheDependencyName AndAlso reason = CacheItemRemovedReason.DependencyChanged Then
                    ' Clear the site map
                    ClearSiteMap()
                End If
            End SyncLock
        End Sub

    End Class

End Namespace
