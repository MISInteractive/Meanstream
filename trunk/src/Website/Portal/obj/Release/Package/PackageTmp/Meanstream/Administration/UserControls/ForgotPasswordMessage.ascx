<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ForgotPasswordMessage.ascx.vb" Inherits="Meanstream_Administration_UserControls_ForgotPasswordMessage" %>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td align="right">
            <strong>Subject:</strong>&nbsp;</td>
        <td>
            
            <asp:TextBox ID="txtSubject" runat="server" Width="300" SkinID="Text"></asp:TextBox></td>
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