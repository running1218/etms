
using System.Web;

namespace ETMS.Utility
{
    public static class UserHelper
    {
        /// <summary>
        /// ��ȡ�û���ʶ
        /// ����Forms��֤��Windows��֤
        /// </summary>
        /// <returns>�û���ʶ</returns>
        public static string GetUserIdentity()
        {
            //���ص�ǰ��¼�߱�ʶ
            //return ETMS.AppContext.UserContext.Current.UserName;
            if (ETMS.AppContext.UserContext.Current != null)
                return ETMS.AppContext.UserContext.Current.UserName;
            else
                return string.Empty;
        }

        /// <summary>
        /// ��ȡ�û�IP��ַ
        /// </summary>
        /// <returns>�û�IP��ַ</returns>
        public static string GetUserIp()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����������
        /// </summary>
        /// <returns></returns>
        public static string GetServerName()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MachineName;
            }
            else
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// ��ȡ�û�����ҳ���ַ
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                return HttpContext.Current.Request.RawUrl;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// ��ȡ�û���������ID
        /// </summary>
        /// <returns></returns>
        public static int GetUserInOrganizationID()
        {
            return ETMS.AppContext.UserContext.Current.OrganizationID;
        }
    }
}
