using System;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ETMS.Components.Scrom.API.Entity
{
    /// <summary>
    /// SCO初始化
    /// </summary>
    [Serializable]
    public class CmiCoreInfo
    {
        #region Constructor
        /// <summary>
        /// 构造函数--默认
        /// </summary>
        public CmiCoreInfo()
        {
        }

        /// <summary>
        /// 构造函数--所有属性
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
        /// 学生编号
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
        /// 学生姓名
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
        /// 课程编号
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
        /// 课件编号
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
        /// 组织编号
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
        /// 章节编号
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
        /// 资源编号
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
        /// 学习模式
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
        /// 学习状态
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
        /// 错误编号
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
        /// 错误信息（错误相关模型）
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
        /// 错误信息（错误相关模型）
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

        #region 获取或设置当前用户Scorm上下文
        /// <summary>
        /// 获取Scorm Cookie
        /// </summary>
        /// <param name="key">Cookie的key值由：用户ID_资源ID组成</param>
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
                byte[] b = System.Convert.FromBase64String(result);  //将得到的字符串根据相同的编码格式分成字节数组
                MemoryStream ms = new MemoryStream(b, 0, b.Length); //从字节数组中得到内存流
                BinaryFormatter bf = new BinaryFormatter();
                CmiCoreInfo cmiCoreInfo = bf.Deserialize(ms) as CmiCoreInfo;  //反序列化得到Person类对象
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
        /// 设置Scorm Cookie
        /// </summary>
        /// <param name="key">Cookie的key值由：用户ID_资源ID组成</param>
        /// <param name="coreInfo">CmiCoreInfo</param>
        /// <returns></returns>
        public static void SetCurrentCoreInfo(string key, CmiCoreInfo coreInfo)
        {
            HttpCookie newCookie = new HttpCookie(key);
            BinaryFormatter bf = new BinaryFormatter();  //声明一个序列化类
            MemoryStream ms = new MemoryStream();  //声明一个内存流bf.Serialize(ms,person);  //执行序列化操作
            byte[] result = new byte[ms.Length];
            bf.Serialize(ms, coreInfo);  //执行序列化操作
            result = ms.ToArray();
            newCookie.Expires = DateTime.Now.AddDays(1);
            string tempCoreInfo = System.Convert.ToBase64String(result);
            newCookie.Value = tempCoreInfo;
            HttpContext.Current.Response.Cookies.Add(newCookie);
            ms.Flush();
            ms.Close();
        }


        /// <summary>
        /// 设置Scorm Cookie
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
        ///  删除Cookie
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
        /// 获取COOKIE的值
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
