using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台分类控制器类
    /// </summary>
    public partial class CategoryController : BaseAdminController
    {
        /// <summary>
        /// 分类列表
        /// </summary>
        public ActionResult CategoryList()
        {
            CategoryListModel model = new CategoryListModel();
            model.CategoryList = AdminCategories.GetCategoryList();
            ShopUtils.SetAdminRefererCookie(Url.Action("categorylist"));
            return View(model);
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        [HttpGet]
        public ViewResult AddCategory()
        {
            CategoryModel model = new CategoryModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        [HttpPost]
        public ActionResult AddCategory(CategoryModel model)
        {
            if (AdminCategories.GetCateIdByName(model.CategroyName) > 0)
                ModelState.AddModelError("CategroyName", "名称已经存在");

            if (model.ParentCateId != 0 && AdminCategories.GetCategoryById(model.ParentCateId) == null)
                ModelState.AddModelError("ParentCateId", "父分类不存在");

            if (ModelState.IsValid)
            {
                CategoryInfo categoryInfo = new CategoryInfo()
                {
                    DisplayOrder = model.DisplayOrder,
                    Name = model.CategroyName,
                    ParentId = model.ParentCateId,
                    PriceRange = CommonHelper.StringArrayToString(CommonHelper.RemoveArrayItem(StringHelper.SplitString(CommonHelper.TBBRTrim(model.PriceRange).Replace("，", ","))))
                };

                AdminCategories.CreateCategory(categoryInfo);
                AddAdminOperateLog("添加分类", "添加分类,分类为:" + model.CategroyName);
                return PromptView("分类添加成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        [HttpGet]
        public ActionResult EditCategory(int cateId = -1)
        {
            CategoryInfo categortInfo = AdminCategories.GetCategoryById(cateId);
            if (categortInfo == null)
                return PromptView("此分类不存在");

            CategoryModel model = new CategoryModel();
            model.CategroyName = categortInfo.Name;
            model.ParentCateId = categortInfo.ParentId;
            model.DisplayOrder = categortInfo.DisplayOrder;
            model.PriceRange = categortInfo.PriceRange;

            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        [HttpPost]
        public ActionResult EditCategory(CategoryModel model, int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("此分类不存在");

            int cateId2 = AdminCategories.GetCateIdByName(model.CategroyName);
            if (cateId2 > 0 && cateId2 != cateId)
                ModelState.AddModelError("CategroyName", "名称已经存在");

            if (model.ParentCateId == categoryInfo.CateId)
                ModelState.AddModelError("ParentCateId", "不能将自己作为父分类");

            if (model.ParentCateId != 0 && AdminCategories.GetCategoryById(model.ParentCateId) == null)
                ModelState.AddModelError("ParentCateId", "父分类不存在");

            if (model.ParentCateId != 0 && AdminCategories.GetChildCategoryList(categoryInfo.CateId, categoryInfo.Layer, true).Exists(x => x.CateId == model.ParentCateId))
                ModelState.AddModelError("ParentCateId", "不能将分类调整到自己的子分类下");

            if (ModelState.IsValid)
            {
                int oldParentId = categoryInfo.ParentId;

                categoryInfo.DisplayOrder = model.DisplayOrder;
                categoryInfo.Name = model.CategroyName;
                categoryInfo.ParentId = model.ParentCateId;
                categoryInfo.PriceRange = CommonHelper.StringArrayToString(CommonHelper.RemoveArrayItem(StringHelper.SplitString(CommonHelper.TBBRTrim(model.PriceRange).Replace("，", ","))));

                AdminCategories.UpdateCategory(categoryInfo, oldParentId);
                AddAdminOperateLog("修改分类", "修改分类,分类ID为:" + cateId);
                return PromptView("商品修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        public ActionResult DelCategory(int cateId = -1)
        {
            int result = AdminCategories.DeleteCategoryById(cateId);
            if (result == -3)
                return PromptView("分类不存在");
            if (result == -2)
                return PromptView("删除失败，请先转移或删除此分类下的子分类");
            if (result == -1)
                return PromptView("删除失败，请先删除此分类下的属性分组");
            if (result == 0)
                return PromptView("删除失败，请先转移或删除此分类下的商品");
            AddAdminOperateLog("删除分类", "删除分类,分类ID为:" + cateId);
            return PromptView("分类删除成功");
        }

        private void Load()
        {
            ViewData["categoryList"] = AdminCategories.GetCategoryList();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }




        /// <summary>
        /// 属性分组列表
        /// </summary>
        public ActionResult AttributeGroupList(int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("分类不存在");

            AttributeGroupListModel model = new AttributeGroupListModel()
            {
                AttributeGroupList = AdminCategories.GetAttributeGroupListByCateId(cateId),
                CateId = cateId,
                CategoryName = categoryInfo.Name
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?cateId={1}", Url.Action("attributegrouplist"), cateId));
            return View(model);
        }

        /// <summary>
        /// 添加属性分组
        /// </summary>
        [HttpGet]
        public ActionResult AddAttributeGroup(int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("分类不存在");

            AttributeGroupModel model = new AttributeGroupModel();
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加属性分组
        /// </summary>
        [HttpPost]
        public ActionResult AddAttributeGroup(AttributeGroupModel model, int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("分类不存在");

            if (AdminCategories.GetAttributeGroupIdByCateIdAndName(cateId, model.AttributeGroupName) > 0)
                ModelState.AddModelError("AttributeGroupName", "名称已经存在");

            if (ModelState.IsValid)
            {
                AttributeGroupInfo attributeGroupInfo = new AttributeGroupInfo()
                {
                    Name = model.AttributeGroupName,
                    CateId = categoryInfo.CateId,
                    DisplayOrder = model.DisplayOrder
                };

                AdminCategories.CreateAttributeGroup(attributeGroupInfo);
                AddAdminOperateLog("添加属性分组", "添加属性分组,属性分组为:" + model.AttributeGroupName);
                return PromptView("属性分组添加成功");
            }
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑属性分组
        /// </summary>
        [HttpGet]
        public ActionResult EditAttributeGroup(int attrGroupId = -1)
        {
            AttributeGroupInfo attributeGroupInfo = AdminCategories.GetAttributeGroupById(attrGroupId);
            if (attributeGroupInfo == null)
                return PromptView("属性分组不存在");

            AttributeGroupModel model = new AttributeGroupModel();
            model.AttributeGroupName = attributeGroupInfo.Name;
            model.DisplayOrder = attributeGroupInfo.DisplayOrder;
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(attributeGroupInfo.CateId);
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑属性分组
        /// </summary>
        [HttpPost]
        public ActionResult EditAttributeGroup(AttributeGroupModel model, int attrGroupId = -1)
        {
            AttributeGroupInfo attributeGroupInfo = AdminCategories.GetAttributeGroupById(attrGroupId);
            if (attributeGroupInfo == null)
                return PromptView("属性分组不存在");

            int attrGroupId2 = AdminCategories.GetAttributeGroupIdByCateIdAndName(attributeGroupInfo.CateId, model.AttributeGroupName);
            if (attrGroupId2 > 0 && attrGroupId2 != attrGroupId)
                ModelState.AddModelError("AttributeGroupName", "名称已经存在");

            if (ModelState.IsValid)
            {
                attributeGroupInfo.Name = model.AttributeGroupName;
                attributeGroupInfo.DisplayOrder = model.DisplayOrder;

                AdminCategories.UpdateAttributeGroup(attributeGroupInfo);
                AddAdminOperateLog("修改属性分组", "修改属性分组,属性分组ID为:" + attrGroupId);
                return PromptView("属性分组修改成功");
            }

            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(attributeGroupInfo.CateId);
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除属性分组
        /// </summary>
        public ActionResult DelAttributeGroup(int attrGroupId = -1)
        {
            int result = AdminCategories.DeleteAttributeGroupById(attrGroupId);
            if (result == 0)
                return PromptView("删除失败，请先删除此属性分组下的属性");
            AddAdminOperateLog("删除属性分组", "删除属性分组,属性分组ID为:" + attrGroupId);
            return PromptView("属性分组删除成功");
        }







        /// <summary>
        /// 属性列表
        /// </summary>
        public ActionResult AttributeList(string sortColumn, string sortDirection, int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("分类不存在");

            string sort = AdminBrands.AdminGetBrandListSort(sortColumn, sortDirection);

            AttributeListModel model = new AttributeListModel()
            {
                AttributeList = AdminCategories.AdminGetAttributeList(cateId, sort),
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CateId = cateId,
                CategoryName = categoryInfo.Name
            };

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?cateId={1}&sortColumn={2}&sortDirection={3}", Url.Action("attributelist"), cateId, sortColumn, sortDirection));
            return View(model);
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        [HttpGet]
        public ActionResult AddAttribute(int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("分类不存在");

            AttributeModel model = new AttributeModel();
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["attributeGroupList"] = GetAttributeGroupSelectList(cateId);
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        [HttpPost]
        public ActionResult AddAttribute(AttributeModel model, int cateId = -1)
        {
            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("分类不存在");

            if (AdminCategories.GetAttrIdByCateIdAndName(cateId, model.AttributName) > 0)
                ModelState.AddModelError("AttributName", "名称已经存在");

            AttributeGroupInfo attributeGroupInfo = AdminCategories.GetAttributeGroupById(model.AttrGroupId);
            if (attributeGroupInfo == null || attributeGroupInfo.CateId != cateId)
                ModelState.AddModelError("AttrGroupId", "属性组不存在");

            if (ModelState.IsValid)
            {
                AttributeInfo attributeInfo = new AttributeInfo();
                attributeInfo.Name = model.AttributName;
                attributeInfo.CateId = cateId;
                attributeInfo.AttrGroupId = model.AttrGroupId;
                attributeInfo.ShowType = model.ShowType;
                attributeInfo.IsFilter = model.IsFilter;
                attributeInfo.DisplayOrder = model.DisplayOrder;

                AdminCategories.CreateAttribute(attributeInfo, attributeGroupInfo);
                AddAdminOperateLog("添加分类属性", "添加分类属性,属性为:" + model.AttributName);
                return PromptView("分类属性添加成功");
            }
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["attributeGroupList"] = GetAttributeGroupSelectList(cateId);
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑属性
        /// </summary>
        [HttpGet]
        public ActionResult EditAttribute(int attrId = -1)
        {
            AttributeInfo attributeInfo = AdminCategories.GetAttributeById(attrId);
            if (attributeInfo == null)
                return PromptView("属性不存在");

            AttributeModel model = new AttributeModel();
            model.AttributName = attributeInfo.Name;
            model.AttrGroupId = attributeInfo.AttrGroupId;
            model.ShowType = attributeInfo.ShowType;
            model.IsFilter = attributeInfo.IsFilter;
            model.DisplayOrder = attributeInfo.DisplayOrder;

            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(attributeInfo.CateId);
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["attributeGroupList"] = GetAttributeGroupSelectList(categoryInfo.CateId);
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑属性
        /// </summary>
        [HttpPost]
        public ActionResult EditAttribute(AttributeModel model, int attrId = -1)
        {
            AttributeInfo attributeInfo = AdminCategories.GetAttributeById(attrId);
            if (attributeInfo == null)
                return PromptView("属性不存在");

            int attrId2 = AdminCategories.GetAttrIdByCateIdAndName(attributeInfo.CateId, model.AttributName);
            if (attrId2 > 0 && attrId2 != attrId)
                ModelState.AddModelError("AttributName", "名称已经存在");

            AttributeGroupInfo attributeGroupInfo = AdminCategories.GetAttributeGroupById(model.AttrGroupId);
            if (attributeGroupInfo == null || attributeGroupInfo.CateId != attributeInfo.CateId)
                ModelState.AddModelError("AttrGroupId", "属性组不存在");

            if (ModelState.IsValid)
            {
                attributeInfo.Name = model.AttributName;
                attributeInfo.AttrGroupId = model.AttrGroupId;
                attributeInfo.IsFilter = model.IsFilter;
                attributeInfo.ShowType = model.ShowType;
                attributeInfo.DisplayOrder = model.DisplayOrder;

                AdminCategories.UpdateAttribute(attributeInfo);
                AddAdminOperateLog("编辑分类属性", "编辑分类属性,分类属性ID为:" + attrId);
                return PromptView("分类属性修改成功");
            }

            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(attributeInfo.CateId);
            ViewData["cateId"] = categoryInfo.CateId;
            ViewData["categoryName"] = categoryInfo.Name;
            ViewData["attributeGroupList"] = GetAttributeGroupSelectList(categoryInfo.CateId);
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        public ActionResult DelAttribute(int attrId)
        {
            int result = AdminCategories.DeleteAttributeById(attrId);
            if (result == 0)
                return PromptView("删除失败，请先删除此属性下的属性值");
            AddAdminOperateLog("删除商品属性", "删除商品属性,属性ID为:" + attrId);
            return PromptView("商品属性删除成功");
        }

        /// <summary>
        /// 获得分类的属性及其值json列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public ContentResult AAndVJsonList(int cateId = -1)
        {
            return Content(AdminCategories.GetCategoryAAndVListJsonCache(cateId));
        }

        private List<SelectListItem> GetAttributeGroupSelectList(int cateId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<AttributeGroupInfo> attributeGroupList = AdminCategories.GetAttributeGroupListByCateId(cateId);
            itemList.Add(new SelectListItem() { Text = "请选择分类", Value = "0" });
            foreach (AttributeGroupInfo attributeGroupInfo in attributeGroupList)
            {
                itemList.Add(new SelectListItem() { Text = attributeGroupInfo.Name, Value = attributeGroupInfo.AttrGroupId.ToString() });
            }
            return itemList;
        }







        /// <summary>
        /// 属性值列表
        /// </summary>
        public ActionResult AttributeValueList(int attrId = -1)
        {
            AttributeInfo attributeInfo = AdminCategories.GetAttributeById(attrId);
            if (attributeInfo == null)
                return PromptView("属性不存在");

            AttributeValueListModel model = new AttributeValueListModel()
            {
                AttributeValueList = AdminCategories.GetAttributeSelectValueListByAttrId(attrId),
                AttrId = attributeInfo.AttrId,
                AttributeName = attributeInfo.Name,
                CateId = attributeInfo.CateId
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?attrId={1}", Url.Action("attributevaluelist"), attrId));
            return View(model);
        }

        /// <summary>
        /// 添加属性值
        /// </summary>
        [HttpGet]
        public ActionResult AddAttributeValue(int attrId = -1)
        {
            AttributeInfo attributeInfo = AdminCategories.GetAttributeById(attrId);
            if (attributeInfo == null)
                return PromptView("属性不存在");

            AttributeValueModel model = new AttributeValueModel();
            ViewData["attrId"] = attributeInfo.AttrId;
            ViewData["attributeName"] = attributeInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加属性值
        /// </summary>
        [HttpPost]
        public ActionResult AddAttributeValue(AttributeValueModel model, int attrId = -1)
        {
            AttributeInfo attributeInfo = AdminCategories.GetAttributeById(attrId);
            if (attributeInfo == null)
                ModelState.AddModelError("AttributName", "属性不存在");

            if (AdminCategories.GetAttributeValueIdByAttrIdAndValue(attrId, model.AttrValue) > 0)
                ModelState.AddModelError("AttributName", "值已经存在");


            if (ModelState.IsValid)
            {
                AttributeGroupInfo attributeGroupInfo = AdminCategories.GetAttributeGroupById(attributeInfo.AttrGroupId);
                AttributeValueInfo attributeValueInfo = new AttributeValueInfo();

                attributeValueInfo.AttrId = attributeInfo.AttrId;
                attributeValueInfo.AttrName = attributeInfo.Name;
                attributeValueInfo.AttrDisplayOrder = attributeInfo.DisplayOrder;
                attributeValueInfo.AttrShowType = attributeInfo.ShowType;

                attributeValueInfo.AttrGroupId = attributeGroupInfo.AttrGroupId;
                attributeValueInfo.AttrGroupName = attributeGroupInfo.Name;
                attributeValueInfo.AttrGroupDisplayOrder = attributeGroupInfo.DisplayOrder;

                attributeValueInfo.AttrValue = model.AttrValue;
                attributeValueInfo.IsInput = 0;
                attributeValueInfo.AttrValueDisplayOrder = model.DisplayOrder;

                AdminCategories.CreateAttributeValue(attributeValueInfo);
                AddAdminOperateLog("添加属性值", "添加属性值,属性值为:" + model.AttrValue);
                return PromptView("属性值添加成功");
            }
            ViewData["attrId"] = attributeInfo.AttrId;
            ViewData["attributeName"] = attributeInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑属性值
        /// </summary>
        [HttpGet]
        public ActionResult EditAttributeValue(int attrValueId = -1)
        {
            AttributeValueInfo attributeValueInfo = AdminCategories.GetAttributeValueById(attrValueId);
            if (attributeValueInfo == null)
                return PromptView("属性值不存在");
            if (attributeValueInfo.IsInput == 1)
                return PromptView("输入型属性值不能修改");

            AttributeValueModel model = new AttributeValueModel();
            model.AttrValue = attributeValueInfo.AttrValue;
            model.DisplayOrder = attributeValueInfo.AttrValueDisplayOrder;

            AttributeInfo attributeInfo = Categories.GetAttributeById(attributeValueInfo.AttrId);
            ViewData["attrId"] = attributeInfo.AttrId;
            ViewData["attributeName"] = attributeInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑属性值
        /// </summary>
        [HttpPost]
        public ActionResult EditAttributeValue(AttributeValueModel model, int attrValueId = 0)
        {
            AttributeValueInfo attributeValueInfo = AdminCategories.GetAttributeValueById(attrValueId);
            if (attributeValueInfo == null)
                return PromptView("属性值不存在");
            if (attributeValueInfo.IsInput == 1)
                return PromptView("输入型属性值不能修改");

            int attrValueId2 = AdminCategories.GetAttributeValueIdByAttrIdAndValue(attributeValueInfo.AttrId, model.AttrValue);
            if (attrValueId2 > 0 && attrValueId2 != attrValueId)
                ModelState.AddModelError("AttrValue", "值已经存在");

            if (ModelState.IsValid)
            {
                attributeValueInfo.AttrValue = model.AttrValue;
                attributeValueInfo.AttrValueDisplayOrder = model.DisplayOrder;
                AdminCategories.UpdateAttributeValue(attributeValueInfo);
                AddAdminOperateLog("修改属性值", "修改属性值,属性值ID为:" + attrValueId);
                return PromptView("属性值修改成功");
            }

            AttributeInfo attributeInfo = Categories.GetAttributeById(attributeValueInfo.AttrId);
            ViewData["attrId"] = attributeInfo.AttrId;
            ViewData["attributeName"] = attributeInfo.Name;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        public ActionResult DelAttributeValue(int attrValueId = -1)
        {
            int result = AdminCategories.DeleteAttributeValueById(attrValueId);
            if (result == -2)
                return PromptView("删除失败，此属性值不存在");
            if (result == -1)
                return PromptView("删除失败，此属性值不存在");
            if (result == 0)
                return PromptView("删除失败，请先删除此属性值下的商品");
            AddAdminOperateLog("删除属性值", "删除属性值,属性值ID为:" + attrValueId);
            return PromptView("属性值删除成功");
        }
    }
}

