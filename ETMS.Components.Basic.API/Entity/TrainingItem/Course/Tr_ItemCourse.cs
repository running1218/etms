
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem.Course
{
    /// <summary>
    /// ��ѵ��Ŀ�γ̱�ҵ��ʵ��
    /// </summary>
    public partial class Tr_ItemCourse:AbstractObject
	{
        /// <summary>
        /// �γ�����ͼ
        /// </summary>
        public string ThumbnailURL
        {
            get;
            set;
        }
        /// <summary>
        /// �γ̱���
        /// </summary>
        public string CourseCode
        {
            get;
            set;
        }
        /// <summary>
        /// �γ�����
        /// </summary>
        public string CourseName
        {
            get;
            set;
        }
        /// <summary>
        /// �γ�����
        /// </summary>
        public int CourseTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// ѧԱ��Ŀѡ�γɼ�
        /// </summary>
        public decimal SumGrade
        {
            get;
            set;
        }

        /// <summary>
        /// �γ�-ѧԱ����״̬
        /// 0: δ������1������Ŀ�±����� 2��������Ŀ�±���
        /// </summary>
        public int SignStatus
        {
            get;
            set;
        }

        /// <summary>
        /// ѧԱ����ID
        /// </summary>
        public Guid StudentSignupID { get; set; }
        /// <summary>
        /// �γ�ѧϰ����
        /// </summary>
        public int StudyTimes { get; set; }
        /// <summary>
        /// ��ѵ��Ŀ�γ̽�ʦ��
        /// </summary>
        public int TeacherNum { get; set; }
	}
    public class TrainItemCourse
    {
        public Guid TrainingItemID { get; set; }
        public string TrainingItemName { get; set; }
        public Guid CourseID { get; set; }
        public string CourseName { get; set; }
        public string ThumbnailURL { get; set; }
        public string CourseBeginTime { get; set; }
        public string CourseEndTime { get; set; }
        public Guid TrainingItemCourseID { get; set; }
        public decimal StudyProcessPercent { get; set; }
        public int CourseModel { get; set; }
        public int LivingType { get; set; }
    }
}
