
Partial Class Meanstream_Widgets_UserControl_Admin
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_PreInit"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreInit
        Page.Theme = Meanstream.Portal.Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(Profile.UserName)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim Custom As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.GetByModuleVersionId(New Guid(Request.Params("WidgetId")))
            Me.txtName.Text = Custom.Name
            Me.txtPath.Text = Custom.VirtualPath
            Me.hId.Value = Custom.Id.ToString
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
        End If
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Click"/> event. Saves the custom usercontrol settings to the data store.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtName.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Name required"
            Return
        End If

        If Me.txtPath.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Path required"
            Return
        End If

        If Not Me.txtPath.Text.Trim.EndsWith(".ascx") Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "UserControl file (.ascx) required"
            Return
        End If

        If Not System.IO.File.Exists(Request.MapPath(Me.txtPath.Text.Trim)) Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "File not found"
            Return
        End If

        Try
            Dim Custom As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.GetById(New Guid(Me.hId.Value))
            Custom.Name = Me.txtName.Text
            Custom.VirtualPath = Me.txtPath.Text
            Custom.Save()
            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub
End Class
