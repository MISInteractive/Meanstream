Imports System.Net.Mail

Namespace Meanstream.Portal.Core.Messaging
    Public Class TextMessageManager

        Private _message As TextMessage

        Sub New(ByRef message As TextMessage)
            _message = message
            Me.Initialize()
        End Sub

        Private Sub Initialize()
            If _message.Id = Nothing Then
                Throw New ArgumentNullException("id cannot be null.")
            End If
            If _message.Sender = Nothing Or _message.Sender.Trim = "" Then
                Throw New ArgumentNullException("sender is required.")
            End If
            If _message.Recipient = Nothing Or _message.Recipient.Trim = "" Then
                Throw New ArgumentNullException("recipient is required.")
            End If
            If _message.Carrier Is Nothing Then
                Throw New ArgumentNullException("gateway is required.")
            End If
        End Sub

        Public Sub Send()
            Dim number As String = _message.Recipient
            Dim smsTo As String = number & "@" & _message.Carrier.Gateway
            Dim message As New MailMessage(_message.Sender, smsTo, _message.Subject, _message.Body)
            Dim mySmtpClient As New SmtpClient()
            mySmtpClient.UseDefaultCredentials = True
            mySmtpClient.Send(message)
        End Sub

        Public Shared Function GetCarriers() As List(Of TextMessageCarrier)
            Dim list As New List(Of TextMessageCarrier)
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSmsGateway) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSmsGatewayProvider.Find("Region=USA AND GatewayType=SMS")
            entities.Sort("Carrier ASC")
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSmsGateway In entities
                Dim carrier As New TextMessageCarrier(entity.Id)
                carrier.Carrier = entity.Carrier
                carrier.Gateway = entity.Gateway
                carrier.GatewayFormat = entity.GatewayFormat
                carrier.GatewayNumber = entity.GatewayNumber
                carrier.Type = [Enum].Parse(GetType(TextMessage.TextMessageType), entity.GatewayType)
                list.Add(carrier)
            Next
            Return list
        End Function

        Public Shared Function GetCarrier(ByVal name As String) As TextMessageCarrier
            Dim carrier As TextMessageCarrier = Nothing
            Dim entities As Meanstream.Portal.CRM.Entities.TList(Of Meanstream.Portal.CRM.Entities.MeanstreamCrmSmsGateway) = Meanstream.Portal.CRM.Data.DataRepository.MeanstreamCrmSmsGatewayProvider.Find("GatewayType=SMS AND Carrier=" & name)
            entities.Sort("Carrier ASC")
            For Each entity As Meanstream.Portal.CRM.Entities.MeanstreamCrmSmsGateway In entities
                carrier = New TextMessageCarrier(entity.Id)
                carrier.Carrier = entity.Carrier
                carrier.Gateway = entity.Gateway
                carrier.GatewayFormat = entity.GatewayFormat
                carrier.GatewayNumber = entity.GatewayNumber
                carrier.Type = [Enum].Parse(GetType(TextMessage.TextMessageType), entity.GatewayType)
            Next
            Return carrier
        End Function
    End Class
End Namespace

