using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Serializable]
    public class User : ETMS.AppContext.AbstractObject, IManageUser
    {
        #region Fields, Properties
        private Int32 userIDField;
        /// <summary>
        /// 用户ID
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
        /// 登陆名
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
        /// 真实姓名
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
        /// 登陆口令
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
        /// 家庭电话
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
        /// 办公电话
        /// </summary>
        public string OfficeTelphone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }
        private String descriptionField;
        /// <summary>
        /// 更多信息
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
        /// 状态
        /// 1：启用 0：禁用
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
        /// 创建者
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
        /// 创建时间
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
        /// 修改者
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
        /// 修改时间
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
        /// 隶属的角色
        /// 说明：登陆后可用看到
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
        /// 是否是系统管理员
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
        /// 账户所属机构编码
        /// 1、所有账户必须指定机构编码
        /// 2、超级管理员账户对应的机构编码为0
        /// </summary>
        public int OrganizationID { get; set; }

        /// <summary>
        /// 机构名
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 学院类型
        /// </summary>
        public string ResettlementWayName { get; set; }

        /// <summary>
        /// 账户所属部门编码
        /// 学员、内部讲师类型账户时必须设置
        /// 管理账户、外部讲师则默认为0
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 是否是系统账户
        /// 系统账户有：
        ///   1、超级管理员账户
        ///   2、机构创建时自动创建机构管理员账户
        /// </summary>
        public bool IsSysAccount { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int32 SexTypeID { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SexTypeName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public String Identity { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 工作职务
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public Int32 PoliticsTypeID { get; set; }
        #endregion Fields, Properties

        #region ORM
        /// <summary>
        /// 数据行转实体对象
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>实体</returns>
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
