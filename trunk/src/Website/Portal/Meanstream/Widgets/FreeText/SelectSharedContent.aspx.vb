
Partial Class Meanstream_Widgets_FreeText_SelectSharedContent
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_PreInit"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreInit
        Page.Theme = Meanstream.Portal.Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(Profile.UserName)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:System.Web.UI.Control.Page_Load"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.ddlContentType.DataTextField = "Type"
            Me.ddlContentType.DataValueField = "Id"
            Dim ContentTypes As List(Of Meanstream.Portal.Core.Content.ContentType) = Meanstream.Portal.Core.Content.ContentType.GetContentTypes
            Me.ddlContentType.DataSource = ContentTypes
            Me.ddlContentType.DataBind()
            Me.ddlContentType.SelectedValue = ContentTypes(0).Type
            Me.ddlContentType.SelectedText = ContentTypes(0).Type
            Me.BindGrid()
        End If
    End Sub

    ''' <summary>
    ''' Binds grid with the list of shared content
    ''' </summary>
    Protected Sub BindGrid()
        Dim ContentList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamContent) = Meanstream.Portal.Core.Content.Content.GetSharedContentByContentTypeId(Me.ddlContentType.SelectedValue)
        ContentList.Sort("Title")
        Me.ContentGrid.DataSource = ContentList
        Me.ContentGrid.DataBind()
    End Sub

    ''' <summary>
    ''' Raises the <see cref="Meanstream.Web.UI.ComboBox.SelectedValueChanged"/> event.
    ''' </summary>
    ''' <param name="o">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.Web.UI.WebControls.EventArgs"/> object that contains the event data.</param>
    Protected Sub ddlContentType_SelectedValueChanged(ByVal o As Object, ByVal e As System.EventArgs) Handles ddlContentType.SelectedValueChanged
        If Page.IsPostBack Then
            Me.BindGrid()
        End If
    End Sub

    ''' <summary>
    ''' Raises the <see cref="Meanstream.Web.UI.Grid.PageIndexChanging"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs"/> object that contains the event data.</param>
    Protected Sub ContentGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ContentGrid.PageIndexChanging
        ContentGrid.PageIndex = e.NewPageIndex
        ContentGrid.EditIndex = -1
        BindGrid()
    End Sub

    ''' <summary>
    ''' Raises the <see cref="Meanstream.Web.UI.Grid.RowCommand"/> event.
    ''' </summary>
    ''' <param name="sender">The <see cref="T:System.Object"/> object that contains the object.</param>
    ''' <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs"/> object that contains the event data.</param>
    Protected Sub ContentGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ContentGrid.RowCommand
        Me.lblStatus.Text = ""
        Dim Id As String = e.CommandArgument

        Select Case e.CommandName
            Case "Select"
                Me.litContent.Value = Id

                Dim Content As Meanstream.Portal.Core.Content.Content = Meanstream.Portal.Core.Content.Content.GetContent(Id)
                Me.lblStatus.Text = "You have selected " & Content.Title
        End Select
    End Sub
End Class
