
Namespace Meanstream.Core.EntityModel

    <Serializable()>
    Public MustInherit Class EntityBase
        Implements IEntity
        Implements IEquatable(Of EntityBase)

#Region " Properties "

        Private _id As Guid
        <EntityField("id", System.Data.SqlDbType.UniqueIdentifier)> _
        Public Property Id As System.Guid Implements IEntity.Id
            Get
                Return _id
            End Get
            Set(ByVal value As Guid)
                _id = value
            End Set
        End Property

#End Region

        Public Overloads Function Equals(ByVal other As EntityBase) As Boolean Implements System.IEquatable(Of EntityBase).Equals
            If Me.Id = other.Id Or Me.GetType = other.GetType Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace

