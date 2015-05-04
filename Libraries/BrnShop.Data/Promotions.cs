using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 促销活动数据访问类
    /// </summary>
    public partial class Promotions
    {
        private static IPromotionNOSQLStrategy _promotionnosql = BSPData.PromotionNOSQL;//促销活动非关系型数据库
        private static IProductNOSQLStrategy _productnosql = BSPData.ProductNOSQL;//商品非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建SinglePromotionInfo
        /// </summary>
        public static SinglePromotionInfo BuildSinglePromotionFromReader(IDataReader reader)
        {
            SinglePromotionInfo singlePromotionInfo = new SinglePromotionInfo();
            singlePromotionInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            singlePromotionInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            singlePromotionInfo.StartTime1 = TypeHelper.ObjectToDateTime(reader["starttime1"]);
            singlePromotionInfo.EndTime1 = TypeHelper.ObjectToDateTime(reader["endtime1"]);
            singlePromotionInfo.StartTime2 = TypeHelper.ObjectToDateTime(reader["starttime2"]);
            singlePromotionInfo.EndTime2 = TypeHelper.ObjectToDateTime(reader["endtime2"]);
            singlePromotionInfo.StartTime3 = TypeHelper.ObjectToDateTime(reader["starttime3"]);
            singlePromotionInfo.EndTime3 = TypeHelper.ObjectToDateTime(reader["endtime3"]);
            singlePromotionInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            singlePromotionInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            singlePromotionInfo.Name = reader["name"].ToString();
            singlePromotionInfo.Slogan = reader["slogan"].ToString();
            singlePromotionInfo.DiscountType = TypeHelper.ObjectToInt(reader["discounttype"]);
            singlePromotionInfo.DiscountValue = TypeHelper.ObjectToInt(reader["discountvalue"]);
            singlePromotionInfo.CouponTypeId = TypeHelper.ObjectToInt(reader["coupontypeid"]);
            singlePromotionInfo.PayCredits = TypeHelper.ObjectToInt(reader["paycredits"]);
            singlePromotionInfo.IsStock = TypeHelper.ObjectToInt(reader["isstock"]);
            singlePromotionInfo.Stock = TypeHelper.ObjectToInt(reader["stock"]);
            singlePromotionInfo.QuotaLower = TypeHelper.ObjectToInt(reader["quotalower"]);
            singlePromotionInfo.QuotaUpper = TypeHelper.ObjectToInt(reader["quotaupper"]);
            singlePromotionInfo.AllowBuyCount = TypeHelper.ObjectToInt(reader["allowbuycount"]);
            return singlePromotionInfo;
        }

        /// <summary>
        /// 从IDataReader创建BuySendPromotionInfo
        /// </summary>
        public static BuySendPromotionInfo BuildBuySendPromotionFromReader(IDataReader reader)
        {
            BuySendPromotionInfo buySendPromotionInfo = new BuySendPromotionInfo();
            buySendPromotionInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            buySendPromotionInfo.StartTime = TypeHelper.ObjectToDateTime(reader["starttime"]);
            buySendPromotionInfo.EndTime = TypeHelper.ObjectToDateTime(reader["endtime"]);
            buySendPromotionInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            buySendPromotionInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            buySendPromotionInfo.Name = reader["name"].ToString();
            buySendPromotionInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            buySendPromotionInfo.BuyCount = TypeHelper.ObjectToInt(reader["buycount"]);
            buySendPromotionInfo.SendCount = TypeHelper.ObjectToInt(reader["sendcount"]);
            return buySendPromotionInfo;
        }

        /// <summary>
        /// 从IDataReader创建GiftPromotionInfo
        /// </summary>
        public static GiftPromotionInfo BuildGiftPromotionFromReader(IDataReader reader)
        {
            GiftPromotionInfo giftPromotionInfo = new GiftPromotionInfo();
            giftPromotionInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            giftPromotionInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            giftPromotionInfo.StartTime1 = TypeHelper.ObjectToDateTime(reader["starttime1"]);
            giftPromotionInfo.EndTime1 = TypeHelper.ObjectToDateTime(reader["endtime1"]);
            giftPromotionInfo.StartTime2 = TypeHelper.ObjectToDateTime(reader["starttime2"]);
            giftPromotionInfo.EndTime2 = TypeHelper.ObjectToDateTime(reader["endtime2"]);
            giftPromotionInfo.StartTime3 = TypeHelper.ObjectToDateTime(reader["starttime3"]);
            giftPromotionInfo.EndTime3 = TypeHelper.ObjectToDateTime(reader["endtime3"]);
            giftPromotionInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            giftPromotionInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            giftPromotionInfo.Name = reader["name"].ToString();
            giftPromotionInfo.QuotaUpper = TypeHelper.ObjectToInt(reader["quotaupper"]);
            return giftPromotionInfo;
        }

        /// <summary>
        /// 从IDataReader创建ExtGiftInfo
        /// </summary>
        public static ExtGiftInfo BuildExtGiftFromReader(IDataReader reader)
        {
            ExtGiftInfo extGiftInfo = new ExtGiftInfo();

            extGiftInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            extGiftInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            extGiftInfo.Number = TypeHelper.ObjectToInt(reader["number"]);
            extGiftInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            extGiftInfo.PSN = reader["psn"].ToString();
            extGiftInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            extGiftInfo.BrandId = TypeHelper.ObjectToInt(reader["brandid"]);
            extGiftInfo.SKUGid = TypeHelper.ObjectToInt(reader["skugid"]);
            extGiftInfo.Name = reader["name"].ToString();
            extGiftInfo.ShopPrice = TypeHelper.ObjectToDecimal(reader["shopprice"]);
            extGiftInfo.MarketPrice = TypeHelper.ObjectToDecimal(reader["marketprice"]);
            extGiftInfo.CostPrice = TypeHelper.ObjectToDecimal(reader["costprice"]);
            extGiftInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            extGiftInfo.IsBest = TypeHelper.ObjectToInt(reader["isbest"]);
            extGiftInfo.IsHot = TypeHelper.ObjectToInt(reader["ishot"]);
            extGiftInfo.IsNew = TypeHelper.ObjectToInt(reader["isnew"]);
            extGiftInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            extGiftInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);
            extGiftInfo.ShowImg = reader["showimg"].ToString();

            return extGiftInfo;
        }

        /// <summary>
        /// 从IDataReader创建SuitPromotionInfo
        /// </summary>
        public static SuitPromotionInfo BuildSuitPromotionFromReader(IDataReader reader)
        {
            SuitPromotionInfo suitPromotionInfo = new SuitPromotionInfo();
            suitPromotionInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            suitPromotionInfo.StartTime1 = TypeHelper.ObjectToDateTime(reader["starttime1"]);
            suitPromotionInfo.EndTime1 = TypeHelper.ObjectToDateTime(reader["endtime1"]);
            suitPromotionInfo.StartTime2 = TypeHelper.ObjectToDateTime(reader["starttime2"]);
            suitPromotionInfo.EndTime2 = TypeHelper.ObjectToDateTime(reader["endtime2"]);
            suitPromotionInfo.StartTime3 = TypeHelper.ObjectToDateTime(reader["starttime3"]);
            suitPromotionInfo.EndTime3 = TypeHelper.ObjectToDateTime(reader["endtime3"]);
            suitPromotionInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            suitPromotionInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            suitPromotionInfo.Name = reader["name"].ToString();
            suitPromotionInfo.QuotaUpper = TypeHelper.ObjectToInt(reader["quotaupper"]);
            suitPromotionInfo.OnlyOnce = TypeHelper.ObjectToInt(reader["onlyonce"]);
            return suitPromotionInfo;
        }

        /// <summary>
        /// 从IDataReader创建ExtSuitProductInfo
        /// </summary>
        public static ExtSuitProductInfo BuildExtSuitProductFromReader(IDataReader reader)
        {
            ExtSuitProductInfo extSuitProductInfo = new ExtSuitProductInfo();

            extSuitProductInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            extSuitProductInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            extSuitProductInfo.Discount = TypeHelper.ObjectToInt(reader["discount"]);
            extSuitProductInfo.Number = TypeHelper.ObjectToInt(reader["number"]);
            extSuitProductInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            extSuitProductInfo.PSN = reader["psn"].ToString();
            extSuitProductInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            extSuitProductInfo.BrandId = TypeHelper.ObjectToInt(reader["brandid"]);
            extSuitProductInfo.SKUGid = TypeHelper.ObjectToInt(reader["skugid"]);
            extSuitProductInfo.Name = reader["name"].ToString();
            extSuitProductInfo.ShopPrice = TypeHelper.ObjectToDecimal(reader["shopprice"]);
            extSuitProductInfo.MarketPrice = TypeHelper.ObjectToDecimal(reader["marketprice"]);
            extSuitProductInfo.CostPrice = TypeHelper.ObjectToDecimal(reader["costprice"]);
            extSuitProductInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            extSuitProductInfo.IsBest = TypeHelper.ObjectToInt(reader["isbest"]);
            extSuitProductInfo.IsHot = TypeHelper.ObjectToInt(reader["ishot"]);
            extSuitProductInfo.IsNew = TypeHelper.ObjectToInt(reader["isnew"]);
            extSuitProductInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            extSuitProductInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);
            extSuitProductInfo.ShowImg = reader["showimg"].ToString();

            return extSuitProductInfo;
        }

        /// <summary>
        /// 从IDataReader创建FullSendPromotionInfo
        /// </summary>
        public static FullSendPromotionInfo BuildFullSendPromotionFromReader(IDataReader reader)
        {
            FullSendPromotionInfo fullSendPromotionInfo = new FullSendPromotionInfo();
            fullSendPromotionInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            fullSendPromotionInfo.StartTime = TypeHelper.ObjectToDateTime(reader["starttime"]);
            fullSendPromotionInfo.EndTime = TypeHelper.ObjectToDateTime(reader["endtime"]);
            fullSendPromotionInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            fullSendPromotionInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            fullSendPromotionInfo.Name = reader["name"].ToString();
            fullSendPromotionInfo.LimitMoney = TypeHelper.ObjectToInt(reader["limitmoney"]);
            fullSendPromotionInfo.AddMoney = TypeHelper.ObjectToInt(reader["addmoney"]);
            return fullSendPromotionInfo;
        }

        /// <summary>
        /// 从IDataReader创建FullCutPromotionInfo
        /// </summary>
        public static FullCutPromotionInfo BuildFullCutPromotionFromReader(IDataReader reader)
        {
            FullCutPromotionInfo fullCutPromotionInfo = new FullCutPromotionInfo();
            fullCutPromotionInfo.PmId = TypeHelper.ObjectToInt(reader["pmid"]);
            fullCutPromotionInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            fullCutPromotionInfo.StartTime = TypeHelper.ObjectToDateTime(reader["starttime"]);
            fullCutPromotionInfo.EndTime = TypeHelper.ObjectToDateTime(reader["endtime"]);
            fullCutPromotionInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            fullCutPromotionInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            fullCutPromotionInfo.Name = reader["name"].ToString();
            fullCutPromotionInfo.LimitMoney1 = TypeHelper.ObjectToInt(reader["limitmoney1"]);
            fullCutPromotionInfo.CutMoney1 = TypeHelper.ObjectToInt(reader["cutmoney1"]);
            fullCutPromotionInfo.LimitMoney2 = TypeHelper.ObjectToInt(reader["limitmoney2"]);
            fullCutPromotionInfo.CutMoney2 = TypeHelper.ObjectToInt(reader["cutmoney2"]);
            fullCutPromotionInfo.LimitMoney3 = TypeHelper.ObjectToInt(reader["limitmoney3"]);
            fullCutPromotionInfo.CutMoney3 = TypeHelper.ObjectToInt(reader["cutmoney3"]);
            return fullCutPromotionInfo;
        }

        #endregion

        /// <summary>
        /// 后台获得单品促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static SinglePromotionInfo AdminGetSinglePromotionById(int pmId)
        {
            SinglePromotionInfo singlePromotionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetSinglePromotionById(pmId);
            if (reader.Read())
            {
                singlePromotionInfo = BuildSinglePromotionFromReader(reader);
            }

            reader.Close();
            return singlePromotionInfo;
        }

        /// <summary>
        /// 创建单品促销活动
        /// </summary>
        public static void CreateSinglePromotion(SinglePromotionInfo singlePromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateSinglePromotion(singlePromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.DeleteProductSinglePromotion(singlePromotionInfo.Pid);
        }

        /// <summary>
        /// 更新单品促销活动
        /// </summary>
        public static void UpdateSinglePromotion(SinglePromotionInfo singlePromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateSinglePromotion(singlePromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.DeleteProductSinglePromotion(singlePromotionInfo.Pid);
        }

        /// <summary>
        /// 删除单品促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id</param>
        public static void DeleteSinglePromotionByPmId(string pmIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteSinglePromotionByPmId(pmIdList);
            if (_promotionnosql != null)
                _promotionnosql.DeleteProductSinglePromotion(GetSinglePromotionPidList(pmIdList));
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
            return BrnShop.Core.BSPData.RDBS.AdminGetSinglePromotionList(pageSize, pageNumber, condition, sort);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetSinglePromotionListCondition(pid, promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得单品促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetSinglePromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetSinglePromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得单品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetSinglePromotionCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetSinglePromotionCount(condition);
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
            return BrnShop.Core.BSPData.RDBS.AdminIsExistSinglePromotion(pid, startTime, endTime);
        }

        /// <summary>
        /// 获得单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static SinglePromotionInfo GetSinglePromotionByPidAndTime(int pid, DateTime nowTime)
        {
            SinglePromotionInfo singlePromotionInfo = null;

            if (_promotionnosql != null)
            {
                singlePromotionInfo = _promotionnosql.GetSinglePromotionByPidAndTime(pid, nowTime);
                if (singlePromotionInfo == null)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSinglePromotionByPidAndTime(pid, nowTime);
                    if (reader.Read())
                    {
                        singlePromotionInfo = BuildSinglePromotionFromReader(reader);
                    }
                    reader.Close();
                    if (singlePromotionInfo != null)
                        _promotionnosql.CreateProductSinglePromotion(singlePromotionInfo);
                    else
                        _promotionnosql.CreateProductSinglePromotion(new SinglePromotionInfo() { Pid = pid });
                }
                else if (singlePromotionInfo.PmId < 1)
                {
                    singlePromotionInfo = null;
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSinglePromotionByPidAndTime(pid, nowTime);
                if (reader.Read())
                {
                    singlePromotionInfo = BuildSinglePromotionFromReader(reader);
                }
                reader.Close();
            }

            return singlePromotionInfo;
        }

        /// <summary>
        /// 获得单品促销活动商品的购买数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static int GetSinglePromotionProductBuyCount(int uid, int pmId)
        {
            return BrnShop.Core.BSPData.RDBS.GetSinglePromotionProductBuyCount(uid, pmId);
        }

        /// <summary>
        /// 更新单品促销活动的库存
        /// </summary>
        /// <param name="singlePromotionList">单品促销活动列表</param>
        public static void UpdateSinglePromotionStock(List<SinglePromotionInfo> singlePromotionList)
        {
            BrnShop.Core.BSPData.RDBS.UpdateSinglePromotionStock(singlePromotionList);
            if (_promotionnosql != null)
                _promotionnosql.UpdateProductSinglePromotionStock(singlePromotionList);
        }

        /// <summary>
        /// 获得单品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<string> GetSinglePromotionPidList(string pmIdList)
        {
            List<string> singlePromotionPidList = new List<string>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSinglePromotionPidList(pmIdList);
            while (reader.Read())
            {
                singlePromotionPidList.Add(reader["pid"].ToString());
            }
            reader.Close();
            return singlePromotionPidList;
        }






        /// <summary>
        /// 后台获得买送促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static BuySendPromotionInfo AdminGetBuySendPromotionById(int pmId)
        {
            BuySendPromotionInfo buySendPromotionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetBuySendPromotionById(pmId);
            if (reader.Read())
            {
                buySendPromotionInfo = BuildBuySendPromotionFromReader(reader);
            }

            reader.Close();
            return buySendPromotionInfo;
        }

        /// <summary>
        /// 创建买送促销活动
        /// </summary>
        public static void CreateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateBuySendPromotion(buySendPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.ClearAllBuySendPromotion();
        }

        /// <summary>
        /// 更新买送促销活动
        /// </summary>
        public static void UpdateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateBuySendPromotion(buySendPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.ClearAllBuySendPromotion();
        }

        /// <summary>
        /// 删除买送促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteBuySendPromotionById(string pmIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteBuySendPromotionById(pmIdList);
            if (_promotionnosql != null)
                _promotionnosql.ClearAllBuySendPromotion();
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
            return BrnShop.Core.BSPData.RDBS.AdminGetBuySendPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得买送促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetBuySendPromotionListCondition(string promotionName, string promotionTime)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetBuySendPromotionListCondition(promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得买送促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetBuySendPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetBuySendPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得买送促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetBuySendPromotionCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetBuySendPromotionCount(condition);
        }

        /// <summary>
        /// 获得买送促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<BuySendPromotionInfo> GetBuySendPromotionList(int pid, DateTime nowTime)
        {
            List<BuySendPromotionInfo> buySendPromotionList = null;

            if (_promotionnosql != null)
            {
                buySendPromotionList = new List<BuySendPromotionInfo>();

                List<BuySendPromotionInfo> allBuySendPromotion = GetAllBuySendPromotion(nowTime);//获得全部买送促销活动
                if (allBuySendPromotion.Count > 0)//当买送促销活动存在时
                {
                    //获得商品的买送促销活动id列表
                    List<int> productBuySendPromotionIdList = _promotionnosql.GetProductBuySendPromotionIdList(pid);
                    if (productBuySendPromotionIdList == null)
                    {
                        productBuySendPromotionIdList = new List<int>();
                        IDataReader reader = BrnShop.Core.BSPData.RDBS.GetBuySendPromotionList(pid, nowTime);
                        while (reader.Read())
                        {
                            productBuySendPromotionIdList.Add(TypeHelper.ObjectToInt(reader["pmid"]));
                        }
                        reader.Close();
                        _promotionnosql.CreateProductBuySendPromotionIdList(pid, productBuySendPromotionIdList);
                    }

                    //是否有冗余买送促销活动的标识
                    bool flag = false;
                    //循环全部买送促销活动
                    foreach (BuySendPromotionInfo buySendPromotionInfo in allBuySendPromotion)
                    {
                        //如果买送促销活动已经过期,则将冗余标识设为true,并跳出本次循环
                        if (buySendPromotionInfo.EndTime <= nowTime)
                        {
                            flag = true;
                            continue;
                        }

                        if (buySendPromotionInfo.Type == 0)//全场参加
                        {
                            buySendPromotionList.Add(buySendPromotionInfo);
                        }
                        else if (buySendPromotionInfo.Type == 1)//部分商品参加
                        {
                            foreach (int pmId in productBuySendPromotionIdList)
                            {
                                if (pmId == buySendPromotionInfo.PmId)
                                {
                                    buySendPromotionList.Add(buySendPromotionInfo);
                                    break;
                                }
                            }
                        }
                        else if (buySendPromotionInfo.Type == 2)//部分商品不参加
                        {
                            bool isJoin = true;
                            foreach (int pmId in productBuySendPromotionIdList)
                            {
                                if (pmId == buySendPromotionInfo.PmId)
                                {
                                    isJoin = false;
                                    break;
                                }
                            }
                            if (isJoin)
                                buySendPromotionList.Add(buySendPromotionInfo);
                        }
                    }

                    //如果存在冗余买送促销活动则清空全部买送促销活动列表
                    if (flag)
                        _promotionnosql.ClearAllBuySendPromotion();

                    if (buySendPromotionList.Count != productBuySendPromotionIdList.Count)
                    {
                        List<int> pmIdList = new List<int>();
                        foreach (BuySendPromotionInfo item in buySendPromotionList)
                        {
                            pmIdList.Add(item.PmId);
                        }
                        _promotionnosql.CreateProductBuySendPromotionIdList(pid, pmIdList);
                    }
                }
            }
            else
            {
                buySendPromotionList = new List<BuySendPromotionInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetBuySendPromotionList(pid, nowTime);
                while (reader.Read())
                {
                    BuySendPromotionInfo buySendPromotionInfo = BuildBuySendPromotionFromReader(reader);
                    buySendPromotionList.Add(buySendPromotionInfo);
                }
                reader.Close();
            }

            return buySendPromotionList;
        }

        /// <summary>
        /// 获得全部买送促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<BuySendPromotionInfo> GetAllBuySendPromotion(DateTime nowTime)
        {
            List<BuySendPromotionInfo> allBuySendPromotion = null;

            if (_promotionnosql != null)
            {
                allBuySendPromotion = _promotionnosql.GetAllBuySendPromotion();
                if (allBuySendPromotion == null)
                {
                    allBuySendPromotion = new List<BuySendPromotionInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAllBuySendPromotion(nowTime);
                    while (reader.Read())
                    {
                        BuySendPromotionInfo buySendPromotionInfo = BuildBuySendPromotionFromReader(reader);
                        allBuySendPromotion.Add(buySendPromotionInfo);
                    }
                    reader.Close();
                    _promotionnosql.AddAllBuySendPromotion(allBuySendPromotion);
                }
            }
            else
            {
                allBuySendPromotion = new List<BuySendPromotionInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAllBuySendPromotion(nowTime);
                while (reader.Read())
                {
                    BuySendPromotionInfo buySendPromotionInfo = BuildBuySendPromotionFromReader(reader);
                    allBuySendPromotion.Add(buySendPromotionInfo);
                }
                reader.Close();
            }

            return allBuySendPromotion;
        }





        /// <summary>
        /// 添加买送商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        public static void AddBuySendProduct(int pmId, int pid)
        {
            BrnShop.Core.BSPData.RDBS.AddBuySendProduct(pmId, pid);
            if (_promotionnosql != null)
                _promotionnosql.DeletProductBuySendPromotionIdList(pid);
        }

        /// <summary>
        /// 删除买送商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static bool DeleteBuySendProductByRecordId(string recordIdList)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteBuySendProductByRecordId(recordIdList);
            if (_promotionnosql != null)
                _promotionnosql.DeletProductBuySendPromotionIdList(GetBuySendPidList(recordIdList));
            return result;
        }

        /// <summary>
        /// 买送商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistBuySendProduct(int pmId, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistBuySendProduct(pmId, pid);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetBuySendProductList(pageSize, pageNumber, pmId, pid);
        }

        /// <summary>
        /// 后台获得买送商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static int AdminGetBuySendProductCount(int pmId, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetBuySendProductCount(pmId, pid);
        }

        /// <summary>
        /// 获得买送商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static List<string> GetBuySendPidList(string recordIdList)
        {
            List<string> buySendPidList = new List<string>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetBuySendPidList(recordIdList);
            while (reader.Read())
            {
                buySendPidList.Add(reader["pid"].ToString());
            }
            reader.Close();
            return buySendPidList;
        }







        /// <summary>
        /// 后台获得赠品促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static GiftPromotionInfo AdminGetGiftPromotionById(int pmId)
        {
            GiftPromotionInfo giftPromotionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetGiftPromotionById(pmId);
            if (reader.Read())
            {
                giftPromotionInfo = BuildGiftPromotionFromReader(reader);
            }

            reader.Close();
            return giftPromotionInfo;
        }

        /// <summary>
        /// 创建赠品促销活动
        /// </summary>
        public static void CreateGiftPromotion(GiftPromotionInfo giftPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateGiftPromotion(giftPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.DeleteProductGiftPromotion(giftPromotionInfo.Pid);
        }

        /// <summary>
        /// 更新赠品促销活动
        /// </summary>
        public static void UpdateGiftPromotion(GiftPromotionInfo giftPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateGiftPromotion(giftPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.DeleteProductGiftPromotion(giftPromotionInfo.Pid);
        }

        /// <summary>
        /// 删除赠品促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteGiftPromotionByPmId(string pmIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteGiftPromotionByPmId(pmIdList);
            if (_promotionnosql != null)
                _promotionnosql.DeleteProductGiftPromotion(GetGiftPromotionPidList(pmIdList));
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
            return BrnShop.Core.BSPData.RDBS.AdminGetGiftPromotionList(pageSize, pageNumber, condition, sort);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetGiftPromotionListCondition(pid, promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得赠品促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetGiftPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetGiftPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得赠品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetGiftPromotionCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetGiftPromotionCount(condition);
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
            return BrnShop.Core.BSPData.RDBS.AdminIsExistGiftPromotion(pid, startTime, endTime);
        }

        /// <summary>
        /// 获得赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static GiftPromotionInfo GetGiftPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            GiftPromotionInfo giftPromotionInfo = null;

            if (_promotionnosql != null)
            {
                giftPromotionInfo = _promotionnosql.GetGiftPromotionByPidAndTime(pid, nowTime);
                if (giftPromotionInfo == null)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetGiftPromotionByPidAndTime(pid, nowTime);
                    if (reader.Read())
                    {
                        giftPromotionInfo = BuildGiftPromotionFromReader(reader);
                    }
                    reader.Close();
                    if (giftPromotionInfo != null)
                        _promotionnosql.CreateProductGiftPromotion(giftPromotionInfo);
                    else
                        _promotionnosql.CreateProductGiftPromotion(new GiftPromotionInfo() { Pid = pid });
                }
                else if (giftPromotionInfo.PmId < 1)
                {
                    giftPromotionInfo = null;
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetGiftPromotionByPidAndTime(pid, nowTime);
                if (reader.Read())
                {
                    giftPromotionInfo = BuildGiftPromotionFromReader(reader);
                }
                reader.Close();
            }

            return giftPromotionInfo;
        }

        /// <summary>
        /// 获得赠品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<string> GetGiftPromotionPidList(string pmIdList)
        {
            List<string> giftPromotionPidList = new List<string>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetGiftPromotionPidList(pmIdList);
            while (reader.Read())
            {
                giftPromotionPidList.Add(reader["pid"].ToString());
            }
            reader.Close();
            return giftPromotionPidList;
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
            BrnShop.Core.BSPData.RDBS.AddGift(pmId, giftId, number, pid);
            if (_promotionnosql != null)
                _promotionnosql.DeleteExtGiftList(pmId);
        }

        /// <summary>
        /// 删除赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <returns></returns>
        public static bool DeleteGiftByPmIdAndGiftId(int pmId, int giftId)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteGiftByPmIdAndGiftId(pmId, giftId);
            if (_promotionnosql != null)
                _promotionnosql.DeleteExtGiftList(pmId);
            return result;
        }

        /// <summary>
        /// 赠品是否已经存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        public static bool IsExistGift(int pmId, int giftId)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistGift(pmId, giftId);
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
            bool result = BrnShop.Core.BSPData.RDBS.UpdateGiftNumber(pmId, giftId, number);
            if (_promotionnosql != null)
                _promotionnosql.DeleteExtGiftList(pmId);
            return result;
        }

        /// <summary>
        /// 后台获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtGiftInfo> AdminGetExtGiftList(int pmId)
        {
            List<ExtGiftInfo> extGiftList = new List<ExtGiftInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetExtGiftList(pmId);
            while (reader.Read())
            {
                ExtGiftInfo extGiftInfo = BuildExtGiftFromReader(reader);
                extGiftList.Add(extGiftInfo);
            }
            reader.Close();
            return extGiftList;
        }

        /// <summary>
        /// 获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtGiftInfo> GetExtGiftList(int pmId)
        {
            List<ExtGiftInfo> extGiftList = null;

            if (_promotionnosql != null)
            {
                extGiftList = _promotionnosql.GetExtGiftList(pmId);
                if (extGiftList == null)
                {
                    extGiftList = new List<ExtGiftInfo>();
                    foreach (ExtGiftInfo item in AdminGetExtGiftList(pmId))
                    {
                        ExtGiftInfo extGiftInfo = new ExtGiftInfo();
                        extGiftInfo.RecordId = item.RecordId;
                        extGiftInfo.PmId = item.PmId;
                        extGiftInfo.Number = item.Number;
                        extGiftInfo.Pid = item.Pid;
                        extGiftList.Add(extGiftInfo);
                    }
                    _promotionnosql.CreateExtGiftList(pmId, extGiftList);
                }

                List<string> pidList = new List<string>();
                foreach (ExtGiftInfo extGiftInfo in extGiftList)
                {
                    pidList.Add(extGiftInfo.Pid.ToString());
                }
                List<PartProductInfo> partProductList = _productnosql.GetPartProductList(pidList);

                List<ExtGiftInfo> filterExtGiftList = new List<ExtGiftInfo>();
                foreach (ExtGiftInfo extGiftInfo in extGiftList)
                {
                    PartProductInfo partProductInfo = partProductList.Find(x => x.Pid == extGiftInfo.Pid);
                    if (partProductInfo != null)
                    {
                        extGiftInfo.PSN = partProductInfo.PSN;
                        extGiftInfo.CateId = partProductInfo.CateId;
                        extGiftInfo.BrandId = partProductInfo.BrandId;
                        extGiftInfo.SKUGid = partProductInfo.SKUGid;
                        extGiftInfo.Name = partProductInfo.Name;
                        extGiftInfo.ShopPrice = partProductInfo.ShopPrice;
                        extGiftInfo.MarketPrice = partProductInfo.MarketPrice;
                        extGiftInfo.CostPrice = partProductInfo.CostPrice;
                        extGiftInfo.State = partProductInfo.State;
                        extGiftInfo.IsBest = partProductInfo.IsBest;
                        extGiftInfo.IsHot = partProductInfo.IsHot;
                        extGiftInfo.IsNew = partProductInfo.IsNew;
                        extGiftInfo.DisplayOrder = partProductInfo.DisplayOrder;
                        extGiftInfo.Weight = partProductInfo.Weight;
                        extGiftInfo.ShowImg = partProductInfo.ShowImg;
                        filterExtGiftList.Add(extGiftInfo);
                    }
                }

                extGiftList = filterExtGiftList;
            }
            else
            {
                extGiftList = new List<ExtGiftInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetExtGiftList(pmId);
                while (reader.Read())
                {
                    ExtGiftInfo extGiftInfo = BuildExtGiftFromReader(reader);
                    extGiftList.Add(extGiftInfo);
                }
                reader.Close();
            }

            return extGiftList;
        }






        /// <summary>
        /// 后台获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static SuitPromotionInfo AdminGetSuitPromotionById(int pmId)
        {
            SuitPromotionInfo suitPromotionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetSuitPromotionById(pmId);
            if (reader.Read())
            {
                suitPromotionInfo = BuildSuitPromotionFromReader(reader);
            }

            reader.Close();
            return suitPromotionInfo;
        }

        /// <summary>
        /// 创建套装促销活动
        /// </summary>
        public static int CreateSuitPromotion(SuitPromotionInfo suitPromotionInfo)
        {
            return BrnShop.Core.BSPData.RDBS.CreateSuitPromotion(suitPromotionInfo);
        }

        /// <summary>
        /// 更新套装促销活动
        /// </summary>
        public static void UpdateSuitPromotion(SuitPromotionInfo suitPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateSuitPromotion(suitPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.DeleteSuitPromotion(suitPromotionInfo.PmId);
        }

        /// <summary>
        /// 删除套装促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteSuitPromotionById(string pmIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteSuitPromotionById(pmIdList);
            if (_promotionnosql != null)
                _promotionnosql.DeleteSuitPromotion(pmIdList);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetSuitPromotionList(pageSize, pageNumber, condition, sort);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetSuitPromotionListCondition(pid, promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得套装促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetSuitPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetSuitPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得套装促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetSuitPromotionCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetSuitPromotionCount(condition);
        }

        /// <summary>
        /// 获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static SuitPromotionInfo GetSuitPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            SuitPromotionInfo suitPromotionInfo = null;

            if (_promotionnosql != null)
            {
                suitPromotionInfo = _promotionnosql.GetSuitPromotion(pmId);
                if (suitPromotionInfo == null)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSuitPromotionByPmIdAndTime(pmId, nowTime);
                    if (reader.Read())
                    {
                        suitPromotionInfo = BuildSuitPromotionFromReader(reader);
                    }
                    reader.Close();
                    if (suitPromotionInfo != null)
                        _promotionnosql.CreateSuitPromotion(suitPromotionInfo);
                }
                else if (!((suitPromotionInfo.StartTime1 <= nowTime && nowTime < suitPromotionInfo.EndTime1) || (suitPromotionInfo.StartTime2 <= nowTime && nowTime < suitPromotionInfo.EndTime2) || (suitPromotionInfo.StartTime3 <= nowTime && nowTime < suitPromotionInfo.EndTime3)))
                {
                    suitPromotionInfo = null;
                    _promotionnosql.DeleteSuitPromotion(pmId);
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSuitPromotionByPmIdAndTime(pmId, nowTime);
                if (reader.Read())
                {
                    suitPromotionInfo = BuildSuitPromotionFromReader(reader);
                }
                reader.Close();
            }

            return suitPromotionInfo;
        }

        /// <summary>
        /// 判断用户是否参加过指定套装促销活动
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static bool IsJoinSuitPromotion(int uid, int pmId)
        {
            return BrnShop.Core.BSPData.RDBS.IsJoinSuitPromotion(uid, pmId);
        }

        /// <summary>
        /// 获得套装促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<SuitPromotionInfo> GetSuitPromotionList(int pid, DateTime nowTime)
        {
            List<SuitPromotionInfo> suitPromotionList = new List<SuitPromotionInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSuitPromotionList(pid, nowTime);
            while (reader.Read())
            {
                SuitPromotionInfo suitPromotionInfo = BuildSuitPromotionFromReader(reader);
                suitPromotionList.Add(suitPromotionInfo);
            }
            reader.Close();
            return suitPromotionList;
        }





        /// <summary>
        /// 添加套装商品
        /// </summary>
        public static void AddSuitProduct(int pmId, int pid, int discount, int number)
        {
            BrnShop.Core.BSPData.RDBS.AddSuitProduct(pmId, pid, discount, number);
            if (_promotionnosql != null)
                _promotionnosql.DeleteSuitProductList(pmId);
        }

        /// <summary>
        /// 删除套装商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool DeleteSuitProductByPmIdAndPid(int pmId, int pid)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteSuitProductByPmIdAndPid(pmId, pid);
            if (_promotionnosql != null)
                _promotionnosql.DeleteSuitProductList(pmId);
            return result;
        }

        /// <summary>
        /// 套装商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistSuitProduct(int pmId, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistSuitProduct(pmId, pid);
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
            bool result = BrnShop.Core.BSPData.RDBS.UpdateSuitProductNumber(pmId, pid, number);
            if (_promotionnosql != null)
                _promotionnosql.DeleteSuitProductList(pmId);
            return result;
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
            bool result = BrnShop.Core.BSPData.RDBS.UpdateSuitProductDiscount(pmId, pid, discount);
            if (_promotionnosql != null)
                _promotionnosql.DeleteSuitProductList(pmId);
            return result;
        }

        /// <summary>
        /// 后台获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> AdminGetExtSuitProductList(int pmId)
        {
            List<ExtSuitProductInfo> extSuitProductList = new List<ExtSuitProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetExtSuitProductList(pmId);
            while (reader.Read())
            {
                ExtSuitProductInfo extSuitProductInfo = BuildExtSuitProductFromReader(reader);
                extSuitProductList.Add(extSuitProductInfo);
            }
            reader.Close();
            return extSuitProductList;
        }

        /// <summary>
        /// 获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> GetExtSuitProductList(int pmId)
        {
            List<ExtSuitProductInfo> extSuitProductList = null;

            if (_promotionnosql != null)
            {
                extSuitProductList = _promotionnosql.GetSuitProductList(pmId);
                if (extSuitProductList == null)
                {
                    extSuitProductList = new List<ExtSuitProductInfo>();
                    foreach (ExtSuitProductInfo item in AdminGetExtSuitProductList(pmId))
                    {
                        ExtSuitProductInfo extSuitProductInfo = new ExtSuitProductInfo();
                        extSuitProductInfo.RecordId = item.RecordId;
                        extSuitProductInfo.PmId = item.PmId;
                        extSuitProductInfo.Discount = item.Discount;
                        extSuitProductInfo.Number = item.Number;
                        extSuitProductInfo.Pid = item.Pid;
                        extSuitProductList.Add(extSuitProductInfo);
                    }
                    _promotionnosql.CreateSuitProductList(pmId, extSuitProductList);
                }

                List<string> pidList = new List<string>();
                foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
                {
                    pidList.Add(extSuitProductInfo.Pid.ToString());
                }
                List<PartProductInfo> partProductList = _productnosql.GetPartProductList(pidList);

                List<ExtSuitProductInfo> filterExtSuitProductList = new List<ExtSuitProductInfo>();
                foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
                {
                    PartProductInfo partProductInfo = partProductList.Find(x => x.Pid == extSuitProductInfo.Pid);
                    if (partProductInfo != null)
                    {
                        extSuitProductInfo.PSN = partProductInfo.PSN;
                        extSuitProductInfo.CateId = partProductInfo.CateId;
                        extSuitProductInfo.BrandId = partProductInfo.BrandId;
                        extSuitProductInfo.SKUGid = partProductInfo.SKUGid;
                        extSuitProductInfo.Name = partProductInfo.Name;
                        extSuitProductInfo.ShopPrice = partProductInfo.ShopPrice;
                        extSuitProductInfo.MarketPrice = partProductInfo.MarketPrice;
                        extSuitProductInfo.CostPrice = partProductInfo.CostPrice;
                        extSuitProductInfo.State = partProductInfo.State;
                        extSuitProductInfo.IsBest = partProductInfo.IsBest;
                        extSuitProductInfo.IsHot = partProductInfo.IsHot;
                        extSuitProductInfo.IsNew = partProductInfo.IsNew;
                        extSuitProductInfo.DisplayOrder = partProductInfo.DisplayOrder;
                        extSuitProductInfo.Weight = partProductInfo.Weight;
                        extSuitProductInfo.ShowImg = partProductInfo.ShowImg;
                        filterExtSuitProductList.Add(extSuitProductInfo);
                    }
                }

                extSuitProductList = filterExtSuitProductList;
            }
            else
            {
                extSuitProductList = new List<ExtSuitProductInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetExtSuitProductList(pmId);
                while (reader.Read())
                {
                    ExtSuitProductInfo extSuitProductInfo = BuildExtSuitProductFromReader(reader);
                    extSuitProductList.Add(extSuitProductInfo);
                }
                reader.Close();
            }

            return extSuitProductList;
        }

        /// <summary>
        /// 后台获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> AdminGetAllExtSuitProductList(string pmIdList)
        {
            List<ExtSuitProductInfo> extSuitProductList = new List<ExtSuitProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetAllExtSuitProductList(pmIdList);
            while (reader.Read())
            {
                ExtSuitProductInfo extSuitProductInfo = BuildExtSuitProductFromReader(reader);
                extSuitProductList.Add(extSuitProductInfo);
            }
            reader.Close();
            return extSuitProductList;
        }

        /// <summary>
        /// 获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public static List<ExtSuitProductInfo> GetAllExtSuitProductList(string pmIdList)
        {
            List<ExtSuitProductInfo> extSuitProductList = new List<ExtSuitProductInfo>();

            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAllExtSuitProductList(pmIdList);
            while (reader.Read())
            {
                ExtSuitProductInfo extSuitProductInfo = BuildExtSuitProductFromReader(reader);
                extSuitProductList.Add(extSuitProductInfo);
            }
            reader.Close();

            return extSuitProductList;
        }






        /// <summary>
        /// 后台获得满赠促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static FullSendPromotionInfo AdminGetFullSendPromotionById(int pmId)
        {
            FullSendPromotionInfo fullSendPromotionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetFullSendPromotionById(pmId);
            if (reader.Read())
            {
                fullSendPromotionInfo = BuildFullSendPromotionFromReader(reader);
            }

            reader.Close();
            return fullSendPromotionInfo;
        }

        /// <summary>
        /// 创建满赠促销活动
        /// </summary>
        public static void CreateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateFullSendPromotion(fullSendPromotionInfo);
        }

        /// <summary>
        /// 更新满赠促销活动
        /// </summary>
        public static void UpdateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateFullSendPromotion(fullSendPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.DeleteFullSendPromotion(fullSendPromotionInfo.PmId);
        }

        /// <summary>
        /// 删除满赠促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteFullSendPromotionById(string pmIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteFullSendPromotionById(pmIdList);
            if (_promotionnosql != null)
                _promotionnosql.DeleteFullSendPromotion(pmIdList);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetFullSendPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得满赠促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetFullSendPromotionListCondition(string promotionName, string promotionTime)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullSendPromotionListCondition(promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得满赠促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetFullSendPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullSendPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得满赠促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetFullSendPromotionCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullSendPromotionCount(condition);
        }

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullSendPromotionInfo GetFullSendPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            FullSendPromotionInfo fullSendPromotionInfo = null;

            if (_promotionnosql != null)
            {
                int fullSendPromotionId = _promotionnosql.GetProductFullSendPromotionId(pid);
                if (fullSendPromotionId == 0)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullSendPromotionByPidAndTime(pid, nowTime);
                    if (reader.Read())
                    {
                        fullSendPromotionInfo = BuildFullSendPromotionFromReader(reader);
                    }
                    reader.Close();
                    if (fullSendPromotionInfo != null)
                        _promotionnosql.CreateProductFullSendPromotionId(pid, fullSendPromotionInfo.PmId);
                    else
                        _promotionnosql.CreateProductFullSendPromotionId(pid, -1);
                }
                else if (fullSendPromotionId > 0)
                {
                    fullSendPromotionInfo = GetFullSendPromotionByPmIdAndTime(fullSendPromotionId, nowTime);
                    if (fullSendPromotionInfo == null)
                        _promotionnosql.CreateProductFullSendPromotionId(pid, -1);
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullSendPromotionByPidAndTime(pid, nowTime);
                if (reader.Read())
                {
                    fullSendPromotionInfo = BuildFullSendPromotionFromReader(reader);
                }
                reader.Close();
            }

            return fullSendPromotionInfo;
        }

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullSendPromotionInfo GetFullSendPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            FullSendPromotionInfo fullSendPromotionInfo = null;

            if (_promotionnosql != null)
            {
                fullSendPromotionInfo = _promotionnosql.GetFullSendPromotionByPmId(pmId);
                if (fullSendPromotionInfo == null)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullSendPromotionByPmIdAndTime(pmId, nowTime);
                    if (reader.Read())
                    {
                        fullSendPromotionInfo = BuildFullSendPromotionFromReader(reader);
                    }
                    reader.Close();
                    if (fullSendPromotionInfo != null)
                        _promotionnosql.CreateFullSendPromotion(fullSendPromotionInfo);
                }
                else if (fullSendPromotionInfo.EndTime <= nowTime)
                {
                    _promotionnosql.DeleteFullSendPromotion(pmId);
                    fullSendPromotionInfo = null;
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullSendPromotionByPmIdAndTime(pmId, nowTime);
                if (reader.Read())
                {
                    fullSendPromotionInfo = BuildFullSendPromotionFromReader(reader);
                }
                reader.Close();
            }

            return fullSendPromotionInfo;
        }





        /// <summary>
        /// 添加满赠商品
        /// </summary>
        public static void AddFullSendProduct(int pmId, int pid, int type)
        {
            BrnShop.Core.BSPData.RDBS.AddFullSendProduct(pmId, pid, type);
            if (_promotionnosql != null)
            {
                if (type == 0)
                    _promotionnosql.DeletProductFullSendPromotionId(pid);
                else
                    _promotionnosql.DeleteFullSendPresentIdList(pmId);
            }
        }

        /// <summary>
        /// 删除满赠商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public static bool DeleteFullSendProductByRecordId(string recordIdList)
        {
            if (_promotionnosql != null)
            {
                List<string> pidList = new List<string>();
                List<string> pmIdList = new List<string>();

                foreach (DataRow row in GetFullSendProductList(recordIdList).Rows)
                {
                    string pmId = row["pmid"].ToString();
                    string pid = row["pid"].ToString();
                    int type = TypeHelper.ObjectToInt(row["type"]);

                    if (type == 0)
                        pidList.Add(pid);
                    else
                        pmIdList.Add(pmId);
                }

                _promotionnosql.DeletProductFullSendPromotionId(pidList);
                _promotionnosql.DeleteFullSendPresentIdList(pmIdList);
            }
            return BrnShop.Core.BSPData.RDBS.DeleteFullSendProductByRecordId(recordIdList);
        }

        /// <summary>
        /// 满赠商品是否存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistFullSendProduct(int pmId, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistFullSendProduct(pmId, pid);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetFullSendProductList(pageSize, pageNumber, pmId, type);
        }

        /// <summary>
        /// 后台获得满赠商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static int AdminGetFullSendProductCount(int pmId, int type)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullSendProductCount(pmId, type);
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
            if (_promotionnosql != null && type == 1)
                return GetFullSendPresentList(pmId).Exists(x => x.Pid == pid);
            return BrnShop.Core.BSPData.RDBS.IsExistFullSendProduct(pmId, pid, type);
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
            List<PartProductInfo> partProductList = new List<PartProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullSendMainProductList(pageSize, pageNumber, pmId, startPrice, endPrice, sortColumn, sortDirection);
            while (reader.Read())
            {
                PartProductInfo partProductInfo = Products.BuildPartProductFromReader(reader);
                partProductList.Add(partProductInfo);
            }

            reader.Close();
            return partProductList;
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
            return BrnShop.Core.BSPData.RDBS.GetFullSendMainProductCount(pmId, startPrice, endPrice);
        }

        /// <summary>
        /// 获得满赠赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetFullSendPresentList(int pmId)
        {
            List<PartProductInfo> fullSendPresentList = null;

            if (_promotionnosql != null)
            {
                List<string> fullSendPresentIdList = _promotionnosql.GetFullSendPresentIdList(pmId);
                if (fullSendPresentIdList == null)
                {
                    fullSendPresentIdList = new List<string>();
                    foreach (DataRow row in AdminGetFullSendProductList(1, 30, pmId, 1).Rows)
                    {
                        fullSendPresentIdList.Add(row["pid"].ToString());
                    }
                    _promotionnosql.CreateFullSendPresentIdList(pmId, fullSendPresentIdList);
                }

                fullSendPresentList = _productnosql.GetPartProductList(fullSendPresentIdList);
            }
            else
            {
                fullSendPresentList = new List<PartProductInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullSendPresentList(pmId);
                while (reader.Read())
                {
                    PartProductInfo partProductInfo = Products.BuildPartProductFromReader(reader);
                    fullSendPresentList.Add(partProductInfo);
                }
                reader.Close();
            }

            return fullSendPresentList;
        }

        /// <summary>
        /// 获得满赠商品列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static DataTable GetFullSendProductList(string recordIdList)
        {
            return BrnShop.Core.BSPData.RDBS.GetFullSendProductList(recordIdList);
        }





        /// <summary>
        /// 后台获得满减促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static FullCutPromotionInfo AdminGetFullCutPromotionById(int pmId)
        {
            FullCutPromotionInfo fullCutPromotionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetFullCutPromotionById(pmId);
            if (reader.Read())
            {
                fullCutPromotionInfo = BuildFullCutPromotionFromReader(reader);
            }

            reader.Close();
            return fullCutPromotionInfo;
        }

        /// <summary>
        /// 创建满减促销活动
        /// </summary>
        public static void CreateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateFullCutPromotion(fullCutPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.ClearAllFullCutPromotion();
        }

        /// <summary>
        /// 更新满减促销活动
        /// </summary>
        public static void UpdateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateFullCutPromotion(fullCutPromotionInfo);
            if (_promotionnosql != null)
                _promotionnosql.ClearAllFullCutPromotion();
        }

        /// <summary>
        /// 删除满减促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public static void DeleteFullCutPromotionById(string pmIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteFullCutPromotionById(pmIdList);
            if (_promotionnosql != null)
                _promotionnosql.ClearAllFullCutPromotion();
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
            return BrnShop.Core.BSPData.RDBS.AdminGetFullCutPromotionList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得满减促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public static string AdminGetFullCutPromotionListCondition(string promotionName, string promotionTime)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullCutPromotionListCondition(promotionName, promotionTime);
        }

        /// <summary>
        /// 后台获得满减促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetFullCutPromotionListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullCutPromotionListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得满减促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetFullCutPromotionCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullCutPromotionCount(condition);
        }

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullCutPromotionInfo GetFullCutPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            FullCutPromotionInfo fullCutPromotionInfo = null;

            if (_promotionnosql != null)
            {
                int productFullCutPromotionId = _promotionnosql.GetProductFullCutPromotionId(pid);
                if (productFullCutPromotionId == 0)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullCutPromotionByPidAndTime(pid, nowTime);
                    if (reader.Read())
                    {
                        fullCutPromotionInfo = BuildFullCutPromotionFromReader(reader);
                    }
                    reader.Close();
                    if (fullCutPromotionInfo != null)
                        _promotionnosql.CreateProductFullCutPromotionId(pid, fullCutPromotionInfo.PmId);
                    else
                        _promotionnosql.CreateProductFullCutPromotionId(pid, -1);
                }
                else if (productFullCutPromotionId > 0)
                {
                    List<FullCutPromotionInfo> allFullCutPromotion = GetAllFullCutPromotion(nowTime);

                    bool flag = false;//是否有冗余满减促销活动的标识
                    foreach (FullCutPromotionInfo item in allFullCutPromotion)
                    {
                        if (item.EndTime <= nowTime)
                        {
                            flag = true;
                            continue;
                        }

                        if (item.PmId == productFullCutPromotionId)
                        {
                            fullCutPromotionInfo = item;
                            break;
                        }
                    }

                    if (flag)
                        _promotionnosql.ClearAllFullCutPromotion();

                    if (fullCutPromotionInfo == null)
                        _promotionnosql.CreateProductFullCutPromotionId(pid, -1);
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullCutPromotionByPidAndTime(pid, nowTime);
                if (reader.Read())
                {
                    fullCutPromotionInfo = BuildFullCutPromotionFromReader(reader);
                }
                reader.Close();
            }

            return fullCutPromotionInfo;
        }

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static FullCutPromotionInfo GetFullCutPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            FullCutPromotionInfo fullCutPromotionInfo = null;

            if (_promotionnosql != null)
            {
                List<FullCutPromotionInfo> allFullCutPromotion = GetAllFullCutPromotion(nowTime);

                bool flag = false;//是否有冗余满减促销活动的标识
                foreach (FullCutPromotionInfo item in allFullCutPromotion)
                {
                    if (item.EndTime <= nowTime)
                    {
                        flag = true;
                        continue;
                    }

                    if (item.PmId == pmId)
                    {
                        fullCutPromotionInfo = item;
                        break;
                    }
                }

                if (flag)
                    _promotionnosql.ClearAllFullCutPromotion();
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullCutPromotionByPmIdAndTime(pmId, nowTime);
                if (reader.Read())
                {
                    fullCutPromotionInfo = BuildFullCutPromotionFromReader(reader);
                }
                reader.Close();
            }

            return fullCutPromotionInfo;
        }

        /// <summary>
        /// 获得全部满减促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<FullCutPromotionInfo> GetAllFullCutPromotion(DateTime nowTime)
        {
            List<FullCutPromotionInfo> allFullCutPromotion = null;

            if (_promotionnosql != null)
            {
                allFullCutPromotion = _promotionnosql.GetAllFullCutPromotion();
                if (allFullCutPromotion == null)
                {
                    allFullCutPromotion = new List<FullCutPromotionInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAllFullCutPromotion(nowTime);
                    while (reader.Read())
                    {
                        FullCutPromotionInfo tempFullCutPromotionInfo = BuildFullCutPromotionFromReader(reader);
                        allFullCutPromotion.Add(tempFullCutPromotionInfo);
                    }
                    reader.Close();
                    _promotionnosql.AddAllFullCutPromotion(allFullCutPromotion);
                }
            }
            else
            {
                allFullCutPromotion = new List<FullCutPromotionInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAllFullCutPromotion(nowTime);
                while (reader.Read())
                {
                    FullCutPromotionInfo tempFullCutPromotionInfo = BuildFullCutPromotionFromReader(reader);
                    allFullCutPromotion.Add(tempFullCutPromotionInfo);
                }
                reader.Close();
            }

            return allFullCutPromotion;
        }





        /// <summary>
        /// 添加满减商品
        /// </summary>
        public static void AddFullCutProduct(int pmId, int pid)
        {
            BrnShop.Core.BSPData.RDBS.AddFullCutProduct(pmId, pid);
            if (_promotionnosql != null)
                _promotionnosql.DeletProductFullCutPromotionId(pid);
        }

        /// <summary>
        /// 删除满减商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public static bool DeleteFullCutProductByRecordId(string recordIdList)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteFullCutProductByRecordId(recordIdList);
            if (_promotionnosql != null)
                _promotionnosql.DeletProductFullCutPromotionId(GetFullCutPidList(recordIdList));
            return result;
        }

        /// <summary>
        /// 满减商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistFullCutProduct(int pmId, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistFullCutProduct(pmId, pid);
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
            return BrnShop.Core.BSPData.RDBS.AdminGetFullCutProductList(pageSize, pageNumber, pmId);
        }

        /// <summary>
        /// 后台获得满减商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public static int AdminGetFullCutProductCount(int pmId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetFullCutProductCount(pmId);
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
            List<PartProductInfo> partProductList = new List<PartProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullCutProductList(pageSize, pageNumber, fullCutPromotionInfo, startPrice, endPrice, sortColumn, sortDirection);
            while (reader.Read())
            {
                PartProductInfo partProductInfo = Products.BuildPartProductFromReader(reader);
                partProductList.Add(partProductInfo);
            }

            reader.Close();
            return partProductList;
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
            return BrnShop.Core.BSPData.RDBS.GetFullCutProductCount(fullCutPromotionInfo, startPrice, endPrice);
        }

        /// <summary>
        /// 获得满减商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static List<string> GetFullCutPidList(string recordIdList)
        {
            List<string> fullCutPidList = new List<string>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetFullCutPidList(recordIdList);
            while (reader.Read())
            {
                fullCutPidList.Add(reader["pid"].ToString());
            }
            reader.Close();
            return fullCutPidList;
        }
    }
}
