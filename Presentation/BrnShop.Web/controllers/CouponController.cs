using System;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Models;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 优惠劵控制器类
    /// </summary>
    public partial class CouponController : BaseWebController
    {
        private static object _locker = new object();//锁对象

        /// <summary>
        /// 领取优惠劵
        /// </summary>
        public ActionResult GetCoupon()
        {
            lock (_locker)
            {
                //判断用户是否登录
                if (WorkContext.Uid < 1)
                    return AjaxResult("login", "请先登录");

                //优惠劵类型id
                int couponTypeId = GetRouteInt("couponTypeId");
                if (couponTypeId == 0)
                    couponTypeId = WebHelper.GetQueryInt("couponTypeId");

                CouponTypeInfo couponTypeInfo = Coupons.GetCouponTypeById(couponTypeId);
                //判断优惠劵类型是否存在
                if (couponTypeInfo == null || couponTypeInfo.SendMode != 0)
                    return AjaxResult("noexist", "优惠劵不存在");
                //判断优惠劵类型是否开始领取
                if (couponTypeInfo.SendStartTime > DateTime.Now)
                    return AjaxResult("unstart", "优惠劵还未开始");
                //判断优惠劵类型是否结束领取
                if (couponTypeInfo.SendEndTime <= DateTime.Now)
                    return AjaxResult("expired", "优惠劵已过期");
                //判断优惠劵类型是否已经领取
                if ((couponTypeInfo.GetMode == 1 && Coupons.GetSendUserCouponCount(WorkContext.Uid, couponTypeId) > 1) || (couponTypeInfo.GetMode == 2 && Coupons.GetTodaySendUserCouponCount(WorkContext.Uid, couponTypeId, DateTime.Now) > 1))
                    return AjaxResult("alreadyget", "优惠劵已经被领取");


                //判断优惠劵是否已经领尽
                int sendCount = Coupons.GetSendCouponCount(couponTypeId);
                if (sendCount >= couponTypeInfo.Count)
                    return AjaxResult("stockout", "优惠劵已领尽");

                string couponSN = Coupons.PullCoupon(WorkContext.PartUserInfo, couponTypeInfo, DateTime.Now, WorkContext.IP);
                return AjaxResult("success", couponSN);
            }
        }

        /// <summary>
        /// 验证优惠劵编号
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCouponSN()
        {
            string couponSN = WebHelper.GetQueryString("couponSN");//优惠劵编号
            if (couponSN.Length == 0)
                return AjaxResult("emptycouponsn", "请输入优惠劵编号");
            else if (couponSN.Length != 16)
                return AjaxResult("errorcouponsn", "优惠劵编号不正确");

            CouponInfo couponInfo = Coupons.GetCouponByCouponSN(couponSN);
            if (couponInfo == null)//不存在
                return AjaxResult("noexist", "优惠劵不存在");
            else if (couponInfo.Oid > 0)//已使用
                return AjaxResult("used", "优惠劵已使用");
            else if (couponInfo.Uid > 0)//已激活
                return AjaxResult("activated", "优惠劵已激活");
            else//未激活
                return AjaxResult("nonactivated", "优惠劵未激活");
        }
    }
}
