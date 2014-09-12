<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Roles.ascx.vb" Inherits="Meanstream_Administration_UserControls_Roles" %>
<script type="text/javascript">
    function windowClose() {
        window.location.href = './Default.aspx?ctl=Roles';
    }
    function goToUsers() {
        var clientid = '<%= btnCreateUser.ClientID %>';
        var control = 'litUserName';
        var win = new msWindowObject(clientid);
        win.control = control;
        var username = win.ControlValue();
        msCloseWin();
        if (username.value != '') {
            window.location.href = './Default.aspx?ctl=Users';
        }
    }
</script>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
   <script>
       var winId = '<%= Window1.ClientID %>';
       $().ready(function () {

           var model = Meanstream.UI.Models.Role.findAll(bindGrid);


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
                               Description: { type: "string" },
                               AutoAssignment: { type: "string" },
                               IsPublic: { type: "string" }

                           }
                       }
                   }
               },

               pageable: true,
               scrollable: false,
               sortable: true,
               editable: {
                   destroy: "true",
                   confirmation: "Are you sure you want to remove this role?"
               },
               batch: true,

               columns: [{

                   width: "20%",
                   title: "role",
                   template: "<span class='rolename' style='display:none;'>#=Name#</span><span style='cursor:pointer;font-weight:bold;text-decoration:underline;' onclick='callWin(\"Module.aspx?ctl=ManageRole&RoleID=#=Id#\")'>#=Name#</span>"

               }, {
                   field: "Description",
                   width: "50%",
                   title: "description"
               },

            {
                field: "AutoAssignment",
                width: "10%",
                title: "auto assignment"
            },
             {
                 field: "IsPublic",
                 width: "10%",
                 title: "public"
             },
             {
                 command: "destroy",
                 width: "10%",
                 title: "  "
             }

           ],
               dataBound: function (e) {
                   this.element.find("a").each(function () {
                       var role = $(this).closest("tr").find('.rolename').text();

                       if ((role == 'Host') || (role == 'Administrator') || (role == 'Registered Users') || (role == 'Content Administrator') || (role == 'All Users') || (role == 'Security Administrator')) {
                           $(this).hide();
                       }

                   });

               },
               remove: function (e) {
                   var role = e.model.data.Name;
                  
                   Meanstream.UI.Models.Role.destroy(role);


               }
           });
       }
       function callWin(url) {
           var w = 900;
           var h = 500;
           Sys.require([Sys.scripts.msWindow], function () {
              
               var win = new msWindowObject(winId);
               //var h0 = ($(window).height() - 40);
               win.height = h;
               win.width = w;
               win.url = url;

               win.open();

           });


       }
</script>
<Meanstream:Window ID="Window1" runat=server Height="900" Width="500" Title="Manage Role" NavigateUrl="/" SkinID="Window" ShowLoader="true"  ShowUrl="true"   /> 
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
  <td valign="top"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Security Roles</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Security Roles</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                        OnClientClose="goToUsers()">
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
     <td><div class="spacer10x10" /></td>
   </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
   <tr>
     <td><div class="spacer10x10" /></td>
   </tr>
</table>
<div id="Grid" style="width:100%;outline:none;"></div>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="RoleGrid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="RoleGrid">
                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="role" />
                            <Meanstream:ColumnHeader ColumnWidth="50%" HeaderText="description" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="auto assignment" />
                            <Meanstream:ColumnHeader ColumnWidth="10%" HeaderText="public" />
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                    
                        <Meanstream:GridItemView ID="itemdata" runat=server>    
                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns1" runat="server">
                                <asp:Label ID="RoleName" runat="server" Text='<%# Eval("Name") %>' Visible="false"></asp:Label>
                                <table width='100%'><tr><td width='5%'><div class='icon-role' /></td><td width='5%'>&nbsp;&nbsp;&nbsp;</td><td width='90%' nowrap>
                                    <asp:PlaceHolder ID="phRole" runat="server" EnableViewState="true"></asp:PlaceHolder>
                                </td></tr></table>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="50%" ID="GridItemColumns2" runat=server>
                                <%#Eval("Description")%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns3" runat=server>
                                <%#GetFlag(Eval("AutoAssignment"))%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="10%" ID="GridItemColumns4" IsLast=true runat=server>
                                <%#GetFlag(Eval("IsPublic"))%>
                            </Meanstream:GridItemColumns>
                        </Meanstream:GridItemView>
                        
                    </ItemTemplate>
                </asp:TemplateField>  
                <Meanstream:ButtonField  ButtonMode="Delete" ButtonType="Image" CommandName="Delete" OnClientClick="javascript:if (!confirm('The role will be deleted permanently.')) return;"></Meanstream:ButtonField> 
            </Columns>
        </Meanstream:Grid>
        <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="RoleGrid" Width="100%">
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