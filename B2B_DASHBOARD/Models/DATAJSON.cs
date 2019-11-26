using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Models
{
    public class  DATAJSON<T> where T : class
    {
        public List<T> data { get; set; }
    }
}