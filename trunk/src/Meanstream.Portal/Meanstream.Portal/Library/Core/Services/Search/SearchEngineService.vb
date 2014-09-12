Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Portal.Core.ExceptionHandling

Namespace Meanstream.Portal.Core.Services.Search

    Public Class SearchEngineService
        Implements IDisposable

#Region " Singleton "
        Private Shared _privateServiceInstance As SearchEngineService
        Private Shared _serviceSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As SearchEngineService
            Get
                If _privateServiceInstance Is Nothing Then
                    SyncLock _serviceSingletonLockObject
                        If _privateServiceInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateServiceInstance = New SearchEngineService(machineName, appFriendlyName)
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

#Region " Methods "
        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The search engine service infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("Search Engine Service initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize Search Engine Service: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function RunCrawler() As PageState()
            Dim url As String = Utilities.AppUtility.GetCurrentSiteUrl
            Return Me.RunCrawler(url)
        End Function

        Public Function RunCrawler(ByVal url As String) As PageState()
            Return IndexingProvider.Current.Crawl(url)
        End Function

        Public Sub Index()
            Dim url As String = Utilities.AppUtility.GetCurrentSiteUrl
            Me.Index(url)
        End Sub

        Public Sub Index(ByVal url As String)
            Dim documents As List(Of Meanstream.Portal.Core.Services.Search.Document) = New List(Of Meanstream.Portal.Core.Services.Search.Document)
            Dim _pages As PageState() = IndexingProvider.Current.Crawl(url)
            Dim _errors As New List(Of PageState)
            For Each page As PageState In _pages
                If GetStatus(page) = "Success" Then
                    Dim document As Meanstream.Portal.Core.Services.Search.Document = New Meanstream.Portal.Core.Services.Search.Document()
                    document.Add(New Meanstream.Portal.Core.Services.Search.Field("url", page.Uri.ToString, True, False))
                    document.Add(New Meanstream.Portal.Core.Services.Search.Field("modified", Date.Now, True, False))
                    document.Add(New Meanstream.Portal.Core.Services.Search.Field("content", page.Content, True, True))
                    documents.Add(document)
                Else
                    _errors.Add(page)
                End If
            Next
            DataProvider.Current.Index(documents)
        End Sub

        Private Function GetStatus(ByVal state As PageState) As String
            If state.StatusCode Is Nothing Or _
                state.StatusCode = "404" Or _
                state.StatusCode = "403" Or _
                state.StatusCode = "500" Or _
                state.StatusCode = "502" Or _
                state.StatusCode = "503" Or _
                state.StatusCode = "504" Then
                Return "Failed"
            End If
            Return (If(state.ProcessSuccessfull, "Success", "Failed "))
        End Function

        Public Sub DeleteIndex()
            DataProvider.Current.DeleteIndex()
        End Sub

        Public Sub RemoveFromIndex(ByVal url As String)
            DataProvider.Current.RemoveFromIndex(url)
        End Sub

        Public Function GetAll() As List(Of Document)
            Return DataProvider.Current.GetAll()
        End Function

        Public Function Search(ByVal keyword As String) As List(Of Document)
            Return DataProvider.Current.Search("content", keyword)
        End Function

        Public Function Search(ByVal fieldName As String, ByVal keyword As String) As List(Of Document)
            Return DataProvider.Current.Search(fieldName, keyword)
        End Function
#End Region

#Region " Properties "
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

        Private _applicationId As Guid
        Public Property ApplicationId() As Guid
            Get
                Return _applicationId
            End Get
            Private Set(ByVal value As Guid)
                _applicationId = value
            End Set
        End Property
#End Region


#Region " IDisposable Support "
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Deinitialize()
        End Sub
#End Region

    End Class
End Namespace