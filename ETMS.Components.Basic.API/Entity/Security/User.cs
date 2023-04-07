using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// �û�����
    /// </summary>
    [Serializable]
    public class User : ETMS.AppContext.AbstractObject, IManageUser
    {
        #region Fields, Properties
        private Int32 userIDField;
        /// <summary>
        /// �û�ID
        /// </summary>
        public Int32 UserID
        {
            get
            {
                return this.userIDField;
            }
            set
            {
                this.userIDField = value;
            }
        }

        private String loginNameField;
        /// <summary>
        /// ��½��
        /// </summary>
        public String LoginName
        {
            get
            {
                return this.loginNameField;
            }
            set
            {
                this.loginNameField = value;
            }
        }

        private String realNameField;
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public String RealName
        {
            get
            {
                return this.realNameField;
            }
            set
            {
                this.realNameField = value;
            }
        }

        private String passWordField;
        /// <summary>
        /// ��½����
        /// </summary>
        public String PassWord
        {
            get
            {
                return this.passWordField;
            }
            set
            {
                this.passWordField = value;
            }
        }

        private String emailField;
        /// <summary>
        /// Email
        /// </summary>
        public String Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        private String telphoneField;
        /// <summary>
        /// ��ͥ�绰
        /// </summary>
        public String Telphone
        {
            get
            {
                return this.telphoneField;
            }
            set
            {
                this.telphoneField = value;
            }
        }
        /// <summary>
        /// �칫�绰
        /// </summary>
        public string OfficeTelphone { get; set; }
        /// <summary>
        /// �ֻ�
        /// </summary>
        public string MobilePhone { get; set; }
        private String descriptionField;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public String Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        private Int32 statusField;
        /// <summary>
        /// ״̬
        /// 1������ 0������
        /// </summary>
        public Int32 Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        private String creatorField;
        /// <summary>
        /// ������
        /// </summary>
        public String Creator
        {
            get
            {
                return this.creatorField;
            }
            set
            {
                this.creatorField = value;
            }
        }

        private DateTime createTimeField;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return this.createTimeField;
            }
            set
            {
                this.createTimeField = value;
            }
        }

        private String modifierField;
        /// <summary>
        /// �޸���
        /// </summary>
        public String Modifier
        {
            get
            {
                return this.modifierField;
            }
            set
            {
                this.modifierField = value;
            }
        }

        private DateTime modifyTimeField;
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        public DateTime ModifyTime
        {
            get
            {
                return this.modifyTimeField;
            }
            set
            {
                this.modifyTimeField = value;
            }
        }

        private IRole[] roleCodeFields;
        /// <summary>
        /// �����Ľ�ɫ
        /// ˵������½����ÿ���
        /// </summary>
        public IRole[] MapRoles
        {
            get
            {
                return roleCodeFields;
            }
            set
            {
                roleCodeFields = value;
            }
        }
        /// <summary>
        /// �Ƿ���ϵͳ����Ա
        /// </summary>
        public bool IsSysAdmin
        {
            get
            {
                if (this.roleCodeFields == null)
                    return false;
                else
                {
                    foreach (IRole role in roleCodeFields)
                    {
                        if (role.IsSysAdminRole)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// �˻�������������
        /// 1�������˻�����ָ����������
        /// 2����������Ա�˻���Ӧ�Ļ�������Ϊ0
        /// </summary>
        public int OrganizationID { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// ѧԺ����
        /// </summary>
        public string ResettlementWayName { get; set; }

        /// <summary>
        /// �˻��������ű���
        /// ѧԱ���ڲ���ʦ�����˻�ʱ��������
        /// �����˻����ⲿ��ʦ��Ĭ��Ϊ0
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// �Ƿ���ϵͳ�˻�
        /// ϵͳ�˻��У�
        ///   1����������Ա�˻�
        ///   2����������ʱ�Զ�������������Ա�˻�
        /// </summary>
        public bool IsSysAccount { get; set; }

        /// <summary>
        /// �û�ͷ��
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// �Ա�
        /// </summary>
        public Int32 SexTypeID { get; set; }

        /// <summary>
        /// �Ա�
        /// </summary>
        public string SexTypeName { get; set; }

        /// <summary>
        /// ���֤��
        /// </summary>
        public String Identity { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// ����ְ��
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// ������ò
        /// </summary>
        public Int32 PoliticsTypeID { get; set; }
        #endregion Fields, Properties

        #region ORM
        /// <summary>
        /// ������תʵ�����
        /// </summary>
        /// <param name="row">������</param>
        /// <returns>ʵ��</returns>
        public static User ConvertDataRowToUser(DataRow row)
        {
            User entity = new User();

            entity.UserID = (Int32)row["UserID"];

            entity.LoginName = Convert.ToString(row["LoginName"]);

            entity.RealName = Convert.ToString(row["RealName"]);

            entity.PassWord = Convert.ToString(row["PassWord"]);

            entity.Email = Convert.ToString(row["Email"]);

            entity.Telphone = Convert.ToString(row["Telphone"]);

            entity.Description = Convert.ToString(row["Description"]);

            entity.Status = (Int32)row["Status"];

            entity.Creator = Convert.ToString(row["Creator"]);

            entity.CreateTime = (DateTime)row["CreateTime"];

            entity.Modifier = Convert.ToString(row["Modifier"]);

            entity.ModifyTime = (DateTime)row["ModifyTime"];

            entity.OrganizationID = Convert.ToInt32(row["OrganizationID"]);
            if (row["DepartmentID"] != DBNull.Value)
                entity.DepartmentID = Convert.ToInt32(row["DepartmentID"]);
            entity.OfficeTelphone = Convert.ToString(row["OfficeTelphone"]);
            entity.MobilePhone = Convert.ToString(row["MobilePhone"]);
            entity.IsSysAccount = (bool)row["IsSysAccount"];
            entity.PhotoUrl = Convert.ToString(row["PhotoUrl"]);

            entity.SexTypeID = Convert.ToInt32(row["SexTypeID"]);
            if (!DBNull.Value.Equals(row["Birthday"]))
            {
                entity.Birthday = (DateTime)row["Birthday"];
            }
            entity.Identity = Convert.ToString(row["Identity"]);
            entity.PoliticsTypeID = Convert.ToInt32(row["PoliticsTypeID"]);
            entity.TitleName = Convert.ToString(row["TitleName"]);
            
            return entity;
        }
        #endregion

        public override string DefaultKeyName
        {
            get { return "UserID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.UserID;
            }
            set
            {
                this.UserID = (int)value;
            }
        }

    }
}
