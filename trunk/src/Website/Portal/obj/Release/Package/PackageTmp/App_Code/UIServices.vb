Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Serialization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Services

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class UIServices
    Inherits System.Web.Services.WebService
    <WebMethod()> _
    Public Function Pages_FindAll() As List(Of Meanstream.UI.Services.Models.Page)

        Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Page)
        Dim Pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)

        For Each p As Meanstream.Portal.Core.Entities.MeanstreamPage In Pages
            Dim page As New Meanstream.UI.Services.Models.Page
            page.Id = p.Id.ToString
            page.Path = page.GetPath(p.Id, portalId)
            page.Skin = page.GetSkinName(p.SkinId)
            page.VisibleFlag = page.GetFlag(p.IsVisible)
            page.Author = p.Author
            page.PublishedFlag = page.GetFlag(p.IsPublished)
            Dim pDate As String = ""
            Try
                pDate = p.PublishedDate
            Catch ex As Exception

            End Try
            page.PublishedDate = pDate

            model.Add(page)
        Next


        Return model
    End Function
    <WebMethod()> _
    Public Function EventLogs_FindAll() As List(Of Meanstream.UI.Services.Models.EventLog)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.EventLog)
        Dim Logs As List(Of Meanstream.Portal.Core.Messaging.ApplicationMessage) = Meanstream.Portal.Core.Messaging.ApplicationMessagingManager.Current.GetApplicationMessages()

        Dim i As Integer = 0
        For Each p As Meanstream.Portal.Core.Messaging.ApplicationMessage In Logs
            Dim msg As New Meanstream.UI.Services.Models.EventLog
            msg.Message = p.Message
            msg.MessageType = p.MessageType
            i = i + 1
            msg.Id = i.ToString()
            model.Add(msg)
        Next


        Return model
    End Function
    <WebMethod()> _
    Public Function Roles_FindAll() As List(Of Meanstream.UI.Services.Models.Role)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Role)
        Dim Roles As List(Of Meanstream.Portal.Core.Membership.Role) = Meanstream.Portal.Core.Membership.MembershipService.Current.GetAllRoles

        Dim i As Integer = 0
        For Each p As Meanstream.Portal.Core.Membership.Role In Roles
            Dim role As New Meanstream.UI.Services.Models.Role
            role.Name = p.Name
            role.Description = p.Description
            role.AutoAssignment = role.GetFlag(p.AutoAssignment)
            role.IsPublic = role.GetFlag(p.IsPublic)
            role.Id = p.Id.ToString
            model.Add(role)
        Next


        Return model
    End Function
    <WebMethod()> _
    Public Function Role_Destroy(ByVal obj As Object) As String

        Dim rolename As String = obj("Role").ToString
        Dim role As Meanstream.Portal.Core.Membership.Role = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleByName(rolename)
        Dim manager As New Meanstream.Portal.Core.Membership.RoleManager(role)
        manager.Delete()

        Return "success"

    End Function
    <WebMethod()> _
    Public Function UserInRole_FindAll(ByVal obj As Object) As List(Of Meanstream.UI.Services.Models.UsersInRole)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.UsersInRole)
        Dim roleId As String = obj("RoleID").ToString

        Dim UsersDataSet As System.Data.DataSet = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsersinRoleDataSet(New Guid(roleId))
        Dim dt As DataTable = UsersDataSet.Tables(0)
        Dim n As Integer = dt.Rows.Count
        n = n - 1
        Dim i As Integer = 0
        For i = 0 To n

            Dim user As New Meanstream.UI.Services.Models.UsersInRole
            user.UserName = dt.Rows(i)("UserName")
            user.UserId = dt.Rows(i)("UserId").ToString
            user.LastActivityDate = dt.Rows(i)("LastActivityDate").ToString
            user.IsAnonymous = user.GetFlag(dt.Rows(i)("IsAnonymous"))

            model.Add(user)

        Next



        Return model
    End Function
    <WebMethod()> _
    Public Function UserInRole_Destroy(ByVal obj As Object) As String
        Dim roleId As String = obj("RoleID").ToString
        Dim UserName As String = obj("UserName").ToString
        Dim RoleName As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetRoleById(New Guid(roleId)).Name
        Meanstream.Portal.Core.Membership.MembershipService.Current.RemoveUserFromRole(UserName, RoleName)


        Return "success"

    End Function
    <WebMethod()> _
    Public Function Sitemap_FindAll() As List(Of Meanstream.UI.Services.Models.Sitemap)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Sitemap)
        Dim i As Integer = 0
        For Each m As System.Web.SiteMapProvider In System.Web.SiteMap.Providers
            Dim p As New Meanstream.UI.Services.Models.Sitemap
            i = i + 1
            p.Id = i.ToString
            p.Name = m.Name
            p.RootProvider = m.RootProvider.ToString
            p.Enabled = IIf(m.RootProvider.ToString = System.Web.SiteMap.Provider.GetType.FullName, True, False)

            model.Add(p)


        Next


        Return model
    End Function
    <WebMethod()> _
    Public Function Tracing_FindAll() As List(Of Meanstream.UI.Services.Models.Tracing)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Tracing)


        Dim ds As DataSet = Meanstream.Portal.Core.Instrumentation.PortalTrace.GetTracing
        Dim dt As DataTable = ds.Tables(0)
        Dim n As Integer = dt.Rows.Count
        n = n - 1
        Dim i As Integer = 0
        For i = 0 To n

            Dim t As New Meanstream.UI.Services.Models.Tracing
            t.TraceDateTime = dt.Rows(i)("TraceDateTime")
            t.Id = (i + 1).ToString
            t.TraceCategory = dt.Rows(i)("TraceCategory").ToString
            t.TraceDescription = dt.Rows(i)("TraceDescription")

            model.Add(t)

        Next



        Return model
    End Function
    <WebMethod()> _
    Public Function Tracing_Destroy() As String
        Meanstream.Portal.Core.Instrumentation.PortalTrace.CleanTracing()

        Return "success"

    End Function
    <WebMethod()> _
    Public Function User_FindAll() As List(Of Meanstream.UI.Services.Models.Users)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Users)


        Dim ds As DataSet = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsers
        Dim dt As DataTable = ds.Tables(0)
        Dim n As Integer = dt.Rows.Count
        n = n - 1
        Dim i As Integer = 0
        For i = 0 To n

            Dim u As New Meanstream.UI.Services.Models.Users
            u.UserId = dt.Rows(i)("UserId").ToString
            u.UserName = dt.Rows(i)("UserName")
            u.LastActivityDate = dt.Rows(i)("LastActivityDate").ToString
            u.LastLoginDate = dt.Rows(i)("LastLoginDate")
            u.IsLockedOut = u.GetFlag(dt.Rows(i)("IsLockedOut"))
            u.CreateDate = dt.Rows(i)("CreateDate")
            u.Email = dt.Rows(i)("Email")

            model.Add(u)

        Next



        Return model
    End Function
    <WebMethod()> _
    Public Function User_FindLike(ByVal obj As Object) As List(Of Meanstream.UI.Services.Models.Users)

        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Users)
        Dim Search As String = obj("Search").ToString

        Dim ds As DataSet = Meanstream.Portal.Core.Membership.MembershipService.Current.SearchForUser(Search, Search)
        Dim dt As DataTable = ds.Tables(0)
        Dim n As Integer = dt.Rows.Count
        n = n - 1
        Dim i As Integer = 0
        For i = 0 To n

            Dim u As New Meanstream.UI.Services.Models.Users
            u.UserId = dt.Rows(i)("UserId").ToString
            u.UserName = dt.Rows(i)("UserName")
            u.LastActivityDate = dt.Rows(i)("LastActivityDate").ToString
            u.LastLoginDate = dt.Rows(i)("LastLoginDate")
            u.IsLockedOut = u.GetFlag(dt.Rows(i)("IsLockedOut"))
            u.CreateDate = dt.Rows(i)("CreateDate")
            u.Email = dt.Rows(i)("Email")

            model.Add(u)

        Next



        Return model
    End Function
    <WebMethod()> _
    Public Function User_Destroy(ByVal obj As Object) As String
        Dim UserName As String = obj("UserName").ToString
        Meanstream.Portal.Core.Membership.MembershipService.Current.DeleteUser(UserName)
        Return "success"

    End Function
    <WebMethod()> _
    Public Function Skin_FindAll() As List(Of Meanstream.UI.Services.Models.Skin)

        Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim model As New List(Of Meanstream.UI.Services.Models.Skin)
        Dim Skins As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkins) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Find("PortalId=" & portalId.ToString)
        For Each s As Meanstream.Portal.Core.Entities.MeanstreamSkins In Skins
            Dim skin As New Meanstream.UI.Services.Models.Skin
            skin.Id = s.Id.ToString
            skin.SkinRoot = s.SkinRoot
            skin.SkinSrc = skin.ParseFilePath(s.SkinSrc)
            skin.Assigned = skin.GetPageCount(s.Id.ToString)

            model.Add(skin)
        Next




        Return model
    End Function

    <WebMethod()> _
    Public Function Skin_Destroy(ByVal obj As Object) As String
        Dim Id As String = obj("Id").ToString
        Dim skin As Meanstream.Portal.Core.Content.Skin = Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(New Guid(Id))
        Dim manager As New Meanstream.Portal.Core.Content.SkinManager(skin)
        manager.Delete()

        Return "success"

    End Function
           
    
           
               
                <WebMethod()> _
    Public Function testPortalId() As String

        Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Return portalId.ToString

    End Function
