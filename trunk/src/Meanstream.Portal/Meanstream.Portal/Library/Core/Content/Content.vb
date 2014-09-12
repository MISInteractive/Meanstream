Namespace Meanstream.Portal.Core.Content
    Public Class Content
        Inherits Meanstream.Portal.Core.Entities.MeanstreamContent

        Sub New()

        End Sub

        Sub New(ByVal id As Integer)
            Dim Content As Meanstream.Portal.Core.Entities.MeanstreamContent = Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.GetById(id)
            If Content Is Nothing Then
                Throw New ApplicationException("Content Not Found")
            End If

            Me.Author = Content.Author
            Me.ContentType = Content.ContentType
            Me.CreatedDate = Content.CreatedDate
            Me.IsShared = Content.IsShared
            Me.LastUpdateDate = Content.LastUpdateDate
            Me.Text = Content.Text
            Me.Id = Content.Id
            Me.UserId = Content.UserId
            Me.Title = Content.Title
        End Sub

        Public Shared Function Create(ByVal Title As String, ByVal Author As String, _
                                      ByVal ContentType As Meanstream.Portal.Core.Content.ContentType, _
                                      ByVal Text As String) As Meanstream.Portal.Core.Content.Content

            If ContentType Is Nothing Then
                Throw New ArgumentException("ContentType cannot be nothing. Use the GetContentTypes() method to find the correct content type. Default content type is 'Free Text'")
            End If

            Dim Types As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamContentType) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentTypeProvider.Find("Type=" & ContentType.Type)

            If Types.Count = 0 Then
                Throw New InvalidOperationException("ContentType does not exist. Use the GetContentTypes() method to find the correct content type. Default content type is 'Free Text'")
            End If

            Dim Content As Meanstream.Portal.Core.Content.Content = New Meanstream.Portal.Core.Content.Content
            Content.Author = Author
            Content.ContentType = Types(0).Type
            Content.CreatedDate = Date.Now
            Content.IsShared = False
            Content.LastUpdateDate = Date.Now
            Content.Text = Text
            Content.Title = Title

            If System.Web.HttpContext.Current.Request.IsAuthenticated Then
                If System.Web.HttpContext.Current.Profile IsNot Nothing And System.Web.HttpContext.Current.Profile.UserName IsNot Nothing Then
                    Content.UserId = Membership.MembershipService.Current.GetUserGuid(System.Web.HttpContext.Current.Profile.UserName)
                End If
            End If

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.Insert(Content) Then
                Return Content
            End If
            Return Nothing
        End Function

        Public Shared Function GetContentByContentTypeId(ByVal contentType As String) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamContent)
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.Find("ContentType=" & contentType)
        End Function

        Public Shared Function GetSharedContentByContentTypeId(ByVal contentType As String) As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamContent)
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.Find("IsShared=True AND ContentType=" & contentType)
        End Function

        Public Shared Function GetContent(ByVal Id As Integer) As Meanstream.Portal.Core.Content.Content
            Dim ContentBase As Meanstream.Portal.Core.Entities.MeanstreamContent = Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.GetById(Id)
            If ContentBase Is Nothing Then
                Return Nothing
            End If
            Dim Content As Meanstream.Portal.Core.Content.Content = New Meanstream.Portal.Core.Content.Content
            Content.Author = ContentBase.Author
            Content.Id = ContentBase.Id
            Content.ContentType = ContentBase.ContentType
            Content.CreatedDate = ContentBase.CreatedDate
            Content.IsShared = ContentBase.IsShared
            Content.LastUpdateDate = ContentBase.LastUpdateDate
            Content.Text = ContentBase.Text
            Content.UserId = ContentBase.UserId
            Content.Title = ContentBase.Title
            Return Content
        End Function

        Public Sub Save()
            Me.LastUpdateDate = Date.Now
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.Update(Me)
        End Sub

        Public Sub Delete()
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentProvider.Delete(Me.Id)
        End Sub
    End Class
End Namespace

