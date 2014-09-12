<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" CodeFile="Admin.aspx.vb" MasterPageFile="~/Meanstream/UI/Skins/Module.master" Inherits="Meanstream_Widgets_FreeText_Admin" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CuteEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CenterPane" Runat="Server">
<script type="text/javascript">
    function getParam(name) {
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var tmpURL = window.location.href;
        var results = regex.exec(tmpURL);
        if (results == null)
            return "";
        else
            return results[1];
    }
    function getSharedContent() {
        var clientid = '<%= SelectSharedContent.ClientID %>';
        var control = 'litContent';
        var id = msWindowObject(clientid, control);
        if (id.value != "") {
            window.location.href = "Admin.aspx?WidgetId=" + getParam("WidgetId") + "&SharedId=" + id.value;
        }  
        closeMSWindow(clientid);
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:HiddenField ID="FreeTextId" runat="server" />
<table align="center">
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <b>Title:</b>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="225" SkinID="Text"></asp:TextBox>
                    </td>                   
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div align="right">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="Checkout" Visible="false" runat="server">Checkout this Content</asp:LinkButton>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <CuteEditor:Editor ID="Editor" runat="server" Width="975" Height="450" AutoConfigure="Simple" ShowBottomBar="true"></CuteEditor:Editor>
        </td>
    </tr>
    <tr style="visibility: hidden;">
        <td>
            <asp:CheckBox ID="Shared" runat="server" />Share this Content&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="SelectSharedContentTarget" runat="server">Select Shared Content</asp:LinkButton>
            <Meanstream:Window ID="SelectSharedContent" runat="server" 
                SkinID="Window" 
                Width="800" 
                Height="300"  
                Title="Select Shared Content" 
                ShowLoader="true" 
                ShowUrl="true" 
                OnClientClose="getSharedContent()" 
                NavigateUrl="SelectSharedContent.aspx" >
            </Meanstream:Window>
        </td>
    </tr>
    <tr>
        <td><br>
            <div align="right">
                <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
            </div>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
