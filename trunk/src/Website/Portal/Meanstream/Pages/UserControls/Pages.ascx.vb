
Partial Class Meanstream_Pages_UserControls_Pages
    Inherits System.Web.UI.UserControl

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Me.BindGrid()
            Me.btnCreatePage.TargetControlId = Me.CreatePageTarget.ClientID
        End If
    End Sub

    'Private Sub BindGrid()
    '    Dim Pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)

    '    Dim dv As System.Data.DataView = New System.Data.DataView(Pages.ToDataSet(True).Tables(0))

    '    Dim sbSortExpression As New StringBuilder
    '    Dim sbSortDirection As New StringBuilder
    '    Dim sExpression As String = ""
    '    Dim sDirection As String = ""

    '    If Grid1.SortExpressions.Count > 0 Then
    '        Dim myKeys(Grid1.SortExpressions.Count) As String
    '        Grid1.SortExpressions.Keys.CopyTo(myKeys, 0)
    '        Dim count As Integer = Grid1.SortExpressions.Count
    '        count = count - 1

    '        sbSortExpression.Append(myKeys(count))
    '        sExpression = sbSortExpression.ToString

    '        sbSortExpression.Append(" ")
    '        sbSortExpression.Append(Grid1.SortExpressions(myKeys(count)))
    '        sbSortDirection.Append(Grid1.SortExpressions(myKeys(count)))

    '        sDirection = sbSortDirection.ToString

    '        dv.Sort = sbSortExpression.ToString
    '    End If

    '    Me.Grid1.DataSource = dv
    '    Me.Grid1.DataBind()

    '    If Me.Grid1.Rows.Count = 0 Then
    '        'Me.Container.Visible = False
    '    End If

    '    Dim HeaderRow As GridViewRow = Grid1.HeaderRow
    '    Dim Header1 As Meanstream.Web.UI.GridHeader = CType(HeaderRow.FindControl("Header1"), Meanstream.Web.UI.GridHeader)
    '    Header1.GridId = Grid1.UniqueID
    '    Header1.SortExpressionViewState = sExpression
    '    Header1.SortDirectionViewState = sDirection
    'End Sub

    'Private SortExpression As ListDictionary
    'Protected Sub Grid1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid1.Sorting
    '    SortExpression = Grid1.SortExpressions

    '    If Not SortExpression.Contains(e.SortExpression) Then
    '        'Grid1.SortExpressions.Add(e.SortExpression, e.SortDirection.ToString.Replace("Ascending", "ASC").Replace("Descending", "DESC"))
    '        SortExpression.Add(e.SortExpression, "ASC")
    '    Else
    '        ' Get sort direction
    '        Dim strSortDirection As String = SortExpression.Item(e.SortExpression)
    '        ' Was it ascending?
    '        If strSortDirection = "ASC" Then
    '            ' Yes, so sort in desc
    '            SortExpression.Item(e.SortExpression) = "DESC"
    '        ElseIf strSortDirection = "DESC" Then
    '            ' it is descending
    '            ' remove the sort order
    '            SortExpression.Remove(e.SortExpression)
    '        End If
    '    End If
    '    Me.Grid1.EditIndex = -1
    '    Grid1.SortExpressions = SortExpression
    '    BindGrid()
    'End Sub

    'Protected Sub Grid1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid1.PageIndexChanging
    '    Grid1.PageIndex = e.NewPageIndex
    '    Grid1.EditIndex = -1
    '    BindGrid()
    'End Sub

    'Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
    '    If TrueOrFalse Then
    '        Return "yes"
    '    End If
    '    Return "no"
    'End Function

    'Public Function GetSkinName(ByVal SkinID As Guid) As String
    '    Return Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(SkinID).Name
    'End Function

    'Public Function GetPath(ByVal pageId As Guid) As String
    '    Return Meanstream.Portal.Core.Utilities.AppUtility.GetBreadCrumbs(portalId, pageId, " > ")
    'End Function
End Class
