
Partial Class Meanstream_Pages_UserControls_Skins
    Inherits System.Web.UI.UserControl

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
    Dim Pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Nothing


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Me.BindGrid()
            Me.btnCreateSkin.TargetControlId = Me.CreateSkinTarget.ClientID
        End If
    End Sub


    'Private Sub BindGrid()
    '    Dim Skins As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkins) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Find("PortalId=" & portalId.ToString)

    '    Dim dv As System.Data.DataView = New System.Data.DataView(Skins.ToDataSet(True).Tables(0))

    '    Dim sbSortExpression As New StringBuilder
    '    Dim sbSortDirection As New StringBuilder
    '    Dim sExpression As String = ""
    '    Dim sDirection As String = ""

    '    If SkinGrid.SortExpressions.Count > 0 Then
    '        Dim myKeys(SkinGrid.SortExpressions.Count) As String
    '        SkinGrid.SortExpressions.Keys.CopyTo(myKeys, 0)
    '        Dim count As Integer = SkinGrid.SortExpressions.Count
    '        count = count - 1
    '        sbSortExpression.Append(myKeys(count))
    '        sExpression = sbSortExpression.ToString
    '        sbSortExpression.Append(" ")
    '        sbSortExpression.Append(SkinGrid.SortExpressions(myKeys(count)))
    '        sbSortDirection.Append(SkinGrid.SortExpressions(myKeys(count)))
    '        sDirection = sbSortDirection.ToString

    '        dv.Sort = sbSortExpression.ToString
    '    End If

    '    If Skins.Count > 0 Then
    '        Me.SkinGrid.DataSource = dv
    '        Me.SkinGrid.DataBind()

    '        Dim HeaderRow As GridViewRow = SkinGrid.HeaderRow
    '        Dim Header1 As Meanstream.Web.UI.GridHeader = CType(HeaderRow.FindControl("Header1"), Meanstream.Web.UI.GridHeader)
    '        Header1.GridId = SkinGrid.UniqueID
    '        Header1.SortExpressionViewState = sExpression
    '        Header1.SortDirectionViewState = sDirection
    '    Else
    '        Me.Container.Visible = False
    '        Me.lblMessage.Text = "No Records Found"
    '    End If
    'End Sub


    'Public Function GetSkinPageList(ByVal SkinId As String) As String
    '    Dim List As String = ""
    '    Dim Pages As List(Of Meanstream.Portal.Core.Content.Page) = Meanstream.Portal.Core.Content.ContentService.Current.GetPagesBySkinId(New Guid(SkinId))
    '    For Each Page As Meanstream.Portal.Core.Content.Page In Pages
    '        List = List & Page.Name
    '    Next
    '    Return List
    'End Function


    'Protected Sub SkinGrid_DataBinding(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles SkinGrid.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim SkinId As String = DataBinder.Eval(e.Row.DataItem, "Id").ToString

    '        If Me.GetPageCount(SkinId) > 0 Then
    '            Dim DeleteButton As ImageButton = e.Row.Cells(2).Controls(0)
    '            DeleteButton.Visible = False
    '        End If

    '    End If
    'End Sub


    'Protected Sub SkinGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles SkinGrid.RowDeleting
    'End Sub


    'Protected Sub SkinGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles SkinGrid.RowCommand

    '    Select Case e.CommandName
    '        Case "Delete"
    '            Dim rowIndex As Integer = CInt(e.CommandArgument)
    '            Dim Id As String = DirectCast(SkinGrid.Rows(rowIndex).FindControl("lblID"), Label).Text

    '            Try
    '                Dim skin As Meanstream.Portal.Core.Content.Skin = Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(New Guid(Id))
    '                Dim manager As New Meanstream.Portal.Core.Content.SkinManager(skin)
    '                manager.Delete()

    '                Me.SkinGrid.EditIndex = -1
    '                BindGrid()
    '                Me.lblStatus.Text = "Skins has been successfully deleted."
    '            Catch ex As Exception
    '                Me.lblStatus.Text = ex.Message
    '            End Try
    '    End Select
    'End Sub


    'Protected Sub SkinGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles SkinGrid.PageIndexChanging
    '    SkinGrid.PageIndex = e.NewPageIndex
    '    SkinGrid.EditIndex = -1
    '    BindGrid()
    'End Sub


    'Public Function GetPageCount(ByVal SkinId As String) As Integer
    '    Return Meanstream.Portal.Core.Content.ContentService.Current.GetPagesBySkinId(New Guid(SkinId)).Count
    'End Function


    'Public Function ParseFilePath(ByVal SkinSrc As String) As String
    '    Return SkinSrc.Substring(SkinSrc.LastIndexOf("/") + 1)
    'End Function


    'Private SortExpression As ListDictionary
    'Protected Sub SkinGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles SkinGrid.Sorting
    '    SortExpression = SkinGrid.SortExpressions
    '    If Not SortExpression.Contains(e.SortExpression) Then
    '        SortExpression.Add(e.SortExpression, "ASC")
    '    Else
    '        Dim strSortDirection As String = SortExpression.Item(e.SortExpression)
    '        If strSortDirection = "ASC" Then
    '            SortExpression.Item(e.SortExpression) = "DESC"
    '        ElseIf strSortDirection = "DESC" Then
    '            SortExpression.Remove(e.SortExpression)
    '        End If
    '    End If
    '    Me.SkinGrid.EditIndex = -1
    '    Me.SkinGrid.SortExpressions = SortExpression
    '    BindGrid()
    'End Sub
End Class
