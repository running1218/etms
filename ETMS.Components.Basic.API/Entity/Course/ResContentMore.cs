using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    [Serializable]
    public partial class ResContentMore : AbstractObject
    {
        public ResContentMore() { }

        #region Properties

        /// <summary>
        /// 
        /// <summary>
        public Guid ContentID { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public Guid CatalogID { get; set; }

        /// <summary>
        /// 课件ID
        /// <summary>
        public Guid? CoursewareID { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public String Name { get; set; }

        /// <summary>
        /// 资源类型，1：视频；2: Office(ppt,pdf,word,excel); 3:练习;4:Text（文字或者富文本）;5：URL
        /// <summary>
        public Int32 Type { get; set; }

        /// <summary>
        /// 根据资源类型存储不同实体
        /// <summary>
        public String DataInfo { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 状态，0：停用；1：启用。
        /// <summary>
        public Int32 Status { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public String TeacherName { get; set; }

        /// <summary>
        /// 视频时长（秒）
        /// <summary>
        //public Int32? VideoTime { get; set; }
        /// <summary>
        /// 资源长度
        /// <summary>
        public Int32? PlayTime { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public DateTime ModifyTime { get; set; }

        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return "ContentID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.ContentID;
            }
            set
            {
                this.ContentID = (Guid)value;
            }
        }

        #endregion override
    }
}
