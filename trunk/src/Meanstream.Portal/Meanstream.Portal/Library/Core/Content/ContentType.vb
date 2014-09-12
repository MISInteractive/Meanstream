Namespace Meanstream.Portal.Core.Content
    Public Class ContentType
        Private _Type As String = ""
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        Public Shared Function CreateContentType(ByVal name As String) As Meanstream.Portal.Core.Content.ContentType
            If Name.Trim = "" Then
                Throw New ArgumentException("ContentType is required")
            End If

            Dim ContentType As Meanstream.Portal.Core.Entities.MeanstreamContentType = New Meanstream.Portal.Core.Entities.MeanstreamContentType
            ContentType.Type = Name

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentTypeProvider.Insert(ContentType) Then
                Dim RContentType As Meanstream.Portal.Core.Content.ContentType = New Meanstream.Portal.Core.Content.ContentType
                RContentType.Type = ContentType.Type
                Return RContentType
            End If
            Return Nothing
        End Function

        Public Shared Function GetContentTypes() As List(Of Meanstream.Portal.Core.Content.ContentType)
            Dim List As List(Of Meanstream.Portal.Core.Content.ContentType) = New List(Of Meanstream.Portal.Core.Content.ContentType)
            For Each Type As Meanstream.Portal.Core.Entities.MeanstreamContentType In Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentTypeProvider.GetAll()
                Dim ContentType As Meanstream.Portal.Core.Content.ContentType = New Meanstream.Portal.Core.Content.ContentType
                ContentType.Type = Type.Type
                List.Add(ContentType)
            Next
            Return List
        End Function
    End Class
End Namespace
