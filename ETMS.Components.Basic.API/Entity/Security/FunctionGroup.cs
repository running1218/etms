using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 功能组类定义
    /// </summary>
    [Serializable]
    public class FunctionGroup : ETMS.Components.Basic.API.Entity.Common.Node
    {
        #region Group 属性扩展

        protected override string StartNodeCode
        {
            get
            {
                return "00";
            }
        }
        /// <summary>
        /// 功能组ID
        /// </summary>
        public Int32 GroupID
        {
            get
            {
                return base.NodeID;
            }
            set
            {
                base.NodeID = value;
            }
        }


        /// <summary>
        /// 功能组名称
        /// </summary>
        public String GroupName
        {
            get
            {
                return base.NodeName;
            }
            set
            {
                base.NodeName = value;
            }
        }


        /// <summary>
        /// 功能组编码
        /// </summary>
        public String GroupCode
        {
            get
            {
                return base.NodeCode;
            }
            set
            {
                base.NodeCode = value;
            }
        }
        
        /// <summary>
        /// 功能组所属组件ID
        /// </summary>
        public string ComponentID { get; set; }
        #endregion
        
        public override string DefaultKeyName
        {
            get { return "GroupID"; }
        }
    }
}
