<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TreeView.ascx.vb" Inherits="Meanstream_Pages_UserControls_TreeView" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="20"><div class="tree-barleft" /></td>
          <td class="tree-barmid"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td class="treeheader">SITE MAP</td>
              <td width="15"><a><div class="icon-helptree"></div></a></td>
              <td width="20"><div class="spacer10x20" /></td>
            </tr>
          </table></td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td class="tree-bg"> 
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="20" rowspan="13"><div class="spacer10x20" /></td>
          <td valign="top">&nbsp;</td>
          <td>&nbsp;</td>
          <td class="nav2">&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5" valign="top">
                <asp:PlaceHolder ID="phTreeview" runat="server"></asp:PlaceHolder>
            </td>
        </tr>                
    </table> 
    <br>            
    </td>
  </tr>
</table>