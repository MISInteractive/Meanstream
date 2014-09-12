
Namespace Meanstream.Portal.CRM
    Public Class ReminderDeliveryPreference

        Sub New(ByVal id As Guid)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _userId As Guid
        Public Property UserId() As Guid
            Get
                Return _userId
            End Get
            Set(ByVal value As Guid)
                _userId = value
            End Set
        End Property

        Private _deliveryAddress As String = ""
        Public Property DeliveryAddress() As String
            Get
                Return _deliveryAddress
            End Get
            Set(ByVal value As String)
                _deliveryAddress = value
            End Set
        End Property

        Private _provider As String = ""
        Public Property Provider() As String
            Get
                Return _provider
            End Get
            Set(ByVal value As String)
                _provider = value
            End Set
        End Property

        Private _type As ReminderDeliveryType
        Public Property Type() As ReminderDeliveryType
            Get
                Return _type
            End Get
            Set(ByVal value As ReminderDeliveryType)
                _type = value
            End Set
        End Property

        Private _enabled As Boolean = False
        Public Property Enabled() As Boolean
            Get
                Return _enabled
            End Get
            Set(ByVal value As Boolean)
                _enabled = value
            End Set
        End Property
#End Region


    End Class
End Namespace

