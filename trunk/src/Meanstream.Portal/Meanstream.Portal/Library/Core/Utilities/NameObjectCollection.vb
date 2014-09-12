Imports Microsoft.VisualBasic
Imports System.Collections.Specialized

Namespace Meanstream.Portal.Core.Utilities
    Public Class NameObjectCollection
        Inherits NameObjectCollectionBase
#Region "Constructors"

        ' Creates an empty collection. 
        Public Sub New()
        End Sub

        ' Adds elements from an IDictionary into the new collection. 
        Public Sub New(ByVal d As IDictionary, ByVal bReadOnly As [Boolean])
            For Each de As DictionaryEntry In d
                Me.BaseAdd(DirectCast(de.Key, [String]), de.Value)
            Next
            Me.IsReadOnly = bReadOnly
        End Sub

#End Region

#Region "Properties"

        ' Gets a String array that contains all the keys in the collection. 
        Public ReadOnly Property AllKeys() As [String]()
            Get
                Return (Me.BaseGetAllKeys())
            End Get
        End Property

        ' Gets a String array that contains all the values in the collection. 
        Public ReadOnly Property AllStringValues() As [String]()
            Get
                Return DirectCast(Me.BaseGetAllValues(GetType(String)), [String]())
            End Get
        End Property

        ' Gets an Object array that contains all the values in the collection. 
        Public ReadOnly Property AllValues() As Array
            Get
                Return (Me.BaseGetAllValues())
            End Get
        End Property

        ' Gets a value indicating if the collection contains keys that are not null. 
        Public ReadOnly Property HasKeys() As [Boolean]
            Get
                Return (Me.BaseHasKeys())
            End Get
        End Property

#End Region

#Region "Indexers"

        ' Gets a key-and-value pair (DictionaryEntry) using an index. 
        Default Public ReadOnly Property Item(ByVal index As Integer) As DictionaryEntry
            Get
                Return (New DictionaryEntry(Me.BaseGetKey(index), Me.BaseGet(index)))
            End Get
        End Property

        ' Gets or sets the value associated with the specified key. 
        Default Public Property Item(ByVal key As [String]) As [Object]
            Get
                Return (Me.BaseGet(key))
            End Get
            Set(ByVal value As [Object])
                Me.BaseSet(key, value)
            End Set
        End Property

#End Region

#Region "Methods"

        ' Adds an entry to the collection. 
        Public Sub Add(ByVal key As [String], ByVal value As [Object])
            Me.BaseAdd(key, value)
        End Sub

        ' Clears all the elements in the collection. 
        Public Sub Clear()
            Me.BaseClear()
        End Sub

        Public Function [Get](ByVal key As String) As Object
            Return Me(key)
        End Function

        ' Removes an entry with the specified key from the collection. 
        Public Sub Remove(ByVal key As [String])
            Me.BaseRemove(key)
        End Sub

        ' Removes an entry in the specified index from the collection. 
        Public Sub Remove(ByVal index As Integer)
            Me.BaseRemoveAt(index)
        End Sub

#End Region
    End Class

End Namespace
