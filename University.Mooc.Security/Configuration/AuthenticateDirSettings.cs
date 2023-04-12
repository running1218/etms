
using System;
using System.IO;
using System.Web;
using System.Configuration;
using MCS.Library.Core;
using MCS.Library.Caching;
//using MCS.Library.Principal;

namespace University.Mooc.Security
{
    /// <summary>
    /// ��WebӦ���У���ЩĿ¼��Ҫ��֤�����ý�
    /// </summary>
    public sealed class AuthenticateDirSettings : ConfigurationSection
    {
        private AuthenticateDirElementCollection authenticateDirs = null;
        private AnonymousDirElementCollection anonymousDirs = null;
        private object syncRoot = new object();
        /// <summary>
        /// ��ȡ������֤Ŀ¼��Ϣ
        /// </summary>
        /// <returns>��֤Ŀ¼����</returns>
        /// <remarks>
        /// </remarks>
        public static AuthenticateDirSettings GetConfig()
        {
            AuthenticateDirSettings settings = (AuthenticateDirSettings)ConfigurationManager.GetSection("autumn.security/authenticateDirSettings");

            if (settings == null)
                settings = new AuthenticateDirSettings();

            return settings;
        }

        /// <summary>
        /// ȱʡ�Ƿ�����Ҫ��֤��
        /// </summary>
        [ConfigurationProperty("defaultAnonymous", DefaultValue = false)]
        public bool DefaultAnonymous
        {
            get
            {
                return (bool)this["defaultAnonymous"];
            }
        }

        /// <summary>
        /// ��Ҫ��֤��Ŀ¼����
        /// </summary>
        [ConfigurationProperty("authenticateDirs")]
        public AuthenticateDirElementCollection AuthenticateDirs
        {
            get
            {
                lock (this.syncRoot)
                {
                    if (this.authenticateDirs == null)
                    {
                        this.authenticateDirs = (AuthenticateDirElementCollection)this["authenticateDirs"];
                        if (this.authenticateDirs == null)
                            this.authenticateDirs = new AuthenticateDirElementCollection();
                    }

                    return this.authenticateDirs;
                }
            }
        }

        /// <summary>
        /// �������ʵ�Ŀ¼����
        /// </summary>
        [ConfigurationProperty("anonymousDirs")]
        public AnonymousDirElementCollection AnonymousDirs
        {
            get
            {
                lock (this.syncRoot)
                {
                    if (this.anonymousDirs == null)
                    {
                        this.anonymousDirs = (AnonymousDirElementCollection)this["anonymousDirs"];
                        if (this.anonymousDirs == null)
                            this.anonymousDirs = new AnonymousDirElementCollection();
                    }

                    return this.anonymousDirs;
                }
            }
        }

        /// <summary>
        /// ȱʡ��Ҫ��֤�ĺ�׺��
        /// </summary>
        [ConfigurationProperty("defualtAuthenticateExts", IsRequired = false, DefaultValue = ".aspx;.ashx;.asmx")]
        public string DefualtAuthenticateExts
        {
            get
            {
                return (string)this["defualtAuthenticateExts"];
            }
        }

        /// <summary>
        /// ҳ���Ƿ���Ҫ��֤
        /// </summary>
        /// <param name="appRelativePath">Ӧ��·���µ����·��</param>
        /// <returns>�Ƿ���Ҫ��֤</returns>
        public bool PageNeedAuthenticate(string appRelativePath)
        {
            bool result = false;

            if (DefaultAnonymous)
                result = this.AuthenticateDirs.GetMatchedElement<AuthenticateDirElement>(appRelativePath) != null;
            else
                result = this.AnonymousDirs.GetMatchedElement<AnonymousDirElement>(appRelativePath) == null;

            return result;
        }

        /// <summary>
        /// ҳ���Ƿ���Ҫ��֤
        /// </summary>
        /// <param name="appRelativePath">Ӧ��·���µ����·��</param>
        /// <returns>�Ƿ���Ҫ��֤</returns>
        public bool PageRolesNeedAuthenticate(string Roles)
        {
            bool result = false;

            result = this.AuthenticateDirs.GetRolesMatchedElement<AuthenticateDirElement>(Roles) != null;

            return result;
        }

        /// <summary>
        /// ��ǰҳ���Ƿ���Ҫ��֤
        /// </summary>
        /// <returns>��ǰҳ���Ƿ���Ҫ��֤</returns>
        public bool PageNeedAuthenticate()
        {
            bool result = NeedAuthenticateByExt();

            if (result)
            {
                result = this.AuthenticateDirs.GetMatchedElement<AuthenticateDirElement>() != null;
            }

            return result;
        }

