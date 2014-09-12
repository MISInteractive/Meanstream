Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Widgets.FreeText
    Public Class WidgetEditBase
        Inherits Meanstream.Portal.Web.UI.WidgetEditBase

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.GetIModuleDTO"/> to retrieve a custom widget module.
        ''' </summary>
        Public Overrides Function GetIModuleDTO() As Meanstream.Portal.Core.WidgetFramework.IModuleDTO
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:GetIModuleDTO()")
            Return Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.GetByModuleVersionId(Me.WidgetId)
        End Function

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnAddToPageCreate"/> called by the system during page creation.
        ''' </summary>
        ''' <param name="ModuleId">The <see cref="T:System.Guid"/> Widget's ModuleId (same as WidgetId).</param>
        Public Overrides Sub OnAddToPageCreate(ByVal moduleId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnAddToPageCreate()")
            Try
                Dim Content As String = "Content Goes Here... <br><br><br>"
                Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.Create("My Content", "", Content, Nothing, moduleId, Nothing, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnAddToPageCreate() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnAddToPage"/> called by the system when a widget is added to a page version.
        ''' </summary>
        Public Overrides Sub OnAddToPage()
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnAddToPage()")
            Try
                Dim Content As String = "Content Goes Here... <br><br><br>"
                Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.Create("My Content", "", Content, Nothing, Nothing, Me.WidgetId, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnAddToPage() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnCopyAndAddFromVersion"/> called by the system when a page is copied to create a new page.
        ''' </summary>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Guid"/> the new version WidgetId.</param>
        ''' <param name="ModuleId">The <see cref="T:System.Guid"/> the new WidgetId.</param>
        Public Overrides Sub OnCopyAndAddFromVersion(ByVal moduleVersionId As Guid, ByVal moduleId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnCopyAndAddFromVersion(): ModuleId=" & Me.WidgetId.ToString)
            Try
                'copy content from Module
                Dim CopyContent As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Me.GetIModuleDTO()
                'published widget
                Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.Create("My Content", "", CopyContent.Content.Text, Nothing, moduleId, Nothing, False)
                'version widget
                Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.Create("My Content", "", CopyContent.Content.Text, Nothing, Nothing, moduleVersionId, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnCopyAndAddFromVersion() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnCreateVersionFromVersion"/> called by the system when a page is copied to create a new page during page publish.
        ''' </summary>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Guid"/> the new version WidgetId.</param>
        Public Overrides Sub OnCreateVersionFromVersion(ByVal moduleVersionId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnCreateVersionFromVersion(): ModuleVersionId=" & moduleVersionId.ToString)
            Try
                Dim CopyContent As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Me.GetIModuleDTO()
                Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.Create("My Content", "", CopyContent.Content.Text, Nothing, Nothing, moduleVersionId, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnCreateVersionFromVersion() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnPublish"/> called by the system when a page is published.
        ''' </summary>
        ''' <param name="PublishedModuleId">The <see cref="T:System.Guid"/> the widget's new published moduleId (same as the new WidgetId).</param>
        Public Overrides Sub OnPublish(ByVal publishedModuleId As Guid)
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnPublish(): PublishedModuleId=" & publishedModuleId.ToString)
            Try
                'copy content from Module
                Dim CopyContent As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Me.GetIModuleDTO()
                Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.Create("My Content", "", CopyContent.Content.Text, Nothing, publishedModuleId, Nothing, False)
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnPublish() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetFramework.WidgetVersionControl.OnWidgetDelete"/> called by the system during widget and page deletion.
        ''' </summary>
        Public Overrides Sub OnWidgetDelete()
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnWidgetDelete()")
            Try
                Dim Content As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Me.GetIModuleDTO()
                Content.Delete()
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetEditBase:OnWidgetDelete() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub
    End Class
End Namespace
