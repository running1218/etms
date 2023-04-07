namespace ETMS.Utility.RequestAuth
{
    /// <summary>
    /// ͼƬ������֤
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

            if (requestAllowed)//�������ã����ظ��ͻ�����ȷ��ͼƬ
            {
                context.Response.Expires = 0;
                context.Response.Clear();
                context.Response.ContentType = FileUtility.GetExtension(context.Request.Path); //����ļ�����
                context.Response.WriteFile(context.Request.PhysicalPath);//���ظ��ͻ�����ȷ��ͼƬ
                context.Response.End();
            }
            else //�������ã����ظ��ͻ�����ʾ�����ͼƬ
            {
                context.Response.Expires = 0;
                context.Response.Clear();
                context.Response.ContentType = "jpg";//FileUtility.GetExtension("~/RequestDisallowed.jpg"); //����ļ�����
                context.Response.WriteFile("~/RequestDisallowed.jpg");//���ظ��ͻ�����ʾ�����ͼƬ
                context.Response.End();
            }

        }
        
    }
}
