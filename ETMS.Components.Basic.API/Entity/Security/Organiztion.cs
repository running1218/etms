using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// ��֯����ʵ��
    /// </summary>
    [Serializable]
    public class Organization : ETMS.Components.Basic.API.Entity.Common.Node
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
        /// ��֯����ID
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
        /// ��֯��������
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
        /// ��֯����ȫ·��
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
        /// ��֯��������
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime? EstablishTime { get; set; }

        /// <summary>
        /// ѧԱ��
        /// </summary>
        public int StudentNum { get; set; }

        /// <summary>
        /// ������ַ
        /// </summary>
        public String Address { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public String PostCode { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public String Telphone { get; set; }

        /// <summary>
        /// ����绰
        /// </summary>
        public String Fax { get; set; }

        /// <summary>
        /// �ƶ��绰
        /// </summary>
        public String MobilePhone { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public String Manager { get; set; }

        /// <summary>
        /// ��ѵ������
        /// </summary>
        public String Trainer { get; set; }

        /// <summary>
        /// ��ѵ�����˵绰
        /// </summary>
        public String TrainerTelphonePhone { get; set; }

        /// <summary>
        /// ��ѵ����������
        /// </summary>
        public String TrainerEmail { get; set; }
        
        /// <summary>
        /// ����Logo
        /// </summary>
        public String Logo { get; set; }

        public string Domain { get; set; }
        /// <summary>
        /// �˵���Ȩ
        /// </summary>
        public string MenuLimit { get; set; }
        /// <summary>
        /// �ײ���Ϣ
        /// </summary>
        public string FooterInfo { get; set; }

        public string Title { get; set; }
        #endregion

        #region ORM
        /// <summary>
        /// ������ת��Ϊ����ʵ��
        /// </summary>
        /// <param name="row">tb_e_PlantRole������</param>
        /// <returns>Role����ʵ��</returns>
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
