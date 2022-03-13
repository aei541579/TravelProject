using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelProject.Models
{
    public class Articel
    {
        public Guid ArticleID { get; set; }
        public Guid UserID { get; set; }
        public string District { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string ArticleContent { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool ViewLimit { get; set; }
        public bool CommentLimit { get; set; }

    }
}