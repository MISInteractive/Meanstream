<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Default.ascx.vb" Inherits="Meanstream_Widgets_Settings_UserControls_Default" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td width="20"><div class="spacer20x20" /></td>
    <td><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Widget Settings</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Widget Settings</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table>
      <table border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td width="20" rowspan="8" valign="middle"><div class="spacer10x20" /></td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="middle" nowrap><strong>Title:</strong></td>
          <td valign="middle">
            <asp:TextBox ID="txtName" runat="server" SkinID="Text" Width="350"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="middle" nowrap><strong>Start Date:</strong></td>
          <td valign="middle">
            <asp:TextBox ID="txtStartDate" runat="server" SkinID="Text" Width="150"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
        <tr>
          <td valign="middle" nowrap><strong>End Date:</strong></td>
          <td valign="middle">
            <asp:TextBox ID="txtEndDate" runat="server" SkinID="Text" Width="150"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td valign="middle" nowrap>&nbsp;</td>
          <td valign="middle">&nbsp;</td>
        </tr>
     </table>    

    <script language="javascript" type="text/javascript">
        var startDateClientId = '<%= txtStartDate.ClientID %>';
        var endDateClientId = '<%= txtEndDate.ClientID %>';
        var year = Date.toString('yyyy');
        $(document).ready(function () {
            $('#' + startDateClientId).datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: year + ':9999'
            });
            $('#' + endDateClientId).datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: year + ':9999'
            });
        });
     </script>
<br>
<b>SECURITY</b>
<table>
    <tr>
        <td>
            <asp:GridView ID="SecurityGrid" runat="server" Width="450" GridLines="None" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                           <div align="left"><b>Role</b></div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Literal ID="RoleID" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Literal>
                            <span class="label"><asp:Literal ID="RoleName" runat="server" Text='<%# Bind("Name") %>'></asp:Literal></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                           <div align="left"><b>View</b></div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="RowLevelCheckBoxView" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                           <div align="left"><b>Edit</b></div>
                        </HeaderTemplate>
                        <ItemTemplate>
            
                            <asp:CheckBox runat="server" ID="RowLevelCheckBoxEdit" />
                        </ItemTemplate>
                    </asp:TemplateField>    
                </Columns>
            </asp:GridView>                         
        </td>
    </tr>
</table>

    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><table border="0" align="right" cellpadding="0" cellspacing="0">
            <tr>
              <td nowrap></td>
              <td><div class="spacer10x20" /></td>
              <td><Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." /></td>
            </tr>
          </table></td>
        </tr>
      </table>
      <asp:Literal ID="litM" runat="server" />
   <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table></td>
    <td width="20"><div class="spacer20x20" /></td>
  </tr>
  <tr>
    <td colspan="3"><div class="spacer20x20" /></td>
  </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>