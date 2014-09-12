<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PageEditor.ascx.vb" Inherits="Meanstream_Pages_UserControls_PageEditor" %>
<script language="javascript" type="text/javascript">
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

    function refreshVersion() {
        var versionID = getParam("VersionID");
        window.location.href = "./Edit.aspx?VersionID=" + versionID;
    }
</script>    
<style>
.spinner {
  margin: 100px auto;
  width: 20px;
  height: 20px;
  position: relative;
}

.container1 > div, .container2 > div, .container3 > div {
  width: 6px;
  height: 6px;
  background-color: #333;

  border-radius: 100%;
  position: absolute;
  -webkit-animation: bouncedelay 1.2s infinite ease-in-out;
  animation: bouncedelay 1.2s infinite ease-in-out;
  /* Prevent first frame from flickering when animation starts */
  -webkit-animation-fill-mode: both;
  animation-fill-mode: both;
}

.spinner .spinner-container {
  position: absolute;
  width: 100%;
  height: 100%;
}

.container2 {
  -webkit-transform: rotateZ(45deg);
  transform: rotateZ(45deg);
}

.container3 {
  -webkit-transform: rotateZ(90deg);
  transform: rotateZ(90deg);
}

.circle1 { top: 0; left: 0; }
.circle2 { top: 0; right: 0; }
.circle3 { right: 0; bottom: 0; }
.circle4 { left: 0; bottom: 0; }

.container2 .circle1 {
  -webkit-animation-delay: -1.1s;
  animation-delay: -1.1s;
}

.container3 .circle1 {
  -webkit-animation-delay: -1.0s;
  animation-delay: -1.0s;
}

.container1 .circle2 {
  -webkit-animation-delay: -0.9s;
  animation-delay: -0.9s;
}

.container2 .circle2 {
  -webkit-animation-delay: -0.8s;
  animation-delay: -0.8s;
}

.container3 .circle2 {
  -webkit-animation-delay: -0.7s;
  animation-delay: -0.7s;
}

.container1 .circle3 {
  -webkit-animation-delay: -0.6s;
  animation-delay: -0.6s;
}

.container2 .circle3 {
  -webkit-animation-delay: -0.5s;
  animation-delay: -0.5s;
}

.container3 .circle3 {
  -webkit-animation-delay: -0.4s;
  animation-delay: -0.4s;
}

.container1 .circle4 {
  -webkit-animation-delay: -0.3s;
  animation-delay: -0.3s;
}

.container2 .circle4 {
  -webkit-animation-delay: -0.2s;
  animation-delay: -0.2s;
}

.container3 .circle4 {
  -webkit-animation-delay: -0.1s;
  animation-delay: -0.1s;
}

@-webkit-keyframes bouncedelay {
  0%, 80%, 100% { -webkit-transform: scale(0.0) }
  40% { -webkit-transform: scale(1.0) }
}

