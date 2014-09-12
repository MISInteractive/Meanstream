<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyProfile.ascx.vb" Inherits="Meanstream_Profile_UserControls_MyProfile" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CuteEditor" %>
<%@ Register Src="~/Meanstream/Profile/UserControls/ChangePassword.ascx" TagName="ChangePassword" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Profile/UserControls/Theme.ascx" TagName="Theme" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/Profile/UserControls/Dashboard.ascx" TagName="Dashboard" TagPrefix="Meanstream" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">My Profile</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>My Profile</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                        <Meanstream:TabContainer ID="TabContainer" runat=server SkinID="Tabs">
                            <Meanstream:TabPanel ID="ProfileTab" HeaderText="profile" runat="server">            
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
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
                                                    <asp:HiddenField ID="HiddenImage" runat="server" />
                                    
                                                    <table>
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Image ID="imgPortrait" runat="server" />
                                                                <br>
                                                                <asp:Label ID="lblPictureMessage" runat="server" CssClass="status"
                                                                    Text="You have not uploaded an image to your profile."></asp:Label>
                                                                <br>
                                                                <table width="250" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:FileUpload ID="FileUpload" runat="server" CssClass="textbox" />
                                                                        </td>
                                                                    </tr>
                                            
                                                                </table>
                                                                <div align="right" style="width: 235px;"><br />
                                                                    <asp:ImageButton ID="ImgUpload" runat="server" />
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:Table ID="tblAccount" runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <asp:Table ID="tblProfile" runat="server">
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell Width="100px">
                                                                                        <asp:Label ID="lblFirstName" runat="server" Font-Bold="true" Text="First Name:"></asp:Label>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:Label ID="lblLastName" runat="server" Font-Bold="true" Text="Last Name:"></asp:Label>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:Label ID="lblCity" runat="server" Font-Bold="true" Text="City:"></asp:Label>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:Label ID="lblWebsite" runat="server" Font-Bold="true" Text="Website:"></asp:Label>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="textbox" Width="300"></asp:TextBox>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell><br />
                                                                                        <asp:Label ID="lblAboutMe" runat="server" Font-Bold="true" Text="About Me:"></asp:Label>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell><br />
                                                                                        <CuteEditor:Editor ID="HTMLEditor" AutoConfigure="Minimal" ShowBottomBar="false" Height="200" Width="400" runat="server" />
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                            
                                                                            </asp:Table>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                                <br />
                                                                <asp:Table ID="tblSave" runat="server" HorizontalAlign="Right">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell ColumnSpan="2">
                                                                            <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
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
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                               </Meanstream:TabPanel>

                               <Meanstream:TabPanel ID="PasswordTab" HeaderText="password" runat="server">
                                <ContentTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td><div class="spacer20x20" /></td>
                                            <td class="nav2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td width="20"><div class="spacer10x20" /></td>
                                            <td width="100%" class="nav2">
                                                <Meanstream:ChangePassword ID="ChangePassword" runat="server" />
                                            </td>
                                          </tr>
                                     </table>
                                </ContentTemplate>
                               </Meanstream:TabPanel>

                               <Meanstream:TabPanel ID="ThemeTab" HeaderText="theme" runat="server">
                                <ContentTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td><div class="spacer20x20" /></td>
                                            <td class="nav2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td width="20"><div class="spacer10x20" /></td>
                                            <td width="100%" class="nav2">
                                                <Meanstream:Theme ID="Theme" runat="server" />
                                            </td>
                                          </tr>
                                     </table>
                                </ContentTemplate>
                               </Meanstream:TabPanel>

                               <Meanstream:TabPanel ID="DashboardTab" HeaderText="dashboard" runat="server">
                                <ContentTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td><div class="spacer20x20" /></td>
                                            <td class="nav2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td width="20"><div class="spacer10x20" /></td>
                                            <td width="100%" class="nav2">
                                                <Meanstream:Dashboard ID="Dashboard" runat="server" />
                                            </td>
                                          </tr>
                                     </table>
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