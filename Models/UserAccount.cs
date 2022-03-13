using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelProject.Models
{
    public class UserAccount
    {
        public Guid UserID { get; set; }
        public string Account { get; set; }
        public string PWD { get; set; }
        public DateTime CreateDate { get; set; }
        public bool AccountStates { get; set; }


    }
}