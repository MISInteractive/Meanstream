
Partial Class Meanstream_Administration_UserControls_ExportEmails
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EmailGrid.DataSource = Membership.GetAllUsers
        EmailGrid.DataBind()

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Emails.xls")
        Response.Charset = ""

        Response.ContentType = "application/ms-excel"
        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        EmailGrid.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString)
        Response.End()
    End Sub
    Public Overloads Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
End Class
