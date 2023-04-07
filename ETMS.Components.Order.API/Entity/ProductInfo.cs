using System;

namespace ETMS.Components.Order.API.Entity
{
    [Serializable]
    public partial class OrderDetail
    {
        public Guid OrderDetailID { get; set; }
        public Guid ProductID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 商品原价
        /// </summary>
        public decimal ProductPrice { get; set; }
        /// <summary>
        /// 商品销售价
        /// </summary>
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// 优惠券优惠额
        /// </summary>
        public decimal Coupon { get; set; }
        /// <summary>
        /// 优惠码，代理商商品优惠码
        /// </summary>
        public string AgencyCode { get; set; }
    }
}
