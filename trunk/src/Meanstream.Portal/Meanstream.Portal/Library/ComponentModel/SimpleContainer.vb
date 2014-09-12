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

Imports System.Collections
Imports System.Collections.Generic


#End Region

Namespace Meanstream.Portal.ComponentModel
    Public Class SimpleContainer
        Inherits AbstractContainer
        Private ReadOnly _name As String
        Private ReadOnly _componentBuilders As New ComponentBuilderCollection()

        Private ReadOnly _componentDependencies As New Dictionary(Of String, IDictionary)()

        Private ReadOnly _componentTypes As New ComponentTypeCollection()

        Private ReadOnly _registeredComponents As New Dictionary(Of Type, String)()

#Region "Constructors"

        ''' <summary>
        '''   Initializes a new instance of the SimpleContainer class.
        ''' </summary>
        Public Sub New()
            Me.New(String.Format("Container_{0}", Guid.NewGuid()))
        End Sub

        ''' <summary>
        '''   Initializes a new instance of the SimpleContainer class.
        ''' </summary>
        ''' <param name = "name"></param>
        Public Sub New(ByVal name As String)
            _name = name
        End Sub

#End Region

#Region "Private Methods"

        Private Sub AddBuilder(ByVal contractType As Type, ByVal builder As IComponentBuilder)
            Dim componentType As ComponentType = GetComponentType(contractType)
            If componentType IsNot Nothing Then
                Dim builders As ComponentBuilderCollection = componentType.ComponentBuilders

                'Using builders.GetWriteLock()
                builders.AddBuilder(builder, True)
                'End Using

                'Using _componentBuilders.GetWriteLock()
                _componentBuilders.AddBuilder(builder, False)
                'End Using
            End If
        End Sub

        Private Sub AddComponentType(ByVal contractType As Type)
            Dim componentType As ComponentType = GetComponentType(contractType)

            If componentType Is Nothing Then
                componentType = New ComponentType(contractType)

                'Using _componentTypes.GetWriteLock()
                _componentTypes(componentType.BaseType) = componentType
                'End Using
            End If
        End Sub

        Private Overloads Function GetComponent(ByVal builder As IComponentBuilder) As Object
            Dim component As Object
            If builder Is Nothing Then
                component = Nothing
            Else
                component = builder.BuildComponent()
            End If
            Return component
        End Function

        Private Function GetComponentBuilder(ByVal name As String) As IComponentBuilder
            Dim builder As IComponentBuilder

            'Using _componentBuilders.GetReadLock()
            _componentBuilders.TryGetValue(name, builder)
            'End Using

            Return builder
        End Function

        Private Function GetDefaultComponentBuilder(ByVal componentType As ComponentType) As IComponentBuilder
            Dim builder As IComponentBuilder

            'Using componentType.ComponentBuilders.GetReadLock()
            builder = componentType.ComponentBuilders.DefaultBuilder
            'End Using

            Return builder
        End Function

        Private Function GetComponentType(ByVal contractType As Type) As ComponentType
            Dim componentType As ComponentType

            'Using _componentTypes.GetReadLock()
            _componentTypes.TryGetValue(contractType, componentType)
            'End Using

            Return componentType
        End Function

        Public Overrides Sub RegisterComponent(ByVal name As String, ByVal type As Type)
            'Using _registeredComponents.GetWriteLock()
            _registeredComponents(type) = name
            'End Using
        End Sub

#End Region

        Public Overrides ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public Overrides Function GetComponent(ByVal name As String) As Object
            Dim builder As IComponentBuilder = GetComponentBuilder(name)

            Return GetComponent(builder)
        End Function

        Public Overrides Function GetComponent(ByVal contractType As Type) As Object
            Dim componentType As ComponentType = GetComponentType(contractType)
            Dim component As Object = Nothing

            If componentType IsNot Nothing Then
                Dim builderCount As Integer

                'Using componentType.ComponentBuilders.GetReadLock()
                builderCount = componentType.ComponentBuilders.Count
                'End Using

                If builderCount > 0 Then
                    Dim builder As IComponentBuilder = GetDefaultComponentBuilder(componentType)

                    component = GetComponent(builder)
                End If
            End If

            Return component
        End Function

        Public Overrides Function GetComponent(ByVal name As String, ByVal contractType As Type) As Object
            Dim componentType As ComponentType = GetComponentType(contractType)
            Dim component As Object = Nothing

            If componentType IsNot Nothing Then
                Dim builder As IComponentBuilder = GetComponentBuilder(name)

                component = GetComponent(builder)
            End If
            Return component
        End Function

        Public Overrides Function GetComponentList(ByVal contractType As Type) As String()
            Dim components = New List(Of String)()

            'Using _registeredComponents.GetReadLock()
            For Each kvp As KeyValuePair(Of Type, String) In _registeredComponents
                If ReferenceEquals(kvp.Key.BaseType, contractType) Then
                    components.Add(kvp.Value)
                End If
            Next
            'End Using
            Return components.ToArray()
        End Function

        Public Overrides Function GetComponentSettings(ByVal name As String) As IDictionary
            Dim settings As IDictionary
            'Using _componentDependencies.GetReadLock()
            settings = _componentDependencies(name)
            'End Using
            Return settings
        End Function

        Public Overrides Sub RegisterComponent(ByVal name As String, ByVal contractType As Type, ByVal type As Type, ByVal lifestyle As ComponentLifeStyleType)
            AddComponentType(contractType)

            Dim builder As IComponentBuilder = Nothing
            Select Case lifestyle
                Case ComponentLifeStyleType.Transient
                    builder = New TransientComponentBuilder(name, type)
                    Exit Select
                Case ComponentLifeStyleType.Singleton
                    builder = New SingletonComponentBuilder(name, type)
                    Exit Select
            End Select
            AddBuilder(contractType, builder)

            RegisterComponent(name, type)
        End Sub

        'Public Overrides Sub RegisterComponentInstance(ByVal name As String, ByVal contractType As Type, ByVal instance As Object)
        '    AddComponentType(contractType)

        '    AddBuilder(contractType, New InstanceComponentBuilder(name, instance))
        'End Sub

        Public Overrides Sub RegisterComponentSettings(ByVal name As String, ByVal dependencies As IDictionary)
            _componentDependencies(name) = dependencies
        End Sub
    End Class
End Namespace