using ETMS.AppContext;
using System;

namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    [Serializable]
    public class Sty_UserStudyProgressDetails : AbstractObject
    {
        public Guid UserStudyProgressDetailsID
        {
            get;
            set;

        }
        public Guid TrainingItemCourseID
        {
            get;
            set;

        }
        
        public Guid ChapterResourceID
        {
            get;
            set;

        }
        public Int32 UserID
        {
            get;
            set;

        }
        public DateTime? StartTime
        {
            get;
            set;

        }
        public DateTime? EndTime
        {
            get;
            set;

        }
        public int? StudyProgress
        {
            get;
            set;

        }
        public decimal? StudyTime
        {
            get;
            set;

        }

        #region Override

        public override string DefaultKeyName
        {
            get { return "UserStudyProgressDetailsID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.UserStudyProgressDetailsID;
            }
            set
            {
                this.UserStudyProgressDetailsID = (Guid)value;
            }
        }

        #endregion override
    }
}
