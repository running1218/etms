
using System;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 公告表业务实体
    /// </summary>
    public partial class Inf_Bulletin
	{ 	

		#region Fields, Properties

        /// <summary>
        /// 培训项目课程ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// 公告发布对象类型
        /// </summary>
        public Int32 BulletinObjectTypeID { get; set; } 
		
		#endregion Fields, Properties

	}
    public class Announcement
    {
        public int ArticleID { get; set; }

        public string ImageUrl { get; set; }

        public string MainHead { get; set; }

        public string CreateTime { get; set; }

        public string ArticleContent { get; set; }
    }
}
