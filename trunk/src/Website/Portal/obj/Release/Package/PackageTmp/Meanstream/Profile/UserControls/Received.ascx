<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Received.ascx.vb" Inherits="Meanstream_Profile_UserControls_Received" %>
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
                        <Meanstream:ColumnHeader ColumnWidth="40%" HeaderText="" />
                        <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="" />
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
                                        <td align="center" style="border: 0;"><br>
                                            <a href="../Profile/Default.aspx?ctl=Profile&UID=<%#Eval("SentFrom")%>">
                                            <asp:Image ID="ImgPortrait" BorderWidth="0" runat="server" ImageUrl=<%#GetProfileImage(Eval("SentFrom"))%> Height="50" /> 
                                            </a><br><a href="../Profile/Default.aspx?ctl=Profile&UID=<%#Eval("SentFrom")%>"><%#GetUsername(Eval("SentFrom"))%></a>
                                                    
                                        </td>
                                    </tr>
                                </table>
                                <br>
                            </Template>
                        </Meanstream:GridItemColumns>
                        <Meanstream:GridItemColumns ColumnWidth="40%" ID="GridItemColumns2" runat="server">
                                   
                                <table>
                                    <tr>
                                        <td style="border: 0;">
                                            <B>Subject:</B> <%#Eval("Subject")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="style="border: 0;">
                                            <B>Message:</B> <asp:Literal ID="litBody" runat="server" Text=<%#GetBody(Eval("Body"))%>></asp:Literal> <asp:LinkButton ID="btnBody" runat="server" CommandName="Body" CommandArgument='<%# Eval("Id")%>'>Read More</asp:LinkButton>        
                                        </td>
                                    </tr>
                                    <tr id="Reply" runat="server" visible='False'>
                                        <td>
                                            <B><asp:Literal ID="litReply" runat="server" Text="Reply" Visible="false"></asp:Literal></B>
                                            <br>
                                                <asp:TextBox ID="txtComment" runat="server" Visible="false" TextMode="MultiLine" Rows="9" Width="250" BorderColor="Black" BorderWidth="1"></asp:TextBox>
                                            <br>
                                                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" Font-Bold="true" ForeColor="red"></asp:Label>
                                            <br>
                                            <div align="left">
                                                <asp:LinkButton ID="btnSend" runat="server" Text="Send" Visible="false" CommandName="Send" CommandArgument='<%# Eval("Id")%>' />
                                            </div>
                                            <br />
                                        </td>
                                    </tr>
                                </table>

                        </Meanstream:GridItemColumns>
                        <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns3" runat="server">
                                <B>Sent:</B> <%#Format(Eval("DateSent"), "MM/dd/yyyy")%>
                        </Meanstream:GridItemColumns>
                        <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns4" IsLast="true" runat="server">
                                <B>Status:</B> <%#Status(Eval("Opened"))%>
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