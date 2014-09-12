

Namespace Meanstream.Portal.Core.Services.SearchEngineSiteMap
    Public Class SiteMapXMLNode

        Public Enum enChangeFrequency
            NoneGiven
            Always
            Hourly
            Daily
            Weekly
            Monthly
            Yearly
            Never
        End Enum

        <System.Xml.Serialization.XmlElement(ElementName:="loc")> _
        Public URL As String

#Region "Last Modified"
        <System.Xml.Serialization.XmlIgnore()> _
        Public LastModified As DateTime
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
        <System.Xml.Serialization.XmlElement(ElementName:="lastmod")> _
        Public Property __LastModified() As String
            Get
                Return (If(LastModified = DateTime.MinValue, Nothing, String.Format("{0:yyyy-MM-dd}", LastModified)))
            End Get
            Set(ByVal value As String)
                LastModified = DateTime.MinValue
            End Set
        End Property
        'Note: This property will never be deserialized
#End Region

#Region "Change Frequency"
        <System.Xml.Serialization.XmlIgnore()> _
        Public ChangeFrequency As enChangeFrequency
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
        <System.Xml.Serialization.XmlElement(ElementName:="changefreq")> _
        Public Property __ChangeFrequency() As String
            Get
                Return (If(ChangeFrequency = enChangeFrequency.NoneGiven, Nothing, ChangeFrequency.ToString().ToLower()))
            End Get
            Set(ByVal value As String)
                ChangeFrequency = enChangeFrequency.NoneGiven
            End Set
        End Property
        'Note: This property will never be deserialized
#End Region

#Region "Priority"
        <System.Xml.Serialization.XmlIgnore()> _
        Public Priority As System.Nullable(Of Decimal)
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
        <System.Xml.Serialization.XmlElement(ElementName:="priority")> _
        Public Property __Priority() As String
            Get
                Return (If(Not Priority.HasValue, Nothing, String.Format("{0:0.0}", Priority)))
            End Get
            Set(ByVal value As String)
                Priority = Nothing
            End Set
        End Property
        'Note: This property will never be deserialized
#End Region

        Public Sub New()

            URL = Nothing
            LastModified = DateTime.MinValue
            ChangeFrequency = enChangeFrequency.NoneGiven
            'default
            Priority = Nothing

        End Sub

        Public Sub New(ByVal node As System.Web.SiteMapNode)

            Me.New()

            If node.Url.Replace("~/", "") = "ROOT" Then
                Return
            End If

            URL = Utilities.AppUtility.GetCurrentSiteUrl() & node.Url.Replace("~/", "")

            Dim portalId As Guid = PortalContext.Current.PortalId

            Dim pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Utilities.AppUtility.GetAllPageEntities(portalId)
            For Each page As Meanstream.Portal.Core.Entities.MeanstreamPage In pages
                If page.Id.ToString = node.Key Then
                    If page.IsHome Then
                        URL = URL.Replace(page.Url, "")
                    End If
                    LastModified = page.PublishedDate
                End If
            Next

            Priority = 0.9
            ChangeFrequency = enChangeFrequency.Weekly

        End Sub

    End Class
End Namespace
