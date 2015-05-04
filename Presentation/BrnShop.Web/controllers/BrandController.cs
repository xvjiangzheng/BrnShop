using System;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

using BrnShop.Web.Models;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 品牌控制器类
    /// </summary>
    public partial class BrandController : BaseWebController
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            string brandName = WebHelper.GetQueryString("brandName");
            int page = WebHelper.GetQueryInt("page");

            if (!SecureHelper.IsSafeSqlString(brandName))
                return PromptView(WorkContext.UrlReferrer, "您搜索的品牌不存在");

            PageModel pageModel = new PageModel(10, page, Brands.GetBrandCount(brandName));
            BrandListModel model = new BrandListModel()
            {
                PageModel = pageModel,
                BrandName = brandName,
                BrandList = Brands.GetBrandList(pageModel.PageSize, pageModel.PageNumber, brandName)
            };

            return View(model);
        }
    }
}
