<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CreateRole.ascx.vb" Inherits="Meanstream_Administration_UserControls_CreateRole" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:HiddenField ID="litRoleName" runat="server" />
<table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td width="20"><div class="spacer20x20" /></td>
    <td><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Create Role</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Create Role</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table>
      <table border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td width="20" rowspan="8" valign="middle"><div class="spacer10x20" /></td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="middle" nowrap><strong>Name:</strong></td>
          <td valign="middle">
            <asp:TextBox ID="txtName" runat="server" Width="225" SkinID="Text"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top" nowrap><strong>Description:</strong></td>
          <td height="23" valign="middle">
          <asp:TextBox ID="txtDescription" TextMode="MultiLine" Width="225" Height="100"
                runat="server" SkinID="Text"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="middle" nowrap><strong>Auto:</strong></td>
          <td valign="middle"><asp:CheckBox ID="chkAddAuto" runat="server" /></td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="middle" nowrap><strong>Public:</strong></td>
          <td valign="middle"><asp:CheckBox ID="chkAddPublic" runat="server" /></td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><table border="0" align="right" cellpadding="0" cellspacing="0">
            <tr>
              <td nowrap></td>
              <td><div class="spacer10x20" /></td>
              <td><Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." /></td>
            </tr>
          </table></td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table></td>
    <td width="20"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>