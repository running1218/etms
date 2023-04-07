using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 角色类定义
    /// </summary>
    [Serializable]
    public class Role : ETMS.Components.Basic.API.Entity.Common.Node, IRole
    {
        #region Role 属性扩展
        /// <summary>
        /// 角色ID
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
        /// 角色名称
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
        /// 角色编码
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
        /// 角色映射编码
        /// 说明：教务教师有相应的角色编码
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
        /// 所属机构ID
        /// 如果机构ID=0，则表示系统内置角色，否则表示机构自建角色。
        /// </summary>
        public int OrganizationID { get; set; }
        #region ORM
        /// <summary>
        /// 行数据转换为对象实体
        /// </summary>
        /// <param name="row">tb_e_PlantRole行数据</param>
        /// <returns>Role对象实体</returns>
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
