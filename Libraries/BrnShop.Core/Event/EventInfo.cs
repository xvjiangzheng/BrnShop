using System;
using System.Xml.Serialization;

namespace BrnShop.Core
{
    /// <summary>
    /// 事件信息类
    /// </summary>
    public class EventInfo
    {
        private string _key;//唯一键
        private string _title;//标题
        private int _timetype;//时间类型(0代表特定时间，1代表时间间隔)
        private int _timevalue;//时间(单位为分钟)
        private string _classname;//类完全限定名
        private string _code;//代码
        private int _enabled;//是否启动

        /// <summary>
        /// 唯一键
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 时间类型(0代表特定时间，1代表时间间隔)
        /// </summary>
        public int TimeType
        {
            get { return _timetype; }
            set { _timetype = value; }
        }
        /// <summary>
        /// 时间(单位为分钟)
        /// </summary>
        public int TimeValue
        {
            get { return _timevalue; }
            set { _timevalue = value; }
        }
        /// <summary>
        /// 类完全限定名
        /// </summary>
        public string ClassName
        {
            get { return _classname; }
            set { _classname = value; }
        }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        /// <summary>
        /// 是否启动
        /// </summary>
        public int Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        #region 辅助内容

        private IEvent _instance = null;//事件实例
        private DateTime? _lastexecutetime = null;//最后执行时间

        /// <summary>
        /// 事件实例
        /// </summary>
        [XmlIgnoreAttribute]
        public IEvent Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = (IEvent)Activator.CreateInstance(Type.GetType(_classname, false, true));
                    }
                    catch (Exception ex)
                    {
                        //throw new BSPException("创建事件:" + _title + "的实例失败", ex);
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        [XmlIgnoreAttribute]
        public DateTime? LastExecuteTime
        {
            get
            {
                if (_lastexecutetime == null)
                {
                    _lastexecutetime = BrnShop.Core.BSPData.RDBS.GetEventLastExecuteTimeByKey(_key);
                }
                return _lastexecutetime;
            }
            set
            {
                _lastexecutetime = value;
            }
        }

        #endregion
    }
}
