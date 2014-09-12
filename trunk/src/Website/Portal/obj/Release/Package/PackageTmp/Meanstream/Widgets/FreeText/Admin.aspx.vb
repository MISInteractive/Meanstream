
Partial Class Meanstream_Widgets_FreeText_Admin
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_PreInit"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreInit
        Page.Theme = Meanstream.Portal.Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(Profile.UserName)
    End Sub

    Private Sub this_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        'Editor.Setting("security:ImageGalleryPath") = "~/Portals/" & Meanstream.Portal.Core.PortalContext.Current.Portal.PortalRoot & "/Images"
        'Editor.Setting("security:ImageBrowserPath") = "~/Portals/" & Meanstream.Portal.Core.PortalContext.Current.Portal.PortalRoot & "/Images"
        'Editor.Setting("security:MediaGalleryPath") = "~/Portals/" & Meanstream.Portal.Core.PortalContext.Current.Portal.PortalRoot & "/Video"
        'Editor.Setting("security:FlashGalleryPath") = "~/Portals/" & Meanstream.Portal.Core.PortalContext.Current.Portal.PortalRoot & "/Flash"
        'Editor.Setting("security:TemplateGalleryPath") = "~/Portals/" & Meanstream.Portal.Core.PortalContext.Current.Portal.PortalRoot & "/Templates"
        'Editor.Setting("security:FilesGalleryPath") = "~/Portals/" & Meanstream.Portal.Core.PortalContext.Current.Portal.PortalRoot & "/Documents"
        Editor.Setting("security:ImageGalleryPath") = "~/Portals/0/images"
        Editor.Setting("security:ImageBrowserPath") = "~/Portals/0/images"
        Editor.Setting("security:MediaGalleryPath") = "~/Portals/0/media"
        Editor.Setting("security:FlashGalleryPath") = "~/Portals/0/flash"
        Editor.Setting("security:TemplateGalleryPath") = "~/Portals/0/templates"
        Editor.Setting("security:FilesGalleryPath") = "~/Portals/0/documents"
        Editor.SetSecurityMaxDocumentFolderSize(50000)
        Editor.SetSecurityMaxDocumentSize(50000)
        Editor.SetSecurityMaxImageDimension(5000, 5000)
        Editor.SetSecurityMaxImageFolderSize(500000)
        Editor.SetSecurityMaxMediaFolderSize(500000)
        Editor.SetSecurityMaxMediaSize(50000)
    End Sub


    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"

            If Request.Params("WidgetId") <> Nothing Then
                Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.GetByModuleVersionId(New Guid(Request.Params("WidgetId")))

                If Request.Params("SharedId") <> Nothing Then
                    Me.Editor.Text = Meanstream.Portal.Core.Content.Content.GetContent(Request.Params("SharedId")).Text
                Else
                    Me.Editor.Text = FreeText.Content.Text
                End If

                Me.FreeTextId.Value = FreeText.Id.ToString
                Me.txtTitle.Text = FreeText.Content.Title

                Me.lblMessage.Text = "This Content is currently checked out by you."
                If FreeText.CheckedOutBy IsNot Nothing Then
                    Dim Username As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUsername(FreeText.CheckedOutBy)
                    If Username IsNot Nothing And Username <> Profile.UserName Then
                        Me.btnSave.Visible = False
                        Me.lblMessage.Text = "This Content is currently checked out by " & Username
                        If Roles.IsUserInRole(Profile.UserName, Meanstream.Portal.Core.AppConstants.ADMINISTRATOR) _
                        Or Roles.IsUserInRole(Profile.UserName, Meanstream.Portal.Core.AppConstants.HOST) _
                        Or Roles.IsUserInRole(Profile.UserName, Meanstream.Portal.Core.AppConstants.CONTENT_ADMINISTRATOR) Then
                            Me.Checkout.Visible = True
                        End If
                    Else
                        FreeText.CheckedOutBy = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUserGuid(Profile.UserName)
                        FreeText.Save()
                    End If
                Else
                    FreeText.CheckedOutBy = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUserGuid(Profile.UserName)
                    FreeText.Save()
                End If

                Me.Shared.Checked = FreeText.Content.IsShared

            End If
            Me.SelectSharedContent.TargetControlId = Me.SelectSharedContentTarget.ClientID
        End If
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Click"/> event. Saves the freetext content and settings to the data store.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtTitle.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Title required"
            Return
        End If

        Try
            Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.GetById(New Guid(Me.FreeTextId.Value))
            FreeText.Content.Text = Me.Editor.Text
            FreeText.Content.IsShared = Me.Shared.Checked
            FreeText.Content.Title = Me.txtTitle.Text
            FreeText.CheckedOutBy = Nothing
            FreeText.IsLocked = False
            FreeText.Save()

            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Raises the <see cref="System.Web.UI.WebControls.LinkButton.Click"/> event. Checks out the content by user and saves the settings to the data store.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub Checkout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Checkout.Click
        Me.btnSave.Visible = True
        Me.Checkout.Visible = False
        Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.GetByModuleVersionId(New Guid(Request.Params("WidgetId").ToString))
        FreeText.CheckedOutBy = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUserGuid(Profile.UserName)
        FreeText.Save()
        Me.lblMessage.Text = "This Content is currently checked out by you."
    End Sub
End Class
