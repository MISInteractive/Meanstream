
Partial Class Meanstream_Dashboard_MyRecentEdits
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        'Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        'Dim recent As Meanstream.Portal.Core.Entities.VList(Of Meanstream.Portal.Core.Entities.VwMeanstreamRecentEdits) = Meanstream.Portal.Core.Content.ContentService.Current.GetMostRecentEdits(portalId, 5)
        'If recent.Count > 0 Then
        '    Me.Grid1.DataSource = recent
        '    Me.Grid1.DataBind()
        'Else
        '    Me.Container.Visible = False
        '    Me.lblMessage.Text = "No Records Found"
        'End If
    End Sub

    Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
        If TrueOrFalse Then
            Return "yes"
        End If
        Return "no"
    End Function
End Class
