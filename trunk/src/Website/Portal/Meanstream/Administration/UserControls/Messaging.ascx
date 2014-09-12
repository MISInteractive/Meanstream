<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Messaging.ascx.vb" Inherits="Meanstream_Administration_UserControls_Messaging" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/RegistrationMessage.ascx" TagName="Registration" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/ResetPasswordMessage.ascx" TagName="ResetPassword" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/ForgotPasswordMessage.ascx" TagName="ForgotPassword" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/AddUserToRoleMessage.ascx" TagName="AddUserToRole" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/AddRoleToUserMessage.ascx" TagName="AddRoleToUser" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/Smtp.ascx" TagName="Smtp" TagPrefix="Meanstream" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Messaging</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Messaging</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
          </tr>
        </table>
          <br></td>
    </tr>
    <tr>
      <td class="tab-bg">
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><div class="spacer20x20" /></td>
                <td class="nav2">&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td width="20"><div class="spacer10x20" /></td>
                <td width="100%" class="nav2">
                    <div align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <Meanstream:TabContainer ID="TabContainer1" runat="server" SkinID="Tabs">
                                    <Meanstream:TabPanel ID="RegistrationTab" HeaderText="registration" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:Registration ID="Registration" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                    <Meanstream:TabPanel ID="ResetPasswordTab" HeaderText="Reset Password" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:ResetPassword ID="ResetPassword" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                    <Meanstream:TabPanel ID="ForgotPasswordTab" HeaderText="Forgot Password" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:ForgotPassword ID="ForgotPassword" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                    <Meanstream:TabPanel ID="AddUserToRoleTab" HeaderText="Add User To Role" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:AddUserToRole ID="AddUserToRole" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                    <Meanstream:TabPanel ID="AddRoleToUserTab" HeaderText="Add Role To User" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:AddRoleToUser ID="AddRoleToUser" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                    <Meanstream:TabPanel ID="SmtpTab" HeaderText="Bulkmail" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:Smtp ID="Smtp" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                </Meanstream:TabContainer>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <br />
                </td>
                <td width="20"><div class="spacer10x20" /></td>
              </tr>
          </table>
      </td>
    </tr>
  </table>