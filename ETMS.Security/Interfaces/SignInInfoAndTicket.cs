
using System;
using System.Xml;
using System.Collections.Generic;

//using MCS.Library.Data.Mapping;

namespace ETMS.Security
{
    /// <summary>
    /// 登录信息的接口
    /// </summary>
    public interface ISignInInfo
    {
        /// <summary>
        /// 登录用户的ID
        /// </summary>
        ////[ORFieldMapping("USER_ID")]
        string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 登录用户的ID
        /// </summary>
        ////[ORFieldMapping("RealName")]
        string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        string UserName
        {
            get;
            set;
        }

		/// <summary>
		/// 扮演前的登录名
		/// </summary>
        ////[NoMapping]
		string OriginalUserID
		{
			get;
			set;
		}

        /// <summary>
        /// 
        /// </summary>
        ////[ORFieldMapping("DOMAIN")]
        string Domain
        {
            get;
        }

        /// <summary>
        /// 是否集成认证
        /// </summary>
        //[NoMapping]
        bool WindowsIntegrated
        {
            get;
        }

        /// <summary>
        /// 登录的SessionID
        /// </summary>
        //[ORFieldMapping("SIGNIN_ID", PrimaryKey = true)]
        //[SqlBehavior(ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        string SignInSessionID
        {
            get;
        }

        /// <summary>
        /// 登录的时间
        /// </summary>
        //[ORFieldMapping("SIGNIN_TIME")]
        //[SqlBehavior(DefaultExpression = "getdate()")]
        DateTime SignInTime
        {
            get;
            set;
        }

        /// <summary>
        /// 登录的过期时间
        /// </summary>
        //[ORFieldMapping("SIGNIN_TIMEOUT")]
        DateTime SignInTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 认证服务器的域名(或者IP)
        /// </summary>
        //[ORFieldMapping("AUTHENTICATE_SERVER")]
        string AuthenticateServer
        {
            get;
        }

        /// <summary>
        /// 是否存在登录超时时间（不是日期的最大和最小值）
        /// </summary>
        //[NoMapping]
        bool ExistsSignInTimeout
        {
            get;
        }

		/// <summary>
		/// 扩展属性集合（不入库）
		/// </summary>
		//[NoMapping]
		Dictionary<string, object> Properties
		{
			get;
		}

        /// <summary>
        /// 将登录信息保存到Cookie中
        /// </summary>
        void SaveToCookie();

        /// <summary>
        /// 将登录信息保存到Xml文档对象中
        /// </summary>
        XmlDocument SaveToXml();

        /// <summary>
        /// SignInInfo是否合法
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }

    /// <summary>
    /// 应用登录以后生成的Ticket
    /// </summary>
    public interface ITicket
    {
        /// <summary>
        /// 登录的信息
        /// </summary>
        //[NoMapping]
        ISignInInfo SignInInfo
        {
            get;
        }

        /// <summary>
        /// 应用登录以后的应用ID
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_ID", PrimaryKey = true)]
        //[SqlBehavior(ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        string AppSignInSessionID
        {
            get;
        }

        /// <summary>
        /// 应用的ID
        /// </summary>
        //[ORFieldMapping("APP_ID")]
        string AppID
        {
            get;
        }

        /// <summary>
        /// 应用登录的时间
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_TIME")]
        DateTime AppSignInTime
        {
            get;
            set;
        }

        /// <summary>
        /// 应用登录的Session过期时间
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_TIMEOUT")]
        DateTime AppSignInTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 应用登录时的IP地址
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_IP")]
        string AppSignInIP
        {
            get;
        }

        /// <summary>
        /// 将应用登录信息保存到Cookie中
        /// </summary>
        void SaveToCookie();

        /// <summary>
        /// 将应用登录信息保存到Xml文档对象中
        /// </summary>
        XmlDocument SaveToXml();

        /// <summary>
        /// Ticket是否合法
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        #region 薛永波修改，目的：增加应用角色列表、机构编码
        /// <summary>
        /// 应用角色列表，多个角色用";"分割
        /// </summary>
        //[NoMapping]
        string AppRoles { get; set; }
        /// <summary>
        /// 应用环境（针对的机构编码）
        /// </summary>
        //[NoMapping]
        string AppEnvironment { get; set; }

        #endregion
    }   
}
