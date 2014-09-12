<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventLogs.ascx.vb" Inherits="Meanstream_Administration_UserControls_EventLogs" %>
<script src="/Meanstream/UI/Services/Meanstream.UI.Models.js" type="text/javascript"></script>
<script>

    $().ready(function () {

        var model = Meanstream.UI.Models.EventLog.findAll(bindGrid);


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
                            Message: { type: "string" },
                            MessageType: { type: "string" }
                        }
                    }
                }
            },

            pageable: true,
            scrollable: false,
            sortable: true,
            //note template syntax for a field: #=field#  
            columns: [{

                field: "MessageType",
                width: "20%",
                title: "Message Type"
            }, {
                field: "Message",
                width: "80%",
                title: "Message"
            }
           ]
        });
    }
</script>
<table valign="top" width="100%">
    <tr>
        <td align="left">
            <br>
            <div id="Grid" style="width:100%;outline:none;"></div>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
                        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
                        <Meanstream:Grid ID="Grid" runat="server" AllowPaging="false"
                            ShowFooter="false" Width="100%" GridMode="Standard">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid">
                                            <Meanstream:ColumnHeader ColumnWidth="20%" HeaderText="MessageType" />
                                            <Meanstream:ColumnHeader ColumnWidth="80%" HeaderText="Message" />
                                        </Meanstream:GridHeader>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <Meanstream:GridItemView ID="itemdata" runat="server">
                                            <Meanstream:GridItemColumns ColumnWidth="20%" ID="GridItemColumns1" runat="server">
                                                <b><%# Eval("MessageType")%></b>
                                            </Meanstream:GridItemColumns>
                                            <Meanstream:GridItemColumns ColumnWidth="80%" ID="GridItemColumns2" runat="server">
                                                <%# Eval("Message")%>
                                            </Meanstream:GridItemColumns>
                                        </Meanstream:GridItemView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </Meanstream:Grid>
                    </Meanstream:GridContainer>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
</table>