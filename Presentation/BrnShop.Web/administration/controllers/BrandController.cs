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
    /// 后台品牌控制器类
    /// </summary>
    public partial class BrandController : BaseAdminController
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ActionResult List(string brandName, string sortColumn, string sortDirection, int pageSize = 15, int pageNumber = 1)
        {
            string condition = AdminBrands.AdminGetBrandListCondition(brandName);
            string sort = AdminBrands.AdminGetBrandListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminBrands.AdminGetBrandCount(condition));

            BrandListModel model = new BrandListModel()
            {
                BrandList = AdminBrands.AdminGetBrandList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                BrandName = brandName
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&brandName={5}",
                                                          Url.Action("list"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          brandName));
            return View(model);
        }

        /// <summary>
        /// 品牌选择列表
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ContentResult SelectList(string brandName, int pageNumber = 1, int pageSize = 24)
        {
            string condition = AdminBrands.AdminGetBrandListCondition(brandName);
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminBrands.AdminGetBrandCount(condition));

            DataTable brandSelectList = AdminBrands.AdminGetBrandSelectList(pageModel.PageSize, pageModel.PageNumber, condition);

            StringBuilder result = new StringBuilder("{");
            result.AppendFormat("\"totalPages\":\"{0}\",\"pageNumber\":\"{1}\",\"items\":[", pageModel.TotalPages, pageModel.PageNumber);
            foreach (DataRow row in brandSelectList.Rows)
                result.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", row["brandid"], row["name"].ToString().Trim(), "}");

            if (brandSelectList.Rows.Count > 0)
                result.Remove(result.Length - 1, 1);

            result.Append("]}");
            return Content(result.ToString());
        }

        /// <summary>
        /// 添加品牌
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            BrandModel model = new BrandModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加品牌
        /// </summary>
        [HttpPost]
        public ActionResult Add(BrandModel model)
        {
            if (AdminBrands.GetBrandIdByName(model.BrandName) > 0)
                ModelState.AddModelError("BrandName", "名称已经存在");

            if (ModelState.IsValid)
            {
                BrandInfo brandInfo = new BrandInfo()
                {
                    DisplayOrder = model.DisplayOrder,
                    Name = model.BrandName,
                    Logo = model.Logo
                };

                AdminBrands.CreateBrand(brandInfo);
                AddAdminOperateLog("添加品牌", "添加品牌,品牌为:" + model.BrandName);
                return PromptView("品牌添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int brandId = -1)
        {
            BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
            if (brandInfo == null)
                return PromptView("品牌不存在");

            BrandModel model = new BrandModel();
            model.DisplayOrder = brandInfo.DisplayOrder;
            model.BrandName = brandInfo.Name;
            model.Logo = brandInfo.Logo;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        [HttpPost]
        public ActionResult Edit(BrandModel model, int brandId = -1)
        {
            BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
            if (brandInfo == null)
                return PromptView("品牌不存在");

            int brandId2 = AdminBrands.GetBrandIdByName(model.BrandName);
            if (brandId2 > 0 && brandId2 != brandId)
                ModelState.AddModelError("BrandName", "名称已经存在");

            if (ModelState.IsValid)
            {
                brandInfo.DisplayOrder = model.DisplayOrder;
                brandInfo.Name = model.BrandName;
                brandInfo.Logo = model.Logo;

                AdminBrands.UpdateBrand(brandInfo);
                AddAdminOperateLog("修改品牌", "修改品牌,品牌ID为:" + brandId);
                return PromptView("品牌修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除品牌
        /// </summary>
        public ActionResult Del(int brandId = -1)
        {
            int result = AdminBrands.DeleteBrandById(brandId);
            if (result == 0)
                return PromptView("删除失败,请先删除此品牌下的商品");
            AddAdminOperateLog("删除品牌", "删除品牌,品牌ID为:" + brandId);
            return PromptView("品牌删除成功");
        }

        private void Load()
        {
            string allowImgType = string.Empty;
            string[] imgTypeList = StringHelper.SplitString(BSPConfig.ShopConfig.UploadImgType, ",");
            foreach (string imgType in imgTypeList)
                allowImgType += string.Format("*{0};", imgType.ToLower());

            string[] sizeList = StringHelper.SplitString(WorkContext.ShopConfig.BrandThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ViewData["allowImgType"] = allowImgType;
            ViewData["maxImgSize"] = BSPConfig.ShopConfig.UploadImgSize;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
