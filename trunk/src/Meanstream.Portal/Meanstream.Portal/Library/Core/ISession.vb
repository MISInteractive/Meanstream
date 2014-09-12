Imports Microsoft.VisualBasic

Namespace Meanstream.Portal.Core
    Interface ISession
        ReadOnly Property SessionID() As String
        ReadOnly Property Count() As Integer
    End Interface
End Namespace

