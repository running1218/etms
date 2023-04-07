using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// �������ඨ��
    /// </summary>
    [Serializable]
    public class FunctionGroup : ETMS.Components.Basic.API.Entity.Common.Node
    {
        #region Group ������չ

        protected override string StartNodeCode
        {
            get
            {
                return "00";
            }
        }
        /// <summary>
        /// ������ID
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
        /// ����������
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
        /// ���������
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
        /// �������������ID
        /// </summary>
        public string ComponentID { get; set; }
        #endregion
        
        public override string DefaultKeyName
        {
            get { return "GroupID"; }
        }
    }
}
