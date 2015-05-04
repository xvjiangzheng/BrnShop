using System;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Models
{
    /// <summary>
    /// 分类列表模型类
    /// </summary>
    public class CategoryListModel
    {
        public CategoryInfo CategoryInfo { get; set; }
        public List<CategoryInfo> CategoryList { get; set; }
    }
}