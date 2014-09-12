<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Credentials.ascx.vb" Inherits="Meanstream_Administration_UserControls_Credentials" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top">
                    <table border="0" cellspacing="0" cellpadding="0">
      
      <tr>
        <td valign="middle" nowrap><strong>Username:</strong></td>
        <td width="20" rowspan="9" valign="middle"><div class="spacer10x20" /></td>
        <td height="23" valign="middle"><asp:Label ID="litUsername" runat="server" Text="" Font-Bold="true"></asp:Label></td>
      </tr>
      <tr>
        <td valign="middle" nowrap>&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      
      <tr>
        <td valign="middle" nowrap><strong>Email:</strong></td>
        <td valign="middle">
            <asp:TextBox ID="txtEmail" Enabled="false" runat="server" SkinID="Text" Width="225"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td valign="middle" nowrap>&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td valign="middle" nowrap><strong>First Name:</strong></td>
        <td valign="middle">
            <asp:TextBox ID="txtFirstName" runat="server" SkinID="Text" Width="225"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td valign="middle" nowrap>&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td valign="middle" nowrap><strong>Last Name:</strong></td>
        <td valign="middle">
            <asp:TextBox ID="txtLastName" runat="server" SkinID="Text" Width="225"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td valign="middle" nowrap>&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td valign="middle" nowrap><strong>Display Name:</strong></td>
        <td valign="middle">
            <asp:TextBox ID="txtDisplayName" runat="server" SkinID="Text" Width="225"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td valign="middle" nowrap>&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td valign="middle" nowrap><strong>Locked:</strong></td>
        <td height="23" valign="middle"><asp:CheckBox ID="ckbLockout" runat="server"/></td>
      </tr>
  </table></td>
<td width="50" valign="top"><div class="spacer50x20" /></td>
<td valign="top"><table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="middle" nowrap><strong>Creation Date:</strong></td>
    <td width="20" rowspan="9" valign="middle" nowrap><div class="spacer10x20" /></td>
    <td height="23" valign="middle" nowrap><asp:Literal ID="CreationDate" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td height="23" valign="middle" nowrap><strong>Authorized:</strong></td>
    <td valign="middle" nowrap><asp:Literal ID="IsApproved" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td height="23" valign="middle" nowrap><strong>Is Locked Out:</strong></td>
    <td valign="middle" nowrap><asp:Literal ID="IsLockedOut" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td height="23" valign="middle" nowrap><strong>Is Online:</strong></td>
    <td valign="middle" nowrap><asp:Literal ID="IsOnline" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td height="23" valign="middle" nowrap><strong>Last Activity Date:</strong></td>
    <td valign="middle" nowrap><asp:Literal ID="LastActivityDate" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td rowspan="14" valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td height="23" valign="middle" nowrap><strong>Last Lockout Date:</strong></td>
    <td valign="middle" nowrap><asp:Literal ID="LastLockoutDate" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td valign="middle" nowrap><strong>Last Login Date:</strong></td>
    <td height="23" valign="middle" nowrap><asp:Literal ID="LastLoginDate" runat="server"></asp:Literal></td>
  </tr>
  <tr>
    <td valign="middle" nowrap>&nbsp;</td>
    <td valign="middle" nowrap>&nbsp;</td>
  </tr>
  <tr>
    <td valign="middle" nowrap><strong>Last Password Changed Date:</strong></td>
    <td height="23" valign="middle" nowrap><asp:Literal ID="LastPasswordChangedDate" runat="server"></asp:Literal></td>
  </tr>
</table></td>
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
        <td><div class="spacer10x20" /></td>
        <td><asp:ImageButton ID="btnRunAsUser" runat="server" Visible="false"></asp:ImageButton></td>
      </tr>
  </table></td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>