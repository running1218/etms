using System;
namespace ETMS.Components.Basic.API.Entity.TrainingItem.Course.Teacher
{
    /// <summary>
    /// ��ѵ��Ŀ�γ̽�ʦ��ҵ��ʵ��
    /// </summary>
    public partial class Tr_ItemCourseTeacher
	{
        public string TeacherCode { get; set; }
        public int TeacherLevelID { get; set; }
        public int TeacherSourceID { get; set; }
        public int TeacherTypeID { get; set; }
        public string RealName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public Guid OuterOrgID { get; set; }
        public int OrganizationID { get; set; }
	}
}
