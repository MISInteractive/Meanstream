Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Text

Namespace Meanstream.Portal.Core.Utilities

    ''' <summary> 
    ''' Represents a weak reference, which references an object while still allowing 
    ''' that object to be reclaimed by garbage collection. 
    ''' </summary> 
    ''' <typeparam name="T">The type of the object that is referenced.</typeparam> 
    <Serializable()> _
    Public Class WeakReference(Of T As Class)
        Inherits WeakReference
#Region "Constructors"

        ''' <summary> 
        ''' Initializes a new instance of the Minimal.WeakReference{T} class, referencing 
        ''' the specified object. 
        ''' </summary> 
        ''' <param name="target">The object to reference.</param> 
        Public Sub New(ByVal target As T)
            MyBase.New(target)
        End Sub

        ''' <summary> 
        ''' Initializes a new instance of the WeakReference{T} class, referencing 
        ''' the specified object and using the specified resurrection tracking. 
        ''' </summary> 
        ''' <param name="target">An object to track.</param> 
        ''' <param name="trackResurrection">Indicates when to stop tracking the object. If true, the object is tracked 
        ''' after finalization; if false, the object is only tracked until finalization.</param> 
        Public Sub New(ByVal target As T, ByVal trackResurrection As Boolean)
            MyBase.New(target, trackResurrection)
        End Sub

        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

#End Region

#Region "Properties"

        ''' <summary> 
        ''' Gets or sets the object (the target) referenced by the current WeakReference{T} 
        ''' object. 
        ''' </summary> 
        Public Shadows Property Target() As T
            Get
                Return DirectCast(MyBase.Target, T)
            End Get
            Set(ByVal value As T)
                MyBase.Target = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary> 
        ''' Casts an object of the type T to a weak reference 
        ''' of T. 
        ''' </summary> 
        Public Shared Widening Operator CType(ByVal target As T) As WeakReference(Of T)
            If target Is Nothing Then
                Throw New ArgumentNullException("target")
            End If
            Return New WeakReference(Of T)(target)
        End Operator

        ''' <summary> 
        ''' Casts a weak reference to an object of the type the 
        ''' reference represents. 
        ''' </summary> 
        Public Shared Widening Operator CType(ByVal reference As WeakReference(Of T)) As T
            If reference IsNot Nothing Then
                Return reference.Target
            Else
                Return Nothing
            End If
        End Operator

#End Region
    End Class
End Namespace