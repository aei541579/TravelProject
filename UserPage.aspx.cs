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
    public partial class MyPage : System.Web.UI.Page
    {
        private ArticleManager _mgr = new ArticleManager();
        private AccountManager _AccMgr = new AccountManager();
        private UserActiveManager _uaMgr = new UserActiveManager();
        private static UserAccount _nowUser;
        private static UserAccount _pageOwner;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ownerAccount = this.Request.QueryString["User"];
            if (!string.IsNullOrWhiteSpace(ownerAccount))
            {
                _pageOwner = _AccMgr.GetAccount(ownerAccount);
                if (_pageOwner != null)
                {
                    this.ltlUserAcc.Text = ownerAccount;
                    InitArticle(_pageOwner.UserID);
                    InitFollower(_pageOwner.UserID);
                    _nowUser = HttpContext.Current.Session["UserAccount"] as UserAccount;
                    if (_pageOwner.UserID == _nowUser.UserID)
                        InitMyPage();
                    else
                        InitGuestMode(_nowUser.UserID, _pageOwner.UserID);
                }
            }
            else
                Response.Redirect("index.aspx");

        }

        protected void btnFollow_Click(object sender, EventArgs e)
        {
            if (!_uaMgr.isFollowed(_nowUser.UserID, _pageOwner.UserID))
                _uaMgr.PressFollow(_nowUser.UserID, _pageOwner.UserID);
            else
                _uaMgr.UnFollow(_nowUser.UserID, _pageOwner.UserID);

            InitGuestMode(_nowUser.UserID, _pageOwner.UserID);
            InitFollower(_pageOwner.UserID);
        }
        private void InitFollower(Guid UserID)
        {
            List<string> followingList = _uaMgr.GetFollowingList(UserID);
            this.ltlFollowingCount.Text = followingList.Count.ToString();
            List<string> fansList = _uaMgr.GetFansList(UserID);
            this.ltlFansCount.Text = fansList.Count.ToString();
        }
        private void InitArticle(Guid UserID)
        {
            List<Articel> articleList = _mgr.GetArticleList(UserID);
            this.rptUserPage.DataSource = articleList;
            this.rptUserPage.DataBind();
            this.ltlArticleCount.Text = articleList.Count.ToString();
        }
        private void InitMyPage()
        {
            this.plcPageOwner.Visible = true;
            this.plcGuest.Visible = false;
        }
        private void InitGuestMode(Guid nowUserID, Guid ownerUserID)
        {
            this.plcPageOwner.Visible = false;
            this.plcGuest.Visible = true;
            if (!_uaMgr.isFollowed(nowUserID, ownerUserID))
                this.btnFollow.Text = "追蹤";
            else
                this.btnFollow.Text = "取消追蹤";
        }
    }
}