<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ManageRepository.ascx.vb" Inherits="Meanstream_Host_UserControls_ManageRepository" %>
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
        <td nowrap><span class="largelink"><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></span></td>
        <td><div class="spacer10x20" /></td>
        <td><a onmouseover="Tip('<b>Datastore</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
      </tr>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><div class="spacer20x20" /></td>
        </tr>
      </table>
      <div width="100%" >
            <Meanstream:TabContainer ID="tabContainer" runat=server Width="645px" SkinID="Tabs">
                <Meanstream:TabPanel ID="PropertiesTab" HeaderText="properties" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hType" runat="server" />
                        <br />
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                          <tr>
                             <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td valign="middle" nowrap><strong>Type*</strong></td>
                                    <td width="20" rowspan="9" valign="middle"><div class="spacer10x20" /></td>
                                    <td height="23" valign="middle"><asp:TextBox ID="txtName" runat="server" SkinID="Text" Width="350" Enabled="true"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  
                                </table>
                             </td>
                          </tr>
                        </table>  
                        <br />
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td><table border="0" align="right" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td nowrap></td>
                                    <td><div class="spacer10x20" /></td>
                                    <td>
                                    <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." /> 
                                    </td>
                                </tr>
                                </table></td>
                            </tr>
                        </table>
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="FieldsTab" HeaderText="fields"  runat="server">
                <ContentTemplate>
                    <br />
                        <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
                          <tr>
                             <td valign="top">
                                <table>
                                <tr><td><strong>Name</strong></td></tr>
                                <tr><td colspan="3"><br /></td></tr>
                                <asp:Repeater ID="rFields" runat="server">
                                    <ItemTemplate>
                                            <asp:HiddenField ID="OriginalKey" runat="server" Value='<%# Eval("key") %>'></asp:HiddenField>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="FieldName" Width="350" SkinID="Text" runat="server" Text='<%# Eval("value") %>'></asp:TextBox>                
                                                </td>
                                                
                                                <td valign="middle">&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnAdd" CommandName="Add" runat="server" CommandArgument='<%# Eval("key")%>' Text="Add" Visible="false"></asp:LinkButton><asp:LinkButton ID="btnSave" CommandName="Save" runat="server" CommandArgument='<%# Eval("key")%>' Text="Save" Visible="false"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnRemove" CommandName="Remove" runat="server" CommandArgument='<%# Eval("key")%>' Text="Remove"></asp:LinkButton><asp:LinkButton ID="btnDelete" CommandName="Delete" runat="server" CommandArgument='<%# Eval("key")%>' Text="Delete" Visible="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                             </td>
                          </tr>
                          <tr>
                            <td>
                                + <asp:LinkButton ID="btnAddField" runat="server">Add field</asp:LinkButton>
                            </td>
                          </tr>
                        </table>         
                    <br />
                    <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td><table border="0" align="right" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <Meanstream:BlockUIImageButton ID="btnSaveFields" runat="server" StartMessage="Saving..." /> 
                                </td>
                                <td><div class="spacer10x20" /></td>
                                <td>
                                    <Meanstream:BlockUIImageButton ID="btnDeleteFields" runat="server" StartMessage="Deleting..." /> 
                                </td>
                            </tr>
                            </table></td>
                        </tr>
                    </table>--%>
                </ContentTemplate>
            </Meanstream:TabPanel>        
      </Meanstream:TabContainer>
      </div> 
         <br />
      
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