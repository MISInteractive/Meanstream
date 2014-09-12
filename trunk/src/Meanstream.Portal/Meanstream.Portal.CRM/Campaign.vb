
Namespace Meanstream.Portal.CRM
    Public Class Campaign
        Inherits Meanstream.Portal.CRM.Extensibility.AttributeEntity
        Implements IEquatable(Of Campaign)

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

        Private _accountId As Guid
        Public Property AccountId() As Guid
            Get
                Return _accountId
            End Get
            Set(ByVal value As Guid)
                _accountId = value
            End Set
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

        Private _steps As List(Of CampaignStep)
        Public Property Steps() As List(Of CampaignStep)
            Get
                Return _steps
            End Get
            Set(ByVal value As List(Of CampaignStep))
                _steps = value
            End Set
        End Property

        Private _participants As List(Of CampaignParticipant)
        Public Property Participants() As List(Of CampaignParticipant)
            Get
                Return _participants
            End Get
            Set(ByVal value As List(Of CampaignParticipant))
                _participants = value
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

        Private _summary As String = ""
        Public Property Summary() As String
            Get
                Return _summary
            End Get
            Set(ByVal value As String)
                _summary = value
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _isActive As Boolean
        Public Property IsActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

        Private _reminderDays As Integer
        Public Property ReminderDays() As Integer
            Get
                Return _reminderDays
            End Get
            Set(ByVal value As Integer)
                _reminderDays = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Campaign) As Boolean Implements System.IEquatable(Of Campaign).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

