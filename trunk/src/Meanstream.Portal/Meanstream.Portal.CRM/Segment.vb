
Namespace Meanstream.Portal.CRM
    Public Class Segment
        Inherits Participant
        Implements IEquatable(Of Segment)

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

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _summary As String
        Public Property Summary() As String
            Get
                Return _summary
            End Get
            Set(ByVal value As String)
                _summary = value
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Segment) As Boolean Implements System.IEquatable(Of Segment).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

