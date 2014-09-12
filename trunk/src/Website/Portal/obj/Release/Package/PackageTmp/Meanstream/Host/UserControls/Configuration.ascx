<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Configuration.ascx.vb" Inherits="Meanstream_Host_UserControls_Configuration" %>
<%@ Register Src="~/Meanstream/Host/UserControls/Portal.ascx" TagName="Portal" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Host/UserControls/Smtp.ascx" TagName="Smtp" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Host/UserControls/SQL.ascx" TagName="SQL" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Host/UserControls/Editor.ascx" TagName="Editor" TagPrefix="Meanstream" %>
<div align="left">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Configuration</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Configuration</b><br />Below is the Host application configuration. This will update the web.config file.', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                        <Meanstream:TabContainer ID="TabContainer1" runat="server" SkinID="Tabs">
                            <Meanstream:TabPanel ID="PortalTab" HeaderText="portal" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Portal ID="Portal" runat="server" />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                            <Meanstream:TabPanel ID="SmtpTab" HeaderText="Smtp" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Smtp ID="Smtp" runat="server" />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                            <Meanstream:TabPanel ID="EditorTab" HeaderText="HTML Editor" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Editor ID="Editor" runat="server" />
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
</div>
