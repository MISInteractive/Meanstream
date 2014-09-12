
Namespace Meanstream.Portal.CRM
    Public Class Participant
        Inherits Meanstream.Portal.CRM.Extensibility.AttributeEntity
        Implements IEquatable(Of Participant)
        
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

        Private _userId As Guid
        Public Property UserId() As Guid
            Get
                Return _userId
            End Get
            Set(ByVal value As Guid)
                _userId = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Participant) As Boolean Implements System.IEquatable(Of Participant).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class
End Namespace

