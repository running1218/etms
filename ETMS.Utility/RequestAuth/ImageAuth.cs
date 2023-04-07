namespace ETMS.Utility.RequestAuth
{
    /// <summary>
    /// 图片请求验证
    /// </summary>
    public class ImageAuth : System.Web.IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            bool requestAllowed = AuthUtility.Authenticate(context);

            if (requestAllowed)//本地引用，返回给客户端正确的图片
            {
                context.Response.Expires = 0;
                context.Response.Clear();
                context.Response.ContentType = FileUtility.GetExtension(context.Request.Path); //获得文件类型
                context.Response.WriteFile(context.Request.PhysicalPath);//返回给客户端正确的图片
                context.Response.End();
            }
            else //盗链引用，返回给客户端提示错误的图片
            {
                context.Response.Expires = 0;
                context.Response.Clear();
                context.Response.ContentType = "jpg";//FileUtility.GetExtension("~/RequestDisallowed.jpg"); //获得文件类型
                context.Response.WriteFile("~/RequestDisallowed.jpg");//返回给客户端提示错误的图片
                context.Response.End();
            }

        }
        
    }
}
