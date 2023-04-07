using System;
using System.Collections.Generic;

namespace ETMS.Components.Order.API.Entity
{
    [Serializable]
    public partial class OrderInfo
    {
        public string OrderNo { get; set; }
        public string OrderDescription { get; set; }
        /// <summary>
        /// 0：待支付；1：支付完成；2：支付失败
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单商品数量
        /// </summary>
        public int BuyNumber { get; set; }
        /// <summary>
        /// 订单支付总金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayFrom { get; set; }
        public int UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public string PayerName { get; set; }
        public DateTime PayTime { get; set; }
        public bool IsChooseCourse { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
