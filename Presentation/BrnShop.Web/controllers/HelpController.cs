using System;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Models;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 帮助控制器类
    /// </summary>
    public partial class HelpController : BaseWebController
    {
        /// <summary>
        /// 问题
        /// </summary>
        public ActionResult Question()
        {
            //问题id
            int id = GetRouteInt("id");
            if (id == 0)
                id = WebHelper.GetQueryInt("id");

            HelpInfo helpInfo = Helps.GetHelpById(id);
            if (helpInfo == null)
                return PromptView("/", "你访问的页面不存在");


            QuestionModel model = new QuestionModel();
            model.HelpInfo = helpInfo;
            model.HelpList = Helps.GetHelpList();
            return View(model);
        }
    }
}
