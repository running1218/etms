using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 角色环境关系定义
    /// </summary>
    public class RoleEnvironment : ETMS.AppContext.AbstractObject
    {
        
        #region Constructor
        /// <summary>
        /// 构造函数--默认
        /// </summary>
        public RoleEnvironment()
        {
        }

        /// <summary>
        /// 构造函数--所有属性
        /// </summary>
        public RoleEnvironment(Int32 roleEnvironmentID, Int32 roleID, Int32 environmentID, String creator, DateTime createTime)
        {
            this.roleEnvironmentIDField = roleEnvironmentID;
            this.roleIDField = roleID;
            this.environmentIDField = environmentID;
            this.creatorField = creator;
            this.createTimeField = createTime;
        }

        #endregion Constructor

        #region Fields, Properties
        private Int32 roleEnvironmentIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 RoleEnvironmentID
        {
            get
            {
                return this.roleEnvironmentIDField;
            }
            set
            {
                this.roleEnvironmentIDField = value;
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
             
        private Int32 environmentIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 EnvironmentID
        {
            get
            {
                return this.environmentIDField;
            }
            set
            {
                this.environmentIDField = value;
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

        #region ORM
        public static RoleEnvironment ConvertDataRowToRoleEnvironment(DataRow row)
        {
            RoleEnvironment entity = new RoleEnvironment();

            entity.RoleEnvironmentID = (Int32)row["RoleEnvironmentID"];

            entity.RoleID = (Int32)row["RoleID"];

            entity.EnvironmentID = (Int32)row["EnvironmentID"];

            entity.Creator = (String)row["Creator"];

            entity.CreateTime = (DateTime)row["CreateTime"];

            return entity;
        }
        #endregion

        public override string DefaultKeyName
        {
            get { return "RoleEnvironmentID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.RoleEnvironmentID;
            }
            set
            {
                this.RoleEnvironmentID = (int)value;
            }
        }
    }
}
