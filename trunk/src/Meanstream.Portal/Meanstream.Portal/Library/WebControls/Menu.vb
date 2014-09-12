Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace Meanstream.Portal.WebControls

    <ParseChildren(True)> _
    Public Class Menu
        Inherits Meanstream.Web.UI.Menu

        Sub New()
            MyBase.New()
        End Sub
    End Class

    <ParseChildren(True)> _
    Public Class MenuItem
        Inherits Meanstream.Web.UI.MenuItem

        Sub New()
            MyBase.New()
        End Sub

        Private _Roles As String = ""
        Public Property Roles() As String
            Get
                Return _Roles
            End Get
            Set(ByVal value As String)
                _Roles = value
            End Set
        End Property

    End Class
End Namespace

