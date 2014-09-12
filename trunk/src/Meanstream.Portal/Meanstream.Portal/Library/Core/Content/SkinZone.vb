
Namespace Meanstream.Portal.Core.Content
    Public Class SkinZone
        Implements IEquatable(Of Meanstream.Portal.Core.Content.SkinZone)

        Sub New(ByVal id As Guid)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _skinId As Guid
        Public Property SkinId() As Guid
            Get
                Return _skinId
            End Get
            Set(ByVal value As Guid)
                _skinId = value
            End Set
        End Property

        Private _pane As String
        Public Property Pane() As String
            Get
                Return _pane
            End Get
            Set(ByVal value As String)
                _pane = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As SkinZone) As Boolean Implements System.IEquatable(Of SkinZone).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

