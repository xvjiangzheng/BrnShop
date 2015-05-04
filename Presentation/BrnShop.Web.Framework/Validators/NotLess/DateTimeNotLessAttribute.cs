using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 比较两个DateTime类型值，其中一个不能小于另一个
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateTimeNotLessAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} 不得小于 {1}";

        public string OtherDTProperty { get; private set; }
        private string OtherDTPropertyName { get; set; }

        public DateTimeNotLessAttribute(string otherDTProperty, string otherDTPropertyName)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherDTProperty))
            {
                throw new ArgumentNullException("otherDTProperty");
            }

            OtherDTProperty = otherDTProperty;
            OtherDTPropertyName = otherDTPropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherDTPropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo otherDTProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherDTProperty);
                object otherDTPropertyValue = otherDTProperty.GetValue(validationContext.ObjectInstance, null);

                DateTime dtThis = Convert.ToDateTime(value);
                DateTime dtOther = Convert.ToDateTime(otherDTPropertyValue);

                if (dtThis < dtOther)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}
