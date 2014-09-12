<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SearchEngineSubmit.ascx.vb" Inherits="Meanstream_Administration_UserControls_SearchEngineSubmit" %>
<table valign="top">          
    <tr>
        <td>
            Search Engine Sitemap:
        </td>
        <td colspan="2">
            <asp:HyperLink ID="SearchEngineSiteMapURL" runat="server" Target="_blank" />  
        </td>
    </tr>
    <tr>
        <td height="25"></td>
        <td>
            <asp:LinkButton ID="btnRefreshIndex" runat="server">Re-Index Site</asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton ID="btnDeleteIndex" runat="server">Delete Index</asp:LinkButton>
        </td>
    </tr>       
    <tr>
        <td height="5"></td>
        <td colspan="2">
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<br>
<table>
    <tr>
        <td>
           Search Engine Submit:
        </td>
        <td valign="bottom">
            <Meanstream:ComboBox ID="cboSearchEngine"  runat="server" Width="150" ComboPanelHeight="100" ImageButtonWidth="25" DefaultDisplayValue="0" DefaultDisplayText="Select">
                <Items>
                    <Meanstream:ComboBoxItem Text="Google" Value="Google" />
                    <Meanstream:ComboBoxItem Text="Bing" Value="Bing" />
                    <Meanstream:ComboBoxItem Text="Ask" Value="Ask" />
                </Items>
            </Meanstream:ComboBox> 
        </td>
        <td valign="bottom">
            &nbsp;&nbsp;&nbsp;<Meanstream:BlockUIImageButton ID="btnSubmit" runat="server" StartMessage="Submitting..." />
        </td>
    </tr>         
</table>
<br />
<table>                      
    <tr>
        <td>
            <asp:Literal ID="litMessage" runat="server" />
        </td>
    </tr>
</table>