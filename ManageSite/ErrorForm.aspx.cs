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

            if (exInner is System.Reflection.TargetInvocationException)//����ǣ�Ŀ����÷����쳣��ȡ�ڲ��쳣
            {
                if (exInner.InnerException != null)
                {
                    exInner = exInner.InnerException;
                }
            }
            /*
             *  Ϊ�˱�֤���е��쳣����ʾ������Ϣ���ڲ���Ϣ���¼���ݿ⡣
             *  ���磺��Щ������Ϣ��Ӧ���ṩ���û�������־��Ҫ��ϸ��¼��
             *        ��ͨ��ҳ�沶���쳣�󣬰�װ���쳣�����쳣�����쳣������ϵͳ�������ڲ���Ȼ���׳���
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
                divBack.InnerHtml = string.Format("<input type=\"button\" id=\"ButtonReturnTest\" onclick=\"javascript:history.back();\" value=\"����\" class=\"btn_errorReturn\" />");
            }
            else
            {
                ETMS.Security.DefaultAuthenticator.ForceClearAllAppTicket();
                RedirctUrl = HttpUtility.UrlDecode(string.Format("http://{0}{1}", Request.Url.Authority, this.ResolveUrl("~/Default.aspx")));
                divBack.InnerHtml = string.Format("<input type=\"button\" id=\"ButtonReturnTest\" onclick=\"goBack();\" value=\"����\" class=\"btn_errorReturn\" />");
            }

            //ת���쳣��Ϣ
            msg = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(logException);
            MsgInfoText.Text = msg;            
        } 
    }
}
