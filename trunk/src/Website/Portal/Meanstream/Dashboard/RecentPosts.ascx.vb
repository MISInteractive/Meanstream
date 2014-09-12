Imports System.Data.SqlClient
Imports System.Data

Partial Class Meanstream_Dashboard_RecentPosts
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Dim posts As DataSet = GetRecentPosts()
        If posts.Tables(0).Rows.Count Then
            Me.Grid.DataSource = posts
            Me.Grid.DataBind()
        Else
            Me.Container.Visible = False
        End If
    End Sub

    Public Function GetRecentPosts() As DataSet
        Dim oConn As SqlConnection
        Dim sConn As String
        Dim q As String

        sConn = System.Configuration.ConfigurationManager.ConnectionStrings("Meanstream").ToString
        oConn = New SqlConnection(sConn)

        q = "SELECT TOP 5 id,post_title,guid,post_modified_gmt,post_status FROM wp_posts WHERE post_type='post' ORDER BY post_modified_gmt DESC"

        Dim sDa As SqlDataAdapter
        Dim ds As DataSet = New DataSet
        sDa = New SqlDataAdapter(q, oConn)
        sDa.Fill(ds, "wp_posts")

        Return ds
    End Function
End Class
