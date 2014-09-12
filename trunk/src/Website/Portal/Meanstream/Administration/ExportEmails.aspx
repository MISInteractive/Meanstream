<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExportEmails.aspx.vb" Inherits="Meanstream_Core_Administration_Security_Users_ExportEmails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="EmailGrid" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Email" DataField="Email" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
