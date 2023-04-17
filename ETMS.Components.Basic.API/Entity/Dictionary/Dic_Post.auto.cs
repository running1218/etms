
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Dictionary
{
    /// <summary>
    /// 业务实体
    /// </summary>
    [Serializable]
    public partial class Dic_Post : AbstractObject
    {
        #region 所有业务基类

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
        /// 专业类别
        /// </summary>
        public Int32 PostTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 OrganizationID { get; set; }

        /// <summary>
        /// 1：启用	   0：停用	   -1：删除
        /// </summary>
        public Int16 Status { get; set; }

        #endregion Fields, Properties
    }
}
