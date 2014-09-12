<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Widget.ascx.vb" Inherits="Controls_Core_Widgets_SiteMap_Widget" %>
<div>
    <asp:DataList ID="rptTree" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" ItemStyle-CssClass="top-items" DataSourceID="SiteMapDataSource1" OnItemDataBound="rptTree_ItemDataBound">
        <ItemTemplate>
            <a id="hylNode" href='<%# VirtualPathUtility.ToAppRelative(Eval("Url")) %>' runat="server" ><%# Eval("Title") %></a>
            <asp:PlaceHolder ID="phSubTree" runat="server" />   
        </ItemTemplate>
    </asp:DataList>
</div>

<asp:SiteMapDataSource 
    ID="SiteMapDataSource1" 
    runat="server"  
    ShowStartingNode="false" /> 


<asp:Literal ID="litTest" runat="server" Visible="false" /> 