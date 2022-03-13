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
    public partial class PostArticle : System.Web.UI.Page
    {
        private ArticleManager _mgr = new ArticleManager();
        private AccountManager _AccMgr = new AccountManager();
        private static UserAccount _account;
        private static Guid _articleID;
        private static bool _isCreateMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            _account = HttpContext.Current.Session["UserAccount"] as UserAccount;
            string articleIDText = this.Request.QueryString["Post"];
            string UserIDText = this.Request.QueryString["Editor"];
            if (articleIDText == null && UserIDText == null)
            {
                _isCreateMode = true;
                InitCreateMode();
            }
            else if(!IsPostBack)
            {
                if (!Guid.TryParse(articleIDText, out _articleID))
                    Response.Redirect("index.aspx", true);
                //比對session和querySrting的UserID是否一致
                if (string.Compare(UserIDText, _account.UserID.ToString()) != 0)
                    Response.Redirect("index.aspx", true);

                Articel articel = _mgr.GetArticle(_articleID);
                if (articel != null)
                {
                    //比對session和Article資料庫中的UserID是否一致
                    if (articel.UserID == _account.UserID)
                    {
                        _isCreateMode = false;
                        InitEditMode(articel);
                    }
                }
                else
                    Response.Redirect("index.aspx", true);
            }
        }
        private void InitEditMode(Articel articel)
        {
            this.txtDistrict.Text = articel.District;
            this.txtContent.Text = articel.ArticleContent;
        }
        private void InitCreateMode()
        {

        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Articel articel = new Articel()
            {
                UserID = _account.UserID,
                District = this.txtDistrict.Text.Trim(),
                ArticleContent = this.txtContent.Text,
                ViewLimit = Convert.ToBoolean(this.ddlViewLimit.SelectedValue),
                CommentLimit = Convert.ToBoolean(this.ddlCommemtLimit.SelectedValue)
            };
            if (_isCreateMode)
            {
                articel.ArticleID = Guid.NewGuid();
                _mgr.CreateArticle(articel);
            }
            else
            {
                articel.ArticleID = _articleID;
                _mgr.UpdateArticle(articel);
            }
            Response.Redirect($"ViewArticle.aspx?Post={articel.ArticleID}");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}