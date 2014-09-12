Imports System.Configuration

Namespace Meanstream.Portal.Core.ExceptionHandling
    Public Class ErrorPageElement
        Inherits ConfigurationElement

        Public Enum ErrorPageType
            BuiltIn
            TransferToPage
            None
        End Enum

        <ConfigurationProperty("type", DefaultValue:=ErrorPageType.None, IsRequired:=True)>
        Public Property Type() As ErrorPageType
            Get
                Return CType(Me("type"), ErrorPageType)
            End Get
            Set(ByVal value As ErrorPageType)
                Me("type") = value
            End Set
        End Property

        <ConfigurationProperty("showHTMLErrorMessage", DefaultValue:="false", IsRequired:=True)>
        Public Property ShowHTMLErrorMessage() As Boolean
            Get
                Return CType(Me("showHTMLErrorMessage"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("showHTMLErrorMessage") = value
            End Set
        End Property

        <ConfigurationProperty("path", DefaultValue:="", IsRequired:=False)>
        Public Property Path() As String
            Get
                Return CType(Me("path"), String)
            End Get
            Set(ByVal value As String)
                Me("path") = value
            End Set
        End Property

    End Class
End Namespace

