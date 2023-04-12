
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
    /// 在Web应用中，哪些目录需要认证的配置节
    /// </summary>
    public sealed class AuthenticateDirSettings : ConfigurationSection
    {
        private AuthenticateDirElementCollection authenticateDirs = null;
        private AnonymousDirElementCollection anonymousDirs = null;
        private object syncRoot = new object();
        /// <summary>
        /// 获取配置认证目录信息
        /// </summary>
        /// <returns>认证目录配置</returns>
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
        /// 缺省是否是需要验证的
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
        /// 需要认证的目录定义
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
        /// 匿名访问的目录定义
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
        /// 缺省需要认证的后缀名
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
        /// 页面是否需要认证
        /// </summary>
        /// <param name="appRelativePath">应用路径下的相对路径</param>
        /// <returns>是否需要认证</returns>
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
        /// 页面是否需要认证
        /// </summary>
        /// <param name="appRelativePath">应用路径下的相对路径</param>
        /// <returns>是否需要认证</returns>
        public bool PageRolesNeedAuthenticate(string Roles)
        {
            bool result = false;

            result = this.AuthenticateDirs.GetRolesMatchedElement<AuthenticateDirElement>(Roles) != null;

            return result;
        }

        /// <summary>
        /// 当前页面是否需要认证
        /// </summary>
        /// <returns>当前页面是否需要认证</returns>
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
    /// 需要认证或匿名访问目录的配置项集合
    /// </summary>
    public abstract class AuthenticateDirElementCollectionBase : ConfigurationElementCollection
    {

        /// <summary>
        /// 使用当前的Web Request的路径进行匹配的结果
        /// </summary>
        /// <typeparam name="T">期望的类型。</typeparam>
        /// <returns>匹配结果。</returns>
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
        /// 路径匹配结果
        /// </summary>
        /// <typeparam name="T">期望的类型。</typeparam>
        /// <param name="url">应用路径下的相对路径</param>
        /// <returns>匹配结果。</returns>
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
        /// 使用当前的Web Request的路径进行匹配的结果
        /// </summary>
        /// <typeparam name="T">期望的类型。</typeparam>
        /// <returns>匹配结果。</returns>
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
        /// 路径匹配结果
        /// </summary>
        /// <typeparam name="T">期望的类型。</typeparam>
        /// <param name="url">应用路径下的相对路径</param>
        /// <returns>匹配结果。</returns>
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
    /// 需要认证目录的配置项集合
    /// </summary>
    public class AuthenticateDirElementCollection : AuthenticateDirElementCollectionBase
    {
        internal AuthenticateDirElementCollection()
        {
        }
        /// <summary>
        /// 获取配置节点的键值。
        /// </summary>
        /// <param name="element">配置节点</param>
        /// <returns>配置节点的键值。</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthenticateDirElement)element).Location;
        }
        /// <summary>
        /// 创建新的配置节点。
        /// </summary>
        /// <returns>新的配置节点。</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthenticateDirElement();
        }
    }

    /// <summary>
    /// 需要匿名访问目录的配置项集合
    /// </summary>
    public class AnonymousDirElementCollection : AuthenticateDirElementCollectionBase
    {
        internal AnonymousDirElementCollection()
        {
        }
        /// <summary>
        /// 获取配置节点的键值。
        /// </summary>
        /// <param name="element">配置节点</param>
        /// <returns>配置节点的键值。</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AnonymousDirElement)element).Location;
        }
        /// <summary>
        /// 创建新的配置节点。
        /// </summary>
        /// <returns>新的配置节点。</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AnonymousDirElement();
        }
    }

    /// <summary>
    /// 认证或者匿名访问的目录的配置项基类
    /// </summary>
    public abstract class AuthenticateDirElementBase : ConfigurationElement
    {
        //private object _syncRoot = new object();
        /// <summary>
        /// 路径。
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
        /// 角色
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
        /// 判断某个路径是否能匹配上BaseDirInfo中的带通配符的路径
        /// </summary>
        /// <param name="path">需要匹配的路径</param>
        /// <returns>是否匹配</returns>
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
    /// 每一个需要匿名访问的目录的配置项
    /// </summary>
    public class AnonymousDirElement : AuthenticateDirElementBase
    {
        internal AnonymousDirElement()
        {
        }
    }

    /// <summary>
    /// 每一个需要认证的目录的配置项
    /// </summary>
    public class AuthenticateDirElement : AuthenticateDirElementBase
    {
        internal AuthenticateDirElement()
        {
        }

        /// <summary>
        /// 该页面的认证页面是否是它自己
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
