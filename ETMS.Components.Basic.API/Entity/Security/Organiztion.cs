using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 组织机构实体
    /// </summary>
    [Serializable]
    public class Organization : ETMS.Components.Basic.API.Entity.Common.Node
    {
        #region Group 属性扩展

        protected override string StartNodeCode
        {
            get
            {
                return "000";
            }
        }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Int32 OrganizationID
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
        /// 组织机构名称
        /// </summary>
        public String OrganizationName
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
        /// 组织机构全路径
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
        /// 组织机构编码
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 机构成立时间
        /// </summary>
        public DateTime? EstablishTime { get; set; }

        /// <summary>
        /// 学员数
        /// </summary>
        public int StudentNum { get; set; }

        /// <summary>
        /// 机构地址
        /// </summary>
        public String Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public String PostCode { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public String Telphone { get; set; }

        /// <summary>
        /// 传真电话
        /// </summary>
        public String Fax { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public String MobilePhone { get; set; }

        /// <summary>
        /// 机构负责人
        /// </summary>
        public String Manager { get; set; }

        /// <summary>
        /// 培训负责人
        /// </summary>
        public String Trainer { get; set; }

        /// <summary>
        /// 培训负责人电话
        /// </summary>
        public String TrainerTelphonePhone { get; set; }

        /// <summary>
        /// 培训负责人邮箱
        /// </summary>
        public String TrainerEmail { get; set; }
        
        /// <summary>
        /// 机构Logo
        /// </summary>
        public String Logo { get; set; }

        public string Domain { get; set; }
        /// <summary>
        /// 菜单授权
        /// </summary>
        public string MenuLimit { get; set; }
        /// <summary>
        /// 底部信息
        /// </summary>
        public string FooterInfo { get; set; }

        public string Title { get; set; }
        #endregion

        #region ORM
        /// <summary>
        /// 行数据转换为对象实体
        /// </summary>
        /// <param name="row">tb_e_PlantRole行数据</param>
        /// <returns>Role对象实体</returns>
        public static Organization ConvertDataRowToRole(DataRow row)
        {
            Organization entity = new Organization();
            entity.OrganizationID = (int)row["OrganizationID"];
            entity.OrganizationName = Convert.ToString(row["OrganizationName"]);
            entity.ParentNodeID = (int)row["ParentID"];
            entity.OrganizationCode = Convert.ToString(row["OrganizationCode"]);
            entity.Path = Convert.ToString(row["Path"]);
            entity.DisplayPath = Convert.ToString(row["DisplayPath"]);
            entity.State = Convert.ToInt32(row["State"]);
            if (!Convert.IsDBNull(row["EstablishTime"]))
            {
                entity.EstablishTime = (DateTime)row["EstablishTime"];
            }
            entity.Address = Convert.ToString(row["Address"]);
            entity.PostCode = Convert.ToString(row["PostCode"]);
            entity.Telphone = Convert.ToString(row["Telphone"]);
            entity.Fax = Convert.ToString(row["Fax"]);
            entity.Manager = Convert.ToString(row["Manager"]);
            entity.MobilePhone = Convert.ToString(row["MobilePhone"]);
            entity.Email = Convert.ToString(row["Email"]);
            entity.Trainer = Convert.ToString(row["Trainer"]);
            entity.TrainerTelphonePhone = Convert.ToString(row["TrainerTelphonePhone"]);
            entity.TrainerEmail = Convert.ToString(row["TrainerEmail"]);
            entity.Logo = Convert.ToString(row["Logo"]);

            entity.Description = Convert.ToString(row["Description"]);
            entity.Creator = Convert.ToString(row["Creator"]);
            entity.CreateTime = (DateTime)row["CreateTime"];
            entity.Modifier = Convert.ToString(row["Modifier"]);
            entity.ModifyTime = (DateTime)row["ModifyTime"];
            entity.OrderNo = (int)row["OrderNo"];
            entity.StudentNum = Convert.IsDBNull(row["StudentNum"]) ? 0 : (int)row["StudentNum"];
            entity.Domain = Convert.ToString(row["Domain"]);
            return entity;
        }
        #endregion
        public override string DefaultKeyName
        {
            get { return "OrganiztionID"; }
        }
    }
}
