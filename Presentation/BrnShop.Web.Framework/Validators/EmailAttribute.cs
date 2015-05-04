using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 邮箱验证属性
    /// </summary>
    public class EmailAttribute : ValidationAttribute, IClientValidatable
    {
        public EmailAttribute()
        {
            ErrorMessage = "不是有效的邮箱";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            else return BrnShop.Core.ValidateHelper.IsEmail(value.ToString());

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new List<ModelClientValidationRule> { 
                new ModelClientValidationRule { 
                    ValidationType = "email", 
                    ErrorMessage = this.ErrorMessage 
                } 
            };
        }
    }
}