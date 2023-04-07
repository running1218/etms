using System;

using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using System.Data;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem
{
    public partial class Tr_AppraiseDataAccess
    {
        public void Save(Tr_Appraise entity)
        {
            string commandName = "dbo.Pr_Tr_Appraise_Save";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@IsCheckCourse", SqlDbType.Bit),   
                    new SqlParameter("@CourseRate", SqlDbType.Int),
                    new SqlParameter("@IsCheckStudying", SqlDbType.Bit),
                    new SqlParameter("@StudyingRate", SqlDbType.Int),
                    new SqlParameter("@IsCheckActual", SqlDbType.Bit),
                    new SqlParameter("@ActualRate", SqlDbType.Int),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = entity.TrainingItemID;
            parms[1].Value = entity.IsCheckCourse;
            parms[2].Value = entity.CourseRate;
            parms[3].Value = entity.IsCheckStudying;
            parms[4].Value = entity.StudyingRate;
            parms[5].Value = entity.IsCheckActual;
            parms[6].Value = entity.ActualRate;

            parms[7].Value = entity.CreateTime;
            parms[8].Value = entity.CreateUserID;
            parms[9].Value = entity.CreateUser;
            parms[10].Value = entity.ModifyTime;
            parms[11].Value = entity.ModifyUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public DataTable GetById(Guid trainingItemID)
        {
            string commandName = "dbo.Pr_Tr_Appraise_GetByID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;

            #endregion
            DataTable data = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
       
            return data;
        }

        public DataTable GetStandardScore(Guid studentCourseID)
        {
            string commandName = @"Select [StudentCourseID]
                                  ,[StudentID]
                                  ,[TrainingItemID]
                                  ,[TrainingItemCourseID]
                                  ,[CourseID]
                                  ,[CourseResourceNum]
                                  ,[StudiedNum]
                                  ,[CourseProgress]
                                  ,[CourseScore]
                                  ,[TestingScore]
                                  ,[ActualScore] From Temp_StandardCalulate Where StudentCourseID = @StudentCourseID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, "GetStandardScore");
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@StudentCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, "GetStandardScore", parms);
            }

            parms[0].Value = studentCourseID;

            #endregion
            DataTable data = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];

            return data;
        }
    }
}
