using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 多提交按钮筛选属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        private string _name = "";
        private string _argument="";

        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var key = ButtonKeyFrom(controllerContext);
            var keyIsValid = IsValid(key);

            if (keyIsValid)
            {
                UpdateValueProviderIn(controllerContext, ValueFrom(key));
            }

            return keyIsValid;
        }

        private string ButtonKeyFrom(ControllerContext controllerContext)
        {
            var keys = controllerContext.HttpContext.Request.Params.AllKeys;
            return keys.FirstOrDefault(KeyStartsWithButtonName);
        }

        private static bool IsValid(string key)
        {
            return key != null;
        }

        private static string ValueFrom(string key)
        {
            var parts = key.Split(":".ToCharArray());
            return parts.Length < 2 ? null : parts[1];
        }

        private void UpdateValueProviderIn(ControllerContext controllerContext, string value)
        {
            if (string.IsNullOrEmpty(Argument)) return;
            controllerContext.RouteData.Values[this.Argument] = value;
            //controllerContext.Controller.ValueProvider[Argument] = new ValueProviderResult(value, value, null);
        }

        private bool KeyStartsWithButtonName(string key)
        {
            return key.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
