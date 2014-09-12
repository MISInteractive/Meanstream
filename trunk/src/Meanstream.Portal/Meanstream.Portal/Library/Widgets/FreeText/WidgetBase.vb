Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Widgets.FreeText
    Public Class WidgetBase
        Inherits Meanstream.Portal.Core.WidgetFramework.WidgetControl

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetBase.GetIModuleDTO"/> to retrieve a custom widget module.
        ''' </summary>
        Public Overrides Function GetIModuleDTO() As Meanstream.Portal.Core.WidgetFramework.IModuleDTO
            Return Meanstream.Portal.Core.Widgets.FreeText.WidgetModule.GetByModuleId(Me.WidgetId)
        End Function

        ''' <summary>
        ''' Raises the <see cref="E:Meanstream.Portal.Core.WidgetBase.OnWidgetDelete"/> called by the system during widget and page deletion.
        ''' </summary>
        Public Overrides Sub OnWidgetDelete()
            PortalTrace.WriteLine("Meanstream.Portal.Core.Widgets.FreeText.WidgetBase:OnWidgetDelete()")
            Try
                Dim Content As Meanstream.Portal.Core.Widgets.FreeText.WidgetModule = Me.GetIModuleDTO()
                Content.Delete()
            Catch ex As Exception
                PortalTrace.Fail("Meanstream.Portal.Core.Widgets.FreeText.WidgetBase:OnWidgetDelete() " & ex.Message, DisplayMethodInfo.FullSignature)
            End Try
        End Sub
    End Class
End Namespace

