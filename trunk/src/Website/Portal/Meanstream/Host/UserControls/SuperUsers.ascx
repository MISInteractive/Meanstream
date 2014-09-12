<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SuperUsers.ascx.vb" Inherits="Meanstream_Host_UserControls_SuperUsers" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<div align="left">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td nowrap>
                            <span class="largelink">SuperUser Accounts</span>
                        </td>
                        <td>
                            <div class="spacer10x20" />
                        </td>
                        <td>
                            <a onmouseover="Tip('<b>SuperUser Accounts</b><br />', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)"
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
                        <td>
                            <div class="spacer20x20" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="20">
                            <div class="spacer10x20" />
                        </td>
                        <td>
                            <div align="left">
                               
                                            <table align="left" Width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellspacing="0" cellpadding="0" align="left">
                                                            <tr>
                                                                <td colspan="2" align="left">
                                                                    <b>Manage Users in Role:</b>
                                                                        <asp:Label ID="lblRoleName" runat="server" Text="RoleName" Font-Bold="true"></asp:Label>
                                                                    
                                                                    <br>
                                                                    <br>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left">
                                                                    <b>Available Users:</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <Meanstream:ComboBox ID="ddlAvailableUsers"  runat="server" Width="225" ComboPanelHeight="140" ImageButtonWidth="25" DefaultDisplayValue="0" DefaultDisplayText="Select User"></Meanstream:ComboBox>
                                       
                                                                </td>
                                                                <td align="left" valign="bottom">
                                                                    &nbsp;
                                                                    <Meanstream:BlockUIImageButton ID="btnAddUserToGroup" runat="server" StartMessage="Saving..." />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left"><br>
                                                                    <asp:CheckBox ID="chkSendNotification" runat="server" Checked="true" />
                                                                    <b>Send Notification?</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br>
                                                        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                                                            <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                                                            <Meanstream:Grid ID="UsersInGroupGrid" AllowSorting="true" runat="server" AllowPaging="true"
                                                                ShowFooter="false" Width="100%" GridMode="Standard">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <Meanstream:GridHeader ID="Header1" runat="server" GridId="UsersInGroupGrid">
                                                                                <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="username" IsSortable="true"
                                                                                    SortExpression="SkinRoot" />
                                                                                <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="is anonymous" />
                                                                                <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="last activity date" />
                                                                            </Meanstream:GridHeader>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <Meanstream:GridItemView ID="itemdata" runat="server">
                                                                                <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns1" runat="server">
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("UserName") %>' Visible="false"></asp:Label>
                                                                                    <b><%#Eval("UserName")%></b>
                                                                                </Meanstream:GridItemColumns>
                                                                                <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns2" runat="server">
                                                                                    <%#Eval("IsAnonymous")%>
                                                                                </Meanstream:GridItemColumns>
                                                                                <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns3" IsLast="true"
                                                                                    runat="server">
                                                                                    <b>
                                                                                        <%#Eval("LastActivityDate")%></b>
                                                                                </Meanstream:GridItemColumns>
                                                                            </Meanstream:GridItemView>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <Meanstream:ButtonField ButtonMode="None" ButtonType="Link" ItemStyle-VerticalAlign="Middle" Text="Remove" CommandName="Remove"
                                                                        OnClientClick="javascript:if (!confirm('The user will be removed from this group. ')) return;">
                                                                    </Meanstream:ButtonField>
                                                                </Columns>
                                                            </Meanstream:Grid>
                                                            <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="UsersInGroupGrid"
                                                                Width="100%">
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
                                                                                (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                                                                                records)
                                                                            </div>
                                                                        </PagerTemplate>
                                                                    </Meanstream:TemplatePagerField>
                                                                </Fields>
                                                            </Meanstream:DataPager>
                                                        </Meanstream:GridContainer>
                                                    </td>
                                                </tr>
                                            </table>
                                       
                            </div>
                            <br />
                            <br />
            
                        </td>
                        <td width="20">
                            <div class="spacer10x20" />
                        </td>
                    </tr>
                </table>
                <br>
                <br>
            </td>
        </tr>
    </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>