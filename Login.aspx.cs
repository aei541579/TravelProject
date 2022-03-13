﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelProject.Managers;

namespace TravelProject
{    
    public partial class Login : System.Web.UI.Page
    {
        private AccountManager _mgr = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["State"] == "1")
                Response.Write("帳號建立成功，請重新登入");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string account = this.txtAcc.Text.Trim();
            string password = this.txtPWD.Text.Trim();
            if (this._mgr.TryLogin(account, password))
                Response.Redirect("index.aspx");
            else
                this.ltlMsg.Text = "登入失敗，請檢查帳號密碼";
        }
    }
}