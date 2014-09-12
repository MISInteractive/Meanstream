<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Users.ascx.vb" Inherits="Meanstream_Administration_UserControls_Users" %>
<script type="text/javascript">
    function windowClose() {
        window.location.href = './Default.aspx?ctl=Users';
    }
    function goToRoles() {
        var clientid = '<%= btnCreateRole.ClientID %>';
        var control = 'litRoleName';
        var win = new msWindowObject(clientid);
        win.control = control;
        var username = win.ControlValue();
        msCloseWin();
        if (username.value != '') {
            window.location.href = './Default.aspx?ctl=Roles';
        }
    }
</script> 
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
   <script>
       var winId = '<%= Window1.ClientID %>';
       var txtSearch = '<%= txtSearch.ClientID %>';
       var model;
       $().ready(function () {

           model = Meanstream.UI.Models.User.findAll(bindGrid);


       });
       function bindGrid(model) {
           var $pageGrid = $('#Grid');
           $pageGrid.kendoGrid({
               dataSource: {
                   data: model,
                   pageSize: 10,

                   schema: {
                       model: {
                           id: "UserId",
                           fields: {
                               UserId: { type: "string", nullable: true },
                               UserName: { type: "string" },
                               Email: { type: "string" },
                               IsLockedOut: { type: "string" },
                               LastLoginDate: { type: "string" },
                               LastActivityDate: { type: "string" },
                               CreateDate: { type: "string" }
                           }
                       }
                   }
               },

               pageable: true,
               scrollable: false,
               sortable: true,
               editable: {
                   destroy: "true",
                   confirmation: "Are you sure you want to remove this user?"
               },
               batch: false,

               columns: [{

                   width: "30%",
                   title: "username",
                   template: "<span class='username' style='display:none;'>#=UserName#</span><span style='cursor:pointer;font-weight:bold;text-decoration:underline;' onclick='callWin(\"Module.aspx?ctl=ManageUser&uid=#=UserId#\")'>#=UserName#</span>"

               }, {
                   field: "Email",
                   width: "30%",
                   title: "email"
               },

            {
                field: "IsLockedOut",
                width: "10%",
                title: "locked"
            },
             {
                 field: "LastLoginDate",
                 width: "10%",
                 title: "last activity date"
             },
             {
                 field: "CreateDate",
                 width: "10%",
                 title: "created date"
             },
             {
                 command: "destroy",
                 width: "10%",
                 title: "  "
             }

           ],
               dataBound: function (e) {
                   this.element.find("a").each(function () {
                       var u = $(this).closest("tr").find('.username').text();

                       if ((u == 'host') || (u == 'admin')) {
                           $(this).hide();
                       }

                   });

               },
               remove: function (e) {
                   var u = e.model.data.UserName;
                   //alert(u);
                   Meanstream.UI.Models.User.destroy(u);


               }
           });
       }
       function callWin(url) {
           var w = 900;
           var h = 800;
           Sys.require([Sys.scripts.msWindow], function () {

               var win = new msWindowObject(winId);
               var h0 = ($(window).height() - 100);
               win.height = h0;
               win.width = w;
               win.url = url;

               win.open();

           });


       }
       function search() {
           var search = $('#' + txtSearch).val();
           var $pageGrid = $('#Grid');
           //$pageGrid.removeData('kendoGrid');
           //$pageGrid.empty();
           $pageGrid.remove();
           var grid = $('<div id="Grid" style="width:100%;outline:none;"></div>');
           $('#gridContainer').append(grid);
           model = Meanstream.UI.Models.User.findLike(bindGrid,search);
           
       }
