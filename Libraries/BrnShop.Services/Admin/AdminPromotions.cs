using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台促销活动操作管理类
    /// </summary>
    public partial class AdminPromotions : Promotions
    {
        /// <summary>
        /// 后台获得单品促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static SinglePromotionInfo AdminGetSinglePromotionById(int pmId)
        {
            if (pmId < 1) return null;
            return BrnShop.Data.Promotions.AdminGetSinglePromotionById(pmId);
        }

        /// <summary>
        /// 创建单品促销活动
        /// </summary>
        public static void CreateSinglePromotion(SinglePromotionInfo singlePromotionInfo)
        {
            BrnShop.Data.Promotions.CreateSinglePromotion(singlePromotionInfo);
        }

        /// <summary>
        /// 更新单品促销活动
        /// </summary>
        public static void UpdateSinglePromotion(SinglePromotionInfo singlePromotionInfo)
        {
            BrnShop.Data.Promotions.UpdateSinglePromotion(singlePromotionInfo);
        }

        /// <summary>
        /// 删除单品促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id</param>
        public static void DeleteSinglePromotionByPmId(int[] pmIdList)
        {
            if (pmIdList != null && pmIdList.Length > 0)
                BrnShop.Data.Promotions.DeleteSinglePromotionByPmId(CommonHelper.IntArrayToString(pmIdList));
        }

        /// <summary>
        /// 后台获得单品促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetSinglePromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Promotions.AdminGetSinglePromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得单品促销活动列表搜索条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetSinglePromotionListCondition(int pid, string promotionName, string promotionTime)
        {
            return BrnShop.Data.Promotions.AdminGetSinglePromotionListCondition(pid, promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得单品促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetSinglePromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Promotions.AdminGetSinglePromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得单品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetSinglePromotionCount(string condition)
        {
            return BrnShop.Data.Promotions.AdminGetSinglePromotionCount(condition);
        }

        /// <summary>
        /// 后台判断商品在指定时间段内是否已经存在单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int AdminIsExistSinglePromotion(int pid, DateTime startTime, DateTime endTime)
        {
            return BrnShop.Data.Promotions.AdminIsExistSinglePromotion(pid, startTime, endTime);
        }

        /// <summary>
        /// 获得单品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<string> GetSinglePromotionPidList(string pmIdList)
        {
            return BrnShop.Data.Promotions.GetSinglePromotionPidList(pmIdList);
        }






        /// <summary>
        /// 后台获得买送促销活动
        /// </summary>
        /// <param name="pmId">获得id</param>
        /// <returns></returns>
        public static BuySendPromotionInfo AdminGetBuySendPromotionById(int pmId)
        {
            if (pmId < 1) return null;
            return BrnShop.Data.Promotions.AdminGetBuySendPromotionById(pmId);
        }

        /// <summary>
        /// 创建买送促销活动
        /// </summary>
        public static void CreateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo)
        {
            BrnShop.Data.Promotions.CreateBuySendPromotion(buySendPromotionInfo);
        }

        /// <summary>
        /// 更新买送促销活动
        /// </summary>
        public static void UpdateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo)
        {
            BrnShop.Data.Promotions.UpdateBuySendPromotion(buySendPromotionInfo);
        }

        /// <summary>
        /// 删除买送促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteBuySendPromotionById(int[] pmIdList)
        {
            if (pmIdList != null && pmIdList.Length > 0)
                BrnShop.Data.Promotions.DeleteBuySendPromotionById(CommonHelper.IntArrayToString(pmIdList));
        }

        /// <summary>
        /// 后台获得买送促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetBuySendPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Promotions.AdminGetBuySendPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得买送促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetBuySendPromotionListCondition(string promotionName, string promotionTime)
        {
            return BrnShop.Data.Promotions.AdminGetBuySendPromotionListCondition(promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得买送促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetBuySendPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Promotions.AdminGetBuySendPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得买送促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetBuySendPromotionCount(string condition)
        {
            return BrnShop.Data.Promotions.AdminGetBuySendPromotionCount(condition);
        }




        /// <summary>
        /// 添加买送商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        public static void AddBuySendProduct(int pmId, int pid)
        {
            if (pmId > 0 && pid > 0)
                BrnShop.Data.Promotions.AddBuySendProduct(pmId, pid);
        }

        /// <summary>
        /// 删除买送商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static bool DeleteBuySendProductByRecordId(int[] recordIdList)
        {
            if (recordIdList != null && recordIdList.Length > 0)
                return BrnShop.Data.Promotions.DeleteBuySendProductByRecordId(CommonHelper.IntArrayToString(recordIdList));
            return false;
        }

        /// <summary>
        /// 后台获得买送商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static DataTable AdminGetBuySendProductList(int pageSize, int pageNumber, int pmId, int pid)
        {
            return BrnShop.Data.Promotions.AdminGetBuySendProductList(pageSize, pageNumber, pmId, pid);
        }

        /// <summary>
        /// 后台获得买送商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static int AdminGetBuySendProductCount(int pmId, int pid)
        {
            if (pmId < 1) return 0;
            return BrnShop.Data.Promotions.AdminGetBuySendProductCount(pmId, pid);
        }

        /// <summary>
        /// 获得买送商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static List<string> GetBuySendPidList(string recordIdList)
        {
            return BrnShop.Data.Promotions.GetBuySendPidList(recordIdList);
        }







        /// <summary>
        /// 后台获得赠品促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static GiftPromotionInfo AdminGetGiftPromotionById(int pmId)
        {
            if (pmId < 1) return null;
            return BrnShop.Data.Promotions.AdminGetGiftPromotionById(pmId);
        }

        /// <summary>
        /// 创建赠品促销活动
        /// </summary>
        public static void CreateGiftPromotion(GiftPromotionInfo giftPromotionInfo)
        {
            BrnShop.Data.Promotions.CreateGiftPromotion(giftPromotionInfo);
        }

        /// <summary>
        /// 更新赠品促销活动
        /// </summary>
        public static void UpdateGiftPromotion(GiftPromotionInfo giftPromotionInfo)
        {
            BrnShop.Data.Promotions.UpdateGiftPromotion(giftPromotionInfo);
        }

        /// <summary>
        /// 删除赠品促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteGiftPromotionByPmId(int[] pmIdList)
        {
            if (pmIdList != null && pmIdList.Length > 0)
                BrnShop.Data.Promotions.DeleteGiftPromotionByPmId(CommonHelper.IntArrayToString(pmIdList));
        }

        /// <summary>
        /// 后台获得赠品促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetGiftPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Promotions.AdminGetGiftPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得赠品促销活动列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetGiftPromotionListCondition(int pid, string promotionName, string promotionTime)
        {
            return BrnShop.Data.Promotions.AdminGetGiftPromotionListCondition(pid, promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得赠品促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetGiftPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Promotions.AdminGetGiftPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得赠品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetGiftPromotionCount(string condition)
        {
            return BrnShop.Data.Promotions.AdminGetGiftPromotionCount(condition);
        }

        /// <summary>
        /// 后台判断商品在指定时间段内是否已经存在赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int AdminIsExistGiftPromotion(int pid, DateTime startTime, DateTime endTime)
        {
            return BrnShop.Data.Promotions.AdminIsExistGiftPromotion(pid, startTime, endTime);
        }

        /// <summary>
        /// 获得赠品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<string> GetGiftPromotionPidList(string pmIdList)
        {
            return BrnShop.Data.Promotions.GetGiftPromotionPidList(pmIdList);
        }





        /// <summary>
        /// 添加赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <param name="number">数量</param>
        /// <param name="pid">商品id</param>
        public static void AddGift(int pmId, int giftId, int number, int pid)
        {
            BrnShop.Data.Promotions.AddGift(pmId, giftId, number, pid);
        }

        /// <summary>
        /// 删除赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <returns></returns>
        public static bool DeleteGiftByPmIdAndGiftId(int pmId, int giftId)
        {
            return BrnShop.Data.Promotions.DeleteGiftByPmIdAndGiftId(pmId, giftId);
        }

        /// <summary>
        /// 更新赠品的数量
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        public static bool UpdateGiftNumber(int pmId, int giftId, int number)
        {
            if (number > 0)
                return BrnShop.Data.Promotions.UpdateGiftNumber(pmId, giftId, number);
            return false;
        }

        /// <summary>
        /// 后台获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtGiftInfo> AdminGetExtGiftList(int pmId)
        {
            return BrnShop.Data.Promotions.AdminGetExtGiftList(pmId);
        }







        /// <summary>
        /// 后台获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static SuitPromotionInfo AdminGetSuitPromotionById(int pmId)
        {
            if (pmId < 1) return null;
            return BrnShop.Data.Promotions.AdminGetSuitPromotionById(pmId);
        }

        /// <summary>
        /// 创建套装促销活动
        /// </summary>
        public static int CreateSuitPromotion(SuitPromotionInfo suitPromotionInfo)
        {
            return BrnShop.Data.Promotions.CreateSuitPromotion(suitPromotionInfo);
        }

        /// <summary>
        /// 更新套装促销活动
        /// </summary>
        public static void UpdateSuitPromotion(SuitPromotionInfo suitPromotionInfo)
        {
            BrnShop.Data.Promotions.UpdateSuitPromotion(suitPromotionInfo);
        }

        /// <summary>
        /// 删除套装促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteSuitPromotionById(int[] pmIdList)
        {
            if (pmIdList != null && pmIdList.Length > 0)
                BrnShop.Data.Promotions.DeleteSuitPromotionById(CommonHelper.IntArrayToString(pmIdList));
        }

        /// <summary>
        /// 后台获得套装促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetSuitPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Promotions.AdminGetSuitPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得套装促销活动列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetSuitPromotionListCondition(int pid, string promotionName, string promotionTime)
        {
            return BrnShop.Data.Promotions.AdminGetSuitPromotionListCondition(pid, promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得套装促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetSuitPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Promotions.AdminGetSuitPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得套装促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetSuitPromotionCount(string condition)
        {
            return BrnShop.Data.Promotions.AdminGetSuitPromotionCount(condition);
        }





        /// <summary>
        /// 添加套装商品
        /// </summary>
        public static void AddSuitProduct(int pmId, int pid, int discount, int number)
        {
            if (pmId > 0 && pid > 0 && discount > -1 && number > 0)
                BrnShop.Data.Promotions.AddSuitProduct(pmId, pid, discount, number);
        }

        /// <summary>
        /// 删除套装商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool DeleteSuitProductByPmIdAndPid(int pmId, int pid)
        {
            return BrnShop.Data.Promotions.DeleteSuitProductByPmIdAndPid(pmId, pid);
        }

        /// <summary>
        /// 修改套装商品数量
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        public static bool UpdateSuitProductNumber(int pmId, int pid, int number)
        {
            if (number > 0)
                return BrnShop.Data.Promotions.UpdateSuitProductNumber(pmId, pid, number);
            return false;
        }

        /// <summary>
        /// 修改套装商品折扣
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="discount">折扣</param>
        /// <returns></returns>
        public static bool UpdateSuitProductDiscount(int pmId, int pid, int discount)
        {
            if (discount > 0)
                return BrnShop.Data.Promotions.UpdateSuitProductDiscount(pmId, pid, discount);
            return false;
        }

        /// <summary>
        /// 后台获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> AdminGetExtSuitProductList(int pmId)
        {
            return BrnShop.Data.Promotions.AdminGetExtSuitProductList(pmId);
        }

        /// <summary>
        /// 后台获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> AdminGetAllExtSuitProductList(string pmIdList)
        {
            return BrnShop.Data.Promotions.AdminGetAllExtSuitProductList(pmIdList);
        }





        /// <summary>
        /// 后台获得满赠促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static FullSendPromotionInfo AdminGetFullSendPromotionById(int pmId)
        {
            if (pmId < 1) return null;
            return BrnShop.Data.Promotions.AdminGetFullSendPromotionById(pmId);
        }

        /// <summary>
        /// 创建满赠促销活动
        /// </summary>
        public static void CreateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo)
        {
            BrnShop.Data.Promotions.CreateFullSendPromotion(fullSendPromotionInfo);
        }

        /// <summary>
        /// 更新满赠促销活动
        /// </summary>
        public static void UpdateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo)
        {
            BrnShop.Data.Promotions.UpdateFullSendPromotion(fullSendPromotionInfo);
        }

        /// <summary>
        /// 删除满赠促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteFullSendPromotionById(int[] pmIdList)
        {
            if (pmIdList != null && pmIdList.Length > 0)
                BrnShop.Data.Promotions.DeleteFullSendPromotionById(CommonHelper.IntArrayToString(pmIdList));
        }

        /// <summary>
        /// 后台获得满赠促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetFullSendPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Promotions.AdminGetFullSendPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得满赠促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetFullSendPromotionListCondition(string promotionName, string promotionTime)
        {
            return BrnShop.Data.Promotions.AdminGetFullSendPromotionListCondition(promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得满赠促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetFullSendPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Promotions.AdminGetFullSendPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得满赠促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetFullSendPromotionCount(string condition)
        {
            return BrnShop.Data.Promotions.AdminGetFullSendPromotionCount(condition);
        }




        /// <summary>
        /// 添加满赠商品
        /// </summary>
        public static void AddFullSendProduct(int pmId, int pid, int type)
        {
            if (pmId > 0 && pid > 0 && (type == 0 || type == 1))
                BrnShop.Data.Promotions.AddFullSendProduct(pmId, pid, type);
        }

        /// <summary>
        /// 删除满赠商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public static bool DeleteFullSendProductByRecordId(int[] recordIdList)
        {
            if (recordIdList != null && recordIdList.Length > 0)
                return BrnShop.Data.Promotions.DeleteFullSendProductByRecordId(CommonHelper.IntArrayToString(recordIdList));
            return false;
        }

        /// <summary>
        /// 后台获得满赠商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static DataTable AdminGetFullSendProductList(int pageSize, int pageNumber, int pmId, int type)
        {
            return BrnShop.Data.Promotions.AdminGetFullSendProductList(pageSize, pageNumber, pmId, type);
        }

        /// <summary>
        /// 后台获得满赠商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static int AdminGetFullSendProductCount(int pmId, int type)
        {
            return BrnShop.Data.Promotions.AdminGetFullSendProductCount(pmId, type);
        }

        /// <summary>
        /// 获得满赠商品列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static DataTable GetFullSendProductList(string recordIdList)
        {
            return BrnShop.Data.Promotions.GetFullSendProductList(recordIdList);
        }






        /// <summary>
        /// 后台获得满减促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static FullCutPromotionInfo AdminGetFullCutPromotionById(int pmId)
        {
            if (pmId < 1) return null;
            return BrnShop.Data.Promotions.AdminGetFullCutPromotionById(pmId);
        }

        /// <summary>
        /// 创建满减促销活动
        /// </summary>
        public static void CreateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo)
        {
            BrnShop.Data.Promotions.CreateFullCutPromotion(fullCutPromotionInfo);
        }

        /// <summary>
        /// 更新满减促销活动
        /// </summary>
        public static void UpdateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo)
        {
            BrnShop.Data.Promotions.UpdateFullCutPromotion(fullCutPromotionInfo);
        }

        /// <summary>
        /// 删除满减促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteFullCutPromotionById(int[] pmIdList)
        {
            if (pmIdList != null && pmIdList.Length > 0)
                BrnShop.Data.Promotions.DeleteFullCutPromotionById(CommonHelper.IntArrayToString(pmIdList));
        }

        /// <summary>
        /// 后台获得满减促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetFullCutPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Promotions.AdminGetFullCutPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得满减促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetFullCutPromotionListCondition(string promotionName, string promotionTime)
        {
            return BrnShop.Data.Promotions.AdminGetFullCutPromotionListCondition(promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得满减促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetFullCutPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Promotions.AdminGetFullCutPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得满减促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetFullCutPromotionCount(string condition)
        {
            return BrnShop.Data.Promotions.AdminGetFullCutPromotionCount(condition);
        }




        /// <summary>
        /// 添加满减商品
        /// </summary>
        public static void AddFullCutProduct(int pmId, int pid)
        {
            if (pmId > 0 && pid > 0)
                BrnShop.Data.Promotions.AddFullCutProduct(pmId, pid);
        }

        /// <summary>
        /// 删除满减商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public static bool DeleteFullCutProductByRecordId(int[] recordIdList)
        {
            if (recordIdList != null && recordIdList.Length > 0)
                return BrnShop.Data.Promotions.DeleteFullCutProductByRecordId(CommonHelper.IntArrayToString(recordIdList));
            return false;
        }

        /// <summary>
        /// 后台获得满减商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static DataTable AdminGetFullCutProductList(int pageSize, int pageNumber, int pmId)
        {
            return BrnShop.Data.Promotions.AdminGetFullCutProductList(pageSize, pageNumber, pmId);
        }

        /// <summary>
        /// 后台获得满减商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static int AdminGetFullCutProductCount(int pmId)
        {
            return BrnShop.Data.Promotions.AdminGetFullCutProductCount(pmId);
        }

        /// <summary>
        /// 获得满减商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static List<string> GetFullCutPidList(string recordIdList)
        {
            return BrnShop.Data.Promotions.GetFullCutPidList(recordIdList);
        }
    }
}