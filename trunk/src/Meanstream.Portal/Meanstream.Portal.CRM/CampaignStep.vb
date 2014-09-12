
Namespace Meanstream.Portal.CRM
    Public Class CampaignStep
        Implements IEquatable(Of CampaignStep)

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

        Private _campaignId As Guid
        Public Property CampaignId() As Guid
            Get
                Return _campaignId
            End Get
            Set(ByVal value As Guid)
                _campaignId = value
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

        Private _task As ICampaignTask
        Public Property Task() As ICampaignTask
            Get
                Return _task
            End Get
            Set(ByVal value As ICampaignTask)
                _task = value
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

        Private _referenceId As Guid
        Public Property ReferenceId() As Guid
            Get
                Return _referenceId
            End Get
            Set(ByVal value As Guid)
                _referenceId = value
            End Set
        End Property

        Private _status As String = ""
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        Private _daysAfter As Integer
        Public Property DaysAfter() As Integer
            Get
                Return _daysAfter
            End Get
            Set(ByVal value As Integer)
                _daysAfter = value
            End Set
        End Property

#End Region

        Public Overloads Function Equals(ByVal other As CampaignStep) As Boolean Implements System.IEquatable(Of CampaignStep).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

