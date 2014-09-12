<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Smtp.ascx.vb" Inherits="Meanstream_Administration_UserControls_Smtp" %>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td align="right">
            <strong>From:&nbsp;</strong></td>
        <td>
            
            <asp:TextBox ID="txtFrom" runat="server" Width="300"
               SkinID="Text"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            <div align="right">
                <br />
                <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />   
            </div>   
        </td>
    </tr>
</table>