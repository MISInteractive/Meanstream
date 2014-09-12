Imports System.Security.Policy
Imports Meanstream.Portal.ComponentModel

Namespace Meanstream.Core.Data

    Public Class DataService
        Implements IDisposable


#Region " Singleton "
        Private Shared _privateServiceInstance As DataService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As DataService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateServiceInstance = New DataService(machineName, appFriendlyName)
                            _privateServiceInstance.Initialize()
                        End If
                    End SyncLock
                End If
                Return _privateServiceInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region


#Region "Methods"
        Private Sub Initialize()
            'Me.ApplicationName = New ApplicationSecurityInfo(AppDomain.CurrentDomain.ActivationContext).ApplicationId.Name

            'If ApplicationName = Nothing Then
            '    Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
            '    Dim machineName As String = Environment.MachineName
            '    Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

            '    Throw New InvalidOperationException(String.Format("The data service has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            'End If


            'set default provider
            Provider = DefaultProvider()

            'Log.Write(LogLevel.Info, [String].Concat("Data Service initialized: ", AppFriendlyName, " #", MachineName))
        End Sub


        ''' <summary>
        ''' Get the default provider.
        ''' </summary>
        Public Function DefaultProvider() As DataProvider
            Return [Get](0)
        End Function


        ''' <summary>
        ''' Get the named provider using the string indexer.
        ''' </summary>
        ''' <param name="key">Name of provider.</param>
        ''' <returns>Provider with supplied key.</returns>
        Public Function [Get](ByVal key As String) As DataProvider
            Dim provider As IDataProvider = ComponentFactory.GetComponent(key)

            If provider Is Nothing Then
                Return Nothing
            End If

            Return provider
        End Function


        ''' <summary>
        ''' Get the named provider using the string indexer.
        ''' </summary>
        ''' <param name="index">Index of provider.</param>
        ''' <returns>Provider with supplied index.</returns>
        Public Function [Get](ByVal index As Integer) As DataProvider
            Dim provider As IDataProvider = Nothing
            If index < 0 Then
                Return provider
            End If

            If index >= Providers.Count Then
                Return Nothing
            End If

            provider = Providers.First.Value

            Return provider
        End Function
#End Region


#Region " Properties "
        Private _applicationName As String
        Public Property ApplicationName() As String
            Get
                Return _applicationName
            End Get
            Private Set(ByVal value As String)
                _applicationName = value
            End Set
        End Property


        Private _appFriendlyName As String
        Public Property AppFriendlyName() As String
            Get
                Return _appFriendlyName
            End Get
            Private Set(ByVal value As String)
                _appFriendlyName = value
            End Set
        End Property


        Private _machineName As String
        Public Property MachineName() As String
            Get
                Return _machineName
            End Get
            Private Set(ByVal value As String)
                _machineName = value
            End Set
        End Property


        ''' <summary>
        ''' Get the all providers.
        ''' </summary>
        Public ReadOnly Property Providers() As IDictionary(Of String, DataProvider)
            Get
                Return ComponentFactory.GetComponents(Of DataProvider)()
            End Get
        End Property


        ''' <summary>
        ''' Get the current provider.
        ''' </summary>
        Private _provider As IDataProvider
        Public Property Provider() As DataProvider
            Get
                Return _provider
            End Get
            Private Set(ByVal value As DataProvider)
                _provider = value
            End Set
        End Property


        ''' <summary>
        ''' Get the number of the providers.
        ''' </summary>
        Public ReadOnly Property Count() As Integer
            Get
                Return Providers.Count
            End Get
        End Property
#End Region


#Region " IDisposable Support "
        Protected Overrides Sub Finalize()
            Try
                Me.Dispose()
            Finally
                MyBase.Finalize()
            End Try
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            _machineName = Nothing
            _appFriendlyName = Nothing
            _provider = Nothing

            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Namespace

