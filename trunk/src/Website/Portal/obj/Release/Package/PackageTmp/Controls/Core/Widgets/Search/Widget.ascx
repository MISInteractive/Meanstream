<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Widget.ascx.vb" Inherits="Controls_Core_Widgets_Search_Widget" %>
<span>SEARCH RESULTS</span>
<br/><br/>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">     
    <tr>
        <td width="100%" colspan="3">
            <b>Keyword(s):</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnSearch" runat="server" Text="Submit" />
            <br /><br />
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Literal ID="litResults" runat="server"></asp:Literal> 
                    </td>
                    <td>
                        <div align="right">
                            <asp:Literal ID="litShowNav" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Literal ID="litNoResults" runat="server" Visible="false">We did not find any results for your keyword(s) search...</asp:Literal>
            <asp:Repeater ID="rptResults" runat="server">
                <ItemTemplate>
                    <a href="<%# Eval("Fields(0).Value")%>"><%# GetTitle(Eval("Fields(2).Value"))%></a><br />
                    <%# GetDescription(("Fields(2).Value"))%>
                </ItemTemplate>
                <SeparatorTemplate>
                    <br /><br />
                </SeparatorTemplate>
            </asp:Repeater>
            <br />
            <div align="right">
                <asp:Literal ID="litShowNav2" runat="server"></asp:Literal>
            </div>
        </td>
    </tr>
</table>