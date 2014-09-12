<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Menu.ascx.vb" Inherits="Meanstream_Controls_Menu" %>
<%@ Register TagPrefix="MeanstreamWebControls" Namespace="Meanstream.Portal.WebControls" Assembly="Meanstream.Portal" %>
<%@ Register Src="~/Meanstream/UI/UserControls/UserMenu.ascx" TagName="UserMenu" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/UI/UserControls/SocialMediaMenu.ascx" TagName="SocialMediaMenu" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/UI/UserControls/HostMenu.ascx" TagName="HostMenu" TagPrefix="Meanstream" %>
<%@ Register Src="~/Meanstream/UI/UserControls/AdministrationMenu.ascx" TagName="AdministrationMenu" TagPrefix="Meanstream" %>

<MeanstreamWebControls:Menu ID="Menu" ChildMenuWidth="600" runat="server" SkinID="Menu" Shadow="true" Type="Horizontal">
    <ChildItems>
        <MeanstreamWebControls:MenuItem ID="Dashboard" runat="server" Text="" NavigateUrl="~/Meanstream/Default.aspx" />
        <MeanstreamWebControls:MenuItem ID="User" runat="server" Text="User Name">
            <ChildItems>
                <MeanstreamWebControls:MenuItem ID="UserMenu" runat="server">
                    <ContentTemplate>
                        <Meanstream:UserMenu ID="UserMenuItem" runat="server" />
                    </ContentTemplate>
                </MeanstreamWebControls:MenuItem>
            </ChildItems>
        </MeanstreamWebControls:MenuItem>
        <MeanstreamWebControls:MenuItem ID="Administration" runat="server" Text="ADMINISTRATION" Roles="Administrator">
            <ChildItems>
                <MeanstreamWebControls:MenuItem ID="AdministrationMenu" runat="server">
                    <ContentTemplate>
                        <Meanstream:AdministrationMenu ID="AdministrationMenuItem" runat="server" />
                    </ContentTemplate>
                </MeanstreamWebControls:MenuItem>
            </ChildItems>
        </MeanstreamWebControls:MenuItem>
        <MeanstreamWebControls:MenuItem ID="Host" runat="server" Text="HOST" Roles="Host">
            <ChildItems>
                <MeanstreamWebControls:MenuItem ID="HostMenu" runat="server">
                    <ContentTemplate>
                        <Meanstream:HostMenu ID="HostMenuItem" runat="server" />
                    </ContentTemplate>
                </MeanstreamWebControls:MenuItem>
            </ChildItems>
        </MeanstreamWebControls:MenuItem>
	<MeanstreamWebControls:MenuItem ID="MenuItem4" runat="server" Text="FURNISHINGS" Visible="true">
            <ChildItems>
                <MeanstreamWebControls:MenuItem>
                <ContentTemplate>
                      <table cellpadding="0" cellspacing="20" width=300 height=100>
                      <tr>
                      <td valign=top style="line-height:30px;"> <span class="title">
                  
                       <a href="/Meanstream/Furnishings/categories/Default.aspx">Categories</a>
                     <br />
                       <a href="/Meanstream/Furnishings/Items/Default.aspx">Items</a>
                      <br />
                       <a href="/Meanstream/Furnishings/Finishes/Default.aspx">Finishes</a>
                      </span>
                      </td>
                      </tr>
                      </table>
                    </ContentTemplate>
                </MeanstreamWebControls:MenuItem>
               
            </ChildItems>
        </MeanstreamWebControls:MenuItem>
	    <MeanstreamWebControls:MenuItem ID="SocialMedia" runat="server" Text="Social Media" Visible="false">
            <ChildItems>
                <MeanstreamWebControls:MenuItem ID="SocialMediaMenu" runat="server">
                    <ContentTemplate>
                        <Meanstream:SocialMediaMenu ID="SocialMediaMenuItem" runat="server" />
                    </ContentTemplate>
                </MeanstreamWebControls:MenuItem>
            </ChildItems>
        </MeanstreamWebControls:MenuItem>
    </ChildItems>
</MeanstreamWebControls:Menu><%--<span align="right" style="width: 100%; z-index: 10000000; top: 5px;"><asp:Literal ID="Site" runat="server"></asp:Literal></span>--%>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var theme = '<%=Page.Theme %>';
        $('img').filter(function () { return $(this).attr('icon'); }).each(function () {
            var value = $(this).attr('icon');
            var themePath = '/App_Themes/' + theme;
            var image = themePath + '/Menu/Icons/' + value;
            $(this).attr('src', image);
        });
    });    
</script>
