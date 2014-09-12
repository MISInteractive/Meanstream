
Partial Class Meanstream_Profile_UserControls_Compose
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim ds As System.Data.DataSet = Meanstream.Portal.Core.Membership.MembershipService.Current.SearchForUser("*")
            Meanstream.Portal.Core.Utilities.AppUtility.AlphabeticSort(ds.Tables(0), "UserName", 0)
            Me.cbbTo.DataSource = ds
            Me.cbbTo.DataBind()

            cbbTo.DataTextField = "UserName"
            cbbTo.DataValueField = "UserName"

            Me.btnSend.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-send.png"
        End If
    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If Me.cbbTo.SelectedValue.Trim = "" Then
            Me.btnSend.ThrowFailure = True
            Me.btnSend.FailMessage = "Select a user"
            Return
        End If

        Try
            Meanstream.Portal.Core.Messaging.MessagingService.Current.SendMessage(Me.cbbTo.SelectedValue, Profile.UserName, Me.txtSubject.Text, Me.txtMessage.Text, System.Net.Mail.MailPriority.Normal, Meanstream.Portal.Core.Messaging.Message.Type.MESSAGE_TYPE_USER, Me.cbSendEmail.Checked)
            Me.btnSend.SuccessMessage = "Your message was sent"
        Catch ex As Exception
            Me.btnSend.ThrowFailure = True
            Me.btnSend.FailMessage = ex.Message
        End Try
    End Sub
End Class
