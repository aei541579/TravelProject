using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelProject.Helpers;
using TravelProject.Models;

namespace TravelProject.Managers
{
    public class UserActiveManager
    {
        private AccountManager _accMgr = new AccountManager();
        private ArticleManager _artMgr = new ArticleManager();
        public void LeaveComment(Comment comment)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Comments] 
                        (UserID, ArticleID, CommentContent, CreateTime)
                    VALUES  
                        (@UserID, @ArticleID, @CommentContent, @CreateTime) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ArticleID", comment.ArticleID);
                        command.Parameters.AddWithValue("@UserID", comment.UserID);
                        command.Parameters.AddWithValue("@CommentContent", comment.CommentContent);
                        command.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.LeaveComment", ex);
                throw;
            }
        }
        public void PressLike(Guid articleID, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Likes] 
                        (ArticleID, UserID)
                    VALUES  
                        (@ArticleID, @UserID) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ArticleID", articleID);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.PressLike", ex);
                throw;
            }
        }
        public void DisLike(Guid articleID, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [Likes]
                    WHERE ArticleID = @ArticleID AND UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ArticleID", articleID);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.DisLike", ex);
                throw;
            }
        }

        public void PressFollow(Guid UserID, Guid FansID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [FollowLists] 
                        (UserID , FansID)
                    VALUES  
                        (@UserID, @FansID) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@FansID", FansID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.PressFollow", ex);
                throw;
            }
        }
        public void UnFollow(Guid UserID, Guid FansID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [FollowLists] 
                    WHERE UserID = @UserID AND FansID = @FansID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@FansID", FansID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.UnFollow", ex);
                throw;
            }
        }

        public void PressCollect(Guid articleID, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Collects] 
                        (ArticleID, UserID)
                    VALUES  
                        (@ArticleID, @UserID) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ArticleID", articleID);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.PressCollect", ex);
                throw;
            }
        }
        public void DisCollect(Guid articleID, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  DELETE FROM [Collects]
                    WHERE ArticleID = @ArticleID AND UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ArticleID", articleID);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.DisCollect", ex);
                throw;
            }
        }
        public List<Comment> GetCommentList(Guid articleID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Comments]
                     WHERE ArticleID = @ArticleID 
                     ORDER BY CreateTime ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ArticleID", articleID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<Comment> commentList = new List<Comment>();
                        while (reader.Read())
                        {
                            Guid UserID = (Guid)reader["UserID"];
                            Comment comment = new Comment()
                            {
                                UserID = UserID,
                                CommentContent = reader["CommentContent"] as string,
                                CreateTime = (DateTime)reader["CreateTime"],
                                Account = _accMgr.GetAccount(UserID)
                            };
                            commentList.Add(comment);
                        }
                        return commentList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.GetCommentList", ex);
                throw;
            }
        }
        public List<string> GetLikeList(Guid articleID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Likes]
                     WHERE ArticleID = @ArticleID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ArticleID", articleID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<string> likeAccount = new List<string>();
                        while (reader.Read())
                        {
                            Guid UserID = (Guid)reader["UserID"];
                            string account = _accMgr.GetAccount(UserID);
                            likeAccount.Add(account);
                        }
                        return likeAccount;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.GetLikeList", ex);
                throw;
            }
        }
        public List<Articel> GetCollectList(Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Collects]
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

                        List<Articel> articleList = new List<Articel>();
                        while (reader.Read())
                        {
                            Guid articleID = (Guid)reader["ArticleID"];
                            Articel article = _artMgr.GetArticle(articleID);
                            articleList.Add(article);
                        }
                        return articleList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.GetCollectList", ex);
                throw;
            }
        }
        public List<string> GetFollowingList(Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [FollowLists]
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

                        List<string> FollowingList = new List<string>();
                        while (reader.Read())
                        {
                            Guid FansID = (Guid)reader["FansID"];
                            string account = _accMgr.GetAccount(FansID);
                            FollowingList.Add(account);
                        }
                        return FollowingList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.GetFollowingList", ex);
                throw;
            }
        }
        public List<string> GetFansList(Guid FansID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [FollowLists]
                     WHERE FansID = @FansID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@FansID", FansID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<string> FansList = new List<string>();
                        while (reader.Read())
                        {
                            Guid UserID = (Guid)reader["UserID"];
                            string account = _accMgr.GetAccount(UserID);
                            FansList.Add(account);
                        }
                        return FansList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.GetFansList", ex);
                throw;
            }
        }
        public bool isFollowed(Guid UserID, Guid FansID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [FollowLists]
                     WHERE UserID = @UserID AND FansID = @FansID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@FansID", FansID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.isFollowed", ex);
                throw;
            }
        }

        public bool isLiked(Guid articleID, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Likes]
                     WHERE ArticleID = @ArticleID AND UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ArticleID", articleID);
                        command.Parameters.AddWithValue("@UserID", UserID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.isLiked", ex);
                throw;
            }
        }
        public bool isCollected(Guid articleID, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Collects]
                     WHERE ArticleID = @ArticleID AND UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@ArticleID", articleID);
                        command.Parameters.AddWithValue("@UserID", UserID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("UserActiveManager.isCollected", ex);
                throw;
            }
        }

    }
}