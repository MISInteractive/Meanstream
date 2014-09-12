Imports Meanstream.Portal.Subscription.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Subscription
    Public Class BillingManager
        Inherits AttributeEntityManager

        Private _entity As Billing

        Sub New(ByRef attributeEntity As Billing)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("billing id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling
            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the billing {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling)
            _entity.Address = entity.Address
            _entity.Address2 = entity.Address2
            _entity.CardHolderName = entity.CardHolderName
            _entity.CCV = entity.Ccv
            _entity.City = entity.City
            _entity.Country = entity.Country
            _entity.CreatedDate = entity.CreatedDate
            _entity.CreditCardNumber = entity.CreditCardNumber
            _entity.CreditCardType = [Enum].Parse(GetType(CreditCard.CreditCardTypes), entity.CreditCardType)
            _entity.ExpirationDate = entity.ExpirationDate.GetValueOrDefault
            _entity.IsDefault = entity.IsDefault
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Method = [Enum].Parse(GetType(Billing.PaymentMethod), entity.PaymentMethod)
            _entity.Notes = entity.Notes
            _entity.StateOrProvince = entity.StateOrProvince
            _entity.Fax = entity.Fax
            _entity.Phone = entity.Phone
            _entity.SubscriberId = entity.SubscriberId
            _entity.Zip = entity.Zip
            _entity.First = entity.First
            _entity.Last = entity.Last
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            If IsNothing(_entity.Method) Then
                Throw New ArgumentException("method is required")
            End If
            If _entity.SubscriberId = Nothing Then
                Throw New ArgumentException("subscriber id required")
            End If
            If _entity.IsDefault = Nothing Then
                Throw New ArgumentException("is default required")
            End If

            If _entity.Method = Billing.PaymentMethod.CreditCard Then
                If CreditCard.Validate(_entity.CreditCardNumber) = CreditCard.CreditCardTypes.Invalid Then
                    Throw New ArgumentException("a valid credit card number is required")
                End If
                If String.IsNullOrEmpty(_entity.CardHolderName) Then
                    Throw New ArgumentException("card holder name is required")
                End If
                If _entity.ExpirationDate = Nothing Then
                    Throw New ArgumentException("expiration date is required")
                End If
                If String.IsNullOrEmpty(_entity.CCV) Then
                    Throw New ArgumentException("ccv is required")
                End If
                If String.IsNullOrEmpty(_entity.Address) Then
                    Throw New ArgumentException("address is required")
                End If
                If String.IsNullOrEmpty(_entity.City) Then
                    Throw New ArgumentException("city is required")
                End If
                If String.IsNullOrEmpty(_entity.StateOrProvince) Then
                    Throw New ArgumentException("state or province is required")
                End If
                If String.IsNullOrEmpty(_entity.Zip) Then
                    Throw New ArgumentException("zip is required")
                End If
                If String.IsNullOrEmpty(_entity.Country) Then
                    Throw New ArgumentException("country is required")
                End If
                If String.IsNullOrEmpty(_entity.First) Then
                    Throw New ArgumentException("first name is required")
                End If
                If String.IsNullOrEmpty(_entity.Last) Then
                    Throw New ArgumentException("last name is required")
                End If
            End If

            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling = Me.GetEntityFromDatasource

            entity.Address = _entity.Address
            entity.Address2 = _entity.Address2
            entity.CardHolderName = _entity.CardHolderName
            entity.Ccv = _entity.CCV
            entity.City = _entity.City
            entity.Country = _entity.Country
            entity.CreatedDate = _entity.CreatedDate
            entity.CreditCardNumber = _entity.CreditCardNumber
            entity.CreditCardType = [Enum].Parse(GetType(CreditCard.CreditCardTypes), _entity.CreditCardType)

            If _entity.ExpirationDate <> Nothing Then
                entity.ExpirationDate = _entity.ExpirationDate
            End If

            entity.IsDefault = _entity.IsDefault
            entity.LastModifiedDate = Date.Now
            entity.PaymentMethod = [Enum].Parse(GetType(Billing.PaymentMethod), _entity.Method)
            entity.Notes = _entity.Notes
            entity.StateOrProvince = _entity.StateOrProvince
            entity.Fax = _entity.Fax
            entity.Phone = _entity.Phone
            entity.SubscriberId = _entity.SubscriberId
            entity.Zip = _entity.Zip
            entity.First = _entity.First
            entity.Last = _entity.Last
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.Delete(_entity.Id)
        End Sub

        Public Shared Sub Create(ByVal billing As Billing)
            If IsNothing(billing.Method) Then
                Throw New ArgumentException("method is required")
            End If
            If billing.SubscriberId = Nothing Then
                Throw New ArgumentException("subscriber id required")
            End If
            If billing.IsDefault = Nothing Then
                Throw New ArgumentException("is default required")
            End If

            If billing.Method = billing.PaymentMethod.CreditCard Then
                If CreditCard.Validate(billing.CreditCardNumber) = CreditCard.CreditCardTypes.Invalid Then
                    Throw New ArgumentException("a valid credit card number is required")
                End If
                If String.IsNullOrEmpty(billing.CardHolderName) Then
                    Throw New ArgumentException("card holder name is required")
                End If
                If billing.ExpirationDate = Nothing Then
                    Throw New ArgumentException("expiration date is required")
                End If
                If String.IsNullOrEmpty(billing.CCV) Then
                    Throw New ArgumentException("ccv is required")
                End If
                If String.IsNullOrEmpty(billing.Address) Then
                    Throw New ArgumentException("address is required")
                End If
                If String.IsNullOrEmpty(billing.City) Then
                    Throw New ArgumentException("city is required")
                End If
                If String.IsNullOrEmpty(billing.StateOrProvince) Then
                    Throw New ArgumentException("state or province is required")
                End If
                If String.IsNullOrEmpty(billing.Zip) Then
                    Throw New ArgumentException("zip is required")
                End If
                If String.IsNullOrEmpty(billing.Country) Then
                    Throw New ArgumentException("country is required")
                End If
                If String.IsNullOrEmpty(billing.First) Then
                    Throw New ArgumentException("first name is required")
                End If
                If String.IsNullOrEmpty(billing.Last) Then
                    Throw New ArgumentException("last name is required")
                End If
            End If

            Dim entity As New Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionSubscriberBilling
            entity.Id = billing.Id
            entity.Address = billing.Address
            entity.Address2 = billing.Address2
            entity.CardHolderName = billing.CardHolderName
            entity.Ccv = billing.CCV
            entity.City = billing.City
            entity.Country = billing.Country
            entity.CreatedDate = Date.Now
            entity.CreditCardNumber = billing.CreditCardNumber
            entity.CreditCardType = [Enum].Parse(GetType(CreditCard.CreditCardTypes), billing.CreditCardType)

            If billing.ExpirationDate <> Nothing Then
                entity.ExpirationDate = billing.ExpirationDate
            End If

            entity.IsDefault = billing.IsDefault
            entity.LastModifiedDate = Date.Now
            entity.PaymentMethod = [Enum].Parse(GetType(Billing.PaymentMethod), billing.Method)
            entity.Notes = billing.Notes
            entity.StateOrProvince = billing.StateOrProvince
            entity.Fax = billing.Fax
            entity.Phone = billing.Phone
            entity.SubscriberId = billing.SubscriberId
            entity.Zip = billing.Zip
            entity.First = billing.First
            entity.Last = billing.Last
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionSubscriberBillingProvider.Insert(entity)
        End Sub
    End Class
End Namespace

