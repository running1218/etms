using ETMS.AppContext;
using System;
using System.Collections.Generic;

namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    [Serializable]
    public partial class CourseContentStudyProgress : AbstractObject
    {
        public CourseContentStudyProgress() { }

        #region Properties

        /// <summary>
        /// 
        /// <summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// 课件ID
        /// <summary>
        //public Guid? CoursewareID { get; set; }

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
        //public Int32 Sort { get; set; }

        /// <summary>
        /// 状态，0：停用；1：启用。
        /// <summary>
        //public Int32 Status { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public String TeacherName { get; set; }

        /// <summary>
        /// 视频时长（秒）
        /// <summary>
        public Int32? VideoTime { get; set; }

        /// <summary>
        /// 文档长度
        /// <summary>
        public Int32? PlayTime { get; set; }
        /// <summary>
        /// 
        /// <summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public DateTime ModifyTime { get; set; }

        public Int16? StudyStatus
        {
            get;
            set;
        }
        public int StudyProgress
        {
            get;
            set;
        }

        public string UrlRoot
        {
            get;
            set;
        }
        /// <summary>
        /// 课程图片
        /// </summary>
        public string ThumbnailURL
        {
            get;
            set;
        }
        public List<DocumentImage> ImageList
        {
            get
            {
                List<DocumentImage> imageList = new List<DocumentImage>();
                if (this.Type == 2 && this.DataInfo != null)
                {
                    //string pdfFile = this.DataInfo.EndsWith("/") ? this.DataInfo.TrimEnd(new char[] { '/' }) : this.DataInfo;
                    string pdfFile = this.DataInfo.Replace(".pdf", "");

                    for (int i = 1; i <= this.PlayTime; i++)
                    {
                        imageList.Add(new DocumentImage()
                        {
                            ImageUrl = string.Format("{0}/{1}/{2}.jpg?r={3}", UrlRoot, pdfFile, i, Guid.NewGuid()),
                            ThumbnailUrl = string.Format("{0}/{1}/{2}_t.jpg?r={3}", UrlRoot, pdfFile, i, Guid.NewGuid()),
                            Description = i.ToString()
                        });
                    }
                }

                return imageList;
            }
        }

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
