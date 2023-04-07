using System;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// ѧϰ��ͼ��ҵ��ʵ��
    /// </summary>
    public partial class Res_StudyMapCourse : Res_StudyMap
	{
        #region �γ���Ϣ Fields, Properties
        /// <summary>
        /// �γ�ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// �γ̱���
        /// </summary>
        public String CourseCode { get; set; }

        /// <summary>
        /// �γ�����
        /// </summary>
        public String CourseName { get; set; }

        /// <summary>
        /// �γ̵ȼ�
        /// </summary>
        public Int32 CourseLevelID { get; set; }

        /// <summary>
        /// �γ�����
        /// </summary>
        public Int32 CourseTypeID { get; set; }

        /// <summary>
        /// �γ�״̬
        /// </summary>
        public Int32 CourseStatus { get; set; }

        /// <summary>
        /// �Ƿ񹫿�
        /// </summary>
        public Boolean IsPublic { get; set; }

        /// <summary>
        /// ��ʱ
        /// </summary>
        public Decimal CourseHours { get; set; }

        /// <summary>
        /// ����ͼ
        /// </summary>
        public String ThumbnailURL { get; set; }

        /// <summary>
        /// ���ö���
        /// </summary>
        public String ForObject { get; set; }

        /// <summary>
        /// �γ̽���
        /// </summary>
        public String CourseIntroduction { get; set; }

        /// <summary>
        /// �γ̴��
        /// </summary>
        public String CourseOutline { get; set; }
        /// <summary>
        /// ѧϰ��ʽ
        /// </summary>
        public int StudyModelID { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string ChargeMan { get; set; }
        #endregion
    }
}