        private bool NeedAuthenticateByExt()
        {
            string[] exts = this.DefualtAuthenticateExts.Split(',', ';');

            string url = HttpContext.Current.Request.Url.GetComponents(
                            UriComponents.SchemeAndServer | UriComponents.Path,
                            UriFormat.SafeUnescaped);

            string ext = Path.GetExtension(url);

            bool result = true;

            if (string.IsNullOrEmpty(ext) == false)
            {
                string matched = Array.Find(exts, delegate(string data) { return string.Compare(data, ext, true) == 0; });
                result = (matched != null);
            }

            return result;
        }
    }

    /// <summary>
    /// ��Ҫ��֤����������Ŀ¼���������
    /// </summary>
    public abstract class AuthenticateDirElementCollectionBase : ConfigurationElementCollection
    {

        /// <summary>
        /// ʹ�õ�ǰ��Web Request��·������ƥ��Ľ��
        /// </summary>
        /// <typeparam name="T">���������͡�</typeparam>
        /// <returns>ƥ������</returns>
        public T GetRolesMatchedElement<T>(string Roles) where T : AuthenticateDirElementBase
        {
            Common.CheckHttpContext();

            HttpRequest request = HttpContext.Current.Request;

            string url = request.Url.GetComponents(
                UriComponents.SchemeAndServer | UriComponents.Path,
                UriFormat.SafeUnescaped);

            return GetRolesMatchedElement<T>(url, Roles);
        }

        /// <summary>
        /// ·��ƥ����
        /// </summary>
        /// <typeparam name="T">���������͡�</typeparam>
        /// <param name="url">Ӧ��·���µ����·��</param>
        /// <returns>ƥ������</returns>
        public T GetRolesMatchedElement<T>(string url, string Roles) where T : AuthenticateDirElementBase
        {
            T result = null;            

            for (int i = 0; i < this.Count; i++)
            {
                T item = (T)BaseGet(i);
                string strTPath = item.Location;
                bool bResult = false;
                bool bRoles = false;

                if (string.IsNullOrEmpty(item.roles))
                {
                    bRoles = true;
                }
                else
                {
                    string[] arr = item.roles.Split(',');
                    string[] rolesArr = Roles.Split(',');

                    for (int j = 0; j < rolesArr.Length; j++)
                    {
                        if (Array.IndexOf(arr, rolesArr[j]) >= 0)
                        {
                            bRoles = true;
                            break;
                        }
                    }
                }

                if (strTPath.IndexOf("*") >= 0)
                    bResult = item.IsWildcharMatched(url);
                else
                    if (url.IndexOf(strTPath, StringComparison.OrdinalIgnoreCase) == 0)
                        bResult = true;

                if (bResult && bRoles)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// ʹ�õ�ǰ��Web Request��·������ƥ��Ľ��
        /// </summary>
        /// <typeparam name="T">���������͡�</typeparam>
        /// <returns>ƥ������</returns>
        public T GetMatchedElement<T>() where T : AuthenticateDirElementBase
        {
            Common.CheckHttpContext();

            HttpRequest request = HttpContext.Current.Request;

            string url = request.Url.GetComponents(
                UriComponents.SchemeAndServer | UriComponents.Path,
                UriFormat.SafeUnescaped);

            return GetMatchedElement<T>(url);
        }

        /// <summary>
        /// ·��ƥ����
        /// </summary>
        /// <typeparam name="T">���������͡�</typeparam>
        /// <param name="url">Ӧ��·���µ����·��</param>
        /// <returns>ƥ������</returns>
        public T GetMatchedElement<T>(string url) where T : AuthenticateDirElementBase
        {
            T result = null;

            bool bResult = false;

            for (int i = 0; i < this.Count; i++)
            {
                T item = (T)BaseGet(i);
                string strTPath = item.Location;

                if (strTPath.IndexOf("*") >= 0)
                    bResult = item.IsWildcharMatched(url);
                else
                    if (url.IndexOf(strTPath, StringComparison.OrdinalIgnoreCase) == 0)
                        bResult = true;

                if (bResult)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }
    }

    /// <summary>
    /// ��Ҫ��֤Ŀ¼���������
    /// </summary>
    public class AuthenticateDirElementCollection : AuthenticateDirElementCollectionBase
    {
        internal AuthenticateDirElementCollection()
        {
        }
        /// <summary>
        /// ��ȡ���ýڵ�ļ�ֵ��
        /// </summary>
        /// <param name="element">���ýڵ�</param>
        /// <returns>���ýڵ�ļ�ֵ��</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthenticateDirElement)element).Location;
        }
        /// <summary>
        /// �����µ����ýڵ㡣
        /// </summary>
        /// <returns>�µ����ýڵ㡣</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthenticateDirElement();
        }
    }

