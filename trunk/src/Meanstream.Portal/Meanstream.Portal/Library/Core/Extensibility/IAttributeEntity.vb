Namespace Meanstream.Portal.Core.Extensibility
    Public Interface IAttributeEntity
        Property Id As Guid
        Property Attributes() As List(Of Attribute)
        Property Attributes(ByVal Name As String) As Object
        Function GetAttribute(ByVal Name As String) As Attribute
    End Interface
End Namespace
