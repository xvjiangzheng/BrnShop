using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品部分信息类
    /// </summary>
    public class PartProductInfo
    {
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
        private int _salecount = 0;//销售数
        private int _visitcount = 0;//访问数
        private int _reviewcount = 0;//评价数
        private int _star1 = 0;//评价星星1
        private int _star2 = 0;//评价星星2
        private int _star3 = 0;//评价星星3
        private int _star4 = 0;//评价星星4
        private int _star5 = 0;//评价星星5
        private DateTime _addtime = DateTime.Now;//商品添加时间

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
        /// 0代表上架，1代表下架，2代表回收站
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
        /// <summary>
        /// 销售数
        /// </summary>
        public int SaleCount
        {
            set { _salecount = value; }
            get { return _salecount; }
        }
        /// <summary>
        /// 访问数
        /// </summary>
        public int VisitCount
        {
            set { _visitcount = value; }
            get { return _visitcount; }
        }
        /// <summary>
        /// 评价数
        /// </summary>
        public int ReviewCount
        {
            set { _reviewcount = value; }
            get { return _reviewcount; }
        }
        /// <summary>
        /// 评价星星1
        /// </summary>
        public int Star1
        {
            set { _star1 = value; }
            get { return _star1; }
        }
        /// <summary>
        /// 评价星星2
        /// </summary>
        public int Star2
        {
            set { _star2 = value; }
            get { return _star2; }
        }
        /// <summary>
        /// 评价星星3
        /// </summary>
        public int Star3
        {
            set { _star3 = value; }
            get { return _star3; }
        }
        /// <summary>
        /// 评价星星4
        /// </summary>
        public int Star4
        {
            set { _star4 = value; }
            get { return _star4; }
        }
        /// <summary>
        /// 评价星星5
        /// </summary>
        public int Star5
        {
            set { _star5 = value; }
            get { return _star5; }
        }
        /// <summary>
        /// 商品添加时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }

        public int GetStarLevel()
        {
            int goodStars = Star1 + Star2 + Star3;
            int allStars = goodStars + Star4 + Star5;

            if (allStars == 0)
                return 100;
            return goodStars * 100 / allStars;
        }
    }

    /// <summary>
    /// 商品信息类
    /// </summary>
    public class ProductInfo : PartProductInfo
    {
        private string _description = "";//商品描述
        
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
    }
}

