<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ManageUser.ascx.vb" Inherits="Meanstream_Administration_UserControls_ManageUser" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/Credentials.ascx" TagName="Credentials" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/UserRoles.ascx" TagName="Roles" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/Password.ascx" TagName="Password" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/Profile.ascx" TagName="UserProfile" TagPrefix="Meanstream" %>

<div style="overflow:scroll;width:885px;height:1250px;">
<table width="858" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td width="20"><div class="spacer20x20" /></td>
    <td valign="top"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Managing User: <asp:Literal ID="litUser" runat="server"></asp:Literal></span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Manage User</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer10x20" /></td>
        </tr>
      </table>
        <div align="right">
         <Meanstream:TabContainer ID="TabContainer1" runat="server" SkinID="Tabs">
            <Meanstream:TabPanel ID="CredentialsTab" HeaderText="credentials" runat="server">
                <ContentTemplate>
                    <Meanstream:Credentials ID="Credentials" runat="server" />
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="PasswordTab" HeaderText="password" runat="server">
                <ContentTemplate>
                    <Meanstream:Password ID="Password" runat="server" />
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="RolesTab" HeaderText="roles" runat="server">
                <ContentTemplate>
                    <Meanstream:Roles ID="Roles" runat="server" />
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="ProfileTab" HeaderText="profile" runat="server">
                <ContentTemplate>
                    <Meanstream:UserProfile ID="UserProfile" runat="server" />
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
</div>