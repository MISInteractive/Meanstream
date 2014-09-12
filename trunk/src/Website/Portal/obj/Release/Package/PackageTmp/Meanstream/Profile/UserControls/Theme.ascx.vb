
Partial Class Meanstream_Profile_UserControls_Theme
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.litTheme.Text = Page.Theme
            'Find Meanstream Themes under the App_Themes Directory
            Dim VirtualPathProvider As System.Web.Hosting.VirtualPathProvider = System.Web.Hosting.HostingEnvironment.VirtualPathProvider
            Dim AppThemeDirectories As System.Web.Hosting.VirtualDirectory = VirtualPathProvider.GetDirectory("~/App_Themes")
            Dim Themes As List(Of System.Web.Hosting.VirtualDirectory) = New List(Of System.Web.Hosting.VirtualDirectory)
            For Each Directory As System.Web.Hosting.VirtualDirectory In AppThemeDirectories.Directories
                If Directory.Name.StartsWith("Meanstream.") Then
                    Themes.Add(Directory)
                End If
            Next

            Me.cbPreferences.DataSource = Themes
            Me.cbPreferences.DataTextField = "Name"
            Me.cbPreferences.DataValueField = "Name"
            Me.cbPreferences.DataBind()
            Me.cbPreferences.SelectedValue = Page.Theme

            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Meanstream.Portal.Core.Membership.MembershipService.Current.UpdateMeanstreamThemeForUser(Profile.UserName, Me.cbPreferences.SelectedValue)
            Me.litTheme.Text = Me.cbPreferences.SelectedValue
            Response.Redirect("Default.aspx?ctl=MyProfile")
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
