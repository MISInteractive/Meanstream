
Namespace Meanstream.Portal.Core.Services.Search
    Public Class Fields
        Inherits List(Of Field)
    End Class

    Public Class Field

        Sub New(ByVal Name As String, ByVal Value As String, ByVal Store As Boolean, ByVal Index As Boolean)
            Me._Name = Name
            Me._Value = Value
            Me._Store = Store
            Me._Index = Index
        End Sub

        Private _Name As String
        ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Private _Value As String
        ReadOnly Property Value() As String
            Get
                Return _Value
            End Get
        End Property

        Private _Store As Boolean
        ReadOnly Property Store() As Boolean
            Get
                Return _Store
            End Get
        End Property

        Private _Index As Boolean
        ReadOnly Property Index() As Boolean
            Get
                Return _Index
            End Get
        End Property
    End Class
End Namespace

