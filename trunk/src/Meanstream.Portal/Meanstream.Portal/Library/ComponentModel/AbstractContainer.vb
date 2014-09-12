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

#End Region

Namespace Meanstream.Portal.ComponentModel
    Public MustInherit Class AbstractContainer
        Implements IContainer

#Region "IContainer Members"

        Public MustOverride ReadOnly Property Name() As String Implements IContainer.Name

        Public MustOverride Function GetComponent(ByVal name As String) As Object Implements IContainer.GetComponent

        Public MustOverride Function GetComponent(ByVal name As String, ByVal contractType As Type) As Object Implements IContainer.GetComponent

        Public MustOverride Function GetComponent(ByVal contractType As Type) As Object

        Public Overridable Function GetComponent(Of TContract)() As TContract Implements IContainer.GetComponent
            Return DirectCast(GetComponent(GetType(TContract)), TContract)
        End Function

        Public Overridable Function GetComponent(Of TContract)(ByVal name As String) As TContract Implements IContainer.GetComponent
            Return DirectCast(GetComponent(name, GetType(TContract)), TContract)
        End Function

        Public MustOverride Function GetComponentList(ByVal contractType As Type) As String() Implements IContainer.GetComponentList

        Public Overridable Function GetComponentList(Of TContract)() As String() Implements IContainer.GetComponentList
            Return GetComponentList(GetType(TContract))
        End Function

        Public MustOverride Function GetComponentSettings(ByVal name As String) As IDictionary Implements IContainer.GetComponentSettings

        Public Overridable Function GetComponentSettings(ByVal component As Type) As IDictionary Implements IContainer.GetComponentSettings
            Return GetComponentSettings(component.FullName)
        End Function

        Public Function GetComponentSettings(Of TComponent)() As IDictionary Implements IContainer.GetComponentSettings
            Return GetComponentSettings(GetType(TComponent).FullName)
        End Function

        Public MustOverride Sub RegisterComponent(ByVal name As String, ByVal contractType As Type, ByVal componentType As Type, ByVal lifestyle As ComponentLifeStyleType) Implements IContainer.RegisterComponent

        'Public Overridable Sub RegisterComponent(ByVal name As String, ByVal contractType As Type, ByVal componentType As Type) Implements IContainer.RegisterComponent
        '    RegisterComponent(name, contractType, componentType, ComponentLifeStyleType.Singleton)
        'End Sub

        Public Overridable Sub RegisterComponent(ByVal name As String, ByVal componentType As Type) Implements IContainer.RegisterComponent
            RegisterComponent(name, componentType, componentType, ComponentLifeStyleType.Singleton)
        End Sub

        'Public Overridable Sub RegisterComponent(ByVal contractType As Type, ByVal componentType As Type) Implements IContainer.RegisterComponent
        '    RegisterComponent(componentType.FullName, contractType, componentType, ComponentLifeStyleType.Singleton)
        'End Sub

        'Public Overridable Sub RegisterComponent(ByVal contractType As Type, ByVal componentType As Type, ByVal lifestyle As ComponentLifeStyleType) Implements IContainer.RegisterComponent
        '    RegisterComponent(componentType.FullName, contractType, componentType, lifestyle)
        'End Sub

        'Public Overridable Sub RegisterComponent(ByVal componentType As Type) Implements IContainer.RegisterComponent
        '    RegisterComponent(componentType.FullName, componentType, componentType, ComponentLifeStyleType.Singleton)
        'End Sub

        'Public Overridable Sub RegisterComponent(Of TComponent As Class)() Implements IContainer.RegisterComponent
        '    RegisterComponent(GetType(TComponent))
        'End Sub

        'Public Overridable Sub RegisterComponent(Of TComponent As Class)(ByVal name As String) Implements IContainer.RegisterComponent
        '    RegisterComponent(name, GetType(TComponent), GetType(TComponent), ComponentLifeStyleType.Singleton)
        'End Sub

        'Public Overridable Sub RegisterComponent(Of TComponent As Class)(ByVal name As String, ByVal lifestyle As ComponentLifeStyleType) Implements IContainer.RegisterComponent
        '    RegisterComponent(name, GetType(TComponent), GetType(TComponent), lifestyle)
        'End Sub

        'Public Overridable Sub RegisterComponent(Of TContract, TComponent As Class)()
        '    RegisterComponent(GetType(TContract), GetType(TComponent))
        'End Sub

        'Public Overridable Sub RegisterComponent(Of TContract, TComponent As Class)(ByVal name As String)
        '    RegisterComponent(name, GetType(TContract), GetType(TComponent), ComponentLifeStyleType.Singleton)
        'End Sub

        'Public Overridable Sub RegisterComponent(Of TContract, TComponent As Class)(ByVal name As String, ByVal lifestyle As ComponentLifeStyleType)
        '    RegisterComponent(name, GetType(TContract), GetType(TComponent), lifestyle)
        'End Sub

        Public MustOverride Sub RegisterComponentSettings(ByVal name As String, ByVal dependencies As IDictionary) Implements IContainer.RegisterComponentSettings

        'Public Overridable Sub RegisterComponentSettings(ByVal component As Type, ByVal dependencies As IDictionary)
        '    RegisterComponentSettings(component.FullName, dependencies)
        'End Sub

        'Public Overridable Sub RegisterComponentSettings(Of TComponent)(ByVal dependencies As IDictionary)
        '    RegisterComponentSettings(GetType(TComponent).FullName, dependencies)
        'End Sub

        'Public MustOverride Sub RegisterComponentInstance(ByVal name As String, ByVal contractType As Type, ByVal instance As Object)

        'Public Sub RegisterComponentInstance(ByVal name As String, ByVal instance As Object)
        '    RegisterComponentInstance(name, instance.[GetType](), instance)
        'End Sub

        'Public Sub RegisterComponentInstance(Of TContract)(ByVal instance As Object)
        '    RegisterComponentInstance(instance.[GetType]().FullName, GetType(TContract), instance)
        'End Sub

        'Public Sub RegisterComponentInstance(Of TContract)(ByVal name As String, ByVal instance As Object)
        '    RegisterComponentInstance(name, GetType(TContract), instance)
        'End Sub

#End Region

        Public Overridable Function GetCustomDependencies(Of TComponent)() As IDictionary
            Return GetComponentSettings(GetType(TComponent).FullName)
        End Function


    End Class
End Namespace