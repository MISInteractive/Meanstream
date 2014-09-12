Imports System.Configuration

Namespace Meanstream.Portal.Core.ExceptionHandling
    Public Class NotificationElement
        Inherits ConfigurationElement

        <ConfigurationProperty("enabled", DefaultValue:="false", IsRequired:=True)>
        Public Property Enabled() As Boolean
            Get
                Return CType(Me("enabled"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("enabled") = value
            End Set
        End Property

        <ConfigurationProperty("subject", DefaultValue:="", IsRequired:=True)>
        Public Property Subject() As String
            Get
                Return CType(Me("subject"), String)
            End Get
            Set(ByVal value As String)
                Me("subject") = value
            End Set
        End Property

        <ConfigurationProperty("priority", DefaultValue:=System.Net.Mail.MailPriority.High, IsRequired:=True)>
        Public Property Priority() As System.Net.Mail.MailPriority
            Get
                Return CType(Me("priority"), System.Net.Mail.MailPriority)
            End Get
            Set(ByVal value As System.Net.Mail.MailPriority)
                Me("priority") = value
            End Set
        End Property

        <ConfigurationProperty("to", DefaultValue:="", IsRequired:=True)>
        Public Property ToAddresses() As String
            Get
                Return CType(Me("to"), String)
            End Get
            Set(ByVal value As String)
                Me("to") = value
            End Set
        End Property

        <ConfigurationProperty("from", DefaultValue:="", IsRequired:=True)>
        Public Property FromAddress() As String
            Get
                Return CType(Me("from"), String)
            End Get
            Set(ByVal value As String)
                Me("from") = value
            End Set
        End Property

    End Class
End Namespace

