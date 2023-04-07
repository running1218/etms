using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 业务实体
    /// </summary>
    [Serializable]
    public class UserRole
    {
        #region Constructor
        /// <summary>
        /// 构造函数--默认
        /// </summary>
        public UserRole()
        {
        }

        /// <summary>
        /// 构造函数--所有属性
        /// </summary>
        public UserRole(Int32 iD, Int32 userID, Int32 roleID, String creator, DateTime createTime)
        {
            this.iDField = iD;
            this.userIDField = userID;
            this.roleIDField = roleID;
            this.creatorField = creator;
            this.createTimeField = createTime;
        }

        #endregion Constructor

        #region Fields, Properties
        private Int32 iDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID
        {
            get
            {
                return this.iDField;
            }
            set
            {
                this.iDField = value;
            }
        }

        private Int32 userIDField;
        /// <summary>
        /// 
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

        private Int32 roleIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 RoleID
        {
            get
            {
                return this.roleIDField;
            }
            set
            {
                this.roleIDField = value;
            }
        }

        private String creatorField;
        /// <summary>
        /// 
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
        /// 
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

        #endregion Fields, Properties

    }
}
