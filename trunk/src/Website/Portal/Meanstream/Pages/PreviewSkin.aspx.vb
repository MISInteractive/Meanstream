
Partial Class Meanstream_Pages_PreviewSkin
    Inherits System.Web.UI.Page

    Dim Skin As Meanstream.Portal.Core.Content.Skin = Nothing


    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
        If Request.Params("SkinID") <> Nothing Then
            Skin = Meanstream.Portal.Core.Content.ContentService.Current.GetSkin(New Guid(Request.Params("SkinID")))

            If Skin IsNot Nothing Then
                'get portal theme
                Me.Theme = Meanstream.Portal.Core.PortalContext.GetPortalById(Skin.PortalId).Theme
                Me.MasterPageFile = Skin.Path
            End If
        End If
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Request.Params("SkinID") = Nothing Then
                Exit Sub
            End If

            For Each SkinPane As Meanstream.Portal.Core.Content.SkinZone In Skin.Zones
                Dim ContentPlaceHolder As ContentPlaceHolder = Nothing
                ContentPlaceHolder = Master.FindControl(SkinPane.Pane)
                Dim LiteralControl As LiteralControl = New LiteralControl
                LiteralControl.Text = "[" & SkinPane.Pane & "]"
                ContentPlaceHolder.Controls.Add(LiteralControl)
            Next
        End If
    End Sub
End Class
