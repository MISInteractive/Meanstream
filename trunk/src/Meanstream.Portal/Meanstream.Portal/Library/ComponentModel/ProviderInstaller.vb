#Region "Copyright"

' 
' DotNetNuke� - http://www.dotnetnuke.com
' Copyright (c) 2002-2011
' by DotNetNuke Corporation
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.

#End Region

#Region "Usings"

Imports System.Collections.Generic
Imports System.Configuration
Imports System.Web.Compilation
Imports Meanstream.Portal.Core.Providers

#End Region

Namespace Meanstream.Portal.ComponentModel
    Public Class ProviderInstaller
        Implements IComponentInstaller

        Private ReadOnly _ComponentLifeStyle As ComponentLifeStyleType
        Private ReadOnly _ProviderInterface As Type
        Private ReadOnly _ProviderType As String

        Public Sub New(ByVal providerType As String, ByVal providerInterface As Type)
            _ComponentLifeStyle = ComponentLifeStyleType.Singleton
            _ProviderType = providerType
            _ProviderInterface = providerInterface
        End Sub

        Public Sub New(ByVal providerType As String, ByVal providerInterface As Type, ByVal lifeStyle As ComponentLifeStyleType)
            _ComponentLifeStyle = lifeStyle
            _ProviderType = providerType
            _ProviderInterface = providerInterface
        End Sub

#Region "IComponentInstaller Members"

        Public Sub InstallComponents(ByVal container As IContainer) Implements IComponentInstaller.InstallComponents
            Dim config As Core.Providers.Configuration = Core.Providers.Configuration.GetConfiguration(_ProviderType)
            'Register the default provider first (so it is the first component registered for its service interface
            If config IsNot Nothing Then
                InstallProvider(container, DirectCast(config.Providers(config.DefaultProvider), Provider))

                'Register the others
                For Each provider As Provider In config.Providers.Values
                    'Skip the default because it was registered above
                    If Not config.DefaultProvider.Equals(provider.Name, StringComparison.OrdinalIgnoreCase) Then
                        InstallProvider(container, provider)
                    End If
                Next
            End If
        End Sub

#End Region

        Private Sub InstallProvider(ByVal container As IContainer, ByVal provider As Provider)
            If provider IsNot Nothing Then
                'Get the provider type
                Dim type As Type = BuildManager.[GetType](provider.Type, False, True)
                If type Is Nothing Then
                    Core.Instrumentation.PortalTrace.Fail(String.Format("Could not load provider {0}", provider.Type), Core.Instrumentation.DisplayMethodInfo.FullSignature)
                Else
                    'Register the component
                    container.RegisterComponent(provider.Name, _ProviderInterface, type, _ComponentLifeStyle)

                    'Load the settings into a dictionary
                    Dim settingsDict = New Dictionary(Of String, String)()
                    settingsDict.Add("providerName", provider.Name)
                    For Each key As String In provider.Attributes.Keys
                        settingsDict.Add(key, provider.Attributes.[Get](key))
                    Next
                    'Register the settings as dependencies
                    container.RegisterComponentSettings(type.FullName, settingsDict)
                End If
            End If
        End Sub

    End Class
End Namespace