<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ManageRole.ascx.vb" Inherits="Meanstream_Administration_UserControls_ManageRole" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/EditRole.ascx" TagName="Edit" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/ManageUsersInRole.ascx" TagName="ManageUsers" TagPrefix="Meanstream" %>

<table width="858" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td width="20"><div class="spacer20x20" /></td>
    <td valign="top"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Managing Role: <asp:Literal ID="litRole" runat="server"></asp:Literal></span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Manage Role</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer10x20" /></td>
        </tr>
      </table>
        <div align="right">
            <Meanstream:TabContainer ID="TabContainer1" runat="server" SkinID="Tabs">
                <Meanstream:TabPanel ID="EditTab" HeaderText="settings" runat="server">
                    <ContentTemplate>
                        <Meanstream:Edit ID="Edit" runat="server" />
                    </ContentTemplate>
                </Meanstream:TabPanel>
                <Meanstream:TabPanel ID="ManageUsersTab" HeaderText="manage users" runat="server">
                    <ContentTemplate>
                        <Meanstream:ManageUsers ID="ManageUsers" runat="server" />
                    </ContentTemplate>
                </Meanstream:TabPanel>
            </Meanstream:TabContainer>
         </div> 
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
