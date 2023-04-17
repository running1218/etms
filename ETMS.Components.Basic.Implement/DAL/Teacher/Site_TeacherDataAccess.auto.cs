//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-3-15 17:28:33.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Teacher;

namespace ETMS.Components.Basic.Implement.DAL.Teacher
{
    /// <summary>
    /// 讲师表数据访问
    /// </summary>
    public partial class Site_TeacherDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Site_Teacher site_Teacher)
        {
            string commandName = "dbo.Pr_Site_Teacher_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@TeacherSourceID", SqlDbType.Int),
					new SqlParameter("@TeacherCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TeacherLevelID", SqlDbType.Int),
					new SqlParameter("@TeacherTypeID", SqlDbType.Int),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@ClassReward", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 10, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@ServiceEnterprise", SqlDbType.NVarChar, -1),
					new SqlParameter("@WorkExperience", SqlDbType.NVarChar, -1),
					new SqlParameter("@Expertise", SqlDbType.NVarChar, -1),
					new SqlParameter("@RepresentativeWorks", SqlDbType.NVarChar, -1),
					new SqlParameter("@TeacherBrief", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsCourseDesigner", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsCollaborate", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Teacher.TeacherID;
            parms[1].Value = site_Teacher.TeacherSourceID;
            if (site_Teacher.TeacherCode != null) { parms[2].Value = site_Teacher.TeacherCode; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = site_Teacher.OuterOrgID;
            parms[4].Value = site_Teacher.TeacherLevelID;
            parms[5].Value = site_Teacher.TeacherTypeID;
            parms[6].Value = site_Teacher.IsUse;
            parms[7].Value = site_Teacher.ClassReward;
            if (site_Teacher.ServiceEnterprise != null) { parms[8].Value = site_Teacher.ServiceEnterprise; } else { parms[8].Value = DBNull.Value; }
            if (site_Teacher.WorkExperience != null) { parms[9].Value = site_Teacher.WorkExperience; } else { parms[9].Value = DBNull.Value; }
            if (site_Teacher.Expertise != null) { parms[10].Value = site_Teacher.Expertise; } else { parms[10].Value = DBNull.Value; }
            if (site_Teacher.RepresentativeWorks != null) { parms[11].Value = site_Teacher.RepresentativeWorks; } else { parms[11].Value = DBNull.Value; }
            if (site_Teacher.TeacherBrief != null) { parms[12].Value = site_Teacher.TeacherBrief; } else { parms[12].Value = DBNull.Value; }
            parms[13].Value = site_Teacher.IsCourseDesigner;
            parms[14].Value = site_Teacher.CreateTime;
            parms[15].Value = site_Teacher.CreateUserID;
            if (site_Teacher.CreateUser != null) { parms[16].Value = site_Teacher.CreateUser; } else { parms[16].Value = DBNull.Value; }
            parms[17].Value = site_Teacher.ModifyTime;
            if (site_Teacher.ModifyUser != null) { parms[18].Value = site_Teacher.ModifyUser; } else { parms[18].Value = DBNull.Value; }
            if (site_Teacher.Remark != null) { parms[19].Value = site_Teacher.Remark; } else { parms[19].Value = DBNull.Value; }
            parms[20].Value = site_Teacher.IsCollaborate;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 teacherID)
        {
            string commandName = "dbo.Pr_Site_Teacher_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = teacherID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Site_Teacher site_Teacher)
        {
            string commandName = "dbo.Pr_Site_Teacher_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int),
					new SqlParameter("@TeacherSourceID", SqlDbType.Int),
					new SqlParameter("@TeacherCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TeacherLevelID", SqlDbType.Int),
					new SqlParameter("@TeacherTypeID", SqlDbType.Int),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@ClassReward", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 10, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@ServiceEnterprise", SqlDbType.NVarChar, -1),
					new SqlParameter("@WorkExperience", SqlDbType.NVarChar, -1),
					new SqlParameter("@Expertise", SqlDbType.NVarChar, -1),
					new SqlParameter("@RepresentativeWorks", SqlDbType.NVarChar, -1),
					new SqlParameter("@TeacherBrief", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsCourseDesigner", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsCollaborate", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Teacher.TeacherID;
            parms[1].Value = site_Teacher.TeacherSourceID;
            if (site_Teacher.TeacherCode != null) { parms[2].Value = site_Teacher.TeacherCode; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = site_Teacher.OuterOrgID;
            parms[4].Value = site_Teacher.TeacherLevelID;
            parms[5].Value = site_Teacher.TeacherTypeID;
            parms[6].Value = site_Teacher.IsUse;
            parms[7].Value = site_Teacher.ClassReward;
            if (site_Teacher.ServiceEnterprise != null) { parms[8].Value = site_Teacher.ServiceEnterprise; } else { parms[8].Value = DBNull.Value; }
            if (site_Teacher.WorkExperience != null) { parms[9].Value = site_Teacher.WorkExperience; } else { parms[9].Value = DBNull.Value; }
            if (site_Teacher.Expertise != null) { parms[10].Value = site_Teacher.Expertise; } else { parms[10].Value = DBNull.Value; }
            if (site_Teacher.RepresentativeWorks != null) { parms[11].Value = site_Teacher.RepresentativeWorks; } else { parms[11].Value = DBNull.Value; }
            if (site_Teacher.TeacherBrief != null) { parms[12].Value = site_Teacher.TeacherBrief; } else { parms[12].Value = DBNull.Value; }
            parms[13].Value = site_Teacher.IsCourseDesigner;
            parms[14].Value = site_Teacher.CreateTime;
            parms[15].Value = site_Teacher.CreateUserID;
            if (site_Teacher.CreateUser != null) { parms[16].Value = site_Teacher.CreateUser; } else { parms[16].Value = DBNull.Value; }
            parms[17].Value = site_Teacher.ModifyTime;
            if (site_Teacher.ModifyUser != null) { parms[18].Value = site_Teacher.ModifyUser; } else { parms[18].Value = DBNull.Value; }
            if (site_Teacher.Remark != null) { parms[19].Value = site_Teacher.Remark; } else { parms[19].Value = DBNull.Value; }
            parms[20].Value = site_Teacher.IsCollaborate;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Site_Teacher GetById(Int32 teacherID)
        {
            Site_Teacher site_Teacher = null;

            string commandName = "dbo.Pr_Site_Teacher_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TeacherID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = teacherID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    site_Teacher = PopulateSite_TeacherFromDataReader(dataReader);
                }
            }

            return site_Teacher;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Site_Teacher_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public IList<Site_Teacher> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Site_Teacher> list = new List<Site_Teacher>();
            string commandName = "dbo.Pr_Site_Teacher_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    list.Add(PopulateSite_TeacherFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }

        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Site_Teacher PopulateSite_TeacherFromDataReader(SqlDataReader reader)
        {
            Site_Teacher site_Teacher = new Site_Teacher();

            int teacherIDIndex = reader.GetOrdinal("TeacherID");
            if (!reader.IsDBNull(teacherIDIndex))
            {
                site_Teacher.TeacherID = reader.GetInt32(teacherIDIndex);
            }

            int teacherSourceIDIndex = reader.GetOrdinal("TeacherSourceID");
            if (!reader.IsDBNull(teacherSourceIDIndex))
            {
                site_Teacher.TeacherSourceID = reader.GetInt32(teacherSourceIDIndex);
            }

            int teacherCodeIndex = reader.GetOrdinal("TeacherCode");
            if (!reader.IsDBNull(teacherCodeIndex))
            {
                site_Teacher.TeacherCode = reader.GetString(teacherCodeIndex);
            }

            int outerOrgIDIndex = reader.GetOrdinal("OuterOrgID");
            if (!reader.IsDBNull(outerOrgIDIndex))
            {
                site_Teacher.OuterOrgID = reader.GetGuid(outerOrgIDIndex);
            }

            int teacherLevelIDIndex = reader.GetOrdinal("TeacherLevelID");
            if (!reader.IsDBNull(teacherLevelIDIndex))
            {
                site_Teacher.TeacherLevelID = reader.GetInt32(teacherLevelIDIndex);
            }

            int teacherTypeIDIndex = reader.GetOrdinal("TeacherTypeID");
            if (!reader.IsDBNull(teacherTypeIDIndex))
            {
                site_Teacher.TeacherTypeID = reader.GetInt32(teacherTypeIDIndex);
            }

            int isUseIndex = reader.GetOrdinal("IsUse");
            if (!reader.IsDBNull(isUseIndex))
            {
                site_Teacher.IsUse = reader.GetInt32(isUseIndex);
            }

            int classRewardIndex = reader.GetOrdinal("ClassReward");
            if (!reader.IsDBNull(classRewardIndex))
            {
                site_Teacher.ClassReward = reader.GetDecimal(classRewardIndex);
            }

            int serviceEnterpriseIndex = reader.GetOrdinal("ServiceEnterprise");
            if (!reader.IsDBNull(serviceEnterpriseIndex))
            {
                site_Teacher.ServiceEnterprise = reader.GetString(serviceEnterpriseIndex);
            }

            int workExperienceIndex = reader.GetOrdinal("WorkExperience");
            if (!reader.IsDBNull(workExperienceIndex))
            {
                site_Teacher.WorkExperience = reader.GetString(workExperienceIndex);
            }

            int expertiseIndex = reader.GetOrdinal("Expertise");
            if (!reader.IsDBNull(expertiseIndex))
            {
                site_Teacher.Expertise = reader.GetString(expertiseIndex);
            }

            int representativeWorksIndex = reader.GetOrdinal("RepresentativeWorks");
            if (!reader.IsDBNull(representativeWorksIndex))
            {
                site_Teacher.RepresentativeWorks = reader.GetString(representativeWorksIndex);
            }

            int teacherBriefIndex = reader.GetOrdinal("TeacherBrief");
            if (!reader.IsDBNull(teacherBriefIndex))
            {
                site_Teacher.TeacherBrief = reader.GetString(teacherBriefIndex);
            }

            int isCourseDesignerIndex = reader.GetOrdinal("IsCourseDesigner");
            if (!reader.IsDBNull(isCourseDesignerIndex))
            {
                site_Teacher.IsCourseDesigner = reader.GetInt32(isCourseDesignerIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                site_Teacher.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            int createUserIDIndex = reader.GetOrdinal("CreateUserID");
            if (!reader.IsDBNull(createUserIDIndex))
            {
                site_Teacher.CreateUserID = reader.GetInt32(createUserIDIndex);
            }

            int createUserIndex = reader.GetOrdinal("CreateUser");
            if (!reader.IsDBNull(createUserIndex))
            {
                site_Teacher.CreateUser = reader.GetString(createUserIndex);
            }

            int modifyTimeIndex = reader.GetOrdinal("ModifyTime");
            if (!reader.IsDBNull(modifyTimeIndex))
            {
                site_Teacher.ModifyTime = reader.GetDateTime(modifyTimeIndex);
            }

            int modifyUserIndex = reader.GetOrdinal("ModifyUser");
            if (!reader.IsDBNull(modifyUserIndex))
            {
                site_Teacher.ModifyUser = reader.GetString(modifyUserIndex);
            }

            int remarkIndex = reader.GetOrdinal("Remark");
            if (!reader.IsDBNull(remarkIndex))
            {
                site_Teacher.Remark = reader.GetString(remarkIndex);
            }

            int isCollaborateIndex = reader.GetOrdinal("IsCollaborate");
            if (!reader.IsDBNull(isCollaborateIndex))
            {
                site_Teacher.IsCollaborate = reader.GetInt32(isCollaborateIndex);
            }

            return site_Teacher;
        }
    }
}
