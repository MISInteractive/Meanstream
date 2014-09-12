
Partial Class Meanstream_Administration_UserControls_ExportProfiles
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Configuration As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        Dim ProfileSection As System.Web.Configuration.ProfileSection = DirectCast(Configuration.GetSection("system.web/profile"), System.Web.Configuration.ProfileSection)
        Dim PropertySettings As System.Web.Configuration.RootProfilePropertySettingsCollection = ProfileSection.PropertySettings

        For Each PropertySetting As System.Web.Configuration.ProfilePropertySettings In PropertySettings
            Me.ProfileGrid.Columns.Add(CreateBoundColumns(New System.Data.DataColumn(PropertySetting.Name)))
        Next

        Dim ProfileList As IList(Of ProfileCommon) = New List(Of ProfileCommon)

        Dim pc As ProfileInfoCollection = ProfileManager.GetAllProfiles(ProfileAuthenticationOption.All)
        For Each pi As ProfileInfo In pc
            Dim prof As ProfileCommon = Profile.GetProfile(pi.UserName)
            ProfileList.Add(prof)
        Next

        Me.ProfileGrid.DataSource = ProfileList
        Me.ProfileGrid.DataBind()

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Profiles.xls")
        Response.Charset = ""

        Response.ContentType = "application/ms-excel"
        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        Me.ProfileGrid.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString)
        Response.End()
    End Sub

    Private Function CreateBoundColumns(ByRef colDataColumn As System.Data.DataColumn) As BoundField
        Dim bndColumn As New BoundField()
        bndColumn.DataField = colDataColumn.ColumnName
        bndColumn.HtmlEncode = False
        bndColumn.HeaderText = colDataColumn.ColumnName.Replace("_", " ")
        bndColumn.DataFormatString = SetFormatString(colDataColumn)
        Return bndColumn
    End Function
    '
    Private Function SetFormatString(ByRef colDataColumn As System.Data.DataColumn) As String
        Dim strDataType As String
        Select Case colDataColumn.DataType.ToString()
            Case "System.Int32"
                strDataType = "{0:#,###}"
            Case "System.Decimal"
                strDataType = "{0:C}"
            Case "System.DateTime"
                strDataType = "{0:dd-mm-yyyy}"
            Case "System.String"
                strDataType = ""
            Case Else
                strDataType = ""
        End Select
        Return strDataType
    End Function


    Public Overloads Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
End Class
