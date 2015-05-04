using System;
using System.Data;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop关系数据库策略之促销活动分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        #region 单品促销活动

        /// <summary>
        /// 后台获得单品促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader AdminGetSinglePromotionById(int pmId);

        /// <summary>
        /// 创建单品促销活动
        /// </summary>
        void CreateSinglePromotion(SinglePromotionInfo singlePromotionInfo);

        /// <summary>
        /// 更新单品促销活动
        /// </summary>
        void UpdateSinglePromotion(SinglePromotionInfo singlePromotionInfo);

        /// <summary>
        /// 删除单品促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id</param>
        void DeleteSinglePromotionByPmId(string pmIdList);

        /// <summary>
        /// 后台获得单品促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetSinglePromotionList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得单品促销活动列表搜索条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        string AdminGetSinglePromotionListCondition(int pid, string promotionName, string promotionTime);

        /// <summary>
        /// 后台获得单品促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetSinglePromotionListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得单品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetSinglePromotionCount(string condition);

        /// <summary>
        /// 后台判断商品在指定时间段内是否已经存在单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        int AdminIsExistSinglePromotion(int pid, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获得单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetSinglePromotionByPidAndTime(int pid, DateTime nowTime);

        /// <summary>
        /// 获得单品促销活动商品的购买数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        int GetSinglePromotionProductBuyCount(int uid, int pmId);

        /// <summary>
        /// 更新单品促销活动的库存
        /// </summary>
        /// <param name="singlePromotionList">单品促销活动列表</param>
        void UpdateSinglePromotionStock(List<SinglePromotionInfo> singlePromotionList);

        /// <summary>
        /// 获得单品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        IDataReader GetSinglePromotionPidList(string pmIdList);

        #endregion

        #region 买送促销活动

        /// <summary>
        /// 后台获得买送促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader AdminGetBuySendPromotionById(int pmId);

        /// <summary>
        /// 创建买送促销活动
        /// </summary>
        void CreateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo);

        /// <summary>
        /// 更新买送促销活动
        /// </summary>
        void UpdateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo);

        /// <summary>
        /// 删除买送促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id</param>
        void DeleteBuySendPromotionById(string pmIdList);

        /// <summary>
        /// 后台获得买送促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetBuySendPromotionList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得买送促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        string AdminGetBuySendPromotionListCondition(string promotionName, string promotionTime);

        /// <summary>
        /// 后台获得买送促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetBuySendPromotionListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得买送促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetBuySendPromotionCount(string condition);

        /// <summary>
        /// 获得买送促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetBuySendPromotionList(int pid, DateTime nowTime);

        /// <summary>
        /// 获得全部买送促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetAllBuySendPromotion(DateTime nowTime);

        #endregion

        #region 买送商品

        /// <summary>
        /// 添加买送商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        void AddBuySendProduct(int pmId, int pid);

        /// <summary>
        /// 删除买送商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        bool DeleteBuySendProductByRecordId(string recordIdList);

        /// <summary>
        /// 买送商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        bool IsExistBuySendProduct(int pmId, int pid);

        /// <summary>
        /// 后台获得买送商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        DataTable AdminGetBuySendProductList(int pageSize, int pageNumber, int pmId, int pid);

        /// <summary>
        /// 后台获得买送商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        int AdminGetBuySendProductCount(int pmId, int pid);

        /// <summary>
        /// 获得买送商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        IDataReader GetBuySendPidList(string recordIdList);

        #endregion

        #region 赠品促销活动

        /// <summary>
        /// 后台获得赠品促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        IDataReader AdminGetGiftPromotionById(int pmId);

        /// <summary>
        /// 创建赠品促销活动
        /// </summary>
        void CreateGiftPromotion(GiftPromotionInfo giftPromotionInfo);

        /// <summary>
        /// 更新赠品促销活动
        /// </summary>
        void UpdateGiftPromotion(GiftPromotionInfo giftPromotionInfo);

        /// <summary>
        /// 删除赠品促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        void DeleteGiftPromotionByPmId(string pmIdList);

        /// <summary>
        /// 后台获得赠品促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetGiftPromotionList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得赠品促销活动列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        string AdminGetGiftPromotionListCondition(int pid, string promotionName, string promotionTime);

        /// <summary>
        /// 后台获得赠品促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetGiftPromotionListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得赠品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetGiftPromotionCount(string condition);

        /// <summary>
        /// 后台判断商品在指定时间段内是否已经存在赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        int AdminIsExistGiftPromotion(int pid, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获得赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetGiftPromotionByPidAndTime(int pid, DateTime nowTime);

        /// <summary>
        /// 获得赠品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        IDataReader GetGiftPromotionPidList(string pmIdList);

        #endregion

        #region 赠品

        /// <summary>
        /// 添加赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <param name="number">数量</param>
        /// <param name="pid">商品id</param>
        void AddGift(int pmId, int giftId, int number, int pid);

        /// <summary>
        /// 删除赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <returns></returns>
        bool DeleteGiftByPmIdAndGiftId(int pmId, int giftId);

        /// <summary>
        /// 赠品是否已经存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        bool IsExistGift(int pmId, int giftId);

        /// <summary>
        /// 更新赠品的数量
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        bool UpdateGiftNumber(int pmId, int giftId, int number);

        /// <summary>
        /// 后台获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader AdminGetExtGiftList(int pmId);

        /// <summary>
        /// 获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader GetExtGiftList(int pmId);

        #endregion

        #region 套装促销活动

        /// <summary>
        /// 后台获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader AdminGetSuitPromotionById(int pmId);

        /// <summary>
        /// 创建套装促销活动
        /// </summary>
        int CreateSuitPromotion(SuitPromotionInfo suitPromotionInfo);

        /// <summary>
        /// 更新套装促销活动
        /// </summary>
        void UpdateSuitPromotion(SuitPromotionInfo suitPromotionInfo);

        /// <summary>
        /// 删除套装促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        void DeleteSuitPromotionById(string pmIdList);

        /// <summary>
        /// 后台获得套装促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetSuitPromotionList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得套装促销活动列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        string AdminGetSuitPromotionListCondition(int pid, string promotionName, string promotionTime);

        /// <summary>
        /// 后台获得套装促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetSuitPromotionListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得套装促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetSuitPromotionCount(string condition);

        /// <summary>
        /// 获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetSuitPromotionByPmIdAndTime(int pmId, DateTime nowTime);

        /// <summary>
        /// 判断用户是否参加过指定套装促销活动
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        bool IsJoinSuitPromotion(int uid, int pmId);

        /// <summary>
        /// 获得套装促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetSuitPromotionList(int pid, DateTime nowTime);

        #endregion

        #region 套装商品

        /// <summary>
        /// 添加套装商品
        /// </summary>
        void AddSuitProduct(int pmId, int pid, int discount, int number);

        /// <summary>
        /// 删除套装商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        bool DeleteSuitProductByPmIdAndPid(int pmId, int pid);

        /// <summary>
        /// 套装商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        bool IsExistSuitProduct(int pmId, int pid);

        /// <summary>
        /// 修改套装商品数量
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        bool UpdateSuitProductNumber(int pmId, int pid, int number);

        /// <summary>
        /// 修改套装商品折扣
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="discount">折扣</param>
        /// <returns></returns>
        bool UpdateSuitProductDiscount(int pmId, int pid, int discount);

        /// <summary>
        /// 后台获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader AdminGetExtSuitProductList(int pmId);

        /// <summary>
        /// 获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader GetExtSuitProductList(int pmId);

        /// <summary>
        /// 后台获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        IDataReader AdminGetAllExtSuitProductList(string pmIdList);

        /// <summary>
        /// 获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        IDataReader GetAllExtSuitProductList(string pmIdList);

        #endregion

        #region 满赠促销活动

        /// <summary>
        /// 后台获得满赠促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        IDataReader AdminGetFullSendPromotionById(int pmId);

        /// <summary>
        /// 创建满赠促销活动
        /// </summary>
        void CreateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo);

        /// <summary>
        /// 更新满赠促销活动
        /// </summary>
        void UpdateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo);

        /// <summary>
        /// 删除满赠促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        void DeleteFullSendPromotionById(string pmIdList);

        /// <summary>
        /// 后台获得满赠促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetFullSendPromotionList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得满赠促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        string AdminGetFullSendPromotionListCondition(string promotionName, string promotionTime);

        /// <summary>
        /// 后台获得满赠促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetFullSendPromotionListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得满赠促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetFullSendPromotionCount(string condition);

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetFullSendPromotionByPidAndTime(int pid, DateTime nowTime);

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetFullSendPromotionByPmIdAndTime(int pmId, DateTime nowTime);

        #endregion

        #region 满赠商品

        /// <summary>
        /// 添加满赠商品
        /// </summary>
        void AddFullSendProduct(int pmId, int pid, int type);

        /// <summary>
        /// 删除满赠商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        bool DeleteFullSendProductByRecordId(string recordIdList);

        /// <summary>
        /// 满赠商品是否存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        bool IsExistFullSendProduct(int pmId, int pid);

        /// <summary>
        /// 后台获得满赠商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        DataTable AdminGetFullSendProductList(int pageSize, int pageNumber, int pmId, int type);

        /// <summary>
        /// 后台获得满赠商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        int AdminGetFullSendProductCount(int pmId, int type);

        /// <summary>
        /// 判断满赠商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        bool IsExistFullSendProduct(int pmId, int pid, int type);

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
        IDataReader GetFullSendMainProductList(int pageSize, int pageNumber, int pmId, int startPrice, int endPrice, int sortColumn, int sortDirection);

        /// <summary>
        /// 获得满赠主商品数量
        /// </summary>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        int GetFullSendMainProductCount(int pmId, int startPrice, int endPrice);

        /// <summary>
        /// 获得满赠赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        IDataReader GetFullSendPresentList(int pmId);

        /// <summary>
        /// 获得满赠商品列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        DataTable GetFullSendProductList(string recordIdList);

        #endregion

        #region 满减促销活动

        /// <summary>
        /// 后台获得满减促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        IDataReader AdminGetFullCutPromotionById(int pmId);

        /// <summary>
        /// 创建满减促销活动
        /// </summary>
        void CreateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo);

        /// <summary>
        /// 更新满减促销活动
        /// </summary>
        void UpdateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo);

        /// <summary>
        /// 删除满减促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        void DeleteFullCutPromotionById(string pmIdList);

        /// <summary>
        /// 后台获得满减促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetFullCutPromotionList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得满减促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        string AdminGetFullCutPromotionListCondition(string promotionName, string promotionTime);

        /// <summary>
        /// 后台获得满减促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetFullCutPromotionListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得满减促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetFullCutPromotionCount(string condition);

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetFullCutPromotionByPidAndTime(int pid, DateTime nowTime);

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetFullCutPromotionByPmIdAndTime(int pmId, DateTime nowTime);

        /// <summary>
        /// 获得全部满减促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetAllFullCutPromotion(DateTime nowTime);

        #endregion

        #region 满减商品

        /// <summary>
        /// 添加满减商品
        /// </summary>
        void AddFullCutProduct(int pmId, int pid);

        /// <summary>
        /// 删除满减商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        bool DeleteFullCutProductByRecordId(string recordIdList);

        /// <summary>
        /// 满减商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        bool IsExistFullCutProduct(int pmId, int pid);

        /// <summary>
        /// 后台获得满减商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        DataTable AdminGetFullCutProductList(int pageSize, int pageNumber, int pmId);

        /// <summary>
        /// 后台获得满减商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        int AdminGetFullCutProductCount(int pmId);

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
        IDataReader GetFullCutProductList(int pageSize, int pageNumber, FullCutPromotionInfo fullCutPromotionInfo, int startPrice, int endPrice, int sortColumn, int sortDirection);

        /// <summary>
        /// 获得满减商品数量
        /// </summary>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        int GetFullCutProductCount(FullCutPromotionInfo fullCutPromotionInfo, int startPrice, int endPrice);

        /// <summary>
        /// 获得满减商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        IDataReader GetFullCutPidList(string recordIdList);

        #endregion

        #region 活动专题

        /// <summary>
        /// 创建活动专题
        /// </summary>
        /// <param name="topicInfo">活动专题信息</param>
        void CreateTopic(TopicInfo topicInfo);

        /// <summary>
        /// 更新活动专题
        /// </summary>
        /// <param name="topicInfo">活动专题信息</param>
        void UpdateTopic(TopicInfo topicInfo);

        /// <summary>
        /// 删除活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        void DeleteTopicById(int topicId);

        /// <summary>
        /// 后台获得活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        /// <returns></returns>
        IDataReader AdminGetTopicById(int topicId);

        /// <summary>
        /// 后台获得活动专题列表条件
        /// </summary>
        /// <param name="sn">编号</param>
        /// <param name="title">标题</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        string AdminGetTopicListCondition(string sn, string title, string startTime, string endTime);

        /// <summary>
        /// 后台获得活动专题列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetTopicListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得活动专题列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetTopicList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得活动专题数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetTopicCount(string condition);

        /// <summary>
        /// 判断活动专题编号是否存在
        /// </summary>
        /// <param name="topicSN">活动专题编号</param>
        /// <returns></returns>
        bool IsExistTopicSN(string topicSN);

        /// <summary>
        /// 获得活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetTopicByIdAndTime(int topicId, DateTime nowTime);

        /// <summary>
        /// 获得活动专题
        /// </summary>
        /// <param name="topicSN">活动专题编号</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetTopicBySNAndTime(string topicSN, DateTime nowTime);

        #endregion

        #region 优惠劵类型

        /// <summary>
        /// 创建优惠劵类型
        /// </summary>
        /// <param name="couponTypeInfo">优惠劵类型信息</param>
        void CreateCouponType(CouponTypeInfo couponTypeInfo);

        /// <summary>
        /// 删除优惠劵类型
        /// </summary>
        /// <param name="couponTypeIdList">优惠劵类型id列表</param>
        void DeleteCouponTypeById(string couponTypeIdList);

        /// <summary>
        /// 获得优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        IDataReader GetCouponTypeById(int couponTypeId);

        /// <summary>
        /// 后台获得优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        IDataReader AdminGetCouponTypeById(int couponTypeId);

        /// <summary>
        /// 后台获得优惠劵类型列表条件
        /// </summary>
        /// <param name="type">0代表正在发放，1代表正在使用，-1代表全部</param>
        /// <param name="couponTypeName">优惠劵类型名称</param>
        /// <returns></returns>
        string AdminGetCouponTypeListCondition(int type, string couponTypeName);

        /// <summary>
        /// 后台获得优惠劵类型列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable AdminGetCouponTypeList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 后台获得优惠劵类型数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetCouponTypeCount(string condition);

        /// <summary>
        /// 改变优惠劵类型状态
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="state">状态(0代表关闭，1代表打开)</param>
        /// <returns></returns>
        bool ChangeCouponTypeState(int couponTypeId, int state);

        /// <summary>
        /// 获得当前正在发放的活动优惠劵类型列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetSendingPromotionCouponTypeList();

        /// <summary>
        /// 获得当前正在发放的优惠劵类型列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetSendingCouponTypeList(DateTime nowTime);

        /// <summary>
        /// 获得当前正在使用的优惠劵类型列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetUsingCouponTypeList(DateTime nowTime);

        #endregion

        #region 优惠劵

        /// <summary>
        /// 创建优惠劵
        /// </summary>
        /// <param name="couponInfo">优惠劵信息</param>
        void CreateCoupon(CouponInfo couponInfo);

        /// <summary>
        /// 删除优惠劵
        /// </summary>
        /// <param name="idList">id列表</param>
        void DeleteCouponById(string idList);

        /// <summary>
        /// 后台获得优惠劵列表条件
        /// </summary>
        /// <param name="sn">编号</param>
        /// <param name="uid">用户id</param>
        /// <param name="coupontTypeId">优惠劵类型id</param>
        /// <returns></returns>
        string AdminGetCouponListCondition(string sn, int uid, int coupontTypeId);

        /// <summary>
        /// 后台获得优惠劵列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable AdminGetCouponList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 后台获得优惠劵数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetCouponCount(string condition);

        /// <summary>
        /// 判断优惠劵编号是否存在
        /// </summary>
        /// <param name="couponSN">优惠劵编号</param>
        /// <returns></returns>
        bool IsExistCouponSN(string couponSN);

        /// <summary>
        /// 获得发放的优惠劵数量
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        int GetSendCouponCount(int couponTypeId);

        /// <summary>
        /// 获得发放给用户的优惠劵数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        int GetSendUserCouponCount(int uid, int couponTypeId);

        /// <summary>
        /// 获得今天用户发放的优惠劵数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        int GetTodaySendUserCouponCount(int uid, int couponTypeId, DateTime today);

        /// <summary>
        /// 获得优惠劵列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(0代表全部，1代表未使用，2代表已使用，3代表已过期)</param>
        /// <returns></returns>
        DataTable GetCouponList(int uid, int type);

        /// <summary>
        /// 获得优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <returns></returns>
        IDataReader GetCouponByCouponId(int couponId);

        /// <summary>
        /// 获得优惠劵
        /// </summary>
        /// <param name="couponSN">优惠劵编号</param>
        /// <returns></returns>
        IDataReader GetCouponByCouponSN(string couponSN);

        /// <summary>
        /// 使用优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="oid">订单id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        void UseCoupon(int couponId, int oid, DateTime time, string ip);

        /// <summary>
        /// 激活和使用优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="uid">用户id</param>
        /// <param name="oid">订单id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        void ActivateAndUseCoupon(int couponId, int uid, int oid, DateTime time, string ip);

        /// <summary>
        /// 激活优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="uid">用户id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        void ActivateCoupon(int couponId, int uid, DateTime time, string ip);

        /// <summary>
        /// 退还订单使用的优惠劵
        /// </summary>
        /// <param name="oid">订单id</param>
        void ReturnUserOrderUseCoupons(int oid);

        /// <summary>
        /// 获得用户订单发放的优惠劵列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        IDataReader GetUserOrderSendCouponList(int oid);

        #endregion

        #region 优惠劵商品

        /// <summary>
        /// 添加优惠劵商品
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pid">商品id</param>
        void AddCouponProduct(int couponTypeId, int pid);

        /// <summary>
        /// 删除优惠劵商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        bool DeleteCouponProductByRecordId(string recordIdList);

        /// <summary>
        /// 优惠劵商品是否已经存在
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pid">商品id</param>
        bool IsExistCouponProduct(int couponTypeId, int pid);

        /// <summary>
        /// 后台获得优惠劵商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        DataTable AdminGetCouponProductList(int pageSize, int pageNumber, int couponTypeId);

        /// <summary>
        /// 后台获得优惠劵商品数量
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        int AdminGetCouponProductCount(int couponTypeId);

        /// <summary>
        /// 商品是否属于同一优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        bool IsSameCouponType(int couponTypeId, string pidList);

        #endregion
    }
}
