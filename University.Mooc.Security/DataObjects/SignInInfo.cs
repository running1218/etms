
using System;
using System.Web;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using MCS.Library.Core;
using University.Mooc.Security.Properties;

namespace University.Mooc.Security
{
    /// <summary>
    /// 用户登录认证信息
    /// </summary>
    public class SignInInfo : ISignInInfo
	{
        private int userID = default(int);
        private string userName = string.Empty;
        private string realName = string.Empty;
        private string nickName = string.Empty;
        private int originalUserID = default(int);
		private string domain = string.Empty;
		private Guid signInSessionID = Guid.Empty;
		private string authenticateServer = string.Empty;
		private bool windowsIntegrated;
		private DateTime signInTime;
		private DateTime signInTimeout = DateTime.MinValue;
        private Int16 userType = -1;
        private string userOrgs = string.Empty;
		
		//add by yuanyong 20090416增加一个扩展属性内容，用于应用存储相应的数据
		private readonly Dictionary<string, object> properties = new Dictionary<string, object>();

		/// <summary>
		/// 从Cookie中读取认证信息
		/// </summary>
		/// <returns></returns>
		public static ISignInInfo LoadFromCookie()
		{
			SignInInfo signInInfo = null;

			Common.CheckHttpContext();

			HttpRequest request = HttpContext.Current.Request;

			HttpCookie cookie = request.Cookies[Common.C_SIGNIN_COOKIE_KEY];

			if (cookie != null)
			{
				string strSignIn = cookie.Value;

				try
				{
					signInInfo = new SignInInfo(Common.DecryptString(strSignIn));
				}
				catch (System.Exception)
				{
					//如果cookie的格式错误，不予理睬
				}
			}

			return signInInfo;
		}
		/// <summary>
		/// 构造类
		/// </summary>
		public SignInInfo()
		{
		}
		/// <summary>
		/// 构造类
		/// </summary>
		/// <param name="strXml">SignInInfo的Xml信息</param>
		/// <remarks>
		/// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="SignInInfoTest" lang="cs" title="SignInInfo对象和Xml对象间的转换" />
		/// </remarks>
		public SignInInfo(string strXml)
		{
			InitFromXml(strXml);
		}

		#region ISignInInfo 成员
		/// <summary>
		/// 用户ID
		/// </summary>
        public int UserID
		{
			get
			{
				return this.userID;
			}
			set
			{
				this.userID = value;
			}
		}
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string RealName
        {
            get { return this.realName; }
            set { this.realName = value; }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return this.nickName; }
            set { this.nickName = value; }
        }
		/// <summary>
		/// 扮演前的用户ID
		/// </summary>
        public int OriginalUserID
		{
			get
			{
				int result = this.userID;

				if (this.originalUserID == default(int))
					result = this.originalUserID;

				return result;
			}
			set
			{
				this.originalUserID = value;
			}
		}

		/// <summary>
		/// 域名
		/// </summary>
		public string Domain
		{
			get
			{
				return this.domain;
			}
            set
            {
                this.domain = value;
            }
		}

		/// <summary>
		/// 是否Windows集成
		/// </summary>
		public bool WindowsIntegrated
		{
			get
			{
				return this.windowsIntegrated;
			}
		}

		/// <summary>
		/// 登录的SessionID
		/// </summary>
		public Guid SignInSessionID
		{
			get
			{
				return this.signInSessionID;
            }
            set
            {
                this.signInSessionID = value;
            }
		}

		/// <summary>
		/// 登录时间
		/// </summary>
		public DateTime SignInTime
		{
			get
			{
				return this.signInTime;
			}
			set
			{
				this.signInTime = value;
			}
		}

		/// <summary>
		/// 注销时间
		/// </summary>
		public DateTime SignInTimeout
		{
			get
			{
				return this.signInTimeout;
			}
			set
			{
				this.signInTimeout = value;
			}
		}

