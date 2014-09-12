
Partial Class Meanstream_Dashboard_RecentPublishedPages
    Inherits System.Web.UI.UserControl

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindGrid()
        End If
    End Sub


    Private Sub BindGrid()
        Dim portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId
        Dim Pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Content.ContentService.Current.GetMostRecentPublished(portalId, 5)
        If Pages.Count > 0 Then
            Me.Grid1.DataSource = Pages
            Me.Grid1.DataBind()
        Else
            Me.Container.Visible = False
            Me.lblMessage.Text = "No Records Found"
        End If
    End Sub


    Public Function GetPath(ByVal pageId As Guid) As String
        Return Meanstream.Portal.Core.Utilities.AppUtility.GetBreadCrumbs(portalId, pageId, " > ")
    End Function


    Public Function GetSkinName(ByVal skinId As Guid) As String
        Return Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(skinId).Name
    End Function


    Public Function GetFlag(ByVal TrueOrFalse As Boolean) As String
        If TrueOrFalse Then
            Return "yes"
        End If
        Return "no"
    End Function
End Class
