
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
    /// �û���¼��֤��Ϣ
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
		
		//add by yuanyong 20090416����һ����չ�������ݣ�����Ӧ�ô洢��Ӧ������
		private readonly Dictionary<string, object> properties = new Dictionary<string, object>();

		/// <summary>
		/// ��Cookie�ж�ȡ��֤��Ϣ
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
					//���cookie�ĸ�ʽ���󣬲������
				}
			}

			return signInInfo;
		}
		/// <summary>
		/// ������
		/// </summary>
		public SignInInfo()
		{
		}
		/// <summary>
		/// ������
		/// </summary>
		/// <param name="strXml">SignInInfo��Xml��Ϣ</param>
		/// <remarks>
		/// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="SignInInfoTest" lang="cs" title="SignInInfo�����Xml������ת��" />
		/// </remarks>
		public SignInInfo(string strXml)
		{
			InitFromXml(strXml);
		}

		#region ISignInInfo ��Ա
		/// <summary>
		/// �û�ID
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
        /// �û���
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
        /// �û�����
        /// </summary>
        public string RealName
        {
            get { return this.realName; }
            set { this.realName = value; }
        }

        /// <summary>
        /// �ǳ�
        /// </summary>
        public string NickName
        {
            get { return this.nickName; }
            set { this.nickName = value; }
        }
		/// <summary>
		/// ����ǰ���û�ID
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
		/// ����
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
		/// �Ƿ�Windows����
		/// </summary>
		public bool WindowsIntegrated
		{
			get
			{
				return this.windowsIntegrated;
			}
		}

		/// <summary>
		/// ��¼��SessionID
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
		/// ��¼ʱ��
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
		/// ע��ʱ��
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
		/// ��֤������
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
		/// �Ƿ�����ʱ
		/// </summary>
		public bool ExistsSignInTimeout
		{
			get
			{
				return this.signInTimeout != DateTime.MaxValue && this.signInTimeout != DateTime.MinValue;
			}
		}

        /// <summary>
        /// �û����ͣ���0��ѧԱ; 1��ϵͳ����Ա; 2���������Ա; 3����ʦ��
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
		/// ������Cookie��
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
		/// �洢��xml�ṹ������
		/// </summary>
		/// <returns>xml�ṹ��SignInInfo</returns>
		/// <remarks>
		/// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="SignInInfoTest" lang="cs" title="SignInInfo�����Xml������ת��" />
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
		/// SignInInfo�Ƿ�Ϸ�
		/// </summary>
		/// <returns>true ���� false</returns>
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
		/// ��չ����
		/// </summary>
		/// <remarks>
		/// �洢Ӧ������Ҫ��չ����Ӧ������������
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
			return (bExpired);		//����ʱ���Ƿ����
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
				bExpired = (DateTime.Now >= dtTO);		//���ʱ�����
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
