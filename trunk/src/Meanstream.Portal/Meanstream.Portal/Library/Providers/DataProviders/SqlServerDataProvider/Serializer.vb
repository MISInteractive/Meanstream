
Namespace Meanstream.Core.Providers.SqlServerDataProvider

    ''' <summary>
    ''' This is a generic class that supports
    ''' serialization and deserialization for any type
    ''' </summary>
    ''' <typeparam name="T">class type</typeparam>
    ''' <remarks></remarks>
    Public Class Serializer(Of T)

#Region "Serialization support"
        ''' <summary>
        ''' Serializer
        ''' </summary>
        Private _serializer As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        'Private _serializer As New System.Xml.Serialization.XmlSerializer(GetType(T))
        ''' <summary>
        ''' Serialize object into string
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Serialize(ByVal obj As T) As String
            'serialise to a memory stream, then read into a string
            Try
                Dim result As String = Nothing
                If obj IsNot Nothing Then
                    Using ms As New System.IO.MemoryStream
                        _serializer.Serialize(ms, obj)
                        result = System.Convert.ToBase64String(ms.ToArray)
                        ms.Close()
                    End Using
                End If
                Return result

            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Deserialize string into an instance of T
        ''' </summary>
        ''' <param name="s"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Deserialize(ByVal s As String) As T
            Try
                'default to no object
                If Not String.IsNullOrEmpty(s) Then
                    Dim sr() As Byte = System.Convert.FromBase64String(s)
                    Using ms As New System.IO.MemoryStream(sr)
                        Return CType(_serializer.Deserialize(ms), T)
                        ms.Close()
                    End Using
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Throw
            End Try
        End Function

#End Region

    End Class

End Namespace

