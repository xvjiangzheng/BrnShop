using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// 事件配置信息类
    /// </summary>
    [Serializable]
    public class EventConfigInfo : IConfigInfo
    {
        private int _bspeventstate;//BrnShop事件状态
        private int _bspeventperiod;//BrnShop事件执行间隔(单位为分钟)
        private List<EventInfo> _bspeventlist;//BrnShop事件列表

        /// <summary>
        /// BrnShop事件状态
        /// </summary>
        public int BSPEventState
        {
            get { return _bspeventstate; }
            set { _bspeventstate = value; }
        }
        /// <summary>
        /// BrnShop事件执行间隔(单位为分钟)
        /// </summary>
        public int BSPEventPeriod
        {
            get { return _bspeventperiod; }
            set { _bspeventperiod = value; }
        }
        /// <summary>
        /// BrnShop事件列表
        /// </summary>
        public List<EventInfo> BSPEventList
        {
            get { return _bspeventlist; }
            set { _bspeventlist = value; }
        }
    }
}
