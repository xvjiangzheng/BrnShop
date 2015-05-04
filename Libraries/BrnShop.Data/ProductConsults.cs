using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 商品咨询数据访问类
    /// </summary>
    public partial class ProductConsults
    {
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ProductConsultInfo信息
        /// </summary>
        public static ProductConsultInfo BuildProductConsultFromReader(IDataReader reader)
        {
            ProductConsultInfo productConsultInfo = new ProductConsultInfo();

            productConsultInfo.ConsultId = TypeHelper.ObjectToInt(reader["consultid"]);
            productConsultInfo.ConsultTypeId = TypeHelper.ObjectToInt(reader["consulttypeid"]);
            productConsultInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            productConsultInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productConsultInfo.ConsultUid = TypeHelper.ObjectToInt(reader["consultuid"]);
            productConsultInfo.ReplyUid = TypeHelper.ObjectToInt(reader["replyuid"]);
            productConsultInfo.ConsultTime = TypeHelper.ObjectToDateTime(reader["consulttime"]);
            productConsultInfo.ReplyTime = TypeHelper.ObjectToDateTime(reader["replytime"]);
            productConsultInfo.ConsultMessage = reader["consultmessage"].ToString();
            productConsultInfo.ReplyMessage = reader["replymessage"].ToString();
            productConsultInfo.PName = reader["pname"].ToString();
            productConsultInfo.ConsultNickName = reader["consultnickname"].ToString();
            productConsultInfo.ReplyNickName = reader["replynickname"].ToString();
            productConsultInfo.PShowImg = reader["pshowimg"].ToString();
            productConsultInfo.ConsultIP = reader["consultip"].ToString();
            productConsultInfo.ReplyIP = reader["replyip"].ToString();

            return productConsultInfo;
        }

        #endregion

        /// <summary>
        /// 创建商品咨询类型
        /// </summary>
        public static void CreateProductConsultType(ProductConsultTypeInfo productConsultTypeInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateProductConsultType(productConsultTypeInfo);
        }

        /// <summary>
        /// 更新商品咨询类型
        /// </summary>
        public static void UpdateProductConsultType(ProductConsultTypeInfo productConsultTypeInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProductConsultType(productConsultTypeInfo);
        }

        /// <summary>
        /// 删除商品咨询类型
        /// </summary>
        /// <param name="consultTypeId">商品咨询类型id</param>
        public static void DeleteProductConsultTypeById(int consultTypeId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteProductConsultTypeById(consultTypeId);
        }

        /// <summary>
        /// 获得商品咨询类型列表
        /// </summary>
        /// <returns></returns>
        public static ProductConsultTypeInfo[] GetProductConsultTypeList()
        {
            DataTable dt = BrnShop.Core.BSPData.RDBS.GetProductConsultTypeList();
            ProductConsultTypeInfo[] productConsultTypeList = new ProductConsultTypeInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                ProductConsultTypeInfo productConsultTypeInfo = new ProductConsultTypeInfo();
                productConsultTypeInfo.ConsultTypeId = TypeHelper.ObjectToInt(row["consulttypeid"]);
                productConsultTypeInfo.Title = row["Title"].ToString();
                productConsultTypeInfo.DisplayOrder = TypeHelper.ObjectToInt(row["displayorder"]);
                productConsultTypeList[index] = productConsultTypeInfo;
                index++;
            }
            return productConsultTypeList;
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
            BrnShop.Core.BSPData.RDBS.ConsultProduct(pid, consultTypeId, consultUid, consultTime, consultMessage, consultNickName, pName, pShowImg, consultIP);
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
            BrnShop.Core.BSPData.RDBS.ReplyProductConsult(consultId, replyUid, replyTime, replyMessage, replyNickName, replyIP);
        }

        /// <summary>
        /// 获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        public static ProductConsultInfo GetProductConsultById(int consultId)
        {
            ProductConsultInfo productConsultInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductConsultById(consultId);
            if (reader.Read())
            {
                productConsultInfo = BuildProductConsultFromReader(reader);
            }
            reader.Close();

            return productConsultInfo;
        }

        /// <summary>
        /// 后台获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        public static ProductConsultInfo AdminGetProductConsultById(int consultId)
        {
            ProductConsultInfo productConsultInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetProductConsultById(consultId);
            if (reader.Read())
            {
                productConsultInfo = BuildProductConsultFromReader(reader);
            }
            reader.Close();

            return productConsultInfo;
        }

        /// <summary>
        /// 删除商品咨询
        /// </summary>
        /// <param name="consultIdList">咨询id</param>
        public static void DeleteProductConsultById(string consultIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteProductConsultById(consultIdList);
        }

        /// <summary>
        /// 后台获得商品咨询列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static List<ProductConsultInfo> AdminGetProductConsultList(int pageSize, int pageNumber, string condition, string sort)
        {
            List<ProductConsultInfo> productConsultList = new List<ProductConsultInfo>();

            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetProductConsultList(pageSize, pageNumber, condition, sort);
            while (reader.Read())
            {
                ProductConsultInfo productConsultInfo = BuildProductConsultFromReader(reader);
                productConsultList.Add(productConsultInfo);
            }
            reader.Close();

            return productConsultList;
        }

        /// <summary>
        /// 后台获得商品咨询列表条件
        /// </summary>
        /// <param name="consultTypeId">商品咨询类型id</param>
        /// <param name="pid">商品id</param>
        /// <param name="uid">用户id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <param name="consultStartTime">咨询开始时间</param>
        /// <param name="consultEndTime">咨询结束时间</param>
        /// <returns></returns>
        public static string AdminGetProductConsultListCondition(int consultTypeId, int pid, int uid, string consultMessage, string consultStartTime, string consultEndTime)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductConsultListCondition(consultTypeId, pid, uid, consultMessage, consultStartTime, consultEndTime);
        }

        /// <summary>
        /// 后台获得商品咨询列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductConsultListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductConsultListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品咨询数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductConsultCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductConsultCount(condition);
        }

        /// <summary>
        /// 更新商品咨询状态
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static bool UpdateProductConsultState(int consultId, int state)
        {
            return BrnShop.Core.BSPData.RDBS.UpdateProductConsultState(consultId, state);
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
            List<ProductConsultInfo> productConsultList = new List<ProductConsultInfo>();

            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductConsultList(pageSize, pageNumber, pid, consultTypeId, consultMessage);
            while (reader.Read())
            {
                ProductConsultInfo productConsultInfo = BuildProductConsultFromReader(reader);
                productConsultList.Add(productConsultInfo);
            }
            reader.Close();

            return productConsultList;
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
            return BrnShop.Core.BSPData.RDBS.GetProductConsultCount(pid, consultTypeId, consultMessage);
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
            List<ProductConsultInfo> productConsultList = new List<ProductConsultInfo>();

            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetUserProductConsultList(uid, pageSize, pageNumber);
            while (reader.Read())
            {
                ProductConsultInfo productConsultInfo = BuildProductConsultFromReader(reader);
                productConsultList.Add(productConsultInfo);
            }
            reader.Close();

            return productConsultList;
        }

        /// <summary>
        /// 获得用户商品咨询数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUserProductConsultCount(int uid)
        {
            return BrnShop.Core.BSPData.RDBS.GetUserProductConsultCount(uid);
        }
    }
}
