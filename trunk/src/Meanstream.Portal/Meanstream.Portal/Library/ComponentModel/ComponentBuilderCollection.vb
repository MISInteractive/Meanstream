﻿#Region "Copyright"

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


#End Region

Namespace Meanstream.Portal.ComponentModel
    Friend Class ComponentBuilderCollection
        Inherits Dictionary(Of String, IComponentBuilder)
        Friend Property DefaultBuilder() As IComponentBuilder
            Get
                Return m_DefaultBuilder
            End Get
            Set(ByVal value As IComponentBuilder)
                m_DefaultBuilder = value
            End Set
        End Property
        Private m_DefaultBuilder As IComponentBuilder

        Friend Sub AddBuilder(ByVal builder As IComponentBuilder, ByVal setDefault As Boolean)
            If Not ContainsKey(builder.Name) Then
                Me(builder.Name) = builder

                If setDefault AndAlso DefaultBuilder Is Nothing Then
                    DefaultBuilder = builder
                End If
            End If
        End Sub
    End Class
End Namespace