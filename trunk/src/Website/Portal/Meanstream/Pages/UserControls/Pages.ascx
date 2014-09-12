<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Pages.ascx.vb" Inherits="Meanstream_Pages_UserControls_Pages" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/TreeView.ascx" TagName="TreeView" TagPrefix="Meanstream" %>
<script language="javascript" type="text/javascript">
    function createRedirectToEdit() {
        var clientid = '<%= btnCreatePage.ClientID %>';
        var control = 'litVersionId';
        var win = new msWindowObject(clientid);
        win.control = control;
        var versionId = win.ControlValue();
        msCloseWin();
        if (versionId.value != '') {
            window.location.href = './Edit.aspx?VersionID=' + versionId.value;
        }
    }
</script>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
<script>

    $().ready(function () {
        
        var model=Meanstream.UI.Models.Page.findAll(bindGrid);
       

    });
    function bindGrid(model) {
        var $pageGrid = $('#pageGrid');
        $pageGrid.kendoGrid({
            dataSource: {
                data: model,
                pageSize: 10,

                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Id: { type: "string" },
                            Path: { type: "string" },
                            Skin: { type: "string" },
                            VisibleFlag: { type: "string" },
                            Author: { type: "string" },
                            PublishedFlag: { type: "string" },
                            PublishedDate: { type: "string" }
                        }
                    }
                }
            },

            pageable: true,
            scrollable: false,
            sortable: true,
            //note template syntax for a field: #=field#  
            columns: [{
                
                width: "30%",
                title: "path",
                template: "<div style='float:left;margin-right:10px;'><a class='icon-icon-pagesm' href='Default.aspx?ctl=ViewPage&PageID=#=Id#'></a></div><div><a href='Default.aspx?ctl=ViewPage&PageID=#=Id#'>#=Path#</a></div>"

            }, {
                field: "Skin",
                width: "20%",
                title: "skin"
            }, {
                field: "VisibleFlag",
                width: "10%",
                title: "display in menu"
            },
            {
               
                width: "15%",
                title: "author",
                template: "<a href='../Profile/Default.aspx?ctl=Profile&Username=#=Author#'>#=Author#</a>"
            },
            {
                field: "PublishedFlag",
                width: "10%",
                title: "published"
            },
             {
                 field: "PublishedDate",
                 width: "15%",
                 title: "published date"
             }
           ]
        });
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
                    <td nowrap><span class="largelink">Manage Pages</span></td>
                    <td><div class="spacer10x20" /></td>
                    <td><a><div class="icon-help"></div></a></td>
                  </tr>
                </table>                  </td>
                <td><div align="right">
                  <table border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                      <td width="12"><div class="icon-addpage"></div></td>
                      <td width="6"><div class="spacer10x6" /></td>
                      <td nowrap class="subnav">
                            <asp:LinkButton ID="CreatePageTarget" runat="server">create a page</asp:LinkButton>
                            <Meanstream:Window ID="btnCreatePage" runat="server" 
                                SkinID="Window" 
                                ShowUrl="true" 
                                ShowLoader="true"
                                Width="700" 
                                Height="625" 
                                Title="Create Page"
                                OnClientClose="createRedirectToEdit()" 
                                NavigateUrl="Module.aspx?ctl=PageSettings&Action=Add" >
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
            <div id="pageGrid" style="width:100%;outline:none;"></div>
           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <script language="javascript" type="text/javascript">
                        function sortGrid() {
                            javascript: __doPostBack('Grid1', 'Sort$TabName');
                        }
                    </script>
                    <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                    <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                    <Meanstream:Grid ID="Grid1" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid1">
                                        <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="page" IsSortable="true" SortExpression="Name" />
                                        <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="skin" IsSortable="true" SortExpression="SkinId" />
                                        <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="display in menu" />
                                        <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="author" IsSortable="true" SortExpression="Author" />
                                        <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="published" />
                                        <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="published date" />
                                    </Meanstream:GridHeader>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <Meanstream:GridItemView ID="itemdata" runat=server>
                                        <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns1" runat="server">
                                            <table width="100%"><tr><td width="5%"><a class='icon-icon-pagesm' href="Default.aspx?ctl=ViewPage&PageID=<%# Eval("Id")%>"></a></td><td width="5%">&nbsp;&nbsp;&nbsp;</td><td width="90%" nowrap><a href="Default.aspx?ctl=ViewPage&PageID=<%# Eval("Id")%>"><%# GetPath(Eval("Id"))%></a></td></tr></table>
                                        </Meanstream:GridItemColumns>
                                        <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns2" runat=server>
                                            <asp:Label ID="lblSkinName" runat="server" Text='<%# GetSkinName(Eval("SkinId")) %>'></asp:Label>
                                        </Meanstream:GridItemColumns>
                                        <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" IsLast=true runat=server>
                                            <asp:Label ID="lblDisplayMenu" runat="server" Text='<%# GetFlag(Eval("IsVisible")) %>'></asp:Label>
                                        </Meanstream:GridItemColumns>
                                        <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns4" IsLast=true runat=server>
                                            <a href="../Profile/Default.aspx?ctl=Profile&Username=<%# Eval("Author") %>"><%# Eval("Author") %></a>
                                        </Meanstream:GridItemColumns>
                                        <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns5" IsLast=true runat=server>
                                            <asp:Label ID="lblPublished" runat="server" Text='<%# GetFlag(Eval("IsPublished")) %>'></asp:Label>
                                        </Meanstream:GridItemColumns>
                                        <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns6" IsLast=true runat=server>
                                            <asp:Label ID="lblPublishedDate" runat="server" Text='<%# Eval("PublishedDate") %>'></asp:Label>
                                        </Meanstream:GridItemColumns>
                                    </Meanstream:GridItemView>
                                </ItemTemplate>
                            </asp:TemplateField>              
                        </Columns>
                    </Meanstream:Grid>
                    <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="Grid1" Width="100%">
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