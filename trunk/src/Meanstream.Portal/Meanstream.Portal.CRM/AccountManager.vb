Imports Meanstream.Portal.CRM.Extensibility
Imports Meanstream.Portal.Core.Messaging
Imports Meanstream.Portal.Core.Instrumentation

Namespace Meanstream.Portal.CRM
    Public Class AccountManager
        Inherits AttributeEntityManager

        Private _entity As Account

        Sub New(ByRef attributeEntity As Account)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _entity.Id = Nothing Then
                Throw New ArgumentNullException("account id cannot be null.")
            End If
        End Sub

        Protected Friend Sub LoadFromDatasource()
            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAccounts = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAccountsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the account {0} cannot be located in database.", _entity.Id))
            End If
            Me.Bind(entity)
        End Sub

        Protected Friend Sub Bind(ByVal entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAccounts)
            _entity.BillingAddress = entity.BillingAddress
            _entity.Email = entity.Email
            _entity.Name = entity.Name
            _entity.Notes = entity.Notes
            _entity.Phone = entity.Phone
            _entity.PreferredDeliveryMethod = entity.PreferredDeliveryMethod
            _entity.PreferredPaymentMethod = entity.PreferredPaymentMethod
            _entity.ShippingAddress = entity.ShippingAddress
            _entity.TaxResaleNum = entity.TaxResaleNum
            _entity.Terms = entity.Terms
            _entity.Website = entity.Website
        End Sub

        Public Overrides Sub Delete()
            MyBase.Delete()
            'delete all user accounts
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserAccounts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserAccountsProvider.Find("AccountId=" & _entity.Id.ToString)
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmUserAccounts In entities
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserAccountsProvider.Delete(entity.Id)
            Next
            'delete main account
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAccountsProvider.Delete(_entity.Id)
        End Sub

        Public Overrides Sub Save()
            MyBase.Save()

            Dim entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmAccounts = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAccountsProvider.GetById(_entity.Id)
            If entity Is Nothing Then
                Throw New InvalidOperationException(String.Format("the account {0} cannot be located in database.", _entity.Id))
            End If

            If _entity.Name Is Nothing Or _entity.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            entity.BillingAddress = _entity.BillingAddress
            entity.Email = _entity.Email
            entity.Name = _entity.Name
            entity.Notes = _entity.Notes
            entity.Phone = _entity.Phone
            entity.PreferredDeliveryMethod = _entity.PreferredDeliveryMethod
            entity.PreferredPaymentMethod = _entity.PreferredPaymentMethod
            entity.ShippingAddress = _entity.ShippingAddress
            entity.TaxResaleNum = _entity.TaxResaleNum
            entity.Terms = _entity.Terms
            entity.Website = _entity.Website
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAccountsProvider.Update(entity)
        End Sub

        Public Function IsUserInAccount(ByVal userId As Guid) As Boolean
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmUserAccounts) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserAccountsProvider.Find("UserId=" & userId.ToString & " AND AccountId=" & _entity.Id.ToString)
            If entities.Count = 0 Then
                Return False
            End If
            Return True
        End Function

        Public Sub AddUserToAccount(ByVal userId As Guid)
            If Not Me.IsUserInAccount(userId) Then
                Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmUserAccounts
                entity.Id = Guid.NewGuid
                entity.AccountId = _entity.Id
                entity.CreatedDate = Date.Now
                entity.UserId = userId
                Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmUserAccountsProvider.Insert(entity)
            End If
        End Sub

        Protected Friend Shared Function Create(ByVal account As Account) As Guid
            If account.Name Is Nothing Or account.Name.Trim = "" Then
                Throw New ArgumentNullException("name is required")
            End If

            Dim entity As New Meanstream.Portal.CRM.Entities.MeanstreamCrmAccounts
            entity.Id = account.Id
            entity.BillingAddress = account.BillingAddress
            entity.Email = account.Email
            entity.Name = account.Name
            entity.Notes = account.Notes
            entity.Phone = account.Phone
            entity.PreferredDeliveryMethod = account.PreferredDeliveryMethod
            entity.PreferredPaymentMethod = account.PreferredPaymentMethod
            entity.ShippingAddress = account.ShippingAddress
            entity.TaxResaleNum = account.TaxResaleNum
            entity.Terms = account.Terms
            entity.Website = account.Website
            Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmAccountsProvider.Insert(entity)

            'add attributes
            For Each Attribute As Meanstream.Portal.Core.Extensibility.Attribute In account.Attributes
                AttributeService.Current.Create(entity.Id, Attribute.Key, Attribute.Value, Attribute.DataType)
            Next

            Return entity.Id
        End Function
    End Class
End Namespace

