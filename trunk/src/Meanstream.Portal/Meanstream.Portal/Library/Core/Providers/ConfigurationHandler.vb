#Region "Copyright"

' Meanstream - http://www.misinteractive.com
' Copyright (c) 2002-2011
' by Managed Information Solutions, LLC

#End Region

#Region "Imports"

Imports System.Configuration
Imports System.Xml

#End Region

Namespace Meanstream.Portal.Core.Providers

    Friend Class ConfigurationHandler
        Implements System.Configuration.IConfigurationSectionHandler

        Public Overridable Function Create(ByVal parent As Object, ByVal context As Object, ByVal node As XmlNode) As Object Implements System.Configuration.IConfigurationSectionHandler.Create
            Dim objProviderConfiguration = New Configuration()
            objProviderConfiguration.LoadConfigurationSection(node)
            Return objProviderConfiguration
        End Function

    End Class
End Namespace