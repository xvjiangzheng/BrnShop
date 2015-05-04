using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 开放授权数据访问类
    /// </summary>
    public partial class OAuths
    {
        private static IUserNOSQLStrategy _usernosql = BSPData.UserNOSQL;//用户非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建OAuthInfo
        /// </summary>
        public static OAuthInfo BuildOAuthFromReader(IDataReader reader)
        {
            OAuthInfo oauthInfo = new OAuthInfo();

            oauthInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
            oauthInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            oauthInfo.OpenId = reader["openid"].ToString();
            oauthInfo.Server = reader["server"].ToString();

            return oauthInfo;
        }

        #endregion

        /// <summary>
        /// 创建开放授权用户
        /// </summary>
        /// <param name="oauthInfo">开放授权信息</param>
        /// <returns></returns>
        public static bool CreateOAuthUser(OAuthInfo oauthInfo)
        {
            return BrnShop.Core.BSPData.RDBS.CreateOAuthUser(oauthInfo);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <returns></returns>
        public static int GetUidByOpenIdAndServer(string openId, string server)
        {
            int uid = -1;
            if (_usernosql != null)
            {
                uid = _usernosql.GetUidByOpenIdAndServer(openId, server);
                if (uid < 1)
                {
                    uid = BrnShop.Core.BSPData.RDBS.GetUidByOpenIdAndServer(openId, server);
                    if (uid > 0)
                        _usernosql.CreateOAuthUid(uid, openId, server);
                }
            }
            else
            {
                uid = BrnShop.Core.BSPData.RDBS.GetUidByOpenIdAndServer(openId, server);
            }
            return uid;
        }

        /// <summary>
        /// 获得开放授权用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static OAuthInfo GetOAuthUserByUid(int uid)
        {
            OAuthInfo oauthInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOAuthUserByUid(uid);
            if (reader.Read())
            {
                oauthInfo = BuildOAuthFromReader(reader);
            }

            reader.Close();
            return oauthInfo;
        }

        /// <summary>
        /// 获得开放授权用户列表
        /// </summary>
        /// <param name="uidList">用户id列表</param>
        /// <returns></returns>
        public static List<OAuthInfo> GetOAuthUserList(string uidList)
        {
            List<OAuthInfo> oauthList = new List<OAuthInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOAuthUserList(uidList);
            while (reader.Read())
            {
                OAuthInfo oauthInfo = BuildOAuthFromReader(reader);
                oauthList.Add(oauthInfo);
            }

            reader.Close();
            return oauthList;
        }
    }
}
