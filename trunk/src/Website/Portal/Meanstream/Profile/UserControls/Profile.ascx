<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Profile.ascx.vb" Inherits="Meanstream_Profile_UserControls_Profile" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink"><asp:Literal ID="litName" runat="server"></asp:Literal></span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Profile</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                        <table  border="0" cellspacing="0" cellpadding="0">
                            <tr valign="top">
                                <td style="padding-right: 15px;">
                                    <asp:Image ID="imgPortrait" runat="server" />
                                    <br>
                                    <center>
                                        <asp:HyperLink ID="btnFacebook" runat="server" Target="_blank">My Facebook Page</asp:HyperLink></center>
                                    <br>
                                </td>
                                <td width="390">
                                    <table width="290" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10" class="content">
                                                <b>From</b><br>
                                                <asp:Literal ID="litCity" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                                Member Since<br>
                                                <asp:Literal ID="litMemberSince" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10" >
                                                <b>Last Visit</b><br>
                                                <asp:Literal ID="litLastVisit" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                                <b>Website</b><br>
                                                <asp:HyperLink ID="btnWebsite" runat="server" Target="_blank"></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="540px" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <b>About Me</b>
                                    <br>
                                    <asp:Literal ID="litAboutMe" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
          
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