End Class

Namespace Meanstream.UI.Services.Models
    Public Class Page
        Public Id As String
        Public Path As String
        Public Skin As String
        Public VisibleFlag As String
        Public Author As String
        Public PublishedFlag As String
        Public PublishedDate As String
        Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
            If TrueOrFalse Then
                Return "yes"
            End If
            Return "no"
        End Function
        Public Function GetSkinName(ByVal SkinID As Guid) As String
            Return Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(SkinID).Name
        End Function
        Public Function GetPath(ByVal pageId As Guid, ByVal PortalId As Guid) As String
            Return Meanstream.Portal.Core.Utilities.AppUtility.GetBreadCrumbs(PortalId, pageId, " > ")
        End Function
    End Class
    Public Class EventLog
        Public Id As String
        Public Message As String
        Public MessageType As String

    End Class
    Public Class Role
        Public Id As String
        Public Name As String
        Public Description As String
        Public AutoAssignment As String
        Public IsPublic As String
        Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
            If TrueOrFalse Then
                Return "yes"
            End If
            Return "no"
        End Function
    End Class
    Public Class UsersInRole
        Public UserName As String
        Public UserId As String
        Public IsAnonymous As String
        Public LastActivityDate As String
        Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
            If TrueOrFalse Then
                Return "yes"
            End If
            Return "no"
        End Function
    End Class
    Public Class Sitemap
        Public Id As String
        Public Name As String
        Public RootProvider As String
        Public Enabled As String

    End Class
    Public Class Tracing
        Public Id As String
        Public TraceDateTime As String
        Public TraceCategory As String
        Public TraceDescription As String

    End Class
    Public Class Users
        Public UserName As String
        Public UserId As String
        Public Email As String
        Public IsLockedOut As String
        Public LastLoginDate As String
        Public LastActivityDate As String
        Public CreateDate As String
        Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
            If TrueOrFalse Then
                Return "yes"
            End If
            Return "no"
        End Function
    End Class
    Public Class Skin
        Public Id As String
        Public SkinRoot As String
        Public SkinSrc As String
        Public Assigned As String
        Public Function GetPageCount(ByVal SkinId As String) As Integer
            Return Meanstream.Portal.Core.Content.ContentService.Current.GetPagesBySkinId(New Guid(SkinId)).Count
        End Function


        Public Function ParseFilePath(ByVal SkinSrc As String) As String
            Return SkinSrc.Substring(SkinSrc.LastIndexOf("/") + 1)
        End Function
    End Class
        End Namespace