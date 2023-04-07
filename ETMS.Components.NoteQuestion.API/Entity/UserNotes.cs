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
        /// ����ID
        /// </summary>
        public Guid NotesID { get; set; }

        /// <summary>
        /// ��Ŀ�γ̹�ϵID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// ��ԴID
        /// </summary>
        public Guid ContentID { get; set; }
        
        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// ����
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// ����
        /// </summary>
        public string NoteContent { get; set; }

        /// <summary>
        /// �Ƿ����0��1��
        /// </summary>
        public Int16 IsPublic { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// �޸�ʱ��
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