		/// <summary>
		/// 认证服务器
		/// </summary>
		public string AuthenticateServer
		{
			get
			{
				return this.authenticateServer;
            }
            set
            {
                this.authenticateServer = value;
            }
		}
		
		/// <summary>
		/// 是否登入后超时
		/// </summary>
		public bool ExistsSignInTimeout
		{
			get
			{
				return this.signInTimeout != DateTime.MaxValue && this.signInTimeout != DateTime.MinValue;
			}
		}

        /// <summary>
        /// 用户类型：（0：学员; 1：系统管理员; 2：教务管理员; 3：教师）
        /// </summary>
        public Int16 UserType
        {
            get
            {
                return this.userType;
            }
            set
            {
                this.userType = value;
            }
        }

        public string UserOrgs
        {
            get
            {
                return this.userOrgs;
            }
            set
            {
                this.userOrgs = value;
            }
        }

		/// <summary>
		/// 保存入Cookie中
		/// </summary>
		public void SaveToCookie()
		{
			Common.CheckHttpContext();

			HttpResponse response = HttpContext.Current.Response;
			//HttpRequest request = HttpContext.Current.Request;

			XmlDocument xmlDoc = SaveToXml();
			string strData = xmlDoc.InnerXml;

			HttpCookie cookie = new HttpCookie(Common.C_SIGNIN_COOKIE_KEY);
            
			cookie.Value = Common.EncryptString(strData);
			cookie.Expires = this.signInTimeout;

			response.Cookies.Add(cookie);
		}

		/// <summary>
		/// 存储到xml结构数据中
		/// </summary>
		/// <returns>xml结构的SignInInfo</returns>
		/// <remarks>
		/// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="SignInInfoTest" lang="cs" title="SignInInfo对象和Xml对象间的转换" />
		/// </remarks>
		public System.Xml.XmlDocument SaveToXml()
		{
			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.LoadXml("<SignInInfo/>");

			XmlElement root = xmlDoc.DocumentElement;

			XmlHelper.AppendNode(root, "SSID", this.signInSessionID);
			XmlHelper.AppendNode(root, "UID", this.userID);
            XmlHelper.AppendNode(root, "UName", this.realName);
            XmlHelper.AppendNode(root, "UNM", this.userName);
            XmlHelper.AppendNode(root, "NickName", this.nickName);
			XmlHelper.AppendNode(root, "DO", this.domain);
			XmlHelper.AppendNode(root, "WI", this.windowsIntegrated);
			XmlHelper.AppendNode(root, "AS", this.authenticateServer);
			XmlHelper.AppendNode(root, "STime", this.signInTime);
			XmlHelper.AppendNode(root, "STimeout", this.signInTimeout);
			XmlHelper.AppendNode(root, "OUID", this.OriginalUserID);
            XmlHelper.AppendNode(root, "UserType", this.UserType);
            XmlHelper.AppendNode(root, "UserOrgs", this.userOrgs);

			if (this.properties.Count > 0) // Add By Yuanyong 20090416
			{
				XmlNode nodeProps = XmlHelper.AppendNode(root, Resource.SignInInfoExtraProperties);

				foreach (KeyValuePair<string, object> kp in this.properties)
				{
					XmlNode nodeProp = XmlHelper.AppendNode(nodeProps, "add");

					XmlHelper.AppendAttr(nodeProp, "key", kp.Key);
					XmlHelper.AppendAttr(nodeProp, "value", kp.Value.ToString());
				}
			}

			return xmlDoc;
		}

		/// <summary>
		/// SignInInfo是否合法
		/// </summary>
		/// <returns>true 或者 false</returns>
		public bool IsValid()
		{
			bool bValid = false;

			try
			{
				ExceptionHelper.TrueThrow(IsAbsoluteTimeExpired(), Resource.AbsoluteTimeExpired);
				ExceptionHelper.TrueThrow(IsSlidingExpired(), Resource.SlidingTimeExpired);
				ExceptionHelper.TrueThrow(IsDifferentAuthenticateServer(), Resource.DifferentAthenticateServer);

				bValid = true;
			}
			catch (System.ApplicationException ex)
			{
				Trace.WriteLine(string.Format(Resource.TicketInvalidReason, this.UserID, ex.Message));
			}

			return bValid;
		}

