using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 帮助数据访问类
    /// </summary>
    public partial class Helps
    {
        /// <summary>
        /// 创建帮助
        /// </summary>
        public static void CreateHelp(HelpInfo helpInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateHelp(helpInfo);
        }

        /// <summary>
        /// 删除帮助
        /// </summary>
        /// <param name="id">帮助id</param>
        public static void DeleteHelpById(int id)
        {
            BrnShop.Core.BSPData.RDBS.DeleteHelpById(id);
        }

        /// <summary>
        /// 更新帮助
        /// </summary>
        public static void UpdateHelp(HelpInfo helpInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateHelp(helpInfo);
        }

        /// <summary>
        /// 获得帮助列表
        /// </summary>
        /// <returns></returns>
        public static List<HelpInfo> GetHelpList()
        {
            List<HelpInfo> helplist = new List<HelpInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetHelpList();

            while (reader.Read())
            {
                HelpInfo helpInfo = new HelpInfo();
                helpInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
                helpInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
                helpInfo.Title = reader["title"].ToString();
                helpInfo.Url = reader["url"].ToString();
                helpInfo.Description = reader["description"].ToString();
                helpInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
                helplist.Add(helpInfo);
            }
            reader.Close();
            return helplist;
        }

        /// <summary>
        /// 更新帮助排序
        /// </summary>
        /// <param name="id">帮助id</param>
        /// <param name="displayOrder">排序</param>
        /// <returns></returns>
        public static bool UpdateHelpDisplayOrder(int id, int displayOrder)
        {
            return BrnShop.Core.BSPData.RDBS.UpdateHelpDisplayOrder(id, displayOrder);
        }
    }
}
