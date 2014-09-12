<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Diagnostics.ascx.vb" Inherits="Meanstream_Administration_UserControls_Diagnostics" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/Tracing.ascx" TagName="Tracing" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/EventLogs.ascx" TagName="EventLogs" TagPrefix="Meanstream" %>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
<script>
    $().ready(function () {

        var model = Meanstream.UI.Models.Tracing.findAll(bindGrid);


    });
</script>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Diagnostics</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Diagnostics</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                            <Meanstream:TabPanel ID="TracingTab" HeaderText="tracing" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Tracing ID="Tracing" runat="server" /><br />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                            <Meanstream:TabPanel ID="EventLogsTab" HeaderText="event logs" runat="server">
                                <ContentTemplate>
                                    <Meanstream:EventLogs ID="EventLogs" runat="server" /><br />
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