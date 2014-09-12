<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Skins.ascx.vb" Inherits="Meanstream_Pages_UserControls_Skins" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/TreeView.ascx" TagName="TreeView" TagPrefix="Meanstream" %>
<script type="text/javascript">
    function windowClose() {
        msCloseWin();
        window.location.href = './Default.aspx?ctl=Skins';
    }
</script> 
 <script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
   <script>
     
       $().ready(function () {

           var model = Meanstream.UI.Models.Skin.findAll(bindGrid);


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
                               SkinRoot: { type: "string" },
                               SkinSrc: { type: "string" },
                               Assigned: { type: "string" }
                               

                           }
                       }
                   }
               },

               pageable: true,
               scrollable: false,
               sortable: true,
               editable: {
                   destroy: "true",
                   confirmation: "Are you sure you want to remove this skin?"
               },
               batch: true,

               columns: [{

                   width: "33%",
                   title: "skin",
                   template: "#=SkinRoot#"

               }, {
                   field: "SkinSrc",
                   width: "23%",
                   title: "master page"
               },

            {
                field: "Assigned",
                width: "23%",
                title: "pages assigned"
            },
             {
                 
                 width: "10%",
                 title: " ",
                 template: "<input type=button class='k-button' value='Preview' onclick='preview(\"#= Id #\")'/>"
             },
             {
                 command: "destroy",
                 width: "10%",
                 title: "  "
             }

           ],
               remove: function (e) {
                   var Id = e.model.data.Id;

                   Meanstream.UI.Models.Skin.destroy(Id);


               }
           });
       }
       function preview(Id) {
           var url = '/preview/skin/' + Id;
        
           window.open(url);

       }
</script>          
<table width="100%" border="0" cellspacing="0" cellpadding="0">      
<tr>
  <td valign="top">
    <Meanstream:TreeView ID="Treeview" runat="server" />
  </td>
  <td width="20" valign="top"><div class="spacer10x20" /></td>
  <td width="75%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Manage Skins</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Manage Skins</b><br />Skins may be Deleted only if they are not assigned to existing <br />pages on the site. The physical Master page will not be deleted.', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
          </tr>
        </table>                  </td>
        <td><div align="right">
          <table border="0" align="right" cellpadding="0" cellspacing="0">
            <tr>
              <td width="12"><div class="icon-addpage"></div></td>
              <td width="6"><div class="spacer10x6" /></td>
              <td nowrap class="subnav">
                    <asp:LinkButton ID="CreateSkinTarget" runat="server">create a skin</asp:LinkButton>
                    <Meanstream:Window ID="btnCreateSkin" runat="server" 
                        SkinID="Window" 
                        Width="550" 
                        Height="225" 
                        ShowLoader="true" 
                        ShowUrl="true" 
                        Title="Create Skin" 
                        NavigateUrl="Module.aspx?ctl=CreateSkin" 
                        OnClientClose="windowClose()">
                    </Meanstream:Window>
              </td>
            </tr>
          </table>
        </div></td>
      </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><div class="spacer10x20" /></td>
      </tr>
    </table><br />
    <div id="Grid" style="width:100%;outline:none;"></div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="status"></asp:Label>
            <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
            <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
            <Meanstream:Grid ID="SkinGrid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <Meanstream:GridHeader ID="Header1" runat="server" GridId="SkinGrid">
                                <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="skin" IsSortable="true" SortExpression="SkinRoot" />
                                <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="master page" IsSortable="true" SortExpression="SkinSrc" />
                                <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="pages assigned" />
                            </Meanstream:GridHeader>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <Meanstream:GridItemView ID="itemdata" runat=server>
                                <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns1" runat="server">
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                    <table width="100%"><tr><td width="5%"><div class='icon-icon-pagesm' /></td><td width="5%">&nbsp;&nbsp;&nbsp;</td><td width="90%" nowrap><%#Eval("SkinRoot")%></td></tr></table>
                                </Meanstream:GridItemColumns>
                                <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns2" runat=server>
                                    <%#ParseFilePath(Eval("SkinSrc"))%>
                                </Meanstream:GridItemColumns>
                                <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns3" IsLast=true runat=server>
                                    <b><%# GetPageCount(Eval("Id").ToString)%></b> Pages using this Skin.
                                </Meanstream:GridItemColumns>
                            </Meanstream:GridItemView>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <Meanstream:HyperLinkImageField ImageMode="Preview" Target="_blank" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="../PreviewSkin.aspx?SkinID={0}" />
                    <Meanstream:ButtonField ButtonMode="Delete" ButtonType="Image" CommandName="Delete" OnClientClick="javascript:if (!confirm('The skin will be deleted permanently. The physical Master Page will not be deleted.')) return;"></Meanstream:ButtonField>
                </Columns>
            </Meanstream:Grid>
            <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="SkinGrid" Width="100%">
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
                                (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" /> records)
                            </div>
                        </PagerTemplate>
                    </Meanstream:TemplatePagerField>
                </Fields>
            </Meanstream:DataPager>
         </Meanstream:GridContainer>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    </td>
</tr>
</table>