
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 配置组（系统字典）业务实体
    /// </summary>
    [Serializable]
    public partial class Site_SysConfigGroup : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "ConfigGroupID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.ConfigGroupID;
            }
            set
            {
                this.ConfigGroupID = (Int32)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 配置组ID
        /// </summary>
        public Int32 ConfigGroupID { get; set; }

        /// <summary>
        /// 配置组名称
        /// </summary>
        public String ConfigGroupName { get; set; }

        /// <summary>
        /// 显示序号
        /// </summary>
        public Int32 OrderNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Int32 IsUse { get; set; }

        #endregion Fields, Properties

    }
}