</script>
<Meanstream:Window ID="Window1" runat=server Height="900" Width="500" Title="Manage User" NavigateUrl="/" SkinID="Window" ShowLoader="true"  ShowUrl="true"   /> 
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
  <td valign="top"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">User Accounts</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>User Accounts</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table></td>
      <td><div align="right">
        <table border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            <td width="12"><div class="create-user"></div></td>
            <td width="6"><div class="spacer10x6" /></td>
            <td nowrap class="subnav">
                <asp:LinkButton ID="CreateUserTarget" runat="server">create a user</asp:LinkButton>
                    <Meanstream:Window ID="btnCreateUser" runat="server" 
                        SkinID="Window" 
                        Width="810" 
                        Height="460" 
                        ShowLoader="true" 
                        ShowUrl="true" 
                        Title="Create User" 
                        NavigateUrl="Module.aspx?ctl=CreateUser" 
                        OnClientClose="windowClose()">
                    </Meanstream:Window>
            </td>
            <td nowrap class="subnav"><div class="spacer25x10" /></td>
            <td nowrap class="subnav"><div class="create-role"></div></td>
            <td nowrap class="subnav"><div class="spacer10x6" /></td>
            <td nowrap class="subnav">
                <asp:LinkButton ID="CreateRoleTarget" runat="server">create a role</asp:LinkButton>
                    <Meanstream:Window ID="btnCreateRole" runat="server" 
                        SkinID="Window" 
                        Width="450" 
                        Height="370" 
                        ShowLoader="true" 
                        ShowUrl="true" 
                        Title="Create Role" 
                        NavigateUrl="Module.aspx?ctl=CreateRole" 
                        OnClientClose="goToRoles()">
                    </Meanstream:Window>
            </td>
            <td nowrap class="subnav"><div class="spacer25x10" /></td>
            <td nowrap class="subnav"><div class="icon-profile-export" /></td>
            <td nowrap class="subnav"><div class="spacer10x6" /></td>
            <td nowrap class="subnav"><a href="./ExportProfiles.aspx" target="_blank">profile export</a></td>
            <td nowrap class="subnav"><div class="spacer25x10" /></td>
            <td nowrap class="subnav"><div class="icon-email-export" /></td>
            <td nowrap class="subnav"><div class="spacer10x6" /></td>
            <td nowrap class="subnav"><a href="./ExportEmails.aspx" target="_blank">email export</a></td>

          </tr>
        </table>
    </div></td>
 </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
   <tr>
     <td><div class="spacer20x20" /></td>
   </tr>
</table>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap>Search for a User:</td>
            <td><div class="spacer10x20" /></td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" SkinID="Text" Width="225" ></asp:TextBox>
            </td>
            <td><div class="spacer10x20" /></td>
            <td><%--<asp:ImageButton ID="btnSearch" runat="server" />--%>
            <img src="/App_Themes/Meanstream.2011/images/button-search.gif" style="cursor:pointer;" onclick="search()">
            </td>
          </tr>
      </table></td>
      <td><div align="right"></div></td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
   <tr>
     <td><div class="spacer20x20" /></td>
   </tr>
</table>
<div id="gridContainer">
<div id="Grid" style="width:100%;outline:none;"></div>
</div>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="UsersGrid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="UsersGrid">
                            <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="username" />
                            <Meanstream:ColumnHeader ColumnWidth="30%" HeaderText="email" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="locked" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="last login date" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="last activity date" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="created date" />
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                    
                        <Meanstream:GridItemView ID="itemdata" runat=server>    
                            <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns1" runat="server">
                                <asp:Label ID="UserName" runat="server" Text='<%# Eval("UserName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="UserId" runat="server" Text='<%# Eval("UserId") %>' Visible="false"></asp:Label>
                                <table width='100%'><tr><td width='5%'><div class='icon-profile' /></td><td width='5%'>&nbsp;&nbsp;&nbsp;</td><td width='90%' nowrap>
                                    <asp:PlaceHolder ID="phUser" runat="server" EnableViewState="true"></asp:PlaceHolder>
                                </td></tr></table>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="30%" ID="GridItemColumns2" runat=server>
                                <%#Eval("Email")%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns6" runat=server>
                                <%#GetFlag(Eval("IsLockedOut"))%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" runat=server>
                                <%#Eval("LastLoginDate")%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns4" IsLast=true runat=server>
                                <%#Eval("LastActivityDate")%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns5" IsLast=true runat=server>
                                <%#Eval("CreateDate")%>
                            </Meanstream:GridItemColumns>
                        </Meanstream:GridItemView>
                        
                    </ItemTemplate>
                </asp:TemplateField>  
                <Meanstream:ButtonField  ButtonMode="Delete" ButtonType="Image" CommandName="Delete" OnClientClick="javascript:if (!confirm('The user will be deleted permanently.')) return;"></Meanstream:ButtonField> 
            </Columns>
        </Meanstream:Grid>
        <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="UsersGrid" Width="100%">
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