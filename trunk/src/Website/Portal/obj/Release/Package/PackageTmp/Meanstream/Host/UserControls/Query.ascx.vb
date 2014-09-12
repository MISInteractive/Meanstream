Imports REVStack.Client.API
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class Meanstream_Host_UserControls_Query
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        If Not String.IsNullOrEmpty(Me.txtScript.Text) Then
            Try
                Dim appId As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.AppId")
                Dim username As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Username")
                Dim password As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Password")
                Dim credentials As ICredentials = New Credentials(appId, username, password, Nothing)
                Dim revstackRequest As REVStackRequest = REVStackClient.CreateRequest(credentials)
                credentials.AccessToken = revstackRequest.User.Login()

                'Dim ds As System.Data.DataSet = Meanstream.Portal.Core.Data.DataRepository.Provider.ExecuteDataSet(System.Data.CommandType.Text, Me.txtScript.Text)
                Dim results As JArray = revstackRequest.Datastore.Lookup(Me.txtScript.Text, -1, -1)
                Dim j As JObject = New JObject()
                j.Add("data", results)
                Dim ds As System.Data.DataSet = j.ToObject(Of System.Data.DataSet)()

                Me.grid.Columns.Clear()

                If ds IsNot Nothing Then
                    For Each column As System.Data.DataColumn In ds.Tables(0).Columns
                        Dim field As New WebControls.BoundField
                        field.DataField = column.ColumnName
                        field.HeaderText = column.ColumnName
                        grid.Columns.Add(field)
                    Next
                    Me.grid.DataSource = ds
                    Me.grid.DataBind()
                End If
            Catch ex As Exception
                Me.btnSave.ThrowFailure = True
                Me.btnSave.FailMessage = ex.Message
            End Try
        End If
    End Sub
End Class
