
Partial Class Meanstream_Profile_UserControls_Sent
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Dim Messages As List(Of Meanstream.Portal.Core.Messaging.Message) = Meanstream.Portal.Core.Messaging.MessagingService.Current.GetAllSent(Profile.UserName)
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

    Protected Function GetUsername(ByVal UserId As Guid) As String
        If UserId = Nothing Then
            Return "System"
        End If

        Try
            Return Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(UserId)
        Catch ex As Exception
        End Try

        Return "User Not Found"
    End Function

    Protected Function GetBody(ByVal Body As String) As String
        If Body.Length > 50 Then
            Body = Body.Substring(0, 50) & "..."
        End If

        Return Body
    End Function

    Protected Sub Messages_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Messages.RowDeleting

    End Sub

    Protected Function GetProfileImage(ByVal UserId As Guid) As String
        Dim Portrait As String = ""
        Try
            If UserId = Nothing Then
                Return "~/App_Themes/" & Me.Page.Theme & "/images/profile-default.jpg"
            End If

            Portrait = Profile.GetProfile(Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(UserId)).Portrait
        Catch ex As Exception
        End Try

        If Portrait = "" Then
            Return "~/App_Themes/" & Me.Page.Theme & "/images/profile-default.jpg"
        End If

        Return Portrait
    End Function

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
                DirectCast(Messages.Rows(rowIndex).FindControl("litBody"), Literal).Text = Message.Body
                DirectCast(Messages.Rows(rowIndex).FindControl("btnBody"), LinkButton).Visible = False
        End Select
    End Sub
End Class
