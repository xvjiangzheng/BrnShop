using System;
using System.Text;
using System.Data;
using System.Data.Common;

using BrnShop.Core;

namespace BrnShop.RDBSStrategy.SqlServer
{
    /// <summary>
    /// SqlServer策略之用户分部类
    /// </summary>
    public partial class RDBSStrategy : IRDBSStrategy
    {
        #region 在线用户

        /// <summary>
        /// 创建在线用户
        /// </summary>
        public int CreateOnlineUser(OnlineUserInfo onlineUserInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4,onlineUserInfo.Uid),
									   GenerateInParam("@sid",SqlDbType.Char,16,onlineUserInfo.Sid),
                                       GenerateInParam("@nickname",SqlDbType.NChar,20,onlineUserInfo.NickName),	
                                       GenerateInParam("@ip",SqlDbType.Char,15,onlineUserInfo.IP),	
                                       GenerateInParam("@regionid",SqlDbType.SmallInt,2,onlineUserInfo.RegionId),	
									   GenerateInParam("@updatetime",SqlDbType.DateTime,8,onlineUserInfo.UpdateTime)
								   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}createonlineuser", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 更新在线用户ip
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">ip</param>
        public void UpdateOnlineUserIP(int olId, string ip)
        {
            DbParameter[] parms = {
									   GenerateInParam("@ip",SqlDbType.Char,15,ip),
									   GenerateInParam("@olid",SqlDbType.Int,4,olId)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{}updateonlineuserip", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新在线用户uid
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">uid</param>
        public void UpdateOnlineUserUid(int olId, int uid)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4,uid),
									   GenerateInParam("@olid",SqlDbType.Int,4,olId)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateonlineuseruid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 获得在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <returns></returns>
        public IDataReader GetOnlineUserBySid(string sid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@sid",SqlDbType.Char,16,sid)
                                  };

            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getonlineuserbysid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="userType">在线用户类型</param>
        /// <returns></returns>
        public int GetOnlineUserCount(int userType)
        {
            DbParameter[] parms = {
                                      GenerateInParam("@usertype",SqlDbType.Int,4,userType)
                                   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getonlineuercount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        public void DeleteOnlineUserBySid(string sid)
        {
            DbParameter[] parms = { 
                                        GenerateInParam("@sid", SqlDbType.Char, 16, sid)
                                    };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}deleteonlineuserbysid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 删除过期在线用户
        /// </summary>
        /// <param name="onlineUserExpire">过期时间</param>
        public void DeleteExpiredOnlineUser(int onlineUserExpire)
        {
            DbParameter[] parms = { 
                                    GenerateInParam("@expiretime", SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(onlineUserExpire * -1))
                                  };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}deleteexpiredonlineuser", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 重置在线用户表
        /// </summary>
        public void ResetOnlineUserTable()
        {
            RDBSHelper.ExecuteNonQuery(CommandType.Text,
                                       string.Format("TRUNCATE TABLE [{0}onlineusers]",
                                       RDBSHelper.RDBSTablePre));
        }

        /// <summary>
        /// 获得在线用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public IDataReader GetOnlineUserList(int pageSize, int pageNumber, int locationType, int locationId, string sort)
        {
            string condition = GetOnlineUserListCondition(locationType, locationId);
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {2} FROM [{1}onlineusers] ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ONLINE_USERS,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}onlineusers] WHERE {2} ORDER BY {4}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                condition,
                                                RDBSFields.ONLINE_USERS,
                                                sort);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}onlineusers] WHERE [olid] NOT IN (SELECT TOP {2} [olid] FROM [{1}onlineusers] ORDER BY {4}) ORDER BY {4}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                RDBSFields.ONLINE_USERS,
                                                sort);
                else
                    commandText = string.Format("SELECT TOP {0} {4} FROM [{1}onlineusers] WHERE [olid] NOT IN (SELECT TOP {2} [olid] FROM [{1}onlineusers] WHERE {3} ORDER BY {5}) AND {3} ORDER BY {5}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                condition,
                                                RDBSFields.ONLINE_USERS,
                                                sort);
            }

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得在线用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string GetOnlineUserListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[olid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        public int GetOnlineUserCount(int locationType, int locationId)
        {
            string condition = GetOnlineUserListCondition(locationType, locationId);
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(olid) FROM [{0}onlineusers]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(olid) FROM [{0}onlineusers] WHERE {1}", RDBSHelper.RDBSTablePre, condition);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得在线用户列表条件
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        private string GetOnlineUserListCondition(int locationType, int locationId)
        {
            if (locationId > 0)
            {
                if (locationType == 0)
                {
                    return string.Format(" [regionid] IN (SELECT [regionid] FROM [{0}regions] WHERE [provinceid]={1})", RDBSHelper.RDBSTablePre, locationId);
                }
                else if (locationType == 1)
                {
                    return string.Format(" [regionid] IN (SELECT [regionid] FROM [{0}regions] WHERE [cityid]={1})", RDBSHelper.RDBSTablePre, locationId);
                }
                else if (locationType == 2)
                {
                    return string.Format(" [regionid]={0}", locationId);
                }
            }

            return "";
        }

        #endregion

        #region 开放授权

        /// <summary>
        /// 创建开放授权用户
        /// </summary>
        /// <param name="oauthInfo">开放授权信息</param>
        /// <returns></returns>
        public bool CreateOAuthUser(OAuthInfo oauthInfo)
        {
            DbParameter[] parms = {
									GenerateInParam("@uid",SqlDbType.Int,4,oauthInfo.Uid),
									GenerateInParam("@openid",SqlDbType.Char,50,oauthInfo.OpenId),
                                    GenerateInParam("@server",SqlDbType.Char,10,oauthInfo.Server)	
								   };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}createoauthuser", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <returns></returns>
        public int GetUidByOpenIdAndServer(string openId, string server)
        {
            DbParameter[] parms = {
									GenerateInParam("@openid",SqlDbType.Char,50,openId),
                                    GenerateInParam("@server",SqlDbType.Char,10,server)	
								   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuidbyopenidandserver", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得开放授权用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetOAuthUserByUid(int uid)
        {
            DbParameter[] parms = {
									GenerateInParam("@uid",SqlDbType.Int,4,uid)	
								   };
            string commandText = string.Format("SELECT {1} FROM [{0}oauth] WHERE [uid]=@uid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.OAUTH);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得开放授权用户列表
        /// </summary>
        /// <param name="uidList">用户id列表</param>
        /// <returns></returns>
        public IDataReader GetOAuthUserList(string uidList)
        {
            string commandText = string.Format("SELECT {1} FROM [{0}oauth] WHERE [uid] IN ({2})",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.OAUTH,
                                                uidList);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion

        #region 用户

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetPartUserById(int uid)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getpartuserbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetUserById(int uid)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getuserbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户细节
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetUserDetailById(int uid)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getuserdetailbyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public IDataReader GetPartUserByName(string userName)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20, userName)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getpartuserbyname", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public IDataReader GetPartUserByEmail(string email)
        {
            DbParameter[] parms = {
									   GenerateInParam("@email",SqlDbType.Char,50, email)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getpartuserbyemail", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public IDataReader GetPartUserByMobile(string mobile)
        {
            DbParameter[] parms = {
									   GenerateInParam("@mobile",SqlDbType.Char,15, mobile)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getpartuserbymobile", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public int GetUidByUserName(string userName)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20, userName)
								   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuidbyusername", RDBSHelper.RDBSTablePre),
                                                                   parms), -1);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public int GetUidByEmail(string email)
        {
            DbParameter[] parms = {
									   GenerateInParam("@email",SqlDbType.Char,50, email)
								   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuidbyemail", RDBSHelper.RDBSTablePre),
                                                                   parms), -1);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public int GetUidByMobile(string mobile)
        {
            DbParameter[] parms = {
									   GenerateInParam("@mobile",SqlDbType.Char,15, mobile)
								   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getuidbymobile", RDBSHelper.RDBSTablePre),
                                                                   parms), -1);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public int CreateUser(UserInfo userInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,userInfo.UserName),
									   GenerateInParam("@email",SqlDbType.Char,50,userInfo.Email),
                                       GenerateInParam("@mobile",SqlDbType.Char,15,userInfo.Mobile),
									   GenerateInParam("@password",SqlDbType.Char,32,userInfo.Password),
									   GenerateInParam("@userrid",SqlDbType.SmallInt,2,userInfo.UserRid),
                                       GenerateInParam("@admingid",SqlDbType.SmallInt,2,userInfo.AdminGid),
									   GenerateInParam("@nickname",SqlDbType.NChar,20,userInfo.NickName),
									   GenerateInParam("@avatar",SqlDbType.Char,40,userInfo.Avatar),
									   GenerateInParam("@paycredits",SqlDbType.Int,4,userInfo.PayCredits),
									   GenerateInParam("@rankcredits",SqlDbType.Int,4,userInfo.RankCredits),
									   GenerateInParam("@verifyemail",SqlDbType.TinyInt,1,userInfo.VerifyEmail),
									   GenerateInParam("@verifymobile",SqlDbType.TinyInt,1,userInfo.VerifyMobile),
									   GenerateInParam("@liftbantime",SqlDbType.DateTime,8,userInfo.LiftBanTime),
                                       GenerateInParam("@salt",SqlDbType.NChar,6,userInfo.Salt),
									   GenerateInParam("@lastvisittime",SqlDbType.DateTime,8,userInfo.LastVisitTime),
                                       GenerateInParam("@lastvisitip",SqlDbType.Char,15,userInfo.LastVisitIP),
                                       GenerateInParam("@lastvisitrgid",SqlDbType.SmallInt,2,userInfo.LastVisitRgId),
									   GenerateInParam("@registertime",SqlDbType.DateTime,8,userInfo.RegisterTime),
                                       GenerateInParam("@registerip",SqlDbType.Char,15,userInfo.RegisterIP),
                                       GenerateInParam("@registerrgid",SqlDbType.SmallInt,2,userInfo.RegisterRgId),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,userInfo.Gender),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,userInfo.RealName),
									   GenerateInParam("@bday",SqlDbType.DateTime,8,userInfo.Bday),
                                       GenerateInParam("@idcard",SqlDbType.VarChar,18,userInfo.IdCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,userInfo.RegionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,userInfo.Address),
									   GenerateInParam("@bio",SqlDbType.NVarChar,300,userInfo.Bio)
								   };

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                      string.Format("{0}createuser", RDBSHelper.RDBSTablePre),
                                                                      parms), -1);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        public void UpdateUser(UserInfo userInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,userInfo.UserName),
									   GenerateInParam("@email",SqlDbType.Char,50,userInfo.Email),
                                       GenerateInParam("@mobile",SqlDbType.Char,15,userInfo.Mobile),
									   GenerateInParam("@password",SqlDbType.Char,32,userInfo.Password),
									   GenerateInParam("@userrid",SqlDbType.SmallInt,2,userInfo.UserRid),
                                       GenerateInParam("@admingid",SqlDbType.SmallInt,2,userInfo.AdminGid),
									   GenerateInParam("@nickname",SqlDbType.NChar,20,userInfo.NickName),
									   GenerateInParam("@avatar",SqlDbType.Char,40,userInfo.Avatar),
									   GenerateInParam("@paycredits",SqlDbType.Int,4,userInfo.PayCredits),
									   GenerateInParam("@rankcredits",SqlDbType.Int,4,userInfo.RankCredits),
									   GenerateInParam("@verifyemail",SqlDbType.TinyInt,1,userInfo.VerifyEmail),
									   GenerateInParam("@verifymobile",SqlDbType.TinyInt,1,userInfo.VerifyMobile),
									   GenerateInParam("@liftbantime",SqlDbType.DateTime,8,userInfo.LiftBanTime),
                                       GenerateInParam("@salt",SqlDbType.NChar,6,userInfo.Salt),
                                       GenerateInParam("@lastvisittime",SqlDbType.DateTime,8,userInfo.LastVisitTime),
                                       GenerateInParam("@lastvisitip",SqlDbType.Char,15,userInfo.LastVisitIP),
                                       GenerateInParam("@lastvisitrgid",SqlDbType.SmallInt,2,userInfo.LastVisitRgId),
									   GenerateInParam("@registertime",SqlDbType.DateTime,8,userInfo.RegisterTime),
                                       GenerateInParam("@registerip",SqlDbType.Char,15,userInfo.RegisterIP),
                                       GenerateInParam("@registerrgid",SqlDbType.SmallInt,2,userInfo.RegisterRgId),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,userInfo.Gender),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,userInfo.RealName),
									   GenerateInParam("@bday",SqlDbType.DateTime,8,userInfo.Bday),
                                       GenerateInParam("@idcard",SqlDbType.VarChar,18,userInfo.IdCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,userInfo.RegionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,userInfo.Address),
									   GenerateInParam("@bio",SqlDbType.NVarChar,300,userInfo.Bio),
									   GenerateInParam("@uid",SqlDbType.Int,4,userInfo.Uid)
								   };

            RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                     string.Format("{0}updateuser", RDBSHelper.RDBSTablePre),
                                     parms);
        }

        /// <summary>
        /// 更新部分用户
        /// </summary>
        /// <returns></returns>
        public void UpdatePartUser(PartUserInfo partUserInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,partUserInfo.UserName),
									   GenerateInParam("@email",SqlDbType.Char,50,partUserInfo.Email),
                                       GenerateInParam("@mobile",SqlDbType.Char,15,partUserInfo.Mobile),
									   GenerateInParam("@password",SqlDbType.Char,32,partUserInfo.Password),
									   GenerateInParam("@userrid",SqlDbType.SmallInt,2,partUserInfo.UserRid),
                                       GenerateInParam("@admingid",SqlDbType.SmallInt,2,partUserInfo.AdminGid),
									   GenerateInParam("@nickname",SqlDbType.NChar,20,partUserInfo.NickName),
									   GenerateInParam("@avatar",SqlDbType.Char,40,partUserInfo.Avatar),
									   GenerateInParam("@paycredits",SqlDbType.Int,4,partUserInfo.PayCredits),
									   GenerateInParam("@rankcredits",SqlDbType.Int,4,partUserInfo.RankCredits),
									   GenerateInParam("@verifyemail",SqlDbType.TinyInt,1,partUserInfo.VerifyEmail),
									   GenerateInParam("@verifymobile",SqlDbType.TinyInt,1,partUserInfo.VerifyMobile),
									   GenerateInParam("@liftbantime",SqlDbType.DateTime,8,partUserInfo.LiftBanTime),
                                       GenerateInParam("@salt",SqlDbType.NChar,6,partUserInfo.Salt),
									   GenerateInParam("@uid",SqlDbType.Int,4,partUserInfo.Uid)
								   };

            RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                     string.Format("{0}updatepartuser", RDBSHelper.RDBSTablePre),
                                     parms);
        }

        /// <summary>
        /// 更新用户细节
        /// </summary>
        /// <returns></returns>
        public void UpdateUserDetail(UserDetailInfo userDetailInfo)
        {
            DbParameter[] parms = {
                                       GenerateInParam("@lastvisittime",SqlDbType.DateTime,8,userDetailInfo.LastVisitTime),
                                       GenerateInParam("@lastvisitip",SqlDbType.Char,15,userDetailInfo.LastVisitIP),
                                       GenerateInParam("@lastvisitrgid",SqlDbType.SmallInt,2,userDetailInfo.LastVisitRgId),
									   GenerateInParam("@registertime",SqlDbType.DateTime,8,userDetailInfo.RegisterTime),
                                       GenerateInParam("@registerip",SqlDbType.Char,15,userDetailInfo.RegisterIP),
                                       GenerateInParam("@registerrgid",SqlDbType.SmallInt,2,userDetailInfo.RegisterRgId),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,userDetailInfo.Gender),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,userDetailInfo.RealName),
									   GenerateInParam("@bday",SqlDbType.DateTime,8,userDetailInfo.Bday),
                                       GenerateInParam("@idcard",SqlDbType.VarChar,18,userDetailInfo.IdCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,userDetailInfo.RegionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,userDetailInfo.Address),
									   GenerateInParam("@bio",SqlDbType.NVarChar,300,userDetailInfo.Bio),
									   GenerateInParam("@uid",SqlDbType.Int,4,userDetailInfo.Uid)
								   };

            RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                     string.Format("{0}updateuserdetail", RDBSHelper.RDBSTablePre),
                                     parms);
        }

        /// <summary>
        /// 更新用户最后访问
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="visitTime">访问时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        public void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4,uid),
									   GenerateInParam("@visittime",SqlDbType.DateTime,8,visitTime),
                                       GenerateInParam("@ip",SqlDbType.Char,15,ip),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,regionId)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateuserlastvisit", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 后台获得用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetUserList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [{1}users].[uid],[{1}users].[username],[{1}users].[email],[{1}users].[mobile],[{1}users].[userrid],[{1}users].[admingid],[{1}users].[nickname],[{1}users].[paycredits],[{1}users].[rankcredits],[{1}userdetails].[lastvisittime],[{1}userdetails].[lastvisitip],[{1}userdetails].[registertime],[{1}userdetails].[gender],[{1}userdetails].[realname],[{1}userranks].[title] AS [utitle],[{1}admingroups].[title] AS [atitle] FROM [{1}users] LEFT JOIN [{1}userdetails] ON [{1}userdetails].[uid] = [{1}users].[uid]  LEFT JOIN [{1}userranks] ON [{1}userranks].[userrid]=[{1}users].[userrid]  LEFT JOIN [{1}admingroups] ON [{1}admingroups].[admingid]=[{1}users].[userrid] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} [{1}users].[uid],[{1}users].[username],[{1}users].[email],[{1}users].[mobile],[{1}users].[userrid],[{1}users].[admingid],[{1}users].[nickname],[{1}users].[paycredits],[{1}users].[rankcredits],[{1}userdetails].[lastvisittime],[{1}userdetails].[lastvisitip],[{1}userdetails].[registertime],[{1}userdetails].[gender],[{1}userdetails].[realname],[{1}userranks].[title] AS [utitle],[{1}admingroups].[title] AS [atitle] FROM [{1}users] LEFT JOIN [{1}userdetails] ON [{1}userdetails].[uid] = [{1}users].[uid]  LEFT JOIN [{1}userranks] ON [{1}userranks].[userrid]=[{1}users].[userrid]  LEFT JOIN [{1}admingroups] ON [{1}admingroups].[admingid]=[{1}users].[userrid] WHERE {2} ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                condition,
                                                sort);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [{1}users].[uid],[{1}users].[username],[{1}users].[email],[{1}users].[mobile],[{1}users].[userrid],[{1}users].[admingid],[{1}users].[nickname],[{1}users].[paycredits],[{1}users].[rankcredits],[{1}userdetails].[lastvisittime],[{1}userdetails].[lastvisitip],[{1}userdetails].[registertime],[{1}userdetails].[gender],[{1}userdetails].[realname],[{1}userranks].[title] AS [utitle],[{1}admingroups].[title] AS [atitle] FROM [{1}users],[{1}userdetails],[{1}userranks],[{1}admingroups]  WHERE [{1}userdetails].[uid] = [{1}users].[uid] AND  [{1}userranks].[userrid]=[{1}users].[userrid] AND  [{1}admingroups].[admingid]=[{1}users].[admingid] AND [{1}users].[uid] < (SELECT min([uid])  FROM (SELECT TOP {2} [uid] FROM [{1}users] ORDER BY {3}) AS temp ) ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                sort);
                else
                    commandText = string.Format("SELECT TOP {0} [{1}users].[uid],[{1}users].[username],[{1}users].[email],[{1}users].[mobile],[{1}users].[userrid],[{1}users].[admingid],[{1}users].[nickname],[{1}users].[paycredits],[{1}users].[rankcredits],[{1}userdetails].[lastvisittime],[{1}userdetails].[lastvisitip],[{1}userdetails].[registertime],[{1}userdetails].[gender],[{1}userdetails].[realname],[{1}userranks].[title] AS [utitle],[{1}admingroups].[title] AS [atitle] FROM [{1}users],[{1}userdetails],[{1}userranks],[{1}admingroups]  WHERE [{1}userdetails].[uid] = [{1}users].[uid] AND  [{1}userranks].[userrid]=[{1}users].[userrid] AND  [{1}admingroups].[admingid]=[{1}users].[admingid] AND [{1}users].[uid] < (SELECT min([uid])  FROM (SELECT TOP {2} [uid] FROM [{1}users] WHERE {4} ORDER BY {3}) AS temp ) AND {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                sort,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得用户列表条件
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机</param>
        /// <param name="userRid">用户等级</param>
        /// <param name="adminGid">管理员组</param>
        /// <returns></returns>
        public string AdminGetUserListCondition(string userName, string email, string mobile, int userRid, int adminGid)
        {
            StringBuilder condition = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(userName))
                condition.AppendFormat(" AND [{1}users].[username] like '{0}%' ", userName, RDBSHelper.RDBSTablePre);

            if (!string.IsNullOrWhiteSpace(email))
                condition.AppendFormat(" AND [{1}users].[email] like '{0}%' ", email, RDBSHelper.RDBSTablePre);

            if (!string.IsNullOrWhiteSpace(mobile))
                condition.AppendFormat(" AND [{1}users].[mobile] like '{0}%' ", mobile, RDBSHelper.RDBSTablePre);

            if (userRid > 0)
                condition.AppendFormat(" AND [{1}users].[userrid] >= '{0}' ", userRid, RDBSHelper.RDBSTablePre);

            if (adminGid > 0)
                condition.AppendFormat(" AND [{1}users].[admingid] <= '{0}' ", adminGid, RDBSHelper.RDBSTablePre);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetUserListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = string.Format("[{0}users].[uid]", RDBSHelper.RDBSTablePre);
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format(" {0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得用户列表数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetUserCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT([{0}users].[uid]) FROM [{0}users]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT([{0}users].[uid]) FROM [{0}users] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uidList">用户id</param>
        public void DeleteUserById(string uidList)
        {
            string commandText = string.Format(@"DELETE FROM [{0}oauth] WHERE [uid] IN ({1});
                                                 DELETE FROM [{0}onlinetime] WHERE [uid] IN ({1});
                                                 DELETE FROM [{0}userdetails] WHERE [uid] IN ({1});
                                                 DELETE FROM [{0}users] WHERE [uid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                uidList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得用户等级下用户的数量
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <returns></returns>
        public int GetUserCountByUserRid(int userRid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@userrid", SqlDbType.SmallInt, 2, userRid)    
                                    };
            string commandText = string.Format("SELECT COUNT([uid]) FROM [{0}users] WHERE [userrid]=@userrid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 获得管理员组下用户的数量
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <returns></returns>
        public int GetUserCountByAdminGid(int adminGid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@admingid", SqlDbType.SmallInt, 2, adminGid)    
                                    };
            string commandText = string.Format("SELECT COUNT([uid]) FROM [{0}users] WHERE [admingid]=@admingid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="nickName">昵称</param>
        /// <param name="avatar">头像</param>
        /// <param name="gender">性别</param>
        /// <param name="realName">真实名称</param>
        /// <param name="bday">出生日期</param>
        /// <param name="idCard">The id card.</param>
        /// <param name="regionId">区域id</param>
        /// <param name="address">所在地</param>
        /// <param name="bio">简介</param>
        /// <returns></returns>
        public bool UpdateUser(int uid, string userName, string nickName, string avatar, int gender, string realName, DateTime bday, string idCard, int regionId, string address, string bio)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,userName),
									   GenerateInParam("@nickname",SqlDbType.NChar,20,nickName),
									   GenerateInParam("@avatar",SqlDbType.Char,40,avatar),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,gender),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,realName),
									   GenerateInParam("@bday",SqlDbType.DateTime,8,bday),
									   GenerateInParam("@idcard",SqlDbType.VarChar,18,idCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,regionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,address),
									   GenerateInParam("@bio",SqlDbType.NVarChar,300,bio),
									   GenerateInParam("@uid",SqlDbType.Int,4,uid),
								   };

            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}updateucenteruser", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        /// <summary>
        /// 更新用户邮箱
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="email">邮箱</param>
        public void UpdateUserEmailByUid(int uid, string email)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@email",SqlDbType.Char,50, email)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateuseremailbyuid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新用户手机
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="mobile">手机</param>
        public void UpdateUserMobileByUid(int uid, string mobile)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@mobile",SqlDbType.Char,15, mobile)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateusermobilebyuid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="password">密码</param>
        public void UpdateUserPasswordByUid(int uid, string password)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@password",SqlDbType.Char,32, password)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateuserpasswordbyuid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新用户解禁时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="liftBanTime">解禁时间</param>
        public void UpdateUserLiftBanTimeByUid(int uid, DateTime liftBanTime)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@liftbantime",SqlDbType.DateTime,8, liftBanTime)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateuserliftbantimebyuid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="userRid">用户等级id</param>
        public void UpdateUserRankByUid(int uid, int userRid)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@userrid",SqlDbType.SmallInt,2, userRid)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateuserrankbyuid", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新用户在线时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="onlineTime">在线时间</param>
        /// <param name="updateTime">更新时间</param>
        public void UpdateUserOnlineTime(int uid, int onlineTime, DateTime updateTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                    GenerateInParam("@onlinetime", SqlDbType.Int, 4, onlineTime),
                                    GenerateInParam("@updatetime", SqlDbType.DateTime, 8, updateTime)
                                   };
            string commandText = string.Format("UPDATE [{0}onlinetime] SET [total]=[total]+@onlinetime,[year]=[year]+@onlinetime,[month]=[month]+@onlinetime,[week]=[week]+@onlinetime,[day]=[day]+@onlinetime,[updatetime]=@updatetime WHERE [uid]=@uid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 通过注册ip获得注册时间
        /// </summary>
        /// <param name="registerIP">注册ip</param>
        /// <returns></returns>
        public DateTime GetRegisterTimeByRegisterIP(string registerIP)
        {
            DbParameter[] parms = {
									GenerateInParam("@registerip",SqlDbType.Char,15, registerIP)
								   };
            return TypeHelper.ObjectToDateTime(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                        string.Format("{0}getregistertimebyregisterip", RDBSHelper.RDBSTablePre),
                                                                        parms), DateTime.Now.AddDays(-1));
        }

        /// <summary>
        /// 获得用户最后访问时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public DateTime GetUserLastVisitTimeByUid(int uid)
        {
            DbParameter[] parms = {
									GenerateInParam("@uid",SqlDbType.Int,4, uid)
								   };
            return TypeHelper.ObjectToDateTime(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                        string.Format("{0}getuserlastvisittimebyuid", RDBSHelper.RDBSTablePre),
                                                                        parms));
        }

        #endregion

        #region 用户等级

        /// <summary>
        /// 获得用户等级列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetUserRankList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}userranks] ORDER BY [system] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.USER_RANKS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 创建用户等级
        /// </summary>
        public void CreateUserRank(UserRankInfo userRankInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@system", SqlDbType.Int, 4, userRankInfo.System),
                                        GenerateInParam("@title", SqlDbType.NChar,50,userRankInfo.Title),
                                        GenerateInParam("@avatar", SqlDbType.Char,50,userRankInfo.Avatar),
                                        GenerateInParam("@creditslower", SqlDbType.Int, 4, userRankInfo.CreditsLower),
                                        GenerateInParam("@creditsupper", SqlDbType.Int,4,userRankInfo.CreditsUpper),
                                        GenerateInParam("@limitdays", SqlDbType.Int,4,userRankInfo.LimitDays)
                                    };
            string commandText = string.Format("INSERT INTO [{0}userranks]([system],[title],[avatar],[creditslower],[creditsupper],[limitdays]) VALUES(@system,@title,@avatar,@creditslower,@creditsupper,@limitdays)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除用户等级
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        public void DeleteUserRankById(int userRid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@userrid", SqlDbType.SmallInt, 2, userRid)    
                                    };
            string commandText = string.Format("DELETE FROM [{0}userranks] WHERE [userrid]=@userrid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        public void UpdateUserRank(UserRankInfo userRankInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@system", SqlDbType.Int, 4, userRankInfo.System),
                                        GenerateInParam("@title", SqlDbType.NChar,50,userRankInfo.Title),
                                        GenerateInParam("@avatar", SqlDbType.Char,50,userRankInfo.Avatar),
                                        GenerateInParam("@creditslower", SqlDbType.Int, 4, userRankInfo.CreditsLower),
                                        GenerateInParam("@creditsupper", SqlDbType.Int,4,userRankInfo.CreditsUpper),
                                        GenerateInParam("@limitdays", SqlDbType.Int,4,userRankInfo.LimitDays),
                                        GenerateInParam("@userrid", SqlDbType.SmallInt, 2, userRankInfo.UserRid)    
                                    };

            string commandText = string.Format("UPDATE [{0}userranks] SET [system]=@system,[title]=@title,[avatar]=@avatar,[creditslower]=@creditslower,[creditsupper]=@creditsupper,[limitdays]=@limitdays WHERE [userrid]=@userrid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion

        #region 管理员组

        /// <summary>
        /// 获得管理员组列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdminGroupList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}admingroups]",
                                                RDBSFields.ADMIN_GROUPS,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 创建管理员组
        /// </summary>
        /// <param name="adminGroupInfo">管理员组信息</param>
        /// <returns></returns>
        public int CreateAdminGroup(AdminGroupInfo adminGroupInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@title", SqlDbType.NChar,50,adminGroupInfo.Title),
                                        GenerateInParam("@actionlist", SqlDbType.Text, 0, adminGroupInfo.ActionList)
                                    };
            string commandText = string.Format("INSERT INTO [{0}admingroups]([title],[actionlist]) VALUES(@title,@actionlist);SELECT SCOPE_IDENTITY();",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 删除管理员组
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        public void DeleteAdminGroupById(int adminGid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@admingid", SqlDbType.SmallInt, 2, adminGid)    
                                    };
            string commandText = string.Format("DELETE FROM [{0}admingroups] WHERE [admingid]=@admingid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新管理员组
        /// </summary>
        public void UpdateAdminGroup(AdminGroupInfo adminGroupInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@title", SqlDbType.NChar,50,adminGroupInfo.Title),
                                        GenerateInParam("@actionlist", SqlDbType.Text, 0, adminGroupInfo.ActionList),
                                        GenerateInParam("@admingid", SqlDbType.SmallInt, 2, adminGroupInfo.AdminGid)    
                                    };
            string commandText = string.Format("UPDATE [{0}admingroups] SET [title]=@title,[actionlist]=@actionlist WHERE [admingid]=@admingid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion

        #region  后台操作

        /// <summary>
        /// 获得后台操作列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetAdminActionList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}adminactions] ORDER BY [displayorder] DESC",
                                                RDBSFields.ADMIN_ACTIONS,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion

        #region 收藏夹

        /// <summary>
        /// 将商品添加到收藏夹
        /// </summary>
        /// <returns></returns>
        public bool AddToFavorite(int uid, int pid, int state, DateTime addTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),    
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),    
                                        GenerateInParam("@state", SqlDbType.TinyInt, 1, state),
                                        GenerateInParam("@addtime", SqlDbType.DateTime, 8, addTime)  
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}addtofavorite", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        /// <summary>
        /// 删除收藏夹的商品
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public bool DeleteFavoriteProductByUidAndPid(int uid, int pid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}deletefavoriteproductbyuidandpid", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        /// <summary>
        /// 商品是否已经收藏
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        public bool IsExistFavoriteProduct(int uid, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),    
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}isexistfavoriteproduct", RDBSHelper.RDBSTablePre),
                                                                   parms)) > 0;
        }

        /// <summary>
        /// 获得收藏夹商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public DataTable GetFavoriteProductList(int pageSize, int pageNumber, int uid, string productName)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pagesize", SqlDbType.Int, 4, pageSize),    
                                        GenerateInParam("@pagenumber", SqlDbType.Int, 4, pageNumber),
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                        GenerateInParam("@productname", SqlDbType.NVarChar, 200, productName)
                                    };
            return RDBSHelper.ExecuteDataset(CommandType.StoredProcedure,
                                             string.Format("{0}getfilterfavoriteproductlist", RDBSHelper.RDBSTablePre),
                                             parms).Tables[0];
        }

        /// <summary>
        /// 获得收藏夹商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public DataTable GetFavoriteProductList(int pageSize, int pageNumber, int uid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pagesize", SqlDbType.Int, 4, pageSize),    
                                        GenerateInParam("@pagenumber", SqlDbType.Int, 4, pageNumber),
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid)
                                    };
            return RDBSHelper.ExecuteDataset(CommandType.StoredProcedure,
                                             string.Format("{0}getfavoriteproductlist", RDBSHelper.RDBSTablePre),
                                             parms).Tables[0];
        }

        /// <summary>
        /// 获得收藏夹商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public int GetFavoriteProductCount(int uid, string productName)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                        GenerateInParam("@productname", SqlDbType.NVarChar, 200, productName)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getfilterfavoriteproductcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得收藏夹商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetFavoriteProductCount(int uid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getfavoriteproductcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 设置收藏夹商品状态
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public bool SetFavoriteProductState(int uid, int pid, int state)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),    
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),    
                                        GenerateInParam("@state", SqlDbType.TinyInt, 1, state)
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}setfavoriteproductstate", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        #endregion

        #region 用户配送地址

        /// <summary>
        /// 创建用户配送地址
        /// </summary>
        public int CreateShipAddress(ShipAddressInfo shipAddressInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, shipAddressInfo.Uid),
                                        GenerateInParam("@regionid", SqlDbType.SmallInt, 2, shipAddressInfo.RegionId),
                                        GenerateInParam("@isdefault", SqlDbType.TinyInt, 1, shipAddressInfo.IsDefault),
                                        GenerateInParam("@alias", SqlDbType.NVarChar, 50, shipAddressInfo.Alias),
                                        GenerateInParam("@consignee", SqlDbType.NVarChar, 20, shipAddressInfo.Consignee),
                                        GenerateInParam("@mobile", SqlDbType.VarChar, 15, shipAddressInfo.Mobile),
                                        GenerateInParam("@phone", SqlDbType.VarChar, 12, shipAddressInfo.Phone),
                                        GenerateInParam("@email", SqlDbType.VarChar, 50, shipAddressInfo.Email),
                                        GenerateInParam("@zipcode", SqlDbType.Char, 6, shipAddressInfo.ZipCode),
                                        GenerateInParam("@address", SqlDbType.NVarChar, 150, shipAddressInfo.Address)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}createshipaddress", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 更新用户配送地址
        /// </summary>
        public void UpdateShipAddress(ShipAddressInfo shipAddressInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, shipAddressInfo.Uid),
                                        GenerateInParam("@regionid", SqlDbType.SmallInt, 2, shipAddressInfo.RegionId),
                                        GenerateInParam("@isdefault", SqlDbType.TinyInt, 1, shipAddressInfo.IsDefault),
                                        GenerateInParam("@alias", SqlDbType.NVarChar, 50, shipAddressInfo.Alias),
                                        GenerateInParam("@consignee", SqlDbType.NVarChar, 20, shipAddressInfo.Consignee),
                                        GenerateInParam("@mobile", SqlDbType.VarChar, 15, shipAddressInfo.Mobile),
                                        GenerateInParam("@phone", SqlDbType.VarChar, 12, shipAddressInfo.Phone),
                                        GenerateInParam("@email", SqlDbType.VarChar, 50, shipAddressInfo.Email),
                                        GenerateInParam("@zipcode", SqlDbType.Char, 6, shipAddressInfo.ZipCode),
                                        GenerateInParam("@address", SqlDbType.NVarChar, 150, shipAddressInfo.Address),
                                        GenerateInParam("@said", SqlDbType.Int, 4, shipAddressInfo.SAId)
                                    };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateshipaddress", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 获得完整用户配送地址列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetFullShipAddressList(int uid)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@uid", SqlDbType.Int, 4, uid)    
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullshipaddresslist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户配送地址数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetShipAddressCount(int uid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid)    
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getshipaddresscount", RDBSHelper.RDBSTablePre),
                                                                   parms), -1);
        }

        /// <summary>
        /// 获得默认完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetDefaultFullShipAddress(int uid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getdefaultfullshipaddress", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得完整用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <returns></returns>
        public IDataReader GetFullShipAddressBySAId(int saId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@said", SqlDbType.Int, 4, saId)   
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullshipaddressbysaid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <returns></returns>
        public IDataReader GetShipAddressBySAId(int saId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@said", SqlDbType.Int, 4, saId)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getshipaddressbysaid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 删除用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <param name="uid">用户id</param>
        public bool DeleteShipAddress(int saId, int uid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@said", SqlDbType.Int, 4, saId), 
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid) 
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}deleteshipaddress", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        /// <summary>
        /// 更新用户配送地址的默认状态
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <param name="uid">用户id</param>
        /// <param name="isDefault">状态</param>
        /// <returns></returns>
        public bool UpdateShipAddressIsDefault(int saId, int uid, int isDefault)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@said", SqlDbType.Int, 4, saId), 
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                        GenerateInParam("@isdefault", SqlDbType.TinyInt, 1, isDefault) 
                                    };
            return RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                              string.Format("{0}updateshipaddressisdefault", RDBSHelper.RDBSTablePre),
                                              parms) > 0;
        }

        #endregion
    }
}
