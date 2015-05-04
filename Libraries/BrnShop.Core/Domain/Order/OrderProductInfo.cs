using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 订单商品信息类
    /// </summary>
    public class OrderProductInfo
    {
        private int _recordid;//记录id
        private int _oid = 0;//订单id
        private int _uid = 0;//用户id
        private string _sid = "";//sessionId
        private int _pid;//商品id
        private string _psn;//商品编码
        private int _cateid;//分类id
        private int _brandid;//品牌id
        private string _name;//商品名称
        private string _showimg;//商品展示图片
        private decimal _discountprice;//商品折扣价格
        private decimal _shopprice;//商品商城价格
        private decimal _costprice;//商品成本价格
        private decimal _marketprice;//商品市场价格
        private int _weight;//商品重量
        private int _isreview;//是否评价(0代表未评价，1代表已评价)
        private int _realcount = 0;//真实数量
        private int _buycount = 0;//商品购买数量
        private int _sendcount = 0;//商品邮寄数量
        private int _type = 0;//商品类型(0为普遍商品,1为普通商品赠品,2为套装商品赠品,3为套装商品,4满赠商品)
        private int _paycredits = 0;//支付积分
        private int _coupontypeid = 0;//赠送优惠劵类型id
        private int _extcode1 = 0;//普通商品时为单品促销活动id,赠品时为赠品促销活动id,套装商品时为套装促销活动id,满赠赠品时为满赠促销活动id
        private int _extcode2 = 0;//普通商品时为买送促销活动id,赠品时为赠品赠送数量,套装商品时为套装商品数量
        private int _extcode3 = 0;//普通商品时为赠品促销活动id,套装商品时为赠品促销活动id
        private int _extcode4 = 0;//普通商品时为满赠促销活动id
        private int _extcode5 = 0;//普通商品时为满减促销活动id
        private DateTime _addtime;//添加时间

        /// <summary>
        /// 记录id
        /// </summary>
        public int RecordId
        {
            get { return _recordid; }
            set { _recordid = value; }
        }
        /// <summary>
        /// 订单id
        /// </summary>
        public int Oid
        {
            get { return _oid; }
            set { _oid = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        /// <summary>
        /// sessionId
        /// </summary>
        public string Sid
        {
            get { return _sid; }
            set { _sid = value.TrimEnd(); }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string PSN
        {
            get { return _psn; }
            set { _psn = value.TrimEnd(); }
        }
        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId
        {
            get { return _cateid; }
            set { _cateid = value; }
        }
        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId
        {
            get { return _brandid; }
            set { _brandid = value; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 商品展示图片
        /// </summary>
        public string ShowImg
        {
            get { return _showimg; }
            set { _showimg = value; }
        }
        /// <summary>
        /// 商品折扣价格
        /// </summary>
        public decimal DiscountPrice
        {
            get { return _discountprice; }
            set { _discountprice = value; }
        }
        /// <summary>
        /// 商品商城价格
        /// </summary>
        public decimal ShopPrice
        {
            get { return _shopprice; }
            set { _shopprice = value; }
        }
        /// <summary>
        /// 商品成本价格
        /// </summary>
        public decimal CostPrice
        {
            get { return _costprice; }
            set { _costprice = value; }
        }
        /// <summary>
        /// 商品市场价格
        /// </summary>
        public decimal MarketPrice
        {
            get { return _marketprice; }
            set { _marketprice = value; }
        }
        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        /// <summary>
        /// 是否评价(0代表未评价，1代表已评价)
        /// </summary>
        public int IsReview
        {
            get { return _isreview; }
            set { _isreview = value; }
        }
        /// <summary>
        /// 真实数量
        /// </summary>
        public int RealCount
        {
            get { return _realcount; }
            set { _realcount = value; }
        }
        /// <summary>
        /// 商品购买数量
        /// </summary>
        public int BuyCount
        {
            get { return _buycount; }
            set { _buycount = value; }
        }
        /// <summary>
        /// 商品邮寄数量
        /// </summary>
        public int SendCount
        {
            get { return _sendcount; }
            set { _sendcount = value; }
        }
        /// <summary>
        /// 商品类型(0为普遍商品,1为普通商品赠品,2为套装商品赠品,3为套装商品,4满赠商品)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 支付积分
        /// </summary>
        public int PayCredits
        {
            get { return _paycredits; }
            set { _paycredits = value; }
        }
        /// <summary>
        /// 赠送优惠劵类型id
        /// </summary>
        public int CouponTypeId
        {
            get { return _coupontypeid; }
            set { _coupontypeid = value; }
        }
        /// <summary>
        /// 普通商品时为单品促销活动id,赠品时为赠品促销活动id,套装商品时为套装促销活动id,满赠赠品时为满赠促销活动id
        /// </summary>
        public int ExtCode1
        {
            get { return _extcode1; }
            set { _extcode1 = value; }
        }
        /// <summary>
        /// 普通商品时为买送促销活动id,赠品时为赠品赠送数量,套装商品时为套装商品数量
        /// </summary>
        public int ExtCode2
        {
            get { return _extcode2; }
            set { _extcode2 = value; }
        }
        /// <summary>
        /// 普通商品时为赠品促销活动id,套装商品时为赠品促销活动id
        /// </summary>
        public int ExtCode3
        {
            get { return _extcode3; }
            set { _extcode3 = value; }
        }
        /// <summary>
        /// 普通商品时为满赠促销活动id
        /// </summary>
        public int ExtCode4
        {
            get { return _extcode4; }
            set { _extcode4 = value; }
        }
        /// <summary>
        /// 普通商品时为满减促销活动id
        /// </summary>
        public int ExtCode5
        {
            get { return _extcode5; }
            set { _extcode5 = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
    }
}

