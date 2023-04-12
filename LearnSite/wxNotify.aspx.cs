using ETMS.Components.Order.Implement.BLL;
using ETMS.Pay;
using System;
using System.Text;
using System.Web.UI;

namespace ETMS.Studying.Study
{
    public partial class WxPayNotify : System.Web.UI.Page
    {
        private OrderLogic logic = new OrderLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    NotifyEvent(this);
                }
                catch (Exception ex)
                {
                    ETMS.Utility.Logging.ErrorLogHelper.WriteLog(ex.ToString());
                }
            }
        }

        private void NotifyEvent(Page page)
        {
            WxPayData data = GetNotifyData(page);
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
                lblMsg.Text = "支付成功，请点击我的课程->精品课程  进入";
            }
            else {
                resXml = "<xml>" + "<return_code><![CDATA[FAIL]]></return_code>"
                        + "<return_msg><![CDATA[支付失败]]></return_msg>" + "</xml> ";
                lblMsg.Text = "支付失败，请重新选购课程";
            }

            ETMS.Utility.Logging.ErrorLogHelper.WriteLog(data.ToJson());

            page.Response.Write(resXml);
        }

        public WxPayData GetNotifyData(Page page)
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = page.Request.InputStream;
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
                page.Response.Write(res.ToXml());
            }

            return data;
        }
    }
}