<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SearchEngine.ascx.vb" Inherits="Meanstream_Administration_UserControls_SearchEngine" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/Sitemap.ascx" TagName="Sitemap" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Administration/UserControls/SearchEngineSubmit.ascx" TagName="SearchEngineSubmit" TagPrefix="Meanstream" %>

<div align="left">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Search Engine</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Search Engine</b><br />', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                <td>
                    <div align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <Meanstream:TabContainer ID="TabContainer1" runat="server" SkinID="Tabs">
                                    <Meanstream:TabPanel ID="SitemapTab" HeaderText="sitemap" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:Sitemap ID="Sitemap" runat="server" />
                                        </ContentTemplate>
                                    </Meanstream:TabPanel>
                                    <Meanstream:TabPanel ID="SearchEngineSubmitTab" HeaderText="search engine submit" runat="server">
                                        <ContentTemplate>
                                            <Meanstream:SearchEngineSubmit ID="SearchEngineSubmit" runat="server" />
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
</div>
