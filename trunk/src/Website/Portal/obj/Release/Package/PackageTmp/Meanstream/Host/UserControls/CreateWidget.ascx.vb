
Partial Class Meanstream_Host_UserControls_CreateWidget
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click

        If Me.txtName.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Name required"
            Return
        End If

        If Me.txtWidgetBasePath.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "WidgetBase Virtual Path required"
            Return
        End If

        If Me.txtWidgetEditBasePath.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "WidgetEditBase Virtual Path required"
            Return
        End If

        Try
            Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.Create(Me.txtName.Text, Me.txtDescription.Text, Me.txtWidgetBasePath.Text, Me.txtWidgetEditBasePath.Text, False, Me.cbEnabled.Checked)
            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try

    End Sub

End Class
