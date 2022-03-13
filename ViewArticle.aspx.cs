using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelProject.Models;
using TravelProject.Managers;

namespace TravelProject
{
    public partial class ViewArticle : System.Web.UI.Page
    {
        private ArticleManager _mgr = new ArticleManager();
        private AccountManager _accMgr = new AccountManager();
        private UserActiveManager _uaMgr = new UserActiveManager();
        private static UserAccount _account;
        private static Guid _articleID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string articleIDText = this.Request.QueryString["Post"];
            if (!Guid.TryParse(articleIDText, out _articleID))
                Response.Redirect("index.aspx", true);

            _account = HttpContext.Current.Session["UserAccount"] as UserAccount;
            InitComment(_articleID);
            InitLike(_articleID);
            InitCollect();

            //取得本文的發布者
            Articel article = _mgr.GetArticle(_articleID);
            string UserAccount = _accMgr.GetAccount(article.UserID);
            if (article.ViewLimit || _account.UserID == article.UserID)
            {
                this.ltlUser.Text = UserAccount;
                this.ltlDistrict.Text = article.District;
                this.ltlArtContent.Text = article.ArticleContent;
                this.ltlCreateDate.Text = article.CreateTime.ToString("yyyy-MM-dd HH:mm");
            }
            if (_account.UserID == article.UserID)
                this.plcEdit.Visible = true;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect($"PostArticle.aspx?Post={_articleID}&Editor={_account.UserID}");
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {
            if (!_uaMgr.isLiked(_articleID, _account.UserID))
                _uaMgr.PressLike(_articleID, _account.UserID);
            else
                _uaMgr.DisLike(_articleID, _account.UserID);
            InitLike(_articleID);
        }

        protected void btnCollect_Click(object sender, EventArgs e)
        {
            if (!_uaMgr.isCollected(_articleID, _account.UserID))
                _uaMgr.PressCollect(_articleID, _account.UserID);
            
            else
                _uaMgr.DisCollect(_articleID, _account.UserID);
            InitCollect();
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            Comment comment = new Comment()
            {
                UserID = _account.UserID,
                ArticleID = _articleID,
                CommentContent = this.txtComment.Text.Trim()
            };
            _uaMgr.LeaveComment(comment);
            this.txtComment.Text = null;
            InitComment(_articleID);
        }
        private void InitComment(Guid articleID)
        {
            List<Comment> commentList = _uaMgr.GetCommentList(articleID);
            this.rptComment.DataSource = commentList;
            this.rptComment.DataBind();
            this.ltlCommentCount.Text = commentList.Count.ToString() + "則留言";            
        }
        private void InitLike(Guid articleID)
        {
            List<string> likeAccount = _uaMgr.GetLikeList(articleID);
            this.lblLikeCount.Text = likeAccount.Count.ToString();
            if (!_uaMgr.isLiked(_articleID, _account.UserID))
                this.btnLike.Text = "讚";
            else
                this.btnLike.Text = "取消讚";
        }
        private void InitCollect()
        {
            if (!_uaMgr.isCollected(_articleID, _account.UserID))
                this.btnCollect.Text = "收藏";
            else
                this.btnCollect.Text = "取消收藏";
        }

       
    }
}
