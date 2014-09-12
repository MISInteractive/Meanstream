
Partial Class Meanstream_Administration_UserControls_Profile
    Inherits System.Web.UI.UserControl

    Dim ProfileCommon As ProfileCommon = Nothing

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Username As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(New Guid(Request.Params("uid")))
        ProfileCommon = Profile.GetProfile(Username)
        If Not Page.IsPostBack Then
            Me.lblUsername.Text = Username
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            Me.BindData()
        End If
    End Sub

    Public Sub BindData()
        Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim ProfileSection As System.Web.Configuration.ProfileSection = DirectCast(Configuration.GetSection("system.web/profile"), System.Web.Configuration.ProfileSection)
        Dim PropertySettings As System.Web.Configuration.RootProfilePropertySettingsCollection = ProfileSection.PropertySettings
        Me.Properties.DataSource = PropertySettings
        Me.Properties.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim result As String = ""

        For Each Item As DataListItem In Me.Properties.Items
            If ((Item.ItemType = ListItemType.Item) OrElse (Item.ItemType = ListItemType.AlternatingItem)) Then
                Dim PropertyName As String = CType(Item.FindControl("lblProperty"), Label).Text
                Dim PropertyValue As String = CType(Item.FindControl("txtProperty"), TextBox).Text
                ProfileCommon.SetPropertyValue(PropertyName, PropertyValue)
            End If
        Next
        Try
            ProfileCommon.Save()
            Me.btnSave.SuccessMessage = "Save sucessful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub Properties_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Properties.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim PropertyName As String = DataBinder.Eval(e.Item.DataItem, "Name").ToString
            CType(e.Item.FindControl("lblProperty"), Label).Text = PropertyName
            CType(e.Item.FindControl("txtProperty"), TextBox).Text = ProfileCommon.GetPropertyValue(PropertyName)
        End If
    End Sub
End Class
