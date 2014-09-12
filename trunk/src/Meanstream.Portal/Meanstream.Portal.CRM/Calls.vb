
Namespace Meanstream.Portal.CRM
    Public Class Calls
        Inherits Meanstream.Portal.CRM.Extensibility.AttributeEntity
        Implements IEquatable(Of Calls)
        Implements ICampaignTask

        Sub New(ByVal id As Guid)
            MyBase.New(id)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid Implements ICampaignTask.Id
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

        Private _campaignId As Guid
        Public Property CampaignId() As Guid Implements ICampaignTask.CampaignId
            Get
                Return _campaignId
            End Get
            Set(ByVal value As Guid)
                _campaignId = value
            End Set
        End Property

        Private _participant As Participant
        Public Property Participant() As Participant
            Get
                Return _participant
            End Get
            Set(ByVal value As Participant)
                _participant = value
            End Set
        End Property

        Private _isExactTime As Boolean
        Public Property IsExactTime() As Boolean
            Get
                Return _isExactTime
            End Get
            Set(ByVal value As Boolean)
                _isExactTime = value
            End Set
        End Property

        Private _isCompleted As Boolean
        Public Property IsCompleted() As Boolean
            Get
                Return _isCompleted
            End Get
            Set(ByVal value As Boolean)
                _isCompleted = value
            End Set
        End Property

        Private _isCanceled As Boolean
        Public Property IsCanceled() As Boolean
            Get
                Return _isCanceled
            End Get
            Set(ByVal value As Boolean)
                _isCanceled = value
            End Set
        End Property

        Private _isAppointment As Boolean
        Public Property IsAppointment() As Boolean
            Get
                Return _isAppointment
            End Get
            Set(ByVal value As Boolean)
                _isAppointment = value
            End Set
        End Property

        Private _occurance As String
        Public Property Occurance() As String
            Get
                Return _occurance
            End Get
            Set(ByVal value As String)
                _occurance = value
            End Set
        End Property

        Private _callResult As String
        Public Property CallResult() As String
            Get
                Return _callResult
            End Get
            Set(ByVal value As String)
                _callResult = value
            End Set
        End Property

        Private _scheduleFollowUp As String
        Public Property ScheduleFollowUp() As String
            Get
                Return _scheduleFollowUp
            End Get
            Set(ByVal value As String)
                _scheduleFollowUp = value
            End Set
        End Property

        Private _summary As String
        Public Property Summary() As String
            Get
                Return _summary
            End Get
            Set(ByVal value As String)
                _summary = value
            End Set
        End Property

        Private _notes As String
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property

        Private _scheduledDateTime As Date
        Public Property ScheduledDateTime() As Date
            Get
                Return _scheduledDateTime
            End Get
            Set(ByVal value As Date)
                _scheduledDateTime = value
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

        Private _campaignStepId As Guid
        Public Property CampaignStepId() As Guid
            Get
                Return _campaignStepId
            End Get
            Set(ByVal value As Guid)
                _campaignStepId = value
            End Set
        End Property

        Private _referenceId As Guid
        Public Property ReferenceId() As Guid
            Get
                Return _referenceId
            End Get
            Set(ByVal value As Guid)
                _referenceId = value
            End Set
        End Property

        Private _phoneScript As String
        Public Property PhoneScript() As String
            Get
                Return _phoneScript
            End Get
            Set(ByVal value As String)
                _phoneScript = value
            End Set
        End Property

        Private _phoneScriptId As Guid
        Public Property PhoneScriptId() As Guid
            Get
                Return _phoneScriptId
            End Get
            Set(ByVal value As Guid)
                _phoneScriptId = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Calls) As Boolean Implements System.IEquatable(Of Calls).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

