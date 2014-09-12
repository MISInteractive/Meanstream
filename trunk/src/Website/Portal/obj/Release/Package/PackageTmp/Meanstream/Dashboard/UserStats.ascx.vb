
Partial Class Meanstream_Dashboard_UserStats
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.litUsers.Text = Meanstream.Portal.Core.Membership.MembershipService.Current.GetNumberOfProfiles
        Me.litUsersOnline.Text = Meanstream.Portal.Core.Membership.MembershipService.Current.GetNumberOfUsersOnline
        Me.litNewUsers.Text = Meanstream.Portal.Core.Membership.MembershipService.Current.GetNewUsersByDateRange(Date.Now.AddDays(-30), Date.Now).Tables(0).Rows.Count
    End Sub
End Class
