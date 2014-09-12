Imports Meanstream.Portal.Core.Instrumentation
Imports Meanstream.Portal.Core.ExceptionHandling

Namespace Meanstream.Portal.CRM.Extensibility
    Public Class AttributeService
        Implements IDisposable


#Region " Singleton "
        Private Shared _privateAttributeManagerInstance As AttributeService
        Private Shared _attributeManagerSingletonLockObject As New Object()

        Public Shared ReadOnly Property Current() As AttributeService
            Get
                If _privateAttributeManagerInstance Is Nothing Then
                    SyncLock _attributeManagerSingletonLockObject
                        If _privateAttributeManagerInstance Is Nothing Then
                            Dim appFriendlyName As String = AppDomain.CurrentDomain.FriendlyName
                            Dim machineName As String = Environment.MachineName

                            _privateAttributeManagerInstance = New AttributeService(machineName, appFriendlyName)
                            _privateAttributeManagerInstance.Initialize()

                        End If
                    End SyncLock
                End If
                Return _privateAttributeManagerInstance
            End Get
        End Property

        Private Sub New(ByVal machineName As String, ByVal appFriendlyName As String)
            Me.AppFriendlyName = appFriendlyName
            Me.MachineName = machineName
        End Sub
#End Region

#Region " Attibute Methods "

        Private Sub Initialize()
            Me.ApplicationId = Meanstream.Portal.Core.Data.DataRepository.AspnetApplicationsProvider.GetByApplicationName(Core.AppConstants.APPLICATION).ApplicationId

            If ApplicationId = Nothing Then
                Dim friendlyName As String = AppDomain.CurrentDomain.FriendlyName
                Dim machineName As String = Environment.MachineName
                Dim appBase As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase

                Throw New InvalidOperationException(String.Format("The CRM attribute extensibility infrastructure has not been initialized. MachineName='{0}', AppDomainFriendlyName='{1}', AppBase='{2}'.", machineName, friendlyName, appBase))
            End If

            PortalTrace.WriteLine([String].Concat("CRM Extensibity initialized: ", AppFriendlyName, " #", ApplicationId))
        End Sub

        Public Sub Deinitialize()
            PortalTrace.WriteLine([String].Concat("Deinitialize CRM Extensibity: ", AppFriendlyName, " #", ApplicationId))
            Me.ApplicationId = Nothing
            Me.AppFriendlyName = Nothing
        End Sub

        Public Function Create(ByVal componentId As Guid, ByVal key As String, ByVal value As Object, ByVal dataType As Type) As Meanstream.Portal.Core.Extensibility.Attribute
            If componentId = Nothing Then
                Throw New ArgumentNullException("componentId is required")
            End If

            If key.Trim = "" Then
                Throw New ArgumentNullException("key is required")
            End If

            If dataType Is Nothing Then
                Throw New ArgumentNullException("dataType is required")
            End If

            Dim Atts As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Find("ComponentId=" & componentId.ToString & " AND Name=" & key)
            If Atts.Count > 0 Then
                'attribute exists so return it
                Return Me.GetAttribute(componentId, key)
            End If

            Dim Attribute As Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes = New Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes
            Attribute.DataType = dataType.FullName
            Attribute.Name = key
            Attribute.Value = value
            Attribute.ComponentId = componentId
            Attribute.Id = Guid.NewGuid
            If Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Insert(Attribute) Then
                Return Me.GetAttribute(componentId, key)
            End If

            Return Nothing
        End Function

        Public Function GetAttributes(ByVal ComponentId As Guid) As List(Of Meanstream.Portal.Core.Extensibility.Attribute)
            If ComponentId = Nothing Then
                Throw New ArgumentNullException("componentId is required")
            End If

            Dim _Attributes As List(Of Meanstream.Portal.Core.Extensibility.Attribute) = New List(Of Meanstream.Portal.Core.Extensibility.Attribute)

            Dim Properties As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Find("ComponentId=" & ComponentId.ToString)
            If Properties.Count > 0 Then
                For Each AttributeProperty As Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes In Properties
                    Dim Attribute As Meanstream.Portal.Core.Extensibility.Attribute = New Meanstream.Portal.Core.Extensibility.Attribute
                    Attribute.Key = AttributeProperty.Name
                    Attribute.Value = AttributeProperty.Value
                    Attribute.ComponentId = AttributeProperty.ComponentId
                    Attribute.DataType = Type.GetType(AttributeProperty.DataType, True, True)
                    _Attributes.Add(Attribute)
                Next
            End If

            Return _Attributes
        End Function

        Public Function GetAttribute(ByVal componentId As Guid, ByVal key As String) As Meanstream.Portal.Core.Extensibility.Attribute
            If componentId = Nothing Then
                Throw New ArgumentNullException("componentId is required")
            End If

            If key = Nothing Then
                Throw New ArgumentNullException("key is required")
            End If

            Dim Atts As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Find("ComponentId=" & componentId.ToString & " AND Name=" & key)
            If Atts.Count = 0 Then
                Throw New NullReferenceException("attribute does not exist")
            End If

            Dim Attribute As Meanstream.Portal.Core.Extensibility.Attribute = New Meanstream.Portal.Core.Extensibility.Attribute
            Attribute.Key = Atts(0).Name
            Attribute.Value = Atts(0).Value
            Attribute.ComponentId = Atts(0).ComponentId
            Attribute.DataType = Type.GetType(Atts(0).DataType, True, True)

            Return Attribute
        End Function

        Public Sub Save(ByVal attribute As Meanstream.Portal.Core.Extensibility.Attribute)
            If attribute.ComponentId = Nothing Then
                Throw New ArgumentNullException("componentId is required")
            End If

            If attribute.Key.Trim = "" Then
                Throw New ArgumentNullException("key is required")
            End If

            If attribute.DataType Is Nothing Then
                Throw New ArgumentNullException("datatype is required")
            End If

            Dim Atts As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Find("ComponentId=" & attribute.ComponentId.ToString & " AND Name=" & attribute.Key)
            If Atts.Count = 0 Then 'insert
                Dim Att As Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes = New Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes
                Att.Name = attribute.Key
                Att.Value = attribute.Value
                Att.DataType = attribute.DataType.FullName
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Insert(Att)
            Else 'update
                Dim Att As Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes = Atts(0)
                Att.Name = attribute.Key
                Att.Value = attribute.Value
                Att.DataType = attribute.DataType.FullName
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Update(Att)
            End If
        End Sub

        Public Sub Delete(ByVal attribute As Meanstream.Portal.Core.Extensibility.Attribute)
            If attribute.ComponentId = Nothing Then
                Throw New ArgumentNullException("componentId is required")
            End If

            If attribute.Key.Trim = "" Then
                Throw New ArgumentNullException("key is required")
            End If

            Dim Atts As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmAttributes) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Find("ComponentId=" & attribute.ComponentId.ToString & " AND Name=" & attribute.Key)
            If Atts.Count = 0 Then
                Throw New NullReferenceException("attribute does not exist")
            End If

            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAttributesProvider.Delete(Atts(0))
        End Sub
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