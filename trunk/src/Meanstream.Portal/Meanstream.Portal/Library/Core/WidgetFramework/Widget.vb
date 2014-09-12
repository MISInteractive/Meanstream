
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class Widget
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Widget)

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

        Private _userControl As New WidgetControl
        Public Property UserControl() As WidgetControl
            Get
                Return _userControl
            End Get
            Set(ByVal value As WidgetControl)
                _userControl = value
            End Set
        End Property

        Private _moduleDefId As Guid
        Public Property ModuleDefId() As Guid
            Get
                Return _moduleDefId
            End Get
            Set(ByVal value As Guid)
                _moduleDefId = value
            End Set
        End Property

        Private _pageId As Guid
        Public Property PageId() As Guid
            Get
                Return _pageId
            End Get
            Set(ByVal value As Guid)
                _pageId = value
            End Set
        End Property

        Private _skinPaneId As Guid
        Public Property SkinPaneId() As Guid
            Get
                Return _skinPaneId
            End Get
            Set(ByVal value As Guid)
                _skinPaneId = value
            End Set
        End Property

        Private _createdBy As String
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property

        Private _title As String
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
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

        Private _displayOrder As Integer
        Public Property DisplayOrder() As Integer
            Get
                Return _displayOrder
            End Get
            Set(ByVal value As Integer)
                _displayOrder = value
            End Set
        End Property

        Private _allPages As Boolean
        Public Property AllPages() As Boolean
            Get
                Return _allPages
            End Get
            Set(ByVal value As Boolean)
                _allPages = value
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

        Private _permissions As List(Of WidgetPermission) = Nothing
        Public ReadOnly Property Permissions() As List(Of WidgetPermission)
            Get
                If _permissions Is Nothing Then
                    _permissions = WidgetService.Current.GetPermissions(Me.Id)
                End If
                Return _permissions
            End Get
        End Property

#End Region

        Public Overloads Function Equals(ByVal other As Widget) As Boolean Implements System.IEquatable(Of Widget).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

