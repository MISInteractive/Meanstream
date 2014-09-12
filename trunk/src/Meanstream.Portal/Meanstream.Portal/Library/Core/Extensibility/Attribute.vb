
Namespace Meanstream.Portal.Core.Extensibility
    Public Class Attribute
        Implements IEquatable(Of Meanstream.Portal.Core.Extensibility.Attribute)

        Sub New()
        End Sub

        Sub New(ByVal componentId As Guid, ByVal key As String, ByVal value As String, ByVal dataType As Type)
            _componentId = componentId
            _key = key
            _value = value
            _dataType = dataType
        End Sub

#Region " Properties "
        Private _componentId As Guid
        Public Property ComponentId() As Guid
            Get
                Return _componentId
            End Get
            Set(ByVal value As Guid)
                _componentId = value
            End Set
        End Property

        Private _key As String = ""
        Public Property Key() As String
            Get
                Return _key
            End Get
            Set(ByVal value As String)
                _key = value
            End Set
        End Property

        Private _value As Object
        Public Property Value() As Object
            Get
                Return _value
            End Get
            Set(ByVal value As Object)
                _value = value
            End Set
        End Property

        Private _dataType As Type
        Public Property DataType() As Type
            Get
                Return _dataType
            End Get
            Set(ByVal value As Type)
                _dataType = value
            End Set
        End Property
#End Region


        Public Overloads Function Equals(ByVal other As Attribute) As Boolean Implements System.IEquatable(Of Attribute).Equals
            If Me.Key = other.Key Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

