using System;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台商品咨询控制器类
    /// </summary>
    public partial class ProductConsultController : BaseAdminController
    {
        /// <summary>
        /// 商品咨询类型列表
        /// </summary>
        public ActionResult ProductConsultTypeList()
        {
            ProductConsultTypeListModel model = new ProductConsultTypeListModel()
            {
                ProductConsultTypeList = AdminProductConsults.GetProductConsultTypeList()
            };
            ShopUtils.SetAdminRefererCookie(Url.Action("productconsulttypelist"));
            return View(model);
        }

        /// <summary>
        /// 添加商品咨询类型
        /// </summary>
        [HttpGet]
        public ActionResult AddProductConsultType()
        {
            ProductConsultTypeModel model = new ProductConsultTypeModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加商品咨询类型
        /// </summary>
        [HttpPost]
        public ActionResult AddProductConsultType(ProductConsultTypeModel model)
        {
            if (ModelState.IsValid)
            {
                ProductConsultTypeInfo productConsultTypeInfo = new ProductConsultTypeInfo()
                {
                    Title = model.Title,
                    DisplayOrder = model.DisplayOrder
                };

                AdminProductConsults.CreateProductConsultType(productConsultTypeInfo);
                AddAdminOperateLog("添加商品咨询类型", "添加商品咨询类型,商品咨询类型为:" + model.Title.Trim());
                return PromptView("商品咨询类型添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑商品咨询类型
        /// </summary>
        [HttpGet]
        public ActionResult EditProductConsultType(int consultTypeId = -1)
        {
            ProductConsultTypeInfo productConsultTypeInfo = AdminProductConsults.GetProductConsultTypeById(consultTypeId);
            if (productConsultTypeInfo == null)
                return PromptView("商品咨询类型不存在");

            ProductConsultTypeModel model = new ProductConsultTypeModel();
            model.Title = productConsultTypeInfo.Title;
            model.DisplayOrder = productConsultTypeInfo.DisplayOrder;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑商品咨询类型
        /// </summary>
        [HttpPost]
        public ActionResult EditProductConsultType(ProductConsultTypeModel model, int consultTypeId = -1)
        {
            ProductConsultTypeInfo productConsultTypeInfo = AdminProductConsults.GetProductConsultTypeById(consultTypeId);
            if (productConsultTypeInfo == null)
                return PromptView("商品咨询类型不存在");

            if (ModelState.IsValid)
            {
                productConsultTypeInfo.Title = model.Title;
                productConsultTypeInfo.DisplayOrder = model.DisplayOrder;

                AdminProductConsults.UpdateProductConsultType(productConsultTypeInfo);
                AddAdminOperateLog("修改商品咨询类型", "修改商品咨询类型,商品咨询类型ID为:" + consultTypeId);
                return PromptView("商品咨询类型修改成功");
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除商品咨询类型
        /// </summary>
        public ActionResult DelProductConsultType(int consultTypeId = -1)
        {
            int result = AdminProductConsults.DeleteProductConsultTypeById(consultTypeId);
            if (result == 0)
                return PromptView("删除失败,请先删除此商品咨询类型下的咨询");

            AddAdminOperateLog("删除商品咨询类型", "删除商品咨询类型,商品咨询类型ID为:" + consultTypeId);
            return PromptView("商品咨询类型删除成功");
        }







        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult ProductConsultList(string accountName, string productName, string consultMessage, string consultStartTime, string consultEndTime, string sortColumn, string sortDirection, int pid = -1, int consultTypeId = -1, int pageNumber = 1, int pageSize = 15)
        {
            int uid = AdminUsers.GetUidByAccountName(accountName);

            string condition = AdminProductConsults.AdminGetProductConsultListCondition(consultTypeId, pid, uid, consultMessage, consultStartTime, consultEndTime);
            string sort = AdminProductConsults.AdminGetProductConsultListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProductConsults.AdminGetProductConsultCount(condition));

            ProductConsultListModel model = new ProductConsultListModel()
            {
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductConsultList = AdminProductConsults.AdminGetProductConsultList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                AccountName = accountName,
                Pid = pid,
                ProductName = pid > 0 ? productName : "选择商品",
                ConsultTypeId = consultTypeId,
                ConsultMessage = consultMessage,
                ConsultStartTime = consultStartTime,
                ConsultEndTime = consultEndTime
            };

            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "全部类型", Value = "0" });
            foreach (ProductConsultTypeInfo productConsultTypeInfo in AdminProductConsults.GetProductConsultTypeList())
            {
                itemList.Add(new SelectListItem() { Text = productConsultTypeInfo.Title, Value = productConsultTypeInfo.ConsultTypeId.ToString() });
            }
            ViewData["productConsultTypeList"] = itemList;

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&consultMessage={5}&pid={6}&productName={7}&consultStartTime={8}&consultEndTime={9}&consultTypeId={10}&accountName={11}",
                                                           Url.Action("productconsultlist"),
                                                           pageModel.PageNumber, pageModel.PageSize,
                                                           sortColumn, sortDirection,
                                                           consultMessage,
                                                           pid, productName,
                                                           consultStartTime, consultEndTime,
                                                           consultTypeId, accountName));
            return View(model);
        }

        /// <summary>
        /// 更新商品咨询状态
        /// </summary>
        /// <param name="consultId">商品咨询id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public ActionResult UpdateProductConsultState(int consultId = -1, int state = -1)
        {
            bool result = AdminProductConsults.UpdateProductConsultState(consultId, state);
            if (result)
            {
                AddAdminOperateLog("更新商品咨询状态", "更新商品咨询状态,咨询ID和状态为:" + consultId + "_" + state);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 回复商品咨询
        /// </summary>
        [HttpGet]
        public ActionResult Reply(int consultId = -1)
        {
            ProductConsultInfo productConsultInfo = AdminProductConsults.AdminGetProductConsultById(consultId);
            if (productConsultInfo == null)
                return PromptView("商品咨询不存在");

            ReplyProductConsultModel model = new ReplyProductConsultModel();
            model.ProductConsultInfo = productConsultInfo;
            model.ReplyMessage = productConsultInfo.ReplyMessage;

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 回复商品咨询
        /// </summary>
        [HttpPost]
        public ActionResult Reply(ReplyProductConsultModel model, int consultId = -1)
        {
            ProductConsultInfo productConsultInfo = AdminProductConsults.AdminGetProductConsultById(consultId);
            if (productConsultInfo == null)
                return PromptView("商品咨询不存在");

            if (ModelState.IsValid)
            {
                AdminProductConsults.ReplyProductConsult(consultId, WorkContext.Uid, DateTime.Now, model.ReplyMessage, WorkContext.NickName, WorkContext.IP);
                AddAdminOperateLog("回复商品咨询", "回复商品咨询,商品咨询为:" + consultId);
                return PromptView("商品咨询回复成功");
            }

            model.ProductConsultInfo = productConsultInfo;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除商品咨询
        /// </summary>
        /// <param name="consultIdList">商品咨询id列表</param>
        /// <returns></returns>
        public ActionResult DelProductConsult(int[] consultIdList)
        {
            AdminProductConsults.DeleteProductConsultById(consultIdList);
            AddAdminOperateLog("删除商品咨询", "删除商品咨询,商品咨询ID为:" + CommonHelper.IntArrayToString(consultIdList));
            return PromptView("商品咨询删除成功");
        }
    }
}
