<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Scheduler.ascx.vb" Inherits="Meanstream_Host_UserControls_Scheduler" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div align="left">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Scheduler</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Scheduler</b><br />', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                <td width="100%" class="nav2">
                    <div align="left">
                        <b>Scheduler Status:</b> <asp:Label ID="lblSchedulerStatus" runat="server" Text=""></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnToggle" runat="server"></asp:LinkButton>
                        <br><br>
                        
                        <table valign="top">
                            <tr><td colspan="2">Add New Task<br></td></tr>
                            <tr>
                                <td><br />
                                    Class name
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtTaskClassName" runat="server" Width="250" CssClass="textbox" Text=""></asp:TextBox> 
                                </td>
                            </tr>
                            <tr>
                                <td><br />
                                    Start Date / Time
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtStartDateTime" runat="server" Width="250" CssClass="textbox"></asp:TextBox> 
                                </td>
                            </tr>
                            <tr>
                                <td><br />
                                    Interval (minutes)
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtInterval" runat="server" Width="250" CssClass="textbox" Text=""></asp:TextBox> 
                                </td>
                            </tr>
                            <tr>
                                <td><br />
                                    Startup Type
                                </td>
                                <td><br />
                                    <Meanstream:ComboBox ID="StartupType"  runat="server" Width="250" ComboPanelHeight="140" ImageButtonWidth="25" DefaultDisplayValue="0" DefaultDisplayText="Select">
                                        <Items>
                                            <Meanstream:ComboBoxItem Text="Automatic" Value="0" />
                                            <Meanstream:ComboBoxItem Text="RunOnce" Value="1" />
                                            <Meanstream:ComboBoxItem Text="Disabled" Value="2" />
                                        </Items>
                                    </Meanstream:ComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div align="right">
                                        <br />
                                        <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                                    </div>   
                                </td>
                            </tr>
   
                        </table>
                    </div>
                    <br />
                </td>
                <td width="20"><div class="spacer10x20" /></td>
              </tr>
          </table>

          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td><div class="spacer20x20" /></td>
                <td class="nav2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td width="20"><div class="spacer10x20" /></td>
                <td colspan="2">
                    Scheduled Tasks<br>
                    <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                        <Meanstream:Grid ID="Grid" runat="server" AllowPaging="false"
                            ShowFooter="false" Width="100%" GridMode="Standard">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid">
                                            <Meanstream:ColumnHeader ColumnWidth="25%" HeaderText="Classname" />
                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="Status" />
                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="StartupType" />
                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="Interval" />
                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="NextRunTime" />
                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="LastRunTime" />
                                            <Meanstream:ColumnHeader ColumnWidth="25%" HeaderText="LastRunResult" />
                                        </Meanstream:GridHeader>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <Meanstream:GridItemView ID="itemdata" runat="server">
                                            <Meanstream:GridItemColumns ColumnWidth="25%" ID="GridItemColumns1" runat="server">
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                <b><%# Eval("Type")%></b>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns2" runat="server">
                                                <%# Eval("Status")%>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" runat="server">
                                                <asp:PlaceHolder ID="phStartupType" runat="server" EnableViewState="true" Visible="false"></asp:PlaceHolder>
                                                <%# Eval("StartupType")%>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns4" runat="server">
                                                <%# FormatInterval(Eval("StartupType"), Eval("Interval"))%>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns5" runat="server">
                                                <%# NextRun(Eval("Status"), Eval("NextRunTime"))%>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns6" runat="server">
                                                <%# LastRun(Eval("LastRunTime"))%>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="25%" ID="GridItemColumns7" runat="server" IsLast="true">
                                                <%# LastRunResult(Eval("LastRunResult"))%>
                                            </Meanstream:GridItemColumns>
                                        </Meanstream:GridItemView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <Meanstream:ButtonField ButtonMode="None" ButtonType="Button" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" Text="Run Now" CommandName="Run"
                                                                                OnClientClick="javascript:if (!confirm('Run the scheduled task?')) return;" />
                                <Meanstream:ButtonField ButtonMode="None" ButtonType="Button" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" Text="Start" CommandName="Stop"
                                                                                OnClientClick="javascript:if (!confirm('Stop the scheduled task?')) return;" >
                                </Meanstream:ButtonField>
                                <Meanstream:ButtonField ButtonMode="None" ButtonType="Button" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" Text="Delete" CommandName="Delete"
                                                                                OnClientClick="javascript:if (!confirm('Delete the scheduled task? This cannot be undone.')) return;" >
                                </Meanstream:ButtonField>
                            </Columns>
                        </Meanstream:Grid>
                    </Meanstream:GridContainer>
                        
                </td>
                
            </tr>
         </table>
      </td>
    </tr>
    
  </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>