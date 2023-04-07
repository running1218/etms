using System;

using System.Web;
using System.Web.SessionState;
namespace ETMS.Utility.Service.ValidCode
{
    /// <summary>
    /// 验证码处理器，需要session支持
    /// </summary>
    public class ValidCodeHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            if (action.Equals("Image", StringComparison.InvariantCultureIgnoreCase))
            {
                Image(context);
            }
            else if (action.Equals("valid", StringComparison.InvariantCultureIgnoreCase))
            {
                Valid(context);
            }
            else
            {
                context.Response.Write("非法访问！");
                context.Response.End();
            }
        }

        public void Image(HttpContext context)
        {
            int validCodeLength = 0;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["System.ValidCode.Length"], out validCodeLength);
            ValidCodeUtility.CreateValidateGraphic(context, (validCodeLength > 0 ? validCodeLength : 5));
        }

        public void Valid(HttpContext context)
        {
            if (context.Request.QueryString["validcode"] != null
                && context.Request.QueryString["validcode"].Equals(ETMS.Utility.ValidCodeUtility.ValidateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                ResponseJsonResult(context.Response, true);
            }
            else
            {
                ResponseJsonResult(context.Response, false);
            }
        }

        /// <summary>
        /// 获取Json返回结果{IsSuccess={true|false},Message=''}
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private void ResponseJsonResult(HttpResponse Response, bool isSuccess)
        {
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-type", "application/json");
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { IsSuccess = isSuccess }));
        }

    }
}
