<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RecycleBin.ascx.vb" Inherits="Meanstream_Pages_UserControls_RecycleBin" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/RecycleBinPages.ascx" TagName="Pages" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/RecycleBinWidgets.ascx" TagName="Widgets" TagPrefix="Meanstream" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Recycle Bin</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Recycle Bin</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
          </tr>
        </table>
          <br /></td>
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
                            <Meanstream:TabPanel ID="PagesTab" HeaderText="pages" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Pages ID="Pages" runat="server" />
                                </ContentTemplate>
                            </Meanstream:TabPanel>
                            <Meanstream:TabPanel ID="WidgetsTab" HeaderText="Widgets" runat="server">
                                <ContentTemplate>
                                    <Meanstream:Widgets ID="Widgets" runat="server" />
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