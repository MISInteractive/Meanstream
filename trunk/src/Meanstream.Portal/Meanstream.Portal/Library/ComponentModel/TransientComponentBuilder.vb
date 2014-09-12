#Region "Copyright"

' 
' DotNetNuke� - http://www.dotnetnuke.com
' Copyright (c) 2002-2011
' by DotNetNuke Corporation
' 

#End Region

#Region "Imports"

#End Region

Namespace Meanstream.Portal.ComponentModel
    Friend Class TransientComponentBuilder
        Implements IComponentBuilder
        Private ReadOnly _Name As String
        Private ReadOnly _Type As Type

        ''' <summary>
        ''' Initializes a new instance of the TransientComponentBuilder class.
        ''' </summary>
        ''' <param name="name">The name of the component</param>
        ''' <param name="type">The type of the component</param>
        Public Sub New(ByVal name As String, ByVal type As Type)
            _Name = name
            _Type = type
        End Sub

#Region "IComponentBuilder Members"

        Public Function BuildComponent() As Object Implements IComponentBuilder.BuildComponent
            Return Activator.CreateInstance(_Type)
        End Function

        Public ReadOnly Property Name() As String Implements IComponentBuilder.Name
            Get
                Return _Name
            End Get
        End Property

#End Region
    End Class
End Namespace