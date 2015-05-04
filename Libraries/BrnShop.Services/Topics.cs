using System;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 活动专题操作管理类
    /// </summary>
    public partial class Topics
    {
        /// <summary>
        /// 获得活动专题
        /// </summary>
        /// <param name="topicId">活动专题id</param>
        /// <returns></returns>
        public static TopicInfo GetTopicById(int topicId)
        {
            TopicInfo topicInfo = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_TOPIC_INFO + topicId) as TopicInfo;
            if (topicInfo == null)
            {
                topicInfo = BrnShop.Data.Topics.GetTopicByIdAndTime(topicId, DateTime.Now);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_TOPIC_INFO + topicId, topicInfo);
            }
            else
            {
                if (topicInfo.StartTime > DateTime.Now || topicInfo.EndTime <= DateTime.Now)
                {
                    BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_TOPIC_INFO + topicId);
                    return null;
                }
            }
            return topicInfo;
        }

        /// <summary>
        /// 获得活动专题
        /// </summary>
        /// <param name="topicSN">活动专题编号</param>
        /// <returns></returns>
        public static TopicInfo GetTopicBySN(string topicSN)
        {
            TopicInfo topicInfo = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_TOPIC_INFO + topicSN) as TopicInfo;
            if (topicInfo == null)
            {
                topicInfo = BrnShop.Data.Topics.GetTopicBySNAndTime(topicSN, DateTime.Now);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_TOPIC_INFO + topicSN, topicInfo);
            }
            else
            {
                if (topicInfo.StartTime > DateTime.Now || topicInfo.EndTime <= DateTime.Now)
                {
                    BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_TOPIC_INFO + topicSN);
                    return null;
                }
            }
            return topicInfo;
        }

        /// <summary>
        /// 生成活动专题编号
        /// </summary>
        /// <returns></returns>
        public static string GenerateTopicSN()
        {
            string sn = Randoms.CreateRandomValue(16, false);
            while (IsExistTopicSN(sn))
                sn = Randoms.CreateRandomValue(16, false);
            return sn;
        }

        /// <summary>
        /// 判断活动专题编号是否存在
        /// </summary>
        /// <param name="topicSN">活动专题编号</param>
        /// <returns></returns>
        public static bool IsExistTopicSN(string topicSN)
        {
            return BrnShop.Data.Topics.IsExistTopicSN(topicSN);
        }
    }
}
