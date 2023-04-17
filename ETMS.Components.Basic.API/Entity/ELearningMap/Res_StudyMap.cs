

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// 学习地图表业务实体
    /// </summary>
    public partial class Res_StudyMap:AbstractObject
	{
        /// <summary>
        /// 课程数
        /// </summary>
        public int CourseNum { get; set; }
        /// <summary>
        /// 资料数
        /// </summary>
        public int DataNum { get; set; }
	}
}
