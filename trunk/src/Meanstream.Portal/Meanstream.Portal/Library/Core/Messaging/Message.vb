
Namespace Meanstream.Portal.Core.Messaging
    Public Class Message
        Inherits Meanstream.Portal.Core.Extensibility.AttributeEntity
        Implements IEquatable(Of Meanstream.Portal.Core.Messaging.Message)

        Sub New(ByVal id As Guid)
            MyBase.New(id)
            _id = id
        End Sub

        Public Enum Type
            MESSAGE_TYPE_GENERAL = 1
            MESSAGE_TYPE_GENERAL_TASK = 2
            MESSAGE_TYPE_WORKFLOW = 3
            MESSAGE_TYPE_WORKFLOW_TASK = 4
            MESSAGE_TYPE_ADMINISTRATION = 6
            MESSAGE_TYPE_HOST = 7
            MESSAGE_TYPE_SECURITY = 8
            MESSAGE_TYPE_USER = 9
        End Enum

#Region " Properties "
        Private _id As Guid
        Public Overloads ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        'Private _referenceId As Guid
        'Public Property ReferenceId() As Guid
        '    Get
        '        Return _referenceId
        '    End Get
        '    Set(ByVal value As Guid)
        '        _referenceId = value
        '    End Set
        'End Property

        Private _messageType As MessageType
        Public Property MessageType() As MessageType
            Get
                Return _messageType
            End Get
            Set(ByVal value As MessageType)
                _messageType = value
            End Set
        End Property

        Private _property As System.Net.Mail.MailPriority
        Public Property Priority() As System.Net.Mail.MailPriority
            Get
                Return _property
            End Get
            Set(ByVal value As System.Net.Mail.MailPriority)
                _property = value
            End Set
        End Property

        Private _sendEmail As Boolean = False
        Public Property SendEmail() As Boolean
            Get
                Return _sendEmail
            End Get
            Set(ByVal value As Boolean)
                _sendEmail = value
            End Set
        End Property

        Private _isQueued As Boolean = False
        Public Property IsQueued() As Boolean
            Get
                Return _isQueued
            End Get
            Set(ByVal value As Boolean)
                _isQueued = value
            End Set
        End Property

        Private _opened As Boolean = False
        Public Property Opened() As Boolean
            Get
                Return _opened
            End Get
            Set(ByVal value As Boolean)
                _opened = value
            End Set
        End Property

        Private _receivedStatus As Boolean = False
        Public Property ReceivedStatus() As Boolean
            Get
                Return _receivedStatus
            End Get
            Set(ByVal value As Boolean)
                _receivedStatus = value
            End Set
        End Property

        Private _sentStatus As Boolean = False
        Public Property SentStatus() As Boolean
            Get
                Return _sentStatus
            End Get
            Set(ByVal value As Boolean)
                _sentStatus = value
            End Set
        End Property

        Private _body As String
        Public Property Body() As String
            Get
                Return _body
            End Get
            Set(ByVal value As String)
                _body = value
            End Set
        End Property

        Private _subject As String
        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        Private _dateOpened As Date
        Public Property DateOpened() As Date
            Get
                Return _dateOpened
            End Get
            Set(ByVal value As Date)
                _dateOpened = value
            End Set
        End Property

        Private _dateRecieved As Date
        Public Property DateRecieved() As Date
            Get
                Return _dateRecieved
            End Get
            Set(ByVal value As Date)
                _dateRecieved = value
            End Set
        End Property

        Private _dateSent As Date
        Public Property DateSent() As Date
            Get
                Return _dateSent
            End Get
            Set(ByVal value As Date)
                _dateSent = value
            End Set
        End Property

        Private _sentTo As Guid
        Public Property SentTo() As Guid
            Get
                Return _sentTo
            End Get
            Set(ByVal value As Guid)
                _sentTo = value
            End Set
        End Property

        Private _sentFrom As Guid
        Public Property SentFrom() As Guid
            Get
                Return _sentFrom
            End Get
            Set(ByVal value As Guid)
                _sentFrom = value
            End Set
        End Property
#End Region

        Public Overloads Function Equals(ByVal other As Message) As Boolean Implements System.IEquatable(Of Message).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

