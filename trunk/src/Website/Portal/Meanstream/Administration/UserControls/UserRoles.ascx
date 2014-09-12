<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserRoles.ascx.vb" Inherits="Meanstream_Administration_UserControls_UserRoles" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><strong>Current Security Roles for <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>:</strong><br>
      <br>
      <asp:DataList ID="UserGroupList" runat="server" Visible="true">
            <HeaderTemplate>
                <table border="0" cellspacing="0" cellpadding="0">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td valign="middle" nowrap><table border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="12">
                            <a  href="Module.aspx?ctl=ManageUser&Action=Remove&RoleName=<%#DataBinder.Eval(Container.DataItem, "Name")%>&uid=<%=Request.params("uid")%>">
                           <%# DisplayRemoveImage(DataBinder.Eval(Container.DataItem, "Name"))%>
                        </td>
                        <td width="6"><div class="spacer10x6" /></td>
                        <td nowrap class="subnav">
                            <a  href="Module.aspx?ctl=ManageUser&Action=Remove&RoleName=<%#DataBinder.Eval(Container.DataItem, "Name")%>&uid=<%=Request.params("uid")%>">
                           <%# DisplayRemove(DataBinder.Eval(Container.DataItem, "Name"))%>
                        </td>
                      </tr>
                    </table></td>
                    <td width="20" rowspan="3" valign="middle"><div class="spacer10x20" /></td>
                    <td height="23" valign="middle"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
                  </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td valign="middle" nowrap>&nbsp;</td>
                    <td valign="middle">&nbsp;</td>
                  </tr>
            </SeparatorTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>    
      </asp:DataList>
        <br>
        <br>
        <table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="23" valign="middle" nowrap><table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="12"><asp:CheckBox ID="chkSendNotification" runat="server" Checked="true" /></td>
                  <td width="6"><div class="spacer10x6" /></td>
                  <td><strong>Send Notification to <asp:Label ID="lblUsername2" runat="server" Text="Username"></asp:Label> when edits are made to the user's Role(s)</strong></td>
                </tr>
            </table></td>
          </tr>
        </table>
        <br>
        <strong><br>
        Add New Role for <asp:Label ID="lblUsername3" runat="server" Text="Username"></asp:Label>:</strong><br>
        <br>
        <table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="middle" nowrap>
                <Meanstream:ComboBox ID="AvailableRoles"  runat="server" Width="225" ComboPanelHeight="140" ImageButtonWidth="25" DefaultDisplayValue="0" DefaultDisplayText="Select Role"></Meanstream:ComboBox>
            </td>
            <td width="20" valign="middle"><div class="spacer10x20" /></td>
            <td height="23" valign="bottom"><Meanstream:BlockUIImageButton ID="btnAddGroupToUser" runat="server" StartMessage="Saving..." SuccessMessage="Save successful"></Meanstream:BlockUIImageButton></td>
          </tr>
        </table>
      <br>
      </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>