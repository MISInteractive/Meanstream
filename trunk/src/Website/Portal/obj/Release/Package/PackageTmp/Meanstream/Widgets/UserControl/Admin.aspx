<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Admin.aspx.vb" Inherits="Meanstream_Widgets_UserControl_Admin" MasterPageFile="~/Meanstream/UI/Skins/Module.master" %>
<asp:Content ContentPlaceHolderID="CenterPane" ID="CenterPane" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:HiddenField ID="hId" runat="server" />
<asp:HiddenField ID="litRoleName" runat="server" />
<table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td width="20"><div class="spacer20x20" /></td>
    <td><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Custom</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Custom UserControl</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
          <td valign="middle" nowrap><strong>Title:</strong></td>
          <td valign="middle">
            <asp:TextBox ID="txtName" runat="server" SkinID="Text" Width="250"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top" nowrap><strong>Virtual Path:</strong></td>
          <td height="23" valign="middle">
          <asp:TextBox ID="txtPath" runat="server" Visible="true" Width="350" SkinID="Text"></asp:TextBox>
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
</asp:Content>