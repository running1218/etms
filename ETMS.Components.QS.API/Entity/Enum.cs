using System;

namespace ETMS.Components.QS.API.Entity
{


    /// <summary>
    /// 调查问卷范围类型枚举
    /// </summary>
    [Serializable]
    public enum EnumQueryAreaType
    {
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

}
