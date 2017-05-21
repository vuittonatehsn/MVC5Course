namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量
        {
            get {
                return OrderLine.Count;
                    
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Price>1000 && Stock > 5)
            {
                yield return new ValidationResult("error", new[] { "price", "stock" });
            }
        }
    }
    
    public partial class ProductMetaData
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請打對商品名稱")]
        [MinLength(3), MaxLength(500)]
        [RegularExpression(".*-.*", ErrorMessage = "輸入規則字元")]
        [DisplayName("商品名稱")]
        [商品名稱需要加Will字串(ErrorMessage ="寫相關的字串拉!!!")]
        public string ProductName { get; set; }
        [DisplayName("商品價格")]
        [Required]
        [Range(0, 9999, ErrorMessage = "Enter right number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }
        [DisplayName("商品有效")]
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [DisplayName("商品數量")]
        [RegularExpression(@"^\d+(.\d{1,2})?$")]
        [Range(-444, 1000, ErrorMessage = "Enter right number")]
        public Nullable<decimal> Stock { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
    }
}
