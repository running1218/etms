
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Course.Resources
{
    /// <summary>
    /// �γ���Դ��ҵ��ʵ��
    /// </summary>
    [Serializable]
    public partial class Res_CourseRes : AbstractObject
    {
        #region ����ҵ�����

        public override string DefaultKeyName
        {
            get { return "CourseResID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.CourseResID;
            }
            set
            {
                this.CourseResID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// �γ���ԴID
        /// </summary>
        public Guid CourseResID { get; set; }

        /// <summary>
        /// �γ���Դ����
        /// </summary>
        public Int32 CourseResTypeID { get; set; }

        /// <summary>
        /// �γ�ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// ��Դ����
        /// </summary>
        public String ResName { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public Int32 IsUse { get; set; }

        /// <summary>
        /// ��Դ��ʼʱ��
        /// </summary>
        public DateTime ResBeginTime { get; set; }

        /// <summary>
        /// ��Դ����ʱ��
        /// </summary>
        public DateTime ResEndTime { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String CreateUser { get; set; }

        /// <summary>
        /// ������ID
        /// </summary>
        public Int32 CreateUserID { get; set; }

        /// <summary>
        /// ��ԴID
        /// </summary>
        public String ResID { get; set; }

        #endregion Fields, Properties

    }
}
