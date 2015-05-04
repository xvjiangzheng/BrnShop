using System;
using System.Threading;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop事件管理类
    /// </summary>
    public class BSPEvent
    {
        private static Timer _timer;//定时器

        static BSPEvent()
        {
            EventConfigInfo eventConfigInfo = BSPConfig.EventConfig;
            if (eventConfigInfo.BSPEventState == 1)
                _timer = new Timer(new TimerCallback(Processor), null, 60000, eventConfigInfo.BSPEventPeriod * 60000);
        }

        /// <summary>
        /// 此方法为空，只是起到激活BrnShop事件处理机制的作用
        /// </summary>
        public static void Start() { }

        /// <summary>
        /// 执行指定事件
        /// </summary>
        public static void Execute(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            EventConfigInfo eventConfigInfo = BSPConfig.EventConfig;
            if (eventConfigInfo.BSPEventState == 0 || eventConfigInfo.BSPEventList.Count == 0)
                return;

            EventInfo eventInfo = eventConfigInfo.BSPEventList.Find(x => x.Key == key);
            if (eventInfo != null && eventInfo.Instance != null)
            {
                eventInfo.LastExecuteTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(eventInfo.Instance.Execute, eventInfo);
            }
        }

        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="state">参数对象</param>
        private static void Processor(object state)
        {
            EventConfigInfo eventConfigInfo = BSPConfig.EventConfig;
            if (eventConfigInfo.BSPEventState == 0 || eventConfigInfo.BSPEventList.Count == 0)
                return;

            //循环执行每个事件
            foreach (EventInfo eventInfo in eventConfigInfo.BSPEventList)
            {
                //如果事件未开启则跳过
                if (eventInfo.Enabled == 0)
                    continue;

                //如果事件实例为空则跳过
                if (eventInfo.Instance == null)
                    continue;

                //当前时间
                DateTime nowTime = DateTime.Now;

                if (eventInfo.TimeType == 0)//特定时间执行
                {
                    //事件今天应该执行的时间
                    DateTime executeTime = nowTime.Date.AddMinutes(eventInfo.TimeValue);
                    //当事件今天已经执行或者还未达到今天的执行时间则跳出
                    if (!(eventInfo.LastExecuteTime.Value <= executeTime && nowTime >= executeTime))
                        continue;
                }
                else if (eventInfo.TimeType == 1)//时间间隔执行
                {
                    //当前时间还未达到下次执行时间时跳出
                    if ((nowTime - eventInfo.LastExecuteTime.Value).Minutes < eventInfo.TimeValue)
                        continue;
                }
                else
                {
                    continue;
                    //throw new BSPException("事件：" + eventInfo.Key + "的时间类型只能是0或1");
                }

                eventInfo.LastExecuteTime = nowTime;
                ThreadPool.QueueUserWorkItem(eventInfo.Instance.Execute, eventInfo);
            }
        }
    }
}
