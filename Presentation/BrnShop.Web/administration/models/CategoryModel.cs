using System;
using System.Data;
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
    /// 属性分组列表模型类
    /// </summary>
    public class AttributeGroupListModel
    {
        public List<AttributeGroupInfo> AttributeGroupList { get; set; }
        public int CateId { get; set; }
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// 属性分组模型类
    /// </summary>
    public class AttributeGroupModel
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(10, ErrorMessage = "名称长度不能大于10")]
        public string AttributeGroupName { get; set; }

        /// <summary>
        /// 分组排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 属性列表模型类
    /// </summary>
    public class AttributeListModel
    {
        public DataTable AttributeList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int CateId { get; set; }
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// 属性模型类
    /// </summary>
    public class AttributeModel
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(15, ErrorMessage = "名称长度不能大于15")]
        public string AttributName { get; set; }

        /// <summary>
        /// 分组id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择属性分组")]
        [DisplayName("属性分组")]
        public int AttrGroupId { get; set; }

        /// <summary>
        /// 展示类型
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择展示类型")]
        [DisplayName("展示类型")]
        public int ShowType { get; set; }

        /// <summary>
        /// 是否为筛选属性
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择筛选条件")]
        [DisplayName("筛选条件")]
        public int IsFilter { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 属性值列表模型类
    /// </summary>
    public class AttributeValueListModel
    {
        public List<AttributeValueInfo> AttributeValueList { get; set; }
        public int AttrId { get; set; }
        public string AttributeName { get; set; }
        public int CateId { get; set; }
    }

    /// <summary>
    /// 属性值模型类
    /// </summary>
    public class AttributeValueModel
    {
        /// <summary>
        /// 属性值
        /// </summary>
        [Required(ErrorMessage = "值不能为空")]
        [StringLength(35, ErrorMessage = "值长度不能大于35")]
        public string AttrValue { get; set; }

        /// <summary>
        /// 属性值排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 分类列表模型类
    /// </summary>
    public class CategoryListModel
    {
        public List<CategoryInfo> CategoryList { get; set; }
    }

    /// <summary>
    /// 分类模型类
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(30, ErrorMessage = "名称长度不能大于30")]
        public string CategroyName { get; set; }

        /// <summary>
        /// 父分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public int ParentCateId { get; set; }

        /// <summary>
        /// 分类价格范围
        /// </summary>
        [Required(ErrorMessage = "价格范围不能为空")]
        [StringLength(200, ErrorMessage = "价格范围长度不能大于200")]
        public string PriceRange { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }
}
