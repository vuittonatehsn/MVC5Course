using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請打對商品名稱")]
        [MinLength(3), MaxLength(500)]
        [RegularExpression(".*-.*", ErrorMessage = "輸入規則字元")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "Enter right number")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Enter right number")]
        public Nullable<decimal> Stock { get; set; }
    }
}