<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Meanstream_Default" MasterPageFile="~/Meanstream/UI/Skins/Default.master" %>
<%@ Register Src="~/Meanstream/Dashboard/UserStats.ascx" TagName="UserStats" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Dashboard/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Dashboard/Announcements.ascx" TagName="Announcements" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Dashboard/MyRecentEdits.ascx" TagName="MyRecentEdits" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Dashboard/RecentPublishedPages.ascx" TagName="RecentPublishedPages" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Dashboard/RecentPosts.ascx" TagName="RecentPosts" TagPrefix="Meanstream" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPane" runat="server">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td colspan="3" class="dashboardtoptable">
          
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="3" class="dash-line"><div class="spacer20x1" /></td>
        </tr>
        <tr>
            <td width="1" class="dash-line"><div class="spacer1x1" /></td>
            <td class="dash-top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                <td colspan="3"><div class="spacer20x20" /></td>
                </tr>
                <tr>
                <td width="20"><div class="spacer20x20" /></td>
                <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top"><a href="./Pages/Default.aspx?ctl=Pages"><div class="icon-pages"></div></a></td>
                        <td valign="top"><div class="spacer15x20" /></td>
                        <td width="33%" valign="top"><div align="left"><span class="largelink"><a href="./Pages/Default.aspx?ctl=Pages">Pages</a></span><br>
                        Update pages, add pages to your site, view what skin pages are using.</div></td>
                        <td width="40" rowspan="5" valign="top"><div class="spacer40x20" /></td>
                        <td valign="top"><a href="http://misonline.vresp.com" target="_blank"><div class="icon-marketing"></div></a></td>
                        <td valign="top"><div class="spacer15x20" /></td>
                        <td width="33%" valign="top"><div align="left"><span class="largelink"><a href="http://misonline.vresp.com" target="_blank">Marketing</a></span><br>
                        Send email blasts.</div></td>
                        <td width="40" rowspan="5"><div class="spacer40x20" /></td>
                        <td width="33%" rowspan="5"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                            <td width="5"><div class="dash-help-left" /></td>
                            <td class="dash-help-mid" ><table width="100%" height="184px" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="15"><div class="spacer35x20" /></td>
                                    <td><div align="left"><strong>NEED HELP?</strong><br>
                                            <br>
                                    Anytime you see the following help icon:<br>
                                    <br>
                                    <div class="icon-help"></div><br>
                                       
                                    Just select it for more information about the area it is placed.</div></td>
                                    <td width="15"><div class="spacer35x20" /></td>
                                </tr>
                            </table>
			
				</td>
                            <td width="5"><div class="dash-help-right" /></td>
                            </tr>
                        </table></td>
                    </tr>
                    <tr>
                        <td valign="top"><div class="spacer20x20" /></td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="top"><a href="javascript:callFileManager()"><div class="icon-files"></div></a></td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top"><div align="left"><span class="largelink"><a href="javascript:callFileManager()">Files</a></span><br>
                        Explore, add and share site files and folders.</div></td>
                        <td valign="top"><a href="./Administration/Default.aspx?ctl=Users"><div class="icon-admin"></div></a></td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top"><div align="left"><span class="largelink"><a href="./Administration/Default.aspx?ctl=Users">User Accounts</a></span><br>
                        Manage users and roles.</div></td>
                    </tr>
                    <tr>
                        <td valign="top"><div class="spacer20x20" /></td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="top"><a href="./Profile/Default.aspx?ctl=Messages"><div class="icon-community"></div></a></td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top"><div align="left"><span class="largelink"><a href="./Profile/Default.aspx?ctl=Messages">Messages</a></span><br>
                        View Inbox.</div></td>
                        <td valign="top">
                        <asp:HyperLink ID="HomeLink1" runat="server" Target="_blank"><div class="icon-live"></div></asp:HyperLink></td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top"><div align="left"><span class="largelink"><asp:HyperLink ID="HomeLink2" runat="server" Target="_blank">Live Site</asp:HyperLink></span><br>
                        A link to your site in production.</div></td>
                    </tr>
                </table></td>
                <td width="20"><div class="spacer20x20" /></td>
                </tr>
                <tr>
                <td colspan="3"><div class="spacer20x20" /></td>
                </tr>
            </table></td>
            <td width="1" class="dash-line"><div class="spacer1x1" /></td>
        </tr>
        <tr>
            <td colspan="3" class="dash-line"><div class="spacer20x1" /></td>
        </tr>
        </table></td>
        </tr>
        
    <tr>
        <td colspan="3"><div class="spacer20x20" /></td>
        </tr>
    <tr>
        <td width="25%" valign="top">
          
        <Meanstream:UserStats ID="UserStats" runat="server" Visible="False" />
          
          
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td><div class="spacer20x20" /></td>
            </tr>
        </table>
            
        <Meanstream:QuickLinks ID="QuickLinks" runat="server" Visible="False" />
            
        </td>
        <td width="20"><div class="spacer20x10" /></td>
        <td width="75%" valign="top">
          
        <Meanstream:MyRecentEdits ID="MyRecentEdits" runat="server" Visible="False" />
            
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td><div class="spacer20x20" /></td>
            </tr>
        </table>
            
        <Meanstream:RecentPublishedPages ID="RecentPublishedPages" runat="server" Visible="False" />
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td><div class="spacer20x20" /></td>
            </tr>
        </table>
          
        </td>
    </tr>
    </table>
</asp:Content>