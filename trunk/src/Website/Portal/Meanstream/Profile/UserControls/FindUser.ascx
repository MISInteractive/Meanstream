<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FindUser.ascx.vb" Inherits="Meanstream_Profile_UserControls_FindUser" %>
<table>
        <tr>
            <td width="100%" colspan="2">
                
                <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                <table>
                    <tr>
                        <td width="250px">
                            <asp:TextBox ID="txtUsername" runat="server" SkinID="Text" Width="250"></asp:TextBox>
                            
                        </td>
                        <td width="550px">
                            <asp:ImageButton ID="btnSearch" runat="server" Text="Search"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="litUsername" runat="server" Value="Find User" />
                                    <Meanstream:GridContainer ID="Container" runat="server" Width="740px">
                                        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                                        <Meanstream:Grid ID="UsersGrid" AllowSorting="true" runat="server" AllowPaging="true"
                                            ShowFooter="false" Width="740px" GridMode="Standard">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="UsersGrid">
                                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="" />
                                                            <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="username" IsSortable="true" SortExpression="Username" />
                                                            <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="email" />
                                                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="is locked out" />
                                                        </Meanstream:GridHeader>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <Meanstream:GridItemView ID="itemdata" runat="server">
                                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns4" runat="server">
                                                                <asp:LinkButton ID="btnSelectUsername" runat="server" Text="Select" CommandArgument=<%#Eval("Username")%> CommandName="Select"></asp:LinkButton> 
                                                            </Meanstream:GridItemColumns>
                                                            <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns1" runat="server">
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("UserName") %>' Visible="false"></asp:Label>
                                                                <%# Eval("UserName") %>
                                                            </Meanstream:GridItemColumns>
                                                            <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns2" runat="server">
                                                                <%#Eval("Email")%>
                                                            </Meanstream:GridItemColumns>
                                                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" IsLast="true"
                                                                runat="server">
                                                                <b>
                                                                    <%#Eval("IsLockedOut")%></b>
                                                            </Meanstream:GridItemColumns>
                                                        </Meanstream:GridItemView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </Meanstream:Grid>
                                        <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="UsersGrid"
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        
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