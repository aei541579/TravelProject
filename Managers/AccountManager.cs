using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelProject.Helpers;
using TravelProject.Models;

namespace TravelProject.Managers
{
    public class AccountManager
    {
        public bool TryLogin(string account, string password)
        {
            bool isAccRight = false;
            bool isPwdRight = false;

            UserAccount User = this.GetAccount(account);
            if (User == null)
                return false;
            if (string.Compare(User.Account, account) == 0)
                isAccRight = true;
            if (string.Compare(User.PWD, password) == 0)
                isPwdRight = true;

            bool result = (isAccRight && isPwdRight);

            if (result)
            {
                User.PWD = null;
                HttpContext.Current.Session["UserAccount"] = User;
            }
            return result;
        }
        public bool IsLogin()
        {
            UserAccount account = GetCurrentUser();
            return (account != null);
        }
        public void Logout()
        {
            HttpContext.Current.Session.Remove("UserAccount");
        }
        public UserAccount GetCurrentUser()
        {
            UserAccount account = HttpContext.Current.Session["UserAccount"] as UserAccount;
            return account;
        }
        public UserAccount GetAccount(string account)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM [UserAccounts]
                    WHERE Account = @account ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@account", account);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            UserAccount User = BuildUserAccount(reader);
                            return User;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.GetAccount", ex);
                throw;
            }
        }
        public string GetAccount(Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM [UserAccounts]
                    WHERE UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            UserAccount User = BuildUserAccount(reader);
                            return User.Account;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.GetAccount", ex);
                throw;
            }
        }

        public void CreateAccount(UserAccount User)
        {
            if (GetAccount(User.Account) != null)
                throw new Exception("已存在相同帳號");

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [UserAccounts] (UserID, Account, PWD, CreateDate,AccountStates)
                    VALUES (@UserID, @Account, @PWD, @CreateDate, @AccountStates) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", Guid.NewGuid());
                        command.Parameters.AddWithValue("@Account", User.Account);
                        command.Parameters.AddWithValue("@PWD", User.PWD);
                        command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        command.Parameters.AddWithValue("@AccountStates", true);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.CreateAccount", ex);
                throw;
            }
        }
        private static UserAccount BuildUserAccount(SqlDataReader reader)
        {
            return new UserAccount()
            {
                UserID = (Guid)reader["UserID"],
                Account = reader["Account"] as string,
                PWD = reader["PWD"] as string,
                CreateDate = (DateTime)reader["CreateDate"],
                AccountStates = (bool)reader["AccountStates"]
            };
        }



    }
}