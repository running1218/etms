using ETMS.Pay.Providers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace ETMS.Pay
{
    /// <summary>
    /// 设置需要支付的订单的数据，创建支付订单URL地址或HTML表单
    /// </summary>
    /// <remarks>
    /// 因为部分支付网关的编码仅支持GB2312，所以所有支付网关统一使用GB2312编码。
    /// 你需要保证输出HTML代码的页面为GB2312编码，否则可能会因为乱码而造成无法正常创建支付订单和识别支付网关的支付通知。
    /// 通过在 Web.config 中的 configuration/system.web 节点设置 <globalization requestEncoding="gb2312" responseEncoding="gb2312" />
    /// 可以将页面的默认编码设置为GB2312。目前只能使用RMB支付，其他货币支付请阅读相关网关接口文档修改。
    /// </remarks>
    public class PaymentSetting
    {

        #region 字段

        GatewayBase gateway;

        #endregion


        #region 构造函数

        public PaymentSetting(GatewayType gatewayType)
        {
            gateway = CreateGateway(gatewayType);
        }


        public PaymentSetting(GatewayType gatewayType, Merchant merchant, Order order)
            : this(gatewayType)
        {
            gateway.Merchant = merchant;
            gateway.Order = order;
        }

        #endregion


        #region 属性

        /// <summary>
        /// 网关
        /// </summary>
        public GatewayBase Gateway
        {
            get
            {
                return gateway;
            }
        }


        /// <summary>
        /// 商家数据
        /// </summary>
        public Merchant Merchant
        {
            get
            {
                return gateway.Merchant;
            }

            set
            {
                gateway.Merchant = value;
            }
        }


        /// <summary>
        /// 订单数据
        /// </summary>
        public Order Order
        {
            get
            {
                return gateway.Order;
            }

            set
            {
                gateway.Order = value;
            }
        }


        public bool CanQueryNotify
        {
            get
            {
                if (gateway is IQueryUrl || gateway is IQueryForm)
                {
                    return true;
                }

                return false;
            }
        }


        public bool CanQueryNow
        {
            get
            {
                return gateway is IQueryNow;
            }
        }

        #endregion


        #region 方法


        private GatewayBase CreateGateway(GatewayType gatewayType)
        {
            switch (gatewayType)
            {
                case GatewayType.Alipay:
                    {
                        return new AlipayGateway();
                    }

                case GatewayType.WeChatPayment:
                    {
                        return new WeChatPaymentGataway();
                    }

                case GatewayType.Tenpay:
                    {
                        return new TenpayGateway();
                    }

                case GatewayType.Yeepay:
                    {
                        return new YeepayGateway();
                    }

                default:
                    {
                        return new NullGateway();
                    }
            }
        }


        /// <summary>
        /// 创建订单的支付Url、Form表单、二维码。
        /// </summary>
        /// <remarks>
        /// 如果创建的是订单的Url或Form表单将跳转到相应网关支付，如果是二维码将输出二维码图片。
        /// </remarks>
        public void Payment()
        {
            IPaymentUrl paymentUrl = gateway as IPaymentUrl;
            if (paymentUrl != null)
            {
                HttpContext.Current.Response.Redirect(paymentUrl.BuildPaymentUrl());
                return;
            }

            IPaymentForm paymentForm = gateway as IPaymentForm;
            if (paymentForm != null)
            {
                HttpContext.Current.Response.Write(paymentForm.BuildPaymentForm());
                return;
            }

            IPaymentQRCode paymentQRCode = gateway as IPaymentQRCode;
            if (paymentQRCode != null)
            {
                BuildQRCodeImage(paymentQRCode.GetPaymentQRCodeContent());
                return;
            }

            throw new NotSupportedException(gateway.GatewayType + " 没有实现支付接口");
        }

        public string MakeQRCode()
        {
            IPaymentQRCode paymentQRCode = gateway as IPaymentQRCode;
            if (paymentQRCode != null)
            {
                return  paymentQRCode.GetPaymentQRCodeContent();                
            }

            return string.Empty;
        }


        /// <summary>
        /// 查询订单，订单的查询通知数据通过跟支付通知一样的形式反回。用处理网关通知一样的方法接受查询订单的数据。
        /// </summary>
        public void QueryNotify()
        {
            IQueryUrl queryUrl = gateway as IQueryUrl;
            if (queryUrl != null)
            {
                HttpContext.Current.Response.Redirect(queryUrl.BuildQueryUrl());
                return;
            }

            IQueryForm queryForm = gateway as IQueryForm;
            if (queryForm != null)
            {
                HttpContext.Current.Response.Write(queryForm.BuildQueryForm());
                return;
            }

            throw new NotSupportedException(gateway.GatewayType + " 没有实现 IQueryUrl 或 IQueryForm 查询接口");
        }

        
        /// <summary>
        /// 查询订单，立即获得订单的查询结果
        /// </summary>
        /// <returns></returns>
        public bool QueryNow()
        {
            IQueryNow queryNow = gateway as IQueryNow;
            if (queryNow != null)
            {
                return queryNow.QueryNow();
            }

            throw new NotSupportedException(gateway.GatewayType + " 没有实现 IQueryNow 查询接口");
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            Gateway.SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue);
        }


        /// <summary>
        /// 生成并输出二维码图片
        /// </summary>
        /// <param name="qrCodeContent">二维码内容</param>
        private void BuildQRCodeImage(string qrCodeContent)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeScale = 4;  // 二维码大小
            Bitmap image = qrCodeEncoder.Encode(qrCodeContent, Encoding.Default);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            HttpContext.Current.Response.ContentType = "image/x-png";
            HttpContext.Current.Response.BinaryWrite(ms.GetBuffer());
        }

        #endregion

    }
}
