Namespace Meanstream.Portal.Core.Content
    Public Class Skin
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Content.Skin)

        Sub New(ByVal id As Guid)
            MyBase.New(id)
            _id = id
        End Sub

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Private _portalId As Guid
        Public Property PortalId() As Guid
            Get
                Return _portalId
            End Get
            Set(ByVal value As Guid)
                _portalId = value
            End Set
        End Property

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _path As String
        Public Property Path() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

        Private _zones As List(Of Meanstream.Portal.Core.Content.SkinZone) = Nothing
        Public Property Zones() As List(Of Meanstream.Portal.Core.Content.SkinZone)
            Get
                Return _zones
            End Get
            Set(ByVal value As List(Of Meanstream.Portal.Core.Content.SkinZone))
                _zones = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Skin) As Boolean Implements System.IEquatable(Of Skin).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

