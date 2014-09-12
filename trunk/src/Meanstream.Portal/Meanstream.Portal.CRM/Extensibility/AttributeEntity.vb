Imports Meanstream.Portal.Core.Extensibility

Namespace Meanstream.Portal.CRM.Extensibility
    Public MustInherit Class AttributeEntity
        Implements IAttributeEntity

        Private Sub New()
        End Sub

        Sub New(ByVal id As Guid)
            _Id = id
        End Sub

        Private _Id As Guid
        Public Property Id As System.Guid Implements Meanstream.Portal.Core.Extensibility.IAttributeEntity.Id
            Get
                Return _Id
            End Get
            Set(ByVal value As System.Guid)
                _Id = value
            End Set
        End Property

        Private _attributes As List(Of Meanstream.Portal.Core.Extensibility.Attribute)
        Public Property Attributes() As List(Of Meanstream.Portal.Core.Extensibility.Attribute) Implements Meanstream.Portal.Core.Extensibility.IAttributeEntity.Attributes
            Get
                If _attributes Is Nothing Then
                    _attributes = New List(Of Attribute)
                End If
                Return _attributes
            End Get
            Set(ByVal value As List(Of Attribute))
                _attributes = value
            End Set
        End Property

        Public Function GetAttribute(ByVal key As String) As Meanstream.Portal.Core.Extensibility.Attribute Implements Meanstream.Portal.Core.Extensibility.IAttributeEntity.GetAttribute
            For Each Att As Attribute In Me.Attributes
                If Att.Key = key Then
                    Return Att
                End If
            Next
            Return Nothing
        End Function

        Public Property Attributes(ByVal key As String) As Object Implements Meanstream.Portal.Core.Extensibility.IAttributeEntity.Attributes
            Get
                'find attribute by name
                For Each Att As Attribute In Me.Attributes
                    If Att.Key = key Then
                        Dim AttType As Type = Att.Value.GetType
                        Return Att.Value
                    End If
                Next
                Return Nothing
            End Get
            Set(ByVal value As Object)
                'find attribute by name
                For Each Att As Attribute In Me.Attributes
                    If Att.Key = key Then
                        'remove
                        Me.Attributes.RemoveAt(Me.Attributes.IndexOf(Att))
                        Att.Value = value
                        Att.DataType = value.GetType
                        'add back to list
                        Me.Attributes.Add(Att)
                        Return
                    End If
                Next
                'if it doesnt exist create a blank attribute and add to list
                Dim Attribute As Attribute = New Attribute
                Attribute.ComponentId = Me.Id
                Attribute.Key = key
                Attribute.Value = value
                Attribute.DataType = value.GetType
                Me.Attributes.Add(Attribute)
            End Set
        End Property
    End Class

End Namespace
