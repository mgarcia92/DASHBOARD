using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Models
{
    public class USERS
    {
        public string USERNAME { get; set; }
        public string DISPLAYNAME { get; set; }
        public string EMAIL { get; set; }
        public string JOB { get; set; }
        public string DEPARTMENT { get; set; }
        public string MANAGER { get; set; }
        public string ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public int ROL_ID { get; set; }
        public int ACTIVE { get; set; }
    }
}