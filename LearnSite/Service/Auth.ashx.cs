using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Web;
using University.Mooc.Security;

namespace ETMS.Studying.Service
{
    /// <summary>
    /// Summary description for Auth
    /// </summary>
    public class Auth : IHttpHandler
    {
        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            ReturnResponseContent(UserAuth());
        }

        protected string UserAuth()
        {
            string username = currentContext.Request["UserName"].ToString().Trim();
            string oriPassword = currentContext.Request["Password"].ToString().Trim();
            string password = this.Decode(oriPassword);
            bool isSaveUserName = false;
            bool isAutoSignIn = false;

            //调用登录
            try
            {
                //登录验证
                string redirectUrl = string.Format("{0}/Index.aspx", WebUtility.AppPath); ;
                User userInfo = new UserLogic().GetUserByLoginName(username);

                if (userInfo != null)
                {
                    new SignInPageData()
                    {
                        IsAutoSignIn = false,
                        IsSaveUserName = false,
                        UserName = username
                    }.SaveToCookie();

                    DefaultAuthenticator.SignIn(username, ETMS.Utility.Cryptography.MD5Utility.MD516(password), isSaveUserName, isAutoSignIn);
                    User u1 = new User();
                    u1.UserID = userInfo.UserID;
                    u1.RealName = userInfo.RealName;
                    return JsonHelper.GetInvokeSuccessJson(u1);
                }
                else
                {
                    return JsonHelper.GetInvokeFailedJson(-1, "用户不存在");
                }
            }
            catch (ETMS.AppContext.BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, University.Mooc.AppContext.BusinessException.GetBusinessErrorCode(ex));
            }
            catch (University.Mooc.AppContext.BusinessException ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, University.Mooc.AppContext.BusinessException.GetBusinessErrorCode(ex));
            }
            catch(Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "系统错误，请与管理员联系!");
            }
        }

        private string Decode(string rawStr)
        {
            int len = rawStr.Length;
            if (len % 4 != 0)
            {
                return "error!";
            }
            int curCharCode = 0;
            List<string> resultStr = new List<string>();
            for (int i = 0; i < len; i += 4)
            {
                curCharCode = Convert.ToInt32(rawStr.Substring(i, 4), 16);
                resultStr.Add(Convert.ToString((char)curCharCode));
            }
            return string.Concat(resultStr).Replace("\n", "\r\r").Replace("\r\r", "\r\n");
        }

        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
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