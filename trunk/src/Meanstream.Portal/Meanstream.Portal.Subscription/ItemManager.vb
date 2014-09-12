Imports Meanstream.Portal.Subscription.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Subscription
    Public Class ItemManager
        Inherits AttributeEntityManager

        Private _entity As Item

        Sub New(ByRef attributeEntity As Item)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("item id cannot be null.")
            End If
        End Sub

        Private Function GetEntityFromDatasource() As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionItem
            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionItem = Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the item {0} cannot be located in database.", _entity.Id))
            End If
            Return entity
        End Function

        Public Sub LoadFromDatasource()
            Me.Bind(Me.GetEntityFromDatasource())
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionItem)
            _entity.CreatedDate = entity.CreatedDate
            _entity.Description = entity.Description
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Name = entity.Name
            _entity.StartPayment = entity.StartPayment
            _entity.PortalId = entity.PortalId
            _entity.Price = entity.Price
        End Sub

        Public Sub Bind(ByVal entity As Meanstream.Portal.Subscription.Entities.VwMeanstreamSubscriptionSubscriberItems)
            _entity.CreatedDate = entity.CreatedDate
            _entity.Description = entity.Description
            _entity.LastModifiedDate = entity.LastModifiedDate
            _entity.Name = entity.Name
            _entity.StartPayment = entity.StartPayment
            _entity.PortalId = entity.PortalId
            _entity.Price = entity.Price
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            If _entity.PortalId = Nothing Then
                Throw New ArgumentException("portal id is required")
            End If
            If String.IsNullOrEmpty(_entity.Name) Then
                Throw New ArgumentException("name is required")
            End If
            If _entity.Price = Nothing Then
                Throw New ArgumentException("price is required")
            End If

            Dim entity As Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionItem = Me.GetEntityFromDatasource

            entity.CreatedDate = _entity.CreatedDate
            entity.Description = _entity.Description
            entity.StartPayment = _entity.StartPayment
            entity.LastModifiedDate = Date.Now
            entity.Name = _entity.Name
            entity.PortalId = _entity.PortalId
            entity.Price = _entity.Price
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.Update(entity)
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.Delete(_entity.Id)
        End Sub

        Public Shared Sub Create(ByVal item As Item)
            If item.PortalId = Nothing Then
                Throw New ArgumentException("portal id is required")
            End If
            If String.IsNullOrEmpty(item.Name) Then
                Throw New ArgumentException("name is required")
            End If
            If item.Price = Nothing Then
                Throw New ArgumentException("price is required")
            End If

            Dim entity As New Meanstream.Portal.Subscription.Entities.MeanstreamSubscriptionItem
            entity.Id = item.Id
            entity.StartPayment = item.StartPayment
            entity.CreatedDate = Date.Now
            entity.Description = item.Description
            entity.LastModifiedDate = Date.Now
            entity.Name = item.Name
            entity.PortalId = item.PortalId
            entity.Price = item.Price
            Meanstream.Portal.Subscription.Data.DataRepository.MeanstreamSubscriptionItemProvider.Insert(entity)
        End Sub

    End Class
End Namespace

