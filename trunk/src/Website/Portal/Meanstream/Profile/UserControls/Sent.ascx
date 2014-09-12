<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Sent.ascx.vb" Inherits="Meanstream_Profile_UserControls_Sent" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<Meanstream:GridContainer ID="Container" runat="server" Width="100%">
    <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
    <Meanstream:Grid ID="Messages" AllowSorting="true" runat="server" AllowPaging="true"
        ShowFooter="false" Width="100%" GridMode="Standard">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <Meanstream:GridHeader ID="Header1" runat="server" GridId="Messages">
                        <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="" />
                        <Meanstream:ColumnHeader ColumnWidth="60%" HeaderText="" />
                        <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="" />
                    </Meanstream:GridHeader>
                </HeaderTemplate>
                <ItemTemplate>
                    <Meanstream:GridItemView ID="itemdata" runat="server">
                        <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns1" runat="server">
                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblSentFrom" runat="server" Text='<%# Eval("SentFrom") %>' Visible="false"></asp:Label>
                            <table>
                                    <tr>
                                        <td align="center"><br />
                                            <a href='../Profile/Default.aspx?ctl=Profile&UID=<%#Eval("SentTo")%>'>
                                            <asp:Image ID="ImgPortrait" BorderWidth="0" runat="server" ImageUrl='<%# GetProfileImage(Eval("SentTo"))%>' Height="50" /> 
                                            </a><br/>
                                                    
                                            <%# "<a href='../Profile/Default.aspx?ctl=Profile&UID=" & Eval("SentTo").ToString() & "'>" & GetUsername(Eval("SentTo")) & "</a>"%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </Template>
                        </Meanstream:GridItemColumns>
                        <Meanstream:GridItemColumns ColumnWidth="40%" ID="GridItemColumns2" runat="server">
                            <table width="400">
                                <tr>
                                    <td style="border: 0;">
                                        <B>Subject:</B> <%#Eval("Subject")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 0;">
                                        <B>Message:</B> <asp:Literal ID="litBody" runat="server" Text='<%#GetBody(Eval("Body"))%>'></asp:Literal> <asp:LinkButton ID="btnBody" runat="server" CommandName="Body" CommandArgument='<%# Eval("Id")%>'>Read More</asp:LinkButton>        
                                    </td>
                                </tr>
                            </table>
                        </Meanstream:GridItemColumns>
                        <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns3" runat="server">
                                <B>Sent:</B> <%#Format(Eval("DateSent"), "MM/dd/yyyy")%>
                        </Meanstream:GridItemColumns>
                                
                    </Meanstream:GridItemView>
                            
                </ItemTemplate>
                        
            </asp:TemplateField>
            <Meanstream:ButtonField ButtonMode="Delete" ButtonType="Image" ItemStyle-VerticalAlign="Middle" CommandName="Delete" OnClientClick="javascript:if (!confirm('The message will be deleted permanently.')) return;"></Meanstream:ButtonField>
        </Columns>
    </Meanstream:Grid>
    <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="Messages"
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