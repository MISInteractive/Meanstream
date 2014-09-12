
Namespace Meanstream.Portal.Providers.PortalDynamicsProvider
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=True, Inherited:=False)> _
    Public Class StoredProcParameter
        Inherits Attribute

        Private _storedProcParameter As String

        Public Property StoredProcParameter() As String
            Get
                Return _storedProcParameter
            End Get
            Set(ByVal value As String)
                _storedProcParameter = value
            End Set
        End Property

        Public Sub New(ByVal StoredProcParameter As String)
            Me.StoredProcParameter = StoredProcParameter
        End Sub

    End Class
End Namespace

