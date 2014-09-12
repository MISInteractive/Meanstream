<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PageSettings.ascx.vb" Inherits="Meanstream_Pages_UserControls_PageSettings" %>
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
        <td><a onmouseover="Tip('<b>Page Settings</b>', BALLOON, true, ABOVE, true, OFFSETX, -17, PADDING, 8)" onmouseout="UnTip()""><div class="icon-help"></div></a></td>
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
                        <br />
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                             <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td valign="middle" nowrap><strong>Menu Title*</strong></td>
                                    <td width="20" rowspan="20" valign="middle"><div class="spacer10x20" /></td>
                                    <td height="23" valign="middle"><asp:TextBox ID="txtMenuName" runat="server" SkinID="Text" Width="300"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  
                                           
                                      <tr>
                                        <td valign="middle" nowrap><strong>Page Type*</strong></td>
                                        <td valign="middle">
                                            <asp:RadioButtonList ID="rbPageType" runat="server" AutoPostBack="true">
                                                <asp:ListItem Text="A New Page" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Page (An existing Page on your site)" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Url (A Link to an external resource or file)" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td valign="middle" nowrap>&nbsp;</td>
                                        <td valign="middle">&nbsp;</td>
                                      </tr>
                                      <asp:Panel ID="UrlPanel" runat="server">
                                          <tr>
                                            <td valign="middle" nowrap><strong>Url*</strong></td>
                                            <td valign="middle">
                                                <asp:Panel ID="InternalUrl" runat="server">
                                                    <b>/</b> <asp:TextBox ID="txtUrlMapping" runat="server" SkinID="Text" Width="400" Text=""></asp:TextBox>
                                                    <br>
                                                    Url cannot contain spaces or special characters. 
                                                    <br>
                                                    <b>Example:</b> Contact-Us
                                                </asp:Panel>
                                                <asp:Panel ID="ExternalUrl" runat="server">
                                                    <asp:TextBox ID="txtExternalUrl" runat="server" SkinID="Text" Width="400" Text="http://"></asp:TextBox>
                                                    <br>
                                                    <b>Example:</b> http://domain.com/Destination.html
                                                </asp:Panel>
                                            </td>
                                          </tr>
                                          <tr>
                                            <td valign="middle" nowrap>&nbsp;</td>
                                            <td valign="middle">&nbsp;</td>
                                          </tr>
                                      </asp:Panel>
                                      <asp:Panel ID="InternalPagePanel" runat="server">
                                          <tr>
                                            <td valign="middle" nowrap><strong>Existing Page*</strong></td>
                                            <td valign="middle">
                                               
                                                        <Meanstream:ComboBox ID="ddlInternalPage" runat="server" SkinID="Combobox" Width="275" ComboPanelHeight="140" DefaultDisplayText="Select a page to link to" ImageButtonWidth="25" PanelCss="msCombo_Panel_TreeView"></Meanstream:ComboBox>     
                                                    
                                            </td>
                                          </tr>
                                          <tr>
                                            <td valign="middle" nowrap>&nbsp;</td>
                                            <td valign="middle">&nbsp;</td>
                                          </tr>
                                      </asp:Panel>
                                    
                                  
                                  <tr>
                                    <td valign="middle" nowrap><strong></strong></td>
                                    <td valign="middle">
                                        Select where in the menu this page should go. You can select a parent from the list of nodes below of default to "This Page has no Parent" to display in the top menu.
                                        <br>
                                        
                                                <Meanstream:ComboBox ID="ddlPage" runat=server Width=275 ComboPanelHeight=140 DefaultDisplayText="This Page has no Parent" DefaultDisplayValue="00000000-0000-0000-0000-000000000000" ImageButtonWidth="25" PanelCss="msCombo_Panel_TreeView"></Meanstream:ComboBox>
                                                
                                    </td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap><strong>Display Order*</strong></td>
                                    <td valign="middle"><asp:TextBox ID="txtMenuOrder" runat="server" Width="40" SkinID="Text" Text="0"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap><strong>Display In Menu</strong></td>
                                    <td valign="middle"><asp:CheckBox ID="chkDisplay" runat="server" /></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap><strong>Disable In Menu</strong></td>
                                    <td valign="middle"><asp:CheckBox ID="chkDisable" runat="server" /></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <asp:Panel ID="SkinTable" runat="server">
                                    <tr>
                                       <td valign="middle" nowrap><strong>Skin*</strong></td>
                                       <td valign="middle"><Meanstream:ComboBox ID="SkinDropDown" runat="server" Width="200" ComboPanelHeight="140" ImageButtonWidth="25"></Meanstream:ComboBox></td>
                                    </tr>
                                    <tr>
                                       <td valign="middle" nowrap>&nbsp;</td>
                                       <td valign="middle">&nbsp;</td>
                                    </tr>
                                  </asp:Panel>
                                    
                                </table>
                                 
                              </td> 
                            </tr>
                        </table>
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="MetaTab" HeaderText="metatags"  runat="server">
                <ContentTemplate>
                    <br />
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                          <tr>
                             <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td valign="middle" nowrap><strong>Title</strong></td>
                                    <td width="20" rowspan="9" valign="middle"><div class="spacer10x20" /></td>
                                    <td height="23" valign="middle"><asp:TextBox ID="txtTitle" runat="server" SkinID="Text" Width="350"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap><strong>Description</strong></td>
                                    <td valign="middle"><asp:TextBox ID="txtDescription" runat="server" Height="50" Width="350" TextMode="MultiLine" SkinID="Text" ></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap>&nbsp;</td>
                                    <td valign="middle">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="middle" nowrap><strong>Keywords</strong></td>
                                    <td valign="middle"><asp:TextBox ID="txtKeywords" runat="server" Height="50" Width="350" TextMode="MultiLine" SkinID="Text"></asp:TextBox></td>
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
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="SecurityTab" HeaderText="permissions"  runat="server">
            
                <ContentTemplate>
               
                
               
                    <br />
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                             <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td>
                                        <asp:GridView ID="SecurityGrid" runat="server" HeaderStyle-HorizontalAlign="Right" AutoGenerateColumns="false" GridLines="None" Width="450" ViewStateMode=Enabled>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div align="left"><b>Role</b></div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                    
                                                    <span class="label">
                                                        <asp:Literal ID="RoleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Id") %>' Visible="false"></asp:Literal>
                                                        <asp:Literal ID="RoleName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Literal>
                                                     </span>
                                                    
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
                             </td>
                          </tr>
                        </table>
                    <br />
                     
                </ContentTemplate>
            </Meanstream:TabPanel>
            <Meanstream:TabPanel ID="AdvancedTab" HeaderText="advanced"  runat="server">
                <ContentTemplate>
                    <br />
                        <table border="0" cellspacing="0" cellpadding="0">
                          <tr>
                             <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td valign="middle" nowrap><strong>Start Date</strong></td>
                                        <td width="20" rowspan="20" valign="middle"><div class="spacer10x20" /></td>
                                        <td height="23" valign="middle">
                                        <asp:TextBox ID="txtStartDate" runat="server" SkinID="Text" CssClass="textbox"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap>&nbsp;</td>
                                      <td valign="middle">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap><strong>End Date</strong></td>
                                      <td valign="middle">
                                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="Text" CssClass="textbox"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap>&nbsp;</td>
                                      <td valign="middle">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap><strong>Enable Caching</strong></td>
                                      <td valign="middle"><asp:CheckBox ID="cbEnableCaching" runat="server" Checked="true" /></td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap>&nbsp;</td>
                                      <td valign="middle">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap><strong>Enable ViewState</strong></td>
                                      <td valign="middle"><asp:CheckBox ID="cbEnableViewState" runat="server" Checked="true" /></td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap>&nbsp;</td>
                                      <td valign="middle">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap><strong>Index this page</strong></td>
                                      <td valign="middle"><asp:CheckBox ID="cbIndex" runat="server" Checked="true" /></td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" nowrap>&nbsp;</td>
                                      <td valign="middle">&nbsp;</td>
                                    </tr>
                                </table>
                             </td>
                          </tr>
                        </table>

                        <script language="javascript" type="text/javascript">
                            var startDateClientId = '<%= txtStartDate.ClientID %>';
                            var endDateClientId = '<%= txtEndDate.ClientID %>';
                            var txtMenuName = '<%=txtMenuName.ClientID %>';
                            var txtUrlMapping = '<%=txtUrlMapping.ClientID %>';
                            var txtTitle = '<%=txtTitle.ClientID %>';
                            var year = Date.toString('yyyy');

                            function pageLoad() {
                                $('#' + startDateClientId).datepicker({
                                    changeMonth: true,
                                    changeYear: true,
                                    yearRange: year + ':2020'
                                });
                                $('#' + endDateClientId).datepicker({
                                    changeMonth: true,
                                    changeYear: true,
                                    yearRange: year + ':2020'

                                });

                            }
                            $().ready(function () {
                                $('#' + txtMenuName).keypress(function () {
                                    var $this = $(this);
                                    window.setTimeout(function () {
                                        var s = $this.val();
                                        var r = s.replace(/\s/g, '-');
                                        $('#' + txtUrlMapping).val(r);
                                        $('#' + txtTitle).val(s);
                                    }, 0);
                                });
                            });
                        </script>
                </ContentTemplate>
            </Meanstream:TabPanel>
        
      
      </Meanstream:TabContainer>
         </div> 
         <br />
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><table border="0" align="right" cellpadding="0" cellspacing="0">
            <tr>
              <td nowrap><asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label></td>
              <td><div class="spacer10x20" /></td>
              <td>
                <Meanstream:BlockUIImageButton ID="btnSave" runat="server" StartMessage="Saving..." />
                <Meanstream:BlockUIImageButton ID="btnAdd"  runat="server" StartMessage="Saving..." /> 
                <Meanstream:BlockUIImageButton ID="btnCopyAdd"  runat="server" StartMessage="Saving..." />  
              </td>
            </tr>
          </table></td>
        </tr>
      </table>
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
<asp:HiddenField ID="litParentId" runat="server" />
<asp:HiddenField ID="litVersionId" runat="server" />

</ContentTemplate>
</asp:UpdatePanel>