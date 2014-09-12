Imports Meanstream.Portal.Core.Extensibility
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetVersionManager
        Inherits AttributeEntityManager

        Private _entity As WidgetVersion

        Sub New(ByRef attributeEntity As WidgetVersion)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("widget version id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion
            Dim widgetModule As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.GetById(_entity.Id)
            If widgetModule Is Nothing Then
                Throw New InvalidOperationException(String.Format("the widget version {0} cannot be located in database.", _entity.Id))
            End If
            Return widgetModule
        End Function

        Protected Friend Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion)
            Dim ModuleControls As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamModuleControls) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleControlsProvider.Find("ModuleDefId=" & entity.ModuleDefId.ToString)
            Dim ModuleControlsPublished As Meanstream.Portal.Core.Entities.MeanstreamModuleControls = ModuleControls.Find("ControlKey", "Version")
            _entity.UserControl = New System.Web.UI.UserControl().LoadControl(ModuleControlsPublished.ControlPath)
            _entity.UserControl.WidgetId = _entity.Id
            _entity.UserControl.VirtualPath = ModuleControlsPublished.ControlPath
            _entity.AllPages = entity.AllPages
            _entity.CreatedBy = entity.CreatedBy
            _entity.DisplayOrder = entity.DisplayOrder
            _entity.EndDate = entity.EndDate
            _entity.IsDeleted = entity.IsDeleted
            _entity.PageVersionId = entity.PageVersionId
            _entity.SkinPaneId = entity.SkinPaneId
            _entity.StartDate = entity.StartDate
            _entity.Title = entity.Title
            _entity.SharedId = entity.SharedId
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.LastModifiedBy = entity.LastModifiedBy
            Try
                If entity.DeletedDate IsNot Nothing And entity.DeletedDate.Trim <> "" Then
                    _entity.DeletedDate = Date.Parse(entity.DeletedDate)
                End If
            Catch ex As Exception
            End Try
            
            _entity.ModuleDefId = entity.ModuleDefId
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            PortalTrace.WriteLine([String].Concat("Delete() widgetId=" & _entity.Id.ToString))
            Try
                _entity.UserControl.OnWidgetDelete()
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Delete(_entity.Id)
                'delete permissions
                WidgetVersionPermissionManager.DeletePermissions(_entity.Id)
            Catch ex As Exception
                PortalTrace.Fail([String].Concat("Delete() widgetId=" & _entity.Id.ToString, " error=" & ex.Message), DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()
            PortalTrace.WriteLine([String].Concat("save() widgetId=" & _entity.Id.ToString))

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamModuleVersion = Me.GetEntityFromDatasource
            entity.AllPages = _entity.AllPages
            entity.CreatedBy = _entity.CreatedBy
            entity.DisplayOrder = _entity.DisplayOrder
            entity.EndDate = _entity.EndDate
            entity.IsDeleted = _entity.IsDeleted
            entity.PageVersionId = _entity.PageVersionId
            entity.SkinPaneId = _entity.SkinPaneId
            entity.StartDate = _entity.StartDate
            entity.Title = _entity.Title
            entity.SharedId = _entity.SharedId
            entity.LastModifiedDate = Date.Now
            entity.LastModifiedBy = System.Web.HttpContext.Current.Profile.UserName
            entity.DeletedDate = _entity.DeletedDate
            entity.ModuleDefId = _entity.ModuleDefId
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamModuleVersionProvider.Update(entity)

            'update permissions
            WidgetVersionPermissionManager.SavePermissions(_entity.Permissions)
        End Sub

        Public Sub Restore()
            PortalTrace.WriteLine([String].Concat("restore() widgetId=" & _entity.Id.ToString))
            'Remove PageContent Cache
            Meanstream.Portal.Core.Utilities.CacheUtility.Remove(Meanstream.Portal.Core.Utilities.CacheUtility.PAGE_CACHE & _entity.PageVersionId.ToString)
            _entity.IsDeleted = False
            _entity.DeletedDate = Nothing
            Me.Save()
        End Sub

        Public Sub SendToRecycleBin()
            PortalTrace.WriteLine([String].Concat("SendToRecycleBin() widgetId=" & _entity.Id.ToString))
            If _entity.AllPages = False Then
                _entity.IsDeleted = True
                _entity.DeletedDate = Date.Now
            End If
            Me.Save()
        End Sub

        Public Sub Move(ByVal SkinPaneId As Guid, ByVal DisplayOrder As Integer)
            PortalTrace.WriteLine([String].Concat("Move() widgetId=" & _entity.Id.ToString, ", SkinPaneId=" & SkinPaneId.ToString & ", DisplayOrder=" & DisplayOrder))
            'Update our position
            _entity.DisplayOrder = DisplayOrder
            _entity.SkinPaneId = SkinPaneId
            Me.Save()
        End Sub
    End Class
End Namespace

