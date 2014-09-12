
Namespace Meanstream.Portal.Core.Services.Data

    <Serializable()>
    Public Class Field
        Implements IEquatable(Of Field)

        Private Sub New()

        End Sub

        Public Sub New(ByVal id As Guid)
            _id = id
        End Sub

#Region "Properties"
        Private _id As Guid
        Public Property Id As System.Guid
            Get
                Return _id
            End Get
            Set(ByVal value As System.Guid)
                _id = value
            End Set
        End Property

        Private _name As String = ""
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value.Replace(" ", "_")
            End Set
        End Property

        Private _dataType As String = ""
        Public Property DataType As String
            Get
                Return _dataType
            End Get
            Set(ByVal value As String)
                _dataType = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Field) As Boolean Implements System.IEquatable(Of Field).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class
End Namespace

