<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TravelProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>登入</h2>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal><br />
        帳號<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox><br />
        密碼<asp:TextBox ID="txtPWD" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" /><br />
        <a href="Regester.aspx">點我註冊</a>|
    <a href="RecallPWD.aspx">忘記密碼</a>
    </form>
</body>
</html>
