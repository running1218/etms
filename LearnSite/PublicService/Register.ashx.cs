using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using System;
using System.Web;
using University.Mooc.Security;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for Register
    /// </summary>
    public class Register : IHttpHandler
    {
        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = currentContext.Request["Method"];
            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
            }
            switch (method.ToLower())
            {
                case "sendphonecdoe":
                    ReturnResponseContent(SendPhoneCode());
                    break;
                case "registerstudent":
                    ReturnResponseContent(RegisterStudent());
                    break;
            }
        }
        public string SendPhoneCode()
        {
            string phone = currentContext.Request["Phone"].ToString();
            string validCode = Number(6, false);
            var result = SMSHelper.SendSigle(phone, validCode, string.Empty);
            return JsonHelper.SerializeObject(new { success = result.success, statusCode = result.statusCode, validCode = validCode });
            //return JsonHelper.SerializeObject(new { success = true, statusCode = 200, validCode = validCode });
        }

        public string RegisterStudent()
        {
            try
            {
                Site_Student entity = new Site_Student();
                entity.LoginName = currentContext.Request["UserName"].ToString();
                entity.RealName = currentContext.Request["RealName"].ToString();
                entity.PassWord = currentContext.Request["Password"].ToString();
                entity.MobilePhone = currentContext.Request["Phone"].ToString();
                entity.Status = 1;
                entity.OrganizationID = BaseUtility.SiteOrganizationID;
                new Site_StudentLogic().Save(entity);
                AutoLoginAfterRegistered();           
            }
            catch(BusinessException bix)
            {
                return JsonHelper.GetInvokeFailedJson(0, ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bix));
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "注册失败");
            }

            return JsonHelper.GetInvokeSuccessJson();
        }

        private void AutoLoginAfterRegistered()
        {
            string username = currentContext.Request["UserName"].ToString();
            string password = currentContext.Request["Password"].ToString();
            bool isSaveUserName = false;
            bool isAutoSignIn = false;

            //调用登录//登录验证
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
                //currentContext.Response.Redirect(redirectUrl);
            }
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

        private string Number(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}