
Partial Class Meanstream_Default
    Inherits System.Web.UI.Page

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId


    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreInit
        Page.Theme = Meanstream.Portal.Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(Profile.UserName)
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.SetDashboard()
        End If
    End Sub


    Private Sub SetDashboard()
        Dim Page As Meanstream.Portal.Core.Entities.MeanstreamPage = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId).Find("IsHome", True)
        If Page IsNot Nothing Then
            Me.HomeLink1.NavigateUrl = Page.Url
            Me.HomeLink2.NavigateUrl = Page.Url
        Else
            Me.HomeLink1.NavigateUrl = "~/"
            Me.HomeLink2.NavigateUrl = "~/"
        End If
        Dim UserId As Guid = Profile.UserId
        Me.SetMyRecentEditsForUser(UserId)
        Me.SetQuickLinksForUser(UserId)
        Me.SetUserStatsForUser(UserId)
        Me.SetRecentPublishedPagesForUser(UserId)
        'Me.SetAnnouncementsForUser(UserId)
    End Sub


    Const MEANSTREAM_DASHBOARD_MY_RECENT_EDITS As String = "MEANSTREAM_DASHBOARD_MY_RECENT_EDITS"
    Private Sub SetMyRecentEditsForUser(ByVal UserId As Guid)
        Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_MY_RECENT_EDITS)
        If UserPreference Is Nothing Then
            'add user to preference
            Dim Preference As Meanstream.Portal.Core.Membership.Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_MY_RECENT_EDITS)
            If Preference Is Nothing Then
                'add new preference if one does not exist
                Meanstream.Portal.Core.Membership.MembershipService.Current.CreatePreference(MEANSTREAM_DASHBOARD_MY_RECENT_EDITS)
                Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_MY_RECENT_EDITS)
            End If
            Profile.AddUserPreference(Preference.PreferenceId, True)
            UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_MY_RECENT_EDITS)
        End If

        If UserPreference.Value = True Then
            Me.MyRecentEdits.Visible = True
        End If
    End Sub


    Const MEANSTREAM_DASHBOARD_QUICKLINKS As String = "MEANSTREAM_DASHBOARD_QUICKLINKS"
    Private Sub SetQuickLinksForUser(ByVal UserId As Guid)
        Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_QUICKLINKS)
        If UserPreference Is Nothing Then
            'add user to preference
            Dim Preference As Meanstream.Portal.Core.Membership.Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_QUICKLINKS)
            If Preference Is Nothing Then
                'add new preference if one does not exist
                Meanstream.Portal.Core.Membership.MembershipService.Current.CreatePreference(MEANSTREAM_DASHBOARD_QUICKLINKS)
                Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_QUICKLINKS)
            End If
            Profile.AddUserPreference(Preference.PreferenceId, True)
            UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_QUICKLINKS)
        End If

        If UserPreference.Value = True Then
            Me.QuickLinks.Visible = True
        End If
    End Sub


    Const MEANSTREAM_DASHBOARD_USER_STATS As String = "MEANSTREAM_DASHBOARD_USER_STATS"
    Private Sub SetUserStatsForUser(ByVal UserId As Guid)
        Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_USER_STATS)
        If UserPreference Is Nothing Then
            'add user to preference
            Dim Preference As Meanstream.Portal.Core.Membership.Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_USER_STATS)
            If Preference Is Nothing Then
                'add new preference if one does not exist
                Meanstream.Portal.Core.Membership.MembershipService.Current.CreatePreference(MEANSTREAM_DASHBOARD_USER_STATS)
                Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_USER_STATS)
            End If
            Profile.AddUserPreference(Preference.PreferenceId, True)
            UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_USER_STATS)
        End If

        If UserPreference.Value = True Then
            Me.UserStats.Visible = True
        End If
    End Sub


    Const MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES As String = "MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES"
    Private Sub SetRecentPublishedPagesForUser(ByVal UserId As Guid)
        Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES)
        If UserPreference Is Nothing Then
            'add user to preference
            Dim Preference As Meanstream.Portal.Core.Membership.Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES)
            If Preference Is Nothing Then
                'add new preference if one does not exist
                Meanstream.Portal.Core.Membership.MembershipService.Current.CreatePreference(MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES)
                Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES)
            End If
            Profile.AddUserPreference(Preference.PreferenceId, True)
            UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES)
        End If

        If UserPreference.Value = True Then
            Me.RecentPublishedPages.Visible = True
        End If
    End Sub


    'Const MEANSTREAM_DASHBOARD_ANNOUNCEMENTS As String = "MEANSTREAM_DASHBOARD_ANNOUNCEMENTS"
    'Private Sub SetAnnouncementsForUser(ByVal UserId As Guid)
    '    Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_ANNOUNCEMENTS)
    '    If UserPreference Is Nothing Then
    '        'add user to preference
    '        Dim Preference As Meanstream.Portal.Core.Membership.Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_ANNOUNCEMENTS)
    '        If Preference Is Nothing Then
    '            'add new preference if one does not exist
    '            Meanstream.Portal.Core.Membership.MembershipService.Current.CreatePreference(MEANSTREAM_DASHBOARD_ANNOUNCEMENTS)
    '            Preference = Meanstream.Portal.Core.Membership.MembershipService.Current.GetPreference(MEANSTREAM_DASHBOARD_ANNOUNCEMENTS)
    '        End If
    '        Profile.AddUserPreference(Preference.PreferenceId, True)
    '        UserPreference = Profile.GetUserPreference(MEANSTREAM_DASHBOARD_ANNOUNCEMENTS)
    '    End If

    '    If UserPreference.Value = True Then
    '        Me.Announcements.Visible = False
    '    End If
    'End Sub

End Class
