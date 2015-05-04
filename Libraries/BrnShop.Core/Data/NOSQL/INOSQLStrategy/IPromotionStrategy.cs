using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop非关系型数据库策略之促销活动接口
    /// </summary>
    public partial interface IPromotionNOSQLStrategy
    {
        #region 单品促销活动

        /// <summary>
        /// 删除商品的单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        void DeleteProductSinglePromotion(int pid);

        /// <summary>
        /// 删除商品的单品促销活动
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        void DeleteProductSinglePromotion(List<string> pidList);

        /// <summary>
        /// 创建商品的单品促销活动
        /// </summary>
        /// <param name="singlePromotionInfo">单品促销活动信息</param>
        void CreateProductSinglePromotion(SinglePromotionInfo singlePromotionInfo);

        /// <summary>
        /// 获得单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        SinglePromotionInfo GetSinglePromotionByPidAndTime(int pid, DateTime nowTime);

        /// <summary>
        /// 更新商品的单品促销活动的库存
        /// </summary>
        /// <param name="singlePromotionList">单品促销活动列表</param>
        void UpdateProductSinglePromotionStock(List<SinglePromotionInfo> singlePromotionList);

        #endregion

        #region 买送促销活动

        /// <summary>
        /// 清空全部买送促销活动
        /// </summary>
        void ClearAllBuySendPromotion();

        /// <summary>
        /// 添加全部买送促销活动
        /// </summary>
        /// <param name="buySendPromotionList">买送促销活动列表</param>
        void AddAllBuySendPromotion(List<BuySendPromotionInfo> buySendPromotionList);

        /// <summary>
        /// 获得全部买送促销活动
        /// </summary>
        /// <returns></returns>
        List<BuySendPromotionInfo> GetAllBuySendPromotion();

        /// <summary>
        /// 获得商品的买送促销活动id列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        List<int> GetProductBuySendPromotionIdList(int pid);

        /// <summary>
        /// 创建商品的买送促销活动id列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pmIdList">促销活动id列表</param>
        void CreateProductBuySendPromotionIdList(int pid, List<int> pmIdList);

        /// <summary>
        /// 删除商品的买送促销活动id列表
        /// </summary>
        /// <param name="pid">商品id</param>
        void DeletProductBuySendPromotionIdList(int pid);

        /// <summary>
        /// 删除商品的买送促销活动id列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        void DeletProductBuySendPromotionIdList(List<string> pidList);

        #endregion

        #region 赠品促销活动

        /// <summary>
        /// 删除商品的赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        void DeleteProductGiftPromotion(int pid);

        /// <summary>
        /// 删除商品的赠品促销活动
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        void DeleteProductGiftPromotion(List<string> pidList);

        /// <summary>
        /// 创建商品的赠品促销活动
        /// </summary>
        /// <param name="giftPromotionInfo">赠品促销活动信息</param>
        void CreateProductGiftPromotion(GiftPromotionInfo giftPromotionInfo);

        /// <summary>
        /// 获得赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        GiftPromotionInfo GetGiftPromotionByPidAndTime(int pid, DateTime nowTime);

        /// <summary>
        /// 删除扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        void DeleteExtGiftList(int pmId);

        /// <summary>
        /// 创建扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="extGiftList">扩展赠品列表</param>
        void CreateExtGiftList(int pmId, List<ExtGiftInfo> extGiftList);

        /// <summary>
        /// 获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        List<ExtGiftInfo> GetExtGiftList(int pmId);

        #endregion

        #region 套装促销活动

        /// <summary>
        /// 删除套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        void DeleteSuitPromotion(int pmId);

        /// <summary>
        /// 删除套装促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        void DeleteSuitPromotion(string pmIdList);

        /// <summary>
        /// 创建套装促销活动
        /// </summary>
        /// <param name="suitPromotionInfo">套装促销活动</param>
        void CreateSuitPromotion(SuitPromotionInfo suitPromotionInfo);

        /// <summary>
        /// 获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        SuitPromotionInfo GetSuitPromotion(int pmId);

        /// <summary>
        /// 删除套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        void DeleteSuitProductList(int pmId);

        /// <summary>
        /// 创建套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="extSuitProductList">扩展套装商品列表</param>
        void CreateSuitProductList(int pmId, List<ExtSuitProductInfo> extSuitProductList);

        /// <summary>
        /// 获得套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        List<ExtSuitProductInfo> GetSuitProductList(int pmId);

        #endregion

        #region 满赠促销活动

        /// <summary>
        /// 删除满赠促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        void DeleteFullSendPromotion(int pmId);

        /// <summary>
        /// 删除满赠促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        void DeleteFullSendPromotion(string pmIdList);

        /// <summary>
        /// 创建满赠促销活动
        /// </summary>
        /// <param name="fullSendPromotionInfo">满赠促销活动信息</param>
        void CreateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo);

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        FullSendPromotionInfo GetFullSendPromotionByPmId(int pmId);

        /// <summary>
        /// 获得商品的满赠促销活动id
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        int GetProductFullSendPromotionId(int pid);

        /// <summary>
        /// 创建商品的满赠促销活动id
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pmId">促销活动id</param>
        void CreateProductFullSendPromotionId(int pid, int pmId);

        /// <summary>
        /// 删除商品的满赠促销活动id
        /// </summary>
        /// <param name="pid">商品id</param>
        void DeletProductFullSendPromotionId(int pid);

        /// <summary>
        /// 删除商品的满赠促销活动id
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        void DeletProductFullSendPromotionId(List<string> pidList);

        /// <summary>
        /// 删除满赠赠品id列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        void DeleteFullSendPresentIdList(int pmId);

        /// <summary>
        /// 删除满赠赠品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        void DeleteFullSendPresentIdList(List<string> pmIdList);

        /// <summary>
        /// 创建满赠赠品id列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="fullSendPresentIdList">满赠赠品id列表</param>
        void CreateFullSendPresentIdList(int pmId, List<string> fullSendPresentIdList);

        /// <summary>
        /// 获得满赠赠品id列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        List<string> GetFullSendPresentIdList(int pmId);

        #endregion

        #region 满减促销活动

        /// <summary>
        /// 清空全部满减促销活动
        /// </summary>
        void ClearAllFullCutPromotion();

        /// <summary>
        /// 添加全部满减促销活动
        /// </summary>
        /// <param name="fullCutPromotionList">满减促销活动列表</param>
        void AddAllFullCutPromotion(List<FullCutPromotionInfo> fullCutPromotionList);

        /// <summary>
        /// 获得全部满减促销活动
        /// </summary>
        /// <returns></returns>
        List<FullCutPromotionInfo> GetAllFullCutPromotion();

        /// <summary>
        /// 获得商品的满减促销活动id
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        int GetProductFullCutPromotionId(int pid);

        /// <summary>
        /// 创建商品的满减促销活动id
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pmId">促销活动id</param>
        void CreateProductFullCutPromotionId(int pid, int pmId);

        /// <summary>
        /// 删除商品的满减促销活动id
        /// </summary>
        /// <param name="pid">商品id</param>
        void DeletProductFullCutPromotionId(int pid);

        /// <summary>
        /// 删除商品的满减促销活动id
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        void DeletProductFullCutPromotionId(List<string> pidList);

        #endregion
    }
}
