Imports Microsoft.VisualBasic

Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions


Namespace Meanstream.Portal.Core.Utilities
    Public Class CssParser
        Private _styleSheets As List(Of String)
        Private _scc As SortedList(Of String, StyleClass)

        Public Property Styles() As SortedList(Of String, StyleClass)
            Get
                Return Me._scc
            End Get
            Set(ByVal value As SortedList(Of String, StyleClass))
                Me._scc = value
            End Set
        End Property

        Public Sub New()
            Me._styleSheets = New List(Of String)()
            Me._scc = New SortedList(Of String, StyleClass)()
        End Sub

        Public Sub AddStyleSheet(ByVal path As String)
            Me._styleSheets.Add(path)
            ProcessStyleSheet(path)
        End Sub

        Public Function GetStyleSheet(ByVal index As Integer) As String
            Return Me._styleSheets(index)
        End Function

        Private Sub ProcessStyleSheet(ByVal path As String)
            Dim content As String = CleanUp(File.ReadAllText(path))
            Dim parts As String() = content.Split("}"c)

            For Each s As String In parts
                If CleanUp(s).IndexOf("{"c) > -1 Then
                    FillStyleClass(s)
                End If
            Next
        End Sub

        Private Sub FillStyleClass(ByVal s As String)
            Dim sc As StyleClass = Nothing
            Dim parts As String() = s.Split("{"c)
            Dim styleName As String = CleanUp(parts(0)).Trim().ToLower()
            If Me._scc.ContainsKey(styleName) Then
                sc = Me._scc(styleName)
                Me._scc.Remove(styleName)
            Else
                sc = New StyleClass()
            End If
            sc.Name = styleName
            Dim atrs As String() = CleanUp(parts(1)).Replace("}", "").Split(";"c)
            For Each a As String In atrs
                If a.Contains(":") Then
                    Dim _key As String = a.Split(":"c)(0).Trim().ToLower()
                    If sc.Attributes.ContainsKey(_key) Then
                        sc.Attributes.Remove(_key)
                    End If
                    sc.Attributes.Add(_key, a.Split(":"c)(1).Trim().ToLower())
                End If
            Next
            Me._scc.Add(sc.Name, sc)
        End Sub

        Private Function CleanUp(ByVal s As String) As String
            Dim temp As String = s
            Dim reg As String = "(/\*(.|[" & vbCr & vbLf & "])*?\*/)|(//.*)"
            Dim r As New Regex(reg)
            temp = r.Replace(temp, "")
            temp = temp.Replace(vbCr, "").Replace(vbLf, "")
            Return temp
        End Function
        Public Class StyleClass
            Private _name As String = String.Empty
            Public Property Name() As String
                Get
                    Return _name
                End Get
                Set(ByVal value As String)
                    _name = value
                End Set
            End Property
            Private _attributes As New SortedList(Of String, String)()
            Public Property Attributes() As SortedList(Of String, String)
                Get
                    Return _attributes
                End Get
                Set(ByVal value As SortedList(Of String, String))
                    _attributes = value
                End Set
            End Property
        End Class
    End Class
End Namespace