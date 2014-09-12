<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SQL.ascx.vb" Inherits="Meanstream_Host_UserControls_SQL" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers>
  <asp:PostBackTrigger ControlID="ImgUpload" />
 </Triggers>
<ContentTemplate>
<div align="left">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td nowrap>
                            <span class="largelink">SQL</span>
                        </td>
                        <td>
                            <div class="spacer10x20" />
                        </td>
                        <td>
                            <a onmouseover="Tip('<b>SQL</b><br />', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)"
                                onmouseout="UnTip()"">
                                <div class="icon-help">
                                </div>
                            </a>
                        </td>
                    </tr>
                </table>
                <br>
            </td>
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
                    <td width="100%" class="nav2">
                        <div align="left">
                            <table>
                                <tr>
                                    <td valign="top">
                                        SQL File: &nbsp;
                                    </td>
                                    <td>
                                       <asp:FileUpload ID="FileUpload" runat="server" CssClass="textbox" /> &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImgUpload" runat="server" ImageAlign="Middle" />
                                    </td>
                                </tr>   
                                <tr>
                                    <td valign="top"><br>
                                        SQL Script:&nbsp;
                                    </td>
                                    <td><br>
                                        <asp:TextBox ID="txtScript" runat="server" Width="450" Height="300" TextMode="MultiLine" CssClass="textbox"></asp:TextBox> 
                                    </td>
                                </tr>   
                            </table>
                            <br />
                        
                            <div style="width:525px;" align="right">
                                <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Executing Query..." />
                            </div>
                    </div>
                </td>
                <td width="20"><div class="spacer10x20" /></td>
              </tr>
          </table>
          <br><br>
      </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grid" runat="server" AutoGenerateColumns="false">
            </asp:GridView>
        </td>
    </tr>
  </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>