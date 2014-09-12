
Partial Class Meanstream_Pages_UserControls_PageEditor
    Inherits System.Web.UI.UserControl

    Public ThemePath As String

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init

        If Request.Params("VersionID") Is Nothing Then
            Throw New Exception("Parameter VersionID is required.")
        End If

        ThemePath = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot & "App_Themes/" & Page.Theme
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            'Me.DisplayWorkflowSettings()

            Dim PageContent As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(New Guid(Request.Params("VersionID").ToString))
            Dim RelativeWebRoot As String = Meanstream.Portal.Core.Utilities.AppUtility.RelativeWebRoot

            Me.litFrame.Text = "<iframe id='editFrame' name='editFrame' scrolling='no' frameborder='0' src='/edit/page/" & Request.Params("VersionID").ToString & "'></iframe>"

            Me.btnPreview.NavigateUrl = "/preview/page/" & PageContent.Id.ToString
            Me.btnPreview.Target = "_blank"
            Me.btnSettings.NavigateUrl = "Module.aspx?ctl=PageSettings&Action=Edit&VersionID=" & PageContent.Id.ToString
            Me.btnSettings.TargetControlId = Me.SettingsTarget.ClientID

            Me.btnWorkflow.NavigateUrl = RelativeWebRoot & "Meanstream/Pages/Module.aspx?ctl=WorkflowVersion&VersionId=" & PageContent.Id.ToString
            Me.btnWorkflow.TargetControlId = Me.WorkflowTarget.ClientID
            Me.btnCloseAndSave.NavigateUrl = "../Default.aspx?ctl=ViewPage&PageID=" & PageContent.PageId.ToString

            Dim WidgetDefinitions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleDefinitions) = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetDefinitions()
            Me.Widgets.DataSource = WidgetDefinitions.ToDataSet(True)
            Me.Widgets.DataTextField = "FriendlyName"
            Me.Widgets.DataValueField = "Id"
            Me.Widgets.DataBind()

            Dim widgetItem As New Meanstream.Web.UI.ComboBoxItem
            widgetItem.Text = "select widget"
            widgetItem.Value = "0"
            Me.Widgets.Items.Insert(0, widgetItem)
            Me.Widgets.SelectedValue = "0"

            Me.SkinPanes.DataSource = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Find("SkinId=" & PageContent.Skin.Id.ToString).ToDataSet(True)
            Me.SkinPanes.DataTextField = "Pane"
            Me.SkinPanes.DataValueField = "Id"
            Me.SkinPanes.DataBind()

            Dim skinPaneItem As New Meanstream.Web.UI.ComboBoxItem
            skinPaneItem.Text = "select pane"
            skinPaneItem.Value = "0"
            Me.SkinPanes.Items.Insert(0, skinPaneItem)
            Me.SkinPanes.SelectedValue = "0"

        End If
    End Sub

    Protected Sub btnAddWidget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddWidget.Click
        If Me.SkinPanes.SelectedValue = "0" Then
            Me.lblMessage.Text = "select pane"
            Return
        End If
        If Me.Widgets.SelectedValue = "0" Then
            Me.lblMessage.Text = "select widget"
            Return
        End If
        Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.AddWidgetToPage(New Guid(Request.Params("VersionID").ToString), New Guid(Me.SkinPanes.SelectedValue), New Guid(Me.Widgets.SelectedValue))
    End Sub

    'Protected Sub DisplayWorkflowSettings()
    '    If SessionManager.GetWorkflowManager.Enabled() Then
    '        Me.pnlWorkflow.Visible = True
    '        Me.btnWorkflow.Visible = True
    '        Me.btnPublish.Visible = False

    '        Dim Status As String = SessionManager.GetWorkflowManager.GetPageVersionStatus(Request.Params("VersionID"))
    '        If Status = "Draft" Then
    '            Me.btnSendForReview.Visible = True
    '        ElseIf Status = "Sent for Review" Then
    '            Me.btnSendForReview.Visible = True
    '        ElseIf Status = "Review Approved" Then
    '            Me.btnSendForApproval.Visible = True
    '        ElseIf Status = "Sent for Approval" Then
    '            Me.btnSendForApproval.Visible = True
    '        ElseIf Status = "Approved" Then
    '            Me.btnPublish.Visible = True
    '        ElseIf Status = "Published" Then
    '        End If
    '    Else
    '        Me.pnlWorkflow.Visible = False
    '        Me.btnPublish.Visible = True
    '    End If
    'End Sub

    Protected Sub btnPublish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPublish.Click
        Dim PageContent As Meanstream.Portal.Core.Content.PageVersion = Meanstream.Portal.Core.Content.ContentService.Current.GetPageVersion(New Guid(Request.Params("VersionID").ToString))
        '**************************** PUBLISH ******************************
        Try
            Dim manager As New Meanstream.Portal.Core.Content.PageVersionManager(PageContent)
            manager.Publish()
            Response.Redirect("Default.aspx?ctl=ViewPage&PageID=" & PageContent.PageId.ToString)
        Catch ex As Exception
            Me.lblMessage.Text = ex.Message
        End Try
        '*******************************************************************
    End Sub

    'Protected Sub btnSendForReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendForReview.Click
    '    Me.lblMessage.Text = ""
    '    Try
    '        Dim PageWorkflow As Meanstream.Portal.Core.PageWorkflow = SessionManager.GetWorkflowManager.GetPageWorkflowById(SessionManager.GetWorkflowManager.GetPageWorkflowByVersionId(Request.Params("VersionID"))(0).Id)
    '        PageWorkflow.SendForReview()
    '    Catch ex As Exception
    '        Me.lblMessage.Text = ex.Message
    '        Exit Sub
    '    End Try
    '    Me.lblMessage.Text = "Your message was sent."
    'End Sub

    'Protected Sub btnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
    '    Me.lblMessage.Text = ""
    '    Try
    '        Dim PageWorkflow As Meanstream.Portal.Core.PageWorkflow = SessionManager.GetWorkflowManager.GetPageWorkflowById(SessionManager.GetWorkflowManager.GetPageWorkflowByVersionId(Request.Params("VersionID"))(0).Id)
    '        PageWorkflow.SendForReview()
    '        PageWorkflow.SendForApproval()
    '    Catch ex As Exception
    '        Me.lblMessage.Text = ex.Message
    '        Exit Sub
    '    End Try
    '    Me.lblMessage.Text = "Your message was sent."
    'End Sub
End Class
