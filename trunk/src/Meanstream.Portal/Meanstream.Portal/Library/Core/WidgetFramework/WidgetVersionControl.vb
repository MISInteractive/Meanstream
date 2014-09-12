
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetVersionControl
        Inherits System.Web.UI.UserControl
        Implements System.Web.UI.INamingContainer
        Implements IModuleVersion

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

        Private _Content As System.Web.UI.Control
        Public Property Content() As System.Web.UI.Control
            Get
                Return _Content
            End Get
            Set(ByVal value As System.Web.UI.Control)
                _Content = value
            End Set
        End Property
#End Region

        Public Overridable Function GetIModuleDTO() As IModuleDTO Implements IModuleVersion.GetIModuleDTO
            Return Nothing
        End Function

        Public Overridable Sub OnAddToPage() Implements IModuleVersion.OnAddToPage
        End Sub

        Public Overridable Sub OnAddToPageCreate(ByVal widgetId As Guid) Implements IModuleVersion.OnAddToPageCreate
        End Sub

        Public Overridable Sub OnCopyAndAddFromVersion(ByVal widgetVersionId As Guid, ByVal widgetId As Guid) Implements IModuleVersion.OnCopyAndAddFromVersion
        End Sub

        Public Overridable Sub OnCreateVersionFromVersion(ByVal newWidgetId As Guid) Implements IModuleVersion.OnCreateVersionFromVersion
        End Sub

        Public Overridable Sub OnPublish(ByVal newWidgetId As Guid) Implements IModuleVersion.OnPublish
        End Sub

        Public Overridable Sub OnWidgetDelete() Implements IModuleVersion.OnWidgetDelete
        End Sub

        Public Overridable Sub OnWidgetUpdate() Implements IModuleVersion.OnWidgetUpdate
        End Sub

        Protected Overrides Sub CreateChildControls()
            MyBase.Controls.Clear()

            If Not ChildControlsCreated Then
                Controls.Add(Content)
            End If
        End Sub
    End Class
End Namespace

