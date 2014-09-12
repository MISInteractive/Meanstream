
Namespace Meanstream.Portal.CRM
    Public Class Email
        Inherits Meanstream.Portal.CRM.Extensibility.AttributeEntity
        Implements IEquatable(Of Email)
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

        Private _isExactTime As Boolean = False
        Public Property IsExactTime() As Boolean
            Get
                Return _isExactTime
            End Get
            Set(ByVal value As Boolean)
                _isExactTime = value
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

        Private _isCanceled As Boolean = False
        Public Property IsCanceled() As Boolean
            Get
                Return _isCanceled
            End Get
            Set(ByVal value As Boolean)
                _isCanceled = value
            End Set
        End Property

        Private _occurance As String = ""
        Public Property Occurance() As String
            Get
                Return _occurance
            End Get
            Set(ByVal value As String)
                _occurance = value
            End Set
        End Property

        Private _purpose As String = ""
        Public Property Purpose() As String
            Get
                Return _purpose
            End Get
            Set(ByVal value As String)
                _purpose = value
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

        Private _lastSentDate As Date
        Public Property LastSentDate() As Date
            Get
                Return _lastSentDate
            End Get
            Set(ByVal value As Date)
                _lastSentDate = value
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

        Private _body As String = ""
        Public Property Body() As String
            Get
                Return _body
            End Get
            Set(ByVal value As String)
                _body = value
            End Set
        End Property

        Private _from As String = ""
        Public Property From() As String
            Get
                Return _from
            End Get
            Set(ByVal value As String)
                _from = value
            End Set
        End Property

        Private _subject As String = ""
        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        Private _signature As String = ""
        Public Property Signature() As String
            Get
                Return _signature
            End Get
            Set(ByVal value As String)
                _signature = value
            End Set
        End Property

        Private _storedEmailId As Guid
        Public Property StoredEmailId() As Guid
            Get
                Return _storedEmailId
            End Get
            Set(ByVal value As Guid)
                _storedEmailId = value
            End Set
        End Property

        Private _storedSignatureId As Guid
        Public Property StoredSignatureId() As Guid
            Get
                Return _storedSignatureId
            End Get
            Set(ByVal value As Guid)
                _storedSignatureId = value
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

        Public Overloads Function Equals(ByVal other As Email) As Boolean Implements System.IEquatable(Of Email).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

