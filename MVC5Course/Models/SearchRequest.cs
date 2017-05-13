using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class SearchRequest
    {
        public string searchKey { get; set; }
        public int stockRangeStart { get; set; }
        public int stockRangeEnd { get; set; }

    }
}