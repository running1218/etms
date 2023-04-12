using ETMS.Components.Order.Implement.BLL;
using ETMS.Pay;
using ETMS.Utility;
using System;
using System.Collections.Generic;

namespace ETMS.Studying.Study
{
    public partial class PayNotify : System.Web.UI.Page
    {
        private OrderLogic logic = new OrderLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            //设置商户数据
            Merchant alipayMerchant = new Merchant();
            alipayMerchant.GatewayType = GatewayType.Alipay;
            alipayMerchant.UserName = PaymentHelper.AlipayMerchantId;
            alipayMerchant.Key = PaymentHelper.AlipayMerchantKey;

            Merchant weChatPaymentMerchant = new Merchant();
            weChatPaymentMerchant.GatewayType = GatewayType.WeChatPayment;
            weChatPaymentMerchant.UserName = PaymentHelper.WeChatMerchantId;
            weChatPaymentMerchant.Key = PaymentHelper.WeChatMerchantKey;

            // 添加到商户数据集合
            List<Merchant> merchantList = new List<Merchant>();
            merchantList.Add(alipayMerchant);
            merchantList.Add(weChatPaymentMerchant);

            // 订阅支付通知事件
            PaymentNotify notify = new PaymentNotify(merchantList);
            notify.PaymentSucceed += new PaymentSucceedEventHandler(notify_PaymentSucceed);
            notify.PaymentFailed += new PaymentFailedEventHandler(notify_PaymentFailed);
            notify.UnknownGateway += new UnknownGatewayEventHandler(notify_UnknownGateway);

            // 接收并处理支付通知
            notify.Received();
        }
        // 支付成功时时的处理代码
        private void notify_PaymentSucceed(object sender, PaymentSucceedEventArgs e)
        {
            // 当前是用户的浏览器自动返回时显示充值成功页面
            if (e.PaymentNotifyMethod == PaymentNotifyMethod.AutoReturn)
            {
                //更改支付状态
                logic.UpdateOrderStatus(e.Order.Id, 1);
                //学生选课
                logic.GenerateChooseCourse(e.Order.Id);
            }

            hfPayNo.Value = e.Order.Id;
        }
        // 支付失败时的处理代码
        private void notify_PaymentFailed(object sender, PaymentFailedEventArgs e)
        {
            //更改支付状态
            logic.UpdateOrderStatus(e.Order.Id, 2);
            hfPayNo.Value = e.Order.Id;
        }
        // 无法识别支付网关时的处理代码
        private void notify_UnknownGateway(object sender, UnknownGatewayEventArgs e)
        {
            //更改支付状态
            logic.UpdateOrderStatus(e.Order.Id, 2);
        }
    }
}