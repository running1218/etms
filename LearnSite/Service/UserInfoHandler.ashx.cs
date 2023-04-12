using University.Mooc.AppContext;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using System;
using System.Web;
namespace ETMS.Studying.Service
{
    /// <summary>
    /// Summary description for UserInfoHandler
    /// </summary>
    public class UserInfoHandler : IHttpHandler
    {

        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = context.Request["Method"];
            switch (method.ToLower())
            {
                case "updateinfo":
                    ReturnResponseContent(UpdateInfo());
                    break;
                case "updatepwd":
                    ReturnResponseContent(UpdatePwd());
                    break;
                case "updateicon":
                    ReturnResponseContent(UpdateIcon());
                    break;
            }
        }

        protected string UpdateInfo()
        {
            var email = currentContext.Request["Email"].ToString();
            var mobilePhone = currentContext.Request["MobilePhone"].ToString();
            var uid = UserContext.Current.UserID;
            try
            {
                UserLogic userLogic = new UserLogic();
                User user = userLogic.GetUserByID(uid);
                user.Email = email;
                user.MobilePhone = mobilePhone;
                userLogic.Save(user);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch (Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存失败，请与管理员联系!");
            }
        }

        protected string UpdatePwd()
        {
            var oldPwd = currentContext.Request["OldPwd"].ToString();
            var newPwd = currentContext.Request["NewPwd"].ToString();
            try
            {
                //获得用户ID 与新密码
                int uid = UserContext.Current.UserID;

                ETMS.Components.Basic.API.Entity.Security.User user = new UserLogic().GetUserByID(uid);
                if (user.PassWord.Equals(ETMS.Utility.Cryptography.MD5Utility.MD516(oldPwd)))
                {
                    //更新密码
                    new UserLogic().ResetPassword(user.UserID, newPwd);
                    return JsonHelper.GetInvokeSuccessJson();
                }
                else
                {
                    return JsonHelper.GetInvokeFailedJson(-1, "原密码错误，请重新输入!");
                }
            }
            catch (Exception ex)//
            {
                //显示错误信息
                return JsonHelper.GetInvokeFailedJson(-1, "修改失败，请与管理员联系!");
            }
        }

        protected string UpdateIcon()
        {
            var imgUrl = currentContext.Request["ImgUrl"].ToString();
            var uid = UserContext.Current.UserID;
            try
            {
                UserLogic userLogic = new UserLogic();
                User user = userLogic.GetUserByID(uid);
                user.PhotoUrl = imgUrl;
                userLogic.Save(user);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch (Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存失败，请与管理员联系!");
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
    }
}