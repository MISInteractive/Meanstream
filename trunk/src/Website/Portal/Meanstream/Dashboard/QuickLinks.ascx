<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuickLinks.ascx.vb" Inherits="Meanstream_Dashboard_QuickLinks" %>
<script type="text/javascript">
    function goToUsers() {
        var clientid = '<%= btnCreateUser.ClientID %>';
        var control = 'litUserName';
        var win = new msWindowObject(clientid);
        win.control = control;
        var username = win.ControlValue();
	    msCloseWin();
        
        if (username.value != '') {
            window.location.href = './Administration/Default.aspx?ctl=Users';
        }
    }
    function createRedirectToEdit() {
        var clientid = '<%= btnCreatePage.ClientID %>';
        var control = 'litVersionId';
        var win = new msWindowObject(clientid);
        win.control = control;
        var versionId = win.ControlValue();
    	msCloseWin();

        if (versionId.value != '') {
            window.location.href = './Pages/Edit.aspx?VersionID=' + versionId.value;
        }
    }
</script>   
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="20px"><div class="tree-barleft"></div></td>
          <td class="tree-barmid"><div class="treeheader">QUICK LINKS</div></td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td class="tree-bg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="20" rowspan="13"><div class="spacer20x10" /></td>
          <td valign="top">&nbsp;</td>
          <td>&nbsp;</td>
          <td class="nav2">&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td width="16" valign="top"><div class="icon-next" /></td>
          <td width="10"><div class="spacer10x10" /></td>
          <td width="100%" class="treelinks">
          <asp:LinkButton ID="CreatePageTarget" runat="server">create a page</asp:LinkButton>
            <Meanstream:Window ID="btnCreatePage" runat="server" 
                SkinID="Window" 
                ShowUrl="true" 
                ShowLoader="true"
                Width="700" 
                Height="625" 
                Title="Create Page"
                OnClientClose="createRedirectToEdit()" 
                NavigateUrl="./Pages/Module.aspx?ctl=PageSettings&Action=Add" >
            </Meanstream:Window>
          
          </td>
          <td width="20"><div class="spacer20x10" /></td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td valign="top"><div class="icon-next" /></td>
          <td>&nbsp;</td>
          <td class="treelinks">
          <asp:LinkButton ID="CreateUserTarget" runat="server">create a user</asp:LinkButton>
            <Meanstream:Window ID="btnCreateUser" runat="server" 
                SkinID="Window" 
                Width="810" 
                Height="425" 
                ShowLoader="true" 
                ShowUrl="true" 
                Title="Create User" 
                NavigateUrl="./Administration/Module.aspx?ctl=CreateUser" 
                OnClientClose="goToUsers()">
            </Meanstream:Window>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td valign="top"><div class="icon-next" /></td>
          <td>&nbsp;</td>
          <td class="treelinks"><a href="http://www.google.com/analytics" target="_blank">google analytics</a></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td>&nbsp;</td>
          <td class="nav2">&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
    </table></td>
  </tr>
</table>