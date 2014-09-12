Imports AuthorizeNet

Namespace Meanstream.Portal.PaymentGateway.Providers
    Public Class AuthorizeNetSubscriptionProvider
        Implements PaymentGateway.ISubscriptionProvider

        Public Function CancelSubscription(ByVal subscriptionId As String) As Boolean Implements ISubscriptionProvider.CancelSubscription
            Dim apiLogin As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.APILogin")
            Dim transactionKey As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.TransactionKey") '"4Ue62xFGr2L93vJ3"
            Dim serviceMode As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.ServiceMode") '"Test|Live"
            Dim mode As Integer = 0
            If serviceMode = "Live" Then
                mode = AuthorizeNet.ServiceMode.Live
            End If
            Dim gateway As New AuthorizeNet.SubscriptionGateway(apiLogin, transactionKey, mode)
            Return gateway.CancelSubscription(subscriptionId.ToString)
        End Function

        Public Function CreateSubscription(ByVal subscription As SubscriptionRequest) As SubscriptionRequest Implements ISubscriptionProvider.CreateSubscription
            Dim apiLogin As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.APILogin")
            Dim transactionKey As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.TransactionKey") '"4Ue62xFGr2L93vJ3"
            Dim serviceMode As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.ServiceMode") '"Test|Live"

            Dim mode As Integer = 0
            If serviceMode = "Live" Then
                mode = AuthorizeNet.ServiceMode.Live
            End If
            Dim gateway As New AuthorizeNet.SubscriptionGateway(apiLogin, transactionKey, mode)

            Dim address As New AuthorizeNet.Address
            address.City = subscription.City
            address.Company = subscription.Company
            address.Country = subscription.Country
            address.Fax = subscription.Fax
            address.Phone = subscription.Phone
            address.Zip = subscription.Zip
            address.ID = subscription.BillingAddressId
            address.State = subscription.StateOrProvince
            address.Street = subscription.Address & " " & subscription.Address2
            address.First = subscription.First
            address.Last = subscription.Last

            Dim request As AuthorizeNet.SubscriptionRequest = Nothing

            If subscription.PaymentSchedule = Meanstream.Portal.PaymentGateway.PaymentSchedule.Monthly Then
                request = AuthorizeNet.SubscriptionRequest.CreateMonthly(subscription.Email, subscription.Name, subscription.Amount)
            ElseIf subscription.PaymentSchedule = Meanstream.Portal.PaymentGateway.PaymentSchedule.Weekly Then
                request = AuthorizeNet.SubscriptionRequest.CreateWeekly(subscription.Email, subscription.Name, subscription.Amount)
            ElseIf subscription.PaymentSchedule = Meanstream.Portal.PaymentGateway.PaymentSchedule.Annual Then
                request = AuthorizeNet.SubscriptionRequest.CreateAnnual(subscription.Email, subscription.Name, subscription.Amount)
            End If

            request.BillingAddress = address
            request.CardCode = subscription.CCV
            request.CardExpirationMonth = subscription.CreditCardExpirationDate.Month
            request.CardExpirationYear = subscription.CreditCardExpirationDate.Year
            request.CardNumber = subscription.CreditCardNumber
            request.SubscriptionID = subscription.Id.ToString
            'subscription.BillingIntervalUnits = AuthorizeNet.BillingIntervalUnits.Months
            'subscription.BillingCycles = 12
            'subscription.BillingInterval = 1
            request.StartsOn = subscription.StartsOn

            request = gateway.CreateSubscription(request)

            subscription.Id = request.SubscriptionID
            Return subscription
        End Function

        Public Function GetSubscriptionStatus(ByVal subscriptionId As String) As SubscriptionStatus Implements ISubscriptionProvider.GetSubscriptionStatus
            Dim apiLogin As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.APILogin")
            Dim transactionKey As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.TransactionKey") '"4Ue62xFGr2L93vJ3"
            Dim serviceMode As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.ServiceMode") '"Test|Live"

            Dim mode As Integer = 0
            If serviceMode = "Live" Then
                mode = AuthorizeNet.ServiceMode.Live
            End If
            Dim gateway As New AuthorizeNet.SubscriptionGateway(apiLogin, transactionKey, mode)

            Dim status As PaymentGateway.SubscriptionStatus
            Dim response As AuthorizeNet.APICore.ARBSubscriptionStatusEnum = gateway.GetSubscriptionStatus(subscriptionId.ToString)
            If response = APICore.ARBSubscriptionStatusEnum.active Then
                status = SubscriptionStatus.Active
            ElseIf response = APICore.ARBSubscriptionStatusEnum.canceled Then
                status = SubscriptionStatus.Canceled
            ElseIf response = APICore.ARBSubscriptionStatusEnum.expired Then
                status = SubscriptionStatus.Expired
            ElseIf status = SubscriptionStatus.Suspended Then
                status = SubscriptionStatus.Suspended
            ElseIf status = SubscriptionStatus.Terminated Then
                status = SubscriptionStatus.Terminated
            End If
            Return status
        End Function

        Public Function UpdateSubscription(ByVal subscription As SubscriptionRequest) As Boolean Implements ISubscriptionProvider.UpdateSubscription
            Dim apiLogin As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.APILogin")
            Dim transactionKey As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.TransactionKey") '"4Ue62xFGr2L93vJ3"
            Dim serviceMode As String = Configuration.ConfigurationManager.AppSettings("Meanstream.PaymentGateway.ServiceMode") '"Test|Live"

            Dim mode As Integer = 0
            If serviceMode = "Live" Then
                mode = AuthorizeNet.ServiceMode.Live
            End If
            Dim gateway As New AuthorizeNet.SubscriptionGateway(apiLogin, transactionKey, mode)

            Dim address As New AuthorizeNet.Address
            address.City = subscription.City
            address.Company = subscription.Company
            address.Country = subscription.Country
            address.Fax = subscription.Fax
            address.Phone = subscription.Phone
            address.Zip = subscription.Zip
            address.ID = subscription.BillingAddressId
            address.State = subscription.StateOrProvince
            address.Street = subscription.Address & " " & subscription.Address2
            address.First = subscription.First
            address.Last = subscription.Last

            Dim request As AuthorizeNet.SubscriptionRequest = Nothing

            If subscription.PaymentSchedule = Meanstream.Portal.PaymentGateway.PaymentSchedule.Monthly Then
                request = AuthorizeNet.SubscriptionRequest.CreateMonthly(subscription.Email, subscription.Name, subscription.Amount)
            ElseIf subscription.PaymentSchedule = Meanstream.Portal.PaymentGateway.PaymentSchedule.Weekly Then
                request = AuthorizeNet.SubscriptionRequest.CreateWeekly(subscription.Email, subscription.Name, subscription.Amount)
            ElseIf subscription.PaymentSchedule = Meanstream.Portal.PaymentGateway.PaymentSchedule.Annual Then
                request = AuthorizeNet.SubscriptionRequest.CreateAnnual(subscription.Email, subscription.Name, subscription.Amount)
            End If

            request.BillingAddress = address
            request.CardCode = subscription.CCV
            request.CardExpirationMonth = subscription.CreditCardExpirationDate.Month
            request.CardExpirationYear = subscription.CreditCardExpirationDate.Year
            request.CardNumber = subscription.CreditCardNumber
            request.SubscriptionID = subscription.Id.ToString
            'subscription.BillingIntervalUnits = AuthorizeNet.BillingIntervalUnits.Months
            'subscription.BillingCycles = 12
            'subscription.BillingInterval = 1
            request.StartsOn = subscription.StartsOn

            Return gateway.UpdateSubscription(request)
        End Function
    End Class
End Namespace

