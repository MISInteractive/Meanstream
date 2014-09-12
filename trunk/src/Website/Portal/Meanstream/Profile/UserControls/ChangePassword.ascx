<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.ascx.vb" Inherits="Meanstream_Profile_UserControls_ChangePassword" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div align="left" style="width: 350px;">
    <table>  
        <tr>
            <td colspan="2">
                To change a password for this user enter the new password and confirm the entry by typing it again.
                <br /><br/>
            </td>
        </tr>
        <tr>
            <td>    
                <b><asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label></b>
            </td>
            <td>    
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>    <br>
                <b><asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:"></asp:Label></b>
            </td>
            <td>    <br>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div align="right" style="width: 295px;"><br>
                    <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                </div>
            </td>
        </tr>
    </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
