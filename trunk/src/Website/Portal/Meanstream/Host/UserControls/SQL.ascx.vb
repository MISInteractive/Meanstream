
Partial Class Meanstream_Host_UserControls_SQL
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not IsPostBack Then
            Me.btnSave.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-save.png"
            Me.ImgUpload.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-upload.png"
        End If
    End Sub

    Protected Sub ImgUpload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUpload.Click
        If Not String.IsNullOrEmpty(FileUpload.FileName) Then
            Dim stream As New System.IO.StreamReader(FileUpload.PostedFile.InputStream)
            Me.txtScript.Text = stream.ReadToEnd
            stream.Close()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        If Not String.IsNullOrEmpty(Me.txtScript.Text) Then
            Try
                Dim ds As System.Data.DataSet = Meanstream.Portal.Core.Data.DataRepository.Provider.ExecuteDataSet(System.Data.CommandType.Text, Me.txtScript.Text)
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
