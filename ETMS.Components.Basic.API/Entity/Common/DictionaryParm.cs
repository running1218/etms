using System;

namespace ETMS.Components.Basic.API.Entity.Common
{
    /// <summary>
    /// 字典项参数
    /// </summary>
    [Serializable]
    public class DictionaryParm
    {
        private string tableEnglishName;
        /// <summary>
        /// 表英文名称
        /// </summary>
        public string TableEnglishName
        {
            get
            {
                return tableEnglishName;
            }
            set
            {
                tableEnglishName = value;
            }
        }
        private string tableChinaName;
        /// <summary>
        /// 表中文名称
        /// </summary>
        public string TableChinaName
        {
            get
            {
                return tableChinaName;
            }
            set
            {
                tableChinaName = value;
            }
        }

        private string columnCodeValue;
        /// <summary>
        /// 列代码名称
        /// </summary>
        public string ColumnCodeValue
        {
            get
            {
                return columnCodeValue;
            }
            set
            {
                columnCodeValue = value;
            }
        }

        private string columnNameValue;
        /// <summary>
        /// 列值名称
        /// </summary>
        public string ColumnNameValue
        {
            get
            {
                return columnNameValue;
            }
            set
            {
                columnNameValue = value;
            }
        }

        private string remark;
        /// <summary>
        /// 列值名称
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public int OrganizationID { get; set; }
    }
}
