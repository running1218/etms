
using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// ѧԱ��Ϣ(�û���չ��)ҵ��ʵ��
    /// </summary>
    [Serializable]
    public partial class Site_Student
    {

        /// <summary>
        /// ����
        /// </summary>
        public String WorkerNo { get; set; }

        /// <summary>
        /// ְ��
        /// </summary>
        public Int32? RankID { get; set; }

        /// <summary>
        /// ��λ
        /// </summary>
        public Int32? PostID { get; set; }
       
        /// <summary>
        /// ֱ���ϼ�
        /// </summary>
        public String Superior { get; set; }

        /// <summary>
        /// ���ѧ��
        /// </summary>
        public String LastEducation { get; set; }

        /// <summary>
        /// רҵ
        /// </summary>
        public String Specialty { get; set; }

        /// <summary>
        /// ��ְ����
        /// </summary>
        public DateTime JoinTime { get; set; }
        
        /// <summary>
        /// ���÷�ʽ
        /// </summary>
        public int ResettlementWayID { get; set; }

    }
}
