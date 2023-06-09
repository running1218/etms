
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Import
{

    /// <summary>
    /// 学员信息导入明细业务实体
    /// </summary>
    [Serializable]
    public partial class Import_Detail_Student : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "DetailID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.DetailID;
            }
            set
            {
                this.DetailID = (Int32)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 任务明细ID
        /// </summary>
        public Int32 DetailID { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public Int32 TaskID { get; set; }

        /// <summary>
        /// 数据项导入状态
        /// </summary>
        public Int16 Status { get; set; }

        /// <summary>
        /// 数据项导入说明
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 学员账户
        /// </summary>
        public String LoginName { get; set; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public String DepartmentName { get; set; }

        /// <summary>
        /// 职级名称
        /// </summary>
        public String RankName { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public String PostName { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        public String PostTypeName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public String Mobile { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public String WorkerNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int32 SexTypeID { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public String Identity { get; set; }

        /// <summary>
        /// 工作职务名称
        /// </summary>
        public String TitleName { get; set; }

        /// <summary>
        /// 直接上级
        /// </summary>
        public String Superior { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public String OfficeTelphone { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public String LastEducation { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public String Specialty { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime JoinTime { get; set; }
        /// <summary>
        /// 安置方式
        /// </summary>
        public String ResettlementWayName { get; set; }

        #endregion Fields, Properties

    }
}
