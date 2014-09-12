Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Collections.Generic

Namespace Meanstream.Portal.Core
    Public Class Session
        Implements ISession

        Private _Session As Meanstream.Portal.Core.Utilities.WeakReference(Of HttpSessionState)

        Public Sub New(ByVal session As HttpSessionState)
            Me._Session = New Meanstream.Portal.Core.Utilities.WeakReference(Of HttpSessionState)(session)
        End Sub

        Public ReadOnly Property Count() As Integer Implements Core.ISession.Count
            Get
                Return Me._Session.Target.Count
            End Get
        End Property

        Public ReadOnly Property SessionID() As String Implements Core.ISession.SessionID
            Get
                Return Me._Session.Target.SessionID
            End Get
        End Property

        Public Property Target(ByVal Name As String) As Object
            Get
                Return Me._Session.Target(Name)
            End Get
            Set(ByVal Value As Object)
                Me._Session.Target(Name) = Value
            End Set
        End Property

        Public Sub Abandon()
            Me._Session.Target.Clear()
        End Sub
    End Class
End Namespace