		/// <summary>
		/// 扩展属性
		/// </summary>
		/// <remarks>
		/// 存储应用中需要扩展的相应属性数据内容
		/// </remarks>
		public Dictionary<string, object> Properties // Add By Yuanyong 20090416
		{
			get
			{
				return this.properties;
			}
		}
		#endregion

        private void InitFromXml(string strXml)
        {
            XmlDocument xmlDoc = XmlHelper.CreateDomDocument(strXml);

            XmlElement root = xmlDoc.DocumentElement;

            this.signInSessionID =Guid.Parse(XmlHelper.GetSingleNodeText(root, "SSID"));
            this.userID =int.Parse(XmlHelper.GetSingleNodeText(root, "UID"));
            this.realName = XmlHelper.GetSingleNodeText(root, "UName");
            this.userName = XmlHelper.GetSingleNodeText(root, "UNM");
            this.domain = XmlHelper.GetSingleNodeText(root, "DO");
            this.windowsIntegrated = XmlHelper.GetSingleNodeValue(root, "WI", false);
            this.authenticateServer = XmlHelper.GetSingleNodeText(root, "AS");
            this.signInTime = XmlHelper.GetSingleNodeValue(root, "STime", DateTime.MinValue);
            this.signInTimeout = XmlHelper.GetSingleNodeValue(root, "STimeout", DateTime.MinValue);
            //this.userType = XmlHelper.GetSingleNodeText(root, "UserType").ToInt16();
            this.userOrgs = XmlHelper.GetSingleNodeText(root, "UserOrgs");
            //this.originalUserID = XmlHelper.GetSingleNodeValue(root, "OUID", this.userID);

            // Add By Yuanyong 20090416
            //XmlNode node = root.SelectSingleNode(Resource.SignInInfoExtraProperties);
            //if (node != null)
            //{
            //    foreach (XmlNode nodeProp in node.ChildNodes)
            //    {
            //        this.properties.Add(XmlHelper.GetAttributeText(nodeProp, "key"), XmlHelper.GetAttributeText(nodeProp, "value"));
            //    }
            //}
        }
		
        private bool IsAbsoluteTimeExpired()
		{
			DateTime newExpireDate = GetConfigExpireDate();

			bool bExpired = (DateTime.Now >= newExpireDate);
#if DELUXEWORKSTEST
            Debug.WriteLineIf(bExpired, "Absolute Time Expired", "SignInPage Check");
#endif
			return (bExpired);		//绝对时间是否过期
		}

		private DateTime GetConfigExpireDate()
		{
			DateTime dt = DateTime.MaxValue;

			PassportSignInSettings settings = PassportSignInSettings.GetConfig();

			if (settings.DefaultTimeout >= TimeSpan.Zero)
				dt = SignInTime.Add(settings.DefaultTimeout);

			return dt;
		}

		private bool IsSlidingExpired()
		{
			bool bExpired = false;

			PassportSignInSettings settings = PassportSignInSettings.GetConfig();

			if (settings.HasSlidingExpiration)
			{
				DateTime dtTO = this.SignInTime.Add(settings.SlidingExpiration);
				bExpired = (DateTime.Now >= dtTO);		//相对时间过期
			}
#if DELUXEWORKSTEST
            Debug.WriteLineIf(bExpired, "Sliding Expired", "SignInPage Check");
#endif
			return bExpired;
		}

		private bool IsDifferentAuthenticateServer()
		{
			HttpRequest request = HttpContext.Current.Request;

			return string.Compare(this.AuthenticateServer, request.Url.Host + ":" + request.Url.Port, true) != 0;
		}

	
	}
}
