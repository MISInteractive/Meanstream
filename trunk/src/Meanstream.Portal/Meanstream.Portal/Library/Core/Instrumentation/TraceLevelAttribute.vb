
Namespace Meanstream.Portal.Core.Instrumentation
    <AttributeUsage(AttributeTargets.[Class], AllowMultiple:=False, Inherited:=False)> _
    Public Class TraceLevelAttribute
        Inherits Attribute
        Private _categoryEnum As TraceLevel
        Private _categoryString As String

        Public ReadOnly Property Name() As String
            Get
                Return If(_categoryString, [Enum].GetName(GetType(TraceLevel), _categoryEnum))
            End Get
        End Property

        Public ReadOnly Property Value() As String
            Get
                Return DirectCast([Enum].Parse(GetType(TraceLevel), _categoryString), Integer)
            End Get
        End Property

        Public Sub New(ByVal category As TraceLevel)
            _categoryEnum = category
        End Sub

        Public Sub New(ByVal category As String)
            _categoryString = category
        End Sub

    End Class
End Namespace
