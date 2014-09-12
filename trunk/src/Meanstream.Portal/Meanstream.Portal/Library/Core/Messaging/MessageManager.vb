Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.Messaging
    Public Class MessageManager
        Inherits AttributeEntityManager

        Private _entity As Message

        Sub New(ByRef attributeEntity As Message)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("message id cannot be null.")
            End If
        End Sub

        Protected Friend Sub LoadFromDatasource()
            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamMessaging = Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the message {0} cannot be located in database.", _entity.Id))
            End If
            Me.Bind(entity)
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamMessaging)
            _entity.Body = entity.Body
            _entity.DateOpened = entity.OpenedOn.GetValueOrDefault
            _entity.DateRecieved = entity.ReceivedOn.GetValueOrDefault
            _entity.DateSent = entity.SentOn.GetValueOrDefault
            _entity.IsQueued = entity.IsQueued
            _entity.MessageType = entity.MessageType
            _entity.Opened = entity.Opened
            _entity.ReceivedStatus = entity.Status
            '_entity.ReferenceId = entity.Id
            _entity.SentFrom = entity.Sender.GetValueOrDefault
            _entity.SentStatus = entity.Status
            _entity.SentTo = entity.Recipient.GetValueOrDefault
            _entity.Subject = entity.Subject
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamMessaging = New Meanstream.Portal.Core.Entities.MeanstreamMessaging
            entity.Body = _entity.Body
            entity.OpenedOn = _entity.DateOpened
            entity.ReceivedOn = _entity.DateRecieved
            entity.SentOn = _entity.DateSent
            entity.IsQueued = _entity.IsQueued
            entity.MessageType = _entity.MessageType
            entity.Opened = _entity.Opened
            entity.Status = _entity.ReceivedStatus
            '_entity.ReferenceId = entity.Id
            entity.Sender = _entity.SentFrom
            entity.Status = _entity.SentStatus
            entity.Recipient = _entity.SentTo
            entity.Subject = _entity.Subject
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamMessagingProvider.Update(entity)
        End Sub
    End Class
End Namespace

