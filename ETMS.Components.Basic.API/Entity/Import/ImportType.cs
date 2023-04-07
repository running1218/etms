namespace ETMS.Components.Basic.API.Entity.Import
{
    /// <summary>
    /// 导入类型
    /// 1、学员基本信息
    /// 2、Scorm课件包
    /// 3、项目学员信息
    /// </summary>
    public enum ImportType
    {
        /// <summary>
        /// 学员基本信息
        /// </summary>
        StudentInfo = 1,
        /// <summary>
        /// Scorm课件包
        /// </summary>
        ScormPackage = 2,
        /// <summary>
        /// 项目学员信息
        /// </summary>
        TrainingItemStudent = 3,
        /// <summary>
        /// 学员学习课程导入
        /// </summary>
        StudentCourseGrade = 4,
        /// <summary>
        /// 课程试题
        /// </summary>
        CourseQuestion = 5
    }
}
