using ETMS.AppContext;
using System;

namespace ETMS.Components.NoteQuestion.API.Entity
{
    [Serializable]
    public partial class UserNotes : AbstractObject
    {
        public UserNotes() { }

        #region Properties

        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid NotesID { get; set; }

        /// <summary>
        /// 项目课程关系ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ContentID { get; set; }
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 内容
        /// </summary>
        public string NoteContent { get; set; }

        /// <summary>
        /// 是否分享，0否，1是
        /// </summary>
        public Int16 IsPublic { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }



        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return "NotesID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.NotesID;
            }
            set
            {
                this.NotesID = (Guid)value;
            }
        }

        #endregion override
    }
}