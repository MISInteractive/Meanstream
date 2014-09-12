Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.Core.Content
    Public Class SkinManager
        Inherits AttributeEntityManager

        Private _entity As Skin

        Sub New(ByRef attributeEntity As Skin)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("skin id cannot be null.")
            End If
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamSkins)
            _entity.PortalId = entity.PortalId
            _entity.Name = entity.SkinRoot
            _entity.Path = entity.SkinSrc
            _entity.Zones = Me.Zones
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Delete(_entity.Id) Then
                Dim SkinPaneList As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Find("SkinId=" & _entity.Id.ToString)
                For Each SkinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane In SkinPaneList
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Delete(SkinPane)
                Next
            End If
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.Core.Entities.MeanstreamSkins = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.GetById(_entity.Id)

            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the skin {0} cannot be located in database.", _entity.Id))
            End If

            'add zones
            For Each zone As SkinZone In Zones
                If Not Me.OriginalZones.Contains(zone) Then
                    Dim skinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane = New Meanstream.Portal.Core.Entities.MeanstreamSkinPane
                    skinPane.Pane = zone.Pane
                    skinPane.SkinId = _entity.Id
                    skinPane.Id = Guid.NewGuid
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Insert(skinPane)
                End If
            Next

            'remove zones
            For Each zone As SkinZone In Me.OriginalZones
                If Not Me.Zones.Contains(zone) Then
                    Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Delete(zone.Id)
                End If
            Next

            'update zones
            For Each zone As SkinZone In Zones
                Dim skinPane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane = New Meanstream.Portal.Core.Entities.MeanstreamSkinPane
                skinPane.Pane = zone.Pane
                skinPane.SkinId = _entity.Id
                skinPane.Id = zone.Id
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Update(skinPane)
            Next

            'reload zones
            _entity.Zones = Me.OriginalZones

            entity.PortalId = _entity.PortalId
            entity.SkinRoot = _entity.Name
            entity.SkinSrc = _entity.Path
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinsProvider.Update(entity)
        End Sub


        Private ReadOnly Property OriginalZones() As List(Of SkinZone)
            Get
                Dim _originalZones As New List(Of SkinZone)
                Dim panes As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamSkinPane) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamSkinPaneProvider.Find("SkinId=" & _entity.Id.ToString)
                For Each pane As Meanstream.Portal.Core.Entities.MeanstreamSkinPane In panes
                    Dim zone As Meanstream.Portal.Core.Content.SkinZone = New Meanstream.Portal.Core.Content.SkinZone(pane.Id)
                    zone.SkinId = pane.SkinId
                    zone.Pane = pane.Pane
                    _originalZones.Add(zone)
                Next
                Return _originalZones
            End Get
        End Property

        Private ReadOnly Property Zones() As List(Of SkinZone)
            Get
                If _entity.Zones Is Nothing Then
                    If Me.OriginalZones IsNot Nothing Then
                        _entity.Zones = Me.OriginalZones
                    Else
                        _entity.Zones = New List(Of SkinZone)
                    End If
                End If
                Return _entity.Zones
            End Get
        End Property
    End Class
End Namespace
