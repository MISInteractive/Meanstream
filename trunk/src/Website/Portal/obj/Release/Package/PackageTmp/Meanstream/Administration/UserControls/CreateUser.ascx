<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CreateUser.ascx.vb" Inherits="Meanstream_Administration_UserControls_CreateUser" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:HiddenField ID="litUserName" runat="server" />
<table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td width="20"><div class="spacer20x20" /></td>
    <td><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Create User</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Create User</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td valign="top"><strong>USER CREDENTIALS</strong><br>
            <br>
            <table border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="middle" nowrap>&nbsp;</td>
              <td width="20" rowspan="10" valign="middle"><div class="spacer10x20" /></td>
              <td valign="middle">&nbsp;</td>
            </tr>
            <tr>
              <td valign="middle" nowrap><strong>Username:</strong></td>
              <td valign="middle">
                <asp:TextBox ID="txtUsername" runat="server" SkinID="Text" Width="225"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td valign="middle" nowrap>&nbsp;</td>
              <td valign="middle">&nbsp;</td>
            </tr>
            <tr>
              <td valign="middle" nowrap><strong>Email:</strong></td>
              <td valign="middle">
                <asp:TextBox ID="txtEmail" runat="server" SkinID="Text" Width="225"></asp:TextBox>
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
              <td valign="middle" nowrap><strong>Authorize:</strong></td>
              <td height="20" valign="middle"><asp:CheckBox ID="chkAuthorize" runat="server" Checked="true"/></td>
            </tr>
            <tr>
              <td valign="middle" nowrap>&nbsp;</td>
              <td valign="middle">&nbsp;</td>
            </tr>
            <tr>
              <td valign="middle" nowrap><strong>Notify:</strong></td>
              <td height="20" valign="middle"><asp:CheckBox ID="chkNotify" runat="server" Checked="true"/></td>
            </tr>
          </table></td>
          <td width="50" valign="top"><div class="spacer50x20" /></td>
          <td valign="top"><strong>PASSWORD INFORMATION</strong><br>
            
            <asp:Panel ID="pSecurityQuestionRequired" runat="server">
            <br>
            <br>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><strong>Security Question:</strong></td>
                        <td width="20" valign="middle"><div class="spacer10x20" /></td>
                        <td>
                        <div>
                        <Meanstream:ComboBox ID="comboQuestion" runat=server  Width="225" ComboPanelHeight="140" ImageButtonWidth="25" DefaultDisplayValue="0" DefaultDisplayText="Select Security Question">
                        <Items>
                            <Meanstream:ComboBoxItem Text="Mother's maiden name?" Value="Mother's maiden name?" />
                            <Meanstream:ComboBoxItem Text="Place of birth?" Value="Place of birth?" />
                            <Meanstream:ComboBoxItem Text="Father's middle name?" Value="Father's middle name?" />
                            <Meanstream:ComboBoxItem Text="First pet's name?" Value="First pet's name?" />
                            <Meanstream:ComboBoxItem Text="Grandmother's first name?" Value="Grandmother's first name?" />
                        </Items>
                        </Meanstream:ComboBox>
                        </div>
                        </td>
                    </tr>
                </table>
                <br>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><strong>Security Answer:</strong></td>
                        <td width="20" valign="middle"><div class="spacer10x20" /></td>
                        <td>
                        <asp:TextBox ID="txtAnswer" runat=server CssClass="input" style="width:225px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br>
            <br>
            Optionally enter a password for this user, or allow the system<br>
            to generate a random password.<br>
            <br>
            <br>
            <table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="middle" nowrap><strong>Random Password:</strong></td>
                <td width="20" valign="middle"><div class="spacer10x20" /></td>
                <td height="20" valign="middle"><asp:CheckBox ID="chkGeneratePassword" runat="server" /></td>
              </tr>
            </table>
            <br>
            <br>
            <div align="left"><strong>-OR-</strong></div>
            <br>
            <br>
            <table border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="middle" nowrap><strong>Password:</strong></td>
                <td width="20" rowspan="3" valign="middle"><div class="spacer10x20" /></td>
                <td valign="middle">
                    <asp:TextBox ID="txtPassword" runat="server" SkinID="Text" Width="225" TextMode="Password"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td valign="middle" nowrap>&nbsp;</td>
                <td valign="middle">&nbsp;</td>
              </tr>
              <tr>
                <td valign="middle" nowrap><strong>Confirm Password:</strong></td>
                <td valign="middle">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" SkinID="Text" Width="225" TextMode="Password"></asp:TextBox>
                </td>
              </tr>
            </table></td>
        </tr>
      </table>
      <br>
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