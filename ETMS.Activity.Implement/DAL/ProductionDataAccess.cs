using System;
using System.Data;
using ETMS.Activity.Entity;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Activity.Implement.DAL
{
    public partial class ProductionDataAccess
    {
        public DataTable GetMyProductions(int userID)
        {
            #region sql
            string sql = @" SELECT A.[ProductID],A.[SiginupID],A.[ProductCode],A.[ProductName],A.[Extention],A.[UploadTime],A.[Address]
	                            ,A.[ProductType],A.[Score],A.[Comment],A.[AppraiseTime],A.[Appraiser],A.[AppraiseStatus]
	                            ,C.TypeName
                            FROM Activity_Production A
                            INNER JOIN Activity_Siginup B ON A.SiginupID = B.SiginupID
                            LEFT JOIN Activity_Dic_ProductType C ON a.ProductType = C.ProductTypeID
                            WHERE B.UserID = @UserID
                            ORDER BY A.UploadTime DESC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                };

            parms[0].Value = userID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        /// <summary>
        /// 根据报名编号获取作品
        /// </summary>
        /// <param name="siginUpID"></param>
        /// <returns></returns>
        public DataTable GetProductions(Guid siginUpID)
        {
            #region sql
            string sql = @" SELECT A.[ProductID],A.[SiginupID],A.[ProductCode],A.[ProductName],A.[Extention],A.[UploadTime],A.[Address]
	                            ,A.[ProductType],A.[Score],A.[Comment],A.[AppraiseTime],A.[Appraiser],A.[AppraiseStatus]
                                ,A.TransStatus,A.TransFilePath, A.TransTime
	                            ,C.TypeName
                            FROM Activity_Production A
                            INNER JOIN Activity_Siginup B ON A.SiginupID = B.SiginupID
                            LEFT JOIN Activity_Dic_ProductType C ON a.ProductType = C.ProductTypeID
                            WHERE B.SiginupID = @SiginupID
                            ORDER BY A.UploadTime DESC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@SiginupID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = siginUpID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetProduction(Guid id)
        {
            #region sql
            string sql = @" SELECT A.[ProductID],A.[SiginupID],A.[ProductCode],A.[ProductName],A.[Extention],A.[UploadTime],A.[Address]
	                            ,A.[ProductType],A.[Score],A.[Comment],A.[AppraiseTime],A.[Appraiser],A.[AppraiseStatus]
                                ,A.[TransStatus],A.[TransFilePath], A.[TransTime],A.[IsExcellent]
                            FROM Activity_Production A                           
                            WHERE ProductID = @ProductID";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = id;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public int Insert(Production product)
        {
            string sql = @"INSERT INTO [Activity_Production]
		                        ([ProductID],[SiginupID],[ProductCode],[ProductName],[Extention],[UploadTime],[Address],[ProductType]
		                        ,[AppraiseStatus],[TransFilePath])
                            VALUES(@ProductID,@SiginupID,@ProductCode,@ProductName,@Extention,@UploadTime,@Address
                                    ,@ProductType,@AppraiseStatus,@Address)";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@SiginupID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Extention", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UploadTime", SqlDbType.DateTime),
                    new SqlParameter("@Address", SqlDbType.NVarChar, 200),
                    new SqlParameter("@ProductType", SqlDbType.Int),
                    new SqlParameter("@AppraiseStatus", SqlDbType.Int)
                };

            parms[0].Value = product.ProductID;
            parms[1].Value = product.SiginupID;
            parms[2].Value = product.ProductCode;
            parms[3].Value = product.ProductName;
            parms[4].Value = product.Extention;
            parms[5].Value = product.UploadTime;
            parms[6].Value = product.Address;
            parms[7].Value = product.ProductType;
            parms[8].Value = product.AppraiseStatus;          

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public int Update(Production product)
        {
            string sql = @"UPDATE Activity_Production
                            SET ProductName = @ProductName
	                            ,Extention = @Extention
	                            ,UploadTime = @UploadTime
	                            ,[Address] = @Address
                                ,[TransStatus] = @TransStatus
                                ,[TransFilePath] = @TransFilePath
                                ,[TransTime] = @TransTime
                            WHERE ProductID = @ProductID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Extention", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UploadTime", SqlDbType.DateTime),
                    new SqlParameter("@Address", SqlDbType.NVarChar, 200),
                    new SqlParameter("@TransStatus", SqlDbType.Int),
                    new SqlParameter("@TransFilePath", SqlDbType.NVarChar, 200),
                    new SqlParameter("@TransTime", SqlDbType.DateTime)
                };

            parms[0].Value = product.ProductID;
            parms[1].Value = product.ProductName;
            parms[2].Value = product.Extention;
            parms[3].Value = product.UploadTime;
            parms[4].Value = product.Address;
            parms[5].Value = product.TransStatus;
            parms[6].Value = product.TransFilePath;
            if (product.TransTime == null)
            {
                parms[7].Value = DBNull.Value;
            }
            else
            {
                parms[7].Value = product.TransTime;
            }
            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public int Mark(Production product)
        {
            string sql = @"UPDATE Activity_Production
                            SET Score = @Score
	                            ,Comment = @Comment
	                            ,Appraiser = @Appraiser
	                            ,AppraiseTime = @AppraiseTime
                                ,AppraiseStatus = @AppraiseStatus
                            WHERE ProductID = @ProductID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@Score", SqlDbType.Decimal),
                    new SqlParameter("@Comment", SqlDbType.NVarChar, 400),
                    new SqlParameter("@Appraiser", SqlDbType.Int),
                    new SqlParameter("@AppraiseTime", SqlDbType.DateTime),
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@AppraiseStatus", SqlDbType.Int)
                };

            parms[0].Value = product.Score;
            parms[1].Value = product.Comment;
            parms[2].Value = product.Appraiser;
            parms[3].Value = product.AppraiseTime;
            parms[4].Value = product.ProductID;
            parms[5].Value = product.AppraiseStatus;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public int Delete(Guid productionID)
        {
            string sql = @"DELETE FROM Activity_Production WHERE ProductID = @ProductID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier)
                };

            parms[0].Value = productionID;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public DataTable GetMarkingStatics(Guid apprisialID)
        {
            #region sql
            string sql = @" SELECT COUNT(A.SiginupID) AS SubmitNum,SUM(AppraiseStatus) AS MarkingNum
                            FROM Activity_Production A
                            INNER JOIN Activity_Siginup B ON A.SiginupID = B.SiginupID
                            WHERE B.AppraisalID= @AppraisalID";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = apprisialID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetMarkingDetailStatics(Guid apprisialID)
        {
            #region sql
            string sql = @" SELECT B.AppraisalID, D.GroupID, D.GroupName, A.ProductType, E.TypeName, COUNT(A.SiginupID) AS SubmitNum,SUM(AppraiseStatus) AS MarkingNum
                            FROM Activity_Production A
                            INNER JOIN Activity_Siginup B ON A.SiginupID = B.SiginupID
                            LEFT JOIN Activity_Dic_Group D ON B.GroupID = D.GroupID
                            LEFT JOIN Activity_Dic_ProductType E ON A.ProductType = E.ProductTypeID
                            WHERE B.AppraisalID=@AppraisalID
                            GROUP BY B.AppraisalID, D.GroupID, D.GroupName, A.ProductType, E.TypeName";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = apprisialID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetProductions(Guid apprisialID, int groupID, int typeID)
        {
            #region sql
            string sql = @" SELECT B.SiginupNo, B.SiginupID,A.ProductID, ProductName,Extention, [Address], Score, Comment, AppraiseStatus
                            FROM Activity_Production A
                            INNER JOIN Activity_Siginup B ON a.SiginupID = b.SiginupID
                            WHERE b.AppraisalID = @AppraisalID 
	                            AND (B.GroupID = @GroupID OR @GroupID = 0)
	                            AND (A.ProductType = @ProductType OR @ProductType = 0)
                            ORDER BY A.AppraiseStatus ASC,A.UploadTime ASC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@GroupID", SqlDbType.Int),
                    new SqlParameter("@ProductType", SqlDbType.Int)
                };

            parms[0].Value = apprisialID;
            parms[1].Value = groupID;
            parms[2].Value = typeID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetShowProductions(Guid apprisialID, int regionID,int pageSize)
        {
            #region sql
            string sql = @"select ";
            if (pageSize > 0) {
                sql += " top " + pageSize;
            }
            sql += @" [Name],ProductName,UploadTime,Extention,Address,TransFilePath,TransStatus,TransTime from Activity_Siginup
    inner join Activity_Production on Activity_Siginup.SiginupID = Activity_Production.SiginupID
where IsExcellent=1 and RegionID = @RegionID and AppraisalID = @AppraisalID
order by UploadTime desc";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@RegionID", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int)
                };

            parms[0].Value = apprisialID;
            parms[1].Value = regionID;
            parms[2].Value = pageSize;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];

        }

        public DataTable GetProductionsByAppraisalID(Guid appraisalID, string productionName)
        {
            #region sql
            string sql = @" SELECT A.[ProductID],A.[SiginupID],A.[ProductCode],A.[ProductName],A.[Extention],A.[UploadTime],A.[Address]
	                            ,A.[ProductType],A.[Score],A.[Comment],A.[AppraiseTime],A.[Appraiser],A.[AppraiseStatus]
                                ,A.[TransStatus],A.[TransFilePath], A.[TransTime],A.[IsExcellent]
                                ,C.RegionName, D.GroupName, B.Name, B.SiginupNo
                            FROM Activity_Production A   
							INNER JOIN Activity_Siginup B ON a.SiginupID = B.SiginupID
                            LEFT JOIN Activity_Dic_Region C ON b.RegionID = C.RegionID
							LEFT JOIN Activity_Dic_Group D ON B.GroupID = D.GroupID                        
                            WHERE B.AppraisalID = @AppraisalID
                                AND A.ProductName LIKE '%' + @ProductName + '%'
							ORDER BY A.Score DESC, A.UploadTime ASC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 50)
                };

            parms[0].Value = appraisalID;
            parms[1].Value = productionName;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetPrizeByAppraisalID(Guid appraisalID, string productionName)
        {
            #region sql
            string sql = @" SELECT A.[ProductID],A.[SiginupID],A.[ProductCode],A.[ProductName],A.[Extention],A.[UploadTime],A.[Address]
	                            ,A.[ProductType],A.[Score],A.[Comment],A.[AppraiseTime],A.[Appraiser],A.[AppraiseStatus]
                                ,A.[TransStatus],A.[TransFilePath], A.[TransTime],A.[IsExcellent]
                                ,C.RegionName, D.GroupName, B.Name, B.SiginupNo
                                ,F.PrizeName
                            FROM Activity_Production A   
							INNER JOIN Activity_Siginup B ON a.SiginupID = B.SiginupID
                            LEFT JOIN Activity_Dic_Region C ON b.RegionID = C.RegionID
							LEFT JOIN Activity_Dic_Group D ON B.GroupID = D.GroupID 
                            LEFT JOIN Activity_PrizeResult E ON A.ProductID = E.ProductID
							LEFT JOIN Activity_Dic_Prize F ON E.PrizeID = F.PrizeID                            
                            WHERE B.AppraisalID = @AppraisalID
                                AND A.ProductName LIKE '%' + @ProductName + '%'
							ORDER BY A.Score DESC, A.UploadTime ASC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar, 50)
                };

            parms[0].Value = appraisalID;
            parms[1].Value = productionName;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetPrizeByProductionID(Guid productionID)
        {
            #region sql
            string sql = @" SELECT A.[ProductID],A.[SiginupID],A.[ProductCode],A.[ProductName],A.[Extention],A.[UploadTime],A.[Address]
	                            ,A.[ProductType],A.[Score],A.[Comment],A.[AppraiseTime],A.[Appraiser],A.[AppraiseStatus]
                                ,A.[TransStatus],A.[TransFilePath], A.[TransTime],A.[IsExcellent]
                                ,C.RegionName, D.GroupName, B.Name, B.SiginupNo
                                ,F.PrizeName
                            FROM Activity_Production A   
							INNER JOIN Activity_Siginup B ON a.SiginupID = B.SiginupID
                            LEFT JOIN Activity_Dic_Region C ON b.RegionID = C.RegionID
							LEFT JOIN Activity_Dic_Group D ON B.GroupID = D.GroupID 
                            LEFT JOIN Activity_PrizeResult E ON A.ProductID = E.ProductID
							LEFT JOIN Activity_Dic_Prize F ON E.PrizeID = F.PrizeID                            
                            WHERE A.ProductID = @ProductID";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("ProductID", SqlDbType.UniqueIdentifier)
                };

            parms[0].Value = productionID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public int SetExcellent(Guid productionID, int isExcellent)
        {
            string sql = @"UPDATE Activity_Production SET IsExcellent = @IsExcellent WHERE ProductID = @ProductID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier)
                    ,new SqlParameter("@IsExcellent", SqlDbType.Int)
                };

            parms[0].Value = productionID;
            parms[1].Value = isExcellent;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }


    }
}
