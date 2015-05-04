using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// 购物车项信息类
    /// </summary>
    public class CartItemInfo : IComparable<CartItemInfo>
    {
        private int _type;//类型(0代表项为购物车商品,1代表项为购物车套装,2代表项为购物车满赠,3代表项为购物车满减)
        private object _item;//项

        /// <summary>
        /// 类型(0代表项为购物车商品,1代表项为购物车套装,2代表项为购物车满赠,3代表项为购物车满减)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 项
        /// </summary>
        public object Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public int CompareTo(CartItemInfo other)
        {
            if (this.Type > other.Type)
                return 1;
            if (this.Type < other.Type)
                return -1;
            return 0;
        }
    }

    /// <summary>
    /// 购物车商品信息类
    /// </summary>
    public class CartProductInfo
    {
        private bool _selected = true;//是否选中
        private OrderProductInfo _orderproductinfo;//商品信息
        private List<OrderProductInfo> _giftlist = null;//赠品列表

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        /// <summary>
        /// 商品信息
        /// </summary>
        public OrderProductInfo OrderProductInfo
        {
            get { return _orderproductinfo; }
            set { _orderproductinfo = value; }
        }
        /// <summary>
        /// 赠品列表
        /// </summary>
        public List<OrderProductInfo> GiftList
        {
            get { return _giftlist; }
            set { _giftlist = value; }
        }
    }

    /// <summary>
    /// 购物车套装信息类
    /// </summary>
    public class CartSuitInfo
    {
        private bool _checked = true;//是否选中
        private int _pmid;//套装促销活动id
        private int _buycount;//购买数量
        private decimal _suitprice;//套装价格
        private decimal _suitamount;//套装合计
        private List<CartProductInfo> _cartproductlist;//购物车商品列表

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        /// <summary>
        /// 套装促销活动id
        /// </summary>
        public int PmId
        {
            get { return _pmid; }
            set { _pmid = value; }
        }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyCount
        {
            get { return _buycount; }
            set { _buycount = value; }
        }
        /// <summary>
        /// 套装价格
        /// </summary>
        public decimal SuitPrice
        {
            get { return _suitprice; }
            set { _suitprice = value; }
        }
        /// <summary>
        /// 套装合计
        /// </summary>
        public decimal SuitAmount
        {
            get { return _suitamount; }
            set { _suitamount = value; }
        }
        /// <summary>
        /// 购物车商品列表
        /// </summary>
        public List<CartProductInfo> CartProductList
        {
            get { return _cartproductlist; }
            set { _cartproductlist = value; }
        }
    }

    /// <summary>
    /// 购物车满赠信息类
    /// </summary>
    public class CartFullSendInfo
    {
        private bool _isenough = false;//是否达到满赠促销活动的金额
        private FullSendPromotionInfo _fullsendpromotioninfo;//满赠促销活动
        private OrderProductInfo _fullsendminororderproductinfo = null;//满赠赠品
        private List<CartProductInfo> _fullsendmaincartproductlist;//满赠主商品列表

        /// <summary>
        /// 是否达到满赠促销活动的金额
        /// </summary>
        public bool IsEnough
        {
            get { return _isenough; }
            set { _isenough = value; }
        }
        /// <summary>
        /// 满赠促销活动
        /// </summary>
        public FullSendPromotionInfo FullSendPromotionInfo
        {
            get { return _fullsendpromotioninfo; }
            set { _fullsendpromotioninfo = value; }
        }
        /// <summary>
        /// 满赠赠品
        /// </summary>
        public OrderProductInfo FullSendMinorOrderProductInfo
        {
            get { return _fullsendminororderproductinfo; }
            set { _fullsendminororderproductinfo = value; }
        }
        /// <summary>
        /// 满赠主商品列表
        /// </summary>
        public List<CartProductInfo> FullSendMainCartProductList
        {
            get { return _fullsendmaincartproductlist; }
            set { _fullsendmaincartproductlist = value; }
        }
    }

    /// <summary>
    /// 购物车满减信息类
    /// </summary>
    public class CartFullCutInfo
    {
        private bool _isenough = false;//是否达到满减促销活动的金额
        private int _limitmoney = 0;//限制金额
        private int _cutmoney = 0;//减小金额
        private FullCutPromotionInfo _fullcutpromotioninfo;//满减促销活动
        private List<CartProductInfo> _fullcutcartproductlist;//满减商品列表

        /// <summary>
        /// 是否达到满减促销活动的金额
        /// </summary>
        public bool IsEnough
        {
            get { return _isenough; }
            set { _isenough = value; }
        }
        /// <summary>
        /// 限制金额
        /// </summary>
        public int LimitMoney
        {
            get { return _limitmoney; }
            set { _limitmoney = value; }
        }
        /// <summary>
        /// 减小金额
        /// </summary>
        public int CutMoney
        {
            get { return _cutmoney; }
            set { _cutmoney = value; }
        }
        /// <summary>
        /// 满减促销活动
        /// </summary>
        public FullCutPromotionInfo FullCutPromotionInfo
        {
            get { return _fullcutpromotioninfo; }
            set { _fullcutpromotioninfo = value; }
        }
        /// <summary>
        /// 满减商品列表
        /// </summary>
        public List<CartProductInfo> FullCutCartProductList
        {
            get { return _fullcutcartproductlist; }
            set { _fullcutcartproductlist = value; }
        }
    }
}
