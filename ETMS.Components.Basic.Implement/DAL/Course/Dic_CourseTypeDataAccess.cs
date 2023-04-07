
using System.Data;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.Course
{
    public class Dic_CourseTypeDataAccess
    {

        /// <summary>
        /// 获取全部课程类型
        /// </summary>
       /// <returns>返回：DataTable集合</returns>
        public DataTable GetCourseTypeList()
        {
            string commandName = "dbo.Pr_Dic_Sys_CourseType_GetAll";
            #region Parameters
           

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName).Tables[0];
            return dt;
        }	
    }
}
