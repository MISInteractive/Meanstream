
Partial Class Meanstream_Administration_UserControls_Credentials
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim Username As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("uid")))
            Me.LoadUserByID(Username)
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            Me.btnRunAsUser.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-run-user.png"
        End If
    End Sub

    Protected Sub LoadUserByID(ByVal Username As String)
        Dim MembershipUser As MembershipUser = System.Web.Security.Membership.GetUser(Username)

        If MembershipUser.UserName = "admin" Or _
           MembershipUser.UserName = "host" Then
            Me.btnRunAsUser.Visible = False
        End If

        Me.txtEmail.Text = MembershipUser.Email
        Me.litUsername.Text = MembershipUser.UserName
        CreationDate.Text = MembershipUser.CreationDate
        IsApproved.Text = MembershipUser.IsApproved
        IsLockedOut.Text = MembershipUser.IsLockedOut
        IsOnline.Text = MembershipUser.IsOnline
        LastActivityDate.Text = MembershipUser.LastActivityDate
        LastLockoutDate.Text = MembershipUser.LastLockoutDate
        LastLoginDate.Text = MembershipUser.LastLoginDate
        LastPasswordChangedDate.Text = MembershipUser.LastPasswordChangedDate
        Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Username)
        txtFirstName.Text = ProfileCommon.FirstName
        txtLastName.Text = ProfileCommon.LastName
        txtDisplayName.Text = ProfileCommon.DisplayName

        If MembershipUser.IsLockedOut Then
            ckbLockout.Checked = True
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim result As String = ""

        Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Me.litUsername.Text.Trim)
        ProfileCommon.FirstName = txtFirstName.Text.Trim
        ProfileCommon.LastName = txtLastName.Text.Trim
        ProfileCommon.DisplayName = txtDisplayName.Text.Trim
        ProfileCommon.Save()

        Dim MyMembershipUser As MembershipUser = Membership.GetUser(Me.litUsername.Text.Trim)

        If MyMembershipUser.Email <> Me.txtEmail.Text.Trim Then
            'validate email
            If Membership.FindUsersByEmail(Me.txtEmail.Text.Trim).Count > 0 Then
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = "Email address is being used by another user"
                Return
            End If
        End If

        MyMembershipUser.Email = Me.txtEmail.Text

        If Me.ckbLockout.Checked Then
        Else
            MyMembershipUser.IsApproved = True
            MyMembershipUser.UnlockUser()
        End If

        Membership.UpdateUser(MyMembershipUser)
        Me.btnSave.SuccessMessage = "Save sucessful"
        Me.LoadUserByID(litUsername.Text)
    End Sub

    Protected Sub btnRunAsUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRunAsUser.Click
        FormsAuthentication.RedirectFromLoginPage(Me.litUsername.Text, False)
    End Sub
End Class
