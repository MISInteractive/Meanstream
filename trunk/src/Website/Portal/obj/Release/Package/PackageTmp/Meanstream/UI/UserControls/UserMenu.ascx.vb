
Partial Class Meanstream_UI_UserControls_UserMenu
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim UnreadMessages As Integer = Meanstream.Portal.Core.Messaging.MessagingService.Current.GetUnreadCount(Profile.UserName)
            btnInbox.Text = "Inbox (" & UnreadMessages & ")"
        End If
    End Sub
End Class
