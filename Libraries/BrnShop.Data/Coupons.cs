using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 优惠劵数据访问类
    /// </summary>
    public partial class Coupons
    {
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建CouponTypeInfo信息
        /// </summary>
        public static CouponTypeInfo BuildCouponTypeFromReader(IDataReader reader)
        {
            CouponTypeInfo couponTypeInfo = new CouponTypeInfo();

            couponTypeInfo.CouponTypeId = TypeHelper.ObjectToInt(reader["coupontypeid"]);
            couponTypeInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            couponTypeInfo.Name = reader["name"].ToString();
            couponTypeInfo.Money = TypeHelper.ObjectToInt(reader["money"]);
            couponTypeInfo.Count = TypeHelper.ObjectToInt(reader["count"]);
            couponTypeInfo.SendMode = TypeHelper.ObjectToInt(reader["sendmode"]);
            couponTypeInfo.GetMode = TypeHelper.ObjectToInt(reader["getmode"]);
            couponTypeInfo.UseMode = TypeHelper.ObjectToInt(reader["usemode"]);
            couponTypeInfo.UserRankLower = TypeHelper.ObjectToInt(reader["userranklower"]);
            couponTypeInfo.OrderAmountLower = TypeHelper.ObjectToInt(reader["orderamountlower"]);
            couponTypeInfo.LimitCateId = TypeHelper.ObjectToInt(reader["limitcateid"]);
            couponTypeInfo.LimitBrandId = TypeHelper.ObjectToInt(reader["limitbrandid"]);
            couponTypeInfo.LimitProduct = TypeHelper.ObjectToInt(reader["limitproduct"]);
            couponTypeInfo.SendStartTime = TypeHelper.ObjectToDateTime(reader["sendstarttime"]);
            couponTypeInfo.SendEndTime = TypeHelper.ObjectToDateTime(reader["sendendtime"]);
            couponTypeInfo.UseExpireTime = TypeHelper.ObjectToInt(reader["useexpiretime"]);
            couponTypeInfo.UseStartTime = TypeHelper.ObjectToDateTime(reader["usestarttime"]);
            couponTypeInfo.UseEndTime = TypeHelper.ObjectToDateTime(reader["useendtime"]);

            return couponTypeInfo;
        }

        /// <summary>
        /// 通过IDataReader创建CouponInfo信息
        /// </summary>
        public static CouponInfo BuildCouponFromReader(IDataReader reader)
        {
            CouponInfo couponInfo = new CouponInfo();

            couponInfo.CouponId = TypeHelper.ObjectToInt(reader["couponid"]);
            couponInfo.CouponSN = reader["couponsn"].ToString();
            couponInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            couponInfo.CouponTypeId = TypeHelper.ObjectToInt(reader["coupontypeid"]);
            couponInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            couponInfo.UseTime = TypeHelper.ObjectToDateTime(reader["usetime"]);
            couponInfo.UseIP = reader["useip"].ToString();
            couponInfo.Money = TypeHelper.ObjectToInt(reader["money"]);
            couponInfo.ActivateTime = TypeHelper.ObjectToDateTime(reader["activatetime"]);
            couponInfo.ActivateIP = reader["activateip"].ToString();
            couponInfo.CreateUid = TypeHelper.ObjectToInt(reader["createuid"]);
            couponInfo.CreateOid = TypeHelper.ObjectToInt(reader["createoid"]);
            couponInfo.CreateTime = TypeHelper.ObjectToDateTime(reader["createtime"]);
            couponInfo.CreateIP = reader["createip"].ToString();

            return couponInfo;
        }

        #endregion

        /// <summary>
        /// 创建优惠劵类型
        /// </summary>
        /// <param name="couponTypeInfo">优惠劵类型信息</param>
        public static void CreateCouponType(CouponTypeInfo couponTypeInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateCouponType(couponTypeInfo);
        }

        /// <summary>
        /// 删除优惠劵类型
        /// </summary>
        /// <param name="couponTypeIdList">优惠劵类型id列表</param>
        public static void DeleteCouponTypeById(string couponTypeIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteCouponTypeById(couponTypeIdList);
        }

        /// <summary>
        /// 获得优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static CouponTypeInfo GetCouponTypeById(int couponTypeId)
        {
            CouponTypeInfo couponTypeInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetCouponTypeById(couponTypeId);
            if (reader.Read())
            {
                couponTypeInfo = BuildCouponTypeFromReader(reader);
            }

            reader.Close();
            return couponTypeInfo;
        }

        /// <summary>
        /// 后台获得优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static CouponTypeInfo AdminGetCouponTypeById(int couponTypeId)
        {
            CouponTypeInfo couponTypeInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetCouponTypeById(couponTypeId);
            if (reader.Read())
            {
                couponTypeInfo = BuildCouponTypeFromReader(reader);
            }

            reader.Close();
            return couponTypeInfo;
        }

        /// <summary>
        /// 后台获得优惠劵类型列表条件
        /// </summary>
        /// <param name="type">0代表正在发放，1代表正在使用，-1代表全部</param>
        /// <param name="couponTypeName">优惠劵类型名称</param>
        /// <returns></returns>
        public static string AdminGetCouponTypeListCondition(int type, string couponTypeName)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponTypeListCondition(type, couponTypeName);
        }

        /// <summary>
        /// 后台获得优惠劵类型列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable AdminGetCouponTypeList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponTypeList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 后台获得优惠劵类型数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetCouponTypeCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponTypeCount(condition);
        }

        /// <summary>
        /// 更新优惠劵类型状态
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="state">状态(0代表关闭，1代表打开)</param>
        /// <returns></returns>
        public static bool ChangeCouponTypeState(int couponTypeId, int state)
        {
            return BrnShop.Core.BSPData.RDBS.ChangeCouponTypeState(couponTypeId, state);
        }

        /// <summary>
        /// 获得当前正在发放的活动优惠劵类型列表
        /// </summary>
        /// <returns></returns>
        public static List<CouponTypeInfo> GetSendingPromotionCouponTypeList()
        {
            List<CouponTypeInfo> couponTypeList = new List<CouponTypeInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSendingPromotionCouponTypeList();
            while (reader.Read())
            {
                CouponTypeInfo couponTypeInfo = BuildCouponTypeFromReader(reader);
                couponTypeList.Add(couponTypeInfo);
            }

            reader.Close();
            return couponTypeList;
        }

        /// <summary>
        /// 获得当前正在发放的优惠劵类型列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<CouponTypeInfo> GetSendingCouponTypeList(DateTime nowTime)
        {
            List<CouponTypeInfo> couponTypeList = new List<CouponTypeInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSendingCouponTypeList(nowTime);
            while (reader.Read())
            {
                CouponTypeInfo couponTypeInfo = BuildCouponTypeFromReader(reader);
                couponTypeList.Add(couponTypeInfo);
            }

            reader.Close();
            return couponTypeList;
        }

        /// <summary>
        /// 获得当前正在使用的优惠劵类型列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<CouponTypeInfo> GetUsingCouponTypeList(DateTime nowTime)
        {
            List<CouponTypeInfo> couponTypeList = new List<CouponTypeInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetUsingCouponTypeList(nowTime);
            while (reader.Read())
            {
                CouponTypeInfo couponTypeInfo = BuildCouponTypeFromReader(reader);
                couponTypeList.Add(couponTypeInfo);
            }

            reader.Close();
            return couponTypeList;
        }






        /// <summary>
        /// 创建优惠劵
        /// </summary>
        /// <param name="couponInfo">优惠劵信息</param>
        public static void CreateCoupon(CouponInfo couponInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateCoupon(couponInfo);
        }

        /// <summary>
        /// 删除优惠劵
        /// </summary>
        /// <param name="idList">id列表</param>
        public static void DeleteCouponById(string idList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteCouponById(idList);
        }

        /// <summary>
        /// 后台获得优惠劵列表条件
        /// </summary>
        /// <param name="sn">编号</param>
        /// <param name="uid">用户id</param>
        /// <param name="coupontTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static string AdminGetCouponListCondition(string sn, int uid, int couponTypeId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponListCondition(sn, uid, couponTypeId);
        }

        /// <summary>
        /// 后台获得优惠劵列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable AdminGetCouponList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 后台获得优惠劵列表数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetCouponCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponCount(condition);
        }

        /// <summary>
        /// 判断优惠劵编号是否存在
        /// </summary>
        /// <param name="couponSN">优惠劵编号</param>
        /// <returns></returns>
        public static bool IsExistCouponSN(string couponSN)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistCouponSN(couponSN);
        }

        /// <summary>
        /// 获得发放的优惠劵数量
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static int GetSendCouponCount(int couponTypeId)
        {
            return BrnShop.Core.BSPData.RDBS.GetSendCouponCount(couponTypeId);
        }

        /// <summary>
        /// 获得发放给用户的优惠劵数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static int GetSendUserCouponCount(int uid, int couponTypeId)
        {
            return BrnShop.Core.BSPData.RDBS.GetSendUserCouponCount(uid, couponTypeId);
        }

        /// <summary>
        /// 获得今天用户发放的优惠劵数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetTodaySendUserCouponCount(int uid, int couponTypeId, DateTime today)
        {
            return BrnShop.Core.BSPData.RDBS.GetTodaySendUserCouponCount(uid, couponTypeId, today);
        }

        /// <summary>
        /// 获得优惠劵列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(0代表全部，1代表未使用，2代表已使用，3代表已过期)</param>
        /// <returns></returns>
        public static DataTable GetCouponList(int uid, int type)
        {
            return BrnShop.Core.BSPData.RDBS.GetCouponList(uid, type);
        }

        /// <summary>
        /// 获得优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <returns></returns>
        public static CouponInfo GetCouponByCouponId(int couponId)
        {
            CouponInfo couponInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetCouponByCouponId(couponId);
            if (reader.Read())
            {
                couponInfo = BuildCouponFromReader(reader);
            }

            reader.Close();
            return couponInfo;
        }

        /// <summary>
        /// 获得优惠劵
        /// </summary>
        /// <param name="couponSN">优惠劵编号</param>
        /// <returns></returns>
        public static CouponInfo GetCouponByCouponSN(string couponSN)
        {
            CouponInfo couponInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetCouponByCouponSN(couponSN);
            if (reader.Read())
            {
                couponInfo = BuildCouponFromReader(reader);
            }

            reader.Close();
            return couponInfo;
        }

        /// <summary>
        /// 使用优惠劵
        /// </summary>
        public static void UseCoupon(int couponId, int oid, DateTime time, string ip)
        {
            BrnShop.Core.BSPData.RDBS.UseCoupon(couponId, oid, time, ip);
        }

        /// <summary>
        /// 激活和使用优惠劵
        /// </summary>
        public static void ActivateAndUseCoupon(int couponId, int uid, int oid, DateTime time, string ip)
        {
            BrnShop.Core.BSPData.RDBS.ActivateAndUseCoupon(couponId, uid, oid, time, ip);
        }

        /// <summary>
        /// 激活优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="uid">用户id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        public static void ActivateCoupon(int couponId, int uid, DateTime time, string ip)
        {
            BrnShop.Core.BSPData.RDBS.ActivateCoupon(couponId, uid, time, ip);
        }

        /// <summary>
        /// 退还订单使用的优惠劵
        /// </summary>
        /// <param name="oid">订单id</param>
        public static void ReturnUserOrderUseCoupons(int oid)
        {
            BrnShop.Core.BSPData.RDBS.ReturnUserOrderUseCoupons(oid);
        }

        /// <summary>
        /// 获得用户订单发放的优惠劵列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static List<CouponInfo> GetUserOrderSendCouponList(int oid)
        {
            List<CouponInfo> couponList = new List<CouponInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetUserOrderSendCouponList(oid);
            while (reader.Read())
            {
                CouponInfo couponInfo = BuildCouponFromReader(reader);
                couponList.Add(couponInfo);
            }

            reader.Close();
            return couponList;
        }



        /// <summary>
        /// 添加优惠劵商品
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pid">商品id</param>
        public static void AddCouponProduct(int couponTypeId, int pid)
        {
            BrnShop.Core.BSPData.RDBS.AddCouponProduct(couponTypeId, pid);
        }

        /// <summary>
        /// 删除优惠劵商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public static bool DeleteCouponProductByRecordId(string recordIdList)
        {
            return BrnShop.Core.BSPData.RDBS.DeleteCouponProductByRecordId(recordIdList);
        }

        /// <summary>
        /// 优惠劵商品是否已经存在
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistCouponProduct(int couponTypeId, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistCouponProduct(couponTypeId, pid);
        }

        /// <summary>
        /// 后台获得优惠劵商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static DataTable AdminGetCouponProductList(int pageSize, int pageNumber, int couponTypeId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponProductList(pageSize, pageNumber, couponTypeId);
        }

        /// <summary>
        /// 后台获得优惠劵商品数量
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public static int AdminGetCouponProductCount(int couponTypeId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCouponProductCount(couponTypeId);
        }

        /// <summary>
        /// 商品是否属于同一优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static bool IsSameCouponType(int couponTypeId, string pidList)
        {
            return BrnShop.Core.BSPData.RDBS.IsSameCouponType(couponTypeId, pidList);
        }
    }
}
