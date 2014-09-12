Imports Meanstream.Portal.ComponentModel

Namespace Meanstream.Portal.Core.Services.PaymentGateway

    Public Enum SubscriptionStatus
        Active
        Canceled
        Expired
        Suspended
        Terminated
    End Enum

    Public Enum PaymentSchedule
        Weekly
        Monthly
        Annual
    End Enum

    Public MustInherit Class SubscriptionProvider
        Public Sub New()

        End Sub

        Public Shared Function Current() As SubscriptionProvider
            'initialize provider
            Return ComponentFactory.GetComponent(Of SubscriptionProvider)()
        End Function

        Public ReadOnly Property Settings() As Dictionary(Of String, String)
            Get
                Return ComponentFactory.GetComponentSettings(Me.GetType.FullName)
            End Get
        End Property

        Public MustOverride Function CreateSubscription(ByVal subscription As SubscriptionRequest) As SubscriptionRequest
        Public MustOverride Function CancelSubscription(ByVal subscriptionId As String) As Boolean
        Public MustOverride Function UpdateSubscription(ByVal subscription As SubscriptionRequest) As Boolean
        Public MustOverride Function GetSubscriptionStatus(ByVal subscriptionId As String) As SubscriptionStatus
    End Class
End Namespace

