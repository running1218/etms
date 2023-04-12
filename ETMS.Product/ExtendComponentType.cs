using System;

namespace ETMS.Product
{
    /// <summary>
    /// 扩展组件类型
    /// 注意：如有新模块，请按照命名规则追加
    /// </summary>
    [Serializable]
    public enum ExtendComponentType
    {
        /// <summary>
        /// 在线课件模块
        /// </summary>
        CourseWare = 1,

        /// <summary>
        /// 在线作业模块
        /// </summary>
        ExOnlineJob = 2,

        /// <summary>
        /// 在线考试模块
        /// </summary>
        ExOnlineTest = 3,

        /// <summary>
        /// SCORM课件
        /// </summary>
        CourseWare_SCORM = 4,

        /// <summary>
        /// 非SCORM课件
        /// </summary>
        CourseWare_NotSCORM = 5,

        /// <summary>
        /// 导学资料
        /// </summary>
        Guidance = 6,

        /// <summary>
        /// 离线作业
        /// </summary>
        OfflineJob = 7,

        /// <summary>
        /// 公共论坛
        /// </summary>
        PublicBBS = 8,

        /// <summary>
        /// 课程论坛
        /// </summary>
        CourseBBS = 9,

        /// <summary>
        /// 班级论坛
        /// </summary>
        ClassBBS = 10,

        /// <summary>
        /// 培训计划
        /// </summary>
        TraningPlan = 11,

        /// <summary>
        /// 课程点评
        /// </summary>
        CourseComment = 12,

        /// <summary>
        /// 请假
        /// </summary>
        Leave = 13,

        /// <summary>
        /// 导师
        /// </summary>
        Mentor = 14,

        /// <summary>
        /// 费用
        /// </summary>
        Fee = 15,

        /// <summary>
        /// 积分
        /// </summary>
        Points = 16,
        
        /// <summary>
        /// 满意度调查
        /// </summary>
        Poll_R1 = 17,

        /// <summary>
        /// 培训需求调查
        /// </summary>
        Poll_R2 = 18,

        /// <summary>
        /// 项目公告
        /// </summary>
        ItemNotice = 19,

        /// <summary>
        /// 项目课程导学资料
        /// </summary>
        CourseGuidance =20
    }
}
