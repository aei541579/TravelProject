<%@ Page Title="" Language="C#" MasterPageFile="~/FrountMain.Master" AutoEventWireup="true" CodeBehind="ViewArticle.aspx.cs" Inherits="TravelProject.ViewArticle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>文章頁面</h2>
    <asp:PlaceHolder ID="plcEdit" runat="server" Visible="false">
        <asp:Button ID="btnEdit" runat="server" Text="編輯" OnClick="btnEdit_Click" />
    </asp:PlaceHolder>
    <div>發布者:
        <asp:Literal ID="ltlUser" runat="server"></asp:Literal>
    </div>
    <div>照片:
        <asp:Literal ID="ltlPhoto" runat="server"></asp:Literal>
    </div>
    <div>地點:
        <asp:Literal ID="ltlDistrict" runat="server"></asp:Literal>
    </div>
    <div>內文:
        <asp:Literal ID="ltlArtContent" runat="server"></asp:Literal>
    </div>
    <div>發布日期:
        <asp:Literal ID="ltlCreateDate" runat="server"></asp:Literal>
    </div>
    <div>
        <asp:Button ID="btnLike" runat="server" Text="讚" OnClick="btnLike_Click" />
        <asp:Label ID="lblLikeCount" runat="server" Text="Label"></asp:Label>|
        <asp:Button ID="btnCollect" runat="server" Text="收藏" OnClick="btnCollect_Click" />
        <div>
            <asp:TextBox ID="txtComment" runat="server"></asp:TextBox>
            <asp:Button ID="btnComment" runat="server" Text="留言" OnClick="btnComment_Click" /><br />
            <asp:Literal ID="ltlCommentCount" runat="server"></asp:Literal><br />
            <asp:Repeater ID="rptComment" runat="server">
                <ItemTemplate>
                    @<%# Eval("Account") %>: <%# Eval("CommentContent") %> 發布時間<%# Eval("CreateTime") %>                    
                </ItemTemplate>
                <SeparatorTemplate><hr /></SeparatorTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
