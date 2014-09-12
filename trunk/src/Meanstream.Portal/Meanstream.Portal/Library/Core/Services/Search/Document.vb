Namespace Meanstream.Portal.Core.Services.Search
    Public Class Document

        Private _Fields As List(Of Field) = New List(Of Field)
        ReadOnly Property Fields() As List(Of Field)
            Get
                Return _Fields
            End Get
        End Property

        Public Sub Add(ByVal Field As Field)
            Me._Fields.Add(Field)
        End Sub

    End Class
End Namespace
