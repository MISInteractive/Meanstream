<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ManageUsersInRole.ascx.vb" Inherits="Meanstream_Administration_UserControls_ManageUsersInRole" %>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
   <script>
       var roleId;
       var _util = new _scriptUtilities();
       roleId = _util.queryString('RoleID');

       $().ready(function () {
           
           var model = Meanstream.UI.Models.UserInRole.findAll(roleId, bindGrid);


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
                               IsAnonymous: { type: "string" },
                               LastActivityDate: { type: "string" }


                           }
                       }
                   }
               },

               pageable: true,
               scrollable: false,
               sortable: true,
               editable: {
                   destroy: "true",
                   confirmation: "Are you sure you want to remove this user from the role?"
               },
               batch: true,

               columns: [{

                   width: "60%",
                   title: "username",
                   template: "<span class='username' style='display:none;'>#=UserName#</span><a class='ulink' href='Module.aspx?ctl=ManageUser&uid=#=UserId#'>#=UserName#</a>"

               }, {
                   field: "IsAnonymous",
                   width: "15%",
                   title: "is anonymous"
               },

            {
                field: "LastActivityDate",
                width: "25%",
                title: "last activity date"
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
                       var cls = $(this).attr('class');
                       if(cls != 'ulink'){
                         if ((u == 'host') || (u == 'admin')) {
                             $(this).hide();
                         }
                       }
                   });

               },
               remove: function (e) {
                   var user = e.model.data.UserName;
                   Meanstream.UI.Models.UserInRole.destroy(roleId, user);


               }
           });
       }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<asp:Literal ID="lblRoleName" runat="server" Visible="false"></asp:Literal>
<table border="0" cellspacing="0" cellpadding="0" width="100%">             
    <tr>
        <td>
         <b>Available Users:</b>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="23" valign="middle" nowrap><table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                      <td width="12"><Meanstream:ComboBox ID="ddlAvailableUsers"  runat="server" Width="300" ComboPanelHeight="140" ImageButtonWidth="25" DefaultDisplayValue="0" DefaultDisplayText="Select User" /></td>
                      <td width="6"><div class="spacer10x6" /></td>
                      <td><Meanstream:BlockUIImageButton ID="btnAddUserToGroup" runat="server" StartMessage="Saving..." /></td>
                    </tr>
                </table></td>
              </tr>
            </table>   
            <br />
        </td>
    </tr>
    <tr>
        <td><br />
            <asp:CheckBox ID="chkSendNotification" runat="server" Checked="true" />
            <b>Send Notification?</b><br />
        </td>
    </tr>
    <tr>
       <td><br /> <br />
       <asp:Label ID="lblStatus" runat=server></asp:Label><br />
       <div id="Grid" style="width:100%;outline:none;"></div>
        <%--<Meanstream:GridContainer ID="Container" runat="server" Width="100%">
            <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
            <Meanstream:Grid ID="UsersInRoleGrid" AllowSorting="true" runat="server" AllowPaging="true"
                ShowFooter="false" Width="750px" GridMode="Standard">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <Meanstream:GridHeader ID="Header1" runat="server" GridId="UsersInRoleGrid">
                                <Meanstream:ColumnHeader ColumnWidth="60%" HeaderText="username" IsSortable="true"
                                    SortExpression="SkinRoot" />
                                <Meanstream:ColumnHeader ColumnWidth="15%" HeaderText="is anonymous" />
                                <Meanstream:ColumnHeader ColumnWidth="25%" HeaderText="last activity date" />
                            </Meanstream:GridHeader>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <Meanstream:GridItemView ID="itemdata" runat="server">
                                <Meanstream:GridItemColumns ColumnWidth="60%" ID="GridItemColumns1" runat="server">
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("UserName") %>' Visible="false"></asp:Label>
                                    <a href="Module.aspx?ctl=ManageUser&uid=<%#Eval("UserId")%>">
                                        <%#Eval("UserName")%></a>
                                </Meanstream:GridItemColumns>
                                <Meanstream:GridItemColumns ColumnWidth="15%" ID="GridItemColumns2" runat="server">
                                    <%#GetFlag(Eval("IsAnonymous"))%>
                                </Meanstream:GridItemColumns>
                                <Meanstream:GridItemColumns ColumnWidth="25%" ID="GridItemColumns3" IsLast="true"
                                    runat="server">
                                    <b>
                                        <%#Eval("LastActivityDate")%></b>
                                </Meanstream:GridItemColumns>
                            </Meanstream:GridItemView>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <Meanstream:ButtonField ButtonMode="Delete" ButtonType="Image" ItemStyle-VerticalAlign="Middle" Text="Remove" CommandName="Remove"
                        OnClientClick="javascript:if (!confirm('The user will be removed from this role. ')) return;">
                    </Meanstream:ButtonField>
                </Columns>
            </Meanstream:Grid>
            <Meanstream:DataPager ID="pager" runat="server" PageSize="9" PagedControlID="UsersInRoleGrid" Width="100%">
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
        </Meanstream:GridContainer>--%>
        </td>
    </tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>           
            