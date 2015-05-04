using System;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 邮政编码验证属性
    /// </summary>
    public class ZipCodeAttribute : ValidationAttribute
    {
        public ZipCodeAttribute()
        {
            ErrorMessage = "不是有效的邮政编码";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            else return BrnShop.Core.ValidateHelper.IsZipCode(value.ToString());

        }
    }
}
