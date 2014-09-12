Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Subscription
    Public Class SubscriptionService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As SubscriptionService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As SubscriptionService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName
                            _privateServiceInstance = New SubscriptionService(machineName, appFriendlyName)
                            _privateServiceInstance.Initialize()
                        End If
                    End SyncLock
                End If
                Return _privateServiceInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

        Private gateway As Meanstream.Portal.PaymentGateway.ISubscriptionProvider

#Region " Methods "
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(Core.AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The subscription service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            'get default provider
            gateway = New Meanstream.Portal.PaymentGateway.Providers.AuthorizeNetSubscriptionProvider 'get provider from settings....

            PortalTrace.WriteLine([String].Concat("Subscription Service initializing Meanstream.Portal.PaymentGateway.Providers.AuthorizeNetSubscriptionProvider as gateway provider: ", AppFriendlyName, " #", ApplicationId))
            PortalTrace.WriteLine([String].Concat("Subscription Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Subscription Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function GetSubscribers() As List(Of Subscriber)
            Dim list As New List(Of Subscriber)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionSubscriber) = Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.GetAll
            For Each entity As Subscription.Entities.MeanstreamSubscriptionSubscriber In entities
                Try
                    Dim obj As New Subscriber(entity.Id)
                    Dim manager As New SubscriberManager(obj)
                    manager.Bind(entity)
                    list.Add(obj)
                Catch ex As Exception
                End Try
            Next
            Return list
        End Function

        Public Function GetSubscribers(ByVal portalId As Guid) As List(Of Subscriber)
            Return Me.GetSubscribers(portalId.ToString, Entities.MeanstreamSubscriptionSubscriberColumn.PortalId)
        End Function

        Public Function GetMostRecentSubscribers(ByVal returnCount As Integer) As List(Of Subscriber)
            Dim list As New List(Of Subscriber)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionSubscriber) = Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.GetAll
            entities.Sort("SignupDate DESC")
            If entities.Count > returnCount Then
                entities = entities.GetRange(0, returnCount)
            End If
            For Each entity As Subscription.Entities.MeanstreamSubscriptionSubscriber In entities
                Try
                    Dim obj As New Subscriber(entity.Id)
                    Dim manager As New SubscriberManager(obj)
                    manager.Bind(entity)
                    list.Add(obj)
                Catch ex As Exception
                End Try
            Next
            Return list
        End Function

        Public Function GetSubscribers(ByVal value As String, ByVal column As Subscription.Entities.MeanstreamSubscriptionSubscriberColumn) As List(Of Subscriber)
            Dim list As New List(Of Subscriber)
            Dim query As New Subscription.Data.MeanstreamSubscriptionSubscriberQuery
            query.AppendEquals(column, value)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionSubscriber) = Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.Find(query.GetParameters)
            For Each entity As Subscription.Entities.MeanstreamSubscriptionSubscriber In entities
                Try
                    Dim obj As New Subscriber(entity.Id)
                    Dim manager As New SubscriberManager(obj)
                    manager.Bind(entity)
                    list.Add(obj)
                Catch ex As Exception
                End Try
            Next
            Return list
        End Function

        Public Function SearchSubscribers(ByVal Username As String) As List(Of Meanstream.Portal.Subscription.Subscriber)
            Dim Query As Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery = New Meanstream.Portal.Core.Data.VwAspnetMembershipUsersQuery
            Query.AppendLike(Meanstream.Portal.Core.Entities.VwAspnetMembershipUsersColumn.UserName, "%" & Username & "%")
            Dim users As Meanstream.Portal.Core.Entities.VList(Of Meanstream.Portal.Core.Entities.VwAspnetMembershipUsers) = Meanstream.Portal.Core.Data.DataRepository.VwAspnetMembershipUsersProvider.Find(Query.GetParameters)

            Dim list As New List(Of Meanstream.Portal.Subscription.Subscriber)
            If users.Count = 0 Then
                Return list
            End If

            Dim ids() As String = {}
            Dim idString As String = ""
            Dim index As Integer = 0
            For Each user As Meanstream.Portal.Core.Entities.VwAspnetMembershipUsers In users
                If index = 0 Then
                    idString = user.UserId.ToString
                Else
                    idString = idString & "," & user.UserId.ToString
                End If
                index = index + 1
            Next

            ids = idString.Split(",")

            If ids.Length = 0 Then
                Return list
            End If

            Dim query2 As New Meanstream.Portal.Subscription.Data.MeanstreamSubscriptionSubscriberQuery
            query2.AppendIn(Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberColumn.UserId, ids)

            Dim entities As Meanstream.Portal.Subscription.Entities.TList(Of Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber) = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.Find(query2.GetParameters)

            For Each entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber In entities
                Try
                    Dim obj As New Meanstream.Portal.Subscription.Subscriber(entity.Id)
                    Dim manager As New Meanstream.Portal.Subscription.SubscriberManager(obj)
                    manager.Bind(entity)
                    list.Add(obj)
                Catch ex As Exception
                End Try
            Next
            Return list
        End Function

        Public Function GetPayments(ByVal value As String, ByVal column As Subscription.Entities.MeanstreamSubscriptionPaymentColumn) As List(Of Payment)
            Dim list As New List(Of Payment)
            Dim query As New Subscription.Data.MeanstreamSubscriptionPaymentQuery
            query.AppendEquals(column, value)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionPayment) = Subscription.Data.DataRepository.MeanstreamSubscriptionPaymentProvider.Find(query.GetParameters)
            For Each entity As Subscription.Entities.MeanstreamSubscriptionPayment In entities
                Dim obj As New Payment(entity.Id)
                Dim manager As New PaymentManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Function GetPayments(ByVal subscriberId As Guid) As List(Of Payment)
            Return Me.GetPayments(subscriberId.ToString, Entities.MeanstreamSubscriptionPaymentColumn.SubscriberId)
        End Function

        Public Function GetPromoCodes(ByVal portalId As Guid) As List(Of PromoCode)
            Dim list As New List(Of PromoCode)
            Dim query As New Subscription.Data.MeanstreamSubscriptionPromoCodeQuery
            query.AppendEquals(Subscription.Entities.MeanstreamSubscriptionPromoCodeColumn.PortalId, portalId.ToString)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionPromoCode) = Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.Find(query.Parameters)
            For Each entity As Subscription.Entities.MeanstreamSubscriptionPromoCode In entities
                Dim obj As New PromoCode(entity.Code)
                Dim manager As New PromoCodeManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Function GetPromoCodes() As List(Of PromoCode)
            Dim list As New List(Of PromoCode)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionPromoCode) = Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.GetAll
            For Each entity As Subscription.Entities.MeanstreamSubscriptionPromoCode In entities
                Dim obj As New PromoCode(entity.Code)
                Dim manager As New PromoCodeManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Function GetItems(ByVal portalId As Guid) As List(Of Item)
            Return Me.GetItems(portalId.ToString, Entities.MeanstreamSubscriptionItemColumn.PortalId)
        End Function

        Public Function GetItems(ByVal value As String, ByVal column As Subscription.Entities.MeanstreamSubscriptionItemColumn) As List(Of Item)
            Dim list As New List(Of Item)
            Dim query As New Subscription.Data.MeanstreamSubscriptionItemQuery
            query.AppendEquals(column, value)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionItem) = Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.Find(query.GetParameters)
            For Each entity As Subscription.Entities.MeanstreamSubscriptionItem In entities
                Dim obj As New Item(entity.Id)
                Dim manager As New ItemManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Function GetItems() As List(Of Item)
            Dim list As New List(Of Item)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionItem) = Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.GetAll
            For Each entity As Subscription.Entities.MeanstreamSubscriptionItem In entities
                Dim obj As New Item(entity.Id)
                Dim manager As New ItemManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Function GetSubscriberItems(ByVal subscriberId As Guid) As List(Of Item)
            Dim list As New List(Of Item)
            Dim query As New Subscription.Data.VwMeanstreamSubscriptionSubscriberItemsQuery
            query.AppendEquals(Subscription.Entities.VwMeanstreamSubscriptionSubscriberItemsColumn.SubscriberId, subscriberId.ToString)
            Dim entities As Subscription.Entities.VList(Of Subscription.Entities.VwMeanstreamSubscriptionSubscriberItems) = Subscription.Data.DataRepository.VwMeanstreamSubscriptionSubscriberItemsProvider.Find(query.GetParameters)
            For Each entity As Subscription.Entities.VwMeanstreamSubscriptionSubscriberItems In entities
                Dim obj As New Item(entity.Id)
                Dim manager As New ItemManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Function GetBilling(ByVal subscriberId As Guid) As List(Of Billing)
            Dim list As New List(Of Billing)
            Dim query As New Subscription.Data.MeanstreamSubscriptionSubscriberBillingQuery
            query.AppendEquals(Subscription.Entities.MeanstreamSubscriptionSubscriberBillingColumn.SubscriberId, subscriberId.ToString)
            Dim entities As Subscription.Entities.TList(Of Subscription.Entities.MeanstreamSubscriptionSubscriberBilling) = Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.Find(query.GetParameters)
            For Each entity As Subscription.Entities.IMeanstreamSubscriptionSubscriberBilling In entities
                Dim obj As New Billing(entity.Id)
                Dim manager As New BillingManager(obj)
                manager.Bind(entity)
                list.Add(obj)
            Next
            Return list
        End Function

        Public Overridable Sub SubscribeBypassPaymentGateway(ByVal username As String, ByVal password As String, ByVal roles() As String, ByVal subscriber As Subscriber, _
                                                             ByVal items As List(Of Item), ByVal billing As Billing)

            'minimum required fields
            If items.Count = 0 Then
                Throw New CreateSubscriptionException("at least one subscription item is required")
            End If
            Dim Expression As New System.Text.RegularExpressions.Regex("\S+@\S+\.\S+")
            If Not Expression.IsMatch(subscriber.Email) Then
                Throw New CreateSubscriptionException("a valid email address is required")
            End If
            If subscriber.FirstName.Trim = "" Then
                Throw New CreateSubscriptionException("first name required")
            ElseIf subscriber.LastName.Trim = "" Then
                Throw New CreateSubscriptionException("last name required")
            ElseIf password.Trim = "" Then
                Throw New CreateSubscriptionException("password required")
            ElseIf subscriber.Id = Nothing Then
                Throw New CreateSubscriptionException("subscriber id required")
            ElseIf billing.Address.Trim = "" Then
                Throw New CreateSubscriptionException("billing address required")
            ElseIf billing.City.Trim = "" Then
                Throw New CreateSubscriptionException("billing city required")
            ElseIf billing.Zip.Trim = "" Then
                Throw New CreateSubscriptionException("billing zip required")
            ElseIf billing.StateOrProvince.Trim = "" Then
                Throw New CreateSubscriptionException("billing state or province required")
            ElseIf IsNothing(billing.Method) Then
                Throw New CreateSubscriptionException("billing payment method required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.CardHolderName.Trim = "" Then
                Throw New CreateSubscriptionException("billing card holder name required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.CreditCardNumber.Trim = "" Then
                Throw New CreateSubscriptionException("billing credit card number required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.ExpirationDate = Nothing Then
                Throw New CreateSubscriptionException("billing card holder name required")
            ElseIf IsNothing(subscriber.PaymentPlan) Then
                Throw New CreateSubscriptionException("subscriber payment plan required")
            ElseIf IsNothing(subscriber.Status) Then
                Throw New CreateSubscriptionException("subscriber status required")
            ElseIf subscriber.PortalId = Nothing Then
                Throw New CreateSubscriptionException("subscriber portal id required")
            ElseIf subscriber.SignupDate = Nothing Then
                Throw New CreateSubscriptionException("subscriber signup date required")
            End If

            If subscriber.PromoCode.Trim <> "" Then
                Dim code As New PromoCode(subscriber.PromoCode)
                Dim promoCodeManager As New PromoCodeManager(code)
                promoCodeManager.LoadFromDatasource()
                If code.Allocation = code.Used Then
                    Throw New CreateSubscriptionException("the promo code has exceeded its allocation")
                End If
            End If

            Dim status As System.Web.Security.MembershipCreateStatus = Meanstream.Portal.Core.Membership.MembershipService.Current.CreateUser(username, subscriber.Email, password, roles, True, False, False)

            If status = System.Web.Security.MembershipCreateStatus.Success Then
                'if create user is successful then create subscriber
                Try
                    Dim UserProfile As Meanstream.Portal.Core.Membership.UserProfile = New Meanstream.Portal.Core.Membership.UserProfile
                    UserProfile.Initialize(username, True)
                    UserProfile.SetPropertyValue("FirstName", subscriber.FirstName)
                    UserProfile.SetPropertyValue("LastName", subscriber.LastName)
                    UserProfile.SetPropertyValue("DisplayName", subscriber.DisplayName)
                    UserProfile.Save()

                    subscriber.UserId = UserProfile.UserId

                    SubscriberManager.Create(subscriber)

                    Dim s As New Subscriber(subscriber.Id)
                    Dim manager As New SubscriberManager(subscriber)
                    manager.LoadFromDatasource()

                    For Each item As Item In items
                        Dim itemManager As New ItemManager(item)
                        itemManager.LoadFromDatasource()
                        manager.AddItemToSubscriber(item.Id)
                    Next

                    If subscriber.PromoCode.Trim <> "" Then
                        Dim code As New PromoCode(subscriber.PromoCode)
                        Dim promoCodeManager As New PromoCodeManager(code)
                        promoCodeManager.LoadFromDatasource()
                        promoCodeManager.IncrementUsed(1)
                    End If

                    BillingManager.Create(billing)
                Catch ex As Exception
                    'rollback
                    Meanstream.Portal.Core.Membership.MembershipService.Current.DeleteUser(username)
                    'delete subscriber
                    Dim s As New Subscriber(subscriber.Id)
                    Dim sManager As New SubscriberManager(s)
                    sManager.Delete()
                    'delete billing
                    Dim b As New Billing(billing.Id)
                    Dim bManager As New BillingManager(b)
                    bManager.Delete()
                    Throw New CreateSubscriptionException(ex.Message)
                End Try
                
            ElseIf status = System.Web.Security.MembershipCreateStatus.DuplicateEmail Then
                Throw New CreateSubscriptionException("email exists in the system")
            ElseIf status = System.Web.Security.MembershipCreateStatus.DuplicateUserName Then
                Throw New CreateSubscriptionException("username exists in the system")
            ElseIf status = System.Web.Security.MembershipCreateStatus.InvalidUserName Then
                Throw New CreateSubscriptionException("please enter a valid username (6-20 characters)")
            ElseIf status = System.Web.Security.MembershipCreateStatus.InvalidQuestion Then
                Throw New CreateSubscriptionException("please enter a valid security question")
            ElseIf status = System.Web.Security.MembershipCreateStatus.InvalidAnswer Then
                Throw New CreateSubscriptionException("please enter a valid security answer")
            ElseIf status = System.Web.Security.MembershipCreateStatus.InvalidPassword Then
                Throw New CreateSubscriptionException("please enter a valid password")
            ElseIf status = System.Web.Security.MembershipCreateStatus.InvalidEmail Then
                Throw New CreateSubscriptionException("please enter a valid email address")
            End If
        End Sub

        Public Overridable Sub Subscribe(ByVal username As String, ByVal password As String, ByVal roles() As String, ByVal subscriber As Subscriber, _
                                              ByVal items As List(Of Item), ByVal billing As Billing)
            Dim success As Boolean = False
            Dim amountToCharge As Decimal = 0.0

            'minimum required fields
            If items.Count = 0 Then
                Throw New CreateSubscriptionException("at least one subscription item is required")
            End If
            Dim Expression As New System.Text.RegularExpressions.Regex("\S+@\S+\.\S+")
            If Not Expression.IsMatch(subscriber.Email) Then
                Throw New CreateSubscriptionException("a valid email address is required")
            End If
            If subscriber.FirstName.Trim = "" Then
                Throw New CreateSubscriptionException("first name required")
            ElseIf subscriber.LastName.Trim = "" Then
                Throw New CreateSubscriptionException("last name required")
            ElseIf password.Trim = "" Then
                Throw New CreateSubscriptionException("password required")
            ElseIf subscriber.Id = Nothing Then
                Throw New CreateSubscriptionException("subscriber id required")
            ElseIf billing.Address.Trim = "" Then
                Throw New CreateSubscriptionException("billing address required")
            ElseIf billing.City.Trim = "" Then
                Throw New CreateSubscriptionException("billing city required")
            ElseIf billing.Zip.Trim = "" Then
                Throw New CreateSubscriptionException("billing zip required")
            ElseIf billing.StateOrProvince.Trim = "" Then
                Throw New CreateSubscriptionException("billing state or province required")
            ElseIf IsNothing(billing.Method) Then
                Throw New CreateSubscriptionException("billing payment method required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.CardHolderName.Trim = "" Then
                Throw New CreateSubscriptionException("billing card holder name required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.CreditCardNumber.Trim = "" Then
                Throw New CreateSubscriptionException("billing credit card number required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.ExpirationDate = Nothing Then
                Throw New CreateSubscriptionException("billing card holder name required")
            ElseIf IsNothing(subscriber.PaymentPlan) Then
                Throw New CreateSubscriptionException("subscriber payment plan required")
            ElseIf IsNothing(subscriber.Status) Then
                Throw New CreateSubscriptionException("subscriber status required")
            ElseIf subscriber.PortalId = Nothing Then
                Throw New CreateSubscriptionException("subscriber portal id required")
            ElseIf subscriber.SignupDate = Nothing Then
                Throw New CreateSubscriptionException("subscriber signup date required")
            End If

            'check if user already exists in the system
            If System.Web.Security.Membership.FindUsersByEmail(subscriber.Email).Count > 0 Then
                Throw New CreateSubscriptionException("user already exists")
            End If


            If subscriber.PromoCode.Trim <> "" Then
                Dim code As New PromoCode(subscriber.PromoCode)
                Dim promoCodeManager As New PromoCodeManager(code)
                promoCodeManager.LoadFromDatasource()
                If code.Allocation = code.Used Then
                    Throw New CreateSubscriptionException("the promo code has exceeded its allocation")
                End If
            End If
            
            'Try
            'calaculate total and promo code
            amountToCharge = Me.calculateTotal(items, billing.StateOrProvince, subscriber.PromoCode)

            'get provider and create subscription - if success then update our end
            If billing.Method = Subscription.Billing.PaymentMethod.CreditCard Then
                Dim subscription As New Meanstream.Portal.PaymentGateway.SubscriptionRequest
                subscription.City = billing.City
                subscription.Company = subscriber.Company
                subscription.Country = billing.Country
                subscription.Fax = billing.Fax
                subscription.Phone = billing.Phone
                subscription.Zip = billing.Zip
                subscription.StateOrProvince = billing.StateOrProvince
                subscription.Address = billing.Address
                subscription.Address2 = billing.Address2
                subscription.CCV = billing.CCV
                subscription.CreditCardExpirationDate = billing.ExpirationDate
                subscription.CreditCardNumber = billing.CreditCardNumber
                subscription.Amount = amountToCharge
                subscription.StartsOn = Date.Now 'subscription setting....
                subscription.PaymentSchedule = subscriber.PaymentPlan
                subscription.Name = subscriber.Id.ToString
                subscription.Email = subscriber.Email
                subscription.First = billing.First
                subscription.Last = billing.Last
                subscription.BillingAddressId = billing.Id.ToString

                'make request
                Dim gateway As Meanstream.Portal.PaymentGateway.ISubscriptionProvider = New Meanstream.Portal.PaymentGateway.Providers.AuthorizeNetSubscriptionProvider
                gateway.CreateSubscription(subscription)
                'store subscriber ID we get back from the request
                subscriber.GatewaySubscriptionId = subscription.Id
            End If

            success = True
            'Catch ex As Exception
            '    'there was an issue with gateway
            '    Throw New CreateSubscriptionException(ex.Message)
            'End Try

            'if card run is successful then create user
            If success Then
                Me.SubscribeBypassPaymentGateway(username, password, roles, subscriber, items, billing)
            End If
        End Sub

        Private Function calculateTotal(ByVal items As List(Of Item), ByVal state As String, ByVal promoCode As String) As Double
            Dim Total As Double = 0.0
            Dim SalesTax As Double = 0.0
            Dim SubTotal As Double = 0.0
            Dim amount As Double = 0.0

            'get total amount for items
            For Each item As Item In items
                amount = amount + item.Price
            Next

            If promoCode.Trim <> "" Then
                Dim code As New Meanstream.Portal.Subscription.PromoCode(promoCode.Trim)
                Dim promoCodeManager As New Meanstream.Portal.Subscription.PromoCodeManager(code)

                Try
                    promoCodeManager.LoadFromDatasource()
                Catch ex As Exception
                    Throw New CreateSubscriptionException("please provide a valid promo code")
                End Try

                'percentage off
                If code.Type = Subscription.PromoCode.DiscountType.Percentage Then
                    If code.Discount = 100 Then
                        amount = 0
                    Else
                        amount = amount - (amount * Double.Parse("0." & code.Discount))
                    End If
                End If

                'dollar off
                If code.Type = Subscription.PromoCode.DiscountType.Dollar Then
                    amount = amount - code.Discount
                    If amount < 0 Then
                        amount = 0
                    End If
                End If
            End If

            If state.Trim.ToLower = "NC" Or state.Trim.ToLower = "north carolina" Then 'subscription settings....
                If amount > 0 Then
                    SalesTax = amount * 0.07 'subscription settings...
                End If
            End If

            SubTotal = amount
            Total = SubTotal + SalesTax
            Return Total
        End Function

        Public Overridable Sub CancelSubscription(ByVal subscriberId As Guid, ByVal reasonForCanceling As String)
            Dim subscriber As New Subscriber(subscriberId)
            Dim manager As New SubscriberManager(subscriber)
            manager.LoadFromDatasource()

            'get provider and cancel - if success then update our end
            If Not gateway.CancelSubscription(subscriber.GatewaySubscriptionId) Then
                Throw New CancelSubscriptionException("The payment processing gateway was unable to cancel the subscription. Please contact Support.")
            End If
            'Try
            subscriber.ReasonForCanceling = reasonForCanceling
            subscriber.Status = Meanstream.Portal.PaymentGateway.SubscriptionStatus.Canceled
            manager.Save()
            'Catch ex As Exception

            'End Try
        End Sub

        Public Overridable Function UpdateSubscription(ByVal subscriber As Subscriber, ByVal items As List(Of Item), ByVal billing As Billing) As Boolean
            Dim success As Boolean = False
            Dim amountToCharge As Decimal = 0.0

            'minimum required fields
            If items.Count = 0 Then
                Throw New UpdateSubscriptionException("at least one subscription item is required")
            End If
            Dim Expression As New System.Text.RegularExpressions.Regex("\S+@\S+\.\S+")
            If Not Expression.IsMatch(subscriber.Email) Then
                Throw New UpdateSubscriptionException("a valid email address is required")
            End If
            If subscriber.FirstName.Trim = "" Then
                Throw New UpdateSubscriptionException("first name required")
            ElseIf subscriber.LastName.Trim = "" Then
                Throw New UpdateSubscriptionException("last name required")
            ElseIf subscriber.Id = Nothing Then
                Throw New UpdateSubscriptionException("subscriber id required")
            ElseIf String.IsNullOrEmpty(subscriber.GatewaySubscriptionId) Then
                Throw New UpdateSubscriptionException("GatewaySubscriptionId required")
            ElseIf billing.Address.Trim = "" Then
                Throw New UpdateSubscriptionException("billing address required")
            ElseIf billing.City.Trim = "" Then
                Throw New UpdateSubscriptionException("billing city required")
            ElseIf billing.Zip.Trim = "" Then
                Throw New UpdateSubscriptionException("billing zip required")
            ElseIf billing.StateOrProvince.Trim = "" Then
                Throw New UpdateSubscriptionException("billing state or province required")
            ElseIf IsNothing(billing.Method) Then
                Throw New UpdateSubscriptionException("billing payment method required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.CardHolderName.Trim = "" Then
                Throw New UpdateSubscriptionException("billing card holder name required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.CreditCardNumber.Trim = "" Then
                Throw New UpdateSubscriptionException("billing credit card number required")
            ElseIf billing.Method = Subscription.Billing.PaymentMethod.CreditCard And billing.ExpirationDate = Nothing Then
                Throw New UpdateSubscriptionException("billing card holder name required")
            ElseIf IsNothing(subscriber.PaymentPlan) Then
                Throw New UpdateSubscriptionException("subscriber payment plan required")
            ElseIf IsNothing(subscriber.Status) Then
                Throw New UpdateSubscriptionException("subscriber status required")
            ElseIf subscriber.PortalId = Nothing Then
                Throw New UpdateSubscriptionException("subscriber portal id required")
            ElseIf subscriber.SignupDate = Nothing Then
                Throw New UpdateSubscriptionException("subscriber signup date required")
            End If

            'update promo code if changed then check that it is still valid
            Dim s As New Subscriber(subscriber.Id)
            Dim manager As New SubscriberManager(s)
            manager.LoadFromDatasource()

            If s.PromoCode <> subscriber.PromoCode Then
                Dim code As New PromoCode(subscriber.PromoCode)
                Dim promoCodeManager As New PromoCodeManager(code)
                promoCodeManager.LoadFromDatasource()
                If code.Allocation = code.Used Then
                    Throw New UpdateSubscriptionException("the promo code has exceeded its allocation")
                End If
            End If

            'Try
            'calaculate total and promo code
            amountToCharge = Me.calculateTotal(items, billing.StateOrProvince, subscriber.PromoCode)

            'get provider and create subscription - if success then update our end
            If billing.Method = Subscription.Billing.PaymentMethod.CreditCard Then
                Dim subscription As New Meanstream.Portal.PaymentGateway.SubscriptionRequest
                subscription.City = billing.City
                subscription.Company = subscriber.Company
                subscription.Country = billing.Country
                subscription.Fax = billing.Fax
                subscription.Phone = billing.Phone
                subscription.Zip = billing.Zip
                subscription.StateOrProvince = billing.StateOrProvince
                subscription.Address = billing.Address
                subscription.Address2 = billing.Address2
                subscription.CCV = billing.CCV
                subscription.CreditCardExpirationDate = billing.ExpirationDate
                subscription.CreditCardNumber = billing.CreditCardNumber
                subscription.Amount = amountToCharge
                subscription.StartsOn = subscriber.SignupDate
                subscription.PaymentSchedule = subscriber.PaymentPlan
                subscription.Name = subscriber.Id.ToString
                subscription.Email = subscriber.Email
                subscription.First = billing.First
                subscription.Last = billing.Last
                subscription.BillingAddressId = billing.Id.ToString

                'gateway ID
                subscription.Id = subscriber.GatewaySubscriptionId

                'make request
                Try
                    Dim gateway As Meanstream.Portal.PaymentGateway.ISubscriptionProvider = New Meanstream.Portal.PaymentGateway.Providers.AuthorizeNetSubscriptionProvider
                    success = gateway.UpdateSubscription(subscription)
                Catch ex As Exception
                    Throw New UpdateSubscriptionException(ex.Message.ToString)
                End Try
                    
            End If

            'Catch ex As Exception
            '    'there was an issue with gateway
            '    Throw New CreateSubscriptionException(ex.Message)
            'End Try

            'if gateway update is successful then update system
            If success Then
                success = False

                'update user
                Dim user As System.Web.Security.MembershipUser = System.Web.Security.Membership.GetUser(subscriber.UserId)
                Dim UserProfile As Meanstream.Portal.Core.Membership.UserProfile = New Meanstream.Portal.Core.Membership.UserProfile
                UserProfile.Initialize(user.UserName, True)
                UserProfile.SetPropertyValue("FirstName", subscriber.FirstName)
                UserProfile.SetPropertyValue("LastName", subscriber.LastName)
                UserProfile.SetPropertyValue("DisplayName", subscriber.DisplayName)
                UserProfile.Save()


                'update promo code if changed then increment used
                If s.PromoCode <> subscriber.PromoCode Then
                    Dim code As New PromoCode(subscriber.PromoCode)
                    Dim promoCodeManager As New PromoCodeManager(code)
                    promoCodeManager.LoadFromDatasource()
                    promoCodeManager.IncrementUsed(1)
                End If

                'update subscriber
                manager = New SubscriberManager(subscriber)
                manager.Save()

                'update items
                manager.RemoveAllItemsFromSubscriber()
                For Each item As Item In items
                    Dim itemManager As New ItemManager(item)
                    itemManager.LoadFromDatasource()
                    manager.AddItemToSubscriber(item.Id)
                Next

                'update our billing
                Dim billingManager As New BillingManager(billing)
                billingManager.Save()

                success = True
            Else
                Return success
            End If
            Return success
        End Function

        'schedule a task
        'get payment/transaction list from provider based on subscription billing type
        'update our database
#End Region

#Region " Properties "
        Private _appFriendlyName As String
        Public Property AppFriendlyName() As String
            Get
                Return _appFriendlyName
            End Get
            Private Set(ByVal value As String)
                _appFriendlyName = value
            End Set
        End Property

        Private _machineName As String
        Public Property MachineName() As String
            Get
                Return _machineName
            End Get
            Private Set(ByVal value As String)
                _machineName = value
            End Set
        End Property

        Private _applicationId As Guid
        Public Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
            Private Set(ByVal value As Guid)
                _applicationId = value
            End Set
        End Property
#End Region


#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Deinitialize()
        End Sub
#End Region
    End Class
End Namespace

