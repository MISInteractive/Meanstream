<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PageFunctions.ascx.vb" Inherits="Meanstream_Pages_UserControls_PageFunctions" %>
<script language="javascript" type="text/javascript">
    function createRedirectToEdit() {
        var clientid = '<%= btnCreatePage.ClientID %>';
        var control = 'litVersionId';
        var win = new msWindowObject(clientid);
        win.control = control;
        var versionId = win.ControlValue();
        msCloseWin();
        if (versionId.value != '') {
            window.location.href = './Edit.aspx?VersionID=' + versionId.value;
        }
    }
    function copyRedirectToEdit() {
        var clientid = '<%= btnCopyCreatePage.ClientID %>';
        var control = 'litVersionId';
        var win = new msWindowObject(clientid);
        win.control = control;
        var versionId = win.ControlValue();
        msCloseWin();
        if (versionId.value != '') {
            window.location.href = './Edit.aspx?VersionID=' + versionId.value;
        }
    }

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
    function getPageIDFromHeader() {
        var objHead = document.getElementsByTagName('head');
        var objID = objHead[0].getAttribute('id');
        var sPageID = objID.substring(objID.lastIndexOf('_') + 1);
        return sPageID;
    }
    function getPageName() {
        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
        sPage = sPage.replace(".aspx", "");
        return sPage;
    }
    function PageDeleteEventHandler() {
        var sPage = getPageName();
        var sPageID = getParam("PageID");
        if (sPageID == "1") {
            alert("You cannot delete the home page.");
            window.location.href = "./" + sPage + ".aspx";
            window.event.returnValue = false;
        }
        else {
            var answer = confirm("send this page to recycle bin?");

            if (answer)
                window.location.href = "./Default.aspx?ctl=ViewPage&Action=Delete&PageID=" + sPageID;
        }
    }
    function disableLinks() {
        //var doc = document.frames["pageFrame"].document;
        var iframe = document.getElementById("pageFrame");
        if (!iframe) return;
        var doc = iframe.contentWindow || iframe.contentDocument;
        //if (!content) return;
        //var height = content.document.body.clientHeight;

        var c = doc.links;
        for (var i = 0; i < c.length; i++) {
            // display the href as a ToolTip
            c[i].title = c[i].href;
            c[i].href = "#";
            //c[i].disabled = true;
        }
        disableSubmitButtons(doc.getElementsByTagName("INPUT"));
        disableSubmitButtons(doc.getElementsByTagName("BUTTON"));
    }
    function disableSubmitButtons(c) {
        for (var i = 0; i < c.length; i++) {
            if (c[i].type == "submit")
                c[i].disabled = true;
            if (c[i].type == "image")
                c[i].disabled = true;
        }
    }
</script>
 <div align="left">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">      
        <tr>
        <td width="12"><div class="spacer10x10" /></td>
          <td width="100%">
          <table border="0" cellspacing="0" cellpadding="0">
              <tr><td width="100%"><br>
              <table border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                          <td><strong>page functions:</strong></td>
                          <td width="20"><div class="spacer10x20" /></td>
                          <td><table border="0" align="right" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="12"><div class="icon-addpage" /></td>
                                <td width="6"><div class="spacer10x6" /></td>
                                <td nowrap class="subnav"><asp:LinkButton ID="CreatePageTarget" runat="server">create a page</asp:LinkButton>
                                <Meanstream:Window ID="btnCreatePage" runat="server" 
                                    SkinID="Window" 
                                    Width="700" 
                                    Height="625" 
                                    ShowLoader="true" 
                                    ShowUrl="true" 
                                    OnClientClose="createRedirectToEdit()" 
                                    Title="Create Page" 
                                    NavigateUrl="Module.aspx?ctl=PageSettings&Action=Add">
                                </Meanstream:Window>
                                </td>
                              </tr>
                          </table></td>
                          <td width="20"><div class="spacer10x20" /></td>
                          <td><table border="0" align="right" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="12"><a href="#.htm"><div class="icon-pagecopy"></div></a></td>
                                <td width="6"><div class="spacer10x6" /></td>
                                <td nowrap class="subnav"><asp:LinkButton ID="CopyCreatePageTarget" runat="server" Enabled="false">copy &amp; create a page</asp:LinkButton>
                                <Meanstream:Window ID="btnCopyCreatePage" runat="server" 
                                    SkinID="Window" 
                                    Width="700" 
                                    Height="600" 
                                    ShowLoader="true" 
                                    ShowUrl="true" 
                                    OnClientClose="copyRedirectToEdit()"
                                    Title="Copy and Create Page">
                                </Meanstream:Window>                              
                                </td>
                              </tr>
                          </table></td>
                          <td width="20"><div class="spacer10x20" /></td>
                          <td><table border="0" align="right" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="12"><div class="icon-pageedit" /></td>
                                <td width="6"><div class="spacer10x6" /></td>
                                <td nowrap class="subnav"><asp:Hyperlink ID="btnEdit" runat="server">edit this page</asp:Hyperlink></td>
                              </tr>
                          </table></td>
                          <td width="20"><div class="spacer10x20" /></td>

                                <asp:Panel ID="pIsHomePage" runat="server">
                                    <td>
                                    <table border="0" align="right" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="12"><a href="javascript:PageDeleteEventHandler();"><div class="icon-pagedelete"></div></a></td>
                                            <td width="6"><div class="spacer10x6" /></td>
                                            <td nowrap class="subnav"><a href="javascript:PageDeleteEventHandler();">send to recycle bin</a></td>
                                        </tr>
                                    </table>
                                    </td>
                                    <td width="20"><div class="spacer10x20" /></td>
                                    <td>
                                    <table border="0" align="right" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="12"><div class="icon-home" /></td>
                                            <td width="6"><div class="spacer10x6" /></td>
                                            <td nowrap class="subnav"><asp:LinkButton ID="btnSetHomePage" runat="server">set as home page</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                    </td>
                                </asp:Panel>

                        </tr>
                    </table>
                </td>
              </tr>
            </table>
              <br>
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td colspan="3" class="tab-line"><div class="spacer1x20" /></td>
                </tr>
                <tr>
                  <td width="1" class="tab-line"><div class="spacer10x1" /></td>
                  <td><div align="center"><asp:Literal ID="litFrame" runat="server"></asp:Literal></div></td>
                  <td width="1" class="tab-line"><div class="spacer10x1" /></td>
                </tr>
                <tr>
                  <td colspan="3" class="tab-line"><div class="spacer1x20" /></td>
                </tr>
              </table>
              <br><br>  
            </td>
          <td width="12"><div class="spacer10x10" /></td>
        </tr>      
    </table>  
 </div>   

<script type="text/javascript" language="javascript">
setTimeout("disableLinks()",3000);
</script>