
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetControl
        Inherits System.Web.UI.UserControl
        Implements IModule

#Region " Properties "
        Private _WidgetId As Guid
        Public Property WidgetId() As Guid
            Get
                Return _WidgetId
            End Get
            Set(ByVal value As Guid)
                _WidgetId = value
            End Set
        End Property

        Private _virtualPath As String
        Public Property VirtualPath() As String
            Get
                Return _virtualPath
            End Get
            Set(ByVal Value As String)
                _virtualPath = Value
            End Set
        End Property
#End Region

        Public Overridable Function GetIModuleDTO() As IModuleDTO Implements IModule.GetIModuleDTO
            Return Nothing
        End Function

        Public Overridable Sub OnWidgetDelete() Implements IModule.OnWidgetDelete
        End Sub
    End Class
End Namespace

