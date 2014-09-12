<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Edit.aspx.vb" Inherits="Meanstream_Pages_Edit" %>
<%@ Register Src="~/Meanstream/Pages/UserControls/PageEditor.ascx" TagName="PageEditor" TagPrefix="Meanstream" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<Head id="Head1" runat="server">
   
</Head>
<body class="PageEditor-body">
    <form id="form1" runat="server">
        <Meanstream:ScriptManager ID="ScriptManager1" runat="server">
          <Scripts>
          <asp:ScriptReference Name="MicrosoftAjax.js" Path="/Scripts/MicrosoftAjax/MicrosoftAjax.js" />
          <asp:ScriptReference Name="MicrosoftAjaxWebForms.js" Path="/Scripts/MicrosoftAjax/MicrosoftAjaxWebForms.js" />
         </Scripts>
        </Meanstream:ScriptManager>
         <script src="/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/ScriptLoader.js" type="text/javascript"></script>
    <script src="/Scripts/Meanstream.Web.UI.Dependencies.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/App_Themes/Meanstream.2011/theme.js" type="text/javascript"></script>
    <script src="/Scripts/Meanstream.ScriptServices.js" type="text/javascript"></script>
        <Meanstream:PageEditor ID="PageEditor" runat="server" />
    </form>
</body>
</html>