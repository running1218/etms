using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// ��ɫ�ඨ��
    /// </summary>
    [Serializable]
    public class Role : ETMS.Components.Basic.API.Entity.Common.Node, IRole
    {
        #region Role ������չ
        /// <summary>
        /// ��ɫID
        /// </summary>
        public Int32 RoleID
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
        /// ��ɫ����
        /// </summary>
        public String RoleName
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
        /// ��ɫ����
        /// </summary>
        public String RoleCode
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

        private String m_RoleMapCode = string.Empty;
        /// <summary>
        /// ��ɫӳ�����
        /// ˵���������ʦ����Ӧ�Ľ�ɫ����
        /// </summary>
        public String RoleMapCode
        {
            get
            {
                return m_RoleMapCode;
            }
            set
            {
                m_RoleMapCode = value;
            }
        }

        protected override string StartNodeCode
        {
            get
            {
                return "0000";
            }
        }
        #endregion

        /// <summary>
        /// ��������ID
        /// �������ID=0�����ʾϵͳ���ý�ɫ�������ʾ�����Խ���ɫ��
        /// </summary>
        public int OrganizationID { get; set; }
        #region ORM
        /// <summary>
        /// ������ת��Ϊ����ʵ��
        /// </summary>
        /// <param name="row">tb_e_PlantRole������</param>
        /// <returns>Role����ʵ��</returns>
        public static Role ConvertDataRowToRole(DataRow row)
        {
            Role entity = new Role();
            entity.RoleID = (int)row["RoleID"];
            entity.RoleName = Convert.ToString(row["RoleName"]);
            entity.ParentNodeID = (int)row["ParentID"];
            entity.RoleCode = Convert.ToString(row["RoleCode"]);
            entity.RoleMapCode = Convert.ToString(row["RoleMapCode"]);
            entity.State = (short)row["State"];
            entity.Description = Convert.ToString(row["Description"]);
            entity.Creator = Convert.ToString(row["Creator"]);
            entity.CreateTime = (DateTime)row["CreateTime"];
            entity.Modifier = Convert.ToString(row["Modifier"]);
            entity.ModifyTime = (DateTime)row["ModifyTime"];
            entity.OrganizationID = (int)row["OrganizationID"];
            return entity;
        }
        #endregion

        public override string DefaultKeyName
        {
            get { return "RoleID"; }
        }

        public bool IsSysAdminRole
        {
            get { return this.ParentNodeID == 0; }
        }
    }
}
