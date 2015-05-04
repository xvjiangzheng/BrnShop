using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 商品咨询操作管理类
    /// </summary>
    public partial class ProductConsults
    {
        /// <summary>
        /// 获得商品咨询类型列表
        /// </summary>
        /// <returns></returns>
        public static ProductConsultTypeInfo[] GetProductConsultTypeList()
        {
            ProductConsultTypeInfo[] productConsultTypeList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_PRODUCTCONSULTTYPE_LIST) as ProductConsultTypeInfo[];
            if (productConsultTypeList == null)
            {
                productConsultTypeList = BrnShop.Data.ProductConsults.GetProductConsultTypeList();
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_PRODUCTCONSULTTYPE_LIST, productConsultTypeList);
            }
            return productConsultTypeList;
        }

        /// <summary>
        /// 获得商品咨询类型
        /// </summary>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <returns></returns>
        public static ProductConsultTypeInfo GetProductConsultTypeById(int consultTypeId)
        {
            if (consultTypeId > 0)
            {
                foreach (ProductConsultTypeInfo info in GetProductConsultTypeList())
                {
                    if (consultTypeId == info.ConsultTypeId)
                        return info;
                }
            }
            return null;
        }





        /// <summary>
        /// 咨询商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultUid">咨询用户id</param>
        /// <param name="consultTime">咨询时间</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <param name="consultNickName">咨询昵称</param>
        /// <param name="pName">商品名称</param>
        /// <param name="pShowImg">商品图片</param>
        /// <param name="consultIP">咨询ip</param>
        public static void ConsultProduct(int pid, int consultTypeId, int consultUid, DateTime consultTime, string consultMessage, string consultNickName, string pName, string pShowImg, string consultIP)
        {
            BrnShop.Data.ProductConsults.ConsultProduct(pid, consultTypeId, consultUid, consultTime, consultMessage, consultNickName, pName, pShowImg, consultIP);
        }

        /// <summary>
        /// 回复商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="replyUid">回复用户id</param>
        /// <param name="replyTime">回复时间</param>
        /// <param name="replyMessage">回复内容</param>
        /// <param name="replyNickName">回复昵称</param>
        /// <param name="replyIP">回复ip</param>
        public static void ReplyProductConsult(int consultId, int replyUid, DateTime replyTime, string replyMessage, string replyNickName, string replyIP)
        {
            BrnShop.Data.ProductConsults.ReplyProductConsult(consultId, replyUid, replyTime, replyMessage, replyNickName, replyIP);
        }

        /// <summary>
        /// 获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        public static ProductConsultInfo GetProductConsultById(int consultId)
        {
            if (consultId > 0)
                return BrnShop.Data.ProductConsults.GetProductConsultById(consultId);
            return null;
        }

        /// <summary>
        /// 删除商品咨询
        /// </summary>
        /// <param name="consultIdList">咨询id</param>
        public static void DeleteProductConsultById(int[] consultIdList)
        {
            if (consultIdList != null && consultIdList.Length > 0)
                BrnShop.Data.ProductConsults.DeleteProductConsultById(CommonHelper.IntArrayToString(consultIdList));
        }

        /// <summary>
        /// 更新商品咨询状态
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static bool UpdateProductConsultState(int consultId, int state)
        {
            if (consultId > 0 && (state == 0 || state == 1))
                return BrnShop.Data.ProductConsults.UpdateProductConsultState(consultId, state);
            return false;
        }

        /// <summary>
        /// 获得商品咨询列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <returns></returns>
        public static List<ProductConsultInfo> GetProductConsultList(int pageSize, int pageNumber, int pid, int consultTypeId, string consultMessage)
        {
            return BrnShop.Data.ProductConsults.GetProductConsultList(pageSize, pageNumber, pid, consultTypeId, consultMessage);
        }

        /// <summary>
        /// 获得商品咨询数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <returns></returns>
        public static int GetProductConsultCount(int pid, int consultTypeId, string consultMessage)
        {
            return BrnShop.Data.ProductConsults.GetProductConsultCount(pid, consultTypeId, consultMessage);
        }

        /// <summary>
        /// 获得用户商品咨询列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static List<ProductConsultInfo> GetUserProductConsultList(int uid, int pageSize, int pageNumber)
        {
            return BrnShop.Data.ProductConsults.GetUserProductConsultList(uid, pageSize, pageNumber);
        }

        /// <summary>
        /// 获得用户商品咨询数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUserProductConsultCount(int uid)
        {
            return BrnShop.Data.ProductConsults.GetUserProductConsultCount(uid);
        }
    }
}
