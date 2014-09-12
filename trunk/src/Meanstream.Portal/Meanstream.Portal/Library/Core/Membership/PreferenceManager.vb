Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.Core.Membership
    Public Class PreferenceManager

        Private _entity As Preference

        Sub New(ByRef preference As Preference)
            _entity = preference
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("preference id cannot be null.")
            End If
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.Core.Entities.MeanstreamPreference)
            _entity.Name = entity.Name
            _entity.PreferenceId = entity.PreferenceId
        End Sub

        Public Overridable Sub Delete()

        End Sub

        Public Overridable Sub Save()

        End Sub

        Public Function AddUserPreference(ByVal userId As Guid, ByVal value As Object) As Guid
            Return MembershipService.Current.AddUserPreference(userId, _entity.PreferenceId, value)
        End Function

        Public Function AddUserPreference(ByVal username As String, ByVal value As Object) As Guid
            Return MembershipService.Current.AddUserPreference(username, _entity.PreferenceId, value)
        End Function
    End Class
End Namespace

