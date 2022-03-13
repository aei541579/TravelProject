<%@ Page Title="" Language="C#" MasterPageFile="~/FrountMain.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="TravelProject.MyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>個人頁面</h2>
    <div>
        <asp:Literal ID="ltlUserAcc" runat="server"></asp:Literal>|
            文章:           
                <asp:Literal ID="ltlArticleCount" runat="server"></asp:Literal>|
            追蹤中:           
                <asp:Literal ID="ltlFollowingCount" runat="server"></asp:Literal>|
            粉絲:           
                <asp:Literal ID="ltlFansCount" runat="server"></asp:Literal>
    </div>
    <asp:PlaceHolder ID="plcPageOwner" runat="server" Visible="false">
        <asp:Button ID="btnChange" runat="server" Text="修改個人資料" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcGuest" runat="server" Visible="true">
        <asp:Button ID="btnFollow" runat="server" Text="追蹤" OnClick="btnFollow_Click" />
    </asp:PlaceHolder>
    <asp:Repeater ID="rptUserPage" runat="server">
        <ItemTemplate>
            <p>
                <a href="ViewArticle.aspx?Post=<%# Eval("ArticleID")  %>" title="">
                    <%# Eval("District")  %>
                </a>
            </p>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
