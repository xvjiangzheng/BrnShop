using System;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Models
{
    /// <summary>
    /// 问题模型类
    /// </summary>
    public class QuestionModel
    {
        public HelpInfo HelpInfo { get; set; }
        public List<HelpInfo> HelpList { get; set; }
    }
}