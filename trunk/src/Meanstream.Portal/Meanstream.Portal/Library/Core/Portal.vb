Imports Meanstream.Portal.Core.Messaging
Imports System.Net

Namespace Meanstream.Portal.Core
    Public NotInheritable Class Portal
        Inherits PortalBase
        Implements IEquatable(Of Meanstream.Portal.Core.Portal)

        Private Shared ReadOnly UserControlsFileName = "UserControls"
        Private Shared ReadOnly SkinsFileName = "Skins"
        Private Shared ReadOnly WidgetsFileName = "Widgets"
        Private Shared ReadOnly DocumentsFileName = "Documents"
        Private Shared ReadOnly ImagesDirectory = "Images"

        Protected Friend Sub New(ByVal id As Guid, ByVal applicationId As Guid, _
                                 ByVal name As String, ByVal domain As String, _
                                 ByVal root As String, ByVal loginPageUrl As String, _
                                 ByVal homePageUrl As String, ByVal theme As String)
            MyBase.New(id, applicationId, name, domain, root, loginPageUrl, homePageUrl, theme)
        End Sub

        Public Shared Function GetPortalByDomain(ByVal Domain As String) As Portal
            Dim Portal As Portal = PortalContext.GetPortalByDomain(Domain)
            Return Portal
        End Function

        'start crawler process here if needed


#Region " Properties "
        Public ReadOnly Property UserControlsPath() As String
            Get
                Return [String].Concat(PortalControlsRoot, UserControlsFileName)
            End Get
        End Property

        Public ReadOnly Property SkinsPath() As String
            Get
                Return [String].Concat(PortalControlsRoot, SkinsFileName)
            End Get
        End Property

        Public ReadOnly Property WidgetsPath() As String
            Get
                Return [String].Concat(PortalControlsRoot, WidgetsFileName)
            End Get
        End Property

        Public ReadOnly Property DocumentsPath() As String
            Get
                Return [String].Concat(PortalRoot, DocumentsFileName)
            End Get
        End Property

        Public ReadOnly Property ImagesPath() As String
            Get
                Return [String].Concat(PortalRoot, ImagesDirectory)
            End Get
        End Property

        Public ReadOnly Property ProfilesPath() As String
            Get
                Return [String].Concat(PortalRoot, "Profiles/")
            End Get
        End Property

        Public ReadOnly Property PortalRoot() As String
            Get
                Return [String].Concat(ApplicationRoot, "Portals/", Root & "/")
            End Get
        End Property

        Public ReadOnly Property ApplicationRoot() As String
            Get
                Return Meanstream.Portal.Core.AppConstants.APPLICATION_VIRTUAL_PATH
            End Get
        End Property

        Public ReadOnly Property ControlsRoot() As String
            Get
                Return [String].Concat(ApplicationRoot, "Controls/")
            End Get
        End Property

        Public ReadOnly Property PortalControlsRoot() As String
            Get
                Return [String].Concat(ControlsRoot, "Portals/", Root & "/")
            End Get
        End Property

        Private _urls As Dictionary(Of String, String)
        Public ReadOnly Property Urls() As Dictionary(Of String, String)
            Get
                Dim _urls As New Dictionary(Of String, String)
                PortalContext.PortalUrls.TryGetValue(Id, _urls)
                Return _urls
            End Get
        End Property

#End Region

        Public Overloads Function Equals(ByVal other As Portal) As Boolean Implements System.IEquatable(Of Portal).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

