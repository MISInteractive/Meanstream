
Namespace Meanstream.Core.EntityModel

    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=True, Inherited:=True)> _
    Public NotInheritable Class SerializableEntityField
        Inherits Attribute

        Private _propertyName As String = ""

        Public Property PropertyName() As String
            Get
                Return _propertyName
            End Get
            Set(ByVal value As String)
                _propertyName = value
            End Set
        End Property

        Public Sub New(ByVal propertyName As String)
            Me.PropertyName = propertyName
        End Sub
    End Class

End Namespace
