Imports System.Net.Mail

Partial Class Meanstream_Profile_UserControls_Received
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Dim Messages As List(Of Meanstream.Portal.Core.Messaging.Message) = Meanstream.Portal.Core.Messaging.MessagingService.Current.GetAllRecieved(Profile.UserName)
        If Messages.Count > 0 Then
            Me.Messages.DataSource = Messages
            Me.Messages.DataBind()
        Else
            Me.Container.Visible = False
            Me.lblStatus.Text = "No Records Found"
        End If
    End Sub

    Protected Sub Messages_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Messages.PageIndexChanging
        Messages.PageIndex = e.NewPageIndex
        Messages.EditIndex = -1
        BindGrid()
    End Sub

    Protected Function GetUsername(ByVal UserId As Object) As String
        If UserId.ToString = Nothing Then
            Return "System"
        End If

        Try
            Return Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(UserId.ToString))
        Catch ex As Exception
        End Try
        Return "User Not Found"
    End Function

    Protected Function GetBody(ByVal Body As String) As String
        If Body = Nothing Then
            Return ""
        End If

        If Body.Length > 50 Then
            Body = Body.Substring(0, 50) & "..."
        End If

        Return Body
    End Function

    Protected Function GetProfileImage(ByVal UserId As Object) As String
        Dim Portrait As String = ""

        Try
            If UserId.ToString = Nothing Then
                Return "~/App_Themes/" & Me.Page.Theme & "/images/profile-default.jpg"
            End If

            Portrait = Profile.GetProfile(Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(UserId.ToString))).Portrait
        Catch ex As Exception
        End Try

        If Portrait = "" Then
            Return "~/App_Themes/" & Me.Page.Theme & "/images/profile-default.jpg"
        End If

        Return Portrait
    End Function

    Protected Function CanReply(ByVal UserId As Object) As Boolean
        If UserId.ToString = Nothing Then
            Return False
        End If

        If UserId Is Nothing Then
            Return False
        End If

        Return True
    End Function

    Protected Function Status(ByVal Opened As Boolean) As String
        If Opened Then
            Return "Opened"
        End If

        Return "Unopened"
    End Function

    Protected Sub Messages_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Messages.RowDeleting

    End Sub

    Protected Sub Messages_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Messages.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Try
                    Dim rowIndex As Integer = CInt(e.CommandArgument)
                    Dim Id As String = DirectCast(Messages.Rows(rowIndex).FindControl("lblId"), Label).Text
                    Meanstream.Portal.Core.Messaging.MessagingService.Current.DeleteMessage(New Guid(Id))
                    Me.Messages.EditIndex = -1
                    BindGrid()
                    Me.lblStatus.Text = "Message deleted successfully"
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try

            Case "Body"
                Dim Id As String = e.CommandArgument

                Dim btnBody As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim GridViewRow As GridViewRow = TryCast(btnBody.NamingContainer, GridViewRow)
                Dim rowIndex As Integer = GridViewRow.RowIndex

                Dim Message As Meanstream.Portal.Core.Messaging.Message = Meanstream.Portal.Core.Messaging.MessagingService.Current.GetMessage(New Guid(Id))

                Dim SentFrom As String = DirectCast(Messages.Rows(rowIndex).FindControl("lblSentFrom"), Label).Text
                DirectCast(Messages.Rows(rowIndex).FindControl("Reply"), HtmlTableRow).Visible = Me.CanReply(SentFrom)
                DirectCast(Messages.Rows(rowIndex).FindControl("litBody"), Literal).Text = Message.Body
                DirectCast(Messages.Rows(rowIndex).FindControl("btnBody"), LinkButton).Visible = False
                DirectCast(Messages.Rows(rowIndex).FindControl("litReply"), Literal).Visible = True
                DirectCast(Messages.Rows(rowIndex).FindControl("txtComment"), TextBox).Visible = True
                DirectCast(Messages.Rows(rowIndex).FindControl("btnSend"), ImageButton).Visible = True

                Dim manager As New Meanstream.Portal.Core.Messaging.MessageManager(Message)
                Message.Opened = True
                manager.Save()

            Case "Send"
                Dim Id As String = e.CommandArgument
                Dim btnSend As LinkButton = TryCast(e.CommandSource, LinkButton)
                Dim GridViewRow As GridViewRow = TryCast(btnSend.NamingContainer, GridViewRow)
                Dim rowIndex As Integer = GridViewRow.RowIndex
                Dim Message As Meanstream.Portal.Core.Messaging.Message = Meanstream.Portal.Core.Messaging.MessagingService.Current.GetMessage(New Guid(Id))
                Dim Body As String = DirectCast(Messages.Rows(rowIndex).FindControl("txtComment"), TextBox).Text
                Dim MessageLabel As Label = DirectCast(Messages.Rows(rowIndex).FindControl("lblMessage"), Label)

                Try
                    Meanstream.Portal.Core.Messaging.MessagingService.Current.SendMessage(Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(Message.SentFrom), Profile.UserName, "RE: " & Message.Subject, Body & "<br><br><br>On " & Message.DateSent & " " & Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(Message.SentFrom) & " Sent:<br><br>" & Message.Body, System.Net.Mail.MailPriority.Normal, Meanstream.Portal.Core.Messaging.Message.Type.MESSAGE_TYPE_USER, True)
                    lblStatus.Text = "Message sent successfully"
                Catch ex As Exception
                    lblStatus.Text = ex.Message
                End Try

        End Select
    End Sub
End Class
