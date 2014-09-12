Imports System.Data
'Imports Meanstream.Core.Repository
Imports REVStack.Client.API
Imports Newtonsoft.Json.Linq

Partial Class Meanstream_Host_UserControls_ExportRepository
    Inherits System.Web.UI.Page

    Dim revstackRequest As REVStackRequest = Nothing

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init, Me.Init

        If Not IsPostBack Then
            Dim appId As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.AppId")
            Dim username As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Username")
            Dim password As String = System.Configuration.ConfigurationManager.AppSettings("REVStack.Password")
            Dim credentials As ICredentials = New Credentials(appId, username, password, Nothing)
            revstackRequest = REVStackClient.CreateRequest(credentials)
            credentials.AccessToken = revstackRequest.User.Login()
        End If

        CreateTemplatedGridView()
    End Sub

    Private Sub CreateTemplatedGridView()
        Dim type As String = Request.Params("type")
        Dim whereClause As String = ""

        Dim items As JArray = revstackRequest.Datastore.Lookup("select from " & type, -1, -1)
        Dim j As JObject = New JObject()
        j.Add("data", items)
        Dim ds As DataSet = j.ToObject(Of DataSet)()

        'Dim ds As System.Data.DataSet = Repository.Find(type, whereClause)
        If ds.Tables.Count > 0 Then
            table = ds.Tables(0).Copy
        End If

        'clear grid columns
        ExportGrid.Columns.Clear()

        For i As Integer = 0 To table.Columns.Count - 1
            Dim ItemTmpField As New TemplateField()
            ' create HeaderTemplate 
            ItemTmpField.HeaderTemplate = New DynamicGridViewItemTemplate(ListItemType.Header, table.Columns(i).ColumnName, table.Columns(i).DataType.Name)
            ' create ItemTemplate 
            ItemTmpField.ItemTemplate = New DynamicGridViewItemTemplate(ListItemType.Item, table.Columns(i).ColumnName, table.Columns(i).DataType.Name)

            ExportGrid.Columns.Add(ItemTmpField)
        Next

        ' bind and display the data 
        ExportGrid.DataSource = table
        ExportGrid.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Repository_" & Request.Params("type") & ".xls")
        Response.Charset = ""

        Response.ContentType = "application/ms-excel"
        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

        ExportGrid.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString)
        Response.End()
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub


#Region "Data members"

    Public Shared table As New DataTable()

#End Region

End Class


'Public Class DynamicGridViewItemTemplate
'    Implements ITemplate

'#Region "data memebers"

'    Private ItemType As ListItemType
'    Private FieldName As String
'    Private InfoType As String

'#End Region

'#Region "constructor"

'    Public Sub New(ByVal item_type As ListItemType, ByVal field_name As String, ByVal info_type As String)
'        ItemType = item_type
'        FieldName = field_name
'        InfoType = info_type
'    End Sub

'#End Region

'#Region "Methods"

'    Public Sub InstantiateIn(ByVal Container As System.Web.UI.Control) Implements ITemplate.InstantiateIn

'        Select Case ItemType
'            Case ListItemType.Header
'                Dim header_ltrl As New Literal()
'                header_ltrl.Text = "<b>" & FieldName & "</b>"
'                Container.Controls.Add(header_ltrl)
'                Exit Select
'            Case ListItemType.Item
'                Select Case InfoType
'                    Case "Command"
'                        Dim edit_button As New LinkButton()
'                        edit_button.ID = "edit_button"
'                        edit_button.Text = "Edit"
'                        edit_button.CommandName = "Edit"
'                        edit_button.Style("padding-right") = "10px"
'                        AddHandler edit_button.Click, AddressOf edit_button_Click
'                        edit_button.ToolTip = "Edit"
'                        Container.Controls.Add(edit_button)

'                        Dim delete_button As New LinkButton()
'                        delete_button.ID = "delete_button"
'                        delete_button.Text = "Delete"
'                        delete_button.CommandName = "Delete"
'                        delete_button.ToolTip = "Delete"
'                        delete_button.Style("padding-right") = "10px"
'                        delete_button.OnClientClick = "return confirm('Are you sure to delete the record?')"
'                        Container.Controls.Add(delete_button)

