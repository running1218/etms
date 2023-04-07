using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{

    public class UserFunction : ETMS.AppContext.AbstractObject
    {
        
        #region Constructor
        /// <summary>
        /// 构造函数--默认
        /// </summary>
        public UserFunction()
        {
        }

        /// <summary>
        /// 构造函数--所有属性
        /// </summary>
        public UserFunction(Int32 roleFunctionID, Int32 roleID, Int32 functionGroupID, Int32 functionID, String creator, DateTime createTime)
        {
            this.roleFunctionIDField = roleFunctionID;
            this.roleIDField = roleID;
            this.functionGroupIDField = functionGroupID;
            this.functionIDField = functionID;
            this.creatorField = creator;
            this.createTimeField = createTime;
        }

        #endregion Constructor

        #region Fields, Properties
        private Int32 roleFunctionIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 UserFunctionID
        {
            get
            {
                return this.roleFunctionIDField;
            }
            set
            {
                this.roleFunctionIDField = value;
            }
        }

        private Int32 roleIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 UserID
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

        private Int32 functionGroupIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 FunctionGroupID
        {
            get
            {
                return this.functionGroupIDField;
            }
            set
            {
                this.functionGroupIDField = value;
            }
        }
        public string FunctionGroupCode { get; set; }
        private Int32 functionIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 FunctionID
        {
            get
            {
                return this.functionIDField;
            }
            set
            {
                this.functionIDField = value;
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
        public static UserFunction ConvertDataRowToUserFunction(DataRow row)
        {
            UserFunction entity = new UserFunction();

            entity.UserFunctionID = (Int32)row["UserFunctionID"];

            entity.UserID = (Int32)row["UserID"];

            entity.FunctionGroupID = (Int32)row["FunctionGroupID"]; 
            entity.FunctionGroupCode = (string)row["FunctionGroupCode"];

            entity.FunctionID = (Int32)row["FunctionID"];

            entity.Creator = (String)row["Creator"];

            entity.CreateTime = (DateTime)row["CreateTime"];

            return entity;
        }
        #endregion

        public override string DefaultKeyName
        {
            get { return "UserFunctionID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.UserFunctionID;
            }
            set
            {
                this.UserFunctionID = (int)value;
            }
        }
    }
}
