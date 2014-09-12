<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Compose.ascx.vb" Inherits="Meanstream_Profile_UserControls_Compose" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CuteEditor" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table valign="top">
    <tr>
        <td valign="top">
            <table>
                <tr>
                    <td class="label">
                        <b>To:</b>
                    </td>
                    <td>
                        <Meanstream:ComboBox ID="cbbTo" runat="server" SkinID="Combobox" Width="353" ComboPanelHeight="140"
                            ImageButtonWidth="25">
                        </Meanstream:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <br>
                        <b>Subject:</b>
                    </td>
                    <td>
                        <br>
                        <asp:TextBox ID="txtSubject" runat="server" SkinID="Text" Width="350"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <br>
                        <b>Message:</b>
                    </td>
                    <td>
                        <br>
                        <CuteEditor:Editor ID="txtMessage" AutoConfigure="Simple" ShowBottomBar="false" Height="200" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td><br/></td>
                    <td>
                        <br>
                        <asp:CheckBox ID="cbSendEmail" runat="server" Checked="true" Text="Send email" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="right">
                            <br />
                            <Meanstream:BlockUIImageButton ID="btnSend" runat="server" StartMessage="Sending..." />
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>