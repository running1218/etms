using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Evaluation.API.Entity;

namespace ETMS.Components.Evaluation.Implement.DAL
{
    /// <summary>
    /// 评价量表数据访问
    /// </summary>
    public partial class Evaluation_PlateDataAccess
    {
        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Evaluation_Plate GetByObjectType(int objectTypeID)
        {
            Evaluation_Plate evaluation_Plate = null;

            string commandName = "dbo.Pr_Evaluation_Plate_GetByObjectType";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ObjectTypeID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = objectTypeID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    evaluation_Plate = PopulateEvaluation_PlateFromDataReader(dataReader);
                }
            }

            return evaluation_Plate;
        }
    }
}
