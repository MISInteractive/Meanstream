
Partial Class Meanstream_Pages_UserControls_ViewPage
    Inherits System.Web.UI.UserControl

    Private portalId As Guid = Meanstream.Portal.Core.PortalContext.Current.PortalId


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Request.Params("PageID") <> Nothing Then
                'If SessionManager.GetWorkflowManager.Enabled() Then
                '    Me.WorkflowsTab.Visible = True
                'Else
                '    Me.WorkflowsTab.Visible = False
                'End If

                'Me.litBreadcrumb.Text = Me.GetPath(New Guid(Request.Params("PageID").ToString))
            End If
        End If
    End Sub


    Public Function GetPath(ByVal PageID As Guid) As String
        Dim Path As String = ""
        Dim PageList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)
        Dim Page As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", PageID)

        If Page.ParentId <> Nothing Then
            Dim Parent As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", Page.ParentId)

            If Parent Is Nothing Then
                Return Page.Name
            End If

            If Parent.ParentId <> New Guid("00000000-0000-0000-0000-000000000000") Then
                Dim Parent2 As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", Parent.ParentId)

                If Parent2 Is Nothing Then
                    Return Page.Name
                End If

                If Parent2.ParentId <> New Guid("00000000-0000-0000-0000-000000000000") Then
                    Dim Parent3 As Meanstream.Portal.Core.Entities.MeanstreamPage = PageList.Find("Id", Parent2.ParentId)
                    If Parent3 Is Nothing Then
                        Return Page.Name
                    End If

                    Path = "<a href='Default.aspx?ctl=ViewPage&PageID=" & Parent3.Id.ToString & "'>" & Parent3.Name & "</a>  &nbsp;>&nbsp;  " & "<a href='Default.aspx?ctl=ViewPage&PageID=" & Parent2.Id.ToString & "'>" & Parent2.Name & "</a>  &nbsp;>&nbsp;  " & "<a href='Default.aspx?ctl=ViewPage&PageID=" & Parent.Id.ToString & "'>" & Parent.Name & "</a>"
                Else
                    Path = "<a href='Default.aspx?ctl=ViewPage&PageID=" & Parent2.Id.ToString & "'>" & Parent2.Name & "</a>  &nbsp;>&nbsp;  " & "<a href='Default.aspx?ctl=ViewPage&PageID=" & Parent.Id.ToString & "'>" & Parent.Name & "</a>"
                End If

            Else
                Path = "<a href='Default.aspx?ctl=ViewPage&PageID=" & Parent.Id.ToString & "'>" & Parent.Name & "</a>"
            End If

            Path = Path & "  &nbsp;>&nbsp;  " & Page.Name
        Else
            Path = Page.Name
        End If

        Return Path
    End Function
End Class
