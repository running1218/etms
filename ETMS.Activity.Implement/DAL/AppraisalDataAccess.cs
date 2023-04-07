using System;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Utility;
using ETMS.Activity.Entity;

namespace ETMS.Activity.Implement.DAL
{
    public partial class AppraisalDataAccess
    {
        public DataTable GetPageList(int organizationID, string title, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, out int totalRecords)
        {
            #region sql
            string sql = @";WITH CTE AS           
                            (          
                                SELECT A.[AppraisalID],A.[OrganizationID],A.[AppraisalTitle],A.[TypeID],A.[ShapeID],A.[Province]
			                            ,A.[City],A.[Address],A.[BeginTime],A.[EndTime],A.[ImageUrl],A.[LimitNum],A.[Abstract],A.[Details]
			                            ,A.[ReviewRule],A.[Status],A.[IsTop],A.[Region],A.[Group],A.[CreateTime],A.[Creator],A.[Modifior],A.[ModifyTime]
			                            ,B.ShapeName, C.TypeName
                                ,ROW_NUMBER() OVER(ORDER BY A.CreateTime  desc) AS [Sequency]          
                                ,COUNT(*) OVER(PARTITION BY '') AS [TotalRecords]     
                                From Activity_Appraisal A
                                LEFT JOIN Activity_Dic_Shape B ON A.ShapeID = B.ShapeID
                                LEFT JOIN Activity_Dic_Type C ON A.TypeID = C.TypeID 
                                Where AppraisalTitle Like '%' + @AppraisalTitle + '%' 
                                AND  ((A.BeginTime >= @BeginTime And A.EndTime <= @EndTime)  
                                    Or (A.EndTime >= @BeginTime And A.EndTime <= @EndTime)  
                                    Or (A.BeginTime < @BeginTime And A.EndTime > @EndTime)  
                                    )  
                                AND A.OrganizationID = @OrganizationID     
                            )     
                            SELECT *        
                            FROM CTE CB 
                            WHERE [Sequency] BETWEEN (@PageIndex - 1 ) * @PageSize + 1 AND @PageIndex * @PageSize         
                            ORDER BY [Sequency]";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@AppraisalTitle", SqlDbType.NVarChar),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = title;
            parms[3].Value = beginTime;
            parms[4].Value = endTime;
            parms[5].Value = organizationID;
            #endregion

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            totalRecords = dt.Rows.Count > 0 ? dt.Rows[0]["TotalRecords"].ToInt() : 0;
            return dt;
        }

