
Partial Class Meanstream_Controls_Menu
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Menu.IconPath = "~/App_Themes/" & Me.Page.Theme & "/Menu/Icons/"
        Me.Dashboard.ImageUrl = "~/App_Themes/" & Me.Page.Theme & "/images/nav-swoosh.png"
        Me.User.Text = Profile.UserName
        'Me.Site.Text = Meanstream.Portal.Core.PortalContext.Current.Portal.Name
        Dim UserRoles() As String = Meanstream.Portal.Core.Membership.MembershipService.Current.GetUserRoles(Me.Context.Profile.UserName)

        For Each item As Meanstream.Portal.WebControls.MenuItem In Me.Menu.ChildItems
            Me.Recurse(item, UserRoles)
        Next
    End Sub

    Private Sub Recurse(ByVal item As Meanstream.Portal.WebControls.MenuItem, ByVal UserRoles() As String)
        Dim RolesArray() As String = item.Roles.Split(",")

        If item.Roles.Trim = "" Or item.Visible = False Then
            Exit Sub
        End If

        If Not String.IsNullOrWhiteSpace(item.Roles) And RolesArray.Length = 0 Then
            RolesArray = {item.Roles}
        End If

        item.Visible = False

        For Each Role As String In RolesArray
            'always host
            If Array.IndexOf(UserRoles, Meanstream.Portal.Core.AppConstants.HOST) <> -1 Then
                item.Visible = True
                Exit For
            Else
                If Array.IndexOf(UserRoles, Role.Trim) <> -1 Then
                    item.Visible = True
                    Exit For
                End If
            End If
        Next

        If item.Visible And item.ChildItems.Count > 0 Then
            For Each child As Meanstream.Portal.WebControls.MenuItem In item.ChildItems
                Recurse(child, RolesArray)
            Next
        End If
    End Sub
End Class
