<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ViewPage.ascx.vb" Inherits="Meanstream_Pages_UserControls_ViewPage" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/TreeView.ascx" TagName="TreeView" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/PageFunctions.ascx" TagName="PageFunctions" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/Versions.ascx" TagName="Versions" TagPrefix="Meanstream" %>

<%--<script src="./Scripts/pageFunctions.js" language="javascript" type="text/javascript"></script>--%>
<table width="100%" border="0" cellspacing="0" cellpadding="0">        
        <tr valign="top">
          <td valign="top">
            <Meanstream:TreeView ID="Treeview" runat="server" />
          </td>
          <td width="20"><div class="spacer10x20" /></td>
          <td width="75%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><table border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td nowrap><span class="largelink"><asp:Literal ID="litBreadcrumb" runat="server"></asp:Literal></span></td>
                    <td><div class="spacer10x10" /></td>
                    <td><a onmouseover="Tip('Page', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
                    <td><table border="0" align="right" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="16"><div class="spacer10x35"></td>
                        <td width="16" height="20"><a href="./Default.aspx?ctl=Pages"><div class="icon-prevsmall"></div></a></td>
                        <td width="3"><div class="spacer10x3" /></td>
                        <td nowrap class="subnavsmall"><a href="./Default.aspx?ctl=Pages">go back to all pages</a></td>
                      </tr>
                    </table></td>
                  </tr>
              </table></td>
              <td><div align="right"></div></td>
            </tr>
          </table>
          <div align="right">
            <Meanstream:TabContainer ID="tabContainer" runat=server Width="100%" SkinID="Tabs" ActiveTabIndex="0">
                <Meanstream:TabPanel ID="ViewTab" HeaderText="View" runat="server">
                    <ContentTemplate>
                        <Meanstream:PageFunctions ID="PageFunctions" runat="server" />
                    </ContentTemplate>
                </Meanstream:TabPanel>
                <Meanstream:TabPanel ID="WorkflowsTab" HeaderText="workflows" runat="server" Visible="false">
                    <ContentTemplate>
                        
                    </ContentTemplate>
                </Meanstream:TabPanel>
                <Meanstream:TabPanel ID="HistoryTab" HeaderText="history" runat="server">
                        <ContentTemplate>
                             <Meanstream:Versions ID="Versions" runat="server" />
                        </ContentTemplate>
                </Meanstream:TabPanel>
            </Meanstream:TabContainer>
          </div>
          <br>
          <br>
          <br>
          </td>
        </tr>
      </table>