using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.OrderStrategy.SqlServer
{
    /// <summary>
    /// 基于SqlServer的订单策略
    /// </summary>
    public partial class OrderStrategy : IOrderStrategy
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="isPersistOrderProduct">是否需要持久化订单商品</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns>订单id</returns>
        public int CreateOrder(OrderInfo orderInfo, bool isPersistOrderProduct, List<OrderProductInfo> orderProductList)
        {
            DbParameter[] parms = {
	                                    GenerateInParam("@osn", SqlDbType.Char,30,orderInfo.OSN),
	                                    GenerateInParam("@uid", SqlDbType.Int,4 ,orderInfo.Uid),
	                                    GenerateInParam("@orderstate", SqlDbType.TinyInt,1 ,orderInfo.OrderState),
                                        GenerateInParam("@productamount", SqlDbType.Decimal,8 ,orderInfo.ProductAmount),
                                        GenerateInParam("@orderamount", SqlDbType.Decimal,8 ,orderInfo.OrderAmount),
                                        GenerateInParam("@surplusmoney", SqlDbType.Decimal,8 ,orderInfo.SurplusMoney),
                                        GenerateInParam("@parentid", SqlDbType.Int,4 ,orderInfo.ParentId),
                                        GenerateInParam("@isreview", SqlDbType.TinyInt,1 ,orderInfo.IsReview),
                                        GenerateInParam("@addtime", SqlDbType.DateTime, 8,orderInfo.AddTime),
                                        GenerateInParam("@shipsn", SqlDbType.Char,30 ,orderInfo.ShipSN),
                                        GenerateInParam("@shipsystemname", SqlDbType.Char,20 ,orderInfo.ShipSystemName),
                                        GenerateInParam("@shipfriendname", SqlDbType.NChar,30 ,orderInfo.ShipFriendName),
                                        GenerateInParam("@shiptime", SqlDbType.DateTime, 8,orderInfo.ShipTime),
                                        GenerateInParam("@paysn", SqlDbType.Char,30 ,orderInfo.PaySN),
                                        GenerateInParam("@paysystemname", SqlDbType.Char,20 ,orderInfo.PaySystemName),
                                        GenerateInParam("@payfriendname", SqlDbType.NChar,30 ,orderInfo.PayFriendName),
	                                    GenerateInParam("@paymode", SqlDbType.TinyInt,1 ,orderInfo.PayMode),
                                        GenerateInParam("@paytime", SqlDbType.DateTime, 8,orderInfo.PayTime),
                                        GenerateInParam("@regionid", SqlDbType.SmallInt,2 ,orderInfo.RegionId),
                                        GenerateInParam("@consignee", SqlDbType.NVarChar,30 ,orderInfo.Consignee),
                                        GenerateInParam("@mobile", SqlDbType.VarChar,15 ,orderInfo.Mobile),
                                        GenerateInParam("@phone", SqlDbType.VarChar,12 ,orderInfo.Phone),
                                        GenerateInParam("@email", SqlDbType.VarChar,50 ,orderInfo.Email),
                                        GenerateInParam("@zipcode", SqlDbType.Char,6 ,orderInfo.ZipCode),
	                                    GenerateInParam("@address", SqlDbType.NVarChar,150 ,orderInfo.Address),
                                        GenerateInParam("@besttime", SqlDbType.DateTime,8 ,orderInfo.BestTime),
	                                    GenerateInParam("@shipfee", SqlDbType.Decimal,8 ,orderInfo.ShipFee),
                                        GenerateInParam("@payfee", SqlDbType.Decimal,8 ,orderInfo.PayFee),
                                        GenerateInParam("@fullcut", SqlDbType.Int,4 ,orderInfo.FullCut),
	                                    GenerateInParam("@discount", SqlDbType.Decimal,8 ,orderInfo.Discount),
	                                    GenerateInParam("@paycreditcount", SqlDbType.Int,4 ,orderInfo.PayCreditCount),
	                                    GenerateInParam("@paycreditmoney", SqlDbType.Decimal,8 ,orderInfo.PayCreditMoney),
                                        GenerateInParam("@couponmoney", SqlDbType.Int,4 ,orderInfo.CouponMoney),
	                                    GenerateInParam("@weight", SqlDbType.Int,4 ,orderInfo.Weight),
                                        GenerateInParam("@buyerremark", SqlDbType.NVarChar,250 ,orderInfo.BuyerRemark),
                                        GenerateInParam("@ip", SqlDbType.VarChar,15 ,orderInfo.IP)
                                    };
            int oid = TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                      string.Format("{0}createorder", RDBSHelper.RDBSTablePre),
                                                                      parms), -1);

            if (oid > 0)
            {
                if (isPersistOrderProduct)
                {
                    StringBuilder recordIdList = new StringBuilder();
                    foreach (OrderProductInfo orderProductInfo in orderProductList)
                    {
                        recordIdList.AppendFormat("{0},", orderProductInfo.RecordId);
                    }
                    parms = new DbParameter[] {
                                                GenerateInParam("@recordidlist", SqlDbType.NVarChar, 1000, recordIdList.Remove(recordIdList.Length - 1,1).ToString()),
                                                GenerateInParam("@oid", SqlDbType.Int, 4, oid),
                                              };
                    RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                               string.Format("{0}updateorderproductoid", RDBSHelper.RDBSTablePre),
                                               parms);
                }
                else
                {
                    foreach (OrderProductInfo orderProductInfo in orderProductList)
                    {
                        orderProductInfo.Oid = oid;
                        parms = new DbParameter[] {
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
            }

            return oid;
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

        #endregion
    }
}
