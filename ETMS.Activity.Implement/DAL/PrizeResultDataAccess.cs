using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using ETMS.Activity.Entity;

namespace ETMS.Activity.Implement.DAL
{
    public partial class PrizeResultDataAccess
    {
        /// <summary>
        /// 获取项目奖区
        /// </summary>
        /// <param name="appraisalID"></param>
        /// <returns></returns>
        public DataTable GetPrizeRegion(Guid appraisalID) {
            string sql = @"select Activity_Siginup.RegionID,Activity_Dic_Region.RegionName,Activity_Dic_Region.OrderNo from Activity_Siginup 
	inner join Activity_Dic_Region on Activity_Siginup.RegionID=Activity_Dic_Region.RegionID
	inner join Activity_Production on Activity_Siginup.SiginupID=Activity_Production.SiginupID
	inner join Activity_PrizeResult on Activity_Production.ProductID=Activity_PrizeResult.ProductID
    where AppraisalID=@AppraisalID
    group by Activity_Siginup.RegionID,Activity_Dic_Region.RegionName,Activity_Dic_Region.OrderNo
    order by Activity_Dic_Region.OrderNo";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = appraisalID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        public DataTable GetPrizeRegion(Guid appraisalID,int regionID)
        {
            string sql = @"select Activity_Siginup.RegionID,Activity_Dic_Region.RegionName,Activity_Dic_Region.OrderNo from Activity_Siginup 
	inner join Activity_Dic_Region on Activity_Siginup.RegionID=Activity_Dic_Region.RegionID
	inner join Activity_Production on Activity_Siginup.SiginupID=Activity_Production.SiginupID
	inner join Activity_PrizeResult on Activity_Production.ProductID=Activity_PrizeResult.ProductID
    where AppraisalID=@AppraisalID and Activity_Siginup.RegionID=@RegionID 
    group by Activity_Siginup.RegionID,Activity_Dic_Region.RegionName,Activity_Dic_Region.OrderNo
    order by Activity_Dic_Region.OrderNo";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@RegionID", SqlDbType.Int)
                };
            parms[0].Value = appraisalID;
            parms[1].Value = regionID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取所在区名次
        /// </summary>
        /// <param name="appraisalID"></param>
        /// <param name="regionID"></param>
        /// <returns></returns>
        public DataTable GetPrizeResult(Guid appraisalID,int regionID,int total=0)
        {
            string sql = "select ";
            if (total > 0) {
                sql += " top " + total;
            }
            sql+= @" Activity_Production.ProductID,ProductName,[Name],PrizeName,Activity_Siginup.SiginupID,UserID,RegionID from Activity_PrizeResult
	inner join Activity_Dic_Prize on Activity_PrizeResult.PrizeID=Activity_Dic_Prize.PrizeID
	inner join Activity_Production on Activity_PrizeResult.ProductID=Activity_Production.ProductID
	inner join Activity_Siginup on Activity_Production.SiginupID=Activity_Siginup.SiginupID
	where AppraisalID=@AppraisalID and RegionID=@RegionID
order by Activity_Dic_Prize.OrderNo";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@RegionID", SqlDbType.Int)
                };
            parms[0].Value = appraisalID;
            parms[1].Value = regionID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        public int Insert(PrizeResult entity)
        {
            string sql = @"INSERT INTO [Activity_PrizeResult]([ReusltID],[ProductID],[PrizeID],[CreateTime],[CreatorID])
                                        VALUES(@ReusltID,@ProductID,@PrizeID,@CreateTime,@CreatorID)";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ReusltID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@PrizeID", SqlDbType.Int),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorID", SqlDbType.Int)
                };

            parms[0].Value = entity.ReusltID;
            parms[1].Value = entity.ProductID;
            parms[2].Value = entity.PrizeID;
            parms[3].Value = entity.CreateTime;
            parms[4].Value = entity.CreatorID;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public int Delete(Guid productionID)
        {
            string sql = @"DELETE FROM Activity_PrizeResult WHERE ProductID = @ProductID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = productionID;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }
    }
}
