<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Versions.ascx.vb" Inherits="Meanstream_Pages_UserControls_Versions" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="PagesGrid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="PagesGrid">
                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="page" IsSortable="true" SortExpression="Name" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="skin" />
                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="file name" />
                            <Meanstream:ColumnHeader ColumnWidth="7%" HeaderText="display" />
                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="Author" />
                            <Meanstream:ColumnHeader ColumnWidth="9%" HeaderText="published" />
                            <Meanstream:ColumnHeader ColumnWidth="14%" HeaderText="currently published" />           
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <Meanstream:GridItemView ID="itemdata" runat=server>
                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns1" runat="server">
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                <table width="100%"><tr><td width="5%"><div class='icon-icon-pagesm' /></td><td width="5%">&nbsp;&nbsp;&nbsp;</td><td width="90%" nowrap><%# Eval("Name")%></td></tr></table>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns2" runat=server>
                                <asp:Label ID="lblSkinName" runat="server" Text='<%# GetSkinName(Eval("SkinId")) %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns3" runat=server>
                                <asp:Label ID="lblUrl" runat="server" Text='<%# Eval("Url") %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="7%" ID="GridItemColumns4" runat=server>
                                <asp:Label ID="lblIsVisible" runat="server" Text='<%# Eval("IsVisible") %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns5" runat=server>
                                <asp:Label ID="lblAuthor" runat="server" Text='<%# Eval("Author") %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="9%" ID="GridItemColumns7" runat=server>
                                <asp:Label ID="lblIsPublished" runat="server" Text='<%# GetFlag(Eval("IsPublished")) %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="14%" ID="GridItemColumns8" runat=server>
                                <asp:Label ID="lblIsPublishedVersion" runat="server" Text='<%# GetFlag(Eval("IsPublishedVersion")) %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                        </Meanstream:GridItemView>
                    </ItemTemplate>
                </asp:TemplateField>   
                <Meanstream:HyperLinkImageField ImageMode="Preview" Target="_blank" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/preview/page/{0}" />
                <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="../Edit.aspx?VersionID={0}" />
                <Meanstream:ButtonField ButtonMode="Page_Delete" ButtonType="Image"  CommandName="Delete" OnClientClick="javascript:if (!confirm('The page version will be deleted permanently.')) return;"></Meanstream:ButtonField>
            </Columns>
        </Meanstream:Grid>
        <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="PagesGrid" Width="100%" Visible="true">
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