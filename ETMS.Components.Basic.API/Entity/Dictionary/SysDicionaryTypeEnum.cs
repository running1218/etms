namespace ETMS.Components.Basic.API.Entity.Dictionary
{
    /// <summary>
    /// 系统字典表枚举
    /// </summary>
    public enum SysDicionaryTypeEnum
    {
        #region 系统配置相关
        /// <summary>
        /// 组件清单
        /// </summary>
        Site_Dic_Component,
        #endregion

        #region 系统字典 用户
        /// <summary>
        /// 所有机构
        /// </summary>
        vw_Dic_Sys_Organization,
        /// <summary>
        /// 所有部门
        /// </summary>
        vw_Dic_Sys_Department,
        /// <summary>
        /// 所有岗位
        /// </summary>
        vw_Dic_Sys_Post,
        /// <summary>
        /// 职级(来至视图，输出名称：职级名称+职级类型名称)
        /// </summary>
        vw_Dic_Sys_Rank,
        /// <summary>
        /// 岗位类型
        /// </summary>
        Dic_Sys_PostType,
        /// <summary>
        /// 性别
        /// </summary>
        Dic_Sys_Sex,
        /// <summary>
        /// 政治面貌
        /// </summary>
        Dic_Sys_Politics,
        /// <summary>
        /// 工作职务
        /// </summary>
        Dic_Sys_JobTitle,

        /// <summary>
        /// 安置方式
        /// </summary>
        Dic_Sys_ResettlementWay,
        #endregion


        #region 系统字典
        /// <summary>
        /// 教室用途（系统字典表）
        /// </summary>
        Dic_Sys_ClassRoomPurpose,
        /// <summary>
        /// 课程属性（系统字典表）
        /// </summary>
        Dic_Sys_CourseAttr,
        /// <summary>
        /// 课时安排状态（系统字典表）
        /// </summary>
        Dic_Sys_CourseHoursStatus,
        /// <summary>
        /// 课程等级（系统字典表）
        /// </summary>
        Dic_Sys_CourseLevel,
        /// <summary>
        /// 课程资源类型（系统字典表）
        /// </summary>
        Dic_Sys_CourseResType,
        /// <summary>
        /// 课程类型（系统字典表）
        /// </summary>
        Dic_Sys_CourseType,
        /// <summary>
        /// 课件类型（系统字典表）
        /// </summary>
        Dic_Sys_CoursewareType,
        /// <summary>
        /// 学习地图类型（系统字典表）
        /// </summary>
        Dic_Sys_ELearningMapType,
        /// <summary>
        /// 项目结束方式（系统字典表）
        /// </summary>
        Dic_Sys_ItemEndMode,
        /// <summary>
        /// 计划类型（系统字典表）
        /// </summary>
        Dic_Sys_PlanType,
        /// <summary>
        /// 调查类型（系统字典表）
        /// </summary>
        Dic_Sys_Poll_QueryType,
        /// <summary>
        /// 调查题型（系统字典表）
        /// </summary>
        Dic_Sys_Poll_TitleType,
        /// <summary>
        /// 签到信息（系统字典表）
        /// </summary>
        Dic_Sys_SigninType,
        /// <summary>
        /// 报名方式（系统字典表）
        /// </summary>
        Dic_Sys_SignupMode,
        /// <summary>
        /// 专业类别（系统字典表）
        /// </summary>
        Dic_Sys_SpecialtyType,
        /// <summary>
        /// 学员类型（系统字典表）
        /// </summary>
        Dic_Sys_StudentType,
        /// <summary>
        /// 地图学习类型
        /// </summary>
        Dic_Sys_StudyModel,
        /// <summary>
        /// 讲师等级（系统字典表）
        /// </summary>
        Dic_Sys_TeacherLevel,
        /// <summary>
        /// 讲师来源（系统字典表）
        /// </summary>
        Dic_Sys_TeacherSource,
        /// <summary>
        /// 讲师分类（系统字典表）
        /// </summary>
        Dic_Sys_TeacherType,
        /// <summary>
        /// 授课方式（系统字典表）
        /// </summary>
        Dic_Sys_TeachModel,
        /// <summary>
        /// 培训级别（系统字典表）
        /// </summary>
        Dic_Sys_TrainingLevel,
        /// <summary>
        /// 培训方式（系统字典表）
        /// </summary>
        Dic_Sys_TrainingModel,
        /// <summary>
        /// 培训时间说明（系统字典表）
        /// </summary>
        Dic_Sys_TrainingTimeDesc,
        /// <summary>
        /// 公告级别（系统字典表)
        /// </summary>
        Inf_dic_InfoLevel,
        /// <summary>
        /// 公告类别（系统字典表)
        /// </summary>
        Inf_dic_BulletinType,
        /// <summary>
        /// 公告发布对象类型（系统字典表）
        /// </summary>
        Inf_dic_BulletinObjectType,
        /// <summary>
        /// 违纪情况（系统字典表）（签到用）
        /// </summary>
        Dic_Sys_Lawlessness,
        /// <summary>
        /// 直播类型
        /// 1：1对1小班
        /// 2: 1对6小班
        /// 3: 1对多大班
        /// </summary>
        Dic_Sys_LivingType,

        #endregion

        #region 题库字典
        /// <summary>
        /// 题型
        /// </summary>
        TK_Dic_QuestionType,

        #endregion

        #region 评价系统
        /// <summary>
        /// 评价对象字典
        /// </summary>
        Evaluation_b_ObjectType,

        #endregion

        #region IDP

        /// <summary>
        /// IDP类型
        /// </summary>
        Dic_Sys_IDPType,

        #endregion        
    }
}
