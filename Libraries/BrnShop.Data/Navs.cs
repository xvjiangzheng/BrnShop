using System;
using System.Collections.Generic;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 导航栏数据访问类
    /// </summary>
    public partial class Navs
    {
        /// <summary>
        /// 获得导航栏列表
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetNavList()
        {
            List<NavInfo> navList = new List<NavInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetNavList();
            while (reader.Read())
            {
                NavInfo navInfo = new NavInfo();
                navInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
                navInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
                navInfo.Layer = TypeHelper.ObjectToInt(reader["layer"]);
                navInfo.Name = reader["name"].ToString();
                navInfo.Title = reader["title"].ToString();
                navInfo.Url = reader["url"].ToString();
                navInfo.Target = TypeHelper.ObjectToInt(reader["target"]);
                navInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
                navList.Add(navInfo);
            }
            reader.Close();
            return navList;
        }

        /// <summary>
        /// 创建导航栏
        /// </summary>
        public static void CreateNav(NavInfo navInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateNav(navInfo);
        }

        /// <summary>
        /// 删除导航栏
        /// </summary>
        /// <param name="id">导航栏id</param>
        public static void DeleteNavById(int id)
        {
            BrnShop.Core.BSPData.RDBS.DeleteNavById(id);
        }

        /// <summary>
        /// 更新导航栏
        /// </summary>
        public static void UpdateNav(NavInfo navInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateNav(navInfo);
        }
    }
}
