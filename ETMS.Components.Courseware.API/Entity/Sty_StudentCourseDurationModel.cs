using System;

namespace ETMS.Components.Courseware.API.Entity
{
    [Serializable]
    public partial class Sty_StudentCourseDurationModel
    {
        public Sty_StudentCourseDurationModel()
        { }
        #region Model
        private Guid _studentcoursedurationid;
        private Guid _resourceid;
        private Guid _itemcourseresid;
        private int? _userid;
        private DateTime? _createtime;
        private int? _courserestypeid;
        private Guid? _courseID;
        private int? _numCount;

        public int? NumCount
        {
            get { return _numCount; }
            set { _numCount = value; }
        }
        private int? _countTime;

        public int? CountTime
        {
            get { return _countTime; }
            set { _countTime = value; }
        }

        public Guid? CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid StudentCourseDurationID
        {
            set { _studentcoursedurationid = value; }
            get { return _studentcoursedurationid; }
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
        public Guid ItemCourseResID
        {
            set { _itemcourseresid = value; }
            get { return _itemcourseresid; }
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
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CourseResTypeID
        {
            set { _courserestypeid = value; }
            get { return _courserestypeid; }
        }
        #endregion Model

    }
}
