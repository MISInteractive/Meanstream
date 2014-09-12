<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ResetPasswordMessage.ascx.vb" Inherits="Meanstream_Administration_UserControls_ResetPasswordMessage" %>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td align="right">
            <strong>Subject:&nbsp;</strong></td>
        <td>
            
            <asp:TextBox ID="txtSubject" runat="server" Width="300" SkinID="Text"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right">
            <strong>Body:</strong></td>
        <td>
            
            <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Height="100" Width="500"
                SkinID="Text" CssClass="textbox"></asp:TextBox></td>
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