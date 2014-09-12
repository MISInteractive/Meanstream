<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Smtp.ascx.vb" Inherits="Meanstream_Host_UserControls_Smtp" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table valign="top">
    <tr>
        <td>
            Default From Address:
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" Width="250" CssClass="textbox"></asp:TextBox> 
        </td>
    </tr>
    <tr>
        <td><br />
            Host:
        </td>
        <td><br />
            <asp:TextBox ID="txtHost" runat="server" Width="150" CssClass="textbox"></asp:TextBox> 
        </td>
    </tr>
    <tr>
        <td><br />
            Username:
        </td>
        <td><br />
            <asp:TextBox ID="txtUsername" runat="server" Width="150" CssClass="textbox"></asp:TextBox> 
        </td>
    </tr>
    <tr>
        <td><br />
            Password:
        </td>
        <td><br />
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="150" CssClass="textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><br />
            Port:
        </td>
        <td><br />
            <asp:TextBox ID="txtPort" runat="server" Width="50" CssClass="textbox"></asp:TextBox> 
        </td>
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
</ContentTemplate>
</asp:UpdatePanel>