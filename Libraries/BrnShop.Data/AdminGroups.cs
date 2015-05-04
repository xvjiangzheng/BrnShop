using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 管理员组数据访问类
    /// </summary>
    public partial class AdminGroups
    {
        /// <summary>
        /// 获得管理员组列表
        /// </summary>
        /// <returns></returns>
        public static AdminGroupInfo[] GetAdminGroupList()
        {
            DataTable dt = BrnShop.Core.BSPData.RDBS.GetAdminGroupList();
            AdminGroupInfo[] adminGroupList = new AdminGroupInfo[dt.Rows.Count];
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                AdminGroupInfo adminGroupInfo = new AdminGroupInfo();
                adminGroupInfo.AdminGid = TypeHelper.ObjectToInt(dr["admingid"]);
                adminGroupInfo.Title = dr["title"].ToString();
                adminGroupInfo.ActionList = dr["actionlist"].ToString();
                adminGroupList[index] = adminGroupInfo;
                index++;
            }
            return adminGroupList;
        }

        /// <summary>
        /// 创建管理员组
        /// </summary>
        /// <param name="adminGroupInfo">管理员组信息</param>
        /// <returns></returns>
        public static int CreateAdminGroup(AdminGroupInfo adminGroupInfo)
        {
            return BrnShop.Core.BSPData.RDBS.CreateAdminGroup(adminGroupInfo);
        }

        /// <summary>
        /// 删除管理员组
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        public static void DeleteAdminGroupById(int adminGid)
        {
            BrnShop.Core.BSPData.RDBS.DeleteAdminGroupById(adminGid);
        }

        /// <summary>
        /// 更新管理员组
        /// </summary>
        public static void UpdateAdminGroup(AdminGroupInfo adminGroupInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateAdminGroup(adminGroupInfo);
        }
    }
}
