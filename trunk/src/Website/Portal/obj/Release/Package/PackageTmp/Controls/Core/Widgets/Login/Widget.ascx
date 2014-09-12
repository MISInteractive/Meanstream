<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Widget.ascx.vb" Inherits="Controls_Core_Widgets_Login_Widget" %>
<table ID="tblLogin" runat="server" Width="250px">
    <tr>
        <td colspan="2">
            <br>
            <table border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="signin">
                        <strong>Username:</strong>
                    </td>
                    <td>
                       &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" Width="275" runat="server" CssClass="login_textbox"></asp:TextBox>
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
                        <asp:TextBox ID="txtPassword" runat="server" Width="225" TextMode="Password" CssClass="login_textbox"></asp:TextBox>
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
                <asp:Button ID="btnLogin" runat="server" Text="Sign In" /></div>
        </td>
    </tr>
</table>
<asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
    <UserNameTemplate>
        <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
            <tr>
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td colspan="2">
                                <br>
                                <table border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="signin">
                                                <strong>Username:</strong>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" Width="275" CssClass="login_textbox"></asp:TextBox>
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
                                <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Send Password" ValidationGroup="ctl00$PasswordRecovery1" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </UserNameTemplate>
</asp:PasswordRecovery>
