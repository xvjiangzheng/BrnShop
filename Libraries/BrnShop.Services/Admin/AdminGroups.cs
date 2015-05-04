using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 管理员组操作管理类
    /// </summary>
    public partial class AdminGroups
    {
        //后台导航菜单栏缓存文件夹
        private const string AdminNavMeunCacheFolder = "/administration/cache/menu";

        /// <summary>
        /// 检查当前动作的授权
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <param name="controller">控制器名称</param>
        /// <param name="action">动作方法名称</param>
        /// <returns></returns>
        public static bool CheckAuthority(int adminGid, string controller, string pageKey)
        {
            //非管理员
            if (adminGid == 1)
                return false;

            //系统管理员具有一切权限
            if (adminGid == 2)
                return true;

            HashSet<string> adminActionHashSet = AdminActions.GetAdminActionHashSet();
            HashSet<string> adminGroupActionHashSet = GetAdminGroupActionHashSet(adminGid);

            //动作方法的优先级大于控制器的优先级
            if ((adminActionHashSet.Contains(pageKey) && adminGroupActionHashSet.Contains(pageKey)) ||
                                    (adminActionHashSet.Contains(controller) && adminGroupActionHashSet.Contains(controller)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获得管理员组操作HashSet
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <returns></returns>
        public static HashSet<string> GetAdminGroupActionHashSet(int adminGid)
        {
            HashSet<string> actionHashSet = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_ADMINGROUP_ACTIONHASHSET + adminGid) as HashSet<string>;
            if (actionHashSet == null)
            {
                AdminGroupInfo adminGroupInfo = GetAdminGroupById(adminGid);
                if (adminGroupInfo != null)
                {
                    actionHashSet = new HashSet<string>();
                    string[] actionList = StringHelper.SplitString(adminGroupInfo.ActionList);//将动作列表字符串分隔成动作列表
                    foreach (string action in actionList)
                    {
                        actionHashSet.Add(action);
                    }
                    BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_ADMINGROUP_ACTIONHASHSET + adminGid, actionHashSet);
                }
            }
            return actionHashSet;
        }

        /// <summary>
        /// 获得管理员组列表
        /// </summary>
        /// <returns></returns>
        public static AdminGroupInfo[] GetAdminGroupList()
        {
            AdminGroupInfo[] adminGroupList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_ADMINGROUP_LIST) as AdminGroupInfo[];
            if (adminGroupList == null)
            {
                adminGroupList = BrnShop.Data.AdminGroups.GetAdminGroupList();
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_ADMINGROUP_LIST, adminGroupList);
            }
            return adminGroupList;
        }

        /// <summary>
        /// 获得用户级管理员组列表
        /// </summary>
        /// <returns></returns>
        public static AdminGroupInfo[] GetCustomerAdminGroupList()
        {
            AdminGroupInfo[] adminGroupList = GetAdminGroupList();
            AdminGroupInfo[] customerAdminGroupList = new AdminGroupInfo[adminGroupList.Length - 2];

            int i = 0;
            foreach (AdminGroupInfo adminGroupInfo in adminGroupList)
            {
                if (adminGroupInfo.AdminGid > 2)
                {
                    customerAdminGroupList[i] = adminGroupInfo;
                    i++;
                }
            }

            return customerAdminGroupList;
        }

        /// <summary>
        /// 获得管理员组
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <returns></returns>
        public static AdminGroupInfo GetAdminGroupById(int adminGid)
        {
            foreach (AdminGroupInfo adminGroupInfo in GetAdminGroupList())
            {
                if (adminGid == adminGroupInfo.AdminGid)
                    return adminGroupInfo;
            }
            return null;
        }

        /// <summary>
        /// 获得管理员组id
        /// </summary>
        /// <param name="title">管理员组标题</param>
        /// <returns></returns>
        public static int GetAdminGroupIdByTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                foreach (AdminGroupInfo adminGroupInfo in GetAdminGroupList())
                {
                    if (adminGroupInfo.Title == title)
                        return adminGroupInfo.AdminGid;
                }
            }
            return -1;
        }

        /// <summary>
        /// 创建管理员组
        /// </summary>
        /// <param name="adminGroupInfo">管理员组信息</param>
        public static void CreateAdminGroup(AdminGroupInfo adminGroupInfo)
        {
            adminGroupInfo.ActionList = adminGroupInfo.ActionList.ToLower();
            int adminGid = BrnShop.Data.AdminGroups.CreateAdminGroup(adminGroupInfo);
            if (adminGid > 0)
            {
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_ADMINGROUP_LIST);
                adminGroupInfo.AdminGid = adminGid;
                WriteAdminNavMenuCache(adminGroupInfo);
            }
        }

        /// <summary>
        /// 删除管理员组
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <returns>-2代表内置管理员不能删除，-1代表此管理员组下还有会员未删除，0代表删除失败，1代表删除成功</returns>
        public static int DeleteAdminGroupById(int adminGid)
        {
            if (adminGid < 3)
                return -2;

            if (AdminUsers.GetUserCountByAdminGid(adminGid) > 0)
                return -1;

            if (adminGid > 0)
            {
                BrnShop.Data.AdminGroups.DeleteAdminGroupById(adminGid);
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_ADMINGROUP_ACTIONHASHSET + adminGid);
                BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_ADMINGROUP_LIST);
                File.Delete(IOHelper.GetMapPath(AdminNavMeunCacheFolder + "/" + adminGid + ".js"));
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新管理员组
        /// </summary>
        public static void UpdateAdminGroup(AdminGroupInfo adminGroupInfo)
        {
            adminGroupInfo.ActionList = adminGroupInfo.ActionList.ToLower();
            BrnShop.Data.AdminGroups.UpdateAdminGroup(adminGroupInfo);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_ADMINGROUP_ACTIONHASHSET + adminGroupInfo.AdminGid);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_ADMINGROUP_LIST);
            WriteAdminNavMenuCache(adminGroupInfo);
        }

        /// <summary>
        /// 将管理员组的导航菜单栏缓存写入到文件中
        /// </summary>
        private static void WriteAdminNavMenuCache(AdminGroupInfo adminGroupInfo)
        {
            HashSet<string> adminGroupActionHashSet = new HashSet<string>();
            string[] actionList = StringHelper.SplitString(adminGroupInfo.ActionList);//将后台操作列表字符串分隔成后台操作列表
            foreach (string action in actionList)
            {
                adminGroupActionHashSet.Add(action);
            }

            bool flag = false;
            StringBuilder menu = new StringBuilder();
            StringBuilder menuList = new StringBuilder("var menuList = [");

            #region 商品管理

            menu.AppendFormat("{0}\"title\":\"商品管理\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("product"))
            {
                menu.AppendFormat("{0}\"title\":\"添加商品\",\"url\":\"/admin/product/addproduct\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"添加SKU\",\"url\":\"/admin/product/addsku\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"在售商品\",\"url\":\"/admin/product/onsaleproductlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"下架商品\",\"url\":\"/admin/product/outsaleproductlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"签名商品\",\"url\":\"/admin/product/signproductlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"定时商品\",\"url\":\"/admin/product/timeproductlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"回收站\",\"url\":\"/admin/product/recyclebinproductlist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 促销活动

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"促销活动\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("promotion"))
            {
                menu.AppendFormat("{0}\"title\":\"单品促销\",\"url\":\"/admin/promotion/singlepromotionlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"买送促销\",\"url\":\"/admin/promotion/buysendpromotionlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"赠品促销\",\"url\":\"/admin/promotion/giftpromotionlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"套装促销\",\"url\":\"/admin/promotion/suitpromotionlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"满赠促销\",\"url\":\"/admin/promotion/fullsendpromotionlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"满减促销\",\"url\":\"/admin/promotion/fullcutpromotionlist\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("topic"))
            {
                menu.AppendFormat("{0}\"title\":\"活动专题\",\"url\":\"/admin/topic/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("coupon"))
            {
                menu.AppendFormat("{0}\"title\":\"优惠劵\",\"url\":\"/admin/coupon/coupontypelist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 订单管理

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"订单管理\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("order"))
            {
                menu.AppendFormat("{0}\"title\":\"订单列表\",\"url\":\"/admin/order/orderlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"退款列表\",\"url\":\"/admin/order/refundlist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 咨询评价

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"咨询评价\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("productreview"))
            {
                menu.AppendFormat("{0}\"title\":\"商品评价\",\"url\":\"/admin/productreview/productreviewlist\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("productconsult"))
            {
                menu.AppendFormat("{0}\"title\":\"商品咨询\",\"url\":\"/admin/productconsult/productconsultlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"咨询类型\",\"url\":\"/admin/productconsult/productconsulttypelist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 用户管理

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"用户管理\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("user"))
            {
                menu.AppendFormat("{0}\"title\":\"用户列表\",\"url\":\"/admin/user/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("userrank"))
            {
                menu.AppendFormat("{0}\"title\":\"会员等级\",\"url\":\"/admin/userrank/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("admingroup"))
            {
                menu.AppendFormat("{0}\"title\":\"管理员组\",\"url\":\"/admin/admingroup/list\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 新闻管理

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"新闻管理\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("news"))
            {
                menu.AppendFormat("{0}\"title\":\"新闻类型\",\"url\":\"/admin/news/newstypelist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"新闻列表\",\"url\":\"/admin/news/newslist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 广告管理

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"广告管理\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("advert"))
            {
                menu.AppendFormat("{0}\"title\":\"广告位置\",\"url\":\"/admin/advert/advertpositionlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"广告列表\",\"url\":\"/admin/advert/advertlist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 商城内容

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"商城内容\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("nav"))
            {
                menu.AppendFormat("{0}\"title\":\"导航菜单\",\"url\":\"/admin/nav/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("help"))
            {
                menu.AppendFormat("{0}\"title\":\"商城帮助\",\"url\":\"/admin/help/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("friendlink"))
            {
                menu.AppendFormat("{0}\"title\":\"友情链接\",\"url\":\"/admin/friendlink/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("banner"))
            {
                menu.AppendFormat("{0}\"title\":\"Banner\",\"url\":\"/admin/banner/list\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 商品性质

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"商品性质\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("brand"))
            {
                menu.AppendFormat("{0}\"title\":\"商品品牌\",\"url\":\"/admin/brand/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("category"))
            {
                menu.AppendFormat("{0}\"title\":\"分类管理\",\"url\":\"/admin/category/categorylist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 报表统计

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"报表统计\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("stat"))
            {
                menu.AppendFormat("{0}\"title\":\"在线用户\",\"url\":\"/admin/stat/onlineuserlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"搜索分析\",\"url\":\"/admin/stat/searchwordstatlist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"商品统计\",\"url\":\"/admin/stat/productstat\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"销售明细\",\"url\":\"/admin/stat/salelist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"销售趋势\",\"url\":\"/admin/stat/saletrend\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"地区统计\",\"url\":\"/admin/stat/regionstat\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"客户端统计\",\"url\":\"/admin/stat/clientstat\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 系统设置

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"系统设置\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("brand"))
            {
                menu.AppendFormat("{0}\"title\":\"站点信息\",\"url\":\"/admin/set/site\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"商城设置\",\"url\":\"/admin/set/shop\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"主题管理\",\"url\":\"/admin/set/theme\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"账号设置\",\"url\":\"/admin/set/account\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"上传设置\",\"url\":\"/admin/set/upload\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"性能设置\",\"url\":\"/admin/set/performance\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"访问控制\",\"url\":\"/admin/set/access\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"邮箱设置\",\"url\":\"/admin/set/email\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"短信设置\",\"url\":\"/admin/set/sms\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"积分设置\",\"url\":\"/admin/set/credit\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"打印订单\",\"url\":\"/admin/set/printorder\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("bannedip"))
            {
                menu.AppendFormat("{0}\"title\":\"禁止IP\",\"url\":\"/admin/bannedip/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("filterword"))
            {
                menu.AppendFormat("{0}\"title\":\"筛选词\",\"url\":\"/admin/filterword/list\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 商城插件

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"商城插件\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("plugin"))
            {
                menu.AppendFormat("{0}\"title\":\"授权插件\",\"url\":\"/admin/plugin/list?type=0\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"支付插件\",\"url\":\"/admin/plugin/list?type=1\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"配送插件\",\"url\":\"/admin/plugin/list?type=2\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 日志管理

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"日志管理\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("log"))
            {
                menu.AppendFormat("{0}\"title\":\"后台日志\",\"url\":\"/admin/log/adminoperateloglist\"{1},", "{", "}");
                menu.AppendFormat("{0}\"title\":\"积分日志\",\"url\":\"/admin/log/creditloglist\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            #region 开发人员

            flag = false;
            menu = menu.Clear();
            menu.AppendFormat("{0}\"title\":\"开发人员\",\"subMenuList\":[", "{");
            if (adminGroupActionHashSet.Contains("event"))
            {
                menu.AppendFormat("{0}\"title\":\"事件管理\",\"url\":\"/admin/event/list\"{1},", "{", "}");
                flag = true;
            }
            if (adminGroupActionHashSet.Contains("database"))
            {
                menu.AppendFormat("{0}\"title\":\"数据库管理\",\"url\":\"/admin/database/manage\"{1},", "{", "}");
                flag = true;
            }
            if (flag)
            {
                menu.Remove(menu.Length - 1, 1);
                menu.Append("]},");
                menuList.Append(menu.ToString());
            }

            #endregion

            if (menuList.Length > 16)
                menuList.Remove(menuList.Length - 1, 1);
            menuList.Append("]");

            try
            {
                string fileName = IOHelper.GetMapPath(AdminNavMeunCacheFolder + "/" + adminGroupInfo.AdminGid + ".js");
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes(menuList.ToString());
                    fs.Write(info, 0, info.Length);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch
            { }
        }
    }
}
