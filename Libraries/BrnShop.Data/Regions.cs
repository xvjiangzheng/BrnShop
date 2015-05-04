using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 区域数据访问类
    /// </summary>
    public partial class Regions
    {
        #region 辅助方法

        /// <summary>
        /// 构建区域信息
        /// </summary>
        public static RegionInfo BuildRegionFromRow(DataRow row)
        {
            RegionInfo regionInfo = new RegionInfo();

            regionInfo.RegionId = TypeHelper.ObjectToInt(row["regionid"]);
            regionInfo.Name = row["name"].ToString();
            regionInfo.Spell = row["spell"].ToString();
            regionInfo.ShortSpell = row["shortspell"].ToString();
            regionInfo.DisplayOrder = TypeHelper.ObjectToInt(row["displayorder"]);
            regionInfo.ParentId = TypeHelper.ObjectToInt(row["parentid"]);
            regionInfo.Layer = TypeHelper.ObjectToInt(row["layer"]);
            regionInfo.ProvinceId = TypeHelper.ObjectToInt(row["provinceid"]);
            regionInfo.ProvinceName = row["provincename"].ToString();
            regionInfo.CityId = TypeHelper.ObjectToInt(row["cityid"]);
            regionInfo.CityName = row["cityname"].ToString();

            return regionInfo;
        }

        /// <summary>
        /// 构建区域信息
        /// </summary>
        public static RegionInfo BuildRegionFromReader(IDataReader reader)
        {
            RegionInfo regionInfo = new RegionInfo();

            regionInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            regionInfo.Name = reader["name"].ToString();
            regionInfo.Spell = reader["spell"].ToString();
            regionInfo.ShortSpell = reader["shortspell"].ToString();
            regionInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            regionInfo.ParentId = TypeHelper.ObjectToInt(reader["parentid"]);
            regionInfo.Layer = TypeHelper.ObjectToInt(reader["layer"]);
            regionInfo.ProvinceId = TypeHelper.ObjectToInt(reader["provinceid"]);
            regionInfo.ProvinceName = reader["provincename"].ToString();
            regionInfo.CityId = TypeHelper.ObjectToInt(reader["cityid"]);
            regionInfo.CityName = reader["cityname"].ToString();

            return regionInfo;
        }

        #endregion

        /// <summary>
        /// 获得全部区域
        /// </summary>
        /// <returns></returns>
        public static List<RegionInfo> GetAllRegion()
        {
            DataTable dt = BrnShop.Core.BSPData.RDBS.GetAllRegion();
            List<RegionInfo> regionList = new List<RegionInfo>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
            {
                RegionInfo regionInfo = BuildRegionFromRow(row);
                regionList.Add(regionInfo);
            }
            dt.Dispose();
            return regionList;
        }

        /// <summary>
        /// 获得区域列表
        /// </summary>
        /// <param name="parentId">父id</param>
        /// <returns></returns>
        public static List<RegionInfo> GetRegionList(int parentId)
        {
            List<RegionInfo> regionList = new List<RegionInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetRegionList(parentId);
            while (reader.Read())
            {
                RegionInfo regionInfo = BuildRegionFromReader(reader);
                regionList.Add(regionInfo);
            }
            reader.Close();
            return regionList;
        }

        /// <summary>
        /// 获得区域
        /// </summary>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        public static RegionInfo GetRegionById(int regionId)
        {
            RegionInfo regionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetRegionById(regionId);
            if (reader.Read())
            {
                regionInfo = BuildRegionFromReader(reader);
            }
            reader.Close();
            return regionInfo;
        }

        /// <summary>
        /// 获得区域
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="layer">级别</param>
        /// <returns></returns>
        public static RegionInfo GetRegionByNameAndLayer(string name, int layer)
        {
            RegionInfo regionInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetRegionByNameAndLayer(name, layer);
            if (reader.Read())
            {
                regionInfo = BuildRegionFromReader(reader);
            }
            reader.Close();
            return regionInfo;
        }
    }
}
