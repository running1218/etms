using System;

using System.Data;
namespace ETMS.Components.Basic.API.Entity.Security
{
    public class PageUrl : ETMS.AppContext.AbstractObject
    {
        #region Fields, Properties
        private Int32 pageIDField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 PageID
        {
            get
            {
                return this.pageIDField;
            }
            set
            {
                this.pageIDField = value;
            }
        }

        private String pageURLField;
        /// <summary>
        /// 
        /// </summary>
        public String PageURL
        {
            get
            {
                return this.pageURLField;
            }
            set
            {
                this.pageURLField = value;
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

        private Int32 isMainPageField;
        /// <summary>
        /// 
        /// </summary>
        public Int32 IsMainPage
        {
            get
            {
                return this.isMainPageField;
            }
            set
            {
                this.isMainPageField = value;
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

        #endregion Fields, Properties

        #region ORM
        public static PageUrl ConvertDataRowToPageUrl(DataRow row)
        {
            PageUrl entity = new PageUrl();

            entity.PageID = (Int32)row["PageID"];

            entity.PageURL = (String)row["PageURL"];

            entity.Status = (Int32)row["Status"];

            entity.IsMainPage = (Int32)row["IsMainPage"];

            entity.Description = (String)row["Description"];

            entity.HelpID = (Int32)row["HelpID"];

            entity.FunctionID = (Int32)row["FunctionID"];

            return entity;
        }
        #endregion

        public override string DefaultKeyName
        {
            get { return "PageID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.PageID;
            }
            set
            {
                this.PageID = (int)value;
            }
        }
    }
}
