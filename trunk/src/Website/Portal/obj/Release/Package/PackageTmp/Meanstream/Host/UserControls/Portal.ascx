<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Portal.ascx.vb" Inherits="Meanstream_Host_UserControls_Portal" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table valign="top">
    <tr>
        <td valign="top">
            <table>
                <tr>
                    <td>
                        Portal Version:
                    </td>
                    <td>
                        <asp:Literal ID="txtVersion" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td><br />
                        Support Email Address:
                    </td>
                    <td><br />
                        <asp:TextBox ID="txtSupportEmail" runat="server" Width="250" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><br />
                        Exports Root:
                    </td>
                    <td><br />
                        <asp:TextBox ID="txtExportsRoot" runat="server" Width="200" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><br />
                        Page Cache:
                    </td>
                    <td><br />
                        <asp:TextBox ID="txtCache" runat="server" Width="50" CssClass="textbox"></asp:TextBox>
                        (Hours)
                    </td>
                </tr>
                <tr>
                    <td><br />
                        Enable Caching:
                    </td>
                    <td><br />
                        <asp:CheckBox ID="cbCaching" runat="server" />
                    </td>
                </tr>
                <tr runat="server" visible="false">
                    <td><br />
                        Enable Workflow:
                    </td>
                    <td><br />
                        <asp:CheckBox ID="cbWorkflow" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">     <br />        
                        <asp:LinkButton ID="btnDeleteCache" runat="server">Clear Cache</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">  <br />           
                        <asp:LinkButton ID="btnRecycleApplicationPool" runat="server">Recycle Application Pool</asp:LinkButton>
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
</table>
</ContentTemplate>
</asp:UpdatePanel>