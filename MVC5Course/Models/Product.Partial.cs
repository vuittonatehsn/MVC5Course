namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請打對商品名稱")]
        [MinLength(3), MaxLength(500)]
        [RegularExpression(".*-.*", ErrorMessage = "輸入規則字元")]
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }
        [DisplayName("商品價格")]
        [Required]
        [Range(0, 9999, ErrorMessage = "Enter right number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:0}")]
        public Nullable<decimal> Price { get; set; }
        [DisplayName("商品有效")]
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [DisplayName("商品數量")]
        [Range(0, 100, ErrorMessage = "Enter right number")]
        public Nullable<decimal> Stock { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
