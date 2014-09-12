<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdministrationMenu.ascx.vb" Inherits="Meanstream_UI_UserControls_AdministrationMenu" %>
<table cellpadding="0" cellspacing="20" width="550">
    <tr style="height: 57px;">
        <td valign="top">
            <a href="/Meanstream/Pages/Default.aspx?ctl=Pages"><img id="pages" alt="pages" border="0" src="" icon="manage-pages.png" /></a>
        </td>
        <td valign="top">
           <span class="title"><a href="/Meanstream/Pages/Default.aspx?ctl=Pages">Pages</a></span>
           <br/>
           <span class="text">Update and add pages on the site.</span>
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Pages/Default.aspx?ctl=Skins"><img id="skins" alt="skins" src="" icon='manage-skins.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Pages/Default.aspx?ctl=Skins">Skins</a></span>
            <br/>
            <span class="text">View what design templates are available.</span>       
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Pages/Default.aspx?ctl=RecycleBin"><img id="recycle-bin" alt="recycle bin" src="" icon='recycle.png' border="0" /></a>
            <br>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Pages/Default.aspx?ctl=RecycleBin">Recycle Bin</a></span>
            <br/>
            <span class="text">Restore deleted pages and widgets.</span>  
            <br/>     
        </td>
    </tr>

    <tr style="height: 57px;">
        <td valign="top">
            <a href="/Meanstream/Administration/Default.aspx?ctl=Users"><img id="users" alt="users" border="0" src="" icon="user-accounts.png" /></a>
        </td>
        <td valign="top">
           <span class="title"><a href="/Meanstream/Administration/Default.aspx?ctl=Users">User Accounts</a></span>
           <br/>
           <span class="text">View, add and edit registered users.</span>
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Administration/Default.aspx?ctl=Roles"><img id="roles" alt="security roles" src="" icon='security-roles.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Administration/Default.aspx?ctl=Roles">Security Roles</a></span>
            <br/>
            <span class="text">View, add and edit roles.</span>       
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Administration/Default.aspx?ctl=Messaging"><img id="messaging" alt="messaging" src="" icon='messaging.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Administration/Default.aspx?ctl=Messaging">Messaging</a></span>
            <br/>
            <span class="text">&nbsp;</span>       
        </td>
    </tr>

    <tr style="height: 57px;">
        <td valign="top">
            <a href="/Meanstream/Administration/Default.aspx?ctl=Diagnostics"><img id="diagnostics" alt="diagnostics" border="0" src="" icon="diagnostics.png" /></a>
        </td>
        <td valign="top">
           <span class="title"><a href="/Meanstream/Administration/Default.aspx?ctl=Diagnostics">Diagnostics</a></span>
           <br/>
           <span class="text">View website activity and logs.</span>
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Administration/Default.aspx?ctl=GoogleAnalytics"><img id="GoogleAnalytics" alt="GoogleAnalytics" src="" icon='event-viewer.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Administration/Default.aspx?ctl=GoogleAnalytics">Google Analytics</a></span>
            <br/>
            <span class="text">&nbsp;</span>       
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Administration/Default.aspx?ctl=SearchEngine"><img id="search-engine" alt="search engine" src="" icon='search-engine.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Administration/Default.aspx?ctl=SearchEngine">Search Engine</a></span>
            <br/>
            <span class="text">&nbsp;</span>       
        </td>
    </tr>
    <tr style="height: 57px;">
        <td valign="top">
           <!--<img id="Img1" alt="diagnostics" border="0" src="" icon="page-right.png" onclick="callFileManager()" style="cursor:pointer;"  />-->
        </td>
        <td valign="top">
           <!--<span class="title" id="lnkFileManager" onclick="callFileManager()" style="cursor:pointer;">File Manager</span>-->
           
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            <a href="/Meanstream/Portal/Stores/Default2.aspx"><img id="stores" alt="stores" src="" icon='page-right.png' border="0" /></a>
        </td>
        <td valign="top">
            <span class="title"><a href="/Meanstream/Portal/Stores/Default.aspx">Showrooms</a></span>
            <br/>
            <span class="text">&nbsp;</span>       
        </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">
            
        </td>
        <td valign="top">
            
            <br/>
            <span class="text">&nbsp;</span>       
        </td>
    </tr>
</table>