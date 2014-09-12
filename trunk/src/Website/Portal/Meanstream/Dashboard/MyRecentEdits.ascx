<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyRecentEdits.ascx.vb" Inherits="Meanstream_Dashboard_MyRecentEdits" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><span class="header">MY RECENT EDITS</span></td>
    <!--<td><div align="right"><a href="#.htm">view more</a></div></td>-->
  </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="status"></asp:Label>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="Grid1" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid1">
                            <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="type" />
                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="title" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="location" />
                            <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="published" />
                            <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="edit date" />
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <Meanstream:GridItemView ID="itemdata" runat=server>
                            <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns1" runat="server">
                                <table width="100%"><tr><td width="5%"><a class='icon-icon-pagesm' href="./Pages/Edit.aspx?VersionID=<%# Eval("PageVersionId")%>"></a></td><td width="5%">&nbsp;&nbsp;&nbsp;</td><td width="90%" nowrap><a href="./Pages/Edit.aspx?VersionID=<%# Eval("PageVersionId")%>"><%#Eval("FriendlyName")%></a></td></tr></table>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns2" runat=server>
                                <asp:Label ID="lblModuleTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" IsLast=true runat=server>
                                <a href="./Pages/Default.aspx?ctl=ViewPage&PageID=<%# Eval("PageId")%>">/<%#Eval("Url")%></a>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns4" runat=server>
                                <%#GetFlag(Eval("IsPublished"))%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns6" IsLast=true runat=server>
                                <asp:Label ID="lblLastUpdateDate" runat="server" Text='<%# Eval("LastModifiedDate") %>'></asp:Label>
                            </Meanstream:GridItemColumns>
                        </Meanstream:GridItemView>
                    </ItemTemplate>
                </asp:TemplateField>              
            </Columns>
        </Meanstream:Grid>
     </Meanstream:GridContainer>

    </ContentTemplate>
</asp:UpdatePanel>