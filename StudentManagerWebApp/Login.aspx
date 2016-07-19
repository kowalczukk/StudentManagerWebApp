<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StudentManagerWebApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Witamy w Student Manager!<br />
        <br />
        Proszę się zalogować.</div>
        <asp:Label ID="Label1" runat="server" Text="Login:"></asp:Label>
        <asp:TextBox ID="loginBox" runat="server" Width="150px"></asp:TextBox>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Hasło:"></asp:Label>
            <asp:TextBox ID="passwordBox" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
        </p>
        <asp:Label ID="messageLabel" runat="server" ForeColor="Red" Text="Podany login lub hasło jest niepoprawne." Visible="False"></asp:Label>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Zaloguj" Width="88px" />
        </p>
    </form>
</body>
</html>
