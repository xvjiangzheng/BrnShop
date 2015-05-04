using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 比较两个Decimail类型值，其中一个不能小于另一个
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DecimailNotLessAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} 不得小于 {1}";

        public string OtherDeProperty { get; private set; }
        private string OtherDePropertyName { get; set; }

        public DecimailNotLessAttribute(string otherDeProperty, string otherDePropertyName)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherDeProperty))
            {
                throw new ArgumentNullException("otherDeProperty");
            }

            OtherDeProperty = otherDeProperty;
            OtherDePropertyName = otherDePropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherDePropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo otherDeProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherDeProperty);
                object otherDePropertyValue = otherDeProperty.GetValue(validationContext.ObjectInstance, null);

                decimal dtThis = Convert.ToDecimal(value);
                decimal dtOther = Convert.ToDecimal(otherDePropertyValue);

                if (dtThis < dtOther)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}
