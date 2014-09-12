
Namespace Meanstream.Portal.Core.Content
    Public Class Page
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Content.Page)

        Public Enum PageType
            INTERNAL = 1
            EXISTING = 2
            EXTERNAL = 3
        End Enum

        Sub New(ByVal id As Guid)
            MyBase.New(id)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _displayOrder As Integer
        Public Property DisplayOrder() As Integer
            Get
                Return _displayOrder
            End Get
            Set(ByVal value As Integer)
                _displayOrder = value
            End Set
        End Property

        Private _portalId As Guid
        Public Property PortalId() As Guid
            Get
                Return _portalId
            End Get
            Set(ByVal value As Guid)
                _portalId = value
            End Set
        End Property

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _isVisible As Boolean
        Public Property IsVisible() As Boolean
            Get
                Return _isVisible
            End Get
            Set(ByVal value As Boolean)
                _isVisible = value
            End Set
        End Property

        Private _parentId As Guid
        Public Property ParentId() As Guid
            Get
                Return _parentId
            End Get
            Set(ByVal value As Guid)
                _parentId = value
            End Set
        End Property

        Private _disableLink As Boolean
        Public Property DisableLink() As Boolean
            Get
                Return _disableLink
            End Get
            Set(ByVal value As Boolean)
                _disableLink = value
            End Set
        End Property

        Private _metaTitle As String
        Public Property MetaTitle() As String
            Get
                Return _metaTitle
            End Get
            Set(ByVal value As String)
                _metaTitle = value
            End Set
        End Property

        Private _metaDescription As String
        Public Property MetaDescription() As String
            Get
                Return _metaDescription
            End Get
            Set(ByVal value As String)
                _metaDescription = value
            End Set
        End Property

        Private _metaKeywords As String
        Public Property MetaKeywords() As String
            Get
                Return _metaKeywords
            End Get
            Set(ByVal value As String)
                _metaKeywords = value
            End Set
        End Property

        Private _isDeleted As Boolean
        Public Property IsDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal value As Boolean)
                _isDeleted = value
            End Set
        End Property

        Private _url As String
        Public Property Url() As String
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property

        Private _type As PageType
        Public Property Type() As PageType
            Get
                Return _type
            End Get
            Set(ByVal value As PageType)
                _type = value
            End Set
        End Property

        Private _startDate As Date
        Public Property StartDate() As Date
            Get
                Return _startDate
            End Get
            Set(ByVal value As Date)
                _startDate = value
            End Set
        End Property

        Private _endDate As Date
        Public Property EndDate() As Date
            Get
                Return _endDate
            End Get
            Set(ByVal value As Date)
                _endDate = value
            End Set
        End Property

        Private _publishDate As Date
        Public Property PublishDate() As Date
            Get
                Return _publishDate
            End Get
            Set(ByVal value As Date)
                _publishDate = value
            End Set
        End Property

        Private _isHomePage As Boolean
        Public Property IsHomePage() As Boolean
            Get
                Return _isHomePage
            End Get
            Set(ByVal value As Boolean)
                _isHomePage = value
            End Set
        End Property

        Private _isPublished As Boolean
        Public Property IsPublished() As Boolean
            Get
                Return _isPublished
            End Get
            Set(ByVal value As Boolean)
                _isPublished = value
            End Set
        End Property

        Private _versionId As Guid
        Public Property VersionId() As Guid
            Get
                Return _versionId
            End Get
            Set(ByVal value As Guid)
                _versionId = value
            End Set
        End Property

        Private _author As String
        Public Property Author() As String
            Get
                Return _author
            End Get
            Set(ByVal value As String)
                _author = value
            End Set
        End Property

        Private _enableCaching As Boolean
        Public Property EnableCaching() As Boolean
            Get
                Return _enableCaching
            End Get
            Set(ByVal value As Boolean)
                _enableCaching = value
            End Set
        End Property

        Private _enableViewState As Boolean
        Public Property EnableViewState() As Boolean
            Get
                Return _enableViewState
            End Get
            Set(ByVal value As Boolean)
                _enableViewState = value
            End Set
        End Property

        Private _index As Boolean
        Public Property Index() As Boolean
            Get
                Return _index
            End Get
            Set(ByVal value As Boolean)
                _index = value
            End Set
        End Property

        '********* Skin, permissions and widget can lazy load since their attributes cannot be changed **********'
        Private _skin As Skin
        Public ReadOnly Property Skin() As Skin
            Get
                If _skin Is Nothing Then
                    'get skin
                    _skin = ContentService.Current.GetSkinForPage(Me.Id)
                End If
                Return _skin
            End Get
        End Property

        Private _permissions As List(Of Meanstream.Portal.Core.Content.PagePermission) = Nothing
        Public ReadOnly Property Permissions() As List(Of Meanstream.Portal.Core.Content.PagePermission)
            Get
                If _permissions Is Nothing Then
                    _permissions = ContentService.Current.GetPagePermissions(Me.Id)
                End If
                Return _permissions
            End Get
        End Property

        Private _widgets As List(Of Meanstream.Portal.Core.WidgetFramework.Widget) = Nothing
        Public ReadOnly Property Widgets() As List(Of Meanstream.Portal.Core.WidgetFramework.Widget)
            Get
                'If _Widgets Is Nothing Then  *****IMPORTANT: comment this out so the all controls must be unload during caching
                _widgets = Meanstream.Portal.Core.WidgetFramework.WidgetService.Current.GetWidgetsByPageId(Me.Id)
                'End If
                Return _widgets
            End Get
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Page) As Boolean Implements System.IEquatable(Of Page).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class
End Namespace

