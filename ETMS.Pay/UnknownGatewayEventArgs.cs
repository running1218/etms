﻿
namespace ETMS.Pay
{
    /// <summary>
    /// 未知网关事件数据
    /// </summary>
    public class UnknownGatewayEventArgs : PaymentEventArgs
    {

        #region 构造函数

        /// <summary>
        /// 初始化未知网关事件数据
        /// </summary>
        /// <param name="gateway">支付网关</param>
        public UnknownGatewayEventArgs(GatewayBase gateway)
            : base(gateway)
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 订单数据
        /// </summary>
        public Order Order
        {
            get
            {
                return gateway.Order;
            }
        }
        #endregion
    }
}