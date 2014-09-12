
Namespace Meanstream.Portal.PaymentGateway
    Public Interface ISubscriptionProvider
        Function CreateSubscription(ByVal subscription As SubscriptionRequest) As SubscriptionRequest
        Function CancelSubscription(ByVal subscriptionId As String) As Boolean
        Function UpdateSubscription(ByVal subscription As SubscriptionRequest) As Boolean
        Function GetSubscriptionStatus(ByVal subscriptionId As String) As SubscriptionStatus
    End Interface

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
End Namespace

