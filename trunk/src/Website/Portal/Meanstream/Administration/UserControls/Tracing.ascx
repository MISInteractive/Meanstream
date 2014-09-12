<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tracing.ascx.vb" Inherits="Meanstream_Administration_UserControls_Tracing" %>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>

   <script>
       var model = Meanstream.UI.Models.Tracing.findAll(bindGrid);
      
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
                               TraceDateTime: { type: "string" },
                               TraceCategory: { type: "string" },
                               TraceDescription: { type: "string" }


                           }
                       }
                   }
               },

               pageable: true,
               scrollable: false,
               sortable: true,

               batch: true,

               columns: [{
                   field: "TraceDateTime",
                   width: "33%",
                   title: "TraceDateTime"
               },

            {
                field: "TraceCategory",
                width: "17%",
                title: "TraceCategory"
            },
             {
                 field: "TraceDescription",
                 width: "50%",
                 title: "TraceDescription"
             }

           ]
           });
       }
       function clearTracing() {
           Meanstream.UI.Models.Tracing.destroy();

       }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table valign="top" width="100%">
    <tr>
        <td valign="top">
            <table>
                <tr>
                    <td><br />
                        Enabled:
                    </td>
                    <td><br />
                        <asp:CheckBox ID="cbEnabled" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td><br />
                        Trace Level:
                    </td>
                    <td><br />
                        <Meanstream:ComboBox ID="ddlLevel"  runat="server" Width="50" ComboPanelHeight="85" ImageButtonWidth="25">
                            <Items>
                                <Meanstream:ComboBoxItem Text="1" Value="1" />
                                <Meanstream:ComboBoxItem Text="2" Value="2" />
                                <Meanstream:ComboBoxItem Text="3" Value="3" />
                                <Meanstream:ComboBoxItem Text="4" Value="4" />
                            </Items>
                        </Meanstream:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td><br />
                        Maximum Requests:
                    </td>
                    <td><br />
                        <Meanstream:ComboBox ID="ddlRequests"  runat="server" Width="50" ComboPanelHeight="75" ImageButtonWidth="25">
                            <Items>
                                <Meanstream:ComboBoxItem Text="1" Value="1" />
                                <Meanstream:ComboBoxItem Text="2" Value="2" />
                                <Meanstream:ComboBoxItem Text="3" Value="3" />
                            </Items>
                        </Meanstream:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="right">
                            <br />
                            <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                        </div>   
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
           <%-- <asp:LinkButton ID="btnClearTrace" runat="server">Clear Tracing</asp:LinkButton>--%>
           <a href="#" onclick="clearTracing()">Clear Tracing</a>
            <br><br>
             <div id="Grid" style="width:100%;outline:none;"></div>
            <%--<Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                <Meanstream:Grid ID="Grid" runat="server" AllowPaging="false"
                    ShowFooter="false" Width="100%" GridMode="Standard">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid">
                                    <Meanstream:ColumnHeader ColumnWidth="33%" HeaderText="TraceDateTime" />
                                    <Meanstream:ColumnHeader ColumnWidth="17%" HeaderText="TraceCategory" />
                                    <Meanstream:ColumnHeader ColumnWidth="50%" HeaderText="TraceDescription" />
                                </Meanstream:GridHeader>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <Meanstream:GridItemView ID="itemdata" runat="server">
                                    <Meanstream:GridItemColumns ColumnWidth="33%" ID="GridItemColumns1" runat="server">
                                        <b><%# Eval("TraceDateTime")%></b>
                                    </Meanstream:GridItemColumns>
                                    <Meanstream:GridItemColumns ColumnWidth="17%" ID="GridItemColumns2" runat="server">
                                        <%# Eval("TraceCategory")%>
                                    </Meanstream:GridItemColumns>
                                    <Meanstream:GridItemColumns ColumnWidth="50%" ID="GridItemColumns3" IsLast="true"
                                        runat="server">
                                        <b>
                                            <%#Eval("TraceDescription")%></b>
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
</ContentTemplate>
</asp:UpdatePanel>