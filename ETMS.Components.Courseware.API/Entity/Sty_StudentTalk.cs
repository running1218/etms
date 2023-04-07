using System;

namespace ETMS.Components.Courseware.API.Entity
{
    public class Sty_StudentTalk
    {
        #region Model
        private Guid _talkid;
        private int? _userid;
        private string _realName;

        public string RealName
        {
            get { return _realName; }
            set { _realName = value; }
        }


        private string _talkcontent;
        private Guid _resourceid;
        private Guid _coursewareid;
        private Guid _itemcourseresid;
        private DateTime? _createtime;
        /// <summary>
        /// 
        /// </summary>
        public Guid TalkID
        {
            set { _talkid = value; }
            get { return _talkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TalkContent
        {
            set { _talkcontent = value; }
            get { return _talkcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ResourceID
        {
            set { _resourceid = value; }
            get { return _resourceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid CourseWareID
        {
            set { _coursewareid = value; }
            get { return _coursewareid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ItemCourseResID
        {
            set { _itemcourseresid = value; }
            get { return _itemcourseresid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model
    }
}
