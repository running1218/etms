using System.Collections.Generic;
using Yunpian.conf;
using Yunpian.lib;
using Yunpian.model;

namespace ETMS.Utility
{
    public class SMSHelper
    {
        public static string YunPianKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["YunPianKey"]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile">接收的手机号码</param>
        /// <param name="code">发送的验证码</param>
        /// <param name="company">发送公司</param>
        /// <returns></returns>
        public static Result SendSigle(string mobile, string code, string company)
        {
            Config config = new Config(YunPianKey);
            Dictionary<string, string> data = new Dictionary<string, string>();

            // 发送单条短信
            SmsOperator sms = new SmsOperator(config);
            data.Clear();
            data.Add("mobile", mobile);

            if (string.IsNullOrEmpty(company))
                data.Add("text", string.Format("您的验证码是{0}", code));
            else
                data.Add("text", string.Format("【{0}】您的验证码是{1}", company, code));

            var result = sms.singleSend(data);
            return result; //new { status=result.success, code=result.statusCode, data=result.responseText };// JsonHelper.SerializeObject(sms.singleSend(data));
        }
    }
}
