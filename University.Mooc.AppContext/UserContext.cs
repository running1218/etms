using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Globalization;
using System.Web;
using System.Runtime.Serialization;
using System.Configuration;

namespace University.Mooc.AppContext
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    [Serializable]
    public class UserContext : Dictionary<string, object>
    {
        public UserContext() { }
        protected UserContext(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// 上下文Key
        /// </summary>
        public static string ContextKey = ConfigurationManager.AppSettings["UserCookie"];
        /// <summary>
        /// ContextHeaderLocalName
        /// </summary>
        public const string ContextHeaderLocalName = "UserContext";
        /// <summary>
        /// ContextHeaderNamespace
        /// </summary>
        public const string ContextHeaderNamespace = "http://University.Mooc.AppContext/";
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
                    HttpContext.Current.Items[ContextKey] = value;
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
                return !Current.UserID.Equals(Guid.Empty);
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
                    return default(int);
                }
                //return (Guid)this["__UserID"];
                return int.Parse(this["__UserID"].ToString());
            }
            set
            {
                this["__UserID"] = value;
            }
        }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName
        {
            get
            {
                if (!this.ContainsKey("__LoginName"))
                {
                    return string.Empty;
                }
                return (string)this["__LoginName"];
            }
            set
            {
                this["__LoginName"] = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string NickName
        {
            get
            {
                if (!this.ContainsKey("__NickName"))
                {
                    return string.Empty;
                }
                return (string)this["__NickName"];
            }
            set
            {
                this["__NickName"] = value;
            }
        }

        /// <summary>
        /// 用户真实姓名
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

        /// <summary>
        /// 用户分类
        /// </summary>
        public Int16 UserType
        {
            get
            {
                if (!this.ContainsKey("__UserType"))
                {
                    return -1;
                }
                return (Int16)this["__UserType"];
            }
            set
            {
                this["__UserType"] = value;
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
                    return "zh-CN";
                }
                else if (string.IsNullOrEmpty((string)this["__CultureName"]))
                {
                    return "zh-CN";
                }
                else
                {
                    return (string)this["__CultureName"];
                }
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
        public Guid OrganizationID
        {
            get
            {
                if (!this.ContainsKey("__OrganizationID"))
                {
                    return Guid.Empty;
                }
                return (Guid)this["__OrganizationID"];
            }
            set
            {
                this["__OrganizationID"] = value;
            }
        }

        /// <summary>
        /// 当前用户所有机构，以","分割
        /// </summary>       
        public int UserOrgs
        {
            get
            {
                if (!this.ContainsKey("__UserOrgs"))
                {
                    return default(int);
                }
                return (int)this["__UserOrgs"];
            }
            set
            {
                this["__UserOrgs"] = value;
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
        /// 用户角色
        /// </summary>
        public string Roles
        {
            get
            {
                if (!this.ContainsKey("__Roles"))
                {
                    return string.Empty;
                }
                return (string)this["__Roles"];
            }
            set
            {
                this["__Roles"] = value;
            }
        }

        #endregion
    }
}