'                        ' Similarly add button for insert. 
'                        ' * It is important to know when 'insert' button is added 
'                        ' * its CommandName is set to "Edit" like that of 'edit' button 
'                        ' * only because we want the GridView enter into Edit mode, 
'                        ' * and this time we also want the text boxes for corresponding fields empty 

'                        Dim insert_button As New LinkButton()
'                        insert_button.ID = "insert_button"
'                        insert_button.Text = "Insert"
'                        insert_button.CommandName = "Edit"
'                        insert_button.ToolTip = "Insert"
'                        AddHandler insert_button.Click, AddressOf insert_button_Click
'                        Container.Controls.Add(insert_button)

'                        Exit Select
'                    Case Else

'                        Dim field_lbl As New Label()
'                        field_lbl.ID = FieldName
'                        field_lbl.Text = [String].Empty
'                        'we will bind it later through 'OnDataBinding' event 
'                        AddHandler field_lbl.DataBinding, AddressOf OnDataBinding
'                        Container.Controls.Add(field_lbl)
'                        Exit Select

'                End Select
'                Exit Select
'            Case ListItemType.EditItem
'                If InfoType = "Command" Then
'                    Dim update_button As New LinkButton()
'                    update_button.ID = "update_button"
'                    update_button.CommandName = "Update"
'                    update_button.Text = "Update"
'                    update_button.Style("padding-right") = "10px"
'                    If CInt(New Page().Session("InsertFlag")) = 1 Then
'                        update_button.ToolTip = "Add"
'                        update_button.OnClientClick = "return confirm('Are you sure to add the record?')"
'                    Else
'                        update_button.ToolTip = "Update"
'                        update_button.OnClientClick = "return confirm('Are you sure to update the record?')"
'                    End If
'                    Container.Controls.Add(update_button)

'                    Dim cancel_button As New LinkButton()
'                    cancel_button.Text = "Cancel"
'                    cancel_button.ID = "cancel_button"
'                    cancel_button.CommandName = "Cancel"
'                    cancel_button.ToolTip = "Cancel"

'                    Container.Controls.Add(cancel_button)
'                Else
'                    ' for other 'non-command' i.e. the key and non key fields, bind textboxes with corresponding field values 
'                    Dim field_txtbox As New TextBox()
'                    field_txtbox.ID = FieldName
'                    field_txtbox.Text = [String].Empty
'                    If FieldName = "id" Then
'                        field_txtbox.Enabled = False
'                    End If
'                    ' if Insert is intended no need to bind it with text..keep them empty 
'                    If CInt(New Page().Session("InsertFlag")) = 0 Then
'                        AddHandler field_txtbox.DataBinding, AddressOf OnDataBinding
'                    End If

'                    Container.Controls.Add(field_txtbox)
'                End If
'                Exit Select


'        End Select
'    End Sub

'#End Region

'#Region "Event Handlers"

'    'just sets the insert flag ON so that we ll be able to decide in OnRowUpdating event whether to insert or update 
'    Protected Sub insert_button_Click(ByVal sender As [Object], ByVal e As EventArgs)
'        HttpContext.Current.Session("InsertFlag") = 1
'    End Sub
'    'just sets the insert flag OFF so that we ll be able to decide in OnRowUpdating event whether to insert or update 
'    Protected Sub edit_button_Click(ByVal sender As [Object], ByVal e As EventArgs)
'        HttpContext.Current.Session("InsertFlag") = 0
'    End Sub

'    Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)

'        Dim bound_value_obj As Object = Nothing
'        Dim ctrl As Control = DirectCast(sender, Control)
'        Dim data_item_container As IDataItemContainer = DirectCast(ctrl.NamingContainer, IDataItemContainer)
'        bound_value_obj = DataBinder.Eval(data_item_container.DataItem, FieldName)

'        Select Case ItemType
'            Case ListItemType.Item
'                Dim field_ltrl As Label = DirectCast(sender, Label)
'                field_ltrl.Text = bound_value_obj.ToString()

'                Exit Select
'            Case ListItemType.EditItem
'                Dim field_txtbox As TextBox = DirectCast(sender, TextBox)
'                field_txtbox.Text = bound_value_obj.ToString()
'                field_txtbox.TextMode = TextBoxMode.MultiLine
'                field_txtbox.Style.Add("width", "100%")
'                If FieldName = "id" Then
'                    field_txtbox.Enabled = False
'                End If

'                Exit Select

'        End Select
'    End Sub

'#End Region


'End Class

