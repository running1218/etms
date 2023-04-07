using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Globalization;
using System.Web;
namespace ETMS.AppContext
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    [Serializable]
    public class UserContext : Dictionary<string, object>
    {
        /// <summary>
        /// 上下文Key
        /// </summary>
        public const string ContextKey = "ETMS.AppContext";
        /// <summary>
        /// ContextHeaderLocalName
        /// </summary>
        public const string ContextHeaderLocalName = "UserContext";
        /// <summary>
        /// ContextHeaderNamespace
        /// </summary>
        public const string ContextHeaderNamespace = "http://ETMS.AppContext/";
        /// <summary>
        /// 当前用户上下文
        /// </summary>
        public static UserContext Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items[ContextKey] == null)
                    {
                        HttpContext.Current.Items[ContextKey] = new UserContext();
                    }
                    return HttpContext.Current.Items[ContextKey] as UserContext;
                }
                else
                {
                    if (CallContext.GetData(ContextKey) == null)
                    {
                        CallContext.SetData(ContextKey, new UserContext());
                    }
                    return CallContext.GetData(ContextKey) as UserContext;
                }
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[ContextKey] = value; ;
                }
                else
                {
                    CallContext.SetData(ContextKey, value);
                }
            }
        }

        /// <summary>
        /// 当前用户是否认证登录
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return !Current.UserID.Equals(0);
            }
        }

        #region 用户身份标识相关
        /// <summary>
        /// 用户ID
        /// </summary>       
        public int UserID
        {
            get
            {
                if (!this.ContainsKey("__UserID"))
                {
                    return 0;
                }
                return (int)this["__UserID"];
            }
            set
            {
                this["__UserID"] = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                if (!this.ContainsKey("__UserName"))
                {
                    return string.Empty;
                }
                return (string)this["__UserName"];
            }
            set
            {
                this["__UserName"] = value;
            }
        }

        #endregion

        #region 用户语言环境
        /// <summary>
        /// 语言文化
        /// </summary>    
        public CultureInfo Culture
        {
            get
            {
                return CultureInfo.GetCultureInfo((string)this["__CultureName"]);
            }

        }
        /// <summary>
        /// 语言文化名称
        /// </summary>
        public string CultureName
        {
            get
            {
                if (!this.ContainsKey("__CultureName"))
                {
                    return string.Empty;
                }
                return (string)this["__CultureName"];
            }
            set
            {
                this["__CultureName"] = value;
            }
        }
        #endregion

        #region 用户当前所处的应用,由应用上下文提供！
        /// <summary>
        /// 用户当前所处的应用标识（代表应用类型）
        /// </summary>       
        public string AppCode
        {
            get
            {
                if (!this.ContainsKey("__AppCode"))
                {
                    return string.Empty;
                }
                return (string)this["__AppCode"];
            }
            set
            {
                this["__AppCode"] = value;
            }
        }
        #endregion

        #region 用户当前所处机构
        /// <summary>
        /// 当前用户所处的机构ID
        /// </summary>       
        public int OrganizationID
        {
            get
            {
                if (!this.ContainsKey("__OrganizationID"))
                {
                    return 0;
                }
                return (int)this["__OrganizationID"];
            }
            set
            {
                this["__OrganizationID"] = value;
            }
        }

        /// <summary>
        /// 机构应用ID
        /// </summary>
        public string AppID
        {
            get
            {
                return this.AppCode;
            }
        }
        #endregion

        #region 用户其他信息
        /// <summary>
        /// 访问者IP
        /// </summary>       
        public string IP
        {
            get
            {
                if (!this.ContainsKey("__IP"))
                {
                    return string.Empty;
                }
                return (string)this["__IP"];
            }
            set
            {
                this["__IP"] = value;
            }
        }
        /// <summary>
        /// 请求URL
        /// </summary>       
        public string RequestUrl
        {
            get
            {
                if (!this.ContainsKey("__RequestUrl"))
                {
                    return string.Empty;
                }
                return (string)this["__RequestUrl"];
            }
            set
            {
                this["__RequestUrl"] = value;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string RealName
        {
            get
            {
                if (!this.ContainsKey("__RealName"))
                {
                    return string.Empty;
                }
                return (string)this["__RealName"];
            }
            set
            {
                this["__RealName"] = value;
            }
        }

        public string AppAsignID
        {
            get
            {
                if (!this.ContainsKey("__AppAsignID"))
                {
                    return string.Empty;
                }
                return (string)this["__AppAsignID"];
            }
            set
            {
                this["__AppAsignID"] = value;
            }
        }

        #endregion
    }
}
