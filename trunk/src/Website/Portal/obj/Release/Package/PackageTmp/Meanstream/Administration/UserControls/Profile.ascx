<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Profile.ascx.vb" Inherits="Meanstream_Administration_UserControls_Profile" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top">
        <strong>Managing profile for <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>:</strong>
    </td>
  </tr>
</table>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top">
        <asp:DataList ID="Properties" runat="server" Visible="true">
            <HeaderTemplate>
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                  <td valign="left" nowrap><strong><asp:Label ID="lblProperty" runat="server" Text=""></asp:Label>:</strong></td>
                  <td height="23" valign="left">
                    <asp:TextBox ID="txtProperty" runat="server" SkinID="Text" Width="225"></asp:TextBox>
                  </td>
                </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                  <td valign="left" nowrap><div class="spacer10x20" /></td>
                  <td valign="left"><div class="spacer10x20" /></td>
                </tr>
            </SeparatorTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>
    </td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><div class="spacer20x20" /></td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td nowrap></td>
            <td><div class="spacer20x20" /></td>
            <td><Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                <br />
            </td>
          </tr>
      </table></td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>