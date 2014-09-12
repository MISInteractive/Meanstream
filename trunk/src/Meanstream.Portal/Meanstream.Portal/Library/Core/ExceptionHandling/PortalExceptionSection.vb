Imports System.Configuration

Namespace Meanstream.Portal.Core.ExceptionHandling
    Public Class PortalExceptionSection
        Inherits ConfigurationSection

        ' Create a "logging" element.
        <ConfigurationProperty("logging")> _
        Public Property Logging() As LoggingElement
            Get
                Return CType(Me("logging"), LoggingElement)
            End Get
            Set(ByVal value As LoggingElement)
                Me("logging") = value
            End Set
        End Property

        ' Create a "errorPage" element.
        <ConfigurationProperty("errorPage")> _
        Public Property ErrorPage() As ErrorPageElement
            Get
                Return CType(Me("errorPage"), ErrorPageElement)
            End Get
            Set(ByVal value As ErrorPageElement)
                Me("errorPage") = value
            End Set
        End Property

        ' Create a "notification" element.
        <ConfigurationProperty("notification")> _
        Public Property Notification() As NotificationElement
            Get
                Return CType(Me("notification"), NotificationElement)
            End Get
            Set(ByVal value As NotificationElement)
                Me("notification") = value
            End Set
        End Property


    End Class
End Namespace

