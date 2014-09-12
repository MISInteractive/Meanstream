<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Repositories.ascx.vb" Inherits="Meanstream_Host_UserControls_Repositories" %>
<script type="text/javascript">
    function windowClose() {
        window.location.href = './Default.aspx?ctl=Repositories';
    }
</script> 
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
  <td valign="top"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap><span class="largelink">Repository</span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Repository</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table></td>
      <td><div align="right">
        <table border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            <td width="12"><div class="create-repository"></div></td>
            <td width="6"><div class="spacer10x6" /></td>
            <td nowrap class="subnav">
                <a href="./Default.aspx?ctl=CreateRepositoryWizard">create a datastore</a>
            </td>
          </tr>
        </table>
    </div></td>
 </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
   <tr>
     <td><div class="spacer20x20" /></td>
   </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <Meanstream:GridContainer ID="Container" runat="server" Width="100%">
        <Meanstream:GridStatusMsg ID="lblStatus" runat="server"></Meanstream:GridStatusMsg>
        <Meanstream:Grid ID="Grid" AllowSorting="true" runat="server" AllowPaging="true" ShowFooter="false" Width="100%" GridMode="Standard">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Meanstream:GridHeader ID="Header1" runat="server" GridId="Grid">
                            <Meanstream:ColumnHeader ColumnWidth="50%" HeaderText="name" />
                            <Meanstream:ColumnHeader ColumnWidth="40%" HeaderText="total objects" />
                            <Meanstream:ColumnHeader ColumnWidth="5%" HeaderText="" />
			                <Meanstream:ColumnHeader ColumnWidth="5%" HeaderText="" />
                        </Meanstream:GridHeader>
                    </HeaderTemplate>
                    <ItemTemplate>
                    
                        <Meanstream:GridItemView ID="itemdata" runat=server>    
                            <Meanstream:GridItemColumns ColumnWidth="50%" ID="GridItemColumns1" runat="server">
                                <asp:HiddenField ID="type" runat="server" Value='<%# Eval("@class") %>'></asp:HiddenField>
                                <table width='100%'><tr><td width='5%'><div class='icon-repository' /></td><td width='5%'>&nbsp;&nbsp;&nbsp;</td><td width='90%' nowrap>
                                    <asp:PlaceHolder ID="phRepository" runat="server" EnableViewState="true"></asp:PlaceHolder>
                                </td></tr></table>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="40%" ID="GridItemColumns2" runat=server>
                                <%# GetTotalObjects(Eval("@class"))%>
                            </Meanstream:GridItemColumns>
                            <Meanstream:GridItemColumns ColumnWidth="5%" ID="GridItemColumns4" runat=server>
                                <a href='./Default.aspx?ctl=RepositoryItems&type=<%# Eval("@class")%>'>View/Edit</a>
                            </Meanstream:GridItemColumns>
				            <Meanstream:GridItemColumns ColumnWidth="5%" ID="GridItemColumns5" runat=server>
                                <a href='./UserControls/ExportRepository.aspx?type=<%# Eval("@class")%>' target="_blank">Export</a>
                            </Meanstream:GridItemColumns>
                        </Meanstream:GridItemView>
                        
                    </ItemTemplate>
                </asp:TemplateField>  
                <Meanstream:ButtonField  ButtonMode="Delete" ButtonType="Image" CommandName="Delete" OnClientClick="javascript:if (!confirm('The repository and its data will be deleted permanently.')) return;"></Meanstream:ButtonField> 
            </Columns>
        </Meanstream:Grid>
     </Meanstream:GridContainer>
    </ContentTemplate>
</asp:UpdatePanel>