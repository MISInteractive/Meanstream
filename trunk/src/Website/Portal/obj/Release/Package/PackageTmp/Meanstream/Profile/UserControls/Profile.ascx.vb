
Partial Class Meanstream_Profile_UserControls_Profile
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Username As String = ""
        If Request.Params("UID") <> Nothing Then
            Username = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("UID").ToString))
        End If

        If Request.Params("Username") <> Nothing Then
            Username = Request.Params("Username").ToString
        End If

        If Username.Trim = "" Then
            Me.litName.Text = "User Does Not Exist"
            Exit Sub
        End If

        Me.imgPortrait.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/Images/profile-default.jpg"

        Try
            Dim MembershipUser As MembershipUser = Membership.GetUser(Username)
            Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Username)

            Me.litName.Text = ProfileCommon.FirstName & " " & ProfileCommon.LastName
            If litName.Text.Trim = "" Then
                litName.Text = Username
            End If
            Me.litMemberSince.Text = MembershipUser.CreationDate.ToString("MMM dd, yyyy")
            Me.litLastVisit.Text = MembershipUser.LastLoginDate.ToString("MMM dd, yyyy")
            Me.litAboutMe.Text = ProfileCommon.AboutMe
            Me.btnWebsite.Text = ProfileCommon.Website
            Me.btnWebsite.NavigateUrl = ProfileCommon.Website
            Me.litCity.Text = ProfileCommon.City

            If Not ProfileCommon.Portrait.Trim = "" Then
                If System.IO.File.Exists(Server.MapPath(ProfileCommon.Portrait)) Then
                    Me.imgPortrait.ImageUrl = ProfileCommon.Portrait
                End If
            End If

            Me.btnFacebook.NavigateUrl = ProfileCommon.Facebook

            If ProfileCommon.Facebook.Trim = "" Then
                Me.btnFacebook.Visible = False
            End If
        Catch ex As Exception
            Me.litName.Text = "User Does Not Exist"
        End Try

    End Sub
End Class
