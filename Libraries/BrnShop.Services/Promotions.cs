using System;
using System.Text;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 促销活动操作管理类
    /// </summary>
    public partial class Promotions
    {
        /// <summary>
        /// 获得单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static SinglePromotionInfo GetSinglePromotionByPidAndTime(int pid, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetSinglePromotionByPidAndTime(pid, nowTime);
        }

        /// <summary>
        /// 获得单品促销活动商品的购买数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="pmId">单品促销活动id</param>
        /// <returns></returns>
        public static int GetSinglePromotionProductBuyCount(int uid, int pmId)
        {
            return BrnShop.Data.Promotions.GetSinglePromotionProductBuyCount(uid, pmId);
        }

        /// <summary>
        /// 更新单品促销活动的库存
        /// </summary>
        /// <param name="singlePromotionList">单品促销活动列表</param>
        public static void UpdateSinglePromotionStock(List<SinglePromotionInfo> singlePromotionList)
        {
            BrnShop.Data.Promotions.UpdateSinglePromotionStock(singlePromotionList);
        }




        /// <summary>
        /// 买送商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistBuySendProduct(int pmId, int pid)
        {
            return BrnShop.Data.Promotions.IsExistBuySendProduct(pmId, pid);
        }

        /// <summary>
        /// 获得买送促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<BuySendPromotionInfo> GetBuySendPromotionList(int pid, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetBuySendPromotionList(pid, nowTime);
        }

        /// <summary>
        /// 获得买送促销活动
        /// </summary>
        /// <param name="buyCount">购买数量</param>
        /// <param name="pid">商品id</param>
        /// <param name="buyTime">购买时间</param>
        /// <returns></returns>
        public static BuySendPromotionInfo GetBuySendPromotion(int buyCount, int pid, DateTime buyTime)
        {
            BuySendPromotionInfo buySendPromotionInfo = null;
            //获得买送促销活动列表
            List<BuySendPromotionInfo> buySendPromotionList = GetBuySendPromotionList(pid, buyTime);
            //买送促销活动存在时
            if (buySendPromotionList.Count > 0)
            {
                foreach (BuySendPromotionInfo item in buySendPromotionList)
                {
                    if (item.BuyCount <= buyCount)
                        buySendPromotionInfo = item;
                }
            }
            return buySendPromotionInfo;
        }

        /// <summary>
        /// 获得全部买送促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<BuySendPromotionInfo> GetAllBuySendPromotion(DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetAllBuySendPromotion(nowTime);
        }




        /// <summary>
        /// 获得赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static GiftPromotionInfo GetGiftPromotionByPidAndTime(int pid, DateTime buyTime)
        {
            return BrnShop.Data.Promotions.GetGiftPromotionByPidAndTime(pid, buyTime);
        }




        /// <summary>
        /// 赠品是否已经存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        public static bool IsExistGift(int pmId, int giftId)
        {
            return BrnShop.Data.Promotions.IsExistGift(pmId, giftId);
        }

        /// <summary>
        /// 获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtGiftInfo> GetExtGiftList(int pmId)
        {
            return BrnShop.Data.Promotions.GetExtGiftList(pmId);
        }




        /// <summary>
        /// 获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static SuitPromotionInfo GetSuitPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            if (pmId > 0)
                return BrnShop.Data.Promotions.GetSuitPromotionByPmIdAndTime(pmId, nowTime);
            return null;
        }

        /// <summary>
        /// 判断用户是否参加过指定套装促销活动
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static bool IsJoinSuitPromotion(int uid, int pmId)
        {
            return BrnShop.Data.Promotions.IsJoinSuitPromotion(uid, pmId);
        }

        /// <summary>
        /// 获得套装促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<SuitPromotionInfo> GetSuitPromotionList(int pid, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetSuitPromotionList(pid, nowTime);
        }

        /// <summary>
        /// 获得套装促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<KeyValuePair<SuitPromotionInfo, List<ExtSuitProductInfo>>> GetProductAllSuitPromotion(int pid, DateTime nowTime)
        {
            List<KeyValuePair<SuitPromotionInfo, List<ExtSuitProductInfo>>> result = new List<KeyValuePair<SuitPromotionInfo, List<ExtSuitProductInfo>>>();

            List<SuitPromotionInfo> suitPromotionList = GetSuitPromotionList(pid, nowTime);
            if (suitPromotionList.Count > 0)
            {
                StringBuilder pmIdList = new StringBuilder();
                foreach (SuitPromotionInfo suitPromotionInfo in suitPromotionList)
                {
                    pmIdList.AppendFormat("{0},", suitPromotionInfo.PmId);
                }
                List<ExtSuitProductInfo> allExtSuitProduct = GetAllExtSuitProductList(pmIdList.Remove(pmIdList.Length - 1, 1).ToString());

                foreach (SuitPromotionInfo suitPromotionInfo in suitPromotionList)
                {
                    result.Add(new KeyValuePair<SuitPromotionInfo, List<ExtSuitProductInfo>>(suitPromotionInfo, allExtSuitProduct.FindAll(x => x.PmId == suitPromotionInfo.PmId)));
                }
            }

            return result;
        }




        /// <summary>
        /// 套装商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistSuitProduct(int pmId, int pid)
        {
            return BrnShop.Data.Promotions.IsExistSuitProduct(pmId, pid);
        }

        /// <summary>
        /// 获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> GetExtSuitProductList(int pmId)
        {
            return BrnShop.Data.Promotions.GetExtSuitProductList(pmId);
        }

        /// <summary>
        /// 获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> GetAllExtSuitProductList(string pmIdList)
        {
            return BrnShop.Data.Promotions.GetAllExtSuitProductList(pmIdList);
        }




        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullSendPromotionInfo GetFullSendPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetFullSendPromotionByPidAndTime(pid, nowTime);
        }

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullSendPromotionInfo GetFullSendPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetFullSendPromotionByPmIdAndTime(pmId, nowTime);
        }




        /// <summary>
        /// 满赠商品是否存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistFullSendProduct(int pmId, int pid)
        {
            return BrnShop.Data.Promotions.IsExistFullSendProduct(pmId, pid);
        }

        /// <summary>
        /// 判断满赠商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsExistFullSendProduct(int pmId, int pid, int type)
        {
            return BrnShop.Data.Promotions.IsExistFullSendProduct(pmId, pid, type);
        }

        /// <summary>
        /// 获得满赠主商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetFullSendMainProductList(int pageSize, int pageNumber, int pmId, int startPrice, int endPrice, int sortColumn, int sortDirection)
        {
            return BrnShop.Data.Promotions.GetFullSendMainProductList(pageSize, pageNumber, pmId, startPrice, endPrice, sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得满赠主商品数量
        /// </summary>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        public static int GetFullSendMainProductCount(int pmId, int startPrice, int endPrice)
        {
            return BrnShop.Data.Promotions.GetFullSendMainProductCount(pmId, startPrice, endPrice);
        }

        /// <summary>
        /// 获得满赠赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetFullSendPresentList(int pmId)
        {
            return BrnShop.Data.Promotions.GetFullSendPresentList(pmId);
        }




        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullCutPromotionInfo GetFullCutPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetFullCutPromotionByPidAndTime(pid, nowTime);
        }

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullCutPromotionInfo GetFullCutPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetFullCutPromotionByPmIdAndTime(pmId, nowTime);
        }

        /// <summary>
        /// 满减商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistFullCutProduct(int pmId, int pid)
        {
            if (pmId > 0 && pid > 0)
                return BrnShop.Data.Promotions.IsExistFullCutProduct(pmId, pid);
            return false;
        }

        /// <summary>
        /// 获得全部满减促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<FullCutPromotionInfo> GetAllFullCutPromotion(DateTime nowTime)
        {
            return BrnShop.Data.Promotions.GetAllFullCutPromotion(nowTime);
        }




        /// <summary>
        /// 获得满减商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetFullCutProductList(int pageSize, int pageNumber, FullCutPromotionInfo fullCutPromotionInfo, int startPrice, int endPrice, int sortColumn, int sortDirection)
        {
            return BrnShop.Data.Promotions.GetFullCutProductList(pageSize, pageNumber, fullCutPromotionInfo, startPrice, endPrice, sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得满减商品数量
        /// </summary>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        public static int GetFullCutProductCount(FullCutPromotionInfo fullCutPromotionInfo, int startPrice, int endPrice)
        {
            return BrnShop.Data.Promotions.GetFullCutProductCount(fullCutPromotionInfo, startPrice, endPrice);
        }




        /// <summary>
        /// 生成商品的促销信息
        /// </summary>
        /// <param name="singlePromotionInfo">单品促销活动</param>
        /// <param name="buySendPromotionList">买送促销活动列表</param>
        /// <param name="fullSendPromotionInfo">满赠促销活动</param>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        /// <returns></returns>
        public static string GeneratePromotionMsg(SinglePromotionInfo singlePromotionInfo, List<BuySendPromotionInfo> buySendPromotionList, FullSendPromotionInfo fullSendPromotionInfo, FullCutPromotionInfo fullCutPromotionInfo)
        {
            StringBuilder promotionMsg = new StringBuilder();
            //单品促销
            if (singlePromotionInfo != null)
            {
                //折扣类别
                switch (singlePromotionInfo.DiscountType)
                {
                    case 0://折扣
                        promotionMsg.AppendFormat("折扣：{0}折<br/>", singlePromotionInfo.DiscountValue);
                        break;
                    case 1://直降
                        promotionMsg.AppendFormat("直降：{0}元<br/>", singlePromotionInfo.DiscountValue);
                        break;
                    case 2://折后价
                        promotionMsg.AppendFormat("折后价：{0}元<br/>", singlePromotionInfo.DiscountValue);
                        break;
                }

                //积分
                if (singlePromotionInfo.PayCredits > 0)
                    promotionMsg.AppendFormat("赠送{0}：{1}<br/>", Credits.PayCreditName, singlePromotionInfo.PayCredits);

                //优惠劵
                if (singlePromotionInfo.CouponTypeId > 0)
                {
                    CouponTypeInfo couponTypeInfo = Coupons.GetCouponTypeById(singlePromotionInfo.CouponTypeId);
                    if (couponTypeInfo != null)
                        promotionMsg.AppendFormat("赠送优惠劵：{0}<br/>", couponTypeInfo.Name);
                }
            }
            //买送促销
            if (buySendPromotionList != null && buySendPromotionList.Count > 0)
            {
                promotionMsg.Append("买送促销：");
                foreach (BuySendPromotionInfo buySendPromotionInfo in buySendPromotionList)
                    promotionMsg.AppendFormat("买{0}送{1},", buySendPromotionInfo.BuyCount, buySendPromotionInfo.SendCount);
                promotionMsg.Remove(promotionMsg.Length - 1, 1);
                promotionMsg.Append("<br/>");
            }
            //满赠促销
            if (fullSendPromotionInfo != null)
            {
                promotionMsg.Append("满赠促销：");
                promotionMsg.AppendFormat("满{0}元加{1}元<br/>", fullSendPromotionInfo.LimitMoney, fullSendPromotionInfo.AddMoney);
            }
            //满减促销
            if (fullCutPromotionInfo != null)
            {
                promotionMsg.Append("满减促销：");
                promotionMsg.AppendFormat("满{0}元减{1}元,", fullCutPromotionInfo.LimitMoney1, fullCutPromotionInfo.CutMoney1);
                if (fullCutPromotionInfo.LimitMoney2 > 0 && fullCutPromotionInfo.CutMoney2 > 0)
                    promotionMsg.AppendFormat("满{0}元减{1}元,", fullCutPromotionInfo.LimitMoney2, fullCutPromotionInfo.CutMoney2);
                if (fullCutPromotionInfo.LimitMoney3 > 0 && fullCutPromotionInfo.CutMoney3 > 0)
                    promotionMsg.AppendFormat("满{0}元减{1}元,", fullCutPromotionInfo.LimitMoney3, fullCutPromotionInfo.CutMoney3);
                promotionMsg.Remove(promotionMsg.Length - 1, 1);
                promotionMsg.Append("<br/>");
            }

            return promotionMsg.Length > 0 ? promotionMsg.Remove(promotionMsg.Length - 5, 5).ToString() : "";
        }

        /// <summary>
        /// 计算商品的折扣价
        /// </summary>
        /// <param name="shopPrice">商城价格</param>
        /// <param name="singlePromotionInfo">单品促销活动</param>
        /// <returns></returns>
        public static decimal ComputeDiscountPrice(decimal shopPrice, SinglePromotionInfo singlePromotionInfo)
        {
            decimal discountPrice = shopPrice;
            if (singlePromotionInfo != null)
            {
                switch (singlePromotionInfo.DiscountType)
                {
                    case 0://折扣
                        discountPrice = Math.Ceiling(shopPrice * singlePromotionInfo.DiscountValue / 10);
                        break;
                    case 1://直降
                        discountPrice = shopPrice - singlePromotionInfo.DiscountValue;
                        break;
                    case 2://折后价
                        discountPrice = singlePromotionInfo.DiscountValue;
                        break;
                }
            }
            if (discountPrice < 0 || discountPrice > shopPrice)
                discountPrice = shopPrice;

            return discountPrice;
        }
    }
}
