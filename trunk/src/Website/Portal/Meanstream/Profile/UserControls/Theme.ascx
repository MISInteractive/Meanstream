<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Theme.ascx.vb" Inherits="Meanstream_Profile_UserControls_Theme" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div align="left">
    <table>  
        <tr>
            <td>    
                <b>Current theme:</b>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="litTheme" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="2"><br />
                Pick a theme to use throughout the Administration areas of the site.
                <br />    
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:RadioButtonList ID="cbPreferences" runat="server">
                    <asp:ListItem Selected="True" Text="Default" Value="Default"></asp:ListItem>
                </asp:RadioButtonList>    
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div align="right">
                    <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                </div>
            </td>
        </tr>
    </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>