using ETMS.Pay;
using System;
using System.Collections.Specialized;

namespace ETMS.Utility
{
    /// <summary>
    /// 支付辅助类
    /// </summary>
    public class PaymentHelper
    {
        #region 基础配置
        /// <summary>
        /// 支付配置数据源
        /// </summary>
        private static NameValueCollection PaymentSettingsCollection
        {
            get
            {
                return System.Configuration.ConfigurationManager.GetSection("PaymentSettings") as NameValueCollection;
            }
        }
        /// <summary>
        /// 支付宝商户号
        /// </summary>
        public static string AlipayMerchantId
        {
            get
            {
                return PaymentSettingsCollection["AlipayMerchantId"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 支付宝商户秘钥
        /// </summary>
        public static string AlipayMerchantKey
        {
            get
            {
                return PaymentSettingsCollection["AlipayMerchantKey"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 支付宝网关的参数名称
        /// </summary>
        private static string AlipayGatewayParameterName
        {
            get
            {
                return PaymentSettingsCollection["AlipayGatewayParameterName"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 支付宝网关的参数值
        /// </summary>
        private static string AlipayGatewayParameterValue
        {
            get
            {
                return PaymentSettingsCollection["AlipayGatewayParameterValue"] ?? string.Empty;
            }
        }

        /// <summary>
        /// 微信商户号
        /// </summary>
        public static string WeChatMerchantId
        {
            get
            {
                return PaymentSettingsCollection["WeChatMerchantId"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 微信商户秘钥
        /// </summary>
        public static string WeChatMerchantKey
        {
            get
            {
                return PaymentSettingsCollection["WeChatMerchantKey"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 微信网关的参数名称
        /// </summary>
        private static string WeChatGatewayParameterName
        {
            get
            {
                return PaymentSettingsCollection["WeChatGatewayParameterName"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 微信网关的参数值
        /// </summary>
        private static string WeChatGatewayParameterValue
        {
            get
            {
                return PaymentSettingsCollection["WeChatGatewayParameterValue"] ?? string.Empty;
            }
        }
        /// <summary>
        /// 支付通知地址
        /// </summary>
        public static string PayNotifyUrl
        {
            get
            {
                return PaymentSettingsCollection["PayNotifyUrl"] ?? string.Empty;
            }
        }

        public static string WeChatNotifyUrl
        {
            get
            {
                return PaymentSettingsCollection["WeChatNotifyUrl"] ?? string.Empty;
            }
        }
        #endregion
        /// <summary>
        /// 创建支付宝的支付订单
        /// </summary>
        public static void CreateAlipayOrder(string ProductName,double Amount,string OrderNo, string payNotifyUrl)
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.Alipay);
            paymentSetting.SetGatewayParameterValue(AlipayGatewayParameterName, AlipayGatewayParameterValue);
            paymentSetting.Merchant.UserName = AlipayMerchantId;
            paymentSetting.Merchant.Key = AlipayMerchantKey;
            paymentSetting.Merchant.NotifyUrl = new Uri(payNotifyUrl);

            paymentSetting.Order.Amount = Amount;
            paymentSetting.Order.Id = OrderNo;
            paymentSetting.Order.Subject = ProductName;
            
            paymentSetting.Payment();
        }
        /// <summary>
        /// 创建微信的支付订单
        /// </summary>
        public static void CreateWeChatPaymentOrder(string ProductName, double Amount, string OrderNo)
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.WeChatPayment);
            paymentSetting.SetGatewayParameterValue(WeChatGatewayParameterName, WeChatGatewayParameterValue);
            paymentSetting.Merchant.UserName = WeChatMerchantId;
            paymentSetting.Merchant.Key = WeChatMerchantKey;
            paymentSetting.Merchant.NotifyUrl = new Uri(PayNotifyUrl);

            paymentSetting.Order.Amount = Amount;
            paymentSetting.Order.Id = OrderNo;
            paymentSetting.Order.Subject = ProductName;

            paymentSetting.Payment();
        }

        public static string MakeQRCode(string productName, double amount, string orderNo, string payNotifyUrl)
        {
            PaymentSetting paymentSetting = new PaymentSetting(GatewayType.WeChatPayment);
            paymentSetting.SetGatewayParameterValue(WeChatGatewayParameterName, WeChatGatewayParameterValue);
            paymentSetting.Merchant.UserName = WeChatMerchantId;
            paymentSetting.Merchant.Key = WeChatMerchantKey;
            paymentSetting.Merchant.NotifyUrl = new Uri(payNotifyUrl);

            paymentSetting.Order.Amount = amount;
            paymentSetting.Order.Id = orderNo;
            paymentSetting.Order.Subject = productName;
            return paymentSetting.MakeQRCode();
        }
    }
}