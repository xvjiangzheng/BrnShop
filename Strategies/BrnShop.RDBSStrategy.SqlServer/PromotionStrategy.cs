using System;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.RDBSStrategy.SqlServer
{
    /// <summary>
    /// SqlServer关系数据库策略之促销活动分部类
    /// </summary>
    public partial class RDBSStrategy : IRDBSStrategy
    {
        #region 单品促销

        /// <summary>
        /// 后台获得单品促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetSinglePromotionById(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}singlepromotions] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SINGLE_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建单品促销活动
        /// </summary>
        public void CreateSinglePromotion(SinglePromotionInfo singlePromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, singlePromotionInfo.Pid),
                                        GenerateInParam("@starttime1", SqlDbType.DateTime,8,singlePromotionInfo.StartTime1),
                                        GenerateInParam("@endtime1", SqlDbType.DateTime,8,singlePromotionInfo.EndTime1),
                                        GenerateInParam("@starttime2", SqlDbType.DateTime,8,singlePromotionInfo.StartTime2),
                                        GenerateInParam("@endtime2", SqlDbType.DateTime,8,singlePromotionInfo.EndTime2),
                                        GenerateInParam("@starttime3", SqlDbType.DateTime,8,singlePromotionInfo.StartTime3),
                                        GenerateInParam("@endtime3", SqlDbType.DateTime,8,singlePromotionInfo.EndTime3),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,singlePromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,singlePromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,singlePromotionInfo.Name),
                                        GenerateInParam("@slogan", SqlDbType.NVarChar,60,singlePromotionInfo.Slogan),
                                        GenerateInParam("@discounttype", SqlDbType.TinyInt,1,singlePromotionInfo.DiscountType),
                                        GenerateInParam("@discountvalue", SqlDbType.Int,4,singlePromotionInfo.DiscountValue),
                                        GenerateInParam("@coupontypeid", SqlDbType.Int,4,singlePromotionInfo.CouponTypeId),
                                        GenerateInParam("@paycredits", SqlDbType.Int,4,singlePromotionInfo.PayCredits),
                                        GenerateInParam("@isstock", SqlDbType.TinyInt,1,singlePromotionInfo.IsStock),
                                        GenerateInParam("@stock", SqlDbType.Int,4,singlePromotionInfo.Stock),
                                        GenerateInParam("@quotalower", SqlDbType.Int,4,singlePromotionInfo.QuotaLower),
                                        GenerateInParam("@quotaupper", SqlDbType.Int,4,singlePromotionInfo.QuotaUpper),
                                        GenerateInParam("@allowbuycount", SqlDbType.Int,4,singlePromotionInfo.AllowBuyCount)

                                    };
            string commandText = string.Format("INSERT INTO [{0}singlepromotions]([pid],[starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[slogan],[discounttype],[discountvalue],[coupontypeid],[paycredits],[isstock],[stock],[quotalower],[quotaupper],[allowbuycount]) VALUES(@pid,@starttime1,@endtime1,@starttime2,@endtime2,@starttime3,@endtime3,@userranklower,@state,@name,@slogan,@discounttype,@discountvalue,@coupontypeid,@paycredits,@isstock,@stock,@quotalower,@quotaupper,@allowbuycount)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新单品促销活动
        /// </summary>
        public void UpdateSinglePromotion(SinglePromotionInfo singlePromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, singlePromotionInfo.Pid),
                                        GenerateInParam("@starttime1", SqlDbType.DateTime,8,singlePromotionInfo.StartTime1),
                                        GenerateInParam("@endtime1", SqlDbType.DateTime,8,singlePromotionInfo.EndTime1),
                                        GenerateInParam("@starttime2", SqlDbType.DateTime,8,singlePromotionInfo.StartTime2),
                                        GenerateInParam("@endtime2", SqlDbType.DateTime,8,singlePromotionInfo.EndTime2),
                                        GenerateInParam("@starttime3", SqlDbType.DateTime,8,singlePromotionInfo.StartTime3),
                                        GenerateInParam("@endtime3", SqlDbType.DateTime,8,singlePromotionInfo.EndTime3),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,singlePromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,singlePromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,singlePromotionInfo.Name),
                                        GenerateInParam("@slogan", SqlDbType.NVarChar,60,singlePromotionInfo.Slogan),
                                        GenerateInParam("@discounttype", SqlDbType.TinyInt,1,singlePromotionInfo.DiscountType),
                                        GenerateInParam("@discountvalue", SqlDbType.Int,4,singlePromotionInfo.DiscountValue),
                                        GenerateInParam("@coupontypeid", SqlDbType.Int,4,singlePromotionInfo.CouponTypeId),
                                        GenerateInParam("@paycredits", SqlDbType.Int,4,singlePromotionInfo.PayCredits),
                                        GenerateInParam("@isstock", SqlDbType.TinyInt,1,singlePromotionInfo.IsStock),
                                        GenerateInParam("@stock", SqlDbType.Int,4,singlePromotionInfo.Stock),
                                        GenerateInParam("@quotalower", SqlDbType.Int,4,singlePromotionInfo.QuotaLower),
                                        GenerateInParam("@quotaupper", SqlDbType.Int,4,singlePromotionInfo.QuotaUpper),
                                        GenerateInParam("@allowbuycount", SqlDbType.Int,4,singlePromotionInfo.AllowBuyCount),
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, singlePromotionInfo.PmId)
                                    };
            string commandText = string.Format("UPDATE [{0}singlepromotions] SET [pid]=@pid,[starttime1]=@starttime1,[endtime1]=@endtime1,[starttime2]=@starttime2,[endtime2]=@endtime2,[starttime3]=@starttime3,[endtime3]=@endtime3,[userranklower]=@userranklower,[state]=@state,[name]=@name,[slogan]=@slogan,[discounttype]=@discounttype,[discountvalue]=@discountvalue,[coupontypeid]=@coupontypeid,[paycredits]=@paycredits,[isstock]=@isstock,[stock]=@stock,[quotalower]=@quotalower,[quotaupper]=@quotaupper,[allowbuycount]=@allowbuycount WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除单品促销活动
        /// </summary>
        /// <param name="pmIdList">促销活动id</param>
        public void DeleteSinglePromotionByPmId(string pmIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}singlepromotions] WHERE [pmid] IN ({1});",
                                                RDBSHelper.RDBSTablePre,
                                                pmIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得单品促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetSinglePromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[discounttype],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT TOP {0} [pmid],[pid],[starttime1],[endtime1],[state],[name],[discounttype] FROM [{1}singlepromotions] ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[discounttype],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT TOP {0} [pmid],[pid],[starttime1],[endtime1],[state],[name],[discounttype] FROM [{1}singlepromotions] WHERE {3} ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[discounttype],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT [pmid],[pid],[starttime1],[endtime1],[state],[name],[discounttype] FROM (SELECT TOP {0} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],[pmid],[pid],[starttime1],[endtime1],[state],[name],[discounttype] FROM [{1}singlepromotions]) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageSize * (pageNumber - 1) + 1,
                                                pageSize * pageNumber);

                else
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[discounttype],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT [pmid],[pid],[starttime1],[endtime1],[state],[name],[discounttype] FROM (SELECT TOP {0} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],[pmid],[pid],[starttime1],[endtime1],[state],[name],[discounttype] FROM [{1}singlepromotions] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageSize * (pageNumber - 1) + 1,
                                                pageSize * pageNumber,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得单品促销活动列表搜索条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public string AdminGetSinglePromotionListCondition(int pid, string promotionName, string promotionTime)
        {
            StringBuilder condition = new StringBuilder();

            if (pid > 0)
                condition.AppendFormat(" AND [pid]={0}", pid);

            if (!string.IsNullOrWhiteSpace(promotionName))
                condition.AppendFormat(" AND [name] like '{0}%'", promotionName);

            if (!string.IsNullOrEmpty(promotionTime))
                condition.AppendFormat(" AND (([starttime1]<='{0}' AND [endtime1]>='{0}') OR ([starttime2]<='{0}' AND [endtime2]>='{0}') OR ([starttime3]<='{0}' AND [endtime3]>='{0}')) ", TypeHelper.StringToDateTime(promotionTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得单品促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetSinglePromotionListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[pmid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得单品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetSinglePromotionCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}singlepromotions]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}singlepromotions] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 后台判断商品在指定时间段内是否已经存在单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int AdminIsExistSinglePromotion(int pid, DateTime startTime, DateTime endTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,startTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,endTime)
                                    };
            string commandText = string.Format(@"SELECT TOP 1 [pmid] FROM [{0}singlepromotions] WHERE [pid]=@pid AND 
                                                (([starttime1]<=@starttime AND [endtime1]>@starttime) OR ([starttime2]<=@starttime AND [endtime2]>@starttime) OR ([starttime3]<=@starttime AND [endtime3]>@starttime)
                                                OR ([starttime1]<=@endtime AND [endtime1]>@endtime) OR ([starttime2]<=@endtime AND [endtime2]>@endtime) OR ([starttime3]<=@endtime AND [endtime3]>@endtime))",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 获得单品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetSinglePromotionByPidAndTime(int pid, DateTime nowTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),  
                                        GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getsinglepromotionbypidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得单品促销活动商品的购买数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">单品促销活动id</param>
        /// <returns></returns>
        public int GetSinglePromotionProductBuyCount(int uid, int pmId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@uid", SqlDbType.Int,4,uid),
                                    GenerateInParam("@pmid", SqlDbType.Int,4,pmId)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getsinglepromotionproductbuycount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 更新单品促销活动的库存
        /// </summary>
        /// <param name="singlePromotionList">单品促销活动列表</param>
        public void UpdateSinglePromotionStock(List<SinglePromotionInfo> singlePromotionList)
        {
            StringBuilder commandText = new StringBuilder();
            foreach (SinglePromotionInfo singlePromotionInfo in singlePromotionList)
            {
                commandText.AppendFormat("UPDATE [{0}singlepromotions] SET [stock]={1} WHERE [pmid]={2};",
                                          RDBSHelper.RDBSTablePre,
                                          singlePromotionInfo.Stock,
                                          singlePromotionInfo.PmId);
            }
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText.ToString());
        }

        /// <summary>
        /// 获得单品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public IDataReader GetSinglePromotionPidList(string pmIdList)
        {
            string commandText = string.Format("SELECT DISTINCT [pid] FROM [{0}singlepromotions] WHERE [pmid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                pmIdList);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion

        #region 买送促销活动

        /// <summary>
        /// 后台获得买送促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetBuySendPromotionById(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}buysendpromotions] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BUYSEND_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建买送促销活动
        /// </summary>
        public void CreateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,buySendPromotionInfo.StartTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,buySendPromotionInfo.EndTime),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,buySendPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,buySendPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,buySendPromotionInfo.Name),
                                        GenerateInParam("@type", SqlDbType.TinyInt,1,buySendPromotionInfo.Type),
                                        GenerateInParam("@buycount", SqlDbType.TinyInt, 1, buySendPromotionInfo.BuyCount),
                                        GenerateInParam("@sendcount", SqlDbType.TinyInt, 1, buySendPromotionInfo.SendCount)
                                    };
            string commandText = string.Format("INSERT INTO [{0}buysendpromotions]([starttime],[endtime],[userranklower],[state],[name],[type],[buycount],[sendcount]) VALUES(@starttime,@endtime,@userranklower,@state,@name,@type,@buycount,@sendcount)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新买送促销活动
        /// </summary>
        public void UpdateBuySendPromotion(BuySendPromotionInfo buySendPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,buySendPromotionInfo.StartTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,buySendPromotionInfo.EndTime),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,buySendPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,buySendPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,buySendPromotionInfo.Name),
                                        GenerateInParam("@type", SqlDbType.TinyInt,1,buySendPromotionInfo.Type),
                                        GenerateInParam("@buycount", SqlDbType.TinyInt, 1, buySendPromotionInfo.BuyCount),
                                        GenerateInParam("@sendcount", SqlDbType.TinyInt, 1, buySendPromotionInfo.SendCount),
                                        GenerateInParam("@pmid", SqlDbType.Int,4,buySendPromotionInfo.PmId)
                                    };
            string commandText = string.Format("UPDATE [{0}buysendpromotions] SET [starttime]=@starttime,[endtime]=@endtime,[userranklower]=@userranklower,[state]=@state,[name]=@name,[type]=@type,[buycount]=@buycount,[sendcount]=@sendcount WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除买送促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public void DeleteBuySendPromotionById(string pmIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}buysendproducts] WHERE [pmid] IN ({1});DELETE FROM [{0}buysendpromotions] WHERE [pmid] IN ({1});",
                                               RDBSHelper.RDBSTablePre,
                                               pmIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得买送促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetBuySendPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}buysendpromotions] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                RDBSFields.BUYSEND_PROMOTIONS);

                else
                    commandText = string.Format("SELECT TOP {0} {4} FROM [{1}buysendpromotions] WHERE {3} ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition,
                                                RDBSFields.BUYSEND_PROMOTIONS);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}buysendpromotions]) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.BUYSEND_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize);

                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}buysendpromotions] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.BUYSEND_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得买送促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public string AdminGetBuySendPromotionListCondition(string promotionName, string promotionTime)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(promotionName))
                condition.AppendFormat(" AND [name] like '{0}%'", promotionName);

            if (!string.IsNullOrEmpty(promotionTime))
                condition.AppendFormat(" AND [starttime]<='{0}' AND [endtime]>='{0}'", TypeHelper.StringToDateTime(promotionTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得买送促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetBuySendPromotionListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[pmid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得买送促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetBuySendPromotionCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}buysendpromotions]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}buysendpromotions] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 获得买送促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetBuySendPromotionList(int pid, DateTime nowTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),  
                                        GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getbuysendpromotionlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得全部买送促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetAllBuySendPromotion(DateTime nowTime)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}buysendpromotions] WHERE [state]=1 AND [starttime]<=@nowtime AND [endtime]>@nowtime",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BUYSEND_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        #endregion

        #region 买送商品

        /// <summary>
        /// 添加买送商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        public void AddBuySendProduct(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("INSERT INTO [{0}buysendproducts]([pmid],[pid]) VALUES(@pmid,@pid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除买送商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public bool DeleteBuySendProductByRecordId(string recordIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}buysendproducts] WHERE [recordid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                recordIdList);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0;
        }

        /// <summary>
        /// 买送商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public bool IsExistBuySendProduct(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}buysendproducts] WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 后台获得买送商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public DataTable AdminGetBuySendProductList(int pageSize, int pageNumber, int pmId, int pid)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[pid],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} [recordid],[pmid],[pid] FROM [{1}buysendproducts] WHERE [pmid]={2} {3} ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            pmId,
                                            pid < 1 ? "" : "AND [pid]=" + pid);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[pid],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} [recordid],[pmid],[pid] FROM [{1}buysendproducts] WHERE [pmid]={3} {4} AND [recordid] < (SELECT MIN([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}buysendproducts] WHERE [pmid]={3} {4} ORDER BY [recordid] DESC) AS [temp]) ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            (pageNumber - 1) * pageSize,
                                            pmId,
                                            pid < 1 ? "" : "AND [pid]=" + pid);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得买送商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public int AdminGetBuySendProductCount(int pmId, int pid)
        {
            string commandText;
            if (pid < 1)
                commandText = string.Format("SELECT COUNT(recordid) FROM [{0}buysendproducts] WHERE [pmid]={1}", RDBSHelper.RDBSTablePre, pmId);
            else
                commandText = string.Format("SELECT COUNT(recordid) FROM [{0}buysendproducts] WHERE [pmid]={1} AND [pid]={2}", RDBSHelper.RDBSTablePre, pmId, pid);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得买送商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public IDataReader GetBuySendPidList(string recordIdList)
        {
            string commandText = string.Format("SELECT [pid] FROM [{0}buysendproducts] WHERE [recordid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                recordIdList);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion

        #region 赠品促销活动

        /// <summary>
        /// 后台获得赠品促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetGiftPromotionById(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}giftpromotions] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.GIFT_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建赠品促销活动
        /// </summary>
        public void CreateGiftPromotion(GiftPromotionInfo giftPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, giftPromotionInfo.Pid),
                                        GenerateInParam("@starttime1", SqlDbType.DateTime,8,giftPromotionInfo.StartTime1),
                                        GenerateInParam("@endtime1", SqlDbType.DateTime,8,giftPromotionInfo.EndTime1),
                                        GenerateInParam("@starttime2", SqlDbType.DateTime,8,giftPromotionInfo.StartTime2),
                                        GenerateInParam("@endtime2", SqlDbType.DateTime,8,giftPromotionInfo.EndTime2),
                                        GenerateInParam("@starttime3", SqlDbType.DateTime,8,giftPromotionInfo.StartTime3),
                                        GenerateInParam("@endtime3", SqlDbType.DateTime,8,giftPromotionInfo.EndTime3),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,giftPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,giftPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,giftPromotionInfo.Name),
                                        GenerateInParam("@quotaupper", SqlDbType.Int,4,giftPromotionInfo.QuotaUpper)
                                    };
            string commandText = string.Format("INSERT INTO [{0}giftpromotions]([pid],[starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[quotaupper]) VALUES(@pid,@starttime1,@endtime1,@starttime2,@endtime2,@starttime3,@endtime3,@userranklower,@state,@name,@quotaupper)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新赠品促销活动
        /// </summary>
        public void UpdateGiftPromotion(GiftPromotionInfo giftPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, giftPromotionInfo.Pid),
                                        GenerateInParam("@starttime1", SqlDbType.DateTime,8,giftPromotionInfo.StartTime1),
                                        GenerateInParam("@endtime1", SqlDbType.DateTime,8,giftPromotionInfo.EndTime1),
                                        GenerateInParam("@starttime2", SqlDbType.DateTime,8,giftPromotionInfo.StartTime2),
                                        GenerateInParam("@endtime2", SqlDbType.DateTime,8,giftPromotionInfo.EndTime2),
                                        GenerateInParam("@starttime3", SqlDbType.DateTime,8,giftPromotionInfo.StartTime3),
                                        GenerateInParam("@endtime3", SqlDbType.DateTime,8,giftPromotionInfo.EndTime3),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,giftPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,giftPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,giftPromotionInfo.Name),
                                        GenerateInParam("@quotaupper", SqlDbType.Int,4,giftPromotionInfo.QuotaUpper),
                                        GenerateInParam("@pmid", SqlDbType.Int,4,giftPromotionInfo.PmId)
                                    };
            string commandText = string.Format("UPDATE [{0}giftpromotions] SET [pid]=@pid,[starttime1]=@starttime1,[endtime1]=@endtime1,[starttime2]=@starttime2,[endtime2]=@endtime2,[starttime3]=@starttime3,[endtime3]=@endtime3,[userranklower]=@userranklower,[state]=@state,[name]=@name,[quotaupper]=@quotaupper WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除赠品促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public void DeleteGiftPromotionByPmId(string pmIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}gifts] WHERE [pmid] IN ({1});DELETE FROM [{0}giftpromotions] WHERE [pmid] IN ({1});",
                                               RDBSHelper.RDBSTablePre,
                                               pmIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得赠品促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetGiftPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[quotaupper],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT TOP {0} [pmid],[pid],[starttime1],[endtime1],[state],[name],[quotaupper] FROM [{1}giftpromotions] ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[quotaupper],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT TOP {0} [pmid],[pid],[starttime1],[endtime1],[state],[name],[quotaupper] FROM [{1}giftpromotions] WHERE {3} ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[quotaupper],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT [pmid],[pid],[starttime1],[endtime1],[state],[name],[quotaupper] FROM (SELECT TOP {0} ROW_NUMBER() OVER (ORDER BY {2}) AS [pmid],[pid],[starttime1],[endtime1],[state],[name],[quotaupper] FROM [{1}giftpromotions]) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageSize * (pageNumber - 1) + 1,
                                                pageSize * pageNumber);

                else
                    commandText = string.Format("SELECT [temp1].[pmid],[temp1].[pid],[temp1].[starttime1],[temp1].[endtime1],[temp1].[state],[temp1].[name],[temp1].[quotaupper],[temp2].[name] AS [pname],[temp2].[state] AS [pstate] FROM (SELECT [pmid],[pid],[starttime1],[endtime1],[state],[name],[quotaupper] FROM (SELECT TOP {0} ROW_NUMBER() OVER (ORDER BY {2}) AS [pmid],[pid],[starttime1],[endtime1],[state],[name],[quotaupper] FROM [{1}giftpromotions] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageSize * (pageNumber - 1) + 1,
                                                pageSize * pageNumber,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得赠品促销活动列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public string AdminGetGiftPromotionListCondition(int pid, string promotionName, string promotionTime)
        {
            StringBuilder condition = new StringBuilder();

            if (pid > 0)
                condition.AppendFormat(" AND [pid]={0}", pid);

            if (!string.IsNullOrWhiteSpace(promotionName))
                condition.AppendFormat(" AND [name] like '{0}%'", promotionName);

            if (!string.IsNullOrEmpty(promotionTime))
                condition.AppendFormat(" AND (([starttime1]<='{0}' AND [endtime1]>='{0}') OR ([starttime2]<='{0}' AND [endtime2]>='{0}') OR ([starttime3]<='{0}' AND [endtime3]>='{0}')) ", TypeHelper.StringToDateTime(promotionTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得赠品促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetGiftPromotionListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[pmid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得赠品促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetGiftPromotionCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}giftpromotions]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}giftpromotions] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 后台判断商品在指定时间段内是否已经存在赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int AdminIsExistGiftPromotion(int pid, DateTime startTime, DateTime endTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,startTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,endTime)
                                    };
            string commandText = string.Format(@"SELECT TOP 1 [pmid] FROM [{0}giftpromotions] WHERE [pid]=@pid AND 
                                                (([starttime1]<=@starttime AND [endtime1]>@starttime) OR ([starttime2]<=@starttime AND [endtime2]>@starttime) OR ([starttime3]<=@starttime AND [endtime3]>@starttime)
                                                OR ([starttime1]<=@endtime AND [endtime1]>@endtime) OR ([starttime2]<=@endtime AND [endtime2]>@endtime) OR ([starttime3]<=@endtime AND [endtime3]>@endtime))",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 获得赠品促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetGiftPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pid", SqlDbType.Int, 4, pid),  
                                        GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getgiftpromotionbypidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得赠品促销活动商品id列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public IDataReader GetGiftPromotionPidList(string pmIdList)
        {
            string commandText = string.Format("SELECT DISTINCT [pid] FROM [{0}giftpromotions] WHERE [pmid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                pmIdList);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion

        #region 赠品

        /// <summary>
        /// 添加赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <param name="number">数量</param>
        /// <param name="pid">商品id</param>
        public void AddGift(int pmId, int giftId, int number, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@giftid", SqlDbType.Int,4,giftId),
                                        GenerateInParam("@number", SqlDbType.Int,4,number),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("INSERT INTO [{0}gifts]([pmid],[giftid],[number],[pid]) VALUES(@pmid,@giftid,@number,@pid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除赠品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <returns></returns>
        public bool DeleteGiftByPmIdAndGiftId(int pmId, int giftId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@giftid", SqlDbType.Int,4,giftId)
                                    };
            string commandText = string.Format("DELETE FROM [{0}gifts] WHERE [pmid]=@pmid AND [giftid]=@giftid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 赠品是否已经存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        public bool IsExistGift(int pmId, int giftId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@giftid", SqlDbType.Int,4,giftId)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}gifts] WHERE [pmid]=@pmid AND [giftid]=@giftid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 更新赠品的数量
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="giftId">赠品id</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        public bool UpdateGiftNumber(int pmId, int giftId, int number)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@giftid", SqlDbType.Int,4,giftId),
                                        GenerateInParam("@number", SqlDbType.Int,4,number)
                                    };
            string commandText = string.Format("UPDATE [{0}gifts] SET [number]=@number WHERE [pmid]=@pmid AND [giftid]=@giftid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 后台获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetExtGiftList(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int,4,pmId)
                                    };
            string commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[number],[temp2].[pid],[temp2].[psn],[temp2].[cateid],[temp2].[brandid],[temp2].[skugid],[temp2].[name],[temp2].[shopprice],[temp2].[marketprice],[temp2].[costprice],[temp2].[state],[temp2].[isbest],[temp2].[ishot],[temp2].[isnew],[temp2].[displayorder],[temp2].[weight],[temp2].[showimg] FROM (SELECT {1} FROM [{0}gifts] WHERE [pmid]=@pmid) AS [temp1] LEFT JOIN [{0}products] AS [temp2] ON [temp1].[giftid]=[temp2].[pid]",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.GIFTS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得扩展赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader GetExtGiftList(int pmId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getextgiftlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 套装促销活动

        /// <summary>
        /// 后台获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetSuitPromotionById(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}suitpromotions] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SUIT_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建套装促销活动
        /// </summary>
        public int CreateSuitPromotion(SuitPromotionInfo suitPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@starttime1", SqlDbType.DateTime,8,suitPromotionInfo.StartTime1),
                                        GenerateInParam("@endtime1", SqlDbType.DateTime,8,suitPromotionInfo.EndTime1),
                                        GenerateInParam("@starttime2", SqlDbType.DateTime,8,suitPromotionInfo.StartTime2),
                                        GenerateInParam("@endtime2", SqlDbType.DateTime,8,suitPromotionInfo.EndTime2),
                                        GenerateInParam("@starttime3", SqlDbType.DateTime,8,suitPromotionInfo.StartTime3),
                                        GenerateInParam("@endtime3", SqlDbType.DateTime,8,suitPromotionInfo.EndTime3),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,suitPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,suitPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,suitPromotionInfo.Name),
                                        GenerateInParam("@quotaupper", SqlDbType.Int,4,suitPromotionInfo.QuotaUpper),
                                        GenerateInParam("@onlyonce", SqlDbType.TinyInt, 1, suitPromotionInfo.OnlyOnce)
                                    };
            string commandText = string.Format("INSERT INTO [{0}suitpromotions]([starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[quotaupper],[onlyonce]) VALUES(@starttime1,@endtime1,@starttime2,@endtime2,@starttime3,@endtime3,@userranklower,@state,@name,@quotaupper,@onlyonce);SELECT SCOPE_IDENTITY();",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 更新套装促销活动
        /// </summary>
        public void UpdateSuitPromotion(SuitPromotionInfo suitPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@starttime1", SqlDbType.DateTime,8,suitPromotionInfo.StartTime1),
                                        GenerateInParam("@endtime1", SqlDbType.DateTime,8,suitPromotionInfo.EndTime1),
                                        GenerateInParam("@starttime2", SqlDbType.DateTime,8,suitPromotionInfo.StartTime2),
                                        GenerateInParam("@endtime2", SqlDbType.DateTime,8,suitPromotionInfo.EndTime2),
                                        GenerateInParam("@starttime3", SqlDbType.DateTime,8,suitPromotionInfo.StartTime3),
                                        GenerateInParam("@endtime3", SqlDbType.DateTime,8,suitPromotionInfo.EndTime3),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,suitPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,suitPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,suitPromotionInfo.Name),
                                        GenerateInParam("@quotaupper", SqlDbType.Int,4,suitPromotionInfo.QuotaUpper),
                                        GenerateInParam("@onlyonce", SqlDbType.TinyInt, 1, suitPromotionInfo.OnlyOnce),
                                        GenerateInParam("@pmid", SqlDbType.Int,4,suitPromotionInfo.PmId)
                                    };
            string commandText = string.Format("UPDATE [{0}suitpromotions] SET [starttime1]=@starttime1,[endtime1]=@endtime1,[starttime2]=@starttime2,[endtime2]=@endtime2,[starttime3]=@starttime3,[endtime3]=@endtime3,[userranklower]=@userranklower,[state]=@state,[name]=@name,[quotaupper]=@quotaupper,[onlyonce]=@onlyonce WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除套装促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public void DeleteSuitPromotionById(string pmIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}suitproducts] WHERE [pmid] IN ({1});DELETE FROM [{0}suitpromotions] WHERE [pmid] IN ({1});",
                                                RDBSHelper.RDBSTablePre,
                                                pmIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得套装促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetSuitPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [pmid],[starttime1],[endtime1],[state],[name] FROM [{1}suitpromotions] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} [pmid],[starttime1],[endtime1],[state],[name] FROM [{1}suitpromotions] WHERE {3} ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}suitpromotions]) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.SUIT_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize);

                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}suitpromotions] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.SUIT_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得套装促销活动列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public string AdminGetSuitPromotionListCondition(int pid, string promotionName, string promotionTime)
        {
            StringBuilder condition = new StringBuilder();

            if (pid > 0)
                condition.AppendFormat(" AND [pmid] IN (SELECT [pmid] FROM [{0}suitproducts] WHERE [pid]={1})", RDBSHelper.RDBSTablePre, pid);

            if (!string.IsNullOrWhiteSpace(promotionName))
                condition.AppendFormat(" AND [name] like '{0}%'", promotionName);

            if (!string.IsNullOrEmpty(promotionTime))
                condition.AppendFormat(" AND (([starttime1]<='{0}' AND [endtime1]>='{0}') OR ([starttime2]<='{0}' AND [endtime2]>='{0}') OR ([starttime3]<='{0}' AND [endtime3]>='{0}')) ", TypeHelper.StringToDateTime(promotionTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得套装促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetSuitPromotionListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[pmid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得套装促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetSuitPromotionCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}suitpromotions]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}suitpromotions] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 获得套装促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetSuitPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                     GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getsuitpromotionbypmidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 判断用户是否参加过指定套装促销活动
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public bool IsJoinSuitPromotion(int uid, int pmId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                     GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}isjoinsuitpromotion", RDBSHelper.RDBSTablePre),
                                                                   parms)) > 0;
        }

        /// <summary>
        /// 获得套装促销活动列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetSuitPromotionList(int pid, DateTime nowTime)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pid", SqlDbType.Int, 4, pid),
                                     GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getsuitpromotionlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 套装商品

        /// <summary>
        /// 添加套装商品
        /// </summary>
        public void AddSuitProduct(int pmId, int pid, int discount, int number)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                        GenerateInParam("@discount", SqlDbType.Int,4,discount),
                                        GenerateInParam("@number", SqlDbType.Int,4,number)
                                    };
            string commandText = string.Format("INSERT INTO [{0}suitproducts]([pmid],[pid],[discount],[number]) VALUES(@pmid,@pid,@discount,@number)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除套装商品
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public bool DeleteSuitProductByPmIdAndPid(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("DELETE FROM [{0}suitproducts] WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 套装商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        public bool IsExistSuitProduct(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}suitproducts] WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 修改套装商品数量
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        public bool UpdateSuitProductNumber(int pmId, int pid, int number)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                        GenerateInParam("@number", SqlDbType.Int,4,number)
                                    };
            string commandText = string.Format("UPDATE [{0}suitproducts] SET [number]=@number WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 修改套装商品折扣
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="discount">折扣</param>
        /// <returns></returns>
        public bool UpdateSuitProductDiscount(int pmId, int pid, int discount)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                        GenerateInParam("@discount", SqlDbType.Int,4,discount)
                                    };
            string commandText = string.Format("UPDATE [{0}suitproducts] SET [discount]=@discount WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 后台获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetExtSuitProductList(int pmId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pmid", SqlDbType.Int,4,pmId)
                                    };
            string commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[discount],[temp1].[number],[temp2].[pid],[temp2].[psn],[temp2].[cateid],[temp2].[brandid],[temp2].[skugid],[temp2].[name],[temp2].[shopprice],[temp2].[marketprice],[temp2].[costprice],[temp2].[state],[temp2].[isbest],[temp2].[ishot],[temp2].[isnew],[temp2].[displayorder],[temp2].[weight],[temp2].[showimg] FROM (SELECT [recordid],[pmid],[pid],[discount],[number] FROM [{0}suitproducts] WHERE [pmid]=@pmid) AS [temp1] LEFT JOIN [{0}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得扩展套装商品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader GetExtSuitProductList(int pmId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pmid", SqlDbType.Int,4,pmId)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                             string.Format("{0}getextsuitproductlist", RDBSHelper.RDBSTablePre),
                                             parms);
        }

        /// <summary>
        /// 后台获得全部扩展套装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public IDataReader AdminGetAllExtSuitProductList(string pmIdList)
        {
            string commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[discount],[temp1].[number],[temp2].[pid],[temp2].[psn],[temp2].[cateid],[temp2].[brandid],[temp2].[skugid],[temp2].[name],[temp2].[shopprice],[temp2].[marketprice],[temp2].[costprice],[temp2].[state],[temp2].[isbest],[temp2].[ishot],[temp2].[isnew],[temp2].[displayorder],[temp2].[weight],[temp2].[showimg] FROM (SELECT [recordid],[pmid],[pid],[discount],[number] FROM [{0}suitproducts] WHERE [pmid] IN ({1})) AS [temp1] LEFT JOIN [{0}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                                RDBSHelper.RDBSTablePre,
                                                pmIdList);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得全部套扩展装商品列表
        /// </summary>
        /// <param name="pmIdList">促销活动id列表</param>
        /// <returns></returns>
        public IDataReader GetAllExtSuitProductList(string pmIdList)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@pmidlist", SqlDbType.NVarChar,100,pmIdList)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                             string.Format("{0}getallextsuitproductlist", RDBSHelper.RDBSTablePre),
                                             parms);
        }

        #endregion

        #region 满赠促销活动

        /// <summary>
        /// 后台获得满赠促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetFullSendPromotionById(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}fullsendpromotions] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FULLSEND_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建满赠促销活动
        /// </summary>
        public void CreateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,fullSendPromotionInfo.StartTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,fullSendPromotionInfo.EndTime),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,fullSendPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,fullSendPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,fullSendPromotionInfo.Name),
                                        GenerateInParam("@limitmoney", SqlDbType.Int, 4, fullSendPromotionInfo.LimitMoney),
                                        GenerateInParam("@addmoney", SqlDbType.Int, 4, fullSendPromotionInfo.AddMoney)
                                    };
            string commandText = string.Format("INSERT INTO [{0}fullsendpromotions]([starttime],[endtime],[userranklower],[state],[name],[limitmoney],[addmoney]) VALUES(@starttime,@endtime,@userranklower,@state,@name,@limitmoney,@addmoney)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新满赠促销活动
        /// </summary>
        public void UpdateFullSendPromotion(FullSendPromotionInfo fullSendPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,fullSendPromotionInfo.StartTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,fullSendPromotionInfo.EndTime),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,fullSendPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,fullSendPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,fullSendPromotionInfo.Name),
                                        GenerateInParam("@limitmoney", SqlDbType.Int, 4, fullSendPromotionInfo.LimitMoney),
                                        GenerateInParam("@addmoney", SqlDbType.Int, 4, fullSendPromotionInfo.AddMoney),
                                        GenerateInParam("@pmid", SqlDbType.Int,4,fullSendPromotionInfo.PmId)
                                    };
            string commandText = string.Format("UPDATE [{0}fullsendpromotions] SET [starttime]=@starttime,[endtime]=@endtime,[userranklower]=@userranklower,[state]=@state,[name]=@name,[limitmoney]=@limitmoney,[addmoney]=@addmoney WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除满赠促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public void DeleteFullSendPromotionById(string pmIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}fullsendproducts] WHERE [pmid] IN ({1});DELETE FROM [{0}fullsendpromotions] WHERE [pmid] IN ({1});",
                                               RDBSHelper.RDBSTablePre,
                                               pmIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得满赠促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetFullSendPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [pmid],[starttime],[endtime],[state],[name],[limitmoney],[addmoney] FROM [{1}fullsendpromotions] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} [pmid],[starttime],[endtime],[state],[name],[limitmoney],[addmoney] FROM [{1}fullsendpromotions] WHERE {3} ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}fullsendpromotions]) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.FULLSEND_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize);

                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}fullsendpromotions] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.FULLSEND_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得满赠促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public string AdminGetFullSendPromotionListCondition(string promotionName, string promotionTime)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(promotionName))
                condition.AppendFormat(" AND [name] like '{0}%'", promotionName);

            if (!string.IsNullOrEmpty(promotionTime))
                condition.AppendFormat(" AND [starttime]<='{0}' AND [endtime]>='{0}'", TypeHelper.StringToDateTime(promotionTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得满赠促销活动排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetFullSendPromotionListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[pmid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得满赠促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetFullSendPromotionCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}fullsendpromotions]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}fullsendpromotions] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetFullSendPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid),  
                                    GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullsendpromotionbypidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得满赠促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetFullSendPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),  
                                    GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullsendpromotionbypmidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 满赠商品

        /// <summary>
        /// 添加满赠商品
        /// </summary>
        public void AddFullSendProduct(int pmId, int pid, int type)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid),
                                        GenerateInParam("@type", SqlDbType.TinyInt,1,type)
                                    };
            string commandText = string.Format("INSERT INTO [{0}fullsendproducts]([pmid],[pid],[type]) VALUES(@pmid,@pid,@type)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除满赠商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public bool DeleteFullSendProductByRecordId(string recordIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}fullsendproducts] WHERE [recordid] IN ({1})",
                                               RDBSHelper.RDBSTablePre,
                                               recordIdList);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0;
        }

        /// <summary>
        /// 满赠商品是否存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public bool IsExistFullSendProduct(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}fullsendproducts] WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 后台获得满赠商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public DataTable AdminGetFullSendProductList(int pageSize, int pageNumber, int pmId, int type)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[pid],[temp1].[type],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} {4} FROM [{1}fullsendproducts] WHERE [pmid]={2} AND [type]={3} ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            pmId,
                                            type,
                                            RDBSFields.FULLSEND_PRODUCTS);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[pid],[temp1].[type],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} {5} FROM [{1}fullsendproducts] WHERE [pmid]={3} AND [type]={4} AND [recordid] < SELECT MIN([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}fullsendproducts] WHERE [pmid]={3} AND [type]={4} ORDER BY [recordid] DESC) AS [temp]) ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            (pageNumber - 1) * pageSize,
                                            pmId,
                                            type,
                                            RDBSFields.FULLSEND_PRODUCTS);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得满赠商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int AdminGetFullSendProductCount(int pmId, int type)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                    GenerateInParam("@type", SqlDbType.TinyInt, 1, type)
                                  };
            string commandText = string.Format("SELECT COUNT(recordid) FROM [{0}fullsendproducts] WHERE [pmid]=@pmid AND [type]=@type",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 判断满赠商品是否存在
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public bool IsExistFullSendProduct(int pmId, int pid, int type)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),  
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid),  
                                    GenerateInParam("@type", SqlDbType.TinyInt, 1, type)
                                   };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}isexistfullsendproduct", RDBSHelper.RDBSTablePre),
                                                                   parms)) > 0;
        }

        /// <summary>
        /// 获得满赠主商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public IDataReader GetFullSendMainProductList(int pageSize, int pageNumber, int pmId, int startPrice, int endPrice, int sortColumn, int sortDirection)
        {
            StringBuilder commandText = new StringBuilder();

            if (pageNumber == 1)
            {
                commandText.AppendFormat("SELECT TOP {1} [pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM [{0}products]", RDBSHelper.RDBSTablePre, pageSize);

                commandText.AppendFormat(" WHERE [pid] IN (SELECT [pid] FROM [{0}fullsendproducts] WHERE [pmid]={1} AND [type]=0)", RDBSHelper.RDBSTablePre, pmId);

                if (startPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]>={0}", startPrice);

                if (endPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]<={0}", endPrice);

                commandText.Append(" AND [state]=0");

                commandText.Append(" ORDER BY ");
                switch (sortColumn)
                {
                    case 0:
                        commandText.Append("[salecount]");
                        break;
                    case 1:
                        commandText.Append("[shopprice]");
                        break;
                    case 2:
                        commandText.Append("[reviewcount]");
                        break;
                    case 3:
                        commandText.Append("[addtime]");
                        break;
                    case 4:
                        commandText.Append("[visitcount]");
                        break;
                    default:
                        commandText.Append("[salecount]");
                        break;
                }
                switch (sortDirection)
                {
                    case 0:
                        commandText.Append(" DESC");
                        break;
                    case 1:
                        commandText.Append(" ASC");
                        break;
                    default:
                        commandText.Append(" DESC");
                        break;
                }
            }
            else
            {
                commandText.Append("SELECT [pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM");
                commandText.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY ");
                switch (sortColumn)
                {
                    case 0:
                        commandText.Append("[salecount]");
                        break;
                    case 1:
                        commandText.Append("[shopprice]");
                        break;
                    case 2:
                        commandText.Append("[reviewcount]");
                        break;
                    case 3:
                        commandText.Append("[addtime]");
                        break;
                    case 4:
                        commandText.Append("[visitcount]");
                        break;
                    default:
                        commandText.Append("[salecount]");
                        break;
                }
                switch (sortDirection)
                {
                    case 0:
                        commandText.Append(" DESC");
                        break;
                    case 1:
                        commandText.Append(" ASC");
                        break;
                    default:
                        commandText.Append(" DESC");
                        break;
                }
                commandText.AppendFormat(") AS [rowid],[pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM [{0}products]", RDBSHelper.RDBSTablePre);

                commandText.AppendFormat(" WHERE [pid] IN (SELECT [pid] FROM [{0}fullsendproducts] WHERE [pmid]={1} AND [type]=0)", RDBSHelper.RDBSTablePre, pmId);

                if (startPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]>={0}", startPrice);

                if (endPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]<={0}", endPrice);

                commandText.Append(" AND [state]=0");

                commandText.Append(") AS [temp]");
                commandText.AppendFormat(" WHERE [rowid] BETWEEN {0} AND {1}", pageSize * (pageNumber - 1) + 1, pageSize * pageNumber);

            }

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText.ToString());
        }

        /// <summary>
        /// 获得满赠主商品数量
        /// </summary>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        public int GetFullSendMainProductCount(int pmId, int startPrice, int endPrice)
        {
            StringBuilder commandText = new StringBuilder();

            commandText.AppendFormat("SELECT COUNT([pid]) FROM [{0}products] WHERE [pid] IN (SELECT [pid] FROM [{0}fullsendproducts] WHERE [pmid]={1} AND [type]=0)", RDBSHelper.RDBSTablePre, pmId);
            if (startPrice > 0)
                commandText.AppendFormat(" AND [shopprice]>={0}", startPrice);
            if (endPrice > 0)
                commandText.AppendFormat(" AND [shopprice]<={0}", endPrice);
            commandText.Append(" AND [state]=0");

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText.ToString()));
        }

        /// <summary>
        /// 获得满赠赠品列表
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <returns></returns>
        public IDataReader GetFullSendPresentList(int pmId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullsendpresentlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得满赠商品列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public DataTable GetFullSendProductList(string recordIdList)
        {
            string commandText = string.Format("SELECT {1} FROM [{0}fullsendproducts] WHERE [recordid] IN ({2})",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FULLSEND_PRODUCTS,
                                                recordIdList);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion

        #region 满减促销活动

        /// <summary>
        /// 后台获得满减促销活动
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public IDataReader AdminGetFullCutPromotionById(int pmId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)    
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}fullcutpromotions] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FULLCUT_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 创建满减促销活动
        /// </summary>
        public void CreateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@type", SqlDbType.TinyInt, 1, fullCutPromotionInfo.Type),
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,fullCutPromotionInfo.StartTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,fullCutPromotionInfo.EndTime),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,fullCutPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,fullCutPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,fullCutPromotionInfo.Name),
                                        GenerateInParam("@limitmoney1", SqlDbType.Int, 4, fullCutPromotionInfo.LimitMoney1),
                                        GenerateInParam("@cutmoney1", SqlDbType.Int, 4, fullCutPromotionInfo.CutMoney1),
                                        GenerateInParam("@limitmoney2", SqlDbType.Int, 4, fullCutPromotionInfo.LimitMoney2),
                                        GenerateInParam("@cutmoney2", SqlDbType.Int, 4, fullCutPromotionInfo.CutMoney2),
                                        GenerateInParam("@limitmoney3", SqlDbType.Int, 4, fullCutPromotionInfo.LimitMoney3),
                                        GenerateInParam("@cutmoney3", SqlDbType.Int, 4, fullCutPromotionInfo.CutMoney3)
                                    };
            string commandText = string.Format("INSERT INTO [{0}fullcutpromotions]([type],[starttime],[endtime],[userranklower],[state],[name],[limitmoney1],[cutmoney1],[limitmoney2],[cutmoney2],[limitmoney3],[cutmoney3]) VALUES(@type,@starttime,@endtime,@userranklower,@state,@name,@limitmoney1,@cutmoney1,@limitmoney2,@cutmoney2,@limitmoney3,@cutmoney3)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新满减促销活动
        /// </summary>
        public void UpdateFullCutPromotion(FullCutPromotionInfo fullCutPromotionInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@type", SqlDbType.TinyInt, 1, fullCutPromotionInfo.Type),
                                        GenerateInParam("@starttime", SqlDbType.DateTime,8,fullCutPromotionInfo.StartTime),
                                        GenerateInParam("@endtime", SqlDbType.DateTime,8,fullCutPromotionInfo.EndTime),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,fullCutPromotionInfo.UserRankLower),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,fullCutPromotionInfo.State),
                                        GenerateInParam("@name", SqlDbType.NVarChar,50,fullCutPromotionInfo.Name),
                                        GenerateInParam("@limitmoney1", SqlDbType.Int, 4, fullCutPromotionInfo.LimitMoney1),
                                        GenerateInParam("@cutmoney1", SqlDbType.Int, 4, fullCutPromotionInfo.CutMoney1),
                                        GenerateInParam("@limitmoney2", SqlDbType.Int, 4, fullCutPromotionInfo.LimitMoney2),
                                        GenerateInParam("@cutmoney2", SqlDbType.Int, 4, fullCutPromotionInfo.CutMoney2),
                                        GenerateInParam("@limitmoney3", SqlDbType.Int, 4, fullCutPromotionInfo.LimitMoney3),
                                        GenerateInParam("@cutmoney3", SqlDbType.Int, 4, fullCutPromotionInfo.CutMoney3),
                                        GenerateInParam("@pmid", SqlDbType.Int,4,fullCutPromotionInfo.PmId)
                                    };
            string commandText = string.Format("UPDATE [{0}fullcutpromotions] SET [type]=@type,[starttime]=@starttime,[endtime]=@endtime,[userranklower]=@userranklower,[state]=@state,[name]=@name,[limitmoney1]=@limitmoney1,[cutmoney1]=@cutmoney1,[limitmoney2]=@limitmoney2,[cutmoney2]=@cutmoney2,[limitmoney3]=@limitmoney3,[cutmoney3]=@cutmoney3 WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除满减促销活动
        /// </summary>
        /// <param name="pmIdList">活动id</param>
        public void DeleteFullCutPromotionById(string pmIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}fullcutproducts] WHERE [pmid] IN ({1});DELETE FROM [{0}fullcutpromotions] WHERE [pmid] IN ({1});",
                                               RDBSHelper.RDBSTablePre,
                                               pmIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得满减促销活动列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetFullCutPromotionList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}fullcutpromotions] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                RDBSFields.FULLCUT_PROMOTIONS);

                else
                    commandText = string.Format("SELECT TOP {0} {4} FROM [{1}fullcutpromotions] WHERE {3} ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition,
                                                RDBSFields.FULLCUT_PROMOTIONS);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}fullcutpromotions]) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.FULLCUT_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize);

                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {4} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}fullcutpromotions] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {3} AND {4}",
                                                RDBSFields.FULLCUT_PROMOTIONS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                (pageNumber - 1) * pageSize + 1,
                                                pageNumber * pageSize,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得满减促销活动列表条件
        /// </summary>
        /// <param name="promotionName">活动名称</param>
        /// <param name="promotionTime">活动时间</param>
        /// <returns></returns>
        public string AdminGetFullCutPromotionListCondition(string promotionName, string promotionTime)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(promotionName))
                condition.AppendFormat(" AND [name] like '{0}%'", promotionName);

            if (!string.IsNullOrEmpty(promotionTime))
                condition.AppendFormat(" AND [starttime]<='{0}' AND [endtime]>='{0}'", TypeHelper.StringToDateTime(promotionTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得满减促销活动列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetFullCutPromotionListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[pmid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得满减促销活动数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetFullCutPromotionCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}fullcutpromotions]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(pmid) FROM [{0}fullcutpromotions] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetFullCutPromotionByPidAndTime(int pid, DateTime nowTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pid", SqlDbType.Int, 4, pid),  
                                    GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullcutpromotionbypidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得满减促销活动
        /// </summary>
        /// <param name="pmId">促销活动id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetFullCutPromotionByPmIdAndTime(int pmId, DateTime nowTime)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),  
                                    GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getfullcutpromotionbypmidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得全部满减促销活动
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetAllFullCutPromotion(DateTime nowTime)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}fullcutpromotions] WHERE [state]=1 AND [starttime]<=@nowtime AND [endtime]>@nowtime",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FULLCUT_PROMOTIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        #endregion

        #region 满减商品

        /// <summary>
        /// 添加满减商品
        /// </summary>
        public void AddFullCutProduct(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("INSERT INTO [{0}fullcutproducts]([pmid],[pid]) VALUES(@pmid,@pid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除满减商品
        /// </summary>
        /// <param name="recordIdList">记录id</param>
        /// <returns></returns>
        public bool DeleteFullCutProductByRecordId(string recordIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}fullcutproducts] WHERE [recordid] IN ({1})",
                                               RDBSHelper.RDBSTablePre,
                                               recordIdList);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0;
        }

        /// <summary>
        /// 满减商品是否已经存在
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <param name="pid">商品id</param>
        public bool IsExistFullCutProduct(int pmId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@pmid", SqlDbType.Int, 4, pmId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}fullcutproducts] WHERE [pmid]=@pmid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 后台获得满减商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public DataTable AdminGetFullCutProductList(int pageSize, int pageNumber, int pmId)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[pid],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} {3} FROM [{1}fullcutproducts] WHERE [pmid]={2} ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            pmId,
                                            RDBSFields.FULLCUT_PRODUCTS);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[pmid],[temp1].[pid],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} {4} FROM [{1}fullcutproducts] WHERE [pmid]={3} AND [recordid] < (SELECT MIN([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}fullcutproducts] WHERE [pmid]={3} ORDER BY [recordid] DESC) AS [temp]) ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid] WHERE [temp2].[state]<=1",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            (pageNumber - 1) * pageSize,
                                            pmId,
                                            RDBSFields.FULLCUT_PRODUCTS);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得满减商品数量
        /// </summary>
        /// <param name="pmId">活动id</param>
        /// <returns></returns>
        public int AdminGetFullCutProductCount(int pmId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@pmid", SqlDbType.Int, 4, pmId)
                                  };
            string commandText = string.Format("SELECT COUNT(recordid) FROM [{0}fullcutproducts] WHERE [pmid]=@pmid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 获得满减商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public IDataReader GetFullCutProductList(int pageSize, int pageNumber, FullCutPromotionInfo fullCutPromotionInfo, int startPrice, int endPrice, int sortColumn, int sortDirection)
        {
            StringBuilder commandText = new StringBuilder();

            if (pageNumber == 1)
            {
                commandText.AppendFormat("SELECT TOP {1} [pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM [{0}products]", RDBSHelper.RDBSTablePre, pageSize);

                if (fullCutPromotionInfo.Type == 1)
                    commandText.AppendFormat(" WHERE [pid] IN (SELECT [pid] FROM [{0}fullcutproducts] WHERE [pmid]={1})", RDBSHelper.RDBSTablePre, fullCutPromotionInfo.PmId);
                else if (fullCutPromotionInfo.Type == 2)
                    commandText.AppendFormat(" WHERE [pid] NOT IN (SELECT [pid] FROM [{0}fullcutproducts] WHERE [pmid]={1})", RDBSHelper.RDBSTablePre, fullCutPromotionInfo.PmId);

                if (startPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]>={0}", startPrice);

                if (endPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]<={0}", endPrice);

                commandText.Append(" AND [state]=0");

                commandText.Append(" ORDER BY ");
                switch (sortColumn)
                {
                    case 0:
                        commandText.Append("[salecount]");
                        break;
                    case 1:
                        commandText.Append("[shopprice]");
                        break;
                    case 2:
                        commandText.Append("[reviewcount]");
                        break;
                    case 3:
                        commandText.Append("[addtime]");
                        break;
                    case 4:
                        commandText.Append("[visitcount]");
                        break;
                    default:
                        commandText.Append("[salecount]");
                        break;
                }
                switch (sortDirection)
                {
                    case 0:
                        commandText.Append(" DESC");
                        break;
                    case 1:
                        commandText.Append(" ASC");
                        break;
                    default:
                        commandText.Append(" DESC");
                        break;
                }
            }
            else
            {
                commandText.Append("SELECT [pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM");
                commandText.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY ");
                switch (sortColumn)
                {
                    case 0:
                        commandText.Append("[salecount]");
                        break;
                    case 1:
                        commandText.Append("[shopprice]");
                        break;
                    case 2:
                        commandText.Append("[reviewcount]");
                        break;
                    case 3:
                        commandText.Append("[addtime]");
                        break;
                    case 4:
                        commandText.Append("[visitcount]");
                        break;
                    default:
                        commandText.Append("[salecount]");
                        break;
                }
                switch (sortDirection)
                {
                    case 0:
                        commandText.Append(" DESC");
                        break;
                    case 1:
                        commandText.Append(" ASC");
                        break;
                    default:
                        commandText.Append(" DESC");
                        break;
                }
                commandText.AppendFormat(") AS [rowid],[pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime] FROM [{0}products]", RDBSHelper.RDBSTablePre);

                if (fullCutPromotionInfo.Type == 1)
                    commandText.AppendFormat(" WHERE [pid] IN (SELECT [pid] FROM [{0}fullcutproducts] WHERE [pmid]={1})", RDBSHelper.RDBSTablePre, fullCutPromotionInfo.PmId);
                else if (fullCutPromotionInfo.Type == 2)
                    commandText.AppendFormat(" WHERE [pid] NOT IN (SELECT [pid] FROM [{0}fullcutproducts] WHERE [pmid]={1})", RDBSHelper.RDBSTablePre, fullCutPromotionInfo.PmId);


                if (startPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]>={0}", startPrice);

                if (endPrice > 0)
                    commandText.AppendFormat(" AND [shopprice]<={0}", endPrice);

                commandText.Append(" AND [state]=0");

                commandText.Append(") AS [temp]");
                commandText.AppendFormat(" WHERE [rowid] BETWEEN {0} AND {1}", pageSize * (pageNumber - 1) + 1, pageSize * pageNumber);

            }

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText.ToString());
        }

        /// <summary>
        /// 获得满减商品数量
        /// </summary>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        public int GetFullCutProductCount(FullCutPromotionInfo fullCutPromotionInfo, int startPrice, int endPrice)
        {
            StringBuilder commandText = new StringBuilder();

            commandText.AppendFormat("SELECT COUNT([pid]) FROM [{0}products] WHERE", RDBSHelper.RDBSTablePre);
            if (fullCutPromotionInfo.Type == 1)
                commandText.AppendFormat(" [pid] IN (SELECT [pid] FROM [{0}fullcutproducts] WHERE [pmid]={1})", RDBSHelper.RDBSTablePre, fullCutPromotionInfo.PmId);
            else if (fullCutPromotionInfo.Type == 2)
                commandText.AppendFormat(" [pid] NOT IN (SELECT [pid] FROM [{0}fullcutproducts] WHERE [pmid]={1})", RDBSHelper.RDBSTablePre, fullCutPromotionInfo.PmId);

            if (startPrice > 0)
                commandText.AppendFormat(" AND [shopprice]>={0}", startPrice);
            if (endPrice > 0)
                commandText.AppendFormat(" AND [shopprice]<={0}", endPrice);
            commandText.Append(" AND [state]=0");

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText.ToString()));
        }

        /// <summary>
        /// 获得满减商品id列表
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public IDataReader GetFullCutPidList(string recordIdList)
        {
            string commandText = string.Format("SELECT [pid] FROM [{0}fullcutproducts] WHERE [recordid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                recordIdList);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion

        #region 活动专题

        /// <summary>
        /// 创建活动专题
        /// </summary>
        /// <param name="topicInfo">活动专题信息</param>
        public void CreateTopic(TopicInfo topicInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@starttime", SqlDbType.DateTime, 8, topicInfo.StartTime),
                                    GenerateInParam("@endtime", SqlDbType.DateTime,8,topicInfo.EndTime),
                                    GenerateInParam("@isshow", SqlDbType.TinyInt,1,topicInfo.IsShow),
                                    GenerateInParam("@sn", SqlDbType.Char,16,topicInfo.SN),
                                    GenerateInParam("@title", SqlDbType.NVarChar,100,topicInfo.Title),
                                    GenerateInParam("@headhtml", SqlDbType.NText,0,topicInfo.HeadHtml),
                                    GenerateInParam("@bodyhtml", SqlDbType.NText,0,topicInfo.BodyHtml)
                                    };
            string commandText = string.Format("INSERT INTO [{0}topics]([starttime],[endtime],[isshow],[sn],[title],[headhtml],[bodyhtml]) VALUES(@starttime,@endtime,@isshow,@sn,@title,@headhtml,@bodyhtml)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新活动专题
        /// </summary>
        /// <param name="topicInfo">活动专题信息</param>
        public void UpdateTopic(TopicInfo topicInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@starttime", SqlDbType.DateTime, 8, topicInfo.StartTime),
                                    GenerateInParam("@endtime", SqlDbType.DateTime,8,topicInfo.EndTime),
                                    GenerateInParam("@isshow", SqlDbType.TinyInt,1,topicInfo.IsShow),
                                    GenerateInParam("@sn", SqlDbType.Char,16,topicInfo.SN),
                                    GenerateInParam("@title", SqlDbType.NVarChar,100,topicInfo.Title),
                                    GenerateInParam("@headhtml", SqlDbType.NText,0,topicInfo.HeadHtml),
                                    GenerateInParam("@bodyhtml", SqlDbType.NText,0,topicInfo.BodyHtml),
                                    GenerateInParam("@topicid", SqlDbType.Int,4,topicInfo.TopicId)
                                    };
            string commandText = string.Format("UPDATE [{0}topics] SET [starttime]=@starttime,[endtime]=@endtime,[isshow]=@isshow,[sn]=@sn,[title]=@title,[headhtml]=@headhtml,[bodyhtml]=@bodyhtml WHERE [topicid]=@topicid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        public void DeleteTopicById(int topicId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@topicid", SqlDbType.Int,4,topicId)
                                    };
            string commandText = string.Format("DELETE FROM [{0}topics] WHERE [topicid]=@topicid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        /// <returns></returns>
        public IDataReader AdminGetTopicById(int topicId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@topicid", SqlDbType.Int,4,topicId)
                                    };
            string commandText = string.Format("SELECT {1} FROM [{0}topics] WHERE [topicid]=@topicid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.TOPICS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得活动专题列表条件
        /// </summary>
        /// <param name="sn">编号</param>
        /// <param name="title">标题</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public string AdminGetTopicListCondition(string sn, string title, string startTime, string endTime)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(sn))
                condition.AppendFormat(" AND [sn] like '{0}%' ", sn);
            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [title] like '{0}%' ", title);
            if (!string.IsNullOrEmpty(startTime))
                condition.AppendFormat(" AND [starttime] >= '{0}' ", TypeHelper.StringToDateTime(startTime).ToString("yyyy-MM-dd HH:mm:ss"));
            if (!string.IsNullOrEmpty(endTime))
                condition.AppendFormat(" AND [endtime] <= '{0}' ", TypeHelper.StringToDateTime(endTime).ToString("yyyy-MM-dd HH:mm:ss"));

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得活动专题列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetTopicListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[topicid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得活动专题列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public DataTable AdminGetTopicList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [topicid],[starttime],[endtime],[isshow],[sn],[title] FROM [{1}topics] ORDER BY {2}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} [topicid],[starttime],[endtime],[isshow],[sn],[title] FROM [{1}topics] WHERE {2} ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                condition,
                                                sort);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} [topicid],[starttime],[endtime],[isshow],[sn],[title] FROM [{1}topics] WHERE [topicid] NOT IN (SELECT TOP {2} [topicid] FROM [{1}topics] ORDER BY {3}) ORDER BY {3}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                sort);
                else
                    commandText = string.Format("SELECT TOP {0} [topicid],[starttime],[endtime],[isshow],[sn],[title] FROM [{1}topics] WHERE [topicid] NOT IN (SELECT TOP {2} [topicid] FROM [{1}topics] WHERE {3} ORDER BY {4}) AND {3} ORDER BY {4}",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                condition,
                                                sort);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得活动专题数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetTopicCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(topicid) FROM [{0}topics]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(topicid) FROM [{0}topics] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 判断活动专题编号是否存在
        /// </summary>
        /// <param name="topicSN">活动专题编号</param>
        /// <returns></returns>
        public bool IsExistTopicSN(string topicSN)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@topicsn", SqlDbType.Char, 16, topicSN)
                                   };
            string commandText = string.Format("SELECT TOP 1 topicid FROM [{0}topics] WHERE [sn]=@topicsn", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms)) > 0;
        }

        /// <summary>
        /// 获得活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetTopicByIdAndTime(int topicId, DateTime nowTime)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@topicid", SqlDbType.Int, 4, topicId),
                                     GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}gettopicbyidandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得活动专题
        /// </summary>
        /// <param name="topicSN">活动专题编号</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetTopicBySNAndTime(string topicSN, DateTime nowTime)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@topicsn", SqlDbType.Char, 16, topicSN),
                                     GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)
                                   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}gettopicbysnandtime", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 优惠劵类型

        /// <summary>
        /// 创建优惠劵类型
        /// </summary>
        /// <param name="couponTypeInfo">优惠劵类型信息</param>
        public void CreateCouponType(CouponTypeInfo couponTypeInfo)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@name", SqlDbType.NVarChar, 50, couponTypeInfo.Name),
                                        GenerateInParam("@state", SqlDbType.TinyInt,1,couponTypeInfo.State),
                                        GenerateInParam("@money", SqlDbType.Int,4,couponTypeInfo.Money),
                                        GenerateInParam("@count", SqlDbType.Int,4,couponTypeInfo.Count),
                                        GenerateInParam("@sendmode", SqlDbType.TinyInt,1,couponTypeInfo.SendMode),
                                        GenerateInParam("@getmode", SqlDbType.TinyInt,1,couponTypeInfo.GetMode),
                                        GenerateInParam("@usemode", SqlDbType.TinyInt,1,couponTypeInfo.UseMode),
                                        GenerateInParam("@userranklower", SqlDbType.SmallInt,2,couponTypeInfo.UserRankLower),
                                        GenerateInParam("@orderamountlower", SqlDbType.Int,4,couponTypeInfo.OrderAmountLower),
                                        GenerateInParam("@limitcateid", SqlDbType.SmallInt,2,couponTypeInfo.LimitCateId),
                                        GenerateInParam("@limitbrandid", SqlDbType.Int,4,couponTypeInfo.LimitBrandId),
                                        GenerateInParam("@limitproduct", SqlDbType.TinyInt,1,couponTypeInfo.LimitProduct),
                                        GenerateInParam("@sendstarttime", SqlDbType.DateTime,8,couponTypeInfo.SendStartTime),
                                        GenerateInParam("@sendendtime", SqlDbType.DateTime,8,couponTypeInfo.SendEndTime),
                                        GenerateInParam("@useexpiretime", SqlDbType.Int,4,couponTypeInfo.UseExpireTime),
                                        GenerateInParam("@usestarttime", SqlDbType.DateTime,8,couponTypeInfo.UseStartTime),
                                        GenerateInParam("@useendtime", SqlDbType.DateTime,8,couponTypeInfo.UseEndTime)
                                    };
            string commandText = string.Format("INSERT INTO [{0}coupontypes]([name],[state],[money],[count],[sendmode],[getmode],[usemode],[userranklower],[orderamountlower],[limitcateid],[limitbrandid],[limitproduct],[sendstarttime],[sendendtime],[useexpiretime],[usestarttime],[useendtime]) VALUES(@name,@state,@money,@count,@sendmode,@getmode,@usemode,@userranklower,@orderamountlower,@limitcateid,@limitbrandid,@limitproduct,@sendstarttime,@sendendtime,@useexpiretime,@usestarttime,@useendtime)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除优惠劵类型
        /// </summary>
        /// <param name="couponTypeIdList">优惠劵类型id列表</param>
        public void DeleteCouponTypeById(string couponTypeIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}coupons] WHERE [coupontypeid] IN ({1});DELETE FROM [{0}couponproducts] WHERE [coupontypeid] IN ({1});DELETE FROM [{0}coupontypes] WHERE [coupontypeid] IN ({1});",
                                               RDBSHelper.RDBSTablePre,
                                               couponTypeIdList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public IDataReader GetCouponTypeById(int couponTypeId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getcoupontypebyid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public IDataReader AdminGetCouponTypeById(int couponTypeId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId)    
                                    };
            string commandText = string.Format("SELECT {0} FROM [{1}coupontypes] WHERE [coupontypeid]=@coupontypeid",
                                                RDBSFields.COUPON_TYPES,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获得优惠劵类型列表条件
        /// </summary>
        /// <param name="type">0代表正在发放，1代表正在使用，-1代表全部</param>
        /// <param name="couponTypeName">优惠劵类型名称</param>
        /// <returns></returns>
        public string AdminGetCouponTypeListCondition(int type, string couponTypeName)
        {
            StringBuilder condition = new StringBuilder();

            if (type == 0)
                condition.AppendFormat(" AND ([sendmode]=1 OR [sendmode]=2 OR ([sendmode]=0 AND [sendstarttime]<='{0}' AND [sendendtime]>'{0}'))", DateTime.Now);
            else if (type == 1)
                condition.AppendFormat(" AND ([useexpiretime]>0 OR ([useexpiretime]=0 AND [usestarttime]<='{0}' AND [useendtime]>'{0}'))", DateTime.Now);

            if (!string.IsNullOrWhiteSpace(couponTypeName))
                condition.AppendFormat(" AND [name] like '{0}%' ", couponTypeName);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获得优惠劵类型列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public DataTable AdminGetCouponTypeList(int pageSize, int pageNumber, string condition)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {2} ,(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp2] WHERE [temp1].[coupontypeid]=[temp2].[coupontypeid]) AS [sendcount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp3] WHERE [temp1].[coupontypeid]=[temp3].[coupontypeid] AND [temp3].[uid]>0) AS [activatecount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp4] WHERE [temp1].[coupontypeid]=[temp4].[coupontypeid] AND [temp4].[oid]>0) AS [usecount] FROM [{1}coupontypes] AS [temp1] ORDER BY [coupontypeid] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.COUPON_TYPES);

                else
                    commandText = string.Format("SELECT TOP {0} {3},(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp2] WHERE [temp1].[coupontypeid]=[temp2].[coupontypeid]) AS [sendcount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp3] WHERE [temp1].[coupontypeid]=[temp3].[coupontypeid] AND [temp3].[uid]>0) AS [activatecount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp4] WHERE [temp1].[coupontypeid]=[temp4].[coupontypeid] AND [temp4].[oid]>0) AS [usecount] FROM [{1}coupontypes] AS [temp1] WHERE {2}  ORDER BY [coupontypeid] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                condition,
                                                RDBSFields.COUPON_TYPES);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {3},(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp2] WHERE [temp1].[coupontypeid]=[temp2].[coupontypeid]) AS [sendcount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp3] WHERE [temp1].[coupontypeid]=[temp3].[coupontypeid] AND [temp3].[uid]>0) AS [activatecount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp4] WHERE [temp1].[coupontypeid]=[temp4].[coupontypeid] AND [temp4].[oid]>0) AS [usecount] FROM [{1}coupontypes] AS [temp1] WHERE [coupontypeid] < (SELECT MIN([coupontypeid]) FROM (SELECT TOP {2} [coupontypeid] FROM [{1}coupontypes] ORDER BY [coupontypeid] DESC) AS [temp]) ORDER BY [coupontypeid] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                RDBSFields.COUPON_TYPES);
                else
                    commandText = string.Format("SELECT TOP {0} {4},(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp2] WHERE [temp1].[coupontypeid]=[temp2].[coupontypeid]) AS [sendcount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp3] WHERE [temp1].[coupontypeid]=[temp3].[coupontypeid] AND [temp3].[uid]>0) AS [activatecount],(SELECT COUNT([couponid]) FROM [{1}coupons] AS [temp4] WHERE [temp1].[coupontypeid]=[temp4].[coupontypeid] AND [temp4].[oid]>0) AS [usecount] FROM [{1}coupontypes] AS [temp1] WHERE [coupontypeid] < (SELECT MIN([coupontypeid]) FROM (SELECT TOP {2} [coupontypeid] FROM [{1}coupontypes] WHERE {3}  ORDER BY [coupontypeid] DESC) AS [temp]) AND {3} ORDER BY [coupontypeid] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                condition,
                                                RDBSFields.COUPON_TYPES);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得优惠劵类型数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetCouponTypeCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(coupontypeid) FROM [{0}coupontypes]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(coupontypeid) FROM [{0}coupontypes] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 改变优惠劵类型状态
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="state">状态(0代表关闭，1代表打开)</param>
        /// <returns></returns>
        public bool ChangeCouponTypeState(int couponTypeId, int state)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId),   
                                        GenerateInParam("@state", SqlDbType.TinyInt, 1, state)    
                                    };
            string commandText = string.Format("UPDATE [{0}coupontypes] SET [state]=@state WHERE [coupontypeid]=@coupontypeid",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 获得当前正在发放的活动优惠劵类型列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetSendingPromotionCouponTypeList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}coupontypes] WHERE [sendmode]=2 AND [state]=1",
                                                RDBSFields.COUPON_TYPES,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得当前正在发放的优惠劵类型列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public IDataReader GetSendingCouponTypeList(DateTime nowTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getsendingcoupontypelist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得当前正在使用的优惠劵类型列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetUsingCouponTypeList(DateTime nowTime)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@nowtime", SqlDbType.DateTime, 8, nowTime)    
                                    };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getusingcoupontypelist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 优惠劵

        /// <summary>
        /// 创建优惠劵
        /// </summary>
        /// <param name="couponInfo">优惠劵信息</param>
        public void CreateCoupon(CouponInfo couponInfo)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@couponsn", SqlDbType.Char, 16, couponInfo.CouponSN),
                                    GenerateInParam("@uid", SqlDbType.Int,4,couponInfo.Uid),
                                    GenerateInParam("@coupontypeid", SqlDbType.Int,4,couponInfo.CouponTypeId),
                                    GenerateInParam("@oid", SqlDbType.Int,4,couponInfo.Oid),
                                    GenerateInParam("@usetime", SqlDbType.DateTime,8,couponInfo.UseTime),
                                    GenerateInParam("@useip", SqlDbType.Char,15,couponInfo.UseIP),
                                    GenerateInParam("@money", SqlDbType.Int,4,couponInfo.Money),
                                    GenerateInParam("@activatetime", SqlDbType.DateTime,8,couponInfo.ActivateTime),
                                    GenerateInParam("@activateip", SqlDbType.Char,15,couponInfo.ActivateIP),
                                    GenerateInParam("@createuid", SqlDbType.Int,4,couponInfo.CreateUid),
                                    GenerateInParam("@createoid", SqlDbType.Int,4,couponInfo.CreateOid),
                                    GenerateInParam("@createtime", SqlDbType.DateTime,8,couponInfo.CreateTime),
                                    GenerateInParam("@createip", SqlDbType.Char,15,couponInfo.CreateIP)
                                    };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}createcoupon", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 删除优惠劵
        /// </summary>
        /// <param name="idList">id列表</param>
        public void DeleteCouponById(string idList)
        {
            string commandText = string.Format("DELETE FROM [{0}coupons] WHERE [couponid] IN ({1}) ",
                                               RDBSHelper.RDBSTablePre,
                                               idList);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 后台获得优惠劵列表条件
        /// </summary>
        /// <param name="sn">编号</param>
        /// <param name="uid">用户id</param>
        /// <param name="coupontTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public string AdminGetCouponListCondition(string sn, int uid, int coupontTypeId)
        {
            StringBuilder condition = new StringBuilder();

            condition.AppendFormat(" [coupontypeid]={0}", coupontTypeId);

            if (uid > 0)
                condition.AppendFormat(" AND [uid]={0}", uid);

            if (!string.IsNullOrWhiteSpace(sn))
                condition.AppendFormat(" AND [couponsn] like '{0}%' ", sn);

            return condition.ToString();
        }

        /// <summary>
        /// 后台获得优惠劵列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public DataTable AdminGetCouponList(int pageSize, int pageNumber, string condition)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[couponid],[temp1].[couponsn],[temp1].[uid],[temp1].[coupontypeid],[temp1].[oid],[temp1].[usetime],[temp1].[useip],[temp1].[money],[temp1].[activatetime],[temp1].[activateip],[temp1].[createuid],[temp1].[createoid],[temp1].[createtime],[temp1].[createip],[temp2].[name],[temp2].[sendmode],[temp3].[username] AS [husername],[temp4].[username] AS [cusername] FROM (SELECT TOP {0} {2} FROM [{1}coupons] WHERE {3} ORDER BY [couponid] DESC) AS [temp1]  LEFT JOIN [{1}coupontypes] AS [temp2] ON [temp1].[coupontypeid]=[temp2].[coupontypeid]  LEFT JOIN [{1}users] AS [temp3] ON [temp1].[uid]=[temp3].[uid] LEFT JOIN [{1}users] AS [temp4] ON [temp1].[createuid]=[temp4].[uid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.COUPONS,
                                            condition);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[couponid],[temp1].[couponsn],[temp1].[uid],[temp1].[coupontypeid],[temp1].[oid],[temp1].[usetime],[temp1].[useip],[temp1].[money],[temp1].[activatetime],[temp1].[activateip],[temp1].[createuid],[temp1].[createoid],[temp1].[createtime],[temp1].[createip],[temp2].[name],[temp2].[sendmode],[temp3].[username] AS [husername],[temp4].[username] AS [cusername] FROM (SELECT TOP {0} {2} FROM [{1}coupons] WHERE [couponid] < (SELECT MIN([couponid]) FROM (SELECT TOP {4} [couponid] FROM [{1}coupons] WHERE {3} ORDER BY [couponid] DESC) AS [temp]) AND {3} ORDER BY [couponid] DESC) AS [temp1]  LEFT JOIN [{1}coupontypes] AS [temp2] ON [temp1].[coupontypeid]=[temp2].[coupontypeid]  LEFT JOIN [{1}users] AS [temp3] ON [temp1].[uid]=[temp3].[uid] LEFT JOIN [{1}users] AS [temp4] ON [temp1].[createuid]=[temp4].[uid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.COUPONS,
                                            condition,
                                            (pageNumber - 1) * pageSize);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得优惠劵列表数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int AdminGetCouponCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(couponid) FROM [{0}coupons]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(couponid) FROM [{0}coupons] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        /// <summary>
        /// 获得发放的优惠劵数量
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public int GetSendCouponCount(int couponTypeId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@coupontypeid", SqlDbType.Int,4,couponTypeId)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getsendcouponcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 判断优惠劵编号是否存在
        /// </summary>
        /// <param name="couponSN">优惠劵编号</param>
        /// <returns></returns>
        public bool IsExistCouponSN(string couponSN)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@couponsn", SqlDbType.Char, 16, couponSN)   
                                  };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}isexistcouponsn", RDBSHelper.RDBSTablePre),
                                                                   parms)) > 0;
        }

        /// <summary>
        /// 获得发放给用户的优惠劵数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public int GetSendUserCouponCount(int uid, int couponTypeId)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}getsendusercouponcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得今天发放给用户的优惠劵数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public int GetTodaySendUserCouponCount(int uid, int couponTypeId, DateTime today)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),   
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId),  
                                        GenerateInParam("@today", SqlDbType.DateTime, 8, today.Date)
                                    };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}gettodaysendusercouponcount", RDBSHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 获得优惠劵列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(0代表全部，1代表未使用，2代表已使用，3代表已过期)</param>
        /// <returns></returns>
        public DataTable GetCouponList(int uid, int type)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@uid", SqlDbType.Int, 4, uid),  
                                        GenerateInParam("@type", SqlDbType.TinyInt, 1, type),   
                                        GenerateInParam("@nowtime", SqlDbType.DateTime, 8, DateTime.Now)    
                                    };
            return RDBSHelper.ExecuteDataset(CommandType.StoredProcedure,
                                             string.Format("{0}getcouponlist", RDBSHelper.RDBSTablePre),
                                             parms).Tables[0];
        }

        /// <summary>
        /// 获得优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <returns></returns>
        public IDataReader GetCouponByCouponId(int couponId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@couponid", SqlDbType.Int, 4, couponId)   
                                  };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getcouponbycouponid", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得优惠劵
        /// </summary>
        /// <param name="couponSN">优惠劵编号</param>
        /// <returns></returns>
        public IDataReader GetCouponByCouponSN(string couponSN)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@couponsn", SqlDbType.Char, 16, couponSN)   
                                  };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getcouponbycouponsn", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 使用优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="oid">订单id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        public void UseCoupon(int couponId, int oid, DateTime time, string ip)
        {
            DbParameter[] parms = {
									   GenerateInParam("@couponid",SqlDbType.Int,4, couponId),
									   GenerateInParam("@oid",SqlDbType.Int,4, oid),
									   GenerateInParam("@time",SqlDbType.DateTime,8, time),
									   GenerateInParam("@ip",SqlDbType.Char,15, ip)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}usecoupon", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 激活和使用优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="uid">用户id</param>
        /// <param name="oid">订单id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        public void ActivateAndUseCoupon(int couponId, int uid, int oid, DateTime time, string ip)
        {
            DbParameter[] parms = {
									   GenerateInParam("@couponid",SqlDbType.Int,4, couponId),
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@oid",SqlDbType.Int,4, oid),
									   GenerateInParam("@time",SqlDbType.DateTime,8, time),
									   GenerateInParam("@ip",SqlDbType.Char,15, ip)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}activateandusecoupon", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 激活优惠劵
        /// </summary>
        /// <param name="couponId">优惠劵id</param>
        /// <param name="uid">用户id</param>
        /// <param name="time">时间</param>
        /// <param name="ip">ip</param>
        public void ActivateCoupon(int couponId, int uid, DateTime time, string ip)
        {
            DbParameter[] parms = {
									   GenerateInParam("@couponid",SqlDbType.Int,4, couponId),
									   GenerateInParam("@uid",SqlDbType.Int,4, uid),
									   GenerateInParam("@time",SqlDbType.DateTime,8, time),
									   GenerateInParam("@ip",SqlDbType.Char,15, ip)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}activatecoupon", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 退还订单使用的优惠劵
        /// </summary>
        /// <param name="oid">订单id</param>
        public void ReturnUserOrderUseCoupons(int oid)
        {
            DbParameter[] parms = {
									GenerateInParam("@oid",SqlDbType.Int,4, oid)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}returnuserorderusecoupons", RDBSHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 获得用户订单发放的优惠劵列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public IDataReader GetUserOrderSendCouponList(int oid)
        {
            DbParameter[] parms = {
									GenerateInParam("@oid",SqlDbType.Int,4, oid)
								   };
            return RDBSHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getuserordersendcouponlist", RDBSHelper.RDBSTablePre),
                                            parms);
        }

        #endregion

        #region 优惠劵商品

        /// <summary>
        /// 添加优惠劵商品
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pid">商品id</param>
        public void AddCouponProduct(int couponTypeId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("INSERT INTO [{0}couponproducts]([coupontypeid],[pid]) VALUES(@coupontypeid,@pid)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除优惠劵商品
        /// </summary>
        /// <param name="recordIdList">记录id列表</param>
        /// <returns></returns>
        public bool DeleteCouponProductByRecordId(string recordIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}couponproducts] WHERE [recordid] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                recordIdList);
            return RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0;
        }

        /// <summary>
        /// 优惠劵商品是否已经存在
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pid">商品id</param>
        public bool IsExistCouponProduct(int couponTypeId, int pid)
        {
            DbParameter[] parms = {
                                        GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId),
                                        GenerateInParam("@pid", SqlDbType.Int,4,pid)
                                    };
            string commandText = string.Format("SELECT [recordid] FROM [{0}couponproducts] WHERE [coupontypeid]=@coupontypeid AND [pid]=@pid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1) > 0;
        }

        /// <summary>
        /// 后台获得优惠劵商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public DataTable AdminGetCouponProductList(int pageSize, int pageNumber, int couponTypeId)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[coupontypeid],[temp1].[pid],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} [recordid],[coupontypeid],[pid] FROM [{1}couponproducts] WHERE [coupontypeid]={2} ORDER BY [recordid] DESC) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            couponTypeId);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[recordid],[temp1].[coupontypeid],[temp1].[pid],[temp2].[name],[temp2].[shopprice],[temp2].[state] FROM (SELECT TOP {0} [recordid],[coupontypeid],[pid] FROM [{1}couponproducts] WHERE [coupontypeid]={3} AND [recordid] < (SELECT MIN([recordid]) FROM (SELECT TOP {2} [recordid] FROM [{1}couponproducts] WHERE [coupontypeid]={3} ORDER BY [recordid] DESC) AS [temp]) ORDER BY {2}) AS [temp1] LEFT JOIN [{1}products] AS [temp2] ON [temp1].[pid]=[temp2].[pid]",
                                            pageSize,
                                            RDBSHelper.RDBSTablePre,
                                            (pageNumber - 1) * pageSize,
                                            couponTypeId);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得优惠劵商品数量
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <returns></returns>
        public int AdminGetCouponProductCount(int couponTypeId)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId)
                                   };
            string commandText = string.Format("SELECT COUNT(recordid) FROM [{0}couponproducts] WHERE [coupontypeid]=@coupontypeid",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        /// <summary>
        /// 商品是否属于同一优惠劵类型
        /// </summary>
        /// <param name="couponTypeId">优惠劵类型id</param>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public bool IsSameCouponType(int couponTypeId, string pidList)
        {
            DbParameter[] parms = {
                                     GenerateInParam("@coupontypeid", SqlDbType.Int, 4, couponTypeId), 
                                     GenerateInParam("@pidlist", SqlDbType.NVarChar, 1000, pidList)   
                                  };
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.StoredProcedure,
                                          string.Format("{0}issamecoupontype", RDBSHelper.RDBSTablePre),
                                          parms)) > 0;
        }

        #endregion
    }
}
