using System;
using System.Data;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop关系数据库策略之用户分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        #region 在线用户

        /// <summary>
        /// 创建在线用户
        /// </summary>
        int CreateOnlineUser(OnlineUserInfo onlineUserInfo);

        /// <summary>
        /// 更新在线用户ip
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">ip</param>
        void UpdateOnlineUserIP(int olId, string ip);

        /// <summary>
        /// 更新在线用户uid
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">uid</param>
        void UpdateOnlineUserUid(int olId, int uid);

        /// <summary>
        /// 获得在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <returns></returns>
        IDataReader GetOnlineUserBySid(string sid);

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="userType">在线用户类型</param>
        /// <returns></returns>
        int GetOnlineUserCount(int userType);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        void DeleteOnlineUserBySid(string sid);

        /// <summary>
        /// 删除过期在线用户
        /// </summary>
        /// <param name="onlineUserExpire">过期时间</param>
        void DeleteExpiredOnlineUser(int onlineUserExpire);

        /// <summary>
        /// 重置在线用户表
        /// </summary>
        void ResetOnlineUserTable();

        /// <summary>
        /// 获得在线用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        IDataReader GetOnlineUserList(int pageSize, int pageNumber, int locationType, int locationId, string sort);

        /// <summary>
        /// 获得在线用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string GetOnlineUserListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        int GetOnlineUserCount(int locationType, int locationId);

        #endregion

        #region 开放授权

        /// <summary>
        /// 创建开放授权用户
        /// </summary>
        /// <param name="oauthInfo">开放授权信息</param>
        /// <returns></returns>
        bool CreateOAuthUser(OAuthInfo oauthInfo);

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <returns></returns>
        int GetUidByOpenIdAndServer(string openId, string server);

        /// <summary>
        /// 获得开放授权用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetOAuthUserByUid(int uid);

        /// <summary>
        /// 获得开放授权用户列表
        /// </summary>
        /// <param name="uidList">用户id列表</param>
        /// <returns></returns>
        IDataReader GetOAuthUserList(string uidList);

        #endregion

        #region 用户

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetPartUserById(int uid);

        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetUserById(int uid);

        /// <summary>
        /// 获得用户细节
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetUserDetailById(int uid);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        IDataReader GetPartUserByName(string userName);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        IDataReader GetPartUserByEmail(string email);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        IDataReader GetPartUserByMobile(string mobile);

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        int GetUidByUserName(string userName);

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        int GetUidByEmail(string email);

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        int GetUidByMobile(string mobile);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        int CreateUser(UserInfo userInfo);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        void UpdateUser(UserInfo userInfo);

        /// <summary>
        /// 更新部分用户
        /// </summary>
        /// <returns></returns>
        void UpdatePartUser(PartUserInfo partUserInfo);

        /// <summary>
        /// 更新用户细节
        /// </summary>
        /// <returns></returns>
        void UpdateUserDetail(UserDetailInfo userDetailInfo);

        /// <summary>
        /// 更新用户最后访问
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="visitTime">访问时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId);

        /// <summary>
        /// 后台获得用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetUserList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得用户列表条件
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机</param>
        /// <param name="userRid">用户等级</param>
        /// <param name="adminGid">管理员组</param>
        /// <returns></returns>
        string AdminGetUserListCondition(string userName, string email, string mobile, int userRid, int adminGid);

        /// <summary>
        /// 后台获得用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetUserListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得用户列表数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetUserCount(string condition);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uidList">用户id</param>
        void DeleteUserById(string uidList);

        /// <summary>
        /// 获得用户等级下用户的数量
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <returns></returns>
        int GetUserCountByUserRid(int userRid);

        /// <summary>
        /// 获得管理员组下用户的数量
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <returns></returns>
        int GetUserCountByAdminGid(int adminGid);

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
        bool UpdateUser(int uid, string userName, string nickName, string avatar, int gender, string realName, DateTime bday, string idCard, int regionId, string address, string bio);

        /// <summary>
        /// 更新用户邮箱
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="email">邮箱</param>
        void UpdateUserEmailByUid(int uid, string email);

        /// <summary>
        /// 更新用户手机
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="mobile">手机</param>
        void UpdateUserMobileByUid(int uid, string mobile);

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="password">密码</param>
        void UpdateUserPasswordByUid(int uid, string password);

        /// <summary>
        /// 更新用户解禁时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="liftBanTime">解禁时间</param>
        void UpdateUserLiftBanTimeByUid(int uid, DateTime liftBanTime);

        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="userRid">用户等级id</param>
        void UpdateUserRankByUid(int uid, int userRid);

        /// <summary>
        /// 更新用户在线时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="onlineTime">在线时间</param>
        /// <param name="updateTime">更新时间</param>
        void UpdateUserOnlineTime(int uid, int onlineTime, DateTime updateTime);

        /// <summary>
        /// 通过注册ip获得注册时间
        /// </summary>
        /// <param name="registerIP">注册ip</param>
        /// <returns></returns>
        DateTime GetRegisterTimeByRegisterIP(string registerIP);

        /// <summary>
        /// 获得用户最后访问时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        DateTime GetUserLastVisitTimeByUid(int uid);

        #endregion

        #region 用户等级

        /// <summary>
        /// 获得用户等级列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetUserRankList();

        /// <summary>
        /// 创建用户等级
        /// </summary>
        void CreateUserRank(UserRankInfo userRankInfo);

        /// <summary>
        /// 删除用户等级
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        void DeleteUserRankById(int userRid);

        /// <summary>
        /// 更新用户等级
        /// </summary>
        void UpdateUserRank(UserRankInfo userRankInfo);

        #endregion

        #region 管理员组

        /// <summary>
        /// 获得管理员组列表
        /// </summary>
        /// <returns></returns>
        DataTable GetAdminGroupList();

        /// <summary>
        /// 创建管理员组
        /// </summary>
        /// <param name="adminGroupInfo">管理员组信息</param>
        /// <returns></returns>
        int CreateAdminGroup(AdminGroupInfo adminGroupInfo);

        /// <summary>
        /// 删除管理员组
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        void DeleteAdminGroupById(int adminGid);

        /// <summary>
        /// 更新管理员组
        /// </summary>
        void UpdateAdminGroup(AdminGroupInfo adminGroupInfo);

        #endregion

        #region  后台操作

        /// <summary>
        /// 获得后台操作列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetAdminActionList();

        #endregion

        #region 收藏夹

        /// <summary>
        /// 将商品添加到收藏夹
        /// </summary>
        /// <returns></returns>
        bool AddToFavorite(int uid, int pid, int state, DateTime addTime);

        /// <summary>
        /// 删除收藏夹的商品
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        bool DeleteFavoriteProductByUidAndPid(int uid, int pid);

        /// <summary>
        /// 商品是否已经收藏
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        bool IsExistFavoriteProduct(int uid, int pid);

        /// <summary>
        /// 获得收藏夹商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        DataTable GetFavoriteProductList(int pageSize, int pageNumber, int uid, string productName);

        /// <summary>
        /// 获得收藏夹商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        DataTable GetFavoriteProductList(int pageSize, int pageNumber, int uid);

        /// <summary>
        /// 获得收藏夹商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        int GetFavoriteProductCount(int uid, string productName);

        /// <summary>
        /// 获得收藏夹商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int GetFavoriteProductCount(int uid);

        /// <summary>
        /// 设置收藏夹商品状态
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        bool SetFavoriteProductState(int uid, int pid, int state);

        #endregion

        #region 用户配送地址

        /// <summary>
        /// 创建用户配送地址
        /// </summary>
        int CreateShipAddress(ShipAddressInfo shipAddressInfo);

        /// <summary>
        /// 更新用户配送地址
        /// </summary>
        void UpdateShipAddress(ShipAddressInfo shipAddressInfo);

        /// <summary>
        /// 获得完整用户配送地址列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetFullShipAddressList(int uid);

        /// <summary>
        /// 获得用户配送地址数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int GetShipAddressCount(int uid);

        /// <summary>
        /// 获得默认完整用户配送地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetDefaultFullShipAddress(int uid);

        /// <summary>
        /// 获得完整用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <returns></returns>
        IDataReader GetFullShipAddressBySAId(int saId);

        /// <summary>
        /// 获得用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <returns></returns>
        IDataReader GetShipAddressBySAId(int saId);

        /// <summary>
        /// 删除用户配送地址
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <param name="uid">用户id</param>
        bool DeleteShipAddress(int saId, int uid);

        /// <summary>
        /// 更新用户配送地址的默认状态
        /// </summary>
        /// <param name="saId">配送地址id</param>
        /// <param name="uid">用户id</param>
        /// <param name="isDefault">状态</param>
        /// <returns></returns>
        bool UpdateShipAddressIsDefault(int saId, int uid, int isDefault);

        #endregion
    }
}
