using ETMS.AppContext.Component;

namespace ETMS.Components.Basic.API
{
    /// <summary>
    /// 费用接口
    /// 黄中福：2012－05－04
    /// </summary>
    public interface IFeeFacade : IComponent
    {

        /// <summary>
        /// 获取某个“讲师等级”对应“培训时间说明”的课酬标准
        /// </summary>
        /// <param name="trainingTimeDescID">培训时间说明ID</param>
        /// <param name="teacherLevelID">讲师等级ID</param>
        /// <returns></returns>
        decimal GetCourseFee(int trainingTimeDescID, int teacherLevelID);


    }
}
