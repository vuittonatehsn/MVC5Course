using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ProductBatchVM
    {
        public int? ProductId { get; set; }
        public decimal? Stock { get; set; }
        public decimal? Price { get; set; }

    }
}