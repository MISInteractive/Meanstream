<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Password.ascx.vb" Inherits="Meanstream_Administration_UserControls_Password" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><strong>Managing password for <asp:Label ID="lblUsername" runat="server"></asp:Label>:</strong><br>
      <br>
      <table border="0" cellspacing="0" cellpadding="0">
          
          <tr>
            <td valign="middle" nowrap><strong>Password Last Changed:</strong></td>
            <td width="20" rowspan="3" valign="middle"><div class="spacer10x20" /></td>
            <td height="23" valign="middle" nowrap><asp:Literal ID="LastPasswordChangedDate" runat="server"></asp:Literal></td>
          </tr>
          <tr>
            <td valign="middle" nowrap>&nbsp;</td>
            <td valign="middle">&nbsp;</td>
          </tr>
          <tr>
            <td valign="middle" nowrap><strong>Password Expires:</strong></td>
            <td valign="middle" nowrap>Password never expires</td>
          </tr>

      </table>
        <br>
        <br>
        <br>
        <strong>Change Password</strong><br>
        To change a password for this user enter the new password and confirm the entry by typing it again.<br>
        <br>
        <table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="middle" nowrap><strong>Password:</strong></td>
            <td width="20" rowspan="3" valign="middle"><div class="spacer10x20" /></td>
            <td valign="middle">
                <asp:TextBox ID="txtPassword" runat="server" SkinID="Text" Width="225"></asp:TextBox>
            </td>
            <td rowspan="3" valign="middle"><div class="spacer10x20" /></td>
            <td valign="middle">&nbsp;</td>
          </tr>
          <tr>
            <td valign="middle" nowrap>&nbsp;</td>
            <td valign="middle">&nbsp;</td>
            <td valign="middle">&nbsp;</td>
          </tr>
          <tr>
            <td valign="middle" nowrap><strong>Confirm Password:</strong></td>
            <td valign="middle">
                <asp:TextBox ID="txtConfirmPassword" runat="server" SkinID="Text" Width="225"></asp:TextBox>
            </td>
            <td valign="middle"><Meanstream:BlockUIImageButton ID="btnChangePassword" runat="server" StartMessage="Saving..." /></td>
          </tr>
        </table>
        <br>
        
        <br>
        <br>
        <strong>Reset Password</strong><br>
      By resetting the password it will generate a new password for the user and email the password to the user.<br>
      <br>
      <Meanstream:BlockUIImageButton ID="btnResetPassword" runat="server" StartMessage="Sending new password..." /><br></td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>