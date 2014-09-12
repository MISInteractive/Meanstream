<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SkinPageList.ascx.vb" Inherits="Meanstream_Pages_UserControls_SkinPageList" %>

<span class="content">
                &nbsp;<b><asp:Label ID="Label1" runat="server" Text="PAGES ASSIGNED"></asp:Label></b>
                <br>
                </span>
<table>
    <tr>
        <td>
        &nbsp;
        </td>
        <td>
                
            <table>
                <tr>
                    <td>
                        <span class="content">
                        
                        <asp:Repeater ID="rPageList" runat="server">
                        <ItemTemplate>
                        <a href='Default.aspx?ctl=ViewPage&PageID=<%#Eval("Id")%>'><%# Eval("Name")%></a><br>
                        </ItemTemplate>
                        </asp:Repeater>
                        </span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
