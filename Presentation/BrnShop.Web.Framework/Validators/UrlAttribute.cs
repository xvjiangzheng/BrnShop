using System;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// url验证属性
    /// </summary>
    public class UrlAttribute : ValidationAttribute
    {
        public UrlAttribute()
        {
            ErrorMessage = "地址必须以\"#\"或\"/\"或\"http://\"或\"https://\"开头";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                string url = value.ToString().TrimStart().ToLower();
                if (url.StartsWith("#") || url.StartsWith("/") || url.StartsWith("http://") || url.StartsWith("https://"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
