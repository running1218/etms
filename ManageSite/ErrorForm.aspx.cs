//==================================================================================================
//Version 1.0
//Created By ZhouYonghua
//Date: 2007-6-8
//==================================================================================================
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Globalization;


public partial class ErrorForm : ETMS.Controls.BasePage
{
    public string RedirctUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string msg = null;
        Exception ex = HttpContext.Current.Server.GetLastError();
        if (ex != null)
        {
            Exception exInner = ex.InnerException;
            Exception exBase = ex.GetBaseException();

            if (exInner is System.Reflection.TargetInvocationException)//如果是：目标调用发生异常，取内部异常
            {
                if (exInner.InnerException != null)
                {
                    exInner = exInner.InnerException;
                }
            }
            /*
             *  为了保证所有的异常都显示最终消息，内部消息则记录数据库。
             *  例如：有些出错信息不应该提供给用户，但日志需要详细记录。
             *        则通过页面捕获异常后，包装此异常到新异常（新异常描述：系统出错！）内部，然后抛出。
             * */
            Exception logException = null;
            if (exInner != null)
            {
                logException = exInner;
            }
            else
            {
                logException = ex;
            }

            if ( !(logException is ETMS.Security.InValidRequestException)
                    && !(logException is ETMS.Security.AuthenticateException)
                    && !(logException is ETMS.Security.AuthorizationException)
                 )
            {
                ETMS.Utility.Logging.ErrorLogHelper.Error(logException);
                divBack.InnerHtml = string.Format("<input type=\"button\" id=\"ButtonReturnTest\" onclick=\"javascript:history.back();\" value=\"返回\" class=\"btn_errorReturn\" />");
            }
            else
            {
                ETMS.Security.DefaultAuthenticator.ForceClearAllAppTicket();
                RedirctUrl = HttpUtility.UrlDecode(string.Format("http://{0}{1}", Request.Url.Authority, this.ResolveUrl("~/Default.aspx")));
                divBack.InnerHtml = string.Format("<input type=\"button\" id=\"ButtonReturnTest\" onclick=\"goBack();\" value=\"返回\" class=\"btn_errorReturn\" />");
            }

            //转换异常信息
            msg = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(logException);
            MsgInfoText.Text = msg;            
        } 
    }
}
