
Namespace Meanstream.Portal.CRM
    Public Class Appointment
        Inherits Meanstream.Portal.CRM.Extensibility.AttributeEntity
        Implements IEquatable(Of Appointment)
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

        Private _isCanceled As Boolean = False
        Public Property IsCanceled() As Boolean
            Get
                Return _isCanceled
            End Get
            Set(ByVal value As Boolean)
                _isCanceled = value
            End Set
        End Property

        Private _isCompleted As Boolean = False
        Public Property IsCompleted() As Boolean
            Get
                Return _isCompleted
            End Get
            Set(ByVal value As Boolean)
                _isCompleted = value
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

        Private _location As String = ""
        Public Property Location() As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property

        Private _notes As String = ""
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property

        Private _address1 As String = ""
        Public Property Address1() As String
            Get
                Return _address1
            End Get
            Set(ByVal value As String)
                _address1 = value
            End Set
        End Property

        Private _address2 As String = ""
        Public Property Address2() As String
            Get
                Return _address2
            End Get
            Set(ByVal value As String)
                _address2 = value
            End Set
        End Property

        Private _city As String = ""
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property

        Private _state As String = ""
        Public Property State() As String
            Get
                Return _state
            End Get
            Set(ByVal value As String)
                _state = value
            End Set
        End Property

        Private _zip As String = ""
        Public Property Zip() As String
            Get
                Return _zip
            End Get
            Set(ByVal value As String)
                _zip = value
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
#End Region

        Public Overloads Function Equals(ByVal other As Appointment) As Boolean Implements System.IEquatable(Of Appointment).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

