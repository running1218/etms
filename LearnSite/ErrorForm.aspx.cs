using System;
using System.Web;

namespace ETMS.Studying
{
    public partial class ErrorForm : System.Web.UI.Page
    {
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

                ETMS.Utility.Logging.ErrorLogHelper.Error(logException);
                //转换异常信息
                msg = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(logException);
                MsgInfoText.Text = msg;

            }
        }
    }
}