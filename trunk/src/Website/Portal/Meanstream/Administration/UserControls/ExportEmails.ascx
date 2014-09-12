<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ExportEmails.ascx.vb" Inherits="Meanstream_Administration_UserControls_ExportEmails" %>
   
<asp:GridView ID="EmailGrid" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="Email" DataField="Email" />
    </Columns>
</asp:GridView>
   
