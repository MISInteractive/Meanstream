<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CreateRepositoryWizard.ascx.vb" Inherits="Meanstream_Host_UserControls_CreateRepositoryWizard" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td><table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td nowrap><span class="largelink">Create Datastore</span></td>
            <td><div class="spacer10x20" /></td>
            <td><a onmouseover="Tip('<b>Create Datastore</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
          </tr>
        </table>
          <br/></td>
    </tr>
    <tr>
      <td>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><div class="spacer20x20" /></td>
                <td class="nav2">&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td width="20"><div class="spacer10x20" /></td>
                <td width="100%" class="nav2">
                    <div align="left">
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                          <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                        <ContentTemplate> 

                        <asp:HiddenField ID="HiddenFile" runat="server" />

                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                          <tr>
                             <td valign="top">
                                 <asp:RadioButtonList ID="rListChoose" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text=" <strong>Create a new datastore manually:</strong> Create custom fields and enter data from another source." Value="Manual" Selected="True" />
                                    <asp:ListItem Text=" <strong>Create a new datastore from an external file:</strong> Upload a spreasheet of records to create fields from your current data." Value="Upload" />
                                 </asp:RadioButtonList>
                             </td>
                          </tr>
                        </table>
                        <br />
                        <br />
                        <span class="header">Properties</span>
                        <br />
                        <br />
                        <span class="text">Give your datastore a name. The name must be unique.</span>
                        <br />
                        <br />
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                          <tr>
                             <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td valign="middle" nowrap><strong>Name*</strong></td>
                                    <td width="20" rowspan="9" valign="middle"><div class="spacer10x20" /></td>
                                    <td height="23" valign="middle"><asp:TextBox ID="txtName" runat="server" SkinID="Text" Width="350"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <%--<tr>
                                    <td valign="middle" nowrap><strong>Description</strong></td>
                                    <td valign="middle"><asp:TextBox ID="txtDescription" runat="server" Height="50" Width="350" TextMode="MultiLine" SkinID="Text" ></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap><strong>Tags</strong></td>
                                    <td valign="middle"><asp:TextBox ID="txtTags" runat="server" Height="50" Width="350" TextMode="MultiLine" SkinID="Text"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>--%>
                                </table>
                             </td>
                          </tr>
                        </table>  
                        <br />
                        <br />
                        <asp:Panel ID="pFields" runat="server" Visible="false">
                            <span class="header">Fields</span>
                            <br />
                            <br />
                            <span class="text">Define your fields by adding the field name and selecting the data type from the drop down. You may edit fields at anytime when entering data.</span>
                            <br />
                            <br />
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                              <tr>
                                 <td valign="top">
                                    <table>
                                    <tr><td><strong>Name</strong></td></tr>
                                    <tr><td colspan="3"><br /></td></tr>
                                    <asp:Repeater ID="rFields" runat="server">
                                        <ItemTemplate>
                                        
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="FieldName" Width="350" SkinID="Text" runat="server" Text='<%# Eval("Key") %>'></asp:TextBox>                
                                                    </td>
                                                    <%--<td>
                                                        <asp:HiddenField ID="HDataType" runat="server" Value='<%# Eval("Value") %>' />
                                                        <asp:DropDownList ID="DataType" runat="server">
                                                            <asp:ListItem Text="String" Value="System.String" Selected="True" />
                                                            <asp:ListItem Text="Integer" Value="System.Int32" />
                                                            <asp:ListItem Text="Long" Value="System.Int64" />
                                                            <asp:ListItem Text="Decimal" Value="System.Decimal" />
                                                            <asp:ListItem Text="Boolean" Value="System.Boolean" />
                                                            <asp:ListItem Text="Guid" Value="System.Guid" />
                                                        </asp:DropDownList>                
                                                    </td>--%>
                                                    <td valign="middle">&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkRemove" CommandName="Remove" runat="server" CommandArgument='<%# Eval("Key")%>' Text="Remove"></asp:LinkButton>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                
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
                            <br />  
                            <br />   
                            <div align="left"><Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." /></div>
                        </asp:Panel>   

                        <asp:Panel ID="pUpload" runat="server" Visible="false">
                            <span class="header">Upload Spreadsheet</span>
                            <br />
                            <br />
                            <span class="text">Upload a tab delimited file. The fields in your list will be named according to the first row of fields present in your file. You may edit fields after the upload has finished.</span>
                            <br />
                            <br />
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                              <tr>
                                 <td valign="top">
                                    <table width="250" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="FileUpload" runat="server" SkinID="Text" /> <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red" />
                                            </td>
                                        </tr>  
                                    </table>
                                    <br />
                                    <br />
                                    <asp:ImageButton ID="btnUpload" runat="server" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';"  />
                                    <br />
                                    <br />
                                 </td>
                              </tr>
                            </table>         
                        </asp:Panel>

                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <br />
                </td>
                <td width="20"><div class="spacer10x20" /></td>
              </tr>
          </table>
      </td>
   </tr>
</table>