

Namespace Meanstream.Core.Data

    ''' <summary>
    ''' Interface for Data Provider Settings used to bind default settings from the web.config.
    ''' </summary>
    Public Interface IProviderSettings
        Property Item(ByVal key As String) As String
        ReadOnly Property Settings() As Dictionary(Of String, String)
    End Interface


    ''' <summary>
    ''' Base class for Data Provider Settings.
    ''' </summary>
    Public Class ProviderSettings
        Implements IProviderSettings


        Public Sub New()
            _settings = New Dictionary(Of String, String)
        End Sub


        ''' <summary>
        ''' Get/set additional values.
        ''' </summary>
        ''' <param name="key">Key to settings item.</param>
        ''' <returns>Value of settings item.</returns>
        Default Public Property Item(ByVal key As String) As String Implements IProviderSettings.Item
            Get
                Return _settings(key)
            End Get
            Set(ByVal value As String)
                _settings(key) = value
            End Set
        End Property
        Private _settings As Dictionary(Of String, String)


        ''' <summary>
        ''' Provide read-only access to settings.
        ''' </summary>
        Public ReadOnly Property Settings() As Dictionary(Of String, String) Implements IProviderSettings.Settings
            Get
                Return _settings
            End Get
        End Property

    End Class


End Namespace
