using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 友情链接数据访问类
    /// </summary>
    public partial class FriendLinks
    {
        /// <summary>
        /// 获得友情链接列表
        /// </summary>
        public static FriendLinkInfo[] GetFriendLinkList()
        {
            DataTable dt = BrnShop.Core.BSPData.RDBS.GetFriendLinkList();
            FriendLinkInfo[] friendLinkList = new FriendLinkInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                FriendLinkInfo friendLinkInfo = new FriendLinkInfo();
                friendLinkInfo.Id = TypeHelper.ObjectToInt(row["id"]);
                friendLinkInfo.Name = row["name"].ToString();
                friendLinkInfo.Title = row["title"].ToString();
                friendLinkInfo.Logo = row["logo"].ToString();
                friendLinkInfo.Url = row["url"].ToString();
                friendLinkInfo.Target = TypeHelper.ObjectToInt(row["target"]);
                friendLinkInfo.DisplayOrder = TypeHelper.ObjectToInt(row["displayorder"]);
                friendLinkList[index] = friendLinkInfo;
                index++;
            }
            return friendLinkList;
        }

        /// <summary>
        /// 创建友情链接
        /// </summary>
        public static void CreateFriendLink(FriendLinkInfo friendLinkInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateFriendLink(friendLinkInfo);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="idList">友情链接id</param>
        public static void DeleteFriendLinkById(string idList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteFriendLinkById(idList);
        }

        /// <summary>
        /// 更新友情链接
        /// </summary>
        public static void UpdateFriendLink(FriendLinkInfo friendLinkInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateFriendLink(friendLinkInfo);
        }
    }
}
