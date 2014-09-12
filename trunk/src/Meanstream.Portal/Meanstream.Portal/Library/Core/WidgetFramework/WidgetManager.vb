Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.WidgetFramework
    Friend Class WidgetManager
        Inherits AttributeEntityManager

        Private _entity As Widget

        Sub New(ByRef attributeEntity As Widget)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("widget id cannot be null.")
            End If
        End Sub

        Protected Friend Sub LoadFromDatasource()
            Dim widgetModule As Meanstream.Portal.Core.Entities.MeanstreamModule = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.GetById(_entity.Id)
            If widgetModule Is Nothing Then
                Throw New InvalidOperationException(String.Format("the widget {0} cannot be located in database.", _entity.Id))
            End If
            Me.Bind(widgetModule)
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamModule)
            Dim ModuleControls As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleControls) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleControlsProvider.Find("ModuleDefId=" & entity.ModuleDefId.ToString)
            Dim ModuleControlsPublished As Meanstream.Portal.Core.Entities.MeanstreamModuleControls = ModuleControls.Find("ControlKey", "Published")
            _entity.UserControl = New System.Web.UI.UserControl().LoadControl(ModuleControlsPublished.ControlPath)
            _entity.UserControl.WidgetId = _entity.Id
            _entity.UserControl.VirtualPath = ModuleControlsPublished.ControlPath
            _entity.AllPages = entity.AllPages
            _entity.CreatedBy = entity.CreatedBy
            _entity.DisplayOrder = entity.DisplayOrder
            _entity.EndDate = entity.EndDate
            _entity.IsDeleted = entity.IsDeleted
            _entity.PageId = entity.PageId
            _entity.SkinPaneId = entity.SkinPaneId
            _entity.StartDate = entity.StartDate
            _entity.Title = entity.Title
            _entity.ModuleDefId = entity.ModuleDefId
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()

            For Each ModulePermission As Meanstream.Portal.Core.WidgetFramework.WidgetPermission In _entity.Permissions
                'delete module permissions
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModulePermissionProvider.Delete(ModulePermission.Id)
            Next

            'Delete widget
            _entity.UserControl.OnWidgetDelete()
            'Delete Module
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()
        End Sub
    End Class
End Namespace

