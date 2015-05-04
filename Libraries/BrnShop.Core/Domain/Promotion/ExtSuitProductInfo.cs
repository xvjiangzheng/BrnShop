using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 扩展套装商品信息类
    /// </summary>
    public class ExtSuitProductInfo
    {
        private int _recordid;//记录id
        private int _pmid;//套装促销活动id
        private int _discount;//折扣
        private int _number;//数量
        private int _pid;//商品id
        private string _psn = "";//商品货号
        private int _cateid = 0;//商品分类id
        private int _brandid = 0;//商品品牌id
        private int _skugid = 0;//商品sku组id
        private string _name = "";//商品名称
        private decimal _shopprice = 0M;//商品商城价
        private decimal _marketprice = 0M;//商品市场价
        private decimal _costprice = 0M;//商品成本价
        private int _state = 0;//0代表上架，1代表下架，2代表回收站
        private int _isbest = 0;//商品是否精品
        private int _ishot = 0;//商品是否热销
        private int _isnew = 0;//商品是否新品
        private int _displayorder = 0;//商品排序
        private int _weight = 0;//商品重量
        private string _showimg = "";//商品展示图片

        /// <summary>
        /// 记录id
        /// </summary>
        public int RecordId
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 套装促销活动id
        /// </summary>
        public int PmId
        {
            set { _pmid = value; }
            get { return _pmid; }
        }
        /// <summary>
        /// 折扣
        /// </summary>
        public int Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 商品货号
        /// </summary>
        public string PSN
        {
            set { _psn = value.TrimEnd(); }
            get { return _psn; }
        }
        /// <summary>
        /// 商品分类id
        /// </summary>
        public int CateId
        {
            set { _cateid = value; }
            get { return _cateid; }
        }
        /// <summary>
        /// 商品品牌id
        /// </summary>
        public int BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
        }
        /// <summary>
        /// 商品sku组id
        /// </summary>
        public int SKUGid
        {
            set { _skugid = value; }
            get { return _skugid; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 商品商城价
        /// </summary>
        public decimal ShopPrice
        {
            set { _shopprice = value; }
            get { return _shopprice; }
        }
        /// <summary>
        /// 商品市场价
        /// </summary>
        public decimal MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        /// <summary>
        /// 商品成本价
        /// </summary>
        public decimal CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 0代表上架，1代表下架，2代表回收站，3代表隐藏品牌，4代表隐藏分类
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 商品是否精品
        /// </summary>
        public int IsBest
        {
            set { _isbest = value; }
            get { return _isbest; }
        }
        /// <summary>
        /// 商品是否热销
        /// </summary>
        public int IsHot
        {
            set { _ishot = value; }
            get { return _ishot; }
        }
        /// <summary>
        /// 商品是否新品
        /// </summary>
        public int IsNew
        {
            set { _isnew = value; }
            get { return _isnew; }
        }
        /// <summary>
        /// 商品排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 商品展示图片
        /// </summary>
        public string ShowImg
        {
            set { _showimg = value; }
            get { return _showimg; }
        }
    }
}
