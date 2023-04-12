
using System;
using System.Xml;
using System.Collections.Generic;

//using MCS.Library.Data.Mapping;

namespace ETMS.Security
{
    /// <summary>
    /// ��¼��Ϣ�Ľӿ�
    /// </summary>
    public interface ISignInInfo
    {
        /// <summary>
        /// ��¼�û���ID
        /// </summary>
        ////[ORFieldMapping("USER_ID")]
        string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// ��¼�û���ID
        /// </summary>
        ////[ORFieldMapping("RealName")]
        string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// �û�����
        /// </summary>
        string UserName
        {
            get;
            set;
        }

		/// <summary>
		/// ����ǰ�ĵ�¼��
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
        /// �Ƿ񼯳���֤
        /// </summary>
        //[NoMapping]
        bool WindowsIntegrated
        {
            get;
        }

        /// <summary>
        /// ��¼��SessionID
        /// </summary>
        //[ORFieldMapping("SIGNIN_ID", PrimaryKey = true)]
        //[SqlBehavior(ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        string SignInSessionID
        {
            get;
        }

        /// <summary>
        /// ��¼��ʱ��
        /// </summary>
        //[ORFieldMapping("SIGNIN_TIME")]
        //[SqlBehavior(DefaultExpression = "getdate()")]
        DateTime SignInTime
        {
            get;
            set;
        }

        /// <summary>
        /// ��¼�Ĺ���ʱ��
        /// </summary>
        //[ORFieldMapping("SIGNIN_TIMEOUT")]
        DateTime SignInTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// ��֤������������(����IP)
        /// </summary>
        //[ORFieldMapping("AUTHENTICATE_SERVER")]
        string AuthenticateServer
        {
            get;
        }

        /// <summary>
        /// �Ƿ���ڵ�¼��ʱʱ�䣨�������ڵ�������Сֵ��
        /// </summary>
        //[NoMapping]
        bool ExistsSignInTimeout
        {
            get;
        }

		/// <summary>
		/// ��չ���Լ��ϣ�����⣩
		/// </summary>
		//[NoMapping]
		Dictionary<string, object> Properties
		{
			get;
		}

        /// <summary>
        /// ����¼��Ϣ���浽Cookie��
        /// </summary>
        void SaveToCookie();

        /// <summary>
        /// ����¼��Ϣ���浽Xml�ĵ�������
        /// </summary>
        XmlDocument SaveToXml();

        /// <summary>
        /// SignInInfo�Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }

    /// <summary>
    /// Ӧ�õ�¼�Ժ����ɵ�Ticket
    /// </summary>
    public interface ITicket
    {
        /// <summary>
        /// ��¼����Ϣ
        /// </summary>
        //[NoMapping]
        ISignInInfo SignInInfo
        {
            get;
        }

        /// <summary>
        /// Ӧ�õ�¼�Ժ��Ӧ��ID
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_ID", PrimaryKey = true)]
        //[SqlBehavior(ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        string AppSignInSessionID
        {
            get;
        }

        /// <summary>
        /// Ӧ�õ�ID
        /// </summary>
        //[ORFieldMapping("APP_ID")]
        string AppID
        {
            get;
        }

        /// <summary>
        /// Ӧ�õ�¼��ʱ��
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_TIME")]
        DateTime AppSignInTime
        {
            get;
            set;
        }

        /// <summary>
        /// Ӧ�õ�¼��Session����ʱ��
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_TIMEOUT")]
        DateTime AppSignInTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Ӧ�õ�¼ʱ��IP��ַ
        /// </summary>
        //[ORFieldMapping("APP_SIGNIN_IP")]
        string AppSignInIP
        {
            get;
        }

        /// <summary>
        /// ��Ӧ�õ�¼��Ϣ���浽Cookie��
        /// </summary>
        void SaveToCookie();

        /// <summary>
        /// ��Ӧ�õ�¼��Ϣ���浽Xml�ĵ�������
        /// </summary>
        XmlDocument SaveToXml();

        /// <summary>
        /// Ticket�Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        #region Ѧ�����޸ģ�Ŀ�ģ�����Ӧ�ý�ɫ�б���������
        /// <summary>
        /// Ӧ�ý�ɫ�б������ɫ��";"�ָ�
        /// </summary>
        //[NoMapping]
        string AppRoles { get; set; }
        /// <summary>
        /// Ӧ�û�������ԵĻ������룩
        /// </summary>
        //[NoMapping]
        string AppEnvironment { get; set; }

        #endregion
    }   
}
