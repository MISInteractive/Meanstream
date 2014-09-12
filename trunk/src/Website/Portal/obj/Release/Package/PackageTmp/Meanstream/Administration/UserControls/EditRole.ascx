<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditRole.ascx.vb" Inherits="Meanstream_Administration_UserControls_EditRole" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td height="20">
            <b>Name:</b>
        </td>
        <td>
            <asp:TextBox ID="txtEditName" runat="server" Width="225" SkinID="Text" ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td height="20"><br />
            <b>Description:</b>
        </td>
        <td><br />
            <asp:TextBox ID="txtEditDescription" TextMode="MultiLine" Width="225" Height="100"
                runat="server" SkinID="Text"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td height="20"><br />
            <b>Auto:</b>
        </td>
        <td><br />
            <asp:CheckBox ID="chkEditAuto" runat="server" />
        </td>
    </tr>
    <tr>
        <td height="20"><br />
            <b>Public:</b>
        </td>
        <td><br />
            <asp:CheckBox ID="chkEditPublic" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div align="right">
                <table>
                    <tr>
                        <td>
                            <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                        </td>
                    </tr>
                </table>
            </div>        
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>