#Region "Copyright"

' Meanstream - http://www.misinteractive.com
' Copyright (c) 2002-2011
' by Managed Information Solutions, LLC

#End Region

#Region "Imports"

Imports System.Collections.Specialized
Imports System.Xml
Imports System.Configuration.Provider
#End Region

Namespace Meanstream.Portal.Core.Providers
    Public Class Provider

        Private ReadOnly _attributes As New NameValueCollection()
        Private ReadOnly _name As String
        Private ReadOnly _type As String

        Public Sub New(ByVal Attributes As XmlAttributeCollection)
            _name = Attributes("name").Value
            _type = Attributes("type").Value
            For Each Attribute As XmlAttribute In Attributes
                If Attribute.Name <> "name" AndAlso Attribute.Name <> "type" Then
                    _attributes.Add(Attribute.Name, Attribute.Value)
                End If
            Next
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property Type() As String
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property Attributes() As NameValueCollection
            Get
                Return _attributes
            End Get
        End Property
    End Class
End Namespace