
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Dictionary
{
    /// <summary>
    /// ҵ��ʵ��
    /// </summary>
    [Serializable]
    public partial class Dic_Post : AbstractObject
    {
        #region ����ҵ�����

        public override string DefaultKeyName
        {
            get { return "PostID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.PostID;
            }
            set
            {
                this.PostID = (Int32)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 
        /// </summary>
        public Int32 PostID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PostCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PostName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Liability { get; set; }

        /// <summary>
        /// רҵ���
        /// </summary>
        public Int32 PostTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 OrganizationID { get; set; }

        /// <summary>
        /// 1������	   0��ͣ��	   -1��ɾ��
        /// </summary>
        public Int16 Status { get; set; }

        #endregion Fields, Properties
    }
}
