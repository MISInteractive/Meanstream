
Partial Class Meanstream_Host_UserControls_Scheduler
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        If Not IsPostBack Then
            If Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Enabled Then
                If Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Started Then
                    Me.lblSchedulerStatus.Text = "Started"
                    Me.btnToggle.Text = "Stop Service"
                Else
                    Me.lblSchedulerStatus.Text = "Stopped"
                    Me.btnToggle.Text = "Start Service"
                End If
            Else
                Me.lblSchedulerStatus.Text = "Disabled"
                Me.btnToggle.Visible = False
            End If
            Me.txtStartDateTime.Text = Date.Now.AddHours(1)
            Me.BindGrid()
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If

    End Sub

    Private Sub BindGrid()

        Me.Container.Visible = True

        Me.Grid.DataSource = Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.GetScheduledTasks
        Me.Grid.DataBind()

        If Me.Grid.Rows.Count = 0 Then
            Me.Container.Visible = False
        End If

    End Sub

    Public Function LastRunResult(ByVal result As String) As String

        If result = "1" Then
            Return "successful"
        End If
        If result = "0" Then
            Return "fail"
        End If

        Return result

    End Function

    Public Function NextRun(ByVal status As String, ByVal nextRunTime As String) As String

        If status = "2" Then
            Return ""
        End If

        If Not Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Enabled Or _
            Not Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Started Then
            Return ""
        End If

        Return nextRunTime

    End Function

    Public Function LastRun(ByVal lastRunTime As String) As String

        If lastRunTime = "12:00:00 AM" Then
            Return ""
        End If

        Return lastRunTime

    End Function

    Public Function FormatInterval(ByVal startupType As String, ByVal interval As String) As String

        If startupType = "1" Then

            Return ""

        End If

        Return "Every " & TimeSpan.FromMilliseconds(interval).Minutes & " minutes"

    End Function

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grid.RowDeleting

    End Sub

    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim rowIndex As Integer = CInt(e.CommandArgument)
        Dim Id As String = DirectCast(Grid.Rows(rowIndex).FindControl("lblID"), Label).Text

        Select Case e.CommandName
            Case "Stop"
                Try
                    Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.StopTask(New Guid(Id))
                    Me.lblStatus.Text = "task stopped successfully"
                    Me.BindGrid()
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
            Case "Start"
                Try
                    Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.StartTask(New Guid(Id))
                    Me.lblStatus.Text = "task started successfully"
                    Me.BindGrid()
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
            Case "Run"
                Try
                    Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.RunTaskNow(New Guid(Id))
                    Me.lblStatus.Text = "task ran successfully"
                    Me.BindGrid()
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
            Case "Delete"
                Try
                    Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.RemoveFromSchedule(New Guid(Id))
                    Me.lblStatus.Text = "task deleted successfully"
                    Me.BindGrid()
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
        End Select
    End Sub

    Protected Sub ChangeStartupType(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim comboBox As WebControls.DropDownList = sender
        'Dim schedule As List(Of Object) = Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.GetScheduledTasks
        'Meanstream.Portal.Core.Instrumentation.PortalTrace.Fail(comboBox.SelectedValue, Meanstream.Portal.Core.Instrumentation.DisplayMethodInfo.DoNotDisplay)
        Me.BindGrid()
    End Sub

    Protected Sub Grid_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Id As String = DataBinder.Eval(e.Row.DataItem, "Id").ToString
            Dim Status As String = DataBinder.Eval(e.Row.DataItem, "Status").ToString
            Dim StartupType As String = DataBinder.Eval(e.Row.DataItem, "StartupType").ToString

            Dim ComboBox As New WebControls.DropDownList
            ComboBox.ID = Id
            ComboBox.AutoPostBack = True
            ComboBox.Width = "100"
            ComboBox.EnableViewState = True
            'ComboBox.ComboPanelHeight = "140"
            'ComboBox.ImageButtonWidth = "25"
            AddHandler ComboBox.SelectedIndexChanged, New EventHandler(AddressOf ChangeStartupType)

            Dim item As New WebControls.ListItem
            item.Text = "Automatic"
            item.Value = "0"
            ComboBox.Items.Add(item)
            item = New WebControls.ListItem
            item.Text = "RunOnce"
            item.Value = "1"
            ComboBox.Items.Add(item)
            item = New WebControls.ListItem
            item.Text = "Disabled"
            item.Value = "2"
            ComboBox.Items.Add(item)
            e.Row.Cells(2).Controls.Item(0).FindControl("phStartupType").Controls.Add(ComboBox)
            For Each i As ListItem In ComboBox.Items
                If i.Text = StartupType Then
                    i.Selected = True
                End If
            Next

            Dim cmdRun As Button = e.Row.Cells(1).Controls(0)
            If Status = "Running" Or StartupType = "Disabled" Then
                cmdRun.Enabled = False
            End If
            Dim cmdButton As Button = e.Row.Cells(2).Controls(0)
            If StartupType <> "Disabled" And StartupType <> "RunOnce" Then
                cmdButton.Text = "Start"
                cmdButton.CommandName = "Start"
                cmdButton.OnClientClick = "javascript:if (!confirm('Start and schedule task?')) return;"
                cmdRun.Enabled = True
            End If
            If Status = "Started" Or Status = "Running" And StartupType <> "Disabled" Then
                cmdButton.Text = "Stop"
                cmdButton.CommandName = "Stop"
                cmdButton.OnClientClick = "javascript:if (!confirm('Stop the scheduled task?')) return;"
            End If
            If StartupType = "RunOnce" Then
                cmdButton.Enabled = False
            End If
            If Not Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Enabled Or _
                Not Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Started Or _
                StartupType = "Disabled" Then
                cmdButton.Enabled = False
                cmdRun.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        If Me.txtTaskClassName.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Class name required"
            Return
        End If

        If Me.txtInterval.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Interval required"
            Return
        End If

        If Me.StartupType.SelectedValue = Nothing Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Startup type required"
            Return
        End If

        Dim d As Double
        Dim interval As Double
        If Double.TryParse(txtInterval.Text, d) Then
            'now convert minutes to milliseconds
            interval = (TimeSpan.FromMinutes(d).TotalMilliseconds.ToString("N0"))
        Else
            'the text entered by te user wasn't numeric
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Interval must be a numeric value"
            Return
        End If

        Try
            Dim type As Type = Meanstream.Portal.Core.Utilities.AppUtility.GetGlobalType(Me.txtTaskClassName.Text.Trim)
            Dim startupType As Meanstream.Portal.Core.Services.Scheduling.StartupType = [Enum].Parse(GetType(Meanstream.Portal.Core.Services.Scheduling.StartupType), Me.StartupType.SelectedValue)
            Dim args() As Object = {Guid.NewGuid, interval, startupType}
            Dim task As Meanstream.Portal.Core.Services.Scheduling.Task = System.Activator.CreateInstance(type, args)
            task.NextRunTime = Date.Parse(Me.txtStartDateTime.Text)
            Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.AddToSchedule(task)

            Me.btnSave.SuccessMessage = "Save successful"
            Me.BindGrid()
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        If Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Started Then
            Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.StopService()
            Me.lblSchedulerStatus.Text = "Stopped"
            Me.btnToggle.Text = "Start Service"
        Else
            Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.StartService()
            Me.lblSchedulerStatus.Text = "Started"
            Me.btnToggle.Text = "Stop Service"
        End If
        Me.BindGrid()
    End Sub
End Class
