
Partial Class Meanstream_Profile_UserControls_Dashboard
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Me.LoadDashboard()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Private Sub LoadDashboard()
        For Each ListItem As ListItem In Me.cbPreferences.Items
            Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(ListItem.Value)
            If UserPreference Is Nothing Then
                Continue For
            End If
            If UserPreference.Value = "True" Then
                ListItem.Selected = True
            End If
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Try
            For Each ListItem As ListItem In Me.cbPreferences.Items
                Dim UserPreference As Meanstream.Portal.Core.Membership.UserPreference = Profile.GetUserPreference(ListItem.Value)
                Profile.UpdateUserPreference(UserPreference.PreferenceId, ListItem.Selected)
            Next
            btnSave.SuccessMessage = "Dashboard saved successfully"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
