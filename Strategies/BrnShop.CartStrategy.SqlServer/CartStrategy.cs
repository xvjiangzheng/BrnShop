using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.CartStrategy.SqlServer
{
    /// <summary>
    /// 基于SqlServer的购物车策略
    /// </summary>
    public partial class CartStrategy : ICartStrategy
    {
        /// <summary>
        /// 是否持久化订单商品
        /// </summary>
        public bool IsPersistOrderProduct
        {
            get { return true; }
        }

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetCartProductCount(int uid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int, 4, uid)    
                                   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getcartproductcountbyuid", RDBSHelper.RDBSTablePre),
                                                                   parms), -2);
        }

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        public int GetCartProductCount(string sid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@sid", SqlDbType.Char, 16, sid)    
                                   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getcartproductcountbysid", RDBSHelper.RDBSTablePre),
                                                                   parms), -2);
        }

        /// <summary>
        /// 添加订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void AddOrderProductList(List<OrderProductInfo> orderProductList)
        {
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                DbParameter[] parms = {
                                        GenerateInParam("@oid", SqlDbType.Int, 4, orderProductInfo.Oid),
                                        GenerateInParam("@uid", SqlDbType.Int, 4, orderProductInfo.Uid),
                                        GenerateInParam("@sid", SqlDbType.Char, 16, orderProductInfo.Sid),
                                        GenerateInParam("@pid", SqlDbType.Int, 4, orderProductInfo.Pid),
                                        GenerateInParam("@psn", SqlDbType.Char, 30, orderProductInfo.PSN),
                                        GenerateInParam("@cateid", SqlDbType.SmallInt, 2, orderProductInfo.CateId),
                                        GenerateInParam("@brandid", SqlDbType.Int, 4, orderProductInfo.BrandId),
                                        GenerateInParam("@name", SqlDbType.NVarChar, 200, orderProductInfo.Name),
                                        GenerateInParam("@showimg", SqlDbType.NVarChar, 100, orderProductInfo.ShowImg),
                                        GenerateInParam("@discountprice", SqlDbType.Decimal, 4, orderProductInfo.DiscountPrice),
                                        GenerateInParam("@costprice", SqlDbType.Decimal, 4, orderProductInfo.CostPrice),
                                        GenerateInParam("@shopprice", SqlDbType.Decimal, 4, orderProductInfo.ShopPrice),
                                        GenerateInParam("@marketprice", SqlDbType.Decimal, 4, orderProductInfo.MarketPrice),
                                        GenerateInParam("@weight", SqlDbType.Int, 4, orderProductInfo.Weight),
                                        GenerateInParam("@isreview", SqlDbType.TinyInt, 1, orderProductInfo.IsReview),
                                        GenerateInParam("@realcount", SqlDbType.Int, 4, orderProductInfo.RealCount),
                                        GenerateInParam("@buycount", SqlDbType.Int, 4, orderProductInfo.BuyCount),
                                        GenerateInParam("@sendcount", SqlDbType.Int, 4, orderProductInfo.SendCount),
                                        GenerateInParam("@type", SqlDbType.TinyInt, 1, orderProductInfo.Type),
                                        GenerateInParam("@paycredits", SqlDbType.Int, 4, orderProductInfo.PayCredits),
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, orderProductInfo.CouponTypeId),
                                        GenerateInParam("@extcode1", SqlDbType.Int, 4, orderProductInfo.ExtCode1),
                                        GenerateInParam("@extcode2", SqlDbType.Int, 4, orderProductInfo.ExtCode2),
                                        GenerateInParam("@extcode3", SqlDbType.Int, 4, orderProductInfo.ExtCode3),
                                        GenerateInParam("@extcode4", SqlDbType.Int, 4, orderProductInfo.ExtCode4),
                                        GenerateInParam("@extcode5", SqlDbType.Int, 4, orderProductInfo.ExtCode5),
                                        GenerateInParam("@addtime", SqlDbType.DateTime, 8, orderProductInfo.AddTime)
                                    };
                RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                           string.Format("{0}addorderproduct", RDBSHelper.RDBSTablePre),
                                           parms);
            }
        }

        /// <summary>
        /// 删除订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void DeleteOrderProductList(List<OrderProductInfo> orderProductList)
        {
            StringBuilder recordIdList = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
                recordIdList.AppendFormat("{0},", orderProductInfo.RecordId);

            DbParameter[] parms = {
                                    GenerateInParam("@recordidlist", SqlDbType.NVarChar, 1000, recordIdList.Remove(recordIdList.Length - 1, 1).ToString())
                                   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}deleteorderproductbyrecordid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新订单商品的数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void UpdateOrderProductCount(List<OrderProductInfo> orderProductList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                sql.AppendFormat("UPDATE [{0}orderproducts] SET [realcount]={1},[buycount]={2},[extcode2]={3} WHERE [recordid]={4};", RDBSHelper.RDBSTablePre, orderProductInfo.RealCount, orderProductInfo.BuyCount, orderProductInfo.ExtCode2, orderProductInfo.RecordId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 更新购物车的用户id
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">用户sid</param>
        public void UpdateCartUidBySid(int uid, string sid)
        {
            DbParameter[] parms = {
                                      GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                      GenerateInParam("@sid", SqlDbType.Char, 16, sid)    
                                    };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updatecartuidbysid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新订单商品的买送促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void UpdateOrderProductBuySend(List<OrderProductInfo> orderProductList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                sql.AppendFormat("UPDATE [{0}orderproducts] SET [realcount]={1},[buycount]={2},[extcode2]={3} WHERE [recordid]={4};", RDBSHelper.RDBSTablePre, orderProductInfo.RealCount, orderProductInfo.BuyCount, orderProductInfo.ExtCode2, orderProductInfo.RecordId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 更新订单商品的单品促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void UpdateOrderProductSingle(List<OrderProductInfo> orderProductList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                sql.AppendFormat("UPDATE [{0}orderproducts] SET [discountprice]={1},[paycredits]={2},[coupontypeid]={3},[extcode1]={4} WHERE [recordid]={5};", RDBSHelper.RDBSTablePre, orderProductInfo.DiscountPrice, orderProductInfo.PayCredits, orderProductInfo.CouponTypeId, orderProductInfo.ExtCode1, orderProductInfo.RecordId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 更新订单商品的赠品促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void UpdateOrderProductGift(List<OrderProductInfo> orderProductList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                sql.AppendFormat("UPDATE [{0}orderproducts] SET [extcode3]={1} WHERE [recordid]={2};", RDBSHelper.RDBSTablePre, orderProductInfo.ExtCode3, orderProductInfo.RecordId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 更新订单商品的满赠促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void UpdateOrderProductFullSend(List<OrderProductInfo> orderProductList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                sql.AppendFormat("UPDATE [{0}orderproducts] SET [extcode4]={1} WHERE [recordid]={2};", RDBSHelper.RDBSTablePre, orderProductInfo.ExtCode4, orderProductInfo.RecordId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 更新订单商品的满减促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public void UpdateOrderProductFullCut(List<OrderProductInfo> orderProductList)
        {
            StringBuilder sql = new StringBuilder();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                sql.AppendFormat("UPDATE [{0}orderproducts] SET [extcode5]={1} WHERE [recordid]={2};", RDBSHelper.RDBSTablePre, orderProductInfo.ExtCode5, orderProductInfo.RecordId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public List<OrderProductInfo> GetCartProductList(int uid)
        {
            List<OrderProductInfo> orderProductList = new List<OrderProductInfo>();

            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid)    
                                    };
            IDataReader reader = RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                                          string.Format("{0}getcartproductlistbyuid", RDBSHelper.RDBSTablePre),
                                                          parms);
            while (reader.Read())
            {
                OrderProductInfo orderProductInfo = BuildOrderProductFromReader(reader);
                orderProductList.Add(orderProductInfo);
            }
            reader.Close();

            return orderProductList;
        }

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        public List<OrderProductInfo> GetCartProductList(string sid)
        {
            List<OrderProductInfo> orderProductList = new List<OrderProductInfo>();

            DbParameter[] parms = {
                                        GenerateInParam("@sid", SqlDbType.Char, 16, sid)    
                                    };
            IDataReader reader = RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                                          string.Format("{0}getcartproductlistbysid", RDBSHelper.RDBSTablePre),
                                                          parms);
            while (reader.Read())
            {
                OrderProductInfo orderProductInfo = BuildOrderProductFromReader(reader);
                orderProductList.Add(orderProductInfo);
            }
            reader.Close();

            return orderProductList;
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int ClearCart(int uid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@uid", SqlDbType.Int, 4, uid)
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}clearcartbyuid", RDBSHelper.RDBSTablePre),
                                              parms);
        }

        /// <summary>
        /// 清空购物车的商品
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns></returns>
        public int ClearCart(string sid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@sid", SqlDbType.Char, 16, sid)
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}clearcartbysid", RDBSHelper.RDBSTablePre),
                                              parms);
        }

        /// <summary>
        /// 清空过期购物车
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        public void ClearExpiredCart(DateTime expireTime)
        {
            DbParameter[] parms = {
	                                 GenerateInParam("@expiretime", SqlDbType.DateTime,8,expireTime)
                                   };
            string commandText = string.Format("DELETE FROM [{0}orderproducts] WHERE [oid]=0 AND [addtime]<@expiretime",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #region  辅助方法

        /// <summary>
        /// 生成输入参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private DbParameter GenerateInParam(string paramName, SqlDbType sqlDbType, int size, object value)
        {
            SqlParameter param = new SqlParameter(paramName, sqlDbType, size);
            param.Direction = ParameterDirection.Input;
            if (value != null)
                param.Value = value;
            return param;
        }

        /// <summary>
        /// 从IDataReader创建OrderProductInfo
        /// </summary>
        private OrderProductInfo BuildOrderProductFromReader(IDataReader reader)
        {
            OrderProductInfo orderProductInfo = new OrderProductInfo();
            orderProductInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            orderProductInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            orderProductInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            orderProductInfo.Sid = reader["sid"].ToString();
            orderProductInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            orderProductInfo.PSN = reader["psn"].ToString();
            orderProductInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            orderProductInfo.BrandId = TypeHelper.ObjectToInt(reader["brandid"]);
            orderProductInfo.Name = reader["name"].ToString();
            orderProductInfo.ShowImg = reader["showimg"].ToString();
            orderProductInfo.DiscountPrice = TypeHelper.ObjectToDecimal(reader["discountprice"]);
            orderProductInfo.ShopPrice = TypeHelper.ObjectToDecimal(reader["shopprice"]);
            orderProductInfo.CostPrice = TypeHelper.ObjectToDecimal(reader["costprice"]);
            orderProductInfo.MarketPrice = TypeHelper.ObjectToDecimal(reader["marketprice"]);
            orderProductInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);
            orderProductInfo.IsReview = TypeHelper.ObjectToInt(reader["isreview"]);
            orderProductInfo.RealCount = TypeHelper.ObjectToInt(reader["realcount"]);
            orderProductInfo.BuyCount = TypeHelper.ObjectToInt(reader["buycount"]);
            orderProductInfo.SendCount = TypeHelper.ObjectToInt(reader["sendcount"]);
            orderProductInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            orderProductInfo.PayCredits = TypeHelper.ObjectToInt(reader["paycredits"]);
            orderProductInfo.CouponTypeId = TypeHelper.ObjectToInt(reader["coupontypeid"]);
            orderProductInfo.ExtCode1 = TypeHelper.ObjectToInt(reader["extcode1"]);
            orderProductInfo.ExtCode2 = TypeHelper.ObjectToInt(reader["extcode2"]);
            orderProductInfo.ExtCode3 = TypeHelper.ObjectToInt(reader["extcode3"]);
            orderProductInfo.ExtCode4 = TypeHelper.ObjectToInt(reader["extcode4"]);
            orderProductInfo.ExtCode5 = TypeHelper.ObjectToInt(reader["extcode5"]);
            orderProductInfo.AddTime = TypeHelper.ObjectToDateTime(reader["addtime"]);
            return orderProductInfo;
        }

        #endregion
    }
}
