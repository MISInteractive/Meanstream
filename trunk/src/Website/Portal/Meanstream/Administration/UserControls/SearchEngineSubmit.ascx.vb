
Partial Class Meanstream_Administration_UserControls_SearchEngineSubmit
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim sitemapHandler As String = Meanstream.Portal.Core.Utilities.AppUtility.GetCurrentSiteUrl & "sitemap.ashx"
            Me.SearchEngineSiteMapURL.NavigateUrl = sitemapHandler
            Me.SearchEngineSiteMapURL.Text = sitemapHandler
            Me.btnSubmit.ImageUrl = "~/App_Themes/" & Page.Theme & "/Images/" & "button-send.png"
        End If

    End Sub

    Protected Sub btnDeleteIndex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteIndex.Click

        Try
            Meanstream.Portal.Core.Services.Search.SearchEngineService.Current.DeleteIndex()
            Me.lblMessage.Text = "Index deleted successfully"
        Catch ex As Exception
            Me.lblMessage.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnRefreshIndex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshIndex.Click

        Try
            Meanstream.Portal.Core.Services.Search.SearchEngineService.Current.Index()
            Me.lblMessage.Text = "Site indexed successfully"
        Catch ex As Exception
            Me.lblMessage.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        If Me.cboSearchEngine.SelectedText = "Select" Then
            Me.btnSubmit.ThrowFailure = True
            Me.btnSubmit.FailMessage = "Select a search engine"
            Exit Sub
        End If

        Dim sitemapHandler As String = Meanstream.Portal.Core.Utilities.AppUtility.GetCurrentSiteUrl & "sitemap.ashx"

        If Me.cboSearchEngine.SelectedValue = "Google" Then
            ' resubmit to google
            Try
                Dim reqGoogle As System.Net.WebRequest = System.Net.WebRequest.Create("http://www.google.com/webmasters/tools/ping?sitemap=" & HttpUtility.UrlEncode(sitemapHandler))
                Dim webresponse As System.Net.HttpWebResponse = reqGoogle.GetResponse()
                Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(1252)
                Dim loResponseStream As System.IO.StreamReader = New System.IO.StreamReader(webresponse.GetResponseStream(), enc)
                Dim response As String = loResponseStream.ReadToEnd()
                loResponseStream.Close()
                webresponse.Close()
                Me.btnSubmit.SuccessMessage = "Submit successful"
                Me.litMessage.Text = response
            Catch ex As Exception
                Me.btnSubmit.ThrowFailure = True
                Me.btnSubmit.FailMessage = "Submit failed"
                Me.litMessage.Text = ex.Message
            End Try

        ElseIf Me.cboSearchEngine.SelectedValue = "Bing" Then
            ' resubmit to bing
            Try
                Dim reqBing As System.Net.WebRequest = System.Net.WebRequest.Create("http://www.bing.com/webmaster/ping.aspx?siteMap=" & HttpUtility.UrlEncode(sitemapHandler))
                Dim webresponse As System.Net.HttpWebResponse = reqBing.GetResponse()
                Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(1252)
                Dim loResponseStream As System.IO.StreamReader = New System.IO.StreamReader(webresponse.GetResponseStream(), enc)
                Dim response As String = loResponseStream.ReadToEnd()
                loResponseStream.Close()
                webresponse.Close()
                Me.btnSubmit.SuccessMessage = "Submit successful"
                Me.litMessage.Text = response
            Catch ex As Exception
                Me.btnSubmit.ThrowFailure = True
                Me.btnSubmit.FailMessage = "Submit failed"
                Me.litMessage.Text = ex.Message
            End Try

        ElseIf Me.cboSearchEngine.SelectedValue = "Ask" Then
            ' resubmit to ask
            Try
                Dim reqAsk As System.Net.WebRequest = System.Net.WebRequest.Create("http://submissions.ask.com/ping?sitemap=" & HttpUtility.UrlEncode(sitemapHandler))
                Dim webresponse As System.Net.HttpWebResponse = reqAsk.GetResponse()
                Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(1252)
                Dim loResponseStream As System.IO.StreamReader = New System.IO.StreamReader(webresponse.GetResponseStream(), enc)
                Dim response As String = loResponseStream.ReadToEnd()
                loResponseStream.Close()
                webresponse.Close()
                Me.btnSubmit.SuccessMessage = "Submit successful"
                Me.litMessage.Text = response
            Catch ex As Exception
                Me.btnSubmit.ThrowFailure = True
                Me.btnSubmit.FailMessage = "Submit failed"
                Me.litMessage.Text = ex.Message
            End Try

        End If

    End Sub

End Class
