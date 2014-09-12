<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserMenu.ascx.vb" Inherits="Meanstream_UI_UserControls_UserMenu" %>
<table cellpadding="0" cellspacing="20" width=400>
    <tr style="height: 57px;">
        <td valign="top">
            <a href="/Meanstream/Profile/Default.aspx?ctl=MyProfile"><img id="profile" alt="my profile" src="" icon='profile.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Profile/Default.aspx?ctl=MyProfile">Profile</a></span>
            <br/>
            <span class="text">View and edit your profile. <br />Change your password.</span>       
        </td>
    </tr>
    <tr style="height: 57px;">
        <td valign="top">
            <a href="/Meanstream/Profile/Default.aspx?ctl=Messages"><img id="mail" alt="messages" border="0" src="" icon="mail.png" /></a>
        </td>
        <td valign="top">
           <span class="title"><asp:HyperLink ID="btnInbox" NavigateUrl="~/Meanstream/Profile/Default.aspx?ctl=Messages" runat="server"></asp:HyperLink></span>
           <br/>
           <span class="text">View messages from site users. <br />Send messages and email to users.</span>
        </td>
    </tr>
    <tr style="height: 57px;">
        <td valign="top">
            <a href="/Login.aspx?action=logout"><img ID="btnLogout" alt="logout" src="" icon="sign-off.png" border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Login.aspx?action=logout">Logout</a></span> 
        </td>
    </tr>
</table>
