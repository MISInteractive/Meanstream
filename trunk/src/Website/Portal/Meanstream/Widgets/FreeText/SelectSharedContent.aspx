<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectSharedContent.aspx.vb" MasterPageFile="~/Meanstream/UI/Skins/Module.master" Inherits="Meanstream_Widgets_FreeText_SelectSharedContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CenterPane" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table>
        <tr>
            <td width="100%" colspan="2">
               
                <table>
                    <tr>
                        <td width="30px">
                            <b>Select Type:</b>
                        </td>
                        <td>
                            <Meanstream:ComboBox ID="ddlContentType" runat="server" Width="275" ComboPanelHeight="140" ImageButtonWidth="25" AutoPostBack="true"></Meanstream:ComboBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><br>
                                    <asp:HiddenField ID="litContent" runat="server" Value="" />
                                    <Meanstream:GridContainer ID="Container" runat="server" Width="740px">
                                        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                                        <Meanstream:Grid ID="ContentGrid" AllowSorting="true" runat="server" AllowPaging="true"
                                            ShowFooter="false" Width="740px" GridMode="Standard">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="ContentGrid">
                                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="" />
                                                            <Meanstream:ColumnHeader ColumnWidth="40%" HeaderText="title" />
                                                            <Meanstream:ColumnHeader ColumnWidth="35%" HeaderText="author" />
                                                            <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="last updated" />
                                                        </Meanstream:GridHeader>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <Meanstream:GridItemView ID="itemdata" runat="server">
                                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns4" runat="server">
                                                                <asp:LinkButton ID="btnSelect" runat="server" Text="Select" CommandArgument=<%#Eval("Id")%> CommandName="Select"></asp:LinkButton> 
                                                            </Meanstream:GridItemColumns>
                                                            <Meanstream:GridItemColumns ColumnWidth="40%" ID="GridItemColumns1" runat="server">
                                                                <%#Eval("Title")%>
                                                            </Meanstream:GridItemColumns>
                                                            <Meanstream:GridItemColumns ColumnWidth="35%" ID="GridItemColumns2" runat="server">
                                                                <%#Eval("Author")%>
                                                            </Meanstream:GridItemColumns>
                                                            <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns3" IsLast="true" runat="server">
                                                                <b>
                                                                    <%#Format(Eval("LastUpdatedDate"), "MM/dd/yyyy")%></b>
                                                            </Meanstream:GridItemColumns>
                                                        </Meanstream:GridItemView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </Meanstream:Grid>
                                        <Meanstream:DataPager ID="pager" runat="server" PageSize="6" PagedControlID="ContentGrid" Width="100%">
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
                    <tr>
                        <td colspan="2" align="right">
                            <br>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>