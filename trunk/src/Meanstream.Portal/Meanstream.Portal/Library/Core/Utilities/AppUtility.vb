Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Configuration

Namespace Meanstream.Portal.Core.Utilities

    Public Class AppUtility

        ' <summary>
        ' Gets the relative root of the website.
        ' </summary>
        ' <value>A string that ends with a '/'.</value>
        Public Shared Function RelativeWebRoot() As String
            Return VirtualPathUtility.ToAbsolute("~/")
        End Function
        'alternatively
        'HttpContext.Current.Request.Url.Authority & HttpContext.Current.Request.ApplicationPath

        ' <summary>
        ' Gets the absolute root of the website.
        ' </summary>
        '<value>A string that ends with a '/'.</value>
        Public Shared Function AbsoluteWebRoot() As Uri
            Dim Context As HttpContext = HttpContext.Current
            If Context Is Nothing Then
                Throw New System.Net.WebException("The current HttpContext is null")
            End If
            If Context.Items("absoluteurl") Is Nothing Then
                Context.Items("absoluteurl") = New Uri(Context.Request.Url.GetLeftPart(UriPartial.Authority) & RelativeWebRoot())
            End If
            Return Context.Items("absoluteurl")
        End Function

        Public Shared Function GetCurrentSiteUrl() As String
            Dim portalContext As Meanstream.Portal.Core.PortalContext = Meanstream.Portal.Core.PortalContext.Current
            Dim root As String = RelativeWebRoot()
            Return String.Concat(portalContext.OriginalUri.Scheme, "://", portalContext.SiteUrl, root)
        End Function

        ' <summary>
        ' Gets any object by Type.
        ' </summary>
        ' <value>Object</value>
        Public Shared Function GetObjectByType(ByVal Type As Type) As Object
            Return System.Activator.CreateInstance(Type)
        End Function

        ' <summary>
        ' Gets any Type by Classname.
        ' </summary>
        ' <value>Type</value>
        Public Shared Function GetTypeByClassname(ByVal Classname As String) As Type
            Return Type.GetType(Classname)
        End Function

        Public Shared Function GetEnumValue(ByVal myEnum As System.Enum) As Integer
            Return DirectCast([Enum].Parse(myEnum.GetType(), myEnum.ToString()), Integer)
        End Function

        Public Shared Function GetGlobalType(ByVal s As String) As Type
            Dim t As Type = Nothing
            Dim av() As Reflection.Assembly = AppDomain.CurrentDomain.GetAssemblies()
            For Each a As Reflection.Assembly In av
                t = Type.[GetType](s & "," & a.FullName)
                If t IsNot Nothing Then
                    Exit For
                End If
            Next
            Return t
        End Function

        Public Shared Function BuildUrlForPage(ByVal portalId As Guid, ByVal url As String, ByVal parentId As Guid) As String
            Dim pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = AppUtility.GetAllPageEntities(portalId)

            If parentId <> Nothing Then
                url = RecurseForParent(url, parentId, pages)
            End If
            Return url
        End Function

        Private Shared Function RecurseForParent(ByVal url As String, ByVal parentId As Guid, ByVal pageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)) As String
            Dim page As Meanstream.Portal.Core.Entities.MeanstreamPage = pageList.Find("Id", parentId)
            If page.ParentId <> Nothing Then
                If page.Url.Contains("/") Then
                    page.Url = page.Url.Substring(page.Url.LastIndexOf("/") + 1)
                    url = page.Url & "/" & url
                    'send only if "/" exists
                    url = RecurseForParent(url, parentId, pageList)
                Else
                    url = RecurseForParent(page.Url & "/" & url, page.ParentId, pageList)
                End If
            Else
                url = page.Url & "/" & url
            End If
            Return url
        End Function

        Public Shared Function GetAllPageEntities(ByVal portalId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)
            Dim pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_ENTITIES" & "_PORTAL_" & portalId.ToString)
            If pages Is Nothing Then
                'get the clean page list
                pages = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" & portalId.ToString & " AND IsDeleted=False")
                'cache the pages
                Meanstream.Portal.Core.Utilities.CacheUtility.Add(Meanstream.Portal.Core.Utilities.CacheUtility.ALL_PAGES & "_ENTITIES" & "_PORTAL_" & portalId.ToString, pages)
            End If
            Return pages
        End Function

        'issues with AppendLessThanOrEqual or AppendGreaterThanOrEqual
        Public Shared Function GetPageNavigationEntities(ByVal portalId As Guid) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage)
            Dim pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.CacheUtility.GetCachedObject(Meanstream.Portal.Core.Utilities.CacheUtility.MENU & "_ENTITIES" & "_PORTAL_" & portalId.ToString)
            If pages Is Nothing Then
                Dim Query As Meanstream.Portal.Core.Data.MeanstreamPageQuery = New Meanstream.Portal.Core.Data.MeanstreamPageQuery
                Query.AppendEquals(Entities.MeanstreamPageColumn.PortalId, portalId.ToString)
                Query.AppendEquals(Entities.MeanstreamPageColumn.IsVisible, "True")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.IsDeleted, "False")
                Query.AppendEquals("AND", Entities.MeanstreamPageColumn.IsPublished, "True")
                Query.AppendLessThanOrEqual("AND", Meanstream.Portal.Core.Entities.MeanstreamPageColumn.StartDate, Date.Now.ToString)
                Query.AppendGreaterThanOrEqual("AND", Entities.MeanstreamPageColumn.EndDate, Date.Now.ToString)

                pages = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find(Query.Parameters)
                'cache the pages
                Meanstream.Portal.Core.Utilities.CacheUtility.Add(Meanstream.Portal.Core.Utilities.CacheUtility.MENU & "_ENTITIES" & "_PORTAL_" & portalId.ToString, pages)
            End If
            Return pages
        End Function

        Public Shared Function GetDatasource() As String
            Dim ConnectionStringName As String = Meanstream.Portal.Core.Data.DataRepository.NetTiersSection.Providers("MeanstreamCoreProvider").Parameters("connectionStringName")
            Dim Con As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings(ConnectionStringName).ToString)
            Dim Name As String = Con.Database
            Con.Close()
            Return Name
        End Function

        Public Shared Function GetBreadCrumbs(ByVal portalId As Guid, ByVal pageId As Guid, ByVal delimiter As String) As String
            Dim Path As String = ""
            Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = AppUtility.GetAllPageEntities(portalId)
            Dim Page As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", pageId)
            If Page.ParentId <> Nothing Then
                Dim Parent As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", Page.ParentId)

                If Parent Is Nothing Then
                    Return Page.Url '& Meanstream.Portal.Core.AppicationConst.EXTENSION
                End If

                If Parent.ParentId <> Nothing Then
                    Dim Parent2 As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", Parent.ParentId)

                    If Parent2 Is Nothing Then
                        Return Page.Url '& Meanstream.Portal.Core.AppicationConst.EXTENSION
                    End If

                    If Parent2.ParentId <> Nothing Then
                        Dim Parent3 As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", Parent2.ParentId)
                        If Parent3 Is Nothing Then
                            Return Page.Url ' & Meanstream.Portal.Core.AppicationConst.EXTENSION
                        End If
                        Path = Parent3.Name.ToUpper & delimiter & Parent2.Name.ToUpper & " > " & Parent.Name.ToUpper
                    Else
                        Path = Parent2.Name.ToUpper & delimiter & Parent.Name.ToUpper
                    End If
                Else
                    Path = Parent.Name.ToUpper
                End If

                Path = Path & delimiter & Page.Url '& Meanstream.Portal.Core.AppicationConst.EXTENSION
            Else
                Path = Page.Url '& Meanstream.Portal.Core.AppicationConst.EXTENSION
            End If
            Return Path
        End Function

        Public Shared Function AlphabeticSort(ByVal dtTable As System.Data.DataTable, ByVal column As String, ByVal sortOrder As Integer) As System.Data.DataTable
            Dim dsSorted As New System.Data.DataSet
            Dim columnKey As String = column
            Dim sortDirection As String = ""
            Dim sortFormat As String = "{0} {1}"
            Select Case sortOrder
                Case 0
                    sortDirection = "ASC"
                Case 1
                    sortDirection = "DESC"
                Case Else
                    sortDirection = "ASC"
            End Select

            dtTable.DefaultView.Sort = String.Format(sortFormat, columnKey, sortDirection)
            Return dtTable.DefaultView.Table
        End Function
    End Class
End Namespace

