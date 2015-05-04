using System;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 商品咨询类型列表模型类
    /// </summary>
    public class ProductConsultTypeListModel
    {
        public ProductConsultTypeInfo[] ProductConsultTypeList { get; set; }
    }

    /// <summary>
    /// 商品咨询类型模型类
    /// </summary>
    public class ProductConsultTypeModel
    {
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(15, ErrorMessage = "最多输入15个字")]
        public string Title { get; set; }

        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 商品咨询列表模型类
    /// </summary>
    public class ProductConsultListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public string SortDirection { get; set; }
        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public List<ProductConsultInfo> ProductConsultList { get; set; }
        /// <summary>
        /// 账号名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品咨询类型id
        /// </summary>
        public int ConsultTypeId { get; set; }
        /// <summary>
        /// 咨询信息
        /// </summary>
        public string ConsultMessage { get; set; }
        /// <summary>
        /// 咨询开始时间
        /// </summary>
        public string ConsultStartTime { get; set; }
        /// <summary>
        /// 咨询结束时间
        /// </summary>
        public string ConsultEndTime { get; set; }
    }

    /// <summary>
    /// 回复商品咨询模型类
    /// </summary>
    [Bind(Exclude = "ProductConsultInfo")]
    public class ReplyProductConsultModel
    {
        public ProductConsultInfo ProductConsultInfo { get; set; }

        [Required(ErrorMessage = "回复内容不能为空")]
        [StringLength(100, ErrorMessage = "最多只能输入100个字")]
        public string ReplyMessage { get; set; }
    }
}
