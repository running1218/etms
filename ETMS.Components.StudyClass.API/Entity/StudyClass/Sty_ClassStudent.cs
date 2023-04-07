using System;
namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    /// <summary>
    /// �༶ѧԱ��ҵ��ʵ��
    /// </summary>
    public partial class Sty_ClassStudent
    {
        /// <summary>
        /// �û�����
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string WorkerNo { get; set; }

        /// <summary>
        /// ְ��
        /// </summary>
        public int RankID { get; set; }

        /// <summary>
        /// ��λ
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// ��֯�ṹ
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// �绰
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// ְ��
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// �༶ְ��
        /// </summary>
        public string ClassPostion { get; set; }

        /// <summary>
        /// ѧԱְ��ID
        /// </summary>
        public int StudentTypeID { get; set; }
        /// <summary>
        /// ѧԱ��Ƭ
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// �Ƿ�Ⱥ��ӳ�
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// �༶Ⱥ��
        /// </summary>
        public string ClassSubgroupName { get; set; }
        /// <summary>
        /// �༶Ⱥ��ѧԱID
        /// </summary>
        public Guid SubgroupStudentID { get; set; }
    }
}
