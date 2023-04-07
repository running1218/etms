namespace ETMS.Components.Order.API.Entity
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderEnum
    {
        /// <summary>
        /// 订单无效
        /// </summary>
        OrderInvalid = 0,

        /// <summary>
        /// 订单有效
        /// </summary>
        OrderEffective = 1,
    }

    /// <summary>
    /// 付款状态
    /// </summary>
    public enum PayEnum
    {
        /// <summary>
        /// 未付款
        /// </summary>
        NotPaid = 0,

        /// <summary>
        /// 已付款
        /// </summary>
        HasPaid = 1,

        /// <summary>
        /// 付款失败
        /// </summary>
        PaymentFailed = -1
    }
}