        public int Insert(Appraisal entity)
        {
            string sql = @"INSERT INTO [dbo].[Activity_Appraisal]
	                        ([AppraisalID],[OrganizationID],[AppraisalTitle],[TypeID],[ShapeID],[Province],[City]
	                        ,[Address],[BeginTime],[EndTime],[ImageUrl],[LimitNum],[Abstract],[Details],[ReviewRule]
	                        ,[Status],[IsTop],[Region],[Group],[CreateTime],[Creator],[Modifior],[ModifyTime])
                             VALUES(@AppraisalID,@OrganizationID,@AppraisalTitle,@TypeID,@ShapeID,@Province,@City
	                        ,@Address,@BeginTime,@EndTime,@ImageUrl,@LimitNum,@Abstract,@Details,@ReviewRule
	                        ,@Status,@IsTop,@Region,@Group,@CreateTime,@Creator,@Modifior,@ModifyTime)";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@AppraisalTitle", SqlDbType.NVarChar, 50),
                    new SqlParameter("@TypeID", SqlDbType.Int),
                    new SqlParameter("@ShapeID", SqlDbType.Int),
                    new SqlParameter("@Province", SqlDbType.NVarChar, 50),
                    new SqlParameter("@City", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Address", SqlDbType.NVarChar, 200),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200),
                    new SqlParameter("@LimitNum", SqlDbType.Int),
                    new SqlParameter("@Abstract", SqlDbType.NVarChar, 500),
                    new SqlParameter("@Details", SqlDbType.NVarChar),
                    new SqlParameter("@ReviewRule", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@IsTop", SqlDbType.Bit),
                    new SqlParameter("@Region", SqlDbType.NVarChar, 1024),
                    new SqlParameter("@Group", SqlDbType.NVarChar, 2014),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@Creator", SqlDbType.Int),
                    new SqlParameter("@Modifior", SqlDbType.Int),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                };

            parms[0].Value = entity.AppraisalID;
            parms[1].Value = entity.OrganizationID;
            parms[2].Value = entity.AppraisalTitle;
            parms[3].Value = entity.TypeID;
            parms[4].Value = entity.ShapeID;
            parms[5].Value = entity.Province;
            parms[6].Value = entity.City;
            parms[7].Value = entity.Address;
            parms[8].Value = entity.BeginTime;
            parms[9].Value = entity.EndTime;
            parms[10].Value = entity.ImageUrl;
            parms[11].Value = entity.LimitNum;
            parms[12].Value = entity.Abstract;
            parms[13].Value = entity.Details;
            parms[14].Value = entity.ReviewRule;
            parms[15].Value = entity.Status;
            parms[16].Value = entity.IsTop;
            parms[17].Value = entity.Region;
            parms[18].Value = entity.Group;
            parms[19].Value = entity.CreateTime;
            parms[20].Value = entity.Creator;
            parms[21].Value = entity.Modifior;
            parms[22].Value = entity.ModifyTime;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public int Update(Appraisal entity)
        {
            string sql = @"UPDATE Activity_Appraisal
                            SET OrganizationID = @OrganizationID
	                            ,AppraisalTitle = @AppraisalTitle
	                            ,TypeID = @TypeID
	                            ,ShapeID = @ShapeID
	                            ,Province = @Province
	                            ,City = @City
	                            ,[Address] = @Address
	                            ,BeginTime = @BeginTime
	                            ,EndTime = @EndTime
	                            ,ImageUrl = @ImageUrl
	                            ,LimitNum = @LimitNum
	                            ,Abstract = @Abstract
	                            ,Details = @Details
	                            ,ReviewRule = @ReviewRule
	                            ,[Status] = @Status
	                            ,IsTop = @IsTop
	                            ,Region = @Region
	                            ,[Group] = @Group
	                            ,Modifior = @Modifior
	                            ,ModifyTime = @ModifyTime
                            WHERE AppraisalID = @AppraisalID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@AppraisalTitle", SqlDbType.NVarChar, 50),
                    new SqlParameter("@TypeID", SqlDbType.Int),
                    new SqlParameter("@ShapeID", SqlDbType.Int),
                    new SqlParameter("@Province", SqlDbType.NVarChar, 50),
                    new SqlParameter("@City", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Address", SqlDbType.NVarChar, 200),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200),
                    new SqlParameter("@LimitNum", SqlDbType.Int),
                    new SqlParameter("@Abstract", SqlDbType.NVarChar, 500),
                    new SqlParameter("@Details", SqlDbType.NVarChar),
                    new SqlParameter("@ReviewRule", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@IsTop", SqlDbType.Bit),
                    new SqlParameter("@Region", SqlDbType.NVarChar, 1024),
                    new SqlParameter("@Group", SqlDbType.NVarChar, 2014),
                    new SqlParameter("@Modifior", SqlDbType.Int),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                };

            parms[0].Value = entity.AppraisalID;
            parms[1].Value = entity.OrganizationID;
            parms[2].Value = entity.AppraisalTitle;
            parms[3].Value = entity.TypeID;
            parms[4].Value = entity.ShapeID;
            parms[5].Value = entity.Province;
            parms[6].Value = entity.City;
            parms[7].Value = entity.Address;
            parms[8].Value = entity.BeginTime;
            parms[9].Value = entity.EndTime;
            parms[10].Value = entity.ImageUrl;
            parms[11].Value = entity.LimitNum;
            parms[12].Value = entity.Abstract;
            parms[13].Value = entity.Details;
            parms[14].Value = entity.ReviewRule;
            parms[15].Value = entity.Status;
            parms[16].Value = entity.IsTop;
            parms[17].Value = entity.Region;
            parms[18].Value = entity.Group;
            parms[19].Value = entity.Modifior;
            parms[20].Value = entity.ModifyTime;

            #endregion

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public DataTable GetAppraisalByID(Guid appraisalID)
        {
            #region sql
            string sql = @" SELECT [AppraisalID],[OrganizationID],[AppraisalTitle],[TypeID],[ShapeID],[Province]
			                        ,[City],[Address],[BeginTime],[EndTime],[ImageUrl],[LimitNum],[Abstract],[Details]
			                        ,[ReviewRule],[Status],[IsTop],[Region],[Group],[CreateTime],[Creator],[Modifior],[ModifyTime]			                               
                            From Activity_Appraisal                               
                            Where AppraisalID = @AppraisalID";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = appraisalID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public int Delete(Guid appraisalID)
        {
            string sql = @"DELETE FROM Activity_Appraisal WHERE AppraisalID = @AppraisalID";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
            };
            parms[0].Value = appraisalID;
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, parms);
        }

        public DataTable IsSingnUp(Guid appraisalID)
        {
            #region sql
            string sql = @" SELECT COUNT(1) AS Num
                            FROM Activity_Siginup
                            WHERE AppraisalID = @AppraisalID
	                            AND SiginupStatus = 1";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };

            parms[0].Value = appraisalID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public int CancelOthersTop()
        {
            string sql = @"UPDATE Activity_Appraisal SET IsTop = 0 WHERE IsTop = 1";
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql, null);
        }

        public DataTable GetTopx(int top, int orgID)
        {
            #region sql
            string sql = @" SELECT TOP {0} [AppraisalID],[OrganizationID],[AppraisalTitle],[TypeID],[ShapeID],[Province]
			                        ,[City],[Address],[BeginTime],[EndTime],[ImageUrl],[LimitNum],[Abstract],[Details]
			                        ,[ReviewRule],[Status],[IsTop],[Region],[Group],[CreateTime],[Creator],[Modifior],[ModifyTime]
                            FROM Activity_Appraisal
                            WHERE [Status]=1
                                AND OrganizationID = @OrganizationID
                            ORDER BY IsTop DESC, BeginTime DESC";
            sql = string.Format(sql, top);
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                };

            parms[0].Value = orgID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        /// <summary>
        /// 活动列表页（学生端）
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable GetAppraisalList(int orgID)
        {
            #region sql
            string sql = @" SELECT [AppraisalID],[OrganizationID],[AppraisalTitle],[TypeID],[ShapeID],[Province]
			                        ,[City],[Address],[BeginTime],[EndTime],[ImageUrl],[LimitNum],[Abstract],[Details]
			                        ,[ReviewRule],[Status],[IsTop],[Region],[Group],[CreateTime],[Creator],[Modifior],[ModifyTime]
                            FROM Activity_Appraisal
                            WHERE [Status]=1
                                AND OrganizationID = @OrganizationID
                            ORDER BY IsTop DESC, BeginTime DESC";
            #endregion

            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                };

            parms[0].Value = orgID;
            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetAppraisal(Guid appraisalID)
        {
            string sql = @"select AppraisalTitle,ImageUrl,Abstract,BeginTime,EndTime,LimitNum,Details,ReviewRule 
,Region,[Group]
,t.TypeName
,shape.ShapeName
,area.AreaName as Province
,area1.AreaName as City
,[Address]
from Activity_Appraisal a
inner join Activity_Dic_Type t on a.TypeID=t.TypeID
inner join Activity_Dic_Shape shape on a.ShapeID=shape.ShapeID
left join Dic_Sys_Area area on a.Province=area.AreaCode
left join Dic_Sys_Area area1 on a.City=area.AreaCode
where a.AppraisalID=@AppraisalID";
            #region Parameters
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AppraisalID", SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = appraisalID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];

            //dt.Columns.Add("RegionName");
            //dt.Columns.Add("GroupName");
            //string regions = dt.Rows[0]["Region"] != null ? (dt.Rows[0]["Region"].ToString().Substring(1, dt.Rows[0]["Region"].ToString().Length - 2).Replace("\"", "'")):"";

            //DataTable dtRegion = null;
            //if (regions != null) {
            //    dtRegion = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, "select RegionName from Activity_Dic_Region where Status=1 and RegionID in (" + regions + ") order by OrderNo", parms).Tables[0];
            //    for (int i = 0; i < dtRegion.Rows.Count; i++)
            //    {

            //    }
            //}


            return dt;
        }

        public DataTable GetDicRegion(string Ids)
        {
            string sql = @"select RegionID,RegionName from Activity_Dic_Region where Status=1 and RegionID in (" + Ids + ") order by OrderNo";
            #region Parameters
            //SqlParameter[] parms = new SqlParameter[] {
            //        new SqlParameter("@Regions", SqlDbType.VarChar),
            //    };
            //parms[0].Value = Ids;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetDicGroup(string Ids)
        {
            string sql = @"select GroupID,GroupName from Activity_Dic_Group where Status=1 and GroupID in (" + Ids + ") order by OrderNo";
            #region Parameters
            //SqlParameter[] parms = new SqlParameter[] {
            //        new SqlParameter("@Groups", SqlDbType.VarChar),
            //    };
            //parms[0].Value = Ids;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetDicRegionList()
        {
            string sql = @"select RegionID,RegionName from Activity_Dic_Region where Status=1 order by OrderNo";
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetDicGroupList()
        {
            string sql = @"select GroupID,GroupName from Activity_Dic_Group where Status=1 order by OrderNo";
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }


    }
}
