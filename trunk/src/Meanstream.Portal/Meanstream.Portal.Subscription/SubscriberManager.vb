Imports Meanstream.Portal.Subscription.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Subscription
    Public Class SubscriberManager
        Inherits AttributeEntityManager

        Private _entity As Subscriber

        Sub New(ByRef attributeEntity As Subscriber)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("subscriber id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber
            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the subscriber {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber)
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.PaymentPlan = [Enum].Parse(GetType(Meanstream.Portal.PaymentGateway.PaymentSchedule), entity.PaymentPlan)
            _entity.PortalId = entity.PortalId
            _entity.PromoCode = entity.PromoCode
            _entity.ReasonForCanceling = entity.ReasonForCanceling
            _entity.SignupDate = entity.SignupDate
            _entity.Status = [Enum].Parse(GetType(Meanstream.Portal.PaymentGateway.SubscriptionStatus), entity.Status)
            _entity.UserId = entity.UserId
            _entity.Company = entity.Company
            _entity.GatewaySubscriptionId = entity.GatewaySubscriptionId

            Dim user As System.Web.Security.MembershipUser = System.Web.Security.Membership.GetUser(entity.UserId)
            Dim UserProfile As Meanstream.Portal.Core.Membership.UserProfile = New Meanstream.Portal.Core.Membership.UserProfile
            UserProfile.Initialize(user.UserName, True)
            _entity.Email = user.Email
            _entity.FirstName = UserProfile.GetPropertyValue("FirstName")
            _entity.LastName = UserProfile.GetPropertyValue("LastName")
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            If _entity.PortalId = Nothing Then
                Throw New ArgumentException("portal id is required")
            End If
            'If _entity.UserId = Nothing Then
            '    Throw New ArgumentException("user id is required")
            'End If
            'If _entity.SignupDate = Nothing Then
            '    Throw New ArgumentException("signup date is required")
            'End If
            If IsNothing(_entity.PaymentPlan) Then
                Throw New ArgumentException("payment plan is required")
            End If
            If IsNothing(_entity.Status) Then
                Throw New ArgumentException("Status is required")
            End If

            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber = Me.GetEntityFromDatasource

            entity.LastModifiedDate = Date.Now
            entity.PaymentPlan = [Enum].GetName(GetType(Meanstream.Portal.PaymentGateway.PaymentSchedule), _entity.PaymentPlan)
            entity.PortalId = _entity.PortalId
            entity.PromoCode = _entity.PromoCode
            entity.ReasonForCanceling = _entity.ReasonForCanceling
            'entity.SignupDate = _entity.SignupDate
            entity.Status = [Enum].GetName(GetType(Meanstream.Portal.PaymentGateway.SubscriptionStatus), _entity.Status)
            'entity.UserId = _entity.UserId
            entity.Company = _entity.Company
            entity.GatewaySubscriptionId = _entity.GatewaySubscriptionId

            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()

            Me.LoadFromDatasource()

            Dim entities As Meanstream.Portal.Subscription.Entities.TList(Of Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberItems) = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Find("SubscriberId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberItems In entities
                Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Delete(entity)
            Next    
            Dim addresses As Meanstream.Portal.Subscription.Entities.TList(Of Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling) = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.Find("SubscriberId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling In addresses
                Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.Delete(entity)
            Next
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.Delete(_entity.Id)
            'delete user from the system
            Dim user As System.Web.Security.MembershipUser = System.Web.Security.Membership.GetUser(_entity.UserId)
            Meanstream.Portal.Core.Membership.MembershipService.Current.DeleteUser(user.UserName)
        End Sub

        Public Function IsSubscribed(ByVal itemId As Guid) As Boolean
            Dim itemEntity As Meanstream.Portal.Subscription.Entities.TList(Of Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberItems) = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Find("ItemId=" & itemId.ToString & " AND SubscriberId=" & _entity.Id.ToString)
            If itemEntity.Count > 0 Then
                Return True
            End If
            Return False
        End Function

        Public Sub AddItemToSubscriber(ByVal itemId As Guid)
            Dim itemEntity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionItem = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.GetById(itemId)
            If itemEntity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the item {0} cannot be located in database.", itemId))
            End If

            If Not IsSubscribed(itemId) Then
                Dim entity As New Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberItems
                entity.Id = Guid.NewGuid
                entity.ItemId = itemId
                entity.SubscriberId = _entity.Id
                Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Insert(entity)
            End If
        End Sub

        Public Sub RemoveItemFromSubscriber(ByVal itemId As Guid)
            Dim itemEntity As Meanstream.Portal.Subscription.Entities.TList(Of Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberItems) = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Find("ItemId=" & itemId.ToString & " AND SubscriberId=" & _entity.Id.ToString)
            If itemEntity.Count > 0 Then
                Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Delete(itemEntity)
            End If
        End Sub

        Public Sub RemoveAllItemsFromSubscriber()
            Dim itemEntity As Meanstream.Portal.Subscription.Entities.TList(Of Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberItems) = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Find("SubscriberId=" & _entity.Id.ToString)
            If itemEntity.Count > 0 Then
                Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberItemsProvider.Delete(itemEntity)
            End If
        End Sub

        Public Shared Sub Create(ByVal subscriber As Subscriber)
            If subscriber.PortalId = Nothing Then
                Throw New ArgumentException("portal id is required")
            End If
            If subscriber.UserId = Nothing Then
                Throw New ArgumentException("user id is required")
            End If
            If subscriber.SignupDate = Nothing Then
                Throw New ArgumentException("signup date is required")
            End If
            If IsNothing(subscriber.PaymentPlan) Then
                Throw New ArgumentException("payment plan is required")
            End If
            If IsNothing(subscriber.Status) Then
                Throw New ArgumentException("status is required")
            End If

            Dim entity As New Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriber
            entity.Id = subscriber.Id
            entity.LastModifiedDate = Date.Now
            entity.PaymentPlan = [Enum].GetName(GetType(Meanstream.Portal.PaymentGateway.PaymentSchedule), subscriber.PaymentPlan)
            entity.PortalId = subscriber.PortalId
            entity.PromoCode = subscriber.PromoCode
            entity.ReasonForCanceling = subscriber.ReasonForCanceling
            entity.SignupDate = subscriber.SignupDate
            entity.Status = [Enum].GetName(GetType(Meanstream.Portal.PaymentGateway.SubscriptionStatus), subscriber.Status)
            entity.UserId = subscriber.UserId
            entity.Company = subscriber.Company
            entity.GatewaySubscriptionId = subscriber.GatewaySubscriptionId

            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberProvider.Insert(entity)
        End Sub
    End Class
End Namespace

