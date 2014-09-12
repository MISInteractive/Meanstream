Imports Meanstream.Portal.Core.Extensibility
Imports Meanstream.Portal.Core.Messaging

Namespace Meanstream.Portal.Core
    Friend Class PortalManager
        Inherits AttributeEntityManager

        Private _entity As PortalBase

        Sub New(ByRef attributeEntity As PortalBase)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("Portal Id is null.")
            End If
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'custom code here
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim Portal As Meanstream.Portal.Core.Entities.MeanstreamPortals = Meanstream.Portal.Core.Data.DataRepository.MeanstreamPortalsProvider.GetById(_entity.Id)

            If Portal Is Nothing Then
                Throw New InvalidOperationException(String.Format("the portal {0} cannot be located in database.", _entity.Domain))
            End If

            'Persist portal info here
            Portal.Domain = _entity.Domain
            Portal.Name = _entity.Name
            Portal.Root = _entity.Root
            Portal.LoginPageUrl = _entity.LoginUrl
            Portal.HomePageUrl = _entity.HomePageUrl
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamPortalsProvider.Update(Portal)

            If ApplicationMessagingManager.Enabled Then
                ApplicationMessagingManager.Current.FirePortalContextChangedMessageEvent(New ApplicationMessage("Notify the PortalContextModule that the Portal was Saved on Domain: " & _entity.Domain, ApplicationMessageType.PortalSaved))
            End If
        End Sub
    End Class
End Namespace

