<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RecycleBinWidgets.ascx.vb" Inherits="Meanstream_Pages_UserControls_RecycleBinWidgets" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div align="right">
            <asp:LinkButton ID="btnDeleteAll" runat="server" Visible="false">click here to delete all</asp:LinkButton>
        </div>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="status"></asp:Label>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="WidgetsGrid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="WidgetsGrid">
                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="title"  />
                            <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="type" />
                            <Meanstream:ColumnHeader ColumnWidth="5%" HeaderText="shared" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="author" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="deleted date" />          
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <Meanstream:GridItemView ID="itemdata" runat=server>
                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns1" runat="server">
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                <b><%# Eval("Title")%></b>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns2" runat=server>
                                <%#GetModuleType(Eval("ModuleDefId"))%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="5%" ID="GridItemColumns3" runat=server>
                                <%# IsGlobal(Eval("AllPages"))%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns4" runat=server>
                                <%#Eval("CreatedBy")%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns5" runat=server>
                                <%#Eval("DeletedDate")%>
                            </Meanstream:GridItemColumns>
                        </Meanstream:GridItemView>
                    </ItemTemplate>
                </asp:TemplateField>   
                <Meanstream:ButtonField ButtonMode="None" ButtonType="Link" ItemStyle-VerticalAlign="Middle" Text="Restore" CommandName="Restore" />
                <Meanstream:ButtonField ButtonMode="Delete" ButtonType="Image"  CommandName="Delete" OnClientClick="javascript:if (!confirm('The widget will be deleted permanently.')) return;"></Meanstream:ButtonField>
            </Columns>
        </Meanstream:Grid>
        <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="WidgetsGrid" Width="100%">
            <Fields>
                <Meanstream:PreviousPagerField />
                <Meanstream:NumericPagerField />
                <Meanstream:NextPagerField />
                <Meanstream:TemplatePagerField>
                    <PagerTemplate>
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
    </ContentTemplate>
</asp:UpdatePanel>
