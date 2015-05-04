using System;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台优惠劵控制器类
    /// </summary>
    public partial class CouponController : BaseAdminController
    {
        /// <summary>
        /// 优惠劵类型列表
        /// </summary>
        /// <param name="couponTypeName">优惠劵类型名称</param>
        /// <param name="type">类型0代表正在发放，1代表正在使用，-1代表全部</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult CouponTypeList(string couponTypeName, int type = -1, int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminCoupons.AdminGetCouponTypeListCondition(type, couponTypeName);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminCoupons.AdminGetCouponTypeCount(condition));

            CouponTypeListModel model = new CouponTypeListModel()
            {
                CouponTypeList = AdminCoupons.AdminGetCouponTypeList(pageModel.PageSize, pageModel.PageNumber, condition),
                PageModel = pageModel,
                Type = type,
                CouponTypeName = couponTypeName
            };

            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "全部", Value = "-1" });
            itemList.Add(new SelectListItem() { Text = "正在发放", Value = "0" });
            itemList.Add(new SelectListItem() { Text = "正在使用", Value = "1" });
            ViewData["typeList"] = itemList;

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&CouponTypeName={3}&type={4}",
                                                          Url.Action("coupontypelist"),
                                                          pageModel.PageNumber, pageModel.PageSize,
                                                          couponTypeName, type));
            return View(model);
        }

        /// <summary>
        /// 添加优惠劵类型
        /// </summary>
        [HttpGet]
        public ActionResult AddCouponType()
        {
            CouponTypeModel model = new CouponTypeModel();
            LoadCouponType();
            return View(model);
        }

        /// <summary>
        /// 添加优惠劵类型
        /// </summary>
        [HttpPost]
        public ActionResult AddCouponType(CouponTypeModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime nullTime = new DateTime(1970, 1, 1);

                if (model.SendModel == 1 || model.SendModel == 2)
                {
                    model.GetModel = 0;
                    model.SendStartTime = nullTime;
                    model.SendEndTime = nullTime;
                }

                if (model.UseTimeType == 1)
                {
                    model.UseStartTime = nullTime;
                    model.UseEndTime = nullTime;
                }
                else
                {
                    model.UseExpireTime = 0;
                }

                CouponTypeInfo couponTypeInfo = new CouponTypeInfo()
                {
                    Name = model.CouponTypeName,
                    Money = model.Money,
                    Count = model.Count,
                    SendMode = model.SendModel,
                    GetMode = model.GetModel,
                    UseMode = model.UseModel,
                    UserRankLower = model.UserRankLower,
                    OrderAmountLower = model.OrderAmountLower,
                    LimitCateId = model.LimitCateId,
                    LimitBrandId = model.LimitBrandId,
                    LimitProduct = model.LimitProduct,
                    SendStartTime = model.SendStartTime.Value,
                    SendEndTime = model.SendEndTime.Value,
                    UseExpireTime = model.UseExpireTime,
                    UseStartTime = model.UseStartTime.Value,
                    UseEndTime = model.UseEndTime.Value,
                    State = model.State
                };
                AdminCoupons.CreateCouponType(couponTypeInfo);
                AddAdminOperateLog("添加优惠劵类型", "添加优惠劵类型,优惠劵类型为:" + model.CouponTypeName);
                return PromptView("优惠劵类型添加成功");
            }
            LoadCouponType();
            return View(model);
        }

        /// <summary>
        /// 展示优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        public ActionResult ShowCouponType(int couponTypeId = -1)
        {
            CouponTypeInfo couponTypeInfo = AdminCoupons.AdminGetCouponTypeById(couponTypeId);
            if (couponTypeInfo == null)
                return PromptView("优惠劵类型不存在");

            CouponTypeModel model = new CouponTypeModel();
            model.CouponTypeName = couponTypeInfo.Name;
            model.Money = couponTypeInfo.Money;
            model.Count = couponTypeInfo.Count;
            model.SendModel = couponTypeInfo.SendMode;
            model.GetModel = couponTypeInfo.GetMode;
            model.UseModel = couponTypeInfo.UseMode;
            model.UserRankLower = couponTypeInfo.UserRankLower;
            model.OrderAmountLower = couponTypeInfo.OrderAmountLower;
            model.LimitCateId = couponTypeInfo.LimitCateId;
            model.LimitBrandId = couponTypeInfo.LimitBrandId;
            model.LimitProduct = couponTypeInfo.LimitProduct;
            model.UseExpireTime = couponTypeInfo.UseExpireTime;
            model.SendStartTime = couponTypeInfo.SendStartTime;
            model.SendEndTime = couponTypeInfo.SendEndTime;
            model.UseStartTime = couponTypeInfo.UseStartTime;
            model.UseEndTime = couponTypeInfo.UseEndTime;
            model.State = couponTypeInfo.State;

            CategoryInfo categoryInfo = AdminCategories.GetCategoryById(model.LimitCateId);
            if (categoryInfo != null)
                model.LimitCateName = categoryInfo.Name;
            else
                model.LimitCateName = "未限制分类";

            BrandInfo brandInfo = AdminBrands.GetBrandById(model.LimitBrandId);
            if (brandInfo != null)
                model.LimitBrandName = brandInfo.Name;
            else
                model.LimitBrandName = "未限制品牌";

            LoadCouponType();
            return View(model);
        }

        /// <summary>
        /// 改变优惠劵类型状态
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="state">状态</param>
        public ActionResult ChangeCouponTypeState(int couponTypeId = -1, int state = 0)
        {
            if (AdminCoupons.ChangeCouponTypeState(couponTypeId, state))
                return PromptView("更改优惠劵类型状态成功");
            else
                return PromptView("更改优惠劵类型状态失败");

        }

        /// <summary>
        /// 删除优惠劵类型
        /// </summary>
        /// <param name="couponTypeIdList">优惠劵类型id列表</param>
        /// <returns></returns>
        public ActionResult DelCouponType(int[] couponTypeIdList)
        {
            AdminCoupons.DeleteCouponTypeById(couponTypeIdList);
            AddAdminOperateLog("删除优惠劵类型", "删除优惠劵类型,优惠劵类型ID为:" + CommonHelper.IntArrayToString(couponTypeIdList));
            return PromptView("优惠劵类型删除成功");
        }

        private void LoadCouponType()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            foreach (UserRankInfo userRankInfo in AdminUserRanks.GetCustomerUserRankList())
            {
                itemList.Add(new SelectListItem() { Text = userRankInfo.Title, Value = userRankInfo.UserRid.ToString() });
            }
            ViewData["userRankList"] = itemList;

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }




        /// <summary>
        /// 优惠劵商品列表
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public ActionResult CouponProductList(int couponTypeId = -1, int pid = -1, int pageSize = 15, int pageNumber = 1)
        {
            CouponTypeInfo couponTypeInfo = AdminCoupons.AdminGetCouponTypeById(couponTypeId);
            if (couponTypeInfo == null)
                return PromptView("优惠劵类型不存在");
            if (couponTypeInfo.LimitProduct == 0)
                return PromptView("此优惠劵类型没有限制商品");

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminCoupons.AdminGetCouponProductCount(couponTypeId));

            CouponProductListModel model = new CouponProductListModel()
            {
                CouponProductList = AdminCoupons.AdminGetCouponProductList(pageSize, pageNumber, couponTypeId),
                PageModel = pageModel,
                CouponTypeId = couponTypeId
            };

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&couponTypeId={3}",
                                                          Url.Action("couponproductlist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          couponTypeId));
            return View(model);
        }

        /// <summary>
        /// 添加优惠劵商品
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCouponProduct(int couponTypeId = -1, int pid = 1)
        {
            CouponTypeInfo couponTypeInfo = AdminCoupons.AdminGetCouponTypeById(couponTypeId);
            if (couponTypeInfo == null)
                return PromptView("优惠劵类型不存在");
            if (couponTypeInfo.LimitProduct == 0)
                return PromptView("此优惠劵类型没有限制商品");

            PartProductInfo partProductInfo = AdminProducts.AdminGetPartProductById(pid);
            if (partProductInfo == null)
                return PromptView("此商品不存在");

            if (AdminCoupons.IsExistCouponProduct(couponTypeId, pid))
                return PromptView("此商品已经存在");

            AdminCoupons.AddCouponProduct(couponTypeId, pid);
            AddAdminOperateLog("添加优惠劵商品", "添加优惠劵商品,商品为:" + partProductInfo.Name);
            return PromptView("优惠劵商品添加成功");
        }

        /// <summary>
        /// 删除优惠劵商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        [HttpAJAX]
        public ActionResult DelCouponProduct(int recordId)
        {
            bool result = AdminCoupons.DeleteCouponProductByRecordId(new[] { recordId });
            if (result)
            {
                AddAdminOperateLog("删除优惠劵商品", "删除优惠劵商品,商品ID:" + recordId);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 删除优惠劵商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public ActionResult DelCouponProduct(int[] recordIdList)
        {
            AdminCoupons.DeleteCouponProductByRecordId(recordIdList);
            AddAdminOperateLog("删除优惠劵商品", "删除优惠劵商品,商品ID:" + CommonHelper.IntArrayToString(recordIdList));
            return PromptView("优惠劵商品删除成功");
        }





        /// <summary>
        /// 优惠劵列表
        /// </summary>
        /// <param name="sn">编号</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult CouponList(string sn, string accountName, int couponTypeId = -1, int pageNumber = 1, int pageSize = 15)
        {
            int uid = AdminUsers.GetUidByAccountName(accountName);

            string condition = AdminCoupons.AdminGetCouponListCondition(sn, uid, couponTypeId);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminCoupons.AdminGetCouponCount(condition));

            CouponListModel model = new CouponListModel()
            {
                CouponList = AdminCoupons.AdminGetCouponList(pageModel.PageSize, pageModel.PageNumber, condition),
                PageModel = pageModel,
                CouponTypeId = couponTypeId,
                AccountName = accountName,
                SN = sn
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&couponTypeId={3}&sn={4}&accountName={5}",
                                                           Url.Action("couponlist"),
                                                           pageModel.PageNumber, pageModel.PageSize,
                                                           couponTypeId, sn, accountName));
            return View(model);
        }

        /// <summary>
        /// 发放优惠劵
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        [HttpGet]
        public ActionResult SendCoupon(int couponTypeId = -1)
        {
            CouponTypeInfo couponTypeInfo = AdminCoupons.AdminGetCouponTypeById(couponTypeId);
            if (couponTypeInfo == null)
                return PromptView("优惠劵类型不存在");
            if (couponTypeInfo.SendMode != 1)
                return PromptView("此优惠劵类型不能手动发放");

            SendCouponModel model = new SendCouponModel();

            ViewData["surplusCount"] = couponTypeInfo.Count - Coupons.GetSendCouponCount(couponTypeId);
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 发放优惠劵
        /// </summary>
        /// <param name="model">优惠劵发放模型</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendCoupon(SendCouponModel model, int couponTypeId = -1)
        {
            CouponTypeInfo couponTypeInfo = AdminCoupons.AdminGetCouponTypeById(couponTypeId);
            if (couponTypeInfo == null)
                return PromptView("优惠劵类型不存在");
            if (couponTypeInfo.SendMode != 1)
                return PromptView("此优惠劵类型不能手动发放");


            //发放数量
            int sendCount = Coupons.GetSendCouponCount(couponTypeId);
            //剩余数量
            int surplusCount = couponTypeInfo.Count - sendCount;
            //判断是否大于剩余数量
            if (model.Count > surplusCount)
                ModelState.AddModelError("Count", "数量不能大于" + surplusCount + "!");


            if (ModelState.IsValid)
            {
                if (model.UType == 0)
                {
                    int uid = TypeHelper.StringToInt(model.UValue);
                    AdminCoupons.AdminSendCouponToUser(uid, couponTypeId, couponTypeInfo.Money, model.Count, WorkContext.Uid, DateTime.Now, WorkContext.IP);
                    AddAdminOperateLog("按用户id发放优惠劵", string.Format("用户id:{0},优惠劵类型id:{1},名称:{2},发放数量:{3}", model.UValue, couponTypeId, couponTypeInfo.Name, model.Count));
                }
                else if (model.UType == 1)
                {
                    int uid = Users.GetUidByAccountName(model.UValue);
                    AdminCoupons.AdminSendCouponToUser(uid, couponTypeId, couponTypeInfo.Money, model.Count, WorkContext.Uid, DateTime.Now, WorkContext.IP);
                    AddAdminOperateLog("按账户名发放优惠劵", string.Format("账户名:{0},优惠劵类型id:{1},名称:{2},发放数量:{3}", model.UValue, couponTypeId, couponTypeInfo.Name, model.Count));
                }
                else
                {
                    AdminCoupons.AdminBatchGenerateCoupon(couponTypeId, couponTypeInfo.Money, model.Count, WorkContext.Uid, DateTime.Now, WorkContext.IP);
                    AddAdminOperateLog("批量生成优惠劵", string.Format("优惠劵类型id:{0},名称:{1},生成数量:{2}", couponTypeId, couponTypeInfo.Name, model.Count));
                }

                return PromptView("优惠劵发放成功");
            }

            ViewData["surplusCount"] = couponTypeInfo.Count - Coupons.GetSendCouponCount(couponTypeId);
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 删除优惠劵
        /// </summary>
        /// <param name="couponIdList">优惠劵id列表</param>
        /// <returns></returns>
        public ActionResult DelCoupon(int[] couponIdList)
        {
            AdminCoupons.DeleteCouponById(couponIdList);
            AddAdminOperateLog("删除优惠劵", "删除优惠劵,优惠劵ID为:" + CommonHelper.IntArrayToString(couponIdList));
            return PromptView("优惠劵删除成功");
        }
    }
}
