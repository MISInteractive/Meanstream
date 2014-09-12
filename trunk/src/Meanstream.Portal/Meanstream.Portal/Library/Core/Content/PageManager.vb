Imports Meanstream.Portal.Core.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Content
    Public Class PageManager
        Inherits AttributeEntityManager

        Private _entity As Page

        Sub New(ByRef attributeEntity As Page)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Protected Friend Sub LoadFromDatasource()
            Dim page As Meanstream.Portal.Core.Entities.MeanstreamPage = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.GetById(_entity.Id)
            If page Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page {0} cannot be located in database.", _entity.Id))
            End If
            Me.Bind(page)
        End Sub

        Protected Friend Sub Bind(ByVal page As Meanstream.Portal.Core.Entities.MeanstreamPage)
            _entity.MetaTitle = page.Title
            _entity.Name = page.Name
            _entity.MetaDescription = page.Description
            _entity.MetaKeywords = page.KeyWords
            '_entity.Skin
            _entity.ParentId = page.ParentId
            _entity.DisableLink = page.DisableLink
            _entity.IsDeleted = page.IsDeleted
            _entity.DisplayOrder = page.DisplayOrder
            _entity.IsVisible = page.IsVisible
            _entity.Url = page.Url.Trim
            _entity.Type = page.Type
            _entity.IsPublished = page.IsPublished
            _entity.IsHomePage = page.IsHome
            _entity.Author = page.Author
            _entity.PortalId = page.PortalId
            _entity.StartDate = page.StartDate
            _entity.EndDate = page.EndDate
            _entity.EnableCaching = page.EnableCaching
            _entity.EnableViewState = page.EnableViewState
            _entity.Index = page.Index
            _entity.VersionId = page.VersionId
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("page id cannot be null.")
            End If
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'custom code here
            
            Dim PageVersions As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPageVersion) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageVersionProvider.Find("PageId=" & _entity.Id.ToString)

            If PageVersions.Count = 0 Then
                Throw New Exception("Versions of this page do not exist")
            End If

            Dim PageModules As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModule) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Find("PageId=" & _entity.Id.ToString)

            For Each PageVersion As Meanstream.Portal.Core.Entities.MeanstreamPageVersion In PageVersions

                Try
                    Dim version As New PageVersion(PageVersion.Id)
                    Dim manager As New PageVersionManager(version)
                    manager.Delete()

                Catch ex As Exception

                    Throw New Exception(ex.Message)
                End Try
            Next

            Try
                'Delete page premissions

                For Each Permission As PagePermission In _entity.Permissions
                    Dim manager As New PagePermissionManager(Permission)
                    manager.Delete()

                Next

                'Delete Published Modules
                For Each Modules As Core.WidgetFramework.Widget In _entity.Widgets
                    Dim manager As New Core.WidgetFramework.WidgetManager(Modules)
                    manager.Delete()
                Next

                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Delete(_entity.Id)

                Meanstream.Portal.Core.Utilities.CacheUtility.RefreshPages()
                Meanstream.Portal.Core.Utilities.CacheUtility.RefreshMenu()
                Meanstream.Portal.Core.Utilities.CacheUtility.RefreshSecurity()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamPage = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.GetById(_entity.Id)

            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the page {0} cannot be located in database.", _entity.Id))
            End If

            'Persist page info here
            entity.Title = _entity.MetaTitle
            entity.Name = _entity.Name
            entity.Description = _entity.MetaDescription
            entity.KeyWords = _entity.MetaKeywords
            entity.SkinId = _entity.Skin.Id
            entity.ParentId = _entity.ParentId
            entity.DisableLink = _entity.DisableLink
            entity.IsDeleted = _entity.IsDeleted
            entity.DisplayOrder = _entity.DisplayOrder
            entity.IsVisible = _entity.IsVisible
            entity.Url = _entity.Url.Trim
            entity.Type = _entity.Type
            'entity.IsPublished = _entity.IsPublished
            entity.IsHome = _entity.IsHomePage
            entity.Author = _entity.Author
            entity.PortalId = _entity.PortalId
            entity.StartDate = _entity.StartDate
            entity.EndDate = _entity.EndDate
            entity.EnableCaching = _entity.EnableCaching
            entity.EnableViewState = _entity.EnableViewState
            entity.Index = _entity.Index
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Update(entity)

            'update permissions

            Meanstream.Portal.Core.Utilities.CacheUtility.RemovePage(_entity)

            If ApplicationMessagingManager.Enabled Then
                ApplicationMessagingManager.Current.FirePortalContextChangedMessageEvent(New ApplicationMessage("Notify the PortalContextModule that the Page was Saved on portal: " & _entity.PortalId.ToString, ApplicationMessageType.PageSaved))
            End If
        End Sub

        Public Sub SetAsHomePage()
            Dim Pages As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamPage) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Find("PortalId=" & _entity.PortalId.ToString & " AND IsHome=True")
            For Each Page As Meanstream.Portal.Core.Entities.MeanstreamPage In Pages
                Page.IsHome = False
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamPageProvider.Update(Page)
            Next
            _entity.IsHomePage = True
            Me.Save()
            'clear application cache
            Meanstream.Portal.Core.Utilities.CacheUtility.ClearApplicationCache()
        End Sub

        Public Sub SendToRecycleBin()
            PortalTrace.WriteLine([String].Concat("SendToRecycleBin() PageId=" & _entity.Id.ToString))
            If _entity.IsHomePage Then
                Exit Sub
            End If
            _entity.IsDeleted = True
            _entity.EndDate = Date.Now
            Me.Save()

            Meanstream.Portal.Core.Utilities.CacheUtility.RefreshPages()
            Meanstream.Portal.Core.Utilities.CacheUtility.RefreshMenu()
            Meanstream.Portal.Core.Utilities.CacheUtility.RefreshSecurity()
        End Sub

        Public Sub Restore()
            PortalTrace.WriteLine([String].Concat("Restore() PageId=" & _entity.Id.ToString))
            _entity.IsDeleted = False
            Me.Save()

            Meanstream.Portal.Core.Utilities.CacheUtility.RefreshPages()
            Meanstream.Portal.Core.Utilities.CacheUtility.RefreshMenu()
            Meanstream.Portal.Core.Utilities.CacheUtility.RefreshSecurity()
        End Sub
    End Class
End Namespace
