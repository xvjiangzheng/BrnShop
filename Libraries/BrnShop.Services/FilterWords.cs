using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 筛选词操作管理类
    /// </summary>
    public partial class FilterWords
    {
        private static object _locker = new object();//锁对象
        private static Regex _ignorewordsregex;//忽略词正则表达式

        static FilterWords()
        {
            string[] wordList = StringHelper.SplitString(BSPConfig.ShopConfig.IgnoreWords, "\n");
            if (wordList.Length > 0)
            {
                StringBuilder pattern = new StringBuilder("(");
                foreach (string word in wordList)
                    pattern.AppendFormat("{0}|", CommonHelper.EscapeRegex(word));
                pattern.Remove(pattern.Length - 1, 1);
                pattern.Append(")");
                _ignorewordsregex = new Regex(pattern.ToString(), RegexOptions.IgnoreCase);
            }
        }

        /// <summary>
        /// 重置忽略词正则表达式
        /// </summary>
        public static void ResetIgnoreWordsRegex()
        {
            lock (_locker)
            {
                string[] wordList = StringHelper.SplitString(BSPConfig.ShopConfig.IgnoreWords, "\n");
                if (wordList.Length > 0)
                {
                    StringBuilder pattern = new StringBuilder("(");
                    foreach (string word in wordList)
                        pattern.AppendFormat("{0}|", CommonHelper.EscapeRegex(word));
                    pattern.Remove(pattern.Length - 1, 1);
                    pattern.Append(")");
                    _ignorewordsregex = new Regex(pattern.ToString(), RegexOptions.IgnoreCase);
                }
                else
                {
                    _ignorewordsregex = null;
                }
            }
        }

        /// <summary>
        /// 获得筛选词列表
        /// </summary>
        /// <returns></returns>
        public static List<FilterWordInfo> GetFilterWordList()
        {
            return BrnShop.Data.FilterWords.GetFilterWordList();
        }

        /// <summary>
        /// 获得筛选词正则列表
        /// </summary>
        /// <returns></returns>
        public static object[,] GetFilterWordRegexList()
        {
            object[,] filterWordRegexList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_FILTERWORD_REGEXLIST) as object[,];
            if (filterWordRegexList == null)
            {
                List<FilterWordInfo> filterWordList = GetFilterWordList();
                filterWordRegexList = new object[filterWordList.Count, 2];
                for (int i = 0; i < filterWordList.Count; i++)
                {
                    filterWordRegexList[i, 0] = new Regex(filterWordList[i].Match, RegexOptions.IgnoreCase);
                    filterWordRegexList[i, 1] = filterWordList[i].Replace;
                }
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_FILTERWORD_REGEXLIST, filterWordRegexList);
            }
            return filterWordRegexList;
        }

        /// <summary>
        /// 移除内容中的忽略词
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        private static string RemoveIgnoreWords(string content)
        {
            if (_ignorewordsregex == null)
                return content;

            return _ignorewordsregex.Replace(content, "");
        }

        /// <summary>
        /// 判断内容中是否包含匹配词
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool IsContainWords(string content)
        {
            if (content == null || content.Length == 0)
                return false;

            object[,] filterWordRegexList = GetFilterWordRegexList();
            if (filterWordRegexList.Length == 0)
                return false;

            content = RemoveIgnoreWords(content);
            int length = filterWordRegexList.Length / 2;
            for (int i = 0; i < length; i++)
            {
                if (((Regex)filterWordRegexList[i, 0]).IsMatch(content))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 隐藏内容中的匹配词
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string HideWords(string content)
        {
            if (content == null || content.Length == 0)
                return "";

            object[,] filterWordRegexList = GetFilterWordRegexList();
            if (filterWordRegexList.Length == 0)
                return content;

            content = RemoveIgnoreWords(content);
            int length = filterWordRegexList.Length / 2;
            for (int i = 0; i < length; i++)
            {
                if (((Regex)filterWordRegexList[i, 0]).IsMatch(content))
                    content = ((Regex)filterWordRegexList[i, 0]).Replace(content, "***");
            }
            return content;
        }

        /// <summary>
        /// 获得内容中的匹配词
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static List<string> GetWords(string content)
        {
            List<string> words = new List<string>();

            if (content == null || content.Length == 0)
                return words;

            object[,] filterWordRegexList = GetFilterWordRegexList();
            if (filterWordRegexList.Length == 0)
                return words;

            content = RemoveIgnoreWords(content);
            int length = filterWordRegexList.Length / 2;
            for (int i = 0; i < length; i++)
            {
                if (((Regex)filterWordRegexList[i, 0]).IsMatch(content))
                {
                    foreach (Match item in ((Regex)filterWordRegexList[i, 0]).Matches(content))
                    {
                        words.Add(item.Value);
                    }
                }
            }
            return words;
        }

        /// <summary>
        /// 获得内容中的匹配词
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string GetWord(string content)
        {
            if (content == null || content.Length == 0)
                return "";

            object[,] filterWordRegexList = GetFilterWordRegexList();
            if (filterWordRegexList.Length == 0)
                return "";

            content = RemoveIgnoreWords(content);
            int length = filterWordRegexList.Length / 2;
            for (int i = 0; i < length; i++)
            {
                if (((Regex)filterWordRegexList[i, 0]).IsMatch(content))
                {
                    return ((Regex)filterWordRegexList[i, 0]).Match(content).Value;
                }
            }
            return "";
        }
    }
}
