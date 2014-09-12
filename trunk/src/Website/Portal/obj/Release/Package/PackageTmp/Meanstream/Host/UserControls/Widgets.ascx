<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Widgets.ascx.vb" Inherits="Meanstream_Host_UserControls_Widgets" %>
<script type="text/javascript">
    function windowClose() {
        var clientid = '<%= btnCreateWidget.ClientID %>';
        msCloseWin();
        window.location.href = './Default.aspx?ctl=Widgets';
    }
</script>  
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<div align="left">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Widgets</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Widgets</b><br />Below is the Widget definition configuration. These widgets are available to the client during page editing.', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
          </tr>
        </table>
          <br></td>
          <td><div align="right">
          <table border="0" align="right" cellpadding="0" cellspacing="0">
            <tr>
              <td width="12"><div class="icon-widget"></div></td>
              <td width="6"><div class="spacer10x6" /></td>
              <td nowrap class="subnav">
                    <asp:LinkButton ID="CreateWidgetTarget" runat="server">create a widget</asp:LinkButton>
                    <Meanstream:Window ID="btnCreateWidget" runat="server" 
                        SkinID="Window" 
                        Width="650" 
                        Height="375" 
                        ShowLoader="true" 
                        ShowUrl="true" 
                        Title="Create Widget" 
                        NavigateUrl="Module.aspx?ctl=CreateWidget" 
                        OnClientClose="windowClose()">
                    </Meanstream:Window>
              </td>
            </tr>
          </table>
        </div></td>
    </tr>
    <tr>
      <td width="100%" colspan="2">
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
                        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                        <Meanstream:Grid ID="WidgetsGrid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="WidgetsGrid">
                                            <Meanstream:ColumnHeader ColumnWidth="70%" HeaderText="name" />
                                            <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="enabled" />
                                        </Meanstream:GridHeader>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <Meanstream:GridItemView ID="itemdata" runat=server>
                                            <Meanstream:GridItemColumns ColumnWidth="70%" ID="GridItemColumns2" runat=server>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                <b><%#Eval("FriendlyName")%></b>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns3" IsLast=true runat=server>
                                                <asp:Checkbox ID="Enabled" runat="server" Checked='<%# Eval("Enabled") %>'></asp:Checkbox>
                                            </Meanstream:GridItemColumns>
                                        </Meanstream:GridItemView>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                            </Columns>
                        </Meanstream:Grid>
                        <Meanstream:DataPager ID="pager" runat="server" PageSize="20" PagedControlID="WidgetsGrid" Width="100%">
                            <Fields>
                                <Meanstream:PreviousPagerField />
                                <Meanstream:NumericPagerField />
                                <Meanstream:NextPagerField />
                                <Meanstream:TemplatePagerField>
                                    <PagerTemplate>
                                        </div>
                                        <div class="MSPagerData">                            
                                            Page
                                            <asp:Label runat="server" ID="CurrentPageLabel" Text='<%# If(Container.TotalRowCount>0, (Container.StartRowIndex / Container.PageSize) + 1, 0) %>' />
                                            of
                                            <asp:Label runat="server" ID="TotalPagesLabel" Text='<%# Math.Ceiling(CDbl(Container.TotalRowCount) / Container.PageSize) %>' />
                                            (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" /> records)
                                        </div>
                                    </PagerTemplate>
                                </Meanstream:TemplatePagerField>
                            </Fields>
                        </Meanstream:DataPager>
                        </Meanstream:GridContainer>
                        <br />
                        <div style="width:100%;" align="right">
                            <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                        </div>
                    </div>
                    <br />
                    <br />
                </td>
              </tr>
          </table>
      </td>
    </tr>
  </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
