<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" Theme="Meanstream.2011" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Meanstream Portal : Enterprise Content Management</title>
</head>

<body class="login-body">
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager" runat="server" >
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="centeredcontent">
            <table align="center" width="500" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3" bgcolor="#3c3c3c">
                        <div class="spacer1x10" />
                    </td>
                </tr>
                <tr>
                    <td width="1" bgcolor="#3c3c3c">
                        <div class="spacer1x1" />
                    </td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="3">
                                    <div class="spacer10x10" />
                                </td>
                            </tr>
                            <tr>
                                <td width="10">
                                    <div class="spacer10x10" />
                                </td>
                                <td class="black-trans-bg">
                                    <br>
                                    <asp:Panel ID="tblLogin" runat="server">
                                        <table width="440" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div align="left">
                                                    </div>
                                                </td>
                                                <td>
                                                    <div align="right">
                                                        <div class="logo" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br>
                                                    <table border="0" align="center" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="signin">
                                                                <strong>Username:</strong>
                                                            </td>
                                                            <td>
                                                                <div class="spacer10x20" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUsername" Width="275" Height="23" runat="server" BorderColor="White"
                                                                    BorderStyle="Solid" BorderWidth="1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="signin">
                                                                <strong>Password:</strong>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPassword" runat="server" Width="225" Height="23" TextMode="Password"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="signin">
                                                                <asp:LinkButton ID="btnForgotPassword" runat="server" Text="forgot password?"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br>
                                                    <asp:Label ID="lblInvalidLogin" runat="server" Text="" Visible="false" ForeColor="red"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div align="right">
                                                        <asp:ImageButton ID="btnSignin" ImageUrl="~/App_Themes/Meanstream.2011/Images/button-signin.png"
                                                            runat="server" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" Visible="false" >
                                        <UserNameTemplate>
                                        <table width="440" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div align="left">
                                                    </div>
                                                </td>
                                                <td>
                                                    <div align="right">
                                                        <div class="logo" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br>
                                                    <table border="0" align="center" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="signin">
                                                                    <strong>Username:</strong>
                                                                </td>
                                                                <td>
                                                                    <div class="spacer10x20" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="UserName" runat="server" Width="275" Height="23" BorderColor="White" BorderStyle="Solid"
                                                                        BorderWidth="1"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                        ErrorMessage="Username is required." ToolTip="Username is required." ValidationGroup="ctl00$PasswordRecovery1"
                                                                        Visible="False">*</asp:RequiredFieldValidator>
                                                                        <br>
                                                                        <asp:Label ID="FailureText" runat="server" EnableViewState="False" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                </td>
                                                            </tr>  
                                                        </table>
                                                    <br>
                                                    <asp:Label ID="lblInvalidLogin" runat="server" Text="" Visible="false" ForeColor="red"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div align="right">
                                                    <asp:ImageButton ID="SubmitButton" runat="server" ImageUrl="~/App_Themes/Meanstream.2011/Images/button-send.png"
                                                                CommandName="Submit" ValidationGroup="ctl00$PasswordRecovery1" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        </UserNameTemplate>
                                    </asp:PasswordRecovery>
                                    <br>
                                </td>
                                <td width="10">
                                    <div class="spacer10x10" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="spacer10x10" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="1" bgcolor="#3c3c3c">
                        <div class="spacer1x1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" bgcolor="#3c3c3c">
                        <div class="spacer1x10" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</form>
</body>
</html>