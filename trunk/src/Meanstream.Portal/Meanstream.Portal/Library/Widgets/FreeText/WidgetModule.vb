Namespace Meanstream.Portal.Core.Widgets.FreeText
    Public Class WidgetModule
        Inherits Meanstream.Portal.Core.Entities.MeanstreamFreeText
        Implements Meanstream.Portal.Core.WidgetFramework.IModuleDTO
        Implements Meanstream.Portal.Core.Content.ISearchable

        Sub New()
        End Sub

#Region "Properties"
        Private _Content As Meanstream.Portal.Core.Content.Content

        ''' <summary>
        ''' Implements ISearchable.Content. Retrieves the custom widget content from the data store by ContentId 
        ''' </summary>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Content"/> widget content</returns>
        Public ReadOnly Property Content() As Core.Content.Content Implements Core.Content.ISearchable.Content
            Get
                If _Content Is Nothing Then
                    Try
                        _Content = Meanstream.Portal.Core.Content.Content.GetContent(Me.ContentId)
                    Catch ex As Exception
                    End Try
                End If
                Return _Content
            End Get
        End Property
#End Region

        ''' <summary>
        ''' Initializes the custom widget module and loads from the data store by unique ID  
        ''' </summary>
        ''' <param name="Id">The <see cref="T:System.Guid"/> the unique ID.</param>
        Sub New(ByVal id As Guid)
            Dim FreeText As Meanstream.Portal.Core.Entities.MeanstreamFreeText = Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.GetById(Id)
            If FreeText Is Nothing Then
                Throw New ApplicationException("FreeText Not Found")
            End If
            Me.CheckedOutBy = FreeText.CheckedOutBy
            Me.ContentId = FreeText.ContentId
            Me.CreatedBy = FreeText.CreatedBy
            Me.CreatedDate = FreeText.CreatedDate
            Me.Id = FreeText.Id
            Me.IsLocked = FreeText.IsLocked
            Me.LastModifiedBy = FreeText.LastModifiedBy
            Me.LastModifiedDate = FreeText.LastModifiedDate
            Me.ModuleId = FreeText.ModuleId
            Me.ModuleVersionId = FreeText.ModuleVersionId
        End Sub

        ''' <summary>
        ''' Creates a new freetext widget module and saves the content to the data store   
        ''' </summary>
        ''' <param name="Title">The <see cref="T:System.String"/> Title of the content</param>
        ''' <param name="Author">The <see cref="T:System.String"/> Author of the content</param>
        ''' <param name="Text">The <see cref="T:System.String"/> body of the content</param>
        ''' <param name="CheckedOutBy">The <see cref="T:System.Guid"/> checks out the content be userId</param>
        ''' <param name="ModuleId">The <see cref="T:System.Integer"/> published widgetId content</param>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Integer"/> version widgetId content</param>
        ''' <param name="IsLocked">The <see cref="T:System.Boolean"/> locks the content</param>
        Public Shared Function Create(ByVal Title As String, _
                                      ByVal Author As String, _
                                      ByVal Text As String, _
                                      ByVal CheckedOutBy As Guid, _
                                      ByVal ModuleId As Guid, _
                                      ByVal ModuleVersionId As Guid, _
                                      ByVal IsLocked As Boolean) As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule

            If ModuleId = Nothing And ModuleVersionId = Nothing Then
                Throw New ApplicationException("Both ModuleId and ModuleVersionId cannot be 0")
            End If

            Dim FreeText As Meanstream.Portal.Core.Entities.MeanstreamFreeText = New Meanstream.Portal.Core.Entities.MeanstreamFreeText
            FreeText.CheckedOutBy = CheckedOutBy
            FreeText.CreatedDate = Date.Now
            FreeText.IsLocked = IsLocked
            FreeText.Id = Guid.NewGuid

            If System.Web.HttpContext.Current.Request.IsAuthenticated Then
                FreeText.CreatedBy = Membership.MembershipService.Current.GetUserGuid(System.Web.HttpContext.Current.Profile.UserName)
                FreeText.LastModifiedBy = FreeText.CreatedBy
            Else
                FreeText.CreatedBy = New Guid(System.Web.HttpContext.Current.Request.AnonymousID)
                FreeText.LastModifiedBy = FreeText.CreatedBy
            End If

            FreeText.LastModifiedDate = Date.Now
            FreeText.ModuleId = ModuleId
            FreeText.ModuleVersionId = ModuleVersionId

            Dim ContentTypes As List(Of Meanstream.Portal.Core.Content.ContentType) = Meanstream.Portal.Core.Content.ContentType.GetContentTypes()
            Dim ContentType As Meanstream.Portal.Core.Content.ContentType = Nothing
            For Each Type As Meanstream.Portal.Core.Content.ContentType In ContentTypes
                If Type.Type = "Free Text" Then
                    ContentType = New Meanstream.Portal.Core.Content.ContentType
                    ContentType.Type = Type.Type
                End If
            Next

            If ContentType Is Nothing Then
                Throw New ApplicationException("FreeText ContentType is missing in the database")
            End If

            Dim Content As Meanstream.Portal.Core.Content.Content = Meanstream.Portal.Core.Content.Content.Create(Title, Author, ContentType, Text)
            If Content IsNot Nothing Then
                FreeText.ContentId = Content.Id
                If Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.Insert(FreeText) Then
                    Return Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.GetById(FreeText.Id)
                End If
            Else
                Throw New Exception("There was an error creating FreeText Content")
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Retrieves the custom widget module from the data store by unique ID 
        ''' </summary>
        ''' <param name="Id">The <see cref="T:System.Guid"/> the unique ID.</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.FreeText.WidgetModule"/> widget module</returns>
        Public Shared Function GetById(ByVal id As Guid) As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule
            Dim FreeTextBase As Meanstream.Portal.Core.Entities.MeanstreamFreeText = Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.GetById(Id)
            If FreeTextBase Is Nothing Then
                Return Nothing
            End If

            Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = New Meanstream.Portal.Core.Widgets.FreeText.WidgetModule
            FreeText.CheckedOutBy = FreeTextBase.CheckedOutBy
            FreeText.ContentId = FreeTextBase.ContentId
            FreeText.CreatedBy = FreeTextBase.CreatedBy
            FreeText.CreatedDate = FreeTextBase.CreatedDate
            FreeText.Id = FreeTextBase.Id
            FreeText.IsLocked = FreeTextBase.IsLocked
            FreeText.LastModifiedBy = FreeTextBase.LastModifiedBy
            FreeText.LastModifiedDate = FreeTextBase.LastModifiedDate
            FreeText.ModuleId = FreeTextBase.ModuleId
            FreeText.ModuleVersionId = FreeTextBase.ModuleVersionId

            Return FreeText
        End Function

        ''' <summary>
        ''' Retrieves the published custom widget module from the data store by ModuleId (WidgetId)
        ''' </summary>
        ''' <param name="ModuleId">The <see cref="T:System.Guid"/> the new WidgetId.</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.FreeText.WidgetModule"/> widget module</returns>
        Public Shared Function GetByModuleId(ByVal moduleId As Guid) As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule
            Dim FreeTextBase As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamFreeText) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.Find("ModuleId=" & moduleId.ToString)
            If FreeTextBase.Count = 0 Then
                Return Nothing
            End If

            Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = New Meanstream.Portal.Core.Widgets.FreeText.WidgetModule
            FreeText.CheckedOutBy = FreeTextBase(0).CheckedOutBy
            FreeText.ContentId = FreeTextBase(0).ContentId
            FreeText.CreatedBy = FreeTextBase(0).CreatedBy
            FreeText.CreatedDate = FreeTextBase(0).CreatedDate
            FreeText.Id = FreeTextBase(0).Id
            FreeText.IsLocked = FreeTextBase(0).IsLocked
            FreeText.LastModifiedBy = FreeTextBase(0).LastModifiedBy
            FreeText.LastModifiedDate = FreeTextBase(0).LastModifiedDate
            FreeText.ModuleId = FreeTextBase(0).ModuleId
            FreeText.ModuleVersionId = FreeTextBase(0).ModuleVersionId

            Return FreeText
        End Function

        ''' <summary>
        ''' Retrieves the custom version of a widget module from the data store by ModuleVersionId (WidgetId)
        ''' </summary>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Guid"/> the version WidgetId.</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.FreeText.WidgetModule"/> widget module</returns>
        Public Shared Function GetByModuleVersionId(ByVal ModuleVersionId As Guid) As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule
            Dim FreeTextBase As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamFreeText) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.Find("ModuleVersionId=" & ModuleVersionId.ToString)
            If FreeTextBase.Count = 0 Then
                Return Nothing
            End If

            Dim FreeText As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = New Meanstream.Portal.Core.Widgets.FreeText.WidgetModule
            FreeText.CheckedOutBy = FreeTextBase(0).CheckedOutBy
            FreeText.ContentId = FreeTextBase(0).ContentId
            FreeText.CreatedBy = FreeTextBase(0).CreatedBy
            FreeText.CreatedDate = FreeTextBase(0).CreatedDate
            FreeText.Id = FreeTextBase(0).Id
            FreeText.IsLocked = FreeTextBase(0).IsLocked
            FreeText.LastModifiedBy = FreeTextBase(0).LastModifiedBy
            FreeText.LastModifiedDate = FreeTextBase(0).LastModifiedDate
            FreeText.ModuleId = FreeTextBase(0).ModuleId
            FreeText.ModuleVersionId = FreeTextBase(0).ModuleVersionId

            Return FreeText
        End Function

        Public Shared Function GetContentType() As Meanstream.Portal.Core.Entities.MeanstreamContentType
            Return Meanstream.Portal.Core.Data.DataRepository.MeanstreamContentTypeProvider.GetByType("Free Text")
        End Function

        ''' <summary>
        ''' Implements <see cref="E:Meanstream.Portal.Core.WidgetFramework.IModuleDTO.Save"/> Saves the widget's custom content to the data store.
        ''' </summary>
        Public Sub Save() Implements WidgetFramework.IModuleDTO.Save
            If System.Web.HttpContext.Current.Request.IsAuthenticated Then
                Me.LastModifiedBy = Membership.MembershipService.Current.GetUserGuid(System.Web.HttpContext.Current.Profile.UserName)
            Else
                Me.LastModifiedBy = New Guid(System.Web.HttpContext.Current.Request.AnonymousID)
            End If

            Me.LastModifiedDate = Date.Now

            Me.Content.Save()

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.Update(Me) Then
                Dim Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = WidgetFramework.WidgetService.Current.GetWidgetVersionById(Me.ModuleVersionId)
                Dim manager As New WidgetFramework.WidgetVersionManager(Widget)
                manager.Save()
            End If
        End Sub

        ''' <summary>
        ''' Implements <see cref="E:Meanstream.Portal.Core.WidgetFramework.IModuleDTO.Delete"/> interface. Deletes the widget's custom content from the data store during widget deletion.
        ''' </summary>
        Public Sub Delete() Implements WidgetFramework.IModuleDTO.Delete
            Meanstream.Portal.Core.Content.Content.GetContent(Me.ContentId).Delete()
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamFreeTextProvider.Delete(Me.Id)
        End Sub
    End Class
End Namespace

