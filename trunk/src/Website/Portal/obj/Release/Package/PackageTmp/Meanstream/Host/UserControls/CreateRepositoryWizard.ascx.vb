Imports REVStack.Client.API
Imports Newtonsoft.Json.Linq
Imports LumenWorks.Framework.IO.Csv
Imports System.IO

Partial Class Meanstream_Host_UserControls_CreateRepositoryWizard
    Inherits System.Web.UI.UserControl

    Dim revstackRequest As REVStackRequest = Nothing

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            'create blank field
            Dim fieldList As New Dictionary(Of String, Object)
            fieldList.Add("", "")
            rFields.DataSource = fieldList
            rFields.DataBind()

            Me.pFields.Visible = True

            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
            Me.btnUpload.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If

        Dim appId As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.AppId")
        Dim username As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Username")
        Dim password As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Password")
        Dim credentials As ICredentials = New Credentials(appId, username, password, Nothing)
        revstackRequest = REVStackClient.CreateRequest(credentials)
        credentials.AccessToken = revstackRequest.User.Login()
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpload.Click
        Me.lblMessage.Text = ""

        If Me.txtName.Text.Trim = "" Then
            Me.lblMessage.Text = "Name required"
            Return
        End If

        If Not FileUpload.FileName.EndsWith(".csv") Then
            Me.lblMessage.Text = ".csv required"
            Return
        End If

        'check to see if class exists here!!!!!!!!!!!
        'If Repository.GetKeyValues(Me.txtName.Text.Trim, "").Count > 0 Then
        '    Me.btnSave.ThrowFailure = True
        '    Me.btnSave.FailMessage = "Data store '" & Me.txtName.Text & "' already exists."
        '    Return
        'End If

        Me.btnUpload.Enabled = False

        Me.ReadCsv(FileUpload.PostedFile.InputStream)

        Response.Redirect("Default.aspx?ctl=Repositories")
    End Sub


    Private Sub ReadCsv(ByVal filetoRead As Stream)
        'data rows
        Dim rows As New JArray()
        ' open the file that is a CSV file with headers
        Using csv As New CsvReader(New StreamReader(filetoRead), True)
            ' missing fields will not throw an exception,
            ' but will instead be treated as if there was a null value
            csv.MissingFieldAction = MissingFieldAction.ReplaceByNull
            ' to replace by "" instead, then use the following action:
            'csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
            Dim fieldCount As Integer = csv.FieldCount
            Dim headers As String() = csv.GetFieldHeaders()
            While csv.ReadNextRecord()
                Dim row As New JObject()
                row("@class") = Me.txtName.Text.Trim
                For i As Integer = 0 To fieldCount - 1
                    If headers(i).Trim <> "" Then
                        Dim header As String = headers(i).Replace(" ", "_").Replace("/", "_").Replace("\", "_")
                        row.Add(header, If(csv(i) Is Nothing, "MISSING", csv(i)))
                    End If
                Next
                rows.Add(row)
            End While
        End Using

        
        'create datastore items

        For Each item As JObject In rows
            revstackRequest.Datastore.Create(item)
        Next
    End Sub

    
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtName.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Name required"
            Return
        End If

        'If Repository.GetKeyValues(Me.txtName.Text.Trim, "").Count > 0 Then
        '    Me.btnSave.ThrowFailure = True
        '    Me.btnSave.FailMessage = "Data store '" & Me.txtName.Text & "' already exists."
        '    Return
        'End If

        'create new data table
        Dim data As New JObject()
        data("@class") = Me.txtName.Text.Trim

        'add fields
        Dim index As Integer = 0
        Dim fieldList As Dictionary(Of String, Object) = Me.GetCurrentDisplay
        For Each key As String In fieldList.Keys
            Dim value As String = fieldList(key)
            key = key.Replace(" ", "_")
            data(key) = ""
            index += 1
        Next

        Try
            
            revstackRequest.Datastore.Create(data)
            Me.btnSave.SuccessMessage = "Save successful"
            'redirect in 2 seconds
            Threading.Thread.Sleep(3000)
            Response.Redirect("Default.aspx?ctl=Repositories")
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As Web.UI.WebControls.RepeaterCommandEventArgs) Handles rFields.ItemCommand
        Dim fieldList As Dictionary(Of String, Object) = Me.GetCurrentDisplay

        If e.CommandName = "Remove" Then
            fieldList.Remove(e.CommandArgument)
        End If

        rFields.DataSource = fieldList
        rFields.DataBind()
    End Sub

    Protected Function GetCurrentDisplay() As Dictionary(Of String, Object)
        Dim fieldList As New Dictionary(Of String, Object)
        Dim Count As Integer = Me.rFields.Items.Count
        Dim Collection As RepeaterItemCollection = Me.rFields.Items
        Dim Enumerator As IEnumerator = Collection.GetEnumerator

        While Count > 0
            Enumerator.MoveNext()
            Dim RepeaterItem As RepeaterItem = Enumerator.Current()
            Dim fieldName As String = DirectCast(RepeaterItem.Controls.Item(1), TextBox).Text.Trim
            'Dim fieldValue As String = DirectCast(RepeaterItem.Controls.Item(5), DropDownList).SelectedValue
            fieldList.Add(fieldName, fieldName)
            Count -= 1
        End While

        Return fieldList
    End Function

    Protected Sub btnAddField_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddField.Click
        Dim fieldList As Dictionary(Of String, Object) = Me.GetCurrentDisplay
        fieldList.Add("", "")
        rFields.DataSource = fieldList
        rFields.DataBind()
    End Sub

    Protected Sub rListChoose_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rListChoose.SelectedIndexChanged
        If Me.rListChoose.SelectedValue = "Manual" Then
            Me.pFields.Visible = True
            Me.pUpload.Visible = False
        Else
            Me.pFields.Visible = False
            Me.pUpload.Visible = True
            Me.btnUpload.Visible = True
        End If
    End Sub
End Class
