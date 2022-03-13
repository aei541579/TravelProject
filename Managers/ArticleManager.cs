using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelProject.Models;
using TravelProject.Helpers;
using System.Data.SqlClient;

namespace TravelProject.Managers
{
    public class ArticleManager
    {
        public Articel GetArticle(Guid articleID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Articles]
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

                        Articel article = new Articel();
                        if (reader.Read())
                        {
                            article.ArticleID = (Guid)reader["ArticleID"];
                            article.UserID = (Guid)reader["UserID"];
                            article.District = reader["District"] as string;
                            article.ArticleContent = reader["ArticleContent"] as string;
                            article.CreateTime = (DateTime)reader["CreateTime"];
                            article.ViewLimit = Convert.ToBoolean(reader["ViewLimit"]);
                            article.CommentLimit = Convert.ToBoolean(reader["CommentLimit"]);
                        }
                        return article;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("ArticleManager.GetArticle", ex);
                throw;
            }
        }
        public List<Articel> GetArticleList(Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT *
                     FROM [Articles]
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
                            Articel article = new Articel();
                            article.ArticleID = (Guid)reader["ArticleID"];
                            article.District = reader["District"] as string;
                            article.ArticleContent = reader["ArticleContent"] as string;
                            article.CreateTime = (DateTime)reader["CreateTime"];
                            articleList.Add(article);
                        }
                        return articleList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("ArticleManager.GetArticleList", ex);
                throw;
            }
        }
        public void CreateArticle(Articel articel)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO [Articles] 
                        (ArticleID, UserID, District, ArticleContent, ViewLimit, CommentLimit)
                    VALUES  
                        (@ArticleID, @UserID, @District, @ArticleContent, @ViewLimit, @CommentLimit) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ArticleID", articel.ArticleID);
                        command.Parameters.AddWithValue("@UserID", articel.UserID);
                        command.Parameters.AddWithValue("@District", articel.District);
                        command.Parameters.AddWithValue("@ArticleContent", articel.ArticleContent);
                        command.Parameters.AddWithValue("@ViewLimit", articel.ViewLimit);
                        command.Parameters.AddWithValue("@CommentLimit", articel.CommentLimit);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("ArticleManager.CreateArticle", ex);
                throw;
            }
        }
        public void UpdateArticle(Articel articel)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE [Articles] 
                    SET District = @District, 
                        ArticleContent = @ArticleContent, 
                        ViewLimit = @ViewLimit, 
                        CommentLimit = @CommentLimit, 
                        UpdateTime = @UpdateTime 
                    WHERE ArticleID = @ArticleID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();                        
                        command.Parameters.AddWithValue("@ArticleID", articel.ArticleID);
                        command.Parameters.AddWithValue("@District", articel.District);
                        command.Parameters.AddWithValue("@ArticleContent", articel.ArticleContent);
                        command.Parameters.AddWithValue("@ViewLimit", articel.ViewLimit);
                        command.Parameters.AddWithValue("@CommentLimit", articel.CommentLimit);
                        command.Parameters.AddWithValue("@UpdateTime", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("ArticleManager.UpdateArticle", ex);
                throw;
            }
        }


    }
}