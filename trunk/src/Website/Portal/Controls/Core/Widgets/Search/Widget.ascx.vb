
Partial Class Controls_Core_Widgets_Search_Widget
    Inherits Meanstream.Portal.Core.Widgets.Search.WidgetBase

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim q As String = Request.Params("q")
            If q <> "" Then
                q = Server.UrlDecode(q)
                'Try
                bindSearch(q)
                'Catch ex As Exception
                'Me.litResults.Text = ex.Message
                'End Try

                txtSearch.Text = q
            Else
                Me.litNoResults.Visible = True
            End If

        End If

    End Sub

    Private Sub bindSearch(ByVal keyword As String)

        Dim strPage As String = Request.Params("page")
        Dim intPage As Integer = 1

        If strPage = "" Then
            intPage = 1
        Else
            intPage = CInt(strPage)
        End If

        Dim count As Integer = 10
        
        Dim startindex As Integer = (intPage - 1) * count

        Dim documents As List(Of Meanstream.Portal.Core.Services.Search.Document) = Meanstream.Portal.Core.Services.Search.SearchEngineService.Current.Search(keyword)

        Dim objPds As PagedDataSource = New PagedDataSource
        objPds.DataSource = documents
        objPds.AllowPaging = True
        objPds.PageSize = 10
        objPds.CurrentPageIndex = (intPage - 1)

        Me.rptResults.DataSource = objPds
        Me.rptResults.DataBind()

        Dim PageCount As Integer = objPds.PageCount
        Dim FirstIndex As Integer = objPds.FirstIndexInPage + 1
        Dim LastIndex As Integer = 0

        If objPds.IsFirstPage Then
            LastIndex = objPds.Count
        ElseIf objPds.IsLastPage Then
            LastIndex = documents.Count
        Else
            LastIndex = FirstIndex + objPds.PageSize
        End If

        If objPds.Count > 0 Then

        End If

        Dim s As String = "Results " & FirstIndex & " - " & LastIndex & " of " & documents.Count
        litResults.Text = s
        litShowNav.Text = GenerateNav(intPage, PageCount, Server.UrlEncode(keyword))
        litShowNav2.Text = litShowNav.Text

    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Click"/> event. Validates user credentials and authenticates.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim keyword As String = txtSearch.Text
        keyword = Server.UrlEncode(keyword)
        Dim Pagename As String = Request.RawUrl

        If Request.RawUrl.IndexOf("?") <> -1 Then
            Pagename = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"))
        End If

        Response.Redirect("~/" & Pagename & "?q=" & keyword)

    End Sub

    Private Function GenerateNav(ByVal intPage As Integer, ByVal intTotalPages As Integer, ByVal s As String) As String

        Dim strOutPut As String
        strOutPut = ""
        Dim strNext As String
        strNext = ""

        Dim Pagename As String = Request.RawUrl

        If Request.RawUrl.IndexOf("?") <> -1 Then
            Pagename = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"))
        End If

        If intPage > 1 Then ' prev button
            strOutPut = "<a href='" & Pagename & "?q=" & s & "&page=" & (intPage - 1) & "'>Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"

        End If

        If intPage < intTotalPages Then ' next button
            strNext = "<a href='" & Pagename & "?q=" & s & "&page=" & (intPage + 1) & "&'>" & _
                        "Next</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        End If

        Dim i As Integer

        Dim intTotalPages1 = intTotalPages
        If intTotalPages > 3 Then
            Dim totalpage = intPage + 3
            If totalpage > intTotalPages1 Then
                intTotalPages1 = totalpage - intTotalPages1
                intTotalPages1 = ((intPage + 3) - intTotalPages1)
                i = intPage - 3
            Else
                intTotalPages1 = totalpage
                i = intPage
            End If

        Else
            intTotalPages1 = intTotalPages
        End If

        If i = 0 Then
            i = 1
        End If

        For i = i To intTotalPages1
            If i <> intPage And i > 0 Then
                strOutPut = strOutPut & "<a href='" & Pagename & "?q=" & s & "&page=" & i & "'>" & _
                            i & "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 'last page
            ElseIf i > 0 Then
                strOutPut = strOutPut & "<b>" & i & "</b>&nbsp;&nbsp;" 'page count
            End If
        Next

        strOutPut = strOutPut & strNext
        Return strOutPut

    End Function

    Public Function GetTitle(ByVal content As String) As String
        Dim TitleMatch As Match = Regex.Match(content, "<title>([^<]*)</title>", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
        Return TitleMatch.Groups(1).Value
    End Function

    Public Function GetDescription(ByVal content As String) As String
        Dim DescriptionMatch As Match = Regex.Match(content, "<meta name=""description"" content=""([^<]*)"">", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
        Return DescriptionMatch.Groups(1).Value
    End Function

End Class
