
Namespace Meanstream.Portal.Core.WidgetFramework
    Public Class WidgetVersion
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of WidgetVersion)

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

        Private _userControl As New WidgetVersionControl
        Public Property UserControl() As WidgetVersionControl
            Get
                Return _userControl
            End Get
            Set(ByVal value As WidgetVersionControl)
                _userControl = value
            End Set
        End Property

        Private _pageVersionId As Guid
        Public Property PageVersionId() As Guid
            Get
                Return _pageVersionId
            End Get
            Set(ByVal value As Guid)
                _pageVersionId = value
            End Set
        End Property

        Private _sharedId As Guid
        Public Property SharedId() As Guid
            Get
                Return _sharedId
            End Get
            Set(ByVal value As Guid)
                _sharedId = value
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

        Private _lastModifiedBy As String
        Public Property LastModifiedBy() As String
            Get
                Return _lastModifiedBy
            End Get
            Set(ByVal value As String)
                _lastModifiedBy = value
            End Set
        End Property

        Private _deletedDate As Date
        Public Property DeletedDate() As Date
            Get
                Return _deletedDate
            End Get
            Set(ByVal value As Date)
                _deletedDate = value
            End Set
        End Property

        Private _lastModifiedDate As Date
        Public Property LastModifiedDate() As Date
            Get
                Return _lastModifiedDate
            End Get
            Set(ByVal value As Date)
                _lastModifiedDate = value
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

        Private _permissions As List(Of WidgetVersionPermission) = Nothing
        Public ReadOnly Property Permissions() As List(Of WidgetVersionPermission)
            Get
                If _permissions Is Nothing Then
                    _permissions = WidgetService.Current.GetVersionPermissions(Me.Id)
                End If
                Return _permissions
            End Get
        End Property

#End Region

        Public Overloads Function Equals(ByVal other As WidgetVersion) As Boolean Implements System.IEquatable(Of WidgetVersion).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

