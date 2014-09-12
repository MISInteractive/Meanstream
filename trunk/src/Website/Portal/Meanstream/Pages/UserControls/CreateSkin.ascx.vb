Imports System.IO
Imports Meanstream.Portal.Core
Imports Meanstream.Portal.Core.Instrumentation
Imports HtmlAgilityPack

Partial Class Meanstream_Pages_UserControls_CreateSkin
    Inherits System.Web.UI.UserControl


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim files() As String = Directory.GetFiles(Server.MapPath(PortalContext.Current.Portal.SkinsPath))
            For Each filePath As String In files
                If filePath.EndsWith(".cshtml") Then '.master
                    Dim filename As String = filePath.Substring(filePath.LastIndexOf("\") + 1)
                    Dim item As New Meanstream.Web.UI.ComboBoxItem()
                    item.Text = filename
                    item.Value = PortalContext.Current.Portal.SkinsPath & "/" & filename
                    Me.cboMasterPage.Items.Add(item)
                End If
            Next

            'default combo item
            Dim blankItem As New Meanstream.Web.UI.ComboBoxItem
            blankItem.Text = "select layout"
            blankItem.Value = "0"
            Me.cboMasterPage.Items.Insert(0, blankItem)
            Me.cboMasterPage.SelectedValue = "0"

            Me.btnSave.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/button-save.png"
        End If
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtName.Text.Trim = "" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Name required"
            Return
        End If

        If Me.cboMasterPage.SelectedValue.Trim = "0" Then
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "Layout required"
            Return
        End If

        'Me.btnSave.ThrowFailure = True
        'Me.btnSave.FailMessage = Me.cboMasterPage.SelectedValue
        'Return

        Try
            Dim portalId As Guid = PortalContext.Current.PortalId
            Me.CreateSkin(portalId, Me.txtName.Text.Trim, Me.cboMasterPage.SelectedValue)
            Me.btnSave.SuccessMessage = "Skin created succesfully"
        Catch ex As Exception
            Me.btnSave.ThrowFailure = True
            Me.btnSave.FailMessage = "A Virtual path is required for path"
            Meanstream.Portal.Core.Instrumentation.PortalTrace.Fail(ex.Message, Instrumentation.DisplayMethodInfo.FullSignature)
        End Try
    End Sub

    
    Public Function CreateSkin(ByVal portalId As Guid, ByVal name As String, ByVal path As String) As Guid
        'PortalTrace.WriteLine([String].Concat("CreateSkin() ", AppFriendlyName, " #", ApplicationId, " name=", name, " path=", path))

        If portalId = Nothing Then
            Throw New ArgumentException("portalId is null")
        End If

        Dim Skin As Meanstream.Portal.Core.Content.Skin = Meanstream.Portal.Core.Content.ContentService.Current.GetSkinByPath(path)

        If Skin IsNot Nothing Then
            Throw New ApplicationException("skin already exists")
        End If

        'Dim Page As System.Web.UI.Page = CType(HttpContext.Current.Handler, System.Web.UI.Page)

        'Dim MasterPage As Control = Page.LoadControl(path)

        'If MasterPage Is Nothing Then
        '    Throw New ApplicationException("Cannot find Layout")
        'End If

        
        'Get layout
        Dim html As String = System.IO.File.ReadAllText(Server.MapPath(path), System.Text.Encoding.UTF8)
        Dim i As Integer = html.IndexOf("data-portal-content-pane=""")
        Dim SkinPaneList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = New Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane)

        ' Loop over the found indexes
        Do While (i <> -1)
            ' Write the substring
            Dim f_pane As String = html.Substring(i + 26)
            Dim e As Integer = f_pane.IndexOf("""")
            Dim pane As String = f_pane.Substring(0, e)

            Dim SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane = New Meanstream.Portal.Core.Entities.MeanstreamSkinPane
            SkinPane.Pane = pane
            SkinPaneList.Add(SkinPane)

            ' Get next index
            i = html.IndexOf("data-portal-content-pane=""", i + 1)
        Loop

        Dim entity As New Meanstream.Portal.Core.Entities.MeanstreamSkins
        entity.SkinRoot = name
        entity.SkinSrc = path
        entity.PortalId = portalId
        entity.Id = Guid.NewGuid

        'Dim SkinPaneList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = New Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane)

        
        'For Each Control As Control In MasterPage.Controls
        '    'iterate through to find contentplaceholders
        '    If TypeOf (Control) Is System.Web.UI.HtmlControls.HtmlForm Then
        '        For Each FormControl As Control In Control.Controls
        '            If TypeOf (FormControl) Is System.Web.UI.WebControls.ContentPlaceHolder Then
        '                Dim ContentPlaceHolder As System.Web.UI.WebControls.ContentPlaceHolder = FormControl
        '                'add to list
        '                Dim SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane = New Meanstream.Portal.Core.Entities.MeanstreamSkinPane
        '                SkinPane.Pane = ContentPlaceHolder.ID
        '                SkinPaneList.Add(SkinPane)
        '            End If
        '        Next
        '    End If
        'Next

        If Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Insert(entity) Then
            'add skin panes
            For Each SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane In SkinPaneList
                SkinPane.SkinId = entity.Id
                SkinPane.Id = Guid.NewGuid
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Insert(SkinPane)
            Next
            Return entity.Id
        Else
            Throw New ApplicationException("skin insert failed")
        End If

        Return Nothing
    End Function
End Class
