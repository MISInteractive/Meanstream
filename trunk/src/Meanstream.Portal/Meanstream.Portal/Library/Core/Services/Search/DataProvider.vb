Imports Meanstream.Portal.ComponentModel

Namespace Meanstream.Portal.Core.Services.Search

    Public MustInherit Class DataProvider

        Public Sub New()
            If Not String.IsNullOrEmpty(Settings("directory")) Then
                Directory = Convert.ToString(Settings("directory"))
            Else
                Core.Instrumentation.PortalTrace.Fail(String.Format("Could not load directory attribute {0}", Me.GetType), Instrumentation.DisplayMethodInfo.FullSignature)
            End If
            If Not String.IsNullOrEmpty(Settings("maxFieldLength")) Then
                _maxFieldLength = Convert.ToInt32(Settings("maxFieldLength"))
            Else
                _maxFieldLength = 25000
            End If
        End Sub

        Public Shared Function Current() As DataProvider
            Dim provider As DataProvider = ComponentFactory.GetComponent(Of DataProvider)()
            Return provider
        End Function

        Public ReadOnly Property Settings() As Dictionary(Of String, String)
            Get
                Return ComponentFactory.GetComponentSettings(Me.GetType.FullName)
            End Get
        End Property

        Public ReadOnly Property MaxFieldLength() As Integer
            Get
                Return _maxFieldLength
            End Get
        End Property
        Private Shared _maxFieldLength As Integer

        Public Property Directory() As String
            Get
                Return _directory
            End Get
            Private Set(ByVal value As String)
                _directory = value
            End Set
        End Property
        Private _directory As String

        Public MustOverride Sub Index(ByVal documents As List(Of Document))
        Public MustOverride Sub Index(ByVal document As Document)
        Public MustOverride Sub RemoveFromIndex(ByVal url As String)
        Public MustOverride Sub DeleteIndex()
        Public MustOverride Function Search(ByVal fieldName As String, ByVal keyword As String) As List(Of Document)
        Public MustOverride Function GetAll() As List(Of Document)
    End Class

End Namespace