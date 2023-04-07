using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// �������ඨ��
    /// </summary>
    [Serializable]
    public class Department : ETMS.Components.Basic.API.Entity.Common.Node
    {
        #region Group ������չ

        protected override string StartNodeCode
        {
            get
            {
                return "000";
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int32 DepartmentID
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
        /// ��������
        /// </summary>
        public String DepartmentName
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
        /// ����Path
        /// </summary>
        public String Path
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
        /// ����ҵ�����
        /// </summary>
        public String DepartmentCode { get; set; }

        /// <summary>
        /// ��������֯��������
        /// </summary>
        public int OrganizationID { get; set; }

        /// <summary>
        /// ���Ÿ�����
        /// </summary>
        public String Manager { get; set; }

        #endregion

        #region ORM
        /// <summary>
        /// ������ת��Ϊ����ʵ��
        /// </summary>
        /// <param name="row">tb_e_PlantRole������</param>
        /// <returns>Role����ʵ��</returns>
        public static Department ConvertDataRowToRole(DataRow row)
        {
            Department entity = new Department();
            entity.DepartmentID = (int)row["DepartmentID"];
            entity.DepartmentName = Convert.ToString(row["DepartmentName"]);
            entity.ParentNodeID = (int)row["ParentID"];
            entity.DepartmentCode = Convert.ToString(row["DepartmentCode"]);
            entity.Path = Convert.ToString(row["Path"]);
            entity.DisplayPath = Convert.ToString(row["DisplayPath"]);
            entity.State = Convert.ToInt32(row["State"]);
            entity.OrganizationID = Convert.ToInt32(row["OrganizationID"]);
            entity.Description = Convert.ToString(row["Description"]);
            entity.Manager = row["Manager"].ToString();
            entity.Creator = Convert.ToString(row["Creator"]);
            entity.CreateTime = (DateTime)row["CreateTime"];
            entity.Modifier = Convert.ToString(row["Modifier"]);
            entity.ModifyTime = (DateTime)row["ModifyTime"];
            entity.OrderNo = (int)row["OrderNo"];
            return entity;
        }
        #endregion
        public override string DefaultKeyName
        {
            get { return "DepartmentID"; }
        }
    }
}
