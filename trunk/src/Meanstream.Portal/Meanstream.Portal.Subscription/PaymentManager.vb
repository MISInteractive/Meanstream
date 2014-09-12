Imports Meanstream.Portal.Subscription.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Subscription
    Public Class PaymentManager
        Inherits AttributeEntityManager

        Private _entity As Payment

        Sub New(ByRef attributeEntity As Payment)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("payment id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPayment
            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPayment = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPaymentProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the payment {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPayment)
            _entity.AuthCode = entity.AuthCode
            _entity.BillingId = entity.SubscriberBillingId
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.PaymentDate = entity.PaymentDate
            _entity.PaymentProcessorId = entity.PaymentProcessorId
            _entity.Status = [Enum].Parse(GetType(Payment.PaymentStatus), entity.Status)
            _entity.StatusMessage = entity.StatusMessage
            _entity.SubscriberId = entity.SubscriberId
            _entity.TransactionId = entity.TransactionId
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            If _entity.SubscriberId = Nothing Then
                Throw New ArgumentException("subscriber id is required")
            End If
            If _entity.BillingId = Nothing Then
                Throw New ArgumentException("billing id is required")
            End If
            If _entity.PaymentDate = Nothing Then
                Throw New ArgumentException("payment date is required")
            End If
            If _entity.Status = Nothing Then
                Throw New ArgumentException("status is required")
            End If

            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPayment = Me.GetEntityFromDatasource

            entity.AuthCode = _entity.AuthCode
            entity.SubscriberBillingId = _entity.BillingId
            entity.LastModifiedDate = Date.Now
            entity.PaymentDate = _entity.PaymentDate
            entity.PaymentProcessorId = _entity.PaymentProcessorId
            entity.Status = [Enum].GetName(GetType(Payment.PaymentStatus), _entity.Status)
            entity.StatusMessage = _entity.StatusMessage
            entity.SubscriberId = _entity.SubscriberId
            entity.TransactionId = _entity.TransactionId

            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPaymentProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPaymentProvider.Delete(_entity.Id)
        End Sub

    End Class
End Namespace

