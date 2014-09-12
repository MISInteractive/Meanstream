<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserStats.ascx.vb" Inherits="Meanstream_Dashboard_UserStats" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td class="dash-user-table"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td colspan="3"><div class="spacer10x10" /></td>
          </tr>
        <tr>
          <td width="10"><div class="spacer10x10" /></td>
          <td width="100%"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr >
              <td><div class="user-stat-left-bar"></div></td>
              <td class="brightbarheader">USER STATS</td>
              <td width="6"><div class="user-stat-right-bar" /></td>
            </tr>
          </table>
            
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td colspan="3"><div class="spacer10x10" /></td>
                </tr>
              <tr>
                <td width="10"><div class="spacer10x10" /></td>
                <td width="100%"><asp:Literal ID="litUsersOnline" runat="server"></asp:Literal> Users Online Now<br>
                  <a href="./Administration/Default.aspx?ctl=Users&Action=Online">who is online?</a></td>
                <td width="10"><div class="spacer10x10" /></td>
              </tr>
              <tr>
                <td colspan="3"><div class="spacer10x10" /></td>
                </tr>
              <tr>
                <td colspan="3" class="dash-line"><div class="spacer1x10" /></td>
                </tr>
              <tr>
                <td colspan="3"><div class="spacer10x10" /></td>
                </tr>
              <tr>
                <td>&nbsp;</td>
                <td><asp:Literal ID="litUsers" runat="server"></asp:Literal> Users<br>
                  <a href="./Administration/Default.aspx?ctl=Users">view users</a></td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td colspan="3"><div class="spacer10x10" /></td>
                </tr>
              <tr>
                <td colspan="3" class="dash-line"><div class="spacer1x10" /></td>
                </tr>
              <tr>
                <td colspan="3"><div class="spacer10x10" /></td>
                </tr>
              <tr>
                <td>&nbsp;</td>
                <td><asp:Literal ID="litNewUsers" runat="server"></asp:Literal> New Users in the Last 30 Days<br>
                  <a href="./Administration/Default.aspx?ctl=Users&Action=New">who is new?</a></td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td colspan="3"><div class="spacer10x10" /></td>
                </tr>
            </table></td>
          <td width="10"><div class="spacer10x10" /></td>
        </tr>
        <tr>
          <td colspan="3"><div class="spacer10x10" /></td>
          </tr>
      </table></td>
    </tr>
  </table>