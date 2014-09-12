Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.CRM.Extensibility

    ''' <summary>
    ''' Decorator class that references the IAttributeEntity that processes and tracks attributes.
    ''' This class provides the way to decouple the AttributeEntity from its implementation.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class AttributeEntityManager
        Private _attributeEntityBase As IAttributeEntity

        Sub New(ByRef attributeEntity As IAttributeEntity)
            _attributeEntityBase = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            _attributeEntityBase.Attributes = Me.Attributes
        End Sub

        Public Overridable Sub Delete()
            'delete attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In Me.Attributes
                AttributeService.Current.Delete(Attribute)
            Next
            _attributeEntityBase = Nothing
            _OriginalAttributes = Nothing
        End Sub

        Public Overridable Sub Save()
            'add attributes
            For Each Attribute As Attribute In Attributes
                If Not OriginalAttributes.Contains(Attribute) Then
                    AttributeService.Current.Create(_attributeEntityBase.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
                End If
            Next

            'remove attributes
            For Each OriginalAttribute As Meanstream.Portal.Core.Extensibility.Attribute In Me.OriginalAttributes
                If Not Me.Attributes.Contains(OriginalAttribute) Then
                    AttributeService.Current.Delete(OriginalAttribute)
                End If
            Next

            'update attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In Attributes
                Attribute.ComponentId = _attributeEntityBase.Id
                AttributeService.Current.Save(Attribute)
            Next

            'reload our attributes
            _OriginalAttributes = Nothing
            _attributeEntityBase.Attributes = Me.OriginalAttributes
        End Sub

        Private _OriginalAttributes As List(Of Meanstream.Portal.Core.Extensibility.Attribute)
        Private ReadOnly Property OriginalAttributes() As List(Of Meanstream.Portal.Core.Extensibility.Attribute)
            Get
                If _OriginalAttributes Is Nothing Then
                    Return AttributeService.Current.GetAttributes(_attributeEntityBase.Id)
                End If
                Return _OriginalAttributes
            End Get
        End Property

        Dim attributesLock As Boolean = False
        Private ReadOnly Property Attributes() As List(Of Meanstream.Portal.Core.Extensibility.Attribute)
            Get
                'If _attributeEntityBase.Attributes.Count = 0 Then
                If Not attributesLock Then
                    If _attributeEntityBase.Attributes.Count = 0 Then
                        If Me.OriginalAttributes IsNot Nothing Then
                            _attributeEntityBase.Attributes.AddRange(Me.OriginalAttributes)
                            'For Each OriginalAttribute As Attribute In Me.OriginalAttributes
                            '    If Not _attributeEntityBase.Attributes.Contains(OriginalAttribute) Then
                            '        _attributeEntityBase.Attributes.Add(OriginalAttribute)
                            '    End If
                            'Next
                        End If
                    End If
                    attributesLock = True
                End If
                'End If
                Return _attributeEntityBase.Attributes
            End Get
        End Property
    End Class
End Namespace

