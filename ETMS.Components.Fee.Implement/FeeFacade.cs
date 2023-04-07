
using ETMS.Components.Basic.API;

using ETMS.Components.Fee.Implement.BLL;

using ETMS.AppContext.Component;

namespace ETMS.Components.Fee.Implement
{
    public class FeeFacade:DefaultComponent, IFeeFacade
    {





        #region IFeeFacade 成员



        /// <summary>
        /// 获取某个“讲师等级”对应“培训时间说明”的课酬标准
        /// </summary>
        /// <param name="trainingTimeDescID">培训时间说明ID</param>
        /// <param name="teacherLevelID">讲师等级ID</param>
        /// <returns>如果没有找到，则返回0</returns>
        public decimal GetCourseFee(int trainingTimeDescID, int teacherLevelID)
        {
            Fee_CourseFeeSettingLogic courseFeeLogic = new Fee_CourseFeeSettingLogic();
            string courseFee = courseFeeLogic.getTrainingTimeDesc(trainingTimeDescID, teacherLevelID);
            decimal retValue = 0;
            try
            {
                retValue = decimal.Parse(courseFee);
            }
            catch 
            {
            }
            return retValue;
        }

        #endregion




   }
}
