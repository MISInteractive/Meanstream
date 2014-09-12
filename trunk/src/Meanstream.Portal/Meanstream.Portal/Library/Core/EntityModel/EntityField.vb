
Namespace Meanstream.Core.EntityModel

    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=True, Inherited:=True)> _
    Public NotInheritable Class EntityField
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

        Private _dataType As System.Data.SqlDbType = System.Data.SqlDbType.NVarChar
        Public Property DataType As System.Data.SqlDbType
            Get
                Return _dataType
            End Get
            Set(ByVal value As System.Data.SqlDbType)
                _dataType = value
            End Set
        End Property

        Public Sub New(ByVal propertyName As String, ByVal dataType As System.Data.SqlDbType)
            Me.PropertyName = propertyName
            Me.DataType = dataType
        End Sub

    End Class

End Namespace
