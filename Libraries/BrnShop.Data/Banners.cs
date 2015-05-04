using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// banner数据访问类
    /// </summary>
    public partial class Banners
    {
        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建BannerInfo
        /// </summary>
        public static BannerInfo BuildBannerFromReader(IDataReader reader)
        {
            BannerInfo bannerInfo = new BannerInfo();

            bannerInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
            bannerInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            bannerInfo.StartTime = TypeHelper.ObjectToDateTime(reader["starttime"]);
            bannerInfo.EndTime = TypeHelper.ObjectToDateTime(reader["endtime"]);
            bannerInfo.IsShow = TypeHelper.ObjectToInt(reader["isshow"]);
            bannerInfo.Title = reader["title"].ToString();
            bannerInfo.Img = reader["img"].ToString();
            bannerInfo.Url = reader["url"].ToString();
            bannerInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);

            return bannerInfo;
        }

        /// <summary>
        /// 从DataRow创建BannerInfo
        /// </summary>
        public static BannerInfo BuildBannerFromRow(DataRow row)
        {
            BannerInfo bannerInfo = new BannerInfo();

            bannerInfo.Id = TypeHelper.ObjectToInt(row["id"]);
            bannerInfo.Type = TypeHelper.ObjectToInt(row["type"]);
            bannerInfo.StartTime = TypeHelper.ObjectToDateTime(row["starttime"]);
            bannerInfo.EndTime = TypeHelper.ObjectToDateTime(row["endtime"]);
            bannerInfo.IsShow = TypeHelper.ObjectToInt(row["isshow"]);
            bannerInfo.Title = row["title"].ToString();
            bannerInfo.Img = row["img"].ToString();
            bannerInfo.Url = row["url"].ToString();
            bannerInfo.DisplayOrder = TypeHelper.ObjectToInt(row["displayorder"]);

            return bannerInfo;
        }

        #endregion

        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static BannerInfo[] GetHomeBannerList(int type, DateTime nowTime)
        {
            DataTable dt = BrnShop.Core.BSPData.RDBS.GetHomeBannerList(type, nowTime);
            BannerInfo[] bannerList = new BannerInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                BannerInfo bannerInfo = BuildBannerFromRow(row);
                bannerList[index] = bannerInfo;
                index++;
            }
            return bannerList;
        }

        /// <summary>
        /// 后台获得banner列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static List<BannerInfo> AdminGetBannerList(int pageSize, int pageNumber)
        {
            List<BannerInfo> bannerList = new List<BannerInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetBannerList(pageSize, pageNumber);
            while (reader.Read())
            {
                BannerInfo bannerInfo = BuildBannerFromReader(reader);
                bannerList.Add(bannerInfo);
            }
            reader.Close();
            return bannerList;
        }

        /// <summary>
        /// 后台获得banner数量
        /// </summary>
        /// <returns></returns>
        public static int AdminGetBannerCount()
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetBannerCount();
        }

        /// <summary>
        /// 后台获得banner
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static BannerInfo AdminGetBannerById(int id)
        {
            BannerInfo bannerInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetBannerById(id);
            if (reader.Read())
            {
                bannerInfo = BuildBannerFromReader(reader);
            }
            reader.Close();
            return bannerInfo;
        }

        /// <summary>
        /// 创建banner
        /// </summary>
        public static void CreateBanner(BannerInfo bannerInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateBanner(bannerInfo);
        }

        /// <summary>
        /// 更新banner
        /// </summary>
        public static void UpdateBanner(BannerInfo bannerInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateBanner(bannerInfo);
        }

        /// <summary>
        /// 删除banner
        /// </summary>
        /// <param name="idList">id列表</param>
        public static void DeleteBannerById(string idList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteBannerById(idList);
        }
    }
}
