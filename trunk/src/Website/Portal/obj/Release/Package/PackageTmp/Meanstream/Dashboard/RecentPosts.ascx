<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RecentPosts.ascx.vb" Inherits="Meanstream_Dashboard_RecentPosts" %>
<script type="text/javascript">
    function windowClose() {
        window.location.href = './Default.aspx';
    }
</script> 
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><span class="header">RECENT POSTS</span></td>
    <td><div align="right"><a href="/wordpress/wp-admin/edit.php">view more</a></div></td>
  </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="Grid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid">
                            <Meanstream:ColumnHeader ColumnWidth="35%" HeaderText="title" />
                            <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="status" />
                            <Meanstream:ColumnHeader ColumnWidth="35%" HeaderText="link" />
                            <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="last modified" />
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                    
                        <Meanstream:GridItemView ID="itemdata" runat=server>    
                            <Meanstream:GridItemColumns ColumnWidth="35%" ID="GridItemColumns1" runat="server">
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                <table width='100%'><tr><td width='5%'><td width='90%' nowrap>
                                    <%# "<a href='/wordpress/wp-admin/post.php?post=" & Eval("id") & "&action=edit'>" & Eval("post_title") & "</a>"%>
                                </td></tr></table>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns3" IsLast=false runat=server>
                                <%# Eval("post_status")%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="35%" ID="GridItemColumns2" IsLast=false runat=server>
                                <%# "<a href='" & Eval("guid") & "' target='_blank'>" & Eval("guid") & "</a>"%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns6" IsLast=true runat=server>
                                <%# Eval("post_modified_gmt")%>
                            </Meanstream:GridItemColumns>   
                        </Meanstream:GridItemView>
                        
                    </ItemTemplate>
                </asp:TemplateField>  
            </Columns>
        </Meanstream:Grid>
     </Meanstream:GridContainer>
    </ContentTemplate>
</asp:UpdatePanel>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
    <td><div class="spacer20x20" /></td>
    </tr>
</table>