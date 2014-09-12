
Partial Class Meanstream_Profile_UserControls_MyProfile
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Me.HiddenImage.Value = ""
            Me.ImgUpload.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-upload.png"
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"

            Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Profile.UserName)
            Me.txtFirstName.Text = ProfileCommon.FirstName
            Me.txtLastName.Text = ProfileCommon.LastName
            Me.txtWebsite.Text = ProfileCommon.Website
            Me.txtCity.Text = ProfileCommon.City

            If Profile.Portrait.Trim <> "" Then
                Me.imgPortrait.ImageUrl = Profile.Portrait
                Dim Path As String = Profile.Portrait
                If Not System.IO.File.Exists(Server.MapPath(Path)) Then
                    Me.imgPortrait.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "profile-default.jpg"
                End If
            Else
                Me.imgPortrait.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "profile-default.jpg"
            End If

            If Profile.Portrait <> "" Then
                Me.lblPictureMessage.Text = "Click Browse below to find a new image."
            End If
            Me.HTMLEditor.Text = ProfileCommon.AboutMe
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Profile.UserName)
        ProfileCommon.FirstName = Me.txtFirstName.Text.Trim
        ProfileCommon.LastName = Me.txtLastName.Text.Trim
        ProfileCommon.City = Me.txtCity.Text
        ProfileCommon.AboutMe = Me.HTMLEditor.Text
        ProfileCommon.Website = Me.txtWebsite.Text

        Try
            ProfileCommon.Save()
            Me.btnSave.SuccessMessage = "Save succesful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub ImgUpload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUpload.Click
        Me.lblPictureMessage.Text = ""
        Dim UserId As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUserGuid(Profile.UserName).ToString
        Dim Path As String = Server.MapPath(Profile.ProfileFolderPath & "\\Images\\")
        Dim FileNameWithPath As String = FileUpload.FileName
        ' get the extension name of the file
        Dim Extension As String = System.IO.Path.GetExtension(FileNameWithPath)
        Dim FileName As String = Guid.NewGuid.ToString & Extension

        Try
            If Not System.IO.Directory.Exists(Path) Then
                System.IO.Directory.CreateDirectory(Path)
            End If
            If Not System.IO.Directory.Exists(Path & "tmp\\") Then
                System.IO.Directory.CreateDirectory(Path & "tmp\\")
            End If
            If System.IO.File.Exists(Path & "tmp\\" & FileName) Then
                System.IO.File.Delete(Path & "tmp\\" & FileName)
            End If
            FileUpload.SaveAs(Path & "tmp\\" & FileName)
        Catch ex As Exception
            Me.lblPictureMessage.Text = ex.Message
        End Try

        Me.imgPortrait.ImageUrl = Profile.ProfileFolderPath & "Images/tmp/" & FileName

        'Update image
        If FileName <> "" Then
            Dim MembershipUser As MembershipUser = System.Web.Security.Membership.GetUser(Profile.UserName)
            Dim ProfileCommon As ProfileCommon = Profile.GetProfile(Profile.UserName)

            Extension = FileName.Split(".")(1)
            Dim NewFileName As String = MembershipUser.ProviderUserKey.ToString & "." & Extension
            Path = Server.MapPath(Profile.ProfileFolderPath & "\\Images\\")
            System.IO.File.Delete(Path & NewFileName)
            System.IO.File.Move(Path & "tmp\\" & FileName, Path & NewFileName)
            Me.imgPortrait.ImageUrl = Profile.ProfileFolderPath & "Images/" & NewFileName

            ProfileCommon.Portrait = Me.imgPortrait.ImageUrl
            ProfileCommon.Save()

            Me.lblPictureMessage.Text = "Picture upload Successful"
        End If

    End Sub
End Class
