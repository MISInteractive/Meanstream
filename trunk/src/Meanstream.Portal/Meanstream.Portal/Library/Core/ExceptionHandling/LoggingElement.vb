Imports System.Configuration

Namespace Meanstream.Portal.Core.ExceptionHandling
    Public Class LoggingElement
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

    End Class
End Namespace

