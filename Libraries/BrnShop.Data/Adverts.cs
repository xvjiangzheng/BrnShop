using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 广告数据访问类
    /// </summary>
    public partial class Adverts
    {
        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建AdvertPositionInfo
        /// </summary>
        public static AdvertPositionInfo BuildAdvertPositionFromReader(IDataReader reader)
        {
            AdvertPositionInfo advertPositionInfo = new AdvertPositionInfo();

            advertPositionInfo.AdPosId = TypeHelper.ObjectToInt(reader["adposid"]);
            advertPositionInfo.Title = reader["title"].ToString();
            advertPositionInfo.Description = reader["description"].ToString();

            return advertPositionInfo;
        }

        /// <summary>
        /// 从IDataReader创建AdvertInfo
        /// </summary>
        public static AdvertInfo BuildAdvertFromReader(IDataReader reader)
        {
            AdvertInfo advertInfo = new AdvertInfo();

            advertInfo.AdId = TypeHelper.ObjectToInt(reader["adid"]);
            advertInfo.ClickCount = TypeHelper.ObjectToInt(reader["clickcount"]);
            advertInfo.AdPosId = TypeHelper.ObjectToInt(reader["adposid"]);
            advertInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            advertInfo.StartTime = TypeHelper.ObjectToDateTime(reader["starttime"]);
            advertInfo.EndTime = TypeHelper.ObjectToDateTime(reader["endtime"]);
            advertInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            advertInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            advertInfo.Title = reader["title"].ToString();
            advertInfo.Url = reader["url"].ToString();
            advertInfo.Body = reader["body"].ToString();
            advertInfo.ExtField1 = reader["extfield1"].ToString();
            advertInfo.ExtField2 = reader["extfield2"].ToString();
            advertInfo.ExtField3 = reader["extfield3"].ToString();
            advertInfo.ExtField4 = reader["extfield4"].ToString();
            advertInfo.ExtField5 = reader["extfield5"].ToString();

            return advertInfo;
        }

        #endregion

        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static List<AdvertPositionInfo> GetAdvertPositionList(int pageSize, int pageNumber)
        {
            List<AdvertPositionInfo> advertPositionList = new List<AdvertPositionInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAdvertPositionList(pageSize, pageNumber);
            while (reader.Read())
            {
                AdvertPositionInfo advertPositionInfo = BuildAdvertPositionFromReader(reader);
                advertPositionList.Add(advertPositionInfo);
            }
            reader.Close();
            return advertPositionList;
        }

        /// <summary>
        /// 获得广告位置数量
        /// </summary>
        /// <returns></returns>
        public static int GetAdvertPositionCount()
        {
            return BrnShop.Core.BSPData.RDBS.GetAdvertPositionCount();
        }

        /// <summary>
        /// 获得全部广告位置
        /// </summary>
        /// <returns></returns>
        public static List<AdvertPositionInfo> GetAllAdvertPosition()
        {
            List<AdvertPositionInfo> advertPositionList = new List<AdvertPositionInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAllAdvertPosition();
            while (reader.Read())
            {
                AdvertPositionInfo advertPositionInfo = BuildAdvertPositionFromReader(reader);
                advertPositionList.Add(advertPositionInfo);
            }
            reader.Close();
            return advertPositionList;
        }

        /// <summary>
        /// 创建广告位置
        /// </summary>
        public static void CreateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateAdvertPosition(advertPositionInfo);
        }

        /// <summary>
        /// 更新广告位置
        /// </summary>
        public static void UpdateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateAdvertPosition(advertPositionInfo);
        }

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static AdvertPositionInfo GetAdvertPositionById(int adPosId)
        {
            AdvertPositionInfo advertPositionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAdvertPositionById(adPosId);
            if (reader.Read())
            {
                advertPositionInfo = BuildAdvertPositionFromReader(reader);
            }
            reader.Close();
            return advertPositionInfo;
        }

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        public static void DeleteAdvertPositionById(int adPosId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteAdvertPositionById(adPosId);
        }





        /// <summary>
        /// 创建广告
        /// </summary>
        public static void CreateAdvert(AdvertInfo advertInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateAdvert(advertInfo);
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        public static void UpdateAdvert(AdvertInfo advertInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateAdvert(advertInfo);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adId">广告id</param>
        public static void DeleteAdvertById(int adId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteAdvertById(adId);
        }

        /// <summary>
        /// 后台获得广告
        /// </summary>
        /// <param name="adId">广告id</param>
        /// <returns></returns>
        public static AdvertInfo AdminGetAdvertById(int adId)
        {
            AdvertInfo advertInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetAdvertById(adId);
            if (reader.Read())
            {
                advertInfo = BuildAdvertFromReader(reader);
            }
            reader.Close();
            return advertInfo;
        }

        /// <summary>
        /// 后台获得广告列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static DataTable AdminGetAdvertList(int pageSize, int pageNumber, int adPosId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetAdvertList(pageSize, pageNumber, adPosId);
        }

        /// <summary>
        /// 后台获得广告数量
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static int AdminGetAdvertCount(int adPosId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetAdvertCount(adPosId);
        }

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<AdvertInfo> GetAdvertList(int adPosId, DateTime nowTime)
        {
            List<AdvertInfo> advertList = new List<AdvertInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAdvertList(adPosId, nowTime);
            while (reader.Read())
            {
                AdvertInfo advertInfo = BuildAdvertFromReader(reader);
                advertList.Add(advertInfo);
            }
            reader.Close();
            return advertList;
        }
    }
}
