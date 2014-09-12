
Namespace Meanstream.Portal.CRM
    Public Class CampaignParticipant
        Implements IEquatable(Of CampaignParticipant)

        Sub New(ByVal id As Guid)
            _id = id
        End Sub

#Region " Properties "
        Private Shared _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private Shared _participant As Participant
        Public Property Participant() As Participant
            Get
                Return _participant
            End Get
            Set(ByVal value As Participant)
                _participant = value
            End Set
        End Property

        Private Shared _campaignId As Guid
        Public Property CampaignId() As Guid
            Get
                Return _campaignId
            End Get
            Set(ByVal value As Guid)
                _campaignId = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As CampaignParticipant) As Boolean Implements System.IEquatable(Of CampaignParticipant).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

