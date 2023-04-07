using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    public class Function : ETMS.AppContext.AbstractObject
    {
        #region Fields, Properties
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

        private String functionNameField;
        /// <summary>
        /// 
        /// </summary>
        public String FunctionName
        {
            get
            {
                return this.functionNameField;
            }
            set
            {
                this.functionNameField = value;
            }
        }

        private Int32 functionTypeField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 FunctionType
        {
            get
            {
                return this.functionTypeField;
            }
            set
            {
                this.functionTypeField = value;
            }
        }

        private Int32 orderNoField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 OrderNo
        {
            get
            {
                return this.orderNoField;
            }
            set
            {
                this.orderNoField = value;
            }
        }

        private String descriptionField;
        /// <summary>
        /// 
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

        private Int32 helpIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 HelpID
        {
            get
            {
                return this.helpIDField;
            }
            set
            {
                this.helpIDField = value;
            }
        }

        private Int32 statusField;
        /// <summary>
        /// 
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

        private string CreatorField;
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            get
            {
                return this.CreatorField;
            }
            set
            {
                this.CreatorField = value;
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

        private string ModifierField;
        /// <summary>
        /// 
        /// </summary>
        public string Modifier
        {
            get
            {
                return this.ModifierField;
            }
            set
            {
                this.ModifierField = value;
            }
        }

        private DateTime modifyTimeField;
        /// <summary>
        /// 
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

        public PageUrl PageUrl
        {
            get;
            set;
        }
        
        /// <summary>
        /// 功能所属组件ID
        /// </summary>
        public string ComponentID { get; set; }

        #endregion Fields, Properties

        public override string DefaultKeyName
        {
            get { return "FunctionID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.FunctionID;
            }
            set
            {
                this.FunctionID = (int)value;
            }
        }
    }
}
