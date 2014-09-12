
Namespace Meanstream.Portal.Subscription
    Public Class Item
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Subscription.Item)

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

        Private _portalId As Guid
        Public Property PortalId() As Guid
            Get
                Return _portalId
            End Get
            Set(ByVal value As Guid)
                _portalId = value
            End Set
        End Property

        Private _name As String = ""
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _startPayment As String = ""
        Public Property StartPayment() As String
            Get
                Return _startPayment
            End Get
            Set(ByVal value As String)
                _startPayment = value
            End Set
        End Property

        Private _description As String = ""
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _price As Decimal = 0.0
        Public Property Price() As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
            End Set
        End Property

        Private _createdDate As Date
        Public Property CreatedDate() As Date
            Get
                Return _createdDate
            End Get
            Set(ByVal value As Date)
                _createdDate = value
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
#End Region

        Public Overloads Function Equals(ByVal other As Item) As Boolean Implements System.IEquatable(Of Item).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

