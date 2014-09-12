<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dashboard.ascx.vb" Inherits="Meanstream_Profile_UserControls_Dashboard" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div align="left">
    <table>  
        <tr>
            <td colspan="2">
                Select the modules to display on your dashboard.
                <br />    
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBoxList ID="cbPreferences" runat="server">
                    <asp:ListItem Text="My Recent Edits" Value="MEANSTREAM_DASHBOARD_MY_RECENT_EDITS"></asp:ListItem>
                    <asp:ListItem Text="QuickLinks" Value="MEANSTREAM_DASHBOARD_QUICKLINKS"></asp:ListItem>
                    <asp:ListItem Text="Recent Published Pages" Value="MEANSTREAM_DASHBOARD_RECENT_PUBLISHED_PAGES"></asp:ListItem>
                    <asp:ListItem Text="User Stats" Value="MEANSTREAM_DASHBOARD_USER_STATS"></asp:ListItem>
                </asp:CheckBoxList>    
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