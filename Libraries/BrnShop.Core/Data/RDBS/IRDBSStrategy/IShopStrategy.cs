using System;
using System.Data;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop关系数据库策略之商城分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        #region PV统计

        /// <summary>
        /// 更新PV统计
        /// </summary>
        /// <param name="updatePVStatState">更新PV统计状态</param>
        void UpdatePVStat(UpdatePVStatState updatePVStatState);

        /// <summary>
        /// 获得省级区域统计
        /// </summary>
        /// <returns></returns>
        DataTable GetProvinceRegionStat();

        /// <summary>
        /// 获得市级区域统计
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <returns></returns>
        DataTable GetCityRegionStat(int provinceId);

        /// <summary>
        /// 获得区/县级区域统计
        /// </summary>
        /// <param name="cityId">市id</param>
        /// <returns></returns>
        DataTable GetCountyRegionStat(int cityId);

        /// <summary>
        /// 获得PV统计列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        IDataReader GetPVStatList(string condition);

        /// <summary>
        /// 获得PV统计
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        IDataReader GetPVStatByCategoryAndValue(string category, string value);

        #endregion

        #region 禁止IP

        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetBannedIPList();

        /// <summary>
        /// 获得禁止的ip
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        IDataReader GetBannedIPById(int id);

        /// <summary>
        /// 获得禁止IP的id
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        int GetBannedIPIdByIP(string ip);

        /// <summary>
        /// 添加禁止的ip
        /// </summary>
        void AddBannedIP(BannedIPInfo bannedIPInfo);

        /// <summary>
        /// 更新禁止的ip
        /// </summary>
        void UpdateBannedIP(BannedIPInfo bannedIPInfo);

        /// <summary>
        /// 删除禁止的ip
        /// </summary>
        /// <param name="idList">id列表</param>
        void DeleteBannedIPById(string idList);

        /// <summary>
        /// 后台获得禁止的ip列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="ip">ip</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetBannedIPList(int pageSize, int pageNumber, string ip, string sort);

        /// <summary>
        /// 后台获得禁止的ip列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetBannedIPListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得禁止的ip数量
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        int AdminGetBannedIPCount(string ip);

        #endregion

        #region 筛选词

        /// <summary>
        /// 获得筛选词列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetFilterWordList();

        /// <summary>
        /// 添加筛选词
        /// </summary>
        void AddFilterWord(FilterWordInfo filterWordInfo);

        /// <summary>
        /// 更新筛选词
        /// </summary>
        void UpdateFilterWord(FilterWordInfo filterWordInfo);

        /// <summary>
        /// 删除筛选词
        /// </summary>
        /// <param name="idList">id列表</param>
        void DeleteFilterWordById(string idList);

        #endregion

        #region 登陆失败日志

        /// <summary>
        /// 获得登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <returns></returns>
        IDataReader GetLoginFailLogByIP(long loginIP);

        /// <summary>
        /// 增加登陆失败次数
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <param name="loginTime">登陆时间</param>
        void AddLoginFailTimes(long loginIP, DateTime loginTime);

        /// <summary>
        /// 删除登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        void DeleteLoginFailLogByIP(long loginIP);

        #endregion

        #region 管理员操作日志

        /// <summary>
        /// 创建管理员操作日志
        /// </summary>
        void CreateAdminOperateLog(AdminOperateLogInfo adminOperateLogInfo);

        /// <summary>
        /// 获得管理员操作日志列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        IDataReader GetAdminOperateLogList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 获得管理员操作日志列表条件
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="operation">操作行为</param>
        /// <param name="startTime">操作开始时间</param>
        /// <param name="endTime">操作结束时间</param>
        /// <returns></returns>
        string GetAdminOperateLogListCondition(int uid, string operation, string startTime, string endTime);

        /// <summary>
        /// 获得管理员操作日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int GetAdminOperateLogCount(string condition);

        /// <summary>
        /// 删除管理员操作日志
        /// </summary>
        /// <param name="idList">日志id</param>
        void DeleteAdminOperateLogById(string idList);

        #endregion

        #region 导航栏

        /// <summary>
        /// 获得导航栏列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetNavList();

        /// <summary>
        /// 创建导航栏
        /// </summary>
        void CreateNav(NavInfo navInfo);

        /// <summary>
        /// 删除导航栏
        /// </summary>
        /// <param name="id">导航栏id</param>
        void DeleteNavById(int id);

        /// <summary>
        /// 更新导航栏
        /// </summary>
        void UpdateNav(NavInfo navInfo);

        #endregion

        #region banner

        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        DataTable GetHomeBannerList(int type, DateTime nowTime);

        /// <summary>
        /// 后台获得banner列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        IDataReader AdminGetBannerList(int pageSize, int pageNumber);

        /// <summary>
        /// 后台获得banner数量
        /// </summary>
        /// <returns></returns>
        int AdminGetBannerCount();

        /// <summary>
        /// 后台获得banner
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        IDataReader AdminGetBannerById(int id);

        /// <summary>
        /// 创建banner
        /// </summary>
        void CreateBanner(BannerInfo bannerInfo);

        /// <summary>
        /// 更新banner
        /// </summary>
        void UpdateBanner(BannerInfo bannerInfo);

        /// <summary>
        /// 删除banner
        /// </summary>
        /// <param name="idList">id列表</param>
        void DeleteBannerById(string idList);

        #endregion

        #region 广告位置

        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        IDataReader GetAdvertPositionList(int pageSize, int pageNumber);

        /// <summary>
        /// 获得广告位置数量
        /// </summary>
        /// <returns></returns>
        int GetAdvertPositionCount();

        /// <summary>
        /// 获得全部广告位置
        /// </summary>
        /// <returns></returns>
        IDataReader GetAllAdvertPosition();

        /// <summary>
        /// 创建广告位置
        /// </summary>
        void CreateAdvertPosition(AdvertPositionInfo advertPositionInfo);

        /// <summary>
        /// 更新广告位置
        /// </summary>
        void UpdateAdvertPosition(AdvertPositionInfo advertPositionInfo);

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        IDataReader GetAdvertPositionById(int adPosId);

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        void DeleteAdvertPositionById(int adPosId);

        #endregion

        #region 广告

        /// <summary>
        /// 创建广告
        /// </summary>
        void CreateAdvert(AdvertInfo advertInfo);

        /// <summary>
        /// 更新广告
        /// </summary>
        void UpdateAdvert(AdvertInfo advertInfo);

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adId">广告id</param>
        void DeleteAdvertById(int adId);

        /// <summary>
        /// 后台获得广告
        /// </summary>
        /// <param name="adId">广告id</param>
        /// <returns></returns>
        IDataReader AdminGetAdvertById(int adId);

        /// <summary>
        /// 后台获得广告列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        DataTable AdminGetAdvertList(int pageSize, int pageNumber, int adPosId);

        /// <summary>
        /// 后台获得广告数量
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        int AdminGetAdvertCount(int adPosId);

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetAdvertList(int adPosId, DateTime nowTime);

        #endregion

        #region 新闻类型

        /// <summary>
        /// 创建新闻类型
        /// </summary>
        void CreateNewsType(NewsTypeInfo newsTypeInfo);

        /// <summary>
        /// 删除新闻类型
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        void DeleteNewsTypeById(int newsTypeId);

        /// <summary>
        /// 更新新闻类型
        /// </summary>
        void UpdateNewsType(NewsTypeInfo newsTypeInfo);

        /// <summary>
        /// 获得新闻类型列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetNewsTypeList();

        #endregion

        #region 新闻

        /// <summary>
        /// 创建新闻
        /// </summary>
        void CreateNews(NewsInfo newsInfo);

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="newsIdList">新闻id列表</param>
        void DeleteNewsById(string newsIdList);

        /// <summary>
        /// 更新新闻
        /// </summary>
        void UpdateNews(NewsInfo newsInfo);

        /// <summary>
        /// 获得新闻
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        IDataReader GetNewsById(int newsId);

        /// <summary>
        /// 后台获得新闻
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        IDataReader AdminGetNewsById(int newsId);

        /// <summary>
        /// 后台获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetNewsList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得新闻列表搜索条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        string AdminGetNewsListCondition(int newsTypeId, string title);

        /// <summary>
        /// 后台获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetNewsListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetNewsCount(string condition);

        /// <summary>
        /// 后台根据新闻标题得到新闻id
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        int AdminGetNewsIdByTitle(string title);

        /// <summary>
        /// 获得置首的新闻列表
        /// </summary>
        /// <param name="newsTypeId">新闻类型id(0代表全部类型)</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        DataTable GetHomeNewsList(int newsTypeId, int count);

        /// <summary>
        /// 获得新闻列表条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id(0代表全部类型)</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        string GetNewsListCondition(int newsTypeId, string title);

        /// <summary>
        /// 获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable GetNewsList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int GetNewsCount(string condition);

        #endregion

        #region 帮助

        /// <summary>
        /// 创建帮助
        /// </summary>
        void CreateHelp(HelpInfo helpInfo);

        /// <summary>
        /// 删除帮助
        /// </summary>
        /// <param name="id">帮助id</param>
        void DeleteHelpById(int id);

        /// <summary>
        /// 更新帮助
        /// </summary>
        void UpdateHelp(HelpInfo helpInfo);

        /// <summary>
        /// 获得帮助列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetHelpList();

        /// <summary>
        /// 更新帮助排序
        /// </summary>
        /// <param name="id">帮助id</param>
        /// <param name="displayOrder">排序</param>
        /// <returns></returns>
        bool UpdateHelpDisplayOrder(int id, int displayOrder);

        #endregion

        #region 友情链接

        /// <summary>
        /// 获得友情链接列表
        /// </summary>
        DataTable GetFriendLinkList();

        /// <summary>
        /// 创建友情链接
        /// </summary>
        void CreateFriendLink(FriendLinkInfo friendLinkInfo);

        /// <summary>
        /// 更新友情链接
        /// </summary>
        void UpdateFriendLink(FriendLinkInfo friendLinkInfo);

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="idList">友情链接id</param>
        void DeleteFriendLinkById(string idList);

        #endregion

        #region 全国行政区域

        /// <summary>
        /// 获得全部区域
        /// </summary>
        /// <returns></returns>
        DataTable GetAllRegion();

        /// <summary>
        /// 获得区域列表
        /// </summary>
        /// <param name="parentId">父id</param>
        /// <returns></returns>
        IDataReader GetRegionList(int parentId);

        /// <summary>
        /// 获得区域
        /// </summary>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        IDataReader GetRegionById(int regionId);

        /// <summary>
        /// 获得区域
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="layer">级别</param>
        /// <returns></returns>
        IDataReader GetRegionByNameAndLayer(string name, int layer);

        #endregion

        #region 积分日志

        /// <summary>
        /// 后台获得积分日志列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable AdminGetCreditLogList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 后台获得积分日志列表条件
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        string AdminGetCreditLogListCondition(int uid, string startTime, string endTime);

        /// <summary>
        /// 后台获得积分日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetCreditLogCount(string condition);

        /// <summary>
        /// 发放积分
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <param name="creditLogInfo">积分日志信息</param>
        void SendCredits(int userRid, CreditLogInfo creditLogInfo);

        /// <summary>
        /// 获得今天发放的支付积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        int GetTodaySendPayCredits(int uid, DateTime today);

        /// <summary>
        /// 获得今天发放的等级积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        int GetTodaySendRankCredits(int uid, DateTime today);

        /// <summary>
        /// 是否发放了今天的登陆积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        bool IsSendTodayLoginCredit(int uid, DateTime today);

        /// <summary>
        /// 是否发放了完成用户信息积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        bool IsSendCompleteUserInfoCredit(int uid);

        /// <summary>
        /// 获得支付积分日志列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        IDataReader GetPayCreditLogList(int uid, int type, int pageSize, int pageNumber);

        /// <summary>
        /// 获得支付积分日志数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        /// <returns></returns>
        int GetPayCreditLogCount(int uid, int type);

        /// <summary>
        /// 获得用户订单发放的积分
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        DataTable GetUserOrderSendCredits(int oid);

        #endregion

        #region 事件日志

        /// <summary>
        /// 创建事件日志
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="title">标题</param>
        /// <param name="server">服务器名称</param>
        /// <param name="executeTime">执行时间</param>
        void CreateEventLog(string key, string title, string server, DateTime executeTime);

        /// <summary>
        /// 获得事件的最后执行时间
        /// </summary>
        /// <param name="key">事件key</param>
        /// <returns></returns>
        DateTime GetEventLastExecuteTimeByKey(string key);

        #endregion
    }
}
