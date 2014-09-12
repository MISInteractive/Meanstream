Imports Meanstream.Portal.Subscription.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Subscription
    Public Class PromoCodeManager
        
        Private _entity As PromoCode

        Sub New(ByRef attributeEntity As PromoCode)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If String.IsNullOrEmpty(_entity.Code) Then
                Throw New ArgumentNullException("promoCode id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode
            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.GetByCode(_entity.Code)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the promoCode {0} cannot be located in database.", _entity.Code))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode)
            _entity.Allocation = entity.Allocation
            _entity.CreatedDate = entity.CreatedDate
            _entity.Discount = entity.Discount
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.PortalId = entity.PortalId
            _entity.SalesRep = entity.SalesRep
            _entity.Type = [Enum].Parse(GetType(PromoCode.DiscountType), entity.DiscountType)
            _entity.Used = entity.Used
        End Sub

        Public Overridable Sub Save()

            If _entity.PortalId = Nothing Then
                Throw New ArgumentException("portal id is required")
            End If
            If _entity.Allocation = Nothing Then
                Throw New ArgumentException("allocation is required")
            End If
            If _entity.Discount = Nothing Then
                Throw New ArgumentException("discount is required")
            End If
            If IsNothing(_entity.Type) Then
                Throw New ArgumentException("discount type is required")
            End If
            If _entity.Type = Subscription.PromoCode.DiscountType.Dollar Then
                Decimal.Parse(_entity.Discount)
            End If
            If _entity.Type = Subscription.PromoCode.DiscountType.Percentage Then
                Integer.Parse(_entity.Discount)
            End If

            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode = Me.GetEntityFromDatasource
            'for now do not allow update of code!
            entity.LastModifiedDate = Date.Now
            entity.Allocation = _entity.Allocation
            entity.CreatedDate = _entity.CreatedDate
            entity.Discount = _entity.Discount
            entity.LastModifiedDate = Date.Now
            entity.PortalId = _entity.PortalId
            entity.SalesRep = _entity.SalesRep
            entity.DiscountType = [Enum].GetName(GetType(PromoCode.DiscountType), _entity.Type)
            entity.Used = _entity.Used

            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.Update(entity)
        End Sub

        Public Overridable Sub Delete()
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.Delete(_entity.Code)
        End Sub

        Public Shared Sub Create(ByVal promoCode As PromoCode)
            If promoCode.PortalId = Nothing Then
                Throw New ArgumentException("portal id is required")
            End If
            If promoCode.Allocation = Nothing Then
                Throw New ArgumentException("allocation is required")
            End If
            If promoCode.Discount = Nothing Then
                Throw New ArgumentException("discount is required")
            End If
            If IsNothing(promoCode.Type) Then
                Throw New ArgumentException("discount type is required")
            End If
            If promoCode.Type = Subscription.PromoCode.DiscountType.Dollar Then
                Decimal.Parse(promoCode.Discount)
            End If
            If promoCode.Type = Subscription.PromoCode.DiscountType.Percentage Then
                Integer.Parse(promoCode.Discount)
            End If

            Dim existing As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.GetByCode(promoCode.Code)
            If existing IsNot Nothing Then
                Throw New InvalidOperationException(String.Format("The promo code {0} already exists in the database. A unique promo code is required.", existing.Code))
            End If

            Dim entity As New Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode
            entity.Code = promoCode.Code
            entity.LastModifiedDate = Date.Now
            entity.Allocation = promoCode.Allocation
            entity.CreatedDate = Date.Now
            entity.Discount = promoCode.Discount
            entity.LastModifiedDate = Date.Now
            entity.PortalId = promoCode.PortalId
            entity.SalesRep = promoCode.SalesRep
            entity.DiscountType = [Enum].GetName(GetType(PromoCode.DiscountType), promoCode.Type)
            entity.Used = 0

            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.Insert(entity)
        End Sub

        Protected Friend Sub IncrementUsed(ByVal increment As Integer)
            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionPromoCode = Me.GetEntityFromDatasource
            entity.Used = entity.Used + increment
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionPromoCodeProvider.Update(entity)
        End Sub
    End Class
End Namespace

