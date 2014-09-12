<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GoogleAnalytics.ascx.vb" Inherits="Meanstream_Administration_UserControls_GoogleAnalytics" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div align="left">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Google Analytics</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Google Analytics</b><br />', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
          </tr>
        </table>
          <br></td>
    </tr>
    <tr>
      <td>
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
                        <table valign="top">
                            <tr>
                                <td><br />
                                    Script:
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtScript" runat="server" Width="350" CssClass="textbox" Text="" TextMode="MultiLine" Height="100"></asp:TextBox> 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div align="right" style="width: 453px;">
                                        <br />
                                        <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                                    </div>   
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
</div>
</ContentTemplate>
</asp:UpdatePanel>