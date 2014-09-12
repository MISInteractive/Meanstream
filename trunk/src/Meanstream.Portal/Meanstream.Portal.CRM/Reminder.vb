
Namespace Meanstream.Portal.CRM
    Public Class Reminder
        Implements IEquatable(Of Reminder)

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

        Private _userId As Guid
        Public Property UserId() As Guid
            Get
                Return _userId
            End Get
            Set(ByVal value As Guid)
                _userId = value
            End Set
        End Property

        Private _type As ReminderType
        Public Property Type() As ReminderType
            Get
                Return _type
            End Get
            Set(ByVal value As ReminderType)
                _type = value
            End Set
        End Property

        Private _preference As String
        Public Property Preference() As String
            Get
                Return _preference
            End Get
            Set(ByVal value As String)
                _preference = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Reminder) As Boolean Implements System.IEquatable(Of Reminder).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

