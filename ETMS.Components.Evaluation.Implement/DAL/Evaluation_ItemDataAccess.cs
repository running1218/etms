
using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Evaluation.API.Entity;

namespace ETMS.Components.Evaluation.Implement.DAL
{
    /// <summary>
    /// 评价项表数据访问
    /// </summary>
    public partial class Evaluation_ItemDataAccess
    {
        /// <summary>
        /// 获取指定评价量表的所有评价项
        /// </summary>
        public List<Evaluation_Item> GetByPlate(Guid plateID)
        {
            List<Evaluation_Item> evaluationItems = new List<Evaluation_Item>();

            string commandName = "dbo.Pr_Evaluation_Item_GetByPlate";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = plateID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    evaluationItems.Add(PopulateEvaluation_ItemFromDataReader(dataReader));
                }
            }
            return evaluationItems;
        }

    }
}
