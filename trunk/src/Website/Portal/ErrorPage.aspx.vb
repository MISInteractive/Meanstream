
Partial Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Exception As Exception = Server.GetLastError.GetBaseException
        Me.litType.Text = Exception.GetType.ToString
        Me.litMessage.Text = Exception.Message
        Me.litSource.Text = Exception.Source
        Me.litToString.Text = Exception.ToString
    End Sub
End Class
