//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-10 10:42:20.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// �����飨ϵͳ�ֵ䣩ҵ��ʵ��
    /// </summary>
    [Serializable]
    public partial class Site_SysConfigGroup : AbstractObject
    {
        #region ����ҵ�����

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
        /// ������ID
        /// </summary>
        public Int32 ConfigGroupID { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public String ConfigGroupName { get; set; }

        /// <summary>
        /// ��ʾ���
        /// </summary>
        public Int32 OrderNum { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public Int32 IsUse { get; set; }

        #endregion Fields, Properties

    }
}