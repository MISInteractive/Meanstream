Imports Meanstream.Portal.Core.Messaging

Namespace Meanstream.Portal.Providers.PortalScheduler
    Friend Class Example
        Inherits Meanstream.Portal.Core.Services.Scheduling.Task

        Public Sub New(ByVal id As Guid, ByVal interval As Double, ByVal startupType As Meanstream.Portal.Core.Services.Scheduling.StartupType)
            MyBase.New(id, interval, startupType)


        End Sub

        Public Overrides Sub Execute()
            'MyBase.Execute()

            Try
                'execute task here

            Catch ex As Exception
                ' Handle the exception
                Me.LastRunSuccessful = False
                Me.LastRunResult = ex.Message
            End Try
        End Sub
    End Class
End Namespace

