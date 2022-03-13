﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelProject.Managers;
using TravelProject.Models;

namespace TravelProject
{
    public partial class Regester : System.Web.UI.Page
    {
        private AccountManager _mgr = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblAccMsg.Text = "";
            this.lblPWDMsg.Text = "";
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string account = this.txtAccount.Text.Trim();
            string password = this.txtPWD.Text.Trim();
            string chkPassword = this.txtChkPWD.Text.Trim();
            if (!CheckAccount(account, out string errorAcc))
                this.lblAccMsg.Text = errorAcc;

            else if (!CheckPWD(password, chkPassword, out string errorPWD))
                this.lblPWDMsg.Text = errorPWD;

            else
            {
                UserAccount user = new UserAccount()
                {
                    Account = account,
                    PWD = password
                };
                _mgr.CreateAccount(user);
                Response.Redirect("Login.aspx?State=1");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        private bool CheckAccount(string account, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                errorMsg = "帳號不可為空白";
                return false;
            }
            else if (_mgr.GetAccount(account) != null)
            {
                errorMsg = "已存在相同帳號";
                return false;
            }
            Regex regex = new Regex(@"^(?!.*[^\x21-\x7e])(?=.*[a-z])(?=.*\d).{5,15}$");
            if (!regex.IsMatch(account))
            {
                errorMsg = "帳號格式不正確";
                return false;
            }
            errorMsg = null;
            return true;
        }
        private bool CheckPWD(string password, string chkPassword, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                errorMsg = "密碼不可為空白";
                return false;
            }
            Regex regex = new Regex(@"^(?!.*[^\x21-\x7e])(?=.*[a-z])(?=.*\d).{8,15}$");
            if (!regex.IsMatch(password))
            {
                errorMsg = "密碼格式不正確";
                return false;
            }
            else if (string.Compare(password, chkPassword, false) != 0)
            {
                errorMsg = "確認密碼不一致";
                return false;
            }
            errorMsg = null;
            return true;
        }
    }
}