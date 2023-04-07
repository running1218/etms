using System;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ETMS.Components.Scrom.API.Entity
{
    /// <summary>
    /// SCO��ʼ��
    /// </summary>
    [Serializable]
    public class CmiCoreInfo
    {
        #region Constructor
        /// <summary>
        /// ���캯��--Ĭ��
        /// </summary>
        public CmiCoreInfo()
        {
        }

        /// <summary>
        /// ���캯��--��������
        /// </summary>
        public CmiCoreInfo(int studentID, string studentName, Guid courseID, Guid coursewareID, string orgID,
            string itemID, string resourceID, string lessonMode, string lessonStatus, string lastError, string errorInfo, DateTime startTime)
        {
            this.studentIDField = studentID;
            this.studentNameField = studentName;
            this.courseIDField = courseID;
            this.coursewareIDField = coursewareID;
            this.orgIDField = orgID;
            this.itemIDField = itemID;
            this.resourceIDField = resourceID;
            this.lessonModeField = lessonMode;
            this.lessonStatusField = lessonStatus;
            this.lastErrorField = lastError;
            this.errorInfoField = errorInfo;
            this.startTimeField = startTime;
        }

        #endregion Constructor

        #region Fields, Properties

        private Int32 studentIDField;
        /// <summary>
        /// ѧ�����
        /// </summary>
        public Int32 StudentID
        {
            get
            {
                return this.studentIDField;
            }
            set
            {
                this.studentIDField = value;
            }
        }

        private String studentNameField;
        /// <summary>
        /// ѧ������
        /// </summary>
        public String StudentName
        {
            get
            {
                return this.studentNameField;
            }
            set
            {
                this.studentNameField = value;
            }
        }

        private Guid courseIDField;
        /// <summary>
        /// �γ̱��
        /// </summary>
        public Guid CourseID
        {
            get
            {
                return this.courseIDField;
            }
            set
            {
                this.courseIDField = value;
            }
        }

        private Guid coursewareIDField;
        /// <summary>
        /// �μ����
        /// </summary>
        public Guid CoursewareID
        {
            get
            {
                return this.coursewareIDField;
            }
            set
            {
                this.coursewareIDField = value;
            }
        }

        private String orgIDField;
        /// <summary>
        /// ��֯���
        /// </summary>
        public String OrgID
        {
            get
            {
                return this.orgIDField;
            }
            set
            {
                this.orgIDField = value;
            }
        }

        private String itemIDField;
        /// <summary>
        /// �½ڱ��
        /// </summary>
        public String ItemID
        {
            get
            {
                return this.itemIDField;
            }
            set
            {
                this.itemIDField = value;
            }
        }


        private String resourceIDField;
        /// <summary>
        /// ��Դ���
        /// </summary>
        public String ResourceID
        {
            get
            {
                return this.resourceIDField;
            }
            set
            {
                this.resourceIDField = value;
            }
        }

        private String lessonModeField;
        /// <summary>
        /// ѧϰģʽ
        /// </summary>
        public String LessonMode
        {
            get
            {
                return this.lessonModeField;
            }
            set
            {
                this.lessonModeField = value;
            }
        }


        private String lessonStatusField;
        /// <summary>
        /// ѧϰ״̬
        /// </summary>
        public String LessonStatus
        {
            get
            {
                return this.lessonStatusField;
            }
            set
            {
                this.lessonStatusField = value;
            }
        }

        private String lastErrorField;
        /// <summary>
        /// ������
        /// </summary>
        public String LastError
        {
            get
            {
                return this.lastErrorField;
            }
            set
            {
                this.lastErrorField = value;
            }
        }

        private String errorInfoField;
        /// <summary>
        /// ������Ϣ���������ģ�ͣ�
        /// </summary>
        public String ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }


        private DateTime startTimeField;
        /// <summary>
        /// ������Ϣ���������ģ�ͣ�
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }
        #endregion Fields, Properties

        #region ��ȡ�����õ�ǰ�û�Scorm������
        /// <summary>
        /// ��ȡScorm Cookie
        /// </summary>
        /// <param name="key">Cookie��keyֵ�ɣ��û�ID_��ԴID���</param>
        /// <returns>CmiCoreInfo</returns>
        public static CmiCoreInfo GetCurrentCoreInfo(string key)
        {

            if (HttpContext.Current.Request.Cookies[key] == null)
            {
                return null;
            }
            else
            {
                string result = HttpContext.Current.Request.Cookies[key].Value;
                byte[] b = System.Convert.FromBase64String(result);  //���õ����ַ���������ͬ�ı����ʽ�ֳ��ֽ�����
                MemoryStream ms = new MemoryStream(b, 0, b.Length); //���ֽ������еõ��ڴ���
                BinaryFormatter bf = new BinaryFormatter();
                CmiCoreInfo cmiCoreInfo = bf.Deserialize(ms) as CmiCoreInfo;  //�����л��õ�Person�����
                return cmiCoreInfo;
            }
        }
        public static CmiCoreInfo GetCurrentCoreInfo(string userID, string resourceID, string itemCourseResID)
        {
            return GetCurrentCoreInfo(userID + "_" + itemCourseResID);
        }


        public static void SetCurrentCoreInfo(string userID, string resourceID, Guid itemCourseResID, CmiCoreInfo coreInfo)
        {
            SetCurrentCoreInfo(userID + "_" + itemCourseResID.ToString(), coreInfo);
        }
        /// <summary>
        /// ����Scorm Cookie
        /// </summary>
        /// <param name="key">Cookie��keyֵ�ɣ��û�ID_��ԴID���</param>
        /// <param name="coreInfo">CmiCoreInfo</param>
        /// <returns></returns>
        public static void SetCurrentCoreInfo(string key, CmiCoreInfo coreInfo)
        {
            HttpCookie newCookie = new HttpCookie(key);
            BinaryFormatter bf = new BinaryFormatter();  //����һ�����л���
            MemoryStream ms = new MemoryStream();  //����һ���ڴ���bf.Serialize(ms,person);  //ִ�����л�����
            byte[] result = new byte[ms.Length];
            bf.Serialize(ms, coreInfo);  //ִ�����л�����
            result = ms.ToArray();
            newCookie.Expires = DateTime.Now.AddDays(1);
            string tempCoreInfo = System.Convert.ToBase64String(result);
            newCookie.Value = tempCoreInfo;
            HttpContext.Current.Response.Cookies.Add(newCookie);
            ms.Flush();
            ms.Close();
        }


        /// <summary>
        /// ����Scorm Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="coreInfo">CmiCoreInfo</param>
        /// <returns></returns>
        public static void SetTimeCookie(string key, string value)
        {
            HttpCookie newCookie = new HttpCookie(key);
            newCookie.Expires = DateTime.Now.AddDays(1);
            newCookie.Value = value;
            HttpContext.Current.Response.AppendCookie(newCookie);

        }

        /// <summary>
        ///  ɾ��Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void DeleteCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                HttpContext.Current.Response.Cookies.Remove(cookieName);
            }
        }

        /// <summary>
        /// ��ȡCOOKIE��ֵ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTimeCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] == null)
            {
                return null;
            }
            else
            {
                string result = HttpContext.Current.Request.Cookies[key].Value;
                return result;
            }

        }

        #endregion
    }
}
