<%@ Page Title="" Language="C#" MasterPageFile="~/FrountMain.Master" AutoEventWireup="true" CodeBehind="PostArticle.aspx.cs" Inherits="TravelProject.PostArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>發布文章</h2>
    <div>地點
        <asp:TextBox ID="txtDistrict" runat="server"></asp:TextBox>
    </div>
    <div>上傳圖片
        <asp:FileUpload ID="fuPhoto" runat="server" />
    </div>
    <div>文章內容
        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>瀏覽權限
        <asp:DropDownList ID="ddlViewLimit" runat="server">
            <asp:ListItem Text="公開" Value="true" Selected="True" ></asp:ListItem>
            <asp:ListItem Text="只限本人" Value="false"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>留言權限
        <asp:DropDownList ID="ddlCommemtLimit" runat="server">
            <asp:ListItem Text="開放" Value="true" Selected="True" ></asp:ListItem>
            <asp:ListItem Text="不開放" Value="false"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnPost" runat="server" Text="發布文章" OnClick="btnPost_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="取消發布" OnClick="btnCancel_Click" />
</asp:Content>
