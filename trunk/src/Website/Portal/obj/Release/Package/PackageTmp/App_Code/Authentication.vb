Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Core.EntityModel
Imports Meanstream.Core.Repository

Public Class AuthUser
    Inherits Meanstream.Core.EntityModel.EntityBase

    <EntityField("username", System.Data.SqlDbType.NVarChar)> _
    Public Property Username() As String
        Get
            Return m_username
        End Get
        Set(value As String)
            m_username = value
        End Set
    End Property
    Private m_username As String

End Class

Public Class AuthUtil

    Public Shared Sub SignIn(ByVal user As AuthUser)
        If user.Id = Nothing Then
            user.Id = Guid.NewGuid
        End If
        If String.IsNullOrEmpty(user.Username) Then
            Throw New ArgumentException("Username")
        End If
        'clear all sessions if needed
        SignOut(user.Username)
        'create new session
        Repository.Insert(user)
    End Sub

    Public Shared Function IsValidUser(ByVal user As AuthUser) As Boolean
        If Repository.GetById(Of AuthUser)(user.Id) IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    Public Shared Sub SignOut(ByVal username As String)
        Dim query As New Query
        query.AppendEquals("Username", username)
        Dim list As List(Of AuthUser) = Repository.Find(Of AuthUser)(query)
        If list IsNot Nothing And list.Count > 0 Then
            For Each item As AuthUser In list
                Repository.Delete(item.Id)
            Next
        End If
    End Sub

    Public Shared Function GetDomains(ByVal currentDomain As String) As String
        Dim portals As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPortals) = _
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamPortalsProvider.GetAll()

        Dim strDomains As String = ""

        For Each portal As Meanstream.Portal.Core.Entities.MeanstreamPortals In portals
            If portals.IndexOf(portal) = 0 Then
                strDomains = strDomains & portal.Domain
            Else
                strDomains = strDomains & "," & portal.Domain
            End If
        Next

        'remove calling domain
        strDomains = strDomains.Replace(currentDomain & ",", "").Replace(currentDomain, "")
        'trim first and last ","
        strDomains = strDomains.TrimStart(",")
        strDomains = strDomains.TrimEnd(",")

        Return strDomains.Trim
    End Function

End Class
