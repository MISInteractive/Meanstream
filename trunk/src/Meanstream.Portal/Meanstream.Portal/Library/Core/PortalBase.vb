Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core
    Public Class PortalBase
        Inherits AttributeEntity

        Protected Friend Sub New(ByVal id As Guid, ByVal applicationId As Guid, _
                                 ByVal name As String, ByVal domain As String, _
                                 ByVal root As String, ByVal loginPageUrl As String, _
                                 ByVal homePageUrl As String, ByVal theme As String)
            MyBase.New(id)

            _id = id
            _applicationId = applicationId
            _domain = domain
            _homePageUrl = homePageUrl
            _loginUrl = loginPageUrl
            _name = name
            _root = root
            _theme = theme
        End Sub

        Protected Friend Sub New(ByVal id As Guid)
            MyBase.New(id)

        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _applicationId As Guid
        Public ReadOnly Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
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

        Private _domain As String
        Public Property Domain() As String
            Get
                Return _domain
            End Get
            Set(ByVal value As String)
                _domain = value
            End Set
        End Property

        Private _root As String
        ReadOnly Property Root() As String
            Get
                Return _root
            End Get
        End Property

        Private _loginUrl As String
        Public Property LoginUrl() As String
            Get
                Return _loginUrl
            End Get
            Set(ByVal value As String)
                _loginUrl = value
            End Set
        End Property

        Private _homePageUrl As String
        Public Property HomePageUrl() As String
            Get
                Return _homePageUrl
            End Get
            Set(ByVal value As String)
                _homePageUrl = value
            End Set
        End Property

        Private _theme As String
        ReadOnly Property Theme() As String
            Get
                Return _theme
            End Get
        End Property
#End Region
    End Class
End Namespace