    /// <summary>
    /// ��Ҫ��������Ŀ¼���������
    /// </summary>
    public class AnonymousDirElementCollection : AuthenticateDirElementCollectionBase
    {
        internal AnonymousDirElementCollection()
        {
        }
        /// <summary>
        /// ��ȡ���ýڵ�ļ�ֵ��
        /// </summary>
        /// <param name="element">���ýڵ�</param>
        /// <returns>���ýڵ�ļ�ֵ��</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AnonymousDirElement)element).Location;
        }
        /// <summary>
        /// �����µ����ýڵ㡣
        /// </summary>
        /// <returns>�µ����ýڵ㡣</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AnonymousDirElement();
        }
    }

    /// <summary>
    /// ��֤�����������ʵ�Ŀ¼�����������
    /// </summary>
    public abstract class AuthenticateDirElementBase : ConfigurationElement
    {
        //private object _syncRoot = new object();
        /// <summary>
        /// ·����
        /// </summary>
        [ConfigurationProperty("location", IsRequired = true, IsKey = true)]
        public string Location
        {
            get
            {
                string srcLocation = (string)this["location"];
                string location;

                if (LocationContextCache.Instance.TryGetValue(srcLocation, out location) == false)
                {
                    location = NormalizePath(srcLocation);
                    LocationContextCache.Instance.Add(srcLocation, location);
                }

                return location;
            }
        }

        /// <summary>
        /// ��ɫ
        /// </summary>
        [ConfigurationProperty("roles", DefaultValue = "")]
        public string roles
        {
            get
            {
                return (string)this["roles"];
            }
        }

        /// <summary>
        /// �ж�ĳ��·���Ƿ���ƥ����BaseDirInfo�еĴ�ͨ�����·��
        /// </summary>
        /// <param name="path">��Ҫƥ���·��</param>
        /// <returns>�Ƿ�ƥ��</returns>
        public bool IsWildcharMatched(string path)
        {
            string strTemplate = Location;

            string srcFileName = Path.GetFileNameWithoutExtension(path);
            string srcFileExt = Path.GetExtension(path).Trim('.', ' ');
            string srcDir = Path.GetDirectoryName(path);

            string tempFileName = Path.GetFileNameWithoutExtension(strTemplate);
            string tempFileExt = Path.GetExtension(strTemplate).Trim('.', ' ');
            string tempDir = Path.GetDirectoryName(strTemplate);

            bool bResult = false;

            if (srcDir.IndexOf(tempDir, StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (CompareStringWithWildchar(srcFileName, tempFileName) &&
                    CompareStringWithWildchar(srcFileExt, tempFileExt))
                    bResult = true;
            }

            return bResult;
        }

        private bool CompareStringWithWildchar(string src, string template)
        {
            bool result = false;

            if (src == "*" || template == "*")
                result = true;
            else
                result = (src == template);

            return result;
        }

        private string NormalizePath(string path)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(path))
                result = "/";
            else
                result = ResolveUri(path.Trim());

            return result;
        }

        private string ResolveUri(string uriString)
        {
            Uri url = new Uri(uriString, UriKind.RelativeOrAbsolute);

            if (url.IsAbsoluteUri == false && string.IsNullOrEmpty(uriString) == false)
            {
                if (EnvironmentHelper.Mode == InstanceMode.Web)
                {
                    HttpRequest request = HttpContext.Current.Request;
                    string appPathAndQuery = string.Empty;

                    if (uriString[0] == '~')
                        appPathAndQuery = request.ApplicationPath + uriString.Substring(1);
                    else
                        if (uriString[0] != '/')
                            appPathAndQuery = request.ApplicationPath + "/" + uriString;
                        else
                            appPathAndQuery = uriString;

                    appPathAndQuery = appPathAndQuery.Replace("//", "/");

                    uriString = request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) +
                                appPathAndQuery;
                }
            }

            return uriString;
        }
    }

    /// <summary>
    /// ÿһ����Ҫ�������ʵ�Ŀ¼��������
    /// </summary>
    public class AnonymousDirElement : AuthenticateDirElementBase
    {
        internal AnonymousDirElement()
        {
        }
    }

    /// <summary>
    /// ÿһ����Ҫ��֤��Ŀ¼��������
    /// </summary>
    public class AuthenticateDirElement : AuthenticateDirElementBase
    {
        internal AuthenticateDirElement()
        {
        }

        /// <summary>
        /// ��ҳ�����֤ҳ���Ƿ������Լ�
        /// </summary>
        [ConfigurationProperty("selfAuthenticated", DefaultValue = false)]
        public bool SelfAuthenticated
        {
            get
            {
                return (bool)this["selfAuthenticated"];
            }
        }
    }

    internal class LocationContextCache : ContextCacheQueueBase<string, string>
    {
        public static LocationContextCache Instance
        {
            get
            {
                return ContextCacheManager.GetInstance<LocationContextCache>();
            }
        }

        private LocationContextCache()
        {
        }
    }
}
