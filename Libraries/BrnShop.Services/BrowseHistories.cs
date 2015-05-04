using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 浏览历史操作管理类
    /// </summary>
    public partial class BrowseHistories
    {
        /// <summary>
        /// 获得用户浏览历史
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetUserBrowseHistory(int uid, int pid)
        {
            int broHisCount = BSPConfig.ShopConfig.BroHisCount;
            if (broHisCount > 0)
            {
                List<PartProductInfo> partProductList = null;
                HttpCookie cookie = HttpContext.Current.Request.Cookies["brohis"];
                if (cookie != null)
                {
                    if (cookie.Value.Length > 0)
                    {
                        List<int> pidList = new List<int>();
                        string[] array = StringHelper.SplitString(cookie.Value);
                        foreach (string item in array)
                        {
                            int tempPid = TypeHelper.StringToInt(item);
                            if (tempPid > 0)
                            {
                                if (tempPid != pid)
                                    pidList.Add(tempPid);
                            }
                        }

                        int length = pidList.Count <= broHisCount ? pidList.Count : broHisCount;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < length; i++)
                            sb.AppendFormat("{0},", pidList[i]);
                        string pidListStr = sb.Length > 0 ? sb.Remove(sb.Length - 1, 1).ToString() : "";

                        partProductList = Products.GetPartProductList(pidListStr);
                        if (pidListStr.Length > 0)
                            cookie.Value = string.Format("{0},{1}", pid, pidListStr);
                        else
                            cookie.Value = pid.ToString();
                    }
                    else
                    {
                        partProductList = new List<PartProductInfo>();
                        cookie.Value = pid.ToString();
                    }
                }
                else
                {
                    partProductList = new List<PartProductInfo>();
                    cookie = new HttpCookie("brohis");
                    if (uid > 0)
                    {
                        List<PartProductInfo> userBrowseProductList = GetUserBrowseProductList(BSPConfig.ShopConfig.BroHisCount + 1, 1, uid);
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{0},", pid);
                        int temp = 1;
                        foreach (PartProductInfo partProductInfo in userBrowseProductList)
                        {
                            if (temp == broHisCount)
                                break;
                            if (partProductInfo.Pid != pid)
                            {
                                partProductList.Add(partProductInfo);
                                sb.AppendFormat("{0},", partProductInfo.Pid);
                                temp++;
                            }
                        }
                        cookie.Value = sb.Remove(sb.Length - 1, 1).ToString();
                    }
                    else
                    {
                        cookie.Value = pid.ToString();
                    }
                }

                cookie.Expires = DateTime.Now.AddDays(7);
                HttpContext.Current.Response.AppendCookie(cookie);

                return partProductList;
            }
            return null;
        }

        /// <summary>
        /// 获得用户浏览商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetUserBrowseProductList(int pageSize, int pageNumber, int uid)
        {
            return BrnShop.Data.BrowseHistories.GetUserBrowseProductList(pageSize, pageNumber, uid);
        }

        /// <summary>
        /// 获得用户浏览商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUserBrowseProductCount(int uid)
        {
            return BrnShop.Data.BrowseHistories.GetUserBrowseProductCount(uid);
        }

        /// <summary>
        /// 更新浏览历史
        /// </summary>
        /// <param name="state">UpdateBrowseHistoryState类型对象</param>
        /// <returns></returns>
        public static void UpdateBrowseHistory(object state)
        {
            UpdateBrowseHistoryState updateBrowseHistoryState = (UpdateBrowseHistoryState)state;
            BrnShop.Data.BrowseHistories.UpdateBrowseHistory(updateBrowseHistoryState.Uid, updateBrowseHistoryState.Pid, updateBrowseHistoryState.UpdateTime);
        }

        /// <summary>
        /// 清空过期浏览历史
        /// </summary>
        public static void ClearExpiredBrowseHistory()
        {
            BrnShop.Data.BrowseHistories.ClearExpiredBrowseHistory();
        }
    }
}
