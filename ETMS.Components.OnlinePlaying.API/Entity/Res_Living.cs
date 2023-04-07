using System;
using ETMS.AppContext;

namespace ETMS.Components.OnlinePlaying.API.Entity
{
    [Serializable]
    public partial class Res_Living: AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "LivingID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.LivingID;
            }
            set
            {
                this.LivingID = (string)value;
            }
        }
        #endregion
        #region Fields, Properties
        public string LivingID { get; set; }
        public Guid CourseID { get; set; }
        public string LivingName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeacherID { get; set; }
        public string PartnerID { get; set; }
        public string BID { get; set; }
        public string AnchorKey { get; set; }
        public string AssistantKey { get; set; }
        public string StudentKey { get; set; }
        public int OrgID { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserID { get; set; }
        public string CreateUser { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyUser { get; set; }

        public string Account { get; set; }
        public string NikeName { get; set; }
        #endregion
        #region extention
        /// <summary>
        /// 
        /// </summary>
        public string TeacherName { get; set; }

        public string UserName { get; set; }
        public string CourseName { get; set; }

        public int Flag
        {
            get
            {
                if (this.StartTime >= DateTime.Now)
                {
                    return 1;
                }
                else if (this.EndTime >= DateTime.Now)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Date
        {
            get
            {
                return StartTime.ToString("yyyy-MM-dd");
            }
        }

        public string SHHMM
        {
            get {
                return this.StartTime.ToString("HH:mm");
            }
        }
        public string EHHMM
        {
            get {
                return this.EndTime.ToString("HH:mm");
            }
        }

        public string ThumbnailURL {
            get; set;
        }
        /// <summary>
        /// 直播类型 1:1对1， 2:1对6， 3:1对多
        /// </summary>
        public int LivingType
        {
            get;set;
        }
        /// <summary>
        /// 是否开放
        /// </summary>
        public int IsOpen { get; set; }
        /// <summary>
        /// 学员进入直播状态，默认0：未进入；1：进入
        /// </summary>
        public int StyStatus { get; set; }

        public int Type { get; set; }
        #endregion
    }
}
