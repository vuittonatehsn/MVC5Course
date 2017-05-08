using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class 商品名稱需要加Will字串Attribute : DataTypeAttribute
    {
        public 商品名稱需要加Will字串Attribute() : base(DataType.Text)
        {

           //ErrorMessage = 
        }
        

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string result = value as string;
            return result.Contains("abc");
            //return base.IsValid(value);
        }

    }
}
