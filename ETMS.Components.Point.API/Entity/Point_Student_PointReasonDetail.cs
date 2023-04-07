using System;

using ETMS.AppContext;
namespace ETMS.Components.Point.API.Entity
{
    /// <summary>
    /// ѧԱѧϰ���̻�û��ֱ�ҵ��ʵ��
    /// </summary>
    public partial class Point_Student_PointReasonDetail:AbstractObject
	{
        /// <summary>
        /// ��ѵ��ĿID
        /// </summary>
        public Guid TrainingItemID { get; set; }
        /// <summary>
        /// ��ѵ��Ŀ����
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// ��ѵ��Ŀ��ʼʱ��
        /// </summary>
        public DateTime ItemBeginTime { get; set; }
        /// <summary>
        /// ��ѵ��Ŀ����ʱ��
        /// </summary>
        public DateTime ItemEndTime { get; set; }
        /// <summary>
        /// ��������Ŀ��ѧԱ��
        /// </summary>
        public int StudentNum { get; set; }
        /// <summary>
        /// ��������ĿѧԱ�ܷ�������
        /// </summary>
        public Int64 TotalPoints { get; set; }

        #region ѧԱ��Ϣ
        #endregion
    }

    public partial class PointStudentReasonStudentInfo : Point_Student_PointReasonDetail
    {
        public string RealName { get; set; }
        public int OrganizationID { get; set; }
        public int DepartmentID { get; set; }
    }
}
