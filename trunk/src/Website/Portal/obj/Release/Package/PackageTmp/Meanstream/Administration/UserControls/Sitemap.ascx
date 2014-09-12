<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Sitemap.ascx.vb" Inherits="Meanstream_Administration_UserControls_Sitemap" %>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
   <script>
       
       $().ready(function () {

           var model = Meanstream.UI.Models.Sitemap.findAll(bindGrid);


       });
       function bindGrid(model) {
           var $pageGrid = $('#Grid');
           $pageGrid.kendoGrid({
               dataSource: {
                   data: model,
                   pageSize: 10,

                   schema: {
                       model: {
                           id: "Id",
                           fields: {
                               Id: { type: "string", nullable: true },
                               Name: { type: "string" },
                               RootProvider: { type: "string" },
                               Enabled: { type: "string" }
                              

                           }
                       }
                   }
               },

               pageable: true,
               scrollable: false,
               sortable: true,
              
               batch: true,

               columns: [ {
                   field: "Name",
                   width: "20%",
                   title: "name"
               },

            {
                field: "RootProvider",
                width: "70%",
                title: "type"
            },
             {
                 field: "Enabled",
                 width: "10%",
                 title: "enabled"
             }

           ]
           });
       }
       
</script>
<table valign="top">
    <tr>
        <td>
            Enabled:
        </td>
        <td>
            <asp:Checkbox ID="SitemapEnabled" runat="server" Enabled="false" /> 
        </td>
    </tr>
    <tr>
        <td><br/>
            Sitemap URL:
        </td>
        <td><br/>
            <asp:HyperLink ID="SiteMapURL" runat="server" Target="_blank" /> 
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="left" colspan="2">
            <br/><br/>
            Sitemap Providers<br>
            <div id="Grid" style="width:100%;outline:none;"></div>
            <%--<Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                <Meanstream:Grid ID="Grid" runat="server" AllowPaging="false"
                    ShowFooter="false" Width="100%" GridMode="Standard">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid">
                                    <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="name" />
                                    <Meanstream:ColumnHeader ColumnWidth="70%" HeaderText="type" />
                                    <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="enabled" />
                                </Meanstream:GridHeader>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <Meanstream:GridItemView ID="itemdata" runat="server">
                                    <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns1" runat="server">
                                        <b><%# Eval("Name")%></b>
                                    </Meanstream:GridItemColumns>
                                    <Meanstream:GridItemColumns ColumnWidth="70%" ID="GridItemColumns2" runat="server">
                                        <%# Eval("RootProvider")%>
                                    </Meanstream:GridItemColumns>
                                    <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" runat="server">
                                        <%# IIf(Eval("RootProvider").ToString = System.Web.SiteMap.Provider.GetType.FullName, True, False)%>
                                    </Meanstream:GridItemColumns>
                                </Meanstream:GridItemView>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </Meanstream:Grid>
            </Meanstream:GridContainer>--%>
        </td>
    </tr>
</table>