@keyframes bouncedelay {
  0%, 80%, 100% { 
    transform: scale(0.0);
    -webkit-transform: scale(0.0);
  } 40% { 
    transform: scale(1.0);
    -webkit-transform: scale(1.0);
  }
}
</style>
<table id="pageTable" style="z-index: 1000px;" width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td width="20" height="46">&nbsp;</td><td valign="top">
<table border="0" cellspacing="0" cellpadding="0"><tr valign="top"><td width="10" valign="top"><div class="edit-page-top-left" /></td>
        <td class="edit-page-top-bg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td nowrap class="toolbar">page functions&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="icon-send-review" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;<asp:LinkButton ID="btnPublish" runat="server" Text="publish"></asp:LinkButton><asp:LinkButton ID="btnSendForReview" runat="server" Text="send for review" Visible="false"></asp:LinkButton><asp:LinkButton ID="btnSendForApproval" runat="server" Text="send for approval" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="top-line" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="icon-preview" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;<asp:HyperLink ID="btnPreview" runat="server">preview</asp:Hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="top-line" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="icon-settings" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;<asp:LinkButton ID="SettingsTarget" runat="server">settings</asp:LinkButton>
                <Meanstream:Window ID="btnSettings" runat="server" 
                    SkinID="Window" 
                    Width="700" 
                    Height="650" 
                    ShowUrl="true"
                    ShowLoader="true"
                    Title="Settings" OnClientClose="refreshVersion()">
                </Meanstream:Window>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="top-line" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <asp:Panel ID="pnlWorkflow" runat="server" Visible="false">  
              <td nowrap class="toolbar"><div class="icon-workflow" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;<asp:LinkButton ID="WorkflowTarget" runat="server">workflow</asp:LinkButton>
                <Meanstream:Window ID="btnWorkflow" runat="server" 
                    SkinID="Window" 
                    Width="850" 
                    Height="525" 
                    ShowUrl="true"
                    ShowLoader="true" 
                    Title="Workflow">
                </Meanstream:Window>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              <td nowrap class="toolbar"><div class="top-line" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              </asp:Panel>
              <td nowrap class="toolbar"><div class="icon-close" /></td>
              <td nowrap class="toolbar">&nbsp;&nbsp;<asp:Hyperlink ID="btnCloseAndSave" runat="server" >go back</asp:Hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
        </table></td>
        </tr>
    </table>
    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>     
    </td>
    <td valign="middle" style="visibility: hidden;"><table border="0" align="right" cellpadding="0" cellspacing="0">
      <tr>
        <td nowrap class="toolbar">widgets:&nbsp;&nbsp;</td>
        <td valign="bottom" nowrap>
            <Meanstream:ComboBox ID="Widgets" runat="server" Width="125" ComboPanelHeight="140" ImageButtonWidth="25"></Meanstream:ComboBox>
        </td>
        <td nowrap>&nbsp;&nbsp;</td>
        <td valign="bottom" nowrap>
            <Meanstream:ComboBox ID="SkinPanes" runat="server" Width="125" ComboPanelHeight="140" ImageButtonWidth="25"></Meanstream:ComboBox>
        </td>
        <td nowrap>&nbsp;&nbsp;&nbsp;</td>
        <td nowrap><div class="icon-widget" /></td>
        <td nowrap>&nbsp;&nbsp;</td>
        <td nowrap class="toolbar"><asp:LinkButton ID="btnAddWidget" runat="server" Text="add widget"></asp:LinkButton></td>
        <td nowrap>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td nowrap>

        </td>
      </tr>
    </table></td>
    <td width="20">&nbsp;</td>
  </tr>
</table>
<div>
    <asp:Literal ID="litFrame" runat="server"></asp:Literal>
</div>
<div class="spinner" id="preload-img">
    <div class="spinner-container container1">
    <div class="circle1"></div>
    <div class="circle2"></div>
    <div class="circle3"></div>
    <div class="circle4"></div>
    </div>
    <div class="spinner-container container2">
    <div class="circle1"></div>
    <div class="circle2"></div>
    <div class="circle3"></div>
    <div class="circle4"></div>
    </div>
    <div class="spinner-container container3">
    <div class="circle1"></div>
    <div class="circle2"></div>
    <div class="circle3"></div>
    <div class="circle4"></div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        
        var editor = $('#pageTable');
        var iframe = $('#editFrame');
        editor.css('display', 'none');
        iframe.css('display', 'none');
        
        $('#editFrame').load(function () {
            var h = $(window).height();
            var w = $(window).width();

            editor.css('display', 'block');
            var cssObj = { 'height': '2500px', 'width': w + 'px', 'display': 'block' }
            //var cssObj = { 'height': h + 'px', 'width': w + 'px', 'display': 'block' }
            iframe.css(cssObj);
            $('#preload-img').css('display', 'none');
        });

        
        $(window).bind("resize", resizeIframe);

        function resizeIframe() {
            var h = $(window).height();
            var w = $(window).width();

            //resize editFrame
            //var cssObj = { 'height': '100%', 'width': w + 'px', 'display': 'block' }
            var cssObj = { 'height': '2500px', 'width': w + 'px' }
            iframe.css(cssObj);
        }
     });
</script>
