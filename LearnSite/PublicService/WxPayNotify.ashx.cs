using ETMS.Components.Order.Implement.BLL;
using ETMS.Pay;
using System;
using System.Text;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for WxPayNotify
    /// </summary>
    public class WxPayNotify : IHttpHandler
    {
        private OrderLogic logic = new OrderLogic();
        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            try
            {
                NotifyEvent();
            }
            catch (Exception ex)
            {
                ETMS.Utility.Logging.ErrorLogHelper.WriteLog(ex.ToString());
            }                    
        }

        private void NotifyEvent()
        {
            WxPayData data = GetNotifyData();
            string resXml = string.Empty;

            if (data.GetValue("result_code").ToString() == "SUCCESS" && data.GetValue("return_code").ToString() == "SUCCESS")
            {
                string orderNo = data.GetValue("out_trade_no").ToString();
                //更改支付状态
                logic.UpdateOrderStatus(orderNo, 1);
                //学生选课
                logic.GenerateChooseCourse(orderNo);

                resXml = "<xml>" + "<return_code><![CDATA[SUCCESS]]></return_code>"
                        + "<return_msg><![CDATA[OK]]></return_msg>" + "</xml> ";

            }
            else
            {
                logic.UpdateOrderStatus(data.GetValue("out_trade_no").ToString(), 2);

                resXml = "<xml>" + "<return_code><![CDATA[FAIL]]></return_code>"
                        + "<return_msg><![CDATA[支付失败]]></return_msg>" + "</xml> ";
            }

            ETMS.Utility.Logging.ErrorLogHelper.WriteLog(GetNotifyData().ToJson());

            currentContext.Response.Write(resXml);           
        }

        public WxPayData GetNotifyData()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = currentContext.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            //Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                currentContext.Response.Write(res.ToXml());
            }

            return data;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}