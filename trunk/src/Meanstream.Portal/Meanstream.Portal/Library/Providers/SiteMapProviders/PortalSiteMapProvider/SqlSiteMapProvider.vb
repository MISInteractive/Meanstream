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

Namespace Meanstream.Portal.Providers.PortalSiteMapProvider
    ''' <summary>
    ''' SqlPortalSiteMapProvider
    ''' This class is a site map provider implementation for site map data stored in SQL Server Database.
    ''' This provider provides caching feature so that it will return root SiteMapNode if available in cache.
    ''' </summary>
    ''' 
    <SqlClientPermission(SecurityAction.Demand, Unrestricted:=True)> _
    Public Class SqlSiteMapProvider
        Inherits StaticSiteMapProvider
        Private Const ErrMsg1 As String = "Missing node ID"
        Private Const ErrMsg2 As String = "Duplicate node ID"
        Private Const ErrMsg3 As String = "Missing parent ID"
        Private Const ErrMsg4 As String = "Invalid parent ID"
        Private Const ErrMsg5 As String = "Empty or missing connectionStringName"
        Private Const ErrMsg6 As String = "Missing connection string"
        Private Const ErrMsg7 As String = "Empty connection string"

        Public Const CacheDependencyName As String = "__SiteMapCacheDependency"

        Private m_ConnectionString As String
        ' Database connection string
        Private m_TableName As String = "meanstream_Page"
        ' TableName that store site map
        Private m_CacheKey As String = "SQLSiteMapProvider_Nodes"
        Private m_CacheEnabled As Boolean = False
        Private m_IndexID As Integer, m_IndexTitle As Integer, m_IndexUrl As Integer, m_IndexDesc As Integer, m_IndexRoles As Integer, m_IndexParent, m_IndexType, m_IndexIsVisible, m_IndexDisableLink As Integer
        Private m_NodesDictionary As New Dictionary(Of Integer, SiteMapNode)(16)
        Private ReadOnly m_Lock As New Object()
        Private m_RootNode As SiteMapNode


        Public Overloads Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
            ' Verify that config isn't null
            If config Is Nothing Then
                Throw New ArgumentNullException("config")
            End If

            ' Assign the provider a default name if it doesn't have one
            If [String].IsNullOrEmpty(name) Then
                name = "SqlSiteMapProvider"
            End If

            ' Add a default "description" attribute to config if the
            ' attribute doesn’t exist or is empty
            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "SQL Server site map provider")
            End If

            ' Call the base class's Initialize method
            MyBase.Initialize(name, config)

            ' Initialize m_ConnectionString
            Dim connect As String = config("connectionStringName")
            If [String].IsNullOrEmpty(connect) Then
                Throw New ProviderException(ErrMsg5)
            End If
            config.Remove("connectionStringName")

            If WebConfigurationManager.ConnectionStrings(connect) Is Nothing Then
                Throw New ProviderException(ErrMsg6)
            End If

            m_ConnectionString = WebConfigurationManager.ConnectionStrings(connect).ConnectionString
            If [String].IsNullOrEmpty(m_ConnectionString) Then
                Throw New ProviderException(ErrMsg7)
            End If

            ' Initialize Table Name
            Dim tableName As String = config("tableName")
            If Not [String].IsNullOrEmpty(tableName) Then
                m_TableName = tableName
            End If
            config.Remove("tableName")

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

        Private _AllUserRole As Meanstream.Portal.Core.Membership.Role
        Private ReadOnly Property AllUserRole() As Meanstream.Portal.Core.Membership.Role
            Get
                If _AllUserRole Is Nothing Then
                    _AllUserRole = System.Web.HttpContext.Current.Cache(Meanstream.Portal.Core.AppConstants.ALLUSERS)
                    If _AllUserRole Is Nothing Then
                        _AllUserRole = Core.Membership.MembershipService.Current.GetAllUsersRole
                        'cache the Permissions
                        System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.AppConstants.ALLUSERS, _AllUserRole, Nothing, DateTime.Now.AddHours(System.Configuration.ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                    End If
                End If
                Return _AllUserRole
            End Get
        End Property

        Private Function GetUserRoles(ByVal Username As String) As List(Of Meanstream.Portal.Core.Membership.Role)
            Return Core.Membership.MembershipService.Current.GetRolesForUser(Username)
        End Function

        Private _PagePermissions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission)
        Private ReadOnly Property PagePermissions() As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission)
            Get
                _PagePermissions = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS)
                If _PagePermissions Is Nothing Then
                    _PagePermissions = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPagePermissionProvider.GetAll
                    'cache the Permissions
                    System.Web.HttpContext.Current.Cache.Insert(Meanstream.Portal.Core.Utilities.CacheUtility.VIEWPAGEPERMISSIONS, _PagePermissions, Nothing, DateTime.Now.AddHours(System.Configuration.ConfigurationManager.AppSettings.Get("Meanstream.PageCacheExpiration")), TimeSpan.Zero)
                End If
                Return _PagePermissions
            End Get
        End Property

        <MethodImpl(MethodImplOptions.Synchronized)> _
        Public Overloads Overrides Function BuildSiteMap() As SiteMapNode
            SyncLock m_Lock
                Dim connection As New SqlConnection(m_ConnectionString)
                Dim command As SqlCommand = Nothing

                If m_CacheEnabled Then
                    Dim rootNode As SiteMapNode = DirectCast(HttpRuntime.Cache.[Get](m_CacheKey), SiteMapNode)
                    If rootNode IsNot Nothing Then
                        'HttpContext.Current.Response.Write("Get SiteMapNode from cache<br/>");

                        'check dependency
                        Dim count As Integer = 0
                        Try
                            command = New SqlCommand("SELECT COUNT(*) FROM " & Me.m_TableName, connection)
                            command.CommandType = CommandType.Text
                            connection.Open()
                            count = Convert.ToInt32(command.ExecuteScalar())
                        Finally
                            connection.Close()
                        End Try

                        Dim siteMapNodeCount As Integer = Convert.ToInt32(HttpRuntime.Cache.[Get](CacheDependencyName))
                        'HttpContext.Current.Response.Write("Record count: " + count + "; node count: " + (siteMapNodeCount) + "<br/>");

                        If count <> siteMapNodeCount Then
                            'HttpContext.Current.Response.Write("Remove cache dependency<br/>");

                            'If table records that store sitemap are changed,
                            'remove the cache item so that OnSiteMapChanged event is triggered
                            HttpRuntime.Cache.Remove(CacheDependencyName)
                        Else
                            'HttpContext.Current.Response.Write("Return SiteMapNode from cache<br/>");

                            Return DirectCast(HttpRuntime.Cache.[Get](m_CacheKey), SiteMapNode)
                        End If
                    End If
                End If

                ' Make sure site map is cleared if it exists before continue 
                If m_RootNode IsNot Nothing Then
                    'return m_RootNode;
                    ClearSiteMap()
                End If

                ' Query the database for site map nodes 
                'HttpContext.Current.Response.Write("Query all site map and insert into cache<br/>");
                Try
                    command = New SqlCommand("SELECT ID, NAME, URL, DESCRIPTION, PARENTID, TYPE, ISVISIBLE, DISABLELINK FROM " & Me.m_TableName & " WHERE ISVISIBLE=1 AND ISPUBLISHED=1 ORDER BY PARENTID DESC", connection)
                    command.CommandType = CommandType.Text
                    connection.Open()

                    Dim reader As SqlDataReader = command.ExecuteReader()
                    m_IndexID = reader.GetOrdinal("ID")
                    m_IndexUrl = reader.GetOrdinal("URL")
                    m_IndexTitle = reader.GetOrdinal("NAME")
                    m_IndexDesc = reader.GetOrdinal("DESCRIPTION")
                    'm_IndexRoles = reader.GetOrdinal("ROLES")
                    m_IndexParent = reader.GetOrdinal("PARENTID")
                    m_IndexType = reader.GetOrdinal("TYPE")
                    m_IndexIsVisible = reader.GetOrdinal("ISVISIBLE")
                    m_IndexDisableLink = reader.GetOrdinal("DISABLELINK")

                    If reader.Read() Then
                        ' Create the root SiteMapNode and add it to the site map
                        m_RootNode = CreateRootNode()
                        AddNode(m_RootNode, Nothing)

                        Dim FirstNode As SiteMapNode = CreateSiteMapNodeFromDataReader(reader)
                        'AddNode(FirstNode, GetParentNodeFromDataReader(reader))

                        ' Build a tree of SiteMapNodes underneath the root node
                        Dim counter As Integer = 1
                        While reader.Read()
                            ' Create another site map node and add it to the site map
                            Dim node As SiteMapNode = CreateSiteMapNodeFromDataReader(reader)
                            'AddNode(node, GetParentNodeFromDataReader(reader))
                            counter += 1
                        End While

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
                    End If
                Finally
                    connection.Close()
                End Try

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
        Private Function CreateSiteMapNodeFromDataReader(ByVal reader As DbDataReader) As SiteMapNode
            ' Make sure the node ID is present
            If reader.IsDBNull(m_IndexID) Then
                Throw New ProviderException(ErrMsg1)
            End If

            ' Get the node ID from the DataReader
            Dim id As Integer = BitConverter.ToInt32(reader.GetGuid(m_IndexID).ToByteArray, 0)


            ' Make sure the node ID is unique
            If id <> 0 And m_NodesDictionary.ContainsKey(id) Then
                Throw New ProviderException((ErrMsg2 & "; Title: ") + reader.GetString(m_IndexTitle))
            End If

            ' Get title, URL, description, and roles from the DataReader
            Dim pageId As Guid = If(reader.IsDBNull(m_IndexID), Nothing, reader.GetGuid(m_IndexID))
            Dim isVisible As Boolean = If(reader.IsDBNull(m_IndexIsVisible), Nothing, reader.GetBoolean(m_IndexIsVisible))
            Dim disableLink As Boolean = If(reader.IsDBNull(m_IndexDisableLink), Nothing, reader.GetBoolean(m_IndexDisableLink))
            Dim title As String = If(reader.IsDBNull(m_IndexTitle), Nothing, reader.GetString(m_IndexTitle).Trim())
            Dim url As String = If(reader.IsDBNull(m_IndexUrl), Nothing, reader.GetString(m_IndexUrl).Trim())
            Dim description As String = If(reader.IsDBNull(m_IndexDesc), Nothing, reader.GetString(m_IndexDesc).Trim())
            Dim pType As Integer = If(reader.IsDBNull(m_IndexType), Nothing, reader.GetInt32(m_IndexType))
            'handle roles???

            'Dim roles As String = "" 'If(reader.IsDBNull(m_IndexRoles), Nothing, reader.GetString(m_IndexRoles).Trim())
            ' If roles were specified, turn the list into a string array
            Dim rolelist As String() = Nothing
            'If Not [String].IsNullOrEmpty(roles) Then
            '    rolelist = roles.Split(New Char() {","c, ";"c}, 4000)
            'End If

            If Not Me.HasPermission(pageId) Then
                Return Nothing
            End If

            Dim vUrl As String = url
            If pType = 1 Then
                vUrl = "~/" & vUrl & Meanstream.Portal.Core.AppConstants.EXTENSION
            ElseIf pType = 2 Then
                vUrl = vUrl & Meanstream.Portal.Core.AppConstants.EXTENSION & "?Id=" & Guid.NewGuid.ToString
            ElseIf pType = 3 Then
                vUrl = "~/" & Guid.NewGuid.ToString & Meanstream.Portal.Core.AppConstants.EXTENSION
            End If

            ' Create a SiteMapNode
            Dim node As New SiteMapNode(Me, pageId.ToString(), vUrl, title, description, rolelist, _
            Nothing, Nothing, Nothing)

            ' Record the node in the m_NodesDictionary dictionary
            m_NodesDictionary.Add(id, node)

            ' Return the node
            AddNode(node, GetParentNodeFromDataReader(reader))

            If pType = 3 Then 'workaround sitemapnode validation to include external sites (http://)
                node.Url = url
            ElseIf pType = 2 Then 'workaround sitemapnode validation to include duplicate url
                node.Url = "~/" & url & Meanstream.Portal.Core.AppConstants.EXTENSION
            End If

            If disableLink Then
                node.Url = "#"
            End If


            Return node
        End Function

        Private Function HasPermission(ByVal PageId As Guid) As Boolean
            Dim HasPermissions As Boolean = False
            'Check to see if page is public
            Dim CurrentPagePermission As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission) = Me.PagePermissions.FindAll("PageId", PageId)
            Dim AllUsers As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPagePermission) = CurrentPagePermission.FindAll("RoleId", Me.AllUserRole.Id)

            'If page is public then flag it
            If AllUsers.Count > 0 Then
                HasPermissions = True
            Else
                'check for authentication and query permissions
                If System.Web.HttpContext.Current.Request.IsAuthenticated Then
                    Dim MyRoles As List(Of Meanstream.Portal.Core.Membership.Role) = Me.GetUserRoles(System.Web.HttpContext.Current.Profile.UserName)
                    For Each MyRole As Meanstream.Portal.Core.Membership.Role In MyRoles
                        If CurrentPagePermission.FindAll("RoleId", MyRole.Id).Count > 0 Then
                            HasPermissions = True
                            Exit For
                        End If
                    Next
                End If
            End If
            Return HasPermissions
        End Function

        Private Function CreateRootNode() As SiteMapNode
            ' Create a SiteMapNode
            Dim node As New SiteMapNode(Me, "0", "ROOT", "ROOT", "ROOT", Nothing, _
            Nothing, Nothing, Nothing)

            ' Record the node in the m_NodesDictionary dictionary
            m_NodesDictionary.Add(0, node)
            Return node
        End Function

        Private Function GetParentNodeFromDataReader(ByVal reader As DbDataReader) As SiteMapNode
            ' Make sure the parent ID is present
            If reader.IsDBNull(m_IndexParent) Then
                Throw New ProviderException((ErrMsg3 & "; Title: ") + reader.GetString(m_IndexTitle))
            End If

            ' Get the parent ID from the DataReader

            Dim pid As Integer = BitConverter.ToInt32(reader.GetGuid(m_IndexParent).ToByteArray, 0)

            ' Make sure the parent ID is valid
            If pid <> 0 And Not m_NodesDictionary.ContainsKey(pid) Then
                Throw New ProviderException((ErrMsg4 & "; Title: ") + reader.GetString(m_IndexTitle))
            End If

            ' Return the parent SiteMapNode
            Return m_NodesDictionary(pid)
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