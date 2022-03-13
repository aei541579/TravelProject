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
    public partial class Collection : System.Web.UI.Page
    {
        private UserActiveManager _uamgr = new UserActiveManager();
        private static UserAccount _account;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userIDText = this.Request.QueryString["Collector"];
            if (Guid.TryParse(userIDText, out Guid UserID))
            {
                _account = HttpContext.Current.Session["UserAccount"] as UserAccount;
                if (UserID == _account.UserID)
                {
                    List<Articel> articleList = _uamgr.GetCollectList(UserID);
                    this.rptCollection.DataSource = articleList;
                    this.rptCollection.DataBind();

                }
            }
        }
    }
}