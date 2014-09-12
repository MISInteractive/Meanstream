Imports System.Data.SqlClient
Imports System.Data
Imports REVStack.Client.API
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class Meanstream_Host_UserControls_Repositories
    Inherits System.Web.UI.UserControl

    Dim items As JArray = Nothing
    Dim revstackRequest As REVStackRequest = Nothing

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim appId As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.AppId")
            Dim username As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Username")
            Dim password As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Password")
            Dim credentials As ICredentials = New Credentials(appId, username, password, Nothing)
            revstackRequest = REVStackClient.CreateRequest(credentials)
            credentials.AccessToken = revstackRequest.User.Login()
        End If

        BindGrid()
    End Sub

    Private Sub BindGrid()

        Dim ds As DataSet = GetRepositories()
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Grid.DataSource = ds
            Me.Grid.DataBind()
        Else
            Me.Container.Visible = False
        End If

    End Sub

    Private Function GetRepositories() As DataSet
        
        items = New JArray() 'revstackRequest.Datastore.Lookup("select from reviews", -1, -1)
        Dim j As JObject = New JObject()
        j.Add("data", items)
        Dim ds As DataSet = j.ToObject(Of DataSet)()
        'Dim oConn As SqlConnection
        'Dim sConn As String
        'sConn = System.Configuration.ConfigurationManager.ConnectionStrings("Meanstream").ToString
        'oConn = New SqlConnection(sConn)
        'Dim q As String = "SELECT DISTINCT Type FROM meanstream_dynamics_Item Order By Type"
        'Dim sDa As SqlDataAdapter
        'Dim ds As DataSet = New DataSet
        'sDa = New SqlDataAdapter(q, oConn)
        'sDa.Fill(ds, "meanstream_dynmaics_Item")
        Return ds
    End Function

    Public Function GetTotalObjects(ByVal type As String) As Integer
        Return items.Count
    End Function

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grid.RowDeleting

    End Sub

    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

        Select Case e.CommandName
            Case "Delete"
                Dim rowIndex As Integer = CInt(e.CommandArgument)
                Dim type As String = DirectCast(Grid.Rows(rowIndex).FindControl("type"), HiddenField).Value

                Try
                    revstackRequest.Datastore.Command("drop class " + type)

                    'Dim items As List(Of Dictionary(Of String, Object)) = Repository.GetKeyValues(type, "")
                    'For Each item As Dictionary(Of String, Object) In items
                    '    Repository.Delete(New Guid(item("id").ToString))
                    'Next

                    Me.Grid.EditIndex = -1
                    BindGrid()
                    Me.lblStatus.Text = "The repository has been deleted"
                Catch ex As Exception
                    Me.lblStatus.Text = ex.Message
                End Try
        End Select

    End Sub

    Protected Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Grid.PageIndex = e.NewPageIndex
        Grid.EditIndex = -1
        BindGrid()

    End Sub

    Protected Sub Grid_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim type As String = DataBinder.Eval(e.Row.DataItem, "@class")

            Dim Target As LinkButton = New LinkButton
            Target.ID = "Target"
            Target.Text = type

            e.Row.Cells(Grid.Columns.Count - 1).Controls.Item(0).FindControl("phRepository").Controls.Add(Target)

            Dim Window As Meanstream.Web.UI.Window = New Meanstream.Web.UI.Window
            Window.SkinID = "Window"
            Window.Width = "900"
            Window.Height = "500"
            Window.ShowLoader = "true"
            Window.ShowUrl = "true"
            Window.Title = "Manage Repository"
            Window.NavigateUrl = "Module.aspx?ctl=ManageRepository&type=" & type
            Window.OnClientClose = "windowClose()"
            Window.TargetControlId = Target.ClientID
            e.Row.Cells(Grid.Columns.Count - 1).Controls.Add(Window)
        End If

    End Sub

End Class
