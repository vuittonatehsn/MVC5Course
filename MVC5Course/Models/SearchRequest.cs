using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class SearchRequest : IValidatableObject
    {
        public SearchRequest()
        {
            searchKey = "z";
            stockRangeStart = 0;
            stockRangeEnd = 999999;
        }
        public string searchKey { get; set; }
        public int stockRangeStart { get; set; }
        public int stockRangeEnd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (stockRangeEnd < stockRangeStart)
            {
                yield return new ValidationResult("", new string[] { "stockRangStart"});
            }
            //yield return ValidationResult.Success;
 
        }
    }
}