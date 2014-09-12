Imports System.Reflection

Namespace Meanstream.Core.EntityModel

    Public Class Document
        Inherits EntityBase

        Private m_type As String

        Private Sub New()
        End Sub


        Public Sub New(ByVal id As Guid, ByVal type As String, ByVal serializableObject As Object)
            If id = Nothing Then
                Throw New ArgumentException("id")
            End If

            If serializableObject Is Nothing Then
                Throw New ArgumentException("serializableObject")
            End If

            If Not serializableObject.GetType.IsSerializable Then
                Throw New InvalidOperationException("serializableObject must be serializable")
            End If

            If String.IsNullOrEmpty(type) Then
                type = serializableObject.GetType.FullName
            End If

            Me.Id = id
            m_type = type
            m_document = serializableObject
        End Sub


        Public Sub New(ByVal id As Guid, ByVal serializableObject As Object)
            If id = Nothing Then
                Throw New ArgumentException("id")
            End If

            If serializableObject Is Nothing Then
                Throw New ArgumentException("serializableObject")
            End If

            If Not serializableObject.GetType.IsSerializable Then
                Throw New InvalidOperationException("serializableObject must be serializable")
            End If

            Me.Id = id
            m_type = serializableObject.GetType.FullName
            m_document = serializableObject
        End Sub


        <SerializableEntityField("JsonDocument")> _
        Public Property JsonDocument() As Object
            Get
                Return m_document
            End Get
            Set(value As Object)
                m_document = value
            End Set
        End Property
        Private m_document As Object
    End Class

End Namespace


