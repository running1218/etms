//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-05-09 09:25:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity
{
    /// <summary>
    /// 学习地图与IDP非课程资料关系表业务实体
    /// </summary>
    public partial class Res_StudyMapReferIDP:AbstractObject
	{
        #region Fields, Properties
        /// <summary>
        /// IDP学习内容来源
        /// </summary>
        public Int32 IDPSourceID { get; set; }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

        /// <summary>
        /// 资料编码
        /// </summary>
        public String DataCode { get; set; }

        /// <summary>
        /// 资料名称
        /// </summary>
        public String DataName { get; set; }

        /// <summary>
        /// 学习内容
        /// </summary>
        public String DataCotent { get; set; }

        /// <summary>
        /// 学习纲要
        /// </summary>
        public String DataOutline { get; set; }

        /// <summary>
        /// 预计时长
        /// </summary>
        public Decimal TimeLength { get; set; }

        /// <summary>
        /// 资料状态
        /// </summary>
        public Int32 DataStatus { get; set; }

        /// <summary>
        /// 学习方式
        /// </summary>
        public Int32 StudyModelID { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public Int32 StudyTimes { get; set; }

        /// <summary>
        /// 实施人
        /// </summary>
        public String Implementor { get; set; }

        /// <summary>
        /// 培训资料所在
        /// </summary>
        public String DataURL { get; set; }

        /// <summary>
        /// 责任方
        /// </summary>
        public String DutyMan { get; set; }

        /// <summary>
        /// 学习效果评量方式
        /// </summary>
        public String EvaluationMode { get; set; }
        /// <summary>
        /// 学习次数描述
        /// </summary>
        public String StudyTimeDes { get; set; }

        #endregion Fields, Properties
	}
}
