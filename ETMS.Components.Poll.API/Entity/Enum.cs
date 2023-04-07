using System;

namespace ETMS.Components.Poll.API.Entity
{
    /// <summary>
    /// 与问卷相关的资源分类
    /// </summary>
    [Serializable]
    public class EnumResourceType
    {
        /// <summary>
        /// 满意度调查
        /// </summary>
        public static string R1 = "R1";
        /// <summary>
        /// 培训需求调查
        /// </summary>
        public static string R2 = "R2";
    }

    /// <summary>
    /// 与问卷相关资源编码
    /// </summary>
    [Serializable]
    public class EnumResourceCode
    {
        /// <summary>
        /// 满意度调查
        /// </summary>
        public static string R1_Code = "00000000-0000-0000-0000-000000000001";
        /// <summary>
        /// 培训需求调查
        /// </summary>
        public static string R2_Code = "00000000-0000-0000-0000-000000000002";
    }

    /// <summary>
    /// 调查问卷范围类型枚举
    /// </summary>
    [Serializable]
    public enum EnumQueryAreaType
    {
        /// <summary>
        /// 本机构
        /// </summary>
        CurrentOrg,
        /// <summary>
        /// 仅下级机构
        /// </summary>
        SubOrg,
        /// <summary>
        /// 本机构及下级机构
        /// </summary>
        AllOrg,
        /// <summary>
        /// 培训项目
        /// </summary>
        TrainItem,

        //以下是黄中福2013-01-17加

        /// <summary>
        /// 学员(Site_User表)
        /// </summary>
        Student,
        /// <summary>
        /// 该组织机构下的所有启用学员
        /// </summary>
        OrgStudent,
        /// <summary>
        /// 该培训项目下的所有启用学员
        /// </summary>
        TrainItemStudent,
        /// <summary>
        /// 该培训项目课程下的所有启用学员
        /// </summary>
        TrainItemCourseStudent,
        /// <summary>
        /// 该培训项目课程下的课时(主讲讲师)下的所有学员
        /// </summary>
        TrainItemCourseHoursStudent,
        /// <summary>
        /// 所有学员
        /// </summary>
        AllStudent,
    }

    [Serializable]
    public enum EnumQueryAreaDetailType
    {
        /// <summary>
        /// 学员
        /// </summary>
        Student,

        /// <summary>
        /// 课程（培训项目范围下第二级限制）
        /// </summary>
        Course,

        /// <summary>
        /// 讲师（培训项目范围下第三级限制）
        /// </summary>
        Teacher,
    }
}
