Namespace Meanstream.Portal.Core.Widgets.UserControl
    Public Class WidgetModule
        Inherits Meanstream.Portal.Core.Entities.MeanstreamUserControl
        Implements Meanstream.Portal.Core.WidgetFramework.IModuleDTO

        Sub New()
        End Sub

        ''' <summary>
        ''' Initializes the user control widget module and loads from the data store by unique ID  
        ''' </summary>
        ''' <param name="Id">The <see cref="T:System.Integer"/> the unique ID.</param>
        Sub New(ByVal id As Guid)
            Dim CustomBase As Meanstream.Portal.Core.Entities.MeanstreamUserControl = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.GetById(Id)
            If CustomBase Is Nothing Then
                Throw New ApplicationException("Custom Not Found")
            End If
            Me.CreatedBy = CustomBase.CreatedBy
            Me.CreatedDate = CustomBase.CreatedDate
            Me.Id = CustomBase.Id
            Me.LastModifiedBy = CustomBase.LastModifiedBy
            Me.LastModifiedDate = CustomBase.LastModifiedDate
            Me.ModuleId = CustomBase.ModuleId
            Me.ModuleVersionId = CustomBase.ModuleVersionId
            Me.Name = CustomBase.Name
            Me.VirtualPath = CustomBase.VirtualPath
            Me.Visible = CustomBase.Visible
        End Sub

        ''' <summary>
        ''' Creates a new custom widget module and saves the content to the data store   
        ''' </summary>
        ''' <param name="Name">The <see cref="T:System.String"/> Name of the custom control</param>
        ''' <param name="VirtualPathToUserControl">The <see cref="T:System.String"/> virtual path to the custom control</param>
        ''' <param name="ModuleId">The <see cref="T:System.Integer"/> published WidgetId</param>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Integer"/> version WidgetId</param>
        ''' <param name="Visible">The <see cref="T:System.Boolean"/> visibility of the user control</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.UserControl.WidgetModule"/> new widget module</returns>
        Public Shared Function Create(ByVal Name As String, _
                                      ByVal VirtualPathToUserControl As String, _
                                      ByVal ModuleId As Guid, _
                                      ByVal ModuleVersionId As Guid, _
                                      ByVal Visible As Boolean) As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule

            If ModuleId = Nothing And ModuleVersionId = Nothing Then
                Throw New ApplicationException("ModuleId and ModuleVersionId cannot be null")
            End If

            Dim Custom As Meanstream.Portal.Core.Entities.MeanstreamUserControl = New Meanstream.Portal.Core.Entities.MeanstreamUserControl
            If System.Web.HttpContext.Current.Request.IsAuthenticated Then
                Custom.CreatedBy = Membership.MembershipService.Current.GetUserGuid(System.Web.HttpContext.Current.Profile.UserName)
                Custom.LastModifiedBy = Custom.CreatedBy
            Else
                Custom.CreatedBy = New Guid(System.Web.HttpContext.Current.Request.AnonymousID)
                Custom.LastModifiedBy = Custom.CreatedBy
            End If
            Custom.CreatedDate = Date.Now
            Custom.LastModifiedDate = Date.Now
            Custom.ModuleId = ModuleId
            Custom.ModuleVersionId = ModuleVersionId
            Custom.Name = Name
            Custom.VirtualPath = VirtualPathToUserControl
            Custom.Visible = Visible
            Custom.Id = Guid.NewGuid

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.Insert(Custom) Then
                Return Meanstream.Portal.Core.Widgets.UserControl.WidgetModule.GetById(Custom.Id)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Retrieves the custom widget module from the data store by unique ID 
        ''' </summary>
        ''' <param name="Id">The <see cref="T:System.Guid"/> the unique ID.</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.UserControl.WidgetModule"/> widget module</returns>
        Public Shared Function GetById(ByVal Id As Guid) As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule
            Dim CustomBase As Meanstream.Portal.Core.Entities.MeanstreamUserControl = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.GetById(Id)
            If CustomBase Is Nothing Then
                Return Nothing
            End If
            Dim Custom As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = New Meanstream.Portal.Core.Widgets.UserControl.WidgetModule
            Custom.CreatedBy = CustomBase.CreatedBy
            Custom.CreatedDate = CustomBase.CreatedDate
            Custom.Id = CustomBase.Id
            Custom.LastModifiedBy = CustomBase.LastModifiedBy
            Custom.LastModifiedDate = CustomBase.LastModifiedDate
            Custom.ModuleId = CustomBase.ModuleId
            Custom.ModuleVersionId = CustomBase.ModuleVersionId
            Custom.Name = CustomBase.Name
            Custom.VirtualPath = CustomBase.VirtualPath
            Custom.Visible = CustomBase.Visible
            Return Custom
        End Function

        ''' <summary>
        ''' Retrieves the custom version of a widget module from the data store by ModuleVersionId (WidgetId)
        ''' </summary>
        ''' <param name="ModuleVersionId">The <see cref="T:System.Guid"/> the version WidgetId.</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.UserControl.WidgetModule"/> widget module</returns>
        Public Shared Function GetByModuleVersionId(ByVal moduleVersionId As Guid) As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule
            Dim CustomBase As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamUserControl) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.Find("ModuleVersionId=" & moduleVersionId.ToString)
            If CustomBase.Count = 0 Then
                Return Nothing
            End If
            Dim Custom As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = New Meanstream.Portal.Core.Widgets.UserControl.WidgetModule
            Custom.CreatedBy = CustomBase(0).CreatedBy
            Custom.CreatedDate = CustomBase(0).CreatedDate
            Custom.Id = CustomBase(0).Id
            Custom.LastModifiedBy = CustomBase(0).LastModifiedBy
            Custom.LastModifiedDate = CustomBase(0).LastModifiedDate
            Custom.ModuleId = CustomBase(0).ModuleId
            Custom.ModuleVersionId = CustomBase(0).ModuleVersionId
            Custom.Name = CustomBase(0).Name
            Custom.VirtualPath = CustomBase(0).VirtualPath
            Custom.Visible = CustomBase(0).Visible
            Return Custom
        End Function

        ''' <summary>
        ''' Retrieves the published custom widget module from the data store by ModuleId (WidgetId)
        ''' </summary>
        ''' <param name="ModuleId">The <see cref="T:System.Guid"/> the new WidgetId.</param>
        ''' <returns>The <see cref="Meanstream.Portal.Core.Widgets.UserControl.WidgetModule"/> widget module</returns>
        Public Shared Function GetByModuleId(ByVal moduleId As Guid) As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule
            Dim CustomBase As Meanstream.Portal.Core.Entities.TList(Of Meanstream.Portal.Core.Entities.MeanstreamUserControl) = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.Find("ModuleId=" & moduleId.ToString)
            If CustomBase.Count = 0 Then
                Return Nothing
            End If
            Dim Custom As Meanstream.Portal.Core.Widgets.UserControl.WidgetModule = New Meanstream.Portal.Core.Widgets.UserControl.WidgetModule
            Custom.CreatedBy = CustomBase(0).CreatedBy
            Custom.CreatedDate = CustomBase(0).CreatedDate
            Custom.Id = CustomBase(0).Id
            Custom.LastModifiedBy = CustomBase(0).LastModifiedBy
            Custom.LastModifiedDate = CustomBase(0).LastModifiedDate
            Custom.ModuleId = CustomBase(0).ModuleId
            Custom.ModuleVersionId = CustomBase(0).ModuleVersionId
            Custom.Name = CustomBase(0).Name
            Custom.VirtualPath = CustomBase(0).VirtualPath
            Custom.Visible = CustomBase(0).Visible
            Return Custom
        End Function

        ''' <summary>
        ''' Implements <see cref="Meanstream.Portal.Core.WidgetFramework.IModuleDTO.Save"/> Saves the widget's custom content to the data store.
        ''' </summary>
        Public Sub Save() Implements WidgetFramework.IModuleDTO.Save
            Dim CustomBase As Meanstream.Portal.Core.Entities.MeanstreamUserControl = Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.GetById(Id)
            If CustomBase Is Nothing Then
                Throw New ApplicationException("Custom Not Found")
            End If

            If System.Web.HttpContext.Current.Request.IsAuthenticated Then
                CustomBase.LastModifiedBy = Membership.MembershipService.Current.GetUserGuid(System.Web.HttpContext.Current.Profile.UserName)
                Me.LastModifiedBy = CustomBase.LastModifiedBy
            Else
                CustomBase.LastModifiedBy = New Guid(System.Web.HttpContext.Current.Request.AnonymousID)
                Me.LastModifiedBy = CustomBase.LastModifiedBy
            End If

            CustomBase.LastModifiedDate = Date.Now
            Me.LastModifiedDate = CustomBase.LastModifiedDate
            CustomBase.VirtualPath = Me.VirtualPath
            CustomBase.Name = Me.Name

            If Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.Update(CustomBase) Then
                Dim Widget As Meanstream.Portal.Core.WidgetFramework.WidgetVersion = WidgetFramework.WidgetService.Current.GetWidgetVersionById(Me.ModuleVersionId)
                Dim manager As New WidgetFramework.WidgetVersionManager(Widget)
                manager.Save()
            Else
                Throw New ApplicationException("widget save failed")
            End If
        End Sub

        ''' <summary>
        ''' Implements <see cref="Meanstream.Portal.Core.WidgetFramework.IModuleDTO.Delete"/> interface. Deletes the widget's custom content from the data store during widget deletion.
        ''' </summary>
        Public Sub Delete() Implements WidgetFramework.IModuleDTO.Delete
            Meanstream.Portal.Core.Data.DataRepository.MeanstreamUserControlProvider.Delete(Me.Id)
        End Sub
    End Class
End Namespace

