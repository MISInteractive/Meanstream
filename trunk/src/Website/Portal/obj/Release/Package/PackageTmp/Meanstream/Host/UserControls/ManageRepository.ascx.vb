Imports Meanstream.Core.Repository

Partial Class Meanstream_Host_UserControls_ManageRepository
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim type As String = Request.Params("type")
            Me.hType.Value = type
            Me.Bind(type)
            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
        End If
    End Sub

    Private Sub Bind(ByVal type As String)
        Dim data As List(Of Dictionary(Of String, Object)) = Repository.GetKeyValues(type, "")

        If data.Count = 0 Then
            Return
        End If

        Me.txtName.Text = type
        Me.lblTitle.Text = type

        Dim fields As New Dictionary(Of String, Object)
        For Each key As String In data(0).Keys
            If key.ToLower <> "type" And key.ToLower <> "id" Then
                fields(key) = key
            End If
        Next

        rFields.DataSource = fields
        rFields.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim type As String = Me.hType.Value
        Dim newType As String = Me.txtName.Text.Trim

        Try
            Dim data As List(Of Dictionary(Of String, Object)) = Repository.GetKeyValues(type, "")

            If data.Count = 0 Then
                Return
            End If

            If newType <> type Then
                'update type name if it has changed
                Dim otherType As List(Of Dictionary(Of String, Object)) = Repository.GetKeyValues(newType, "")
                'return if name exists
                If otherType.Count > 0 Then
                    Me.btnSave.ThrowFailure = True
                    Me.btnSave.FailMessage = "Type already exists. Enter a unique type name."
                    Return
                End If
                'rename
                Repository.Rename(type, newType)
                'set new type name for processes below
                type = newType
            End If

            Dim dataRow As Dictionary(Of String, Object) = data(0)
            Dim fieldList As Dictionary(Of String, Object) = Me.GetCurrentDisplay

            'update fields
            For Each key As String In fieldList.Keys
                If dataRow.ContainsKey(key) Then
                    If key <> fieldList(key) Then
                        'update field name
                        Repository.RenameField(type, key, fieldList(key))
                    End If
                Else
                    Repository.AddField(type, fieldList(key), "")
                End If
            Next

            'delete fields
            For Each key As String In dataRow.Keys
                If Not fieldList.ContainsKey(key) Then
                    Repository.DeleteField(type, key)
                End If
            Next

            Me.btnSave.SuccessMessage = "Save successful"
        Catch ex As Exception
            Meanstream.Portal.Core.Instrumentation.PortalTrace.Fail(ex.Message, Meanstream.Portal.Core.Instrumentation.DisplayMethodInfo.DoNotDisplay)
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = ex.Message
        End Try
    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As Web.UI.WebControls.RepeaterCommandEventArgs) Handles rFields.ItemCommand
        Dim fieldList As Dictionary(Of String, Object) = Me.GetCurrentDisplay
        Dim originalKey As String = DirectCast(e.Item.FindControl("OriginalKey"), HiddenField).Value.Trim
        Dim fieldName As String = DirectCast(e.Item.FindControl("FieldName"), TextBox).Text.Trim
        Dim message As Label = e.Item.FindControl("lblMessage")
        Dim type As String = Request.Params("type")

        If e.CommandName = "Remove" Then
            'remove from list
            fieldList.Remove(originalKey)
            rFields.DataSource = fieldList
            rFields.DataBind()
        End If
    End Sub

    Protected Sub ItemDataBound(ByVal source As Object, ByVal e As Web.UI.WebControls.RepeaterItemEventArgs) Handles rFields.ItemDataBound
        Dim originalKey As HiddenField = e.Item.FindControl("OriginalKey")
    End Sub

    Protected Function GetCurrentDisplay() As Dictionary(Of String, Object)
        Dim fieldList As New Dictionary(Of String, Object)
        Dim Count As Integer = Me.rFields.Items.Count
        Dim Collection As RepeaterItemCollection = Me.rFields.Items
        Dim Enumerator As IEnumerator = Collection.GetEnumerator

        While Count > 0
            Enumerator.MoveNext()
            Dim RepeaterItem As RepeaterItem = Enumerator.Current()
            Dim fieldKey As String = DirectCast(RepeaterItem.Controls.Item(1), HiddenField).Value.Trim
            Dim fieldName As String = DirectCast(RepeaterItem.Controls.Item(3), TextBox).Text
            fieldList.Add(fieldKey, fieldName)
            'fieldList(fieldKey) = fieldName
            Count -= 1
        End While

        Return fieldList
    End Function

    Protected Sub btnAddField_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddField.Click
        Dim fieldList As Dictionary(Of String, Object) = Me.GetCurrentDisplay

        If Not fieldList.ContainsKey("") Then
            'create new row
            fieldList.Add("", "")
        ElseIf fieldList.ContainsKey("") Then
            Dim value As String = fieldList("")
            If value.Trim <> "" Then
                fieldList.Remove("")
                'add unique key
                fieldList.Add(value, value)
            End If
        End If

        rFields.DataSource = fieldList
        rFields.DataBind()
    End Sub

End Class
