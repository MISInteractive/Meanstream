Namespace Meanstream.Portal.Core.Extensibility
    Public Class Setting
        Inherits Meanstream.Portal.Core.Entities.MeanstreamApplicationSettings

        Public Shared Function GetSettingByName(ByVal SettingsName As String) As Meanstream.Portal.Core.Extensibility.Setting
            Dim Settings As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamApplicationSettings) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationSettingsProvider.Find("Param=" & SettingsName)
            Dim Result As Meanstream.Portal.Core.Extensibility.Setting = New Meanstream.Portal.Core.Extensibility.Setting
            For Each Setting As Meanstream.Portal.Core.Entities.MeanstreamApplicationSettings In Settings
                If Setting.Param = SettingsName Then
                    Result.Id = Setting.Id
                    Result.Param = Setting.Param
                    Result.Value = Setting.Value
                End If
            Next
            Return Result
        End Function

        Public Shared Function GetGoogleAnalyticsScript(ByVal portalId As Guid) As Setting
            Dim setting As String = Meanstream.Portal.Core.AppConstants.GOOGLE_ANALYTICS_SCRIPT & "_" & portalId.ToString
            Dim Result As Meanstream.Portal.Core.Extensibility.Setting = GetSettingByName(setting)
            If String.IsNullOrEmpty(Result.Param) Then
                Result.Param = setting
                Result.Value = ""
                Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationSettingsProvider.Insert(Result)
            End If
            Return Result
        End Function

        Public Sub Save()
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationSettingsProvider.Update(Me)
        End Sub

        Public Sub Delete()
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamApplicationSettingsProvider.Delete(Me.Id)
        End Sub
    End Class
End Namespace

