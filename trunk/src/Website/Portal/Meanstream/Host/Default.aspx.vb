
Partial Class Meanstream_Host_Default
    Inherits System.Web.UI.Page
    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreInit
        Page.Theme = Meanstream.Portal.Core.Membership.MembershipService.Current.GetMeanstreamThemeForUser(Profile.UserName)
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim ControlToLoad As String = Request.Params("ctl")
        If ControlToLoad <> Nothing And ControlToLoad <> "" Then
            Dim ContentPlaceHolder As ContentPlaceHolder = Nothing
            ContentPlaceHolder = Master.FindControl("ContentPane")
            Dim ModuleControl As Control = LoadControl("./UserControls/" & ControlToLoad & ".ascx")
            ContentPlaceHolder.Controls.Add(ModuleControl)
        End If
    End Sub
End Class
