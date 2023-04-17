
using System;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// �����ҵ��ʵ��
    /// </summary>
    public partial class Inf_Bulletin
	{ 	

		#region Fields, Properties

        /// <summary>
        /// ��ѵ��Ŀ�γ�ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// ���淢����������
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
