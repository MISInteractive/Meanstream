#Region "Copyright"

' Meanstream - http://www.misinteractive.com
' Copyright (c) 2002-2011
' by Managed Information Solutions, LLC

#End Region

#Region "Imports"

Imports System.Collections
Imports System.Xml

#End Region

Namespace Meanstream.Portal.Core.Providers
    Public Class Configuration
        Private ReadOnly _Providers As New Hashtable()
        Private _defaultProvider As String

        Public ReadOnly Property DefaultProvider() As String
            Get
                Return _defaultProvider
            End Get
        End Property

        Public ReadOnly Property Providers() As Hashtable
            Get
                Return _Providers
            End Get
        End Property

        Public Shared Function GetConfiguration(ByVal provider As String) As Configuration
            Return DirectCast(System.Configuration.ConfigurationManager.GetSection("meanstream/" & provider), Configuration)
        End Function

        Friend Sub LoadConfigurationSection(ByVal node As XmlNode)
            Dim attributeCollection As XmlAttributeCollection = node.Attributes
            _defaultProvider = attributeCollection("defaultProvider").Value
            'Read child nodes
            For Each child As XmlNode In node.ChildNodes
                If child.Name = "providers" Then
                    GetProviders(child)
                End If
            Next
        End Sub

        Friend Sub GetProviders(ByVal node As XmlNode)
            For Each Provider As XmlNode In node.ChildNodes
                Select Case Provider.Name
                    Case "add"
                        Providers.Add(Provider.Attributes("name").Value, New Provider(Provider.Attributes))
                        Exit Select
                    Case "remove"
                        Providers.Remove(Provider.Attributes("name").Value)
                        Exit Select
                    Case "clear"
                        Providers.Clear()
                        Exit Select
                End Select
            Next
        End Sub
    End Class
End Namespace