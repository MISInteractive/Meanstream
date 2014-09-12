Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Widgets.UserControl
    Public Class WidgetEditBase
        Inherits Meanstream.Portal.Web.UI.WidgetEditBase

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.GetIModuleDTO"/> to retrieve a custom widget module.
        ''' </summary>
        Public Overrides Function GetIModuleDTO() As Meanstream.Portal.Core.WidgetFramework.IModuleDTO
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:GetIModuleDTO()")
            Return Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.GetByModuleVersionId(Me.WidgetId)
        End Function

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnAddToPageCreate"/> called by the system during page creation.
        ''' </summary>
        ''' <param name="ModuleId">The <see cref="T:System.Integer"/> Widget's ModuleId (same as WidgetId).</param>
        Public Overrides Sub OnAddToPageCreate(ByVal ModuleId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnAddToPageCreate()")
            Try
                Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.Create("", "", ModuleId, Nothing, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnAddToPageCreate() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnAddToPage"/> called by the system when a widget is added to a page version.
        ''' </summary>
        Public Overrides Sub OnAddToPage()
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnAddToPage()")
            Try
                Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.Create("", "", Nothing, Me.WidgetId, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnAddToPage() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnCopyAndAddFromVersion"/> called by the system when a page is copied to create a new page.
        ''' </summary>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Guid"/> the new version WidgetId.</param>
        ''' <param name="ModuleId">The <see cref="T:System.Guid"/> the new WidgetId.</param>
        Public Overrides Sub OnCopyAndAddFromVersion(ByVal ModuleVersionId As Guid, ByVal ModuleId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnCopyAndAddFromVersion(): ModuleId=" & Me.WidgetId.ToString)
            Try
                'copy content from Module
                Dim CopyContent As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Me.GetIModuleDTO()
                'create published module
                Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.Create(CopyContent.Name, CopyContent.VirtualPath, ModuleId, Nothing, False)
                'create version of module
                Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.Create(CopyContent.Name, CopyContent.VirtualPath, Nothing, ModuleVersionId, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnCopyAndAddFromVersion() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnCreateVersionFromVersion"/> called by the system when a page is copied to create a new page during page publish.
        ''' </summary>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Integer"/> the new version WidgetId.</param>
        Public Overrides Sub OnCreateVersionFromVersion(ByVal ModuleVersionId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.Custom.WidgetEditBase:OnCreateVersionFromVersion(): ModuleVersionId=" & ModuleVersionId.ToString)
            Try
                Dim CopyContent As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Me.GetIModuleDTO()
                Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.Create(CopyContent.Name, CopyContent.VirtualPath, Nothing, ModuleVersionId, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnCreateVersionFromVersion() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnPublish"/> called by the system when a page is published.
        ''' </summary>
        ''' <param name="PublishedModuleId">The <see cref="T:System.Guid"/> the widget's new published moduleId (same as the new WidgetId).</param>
        Public Overrides Sub OnPublish(ByVal PublishedModuleId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnPublish(): PublishedModuleId=" & PublishedModuleId.ToString)
            Try
                Dim CopyContent As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Me.GetIModuleDTO()
                Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.Create(CopyContent.Name, CopyContent.VirtualPath, PublishedModuleId, Nothing, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnPublish() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnWidgetDelete"/> called by the system during widget and page deletion.
        ''' </summary>
        Public Overrides Sub OnWidgetDelete()
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.Custom.WidgetEditBase:OnWidgetDelete()")
            Try
                Dim Content As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = Me.GetIModuleDTO()
                Content.Delete()
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.UserControl.WidgetEditBase:OnWidgetDelete() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub
    End Class
End Namespace

