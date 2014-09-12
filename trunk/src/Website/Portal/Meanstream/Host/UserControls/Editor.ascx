<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Editor.ascx.vb" Inherits="Meanstream_Host_UserControls_Editor" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table valign="top"> 
        <tr>
            <td>
                Image Path:
            </td>
            <td>
                <asp:TextBox ID="txtImageGalleryPath" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><br />
                Video Path:
            </td>
            <td><br />
                <asp:TextBox ID="txtVideoGalleryPath" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><br />
                Flash Path:
            </td>
            <td><br />
                <asp:TextBox ID="txtFlashGalleryPath" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><br />
                Templates Path:
            </td>
            <td><br />
                <asp:TextBox ID="txtTemplateGalleryPath" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><br />
                Documents Path:
            </td>
            <td><br />
                <asp:TextBox ID="txtDocumentsPath" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><br />
                CSS Stylesheet Path:
            </td>
            <td><br />
                <asp:TextBox ID="txtCssStylesheetPath" runat="server" CssClass="textbox" 
                    Width="300px"></asp:TextBox>
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
</ContentTemplate>
</asp:UpdatePanel>
                