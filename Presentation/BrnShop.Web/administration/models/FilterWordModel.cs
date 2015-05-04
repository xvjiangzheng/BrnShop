using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 筛选词列表模型类
    /// </summary>
    public class FilterWordListModel
    {
        public PageModel PageModel { get; set; }
        public List<FilterWordInfo> FilterWordList { get; set; }
        public string SearchFilterWord { get; set; }
    }

    /// <summary>
    /// 筛选词模型类
    /// </summary>
    public class FilterWordModel
    {
        [Required(ErrorMessage = "匹配词不能为空")]
        [StringLength(125, ErrorMessage = "匹配词长度不能大于125")]
        public string Match { get; set; }

        [Required(ErrorMessage = "替换词不能为空")]
        [StringLength(125, ErrorMessage = "替换词长度不能大于125")]
        public string Replace { get; set; }
    }
}
