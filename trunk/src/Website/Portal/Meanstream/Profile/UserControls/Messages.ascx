<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Messages.ascx.vb" Inherits="Meanstream_Profile_UserControls_Messages" %>
<%@ Register Src="~/Meanstream/Profile/UserControls/Received.ascx" TagName="Received" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Profile/UserControls/Sent.ascx" TagName="Sent" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Profile/UserControls/Compose.ascx" TagName="Compose" TagPrefix="Meanstream" %>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Messages</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Messages</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                        <Meanstream:TabContainer ID="TabContainer" runat="server" SkinID="Tabs">
                            <Meanstream:TabPanel ID="ReceivedTab" HeaderText="received" runat="server">            
                                <ContentTemplate>
                                    <Meanstream:Received ID="Received" runat="server" />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                            <Meanstream:TabPanel ID="SentTab" HeaderText="sent" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Sent ID="Sent" runat="server" />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                            <Meanstream:TabPanel ID="ComposeTab" HeaderText="compose" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Compose ID="Compose" runat="server" />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                        </Meanstream:TabContainer>
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