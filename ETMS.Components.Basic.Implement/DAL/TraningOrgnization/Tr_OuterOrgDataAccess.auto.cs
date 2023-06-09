//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-3-15 15:40:47.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TraningOrgnization;

namespace ETMS.Components.Basic.Implement.DAL.TraningOrgnization
{
    /// <summary>
    /// 外部培训机构表数据访问
    /// </summary>
    public partial class Tr_OuterOrgDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Tr_OuterOrg tr_OuterOrg)
        {
            string commandName = "dbo.Pr_Tr_OuterOrg_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OuterOrgCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@OuterOrgName", SqlDbType.NVarChar, 200),
					new SqlParameter("@OuterOrgStatus", SqlDbType.Int),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar, 100),
					new SqlParameter("@LinkMode", SqlDbType.NVarChar, 100),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar, 256),
					new SqlParameter("@CommonPlace", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrgAssess", SqlDbType.NVarChar, -1),
					new SqlParameter("@ServiceContent", SqlDbType.NVarChar, -1),
					new SqlParameter("@BestCourse", SqlDbType.NVarChar, -1),
					new SqlParameter("@HistoryCooperation", SqlDbType.NVarChar, -1),
					new SqlParameter("@ContractModal", SqlDbType.NVarChar, -1),
					new SqlParameter("@ContractURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OuterOrgAddr", SqlDbType.NVarChar, 256),
					new SqlParameter("@OuterOrgURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@IsCollaborate", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = tr_OuterOrg.OuterOrgID;
            if (tr_OuterOrg.OuterOrgCode != null) { parms[1].Value = tr_OuterOrg.OuterOrgCode; } else { parms[1].Value = DBNull.Value; }
            if (tr_OuterOrg.OuterOrgName != null) { parms[2].Value = tr_OuterOrg.OuterOrgName; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = tr_OuterOrg.OuterOrgStatus;
            if (tr_OuterOrg.LinkMan != null) { parms[4].Value = tr_OuterOrg.LinkMan; } else { parms[4].Value = DBNull.Value; }
            if (tr_OuterOrg.LinkMode != null) { parms[5].Value = tr_OuterOrg.LinkMode; } else { parms[5].Value = DBNull.Value; }
            if (tr_OuterOrg.EMAIL != null) { parms[6].Value = tr_OuterOrg.EMAIL; } else { parms[6].Value = DBNull.Value; }
            if (tr_OuterOrg.CommonPlace != null) { parms[7].Value = tr_OuterOrg.CommonPlace; } else { parms[7].Value = DBNull.Value; }
            if (tr_OuterOrg.OrgAssess != null) { parms[8].Value = tr_OuterOrg.OrgAssess; } else { parms[8].Value = DBNull.Value; }
            if (tr_OuterOrg.ServiceContent != null) { parms[9].Value = tr_OuterOrg.ServiceContent; } else { parms[9].Value = DBNull.Value; }
            if (tr_OuterOrg.BestCourse != null) { parms[10].Value = tr_OuterOrg.BestCourse; } else { parms[10].Value = DBNull.Value; }
            if (tr_OuterOrg.HistoryCooperation != null) { parms[11].Value = tr_OuterOrg.HistoryCooperation; } else { parms[11].Value = DBNull.Value; }
            if (tr_OuterOrg.ContractModal != null) { parms[12].Value = tr_OuterOrg.ContractModal; } else { parms[12].Value = DBNull.Value; }
            if (tr_OuterOrg.ContractURL != null) { parms[13].Value = tr_OuterOrg.ContractURL; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = tr_OuterOrg.CreateTime;
            parms[15].Value = tr_OuterOrg.CreateUserID;
            if (tr_OuterOrg.CreateUser != null) { parms[16].Value = tr_OuterOrg.CreateUser; } else { parms[16].Value = DBNull.Value; }
            parms[17].Value = tr_OuterOrg.ModifyTime;
            if (tr_OuterOrg.ModifyUser != null) { parms[18].Value = tr_OuterOrg.ModifyUser; } else { parms[18].Value = DBNull.Value; }
            if (tr_OuterOrg.Remark != null) { parms[19].Value = tr_OuterOrg.Remark; } else { parms[19].Value = DBNull.Value; }
            parms[20].Value = tr_OuterOrg.OrgID;
            if (tr_OuterOrg.OuterOrgAddr != null) { parms[21].Value = tr_OuterOrg.OuterOrgAddr; } else { parms[21].Value = DBNull.Value; }
            if (tr_OuterOrg.OuterOrgURL != null) { parms[22].Value = tr_OuterOrg.OuterOrgURL; } else { parms[22].Value = DBNull.Value; }
            parms[23].Value = tr_OuterOrg.IsCollaborate;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid outerOrgID)
        {
            string commandName = "dbo.Pr_Tr_OuterOrg_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = outerOrgID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Tr_OuterOrg tr_OuterOrg)
        {
            string commandName = "dbo.Pr_Tr_OuterOrg_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OuterOrgCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@OuterOrgName", SqlDbType.NVarChar, 200),
					new SqlParameter("@OuterOrgStatus", SqlDbType.Int),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar, 100),
					new SqlParameter("@LinkMode", SqlDbType.NVarChar, 100),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar, 256),
					new SqlParameter("@CommonPlace", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrgAssess", SqlDbType.NVarChar, -1),
					new SqlParameter("@ServiceContent", SqlDbType.NVarChar, -1),
					new SqlParameter("@BestCourse", SqlDbType.NVarChar, -1),
					new SqlParameter("@HistoryCooperation", SqlDbType.NVarChar, -1),
					new SqlParameter("@ContractModal", SqlDbType.NVarChar, -1),
					new SqlParameter("@ContractURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OuterOrgAddr", SqlDbType.NVarChar, 256),
					new SqlParameter("@OuterOrgURL", SqlDbType.NVarChar, 256),
					new SqlParameter("@IsCollaborate", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = tr_OuterOrg.OuterOrgID;
            if (tr_OuterOrg.OuterOrgCode != null) { parms[1].Value = tr_OuterOrg.OuterOrgCode; } else { parms[1].Value = DBNull.Value; }
            if (tr_OuterOrg.OuterOrgName != null) { parms[2].Value = tr_OuterOrg.OuterOrgName; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = tr_OuterOrg.OuterOrgStatus;
            if (tr_OuterOrg.LinkMan != null) { parms[4].Value = tr_OuterOrg.LinkMan; } else { parms[4].Value = DBNull.Value; }
            if (tr_OuterOrg.LinkMode != null) { parms[5].Value = tr_OuterOrg.LinkMode; } else { parms[5].Value = DBNull.Value; }
            if (tr_OuterOrg.EMAIL != null) { parms[6].Value = tr_OuterOrg.EMAIL; } else { parms[6].Value = DBNull.Value; }
            if (tr_OuterOrg.CommonPlace != null) { parms[7].Value = tr_OuterOrg.CommonPlace; } else { parms[7].Value = DBNull.Value; }
            if (tr_OuterOrg.OrgAssess != null) { parms[8].Value = tr_OuterOrg.OrgAssess; } else { parms[8].Value = DBNull.Value; }
            if (tr_OuterOrg.ServiceContent != null) { parms[9].Value = tr_OuterOrg.ServiceContent; } else { parms[9].Value = DBNull.Value; }
            if (tr_OuterOrg.BestCourse != null) { parms[10].Value = tr_OuterOrg.BestCourse; } else { parms[10].Value = DBNull.Value; }
            if (tr_OuterOrg.HistoryCooperation != null) { parms[11].Value = tr_OuterOrg.HistoryCooperation; } else { parms[11].Value = DBNull.Value; }
            if (tr_OuterOrg.ContractModal != null) { parms[12].Value = tr_OuterOrg.ContractModal; } else { parms[12].Value = DBNull.Value; }
            if (tr_OuterOrg.ContractURL != null) { parms[13].Value = tr_OuterOrg.ContractURL; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = tr_OuterOrg.CreateTime;
            parms[15].Value = tr_OuterOrg.CreateUserID;
            if (tr_OuterOrg.CreateUser != null) { parms[16].Value = tr_OuterOrg.CreateUser; } else { parms[16].Value = DBNull.Value; }
            parms[17].Value = tr_OuterOrg.ModifyTime;
            if (tr_OuterOrg.ModifyUser != null) { parms[18].Value = tr_OuterOrg.ModifyUser; } else { parms[18].Value = DBNull.Value; }
            if (tr_OuterOrg.Remark != null) { parms[19].Value = tr_OuterOrg.Remark; } else { parms[19].Value = DBNull.Value; }
            parms[20].Value = tr_OuterOrg.OrgID;
            if (tr_OuterOrg.OuterOrgAddr != null) { parms[21].Value = tr_OuterOrg.OuterOrgAddr; } else { parms[21].Value = DBNull.Value; }
            if (tr_OuterOrg.OuterOrgURL != null) { parms[22].Value = tr_OuterOrg.OuterOrgURL; } else { parms[22].Value = DBNull.Value; }
            parms[23].Value = tr_OuterOrg.IsCollaborate;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Tr_OuterOrg GetById(Guid outerOrgID)
        {
            Tr_OuterOrg tr_OuterOrg = null;

            string commandName = "dbo.Pr_Tr_OuterOrg_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = outerOrgID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    tr_OuterOrg = PopulateTr_OuterOrgFromDataReader(dataReader);
                }
            }

            return tr_OuterOrg;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_OuterOrg_GetPagedList";
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
        public IList<Tr_OuterOrg> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Tr_OuterOrg> list = new List<Tr_OuterOrg>();
            string commandName = "dbo.Pr_Tr_OuterOrg_GetPagedList";
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
                    list.Add(PopulateTr_OuterOrgFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }

        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Tr_OuterOrg PopulateTr_OuterOrgFromDataReader(SqlDataReader reader)
        {
            Tr_OuterOrg tr_OuterOrg = new Tr_OuterOrg();

            int outerOrgIDIndex = reader.GetOrdinal("OuterOrgID");
            if (!reader.IsDBNull(outerOrgIDIndex))
            {
                tr_OuterOrg.OuterOrgID = reader.GetGuid(outerOrgIDIndex);
            }

            int outerOrgCodeIndex = reader.GetOrdinal("OuterOrgCode");
            if (!reader.IsDBNull(outerOrgCodeIndex))
            {
                tr_OuterOrg.OuterOrgCode = reader.GetString(outerOrgCodeIndex);
            }

            int outerOrgNameIndex = reader.GetOrdinal("OuterOrgName");
            if (!reader.IsDBNull(outerOrgNameIndex))
            {
                tr_OuterOrg.OuterOrgName = reader.GetString(outerOrgNameIndex);
            }

            int outerOrgStatusIndex = reader.GetOrdinal("OuterOrgStatus");
            if (!reader.IsDBNull(outerOrgStatusIndex))
            {
                tr_OuterOrg.OuterOrgStatus = reader.GetInt32(outerOrgStatusIndex);
            }

            int linkManIndex = reader.GetOrdinal("LinkMan");
            if (!reader.IsDBNull(linkManIndex))
            {
                tr_OuterOrg.LinkMan = reader.GetString(linkManIndex);
            }

            int linkModeIndex = reader.GetOrdinal("LinkMode");
            if (!reader.IsDBNull(linkModeIndex))
            {
                tr_OuterOrg.LinkMode = reader.GetString(linkModeIndex);
            }

            int eMAILIndex = reader.GetOrdinal("EMAIL");
            if (!reader.IsDBNull(eMAILIndex))
            {
                tr_OuterOrg.EMAIL = reader.GetString(eMAILIndex);
            }

            int commonPlaceIndex = reader.GetOrdinal("CommonPlace");
            if (!reader.IsDBNull(commonPlaceIndex))
            {
                tr_OuterOrg.CommonPlace = reader.GetString(commonPlaceIndex);
            }

            int orgAssessIndex = reader.GetOrdinal("OrgAssess");
            if (!reader.IsDBNull(orgAssessIndex))
            {
                tr_OuterOrg.OrgAssess = reader.GetString(orgAssessIndex);
            }

            int serviceContentIndex = reader.GetOrdinal("ServiceContent");
            if (!reader.IsDBNull(serviceContentIndex))
            {
                tr_OuterOrg.ServiceContent = reader.GetString(serviceContentIndex);
            }

            int bestCourseIndex = reader.GetOrdinal("BestCourse");
            if (!reader.IsDBNull(bestCourseIndex))
            {
                tr_OuterOrg.BestCourse = reader.GetString(bestCourseIndex);
            }

            int historyCooperationIndex = reader.GetOrdinal("HistoryCooperation");
            if (!reader.IsDBNull(historyCooperationIndex))
            {
                tr_OuterOrg.HistoryCooperation = reader.GetString(historyCooperationIndex);
            }

            int contractModalIndex = reader.GetOrdinal("ContractModal");
            if (!reader.IsDBNull(contractModalIndex))
            {
                tr_OuterOrg.ContractModal = reader.GetString(contractModalIndex);
            }

            int contractURLIndex = reader.GetOrdinal("ContractURL");
            if (!reader.IsDBNull(contractURLIndex))
            {
                tr_OuterOrg.ContractURL = reader.GetString(contractURLIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                tr_OuterOrg.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            int createUserIDIndex = reader.GetOrdinal("CreateUserID");
            if (!reader.IsDBNull(createUserIDIndex))
            {
                tr_OuterOrg.CreateUserID = reader.GetInt32(createUserIDIndex);
            }

            int createUserIndex = reader.GetOrdinal("CreateUser");
            if (!reader.IsDBNull(createUserIndex))
            {
                tr_OuterOrg.CreateUser = reader.GetString(createUserIndex);
            }

            int modifyTimeIndex = reader.GetOrdinal("ModifyTime");
            if (!reader.IsDBNull(modifyTimeIndex))
            {
                tr_OuterOrg.ModifyTime = reader.GetDateTime(modifyTimeIndex);
            }

            int modifyUserIndex = reader.GetOrdinal("ModifyUser");
            if (!reader.IsDBNull(modifyUserIndex))
            {
                tr_OuterOrg.ModifyUser = reader.GetString(modifyUserIndex);
            }

            int remarkIndex = reader.GetOrdinal("Remark");
            if (!reader.IsDBNull(remarkIndex))
            {
                tr_OuterOrg.Remark = reader.GetString(remarkIndex);
            }

            int orgIDIndex = reader.GetOrdinal("OrgID");
            if (!reader.IsDBNull(orgIDIndex))
            {
                tr_OuterOrg.OrgID = reader.GetInt32(orgIDIndex);
            }

            int outerOrgAddrIndex = reader.GetOrdinal("OuterOrgAddr");
            if (!reader.IsDBNull(outerOrgAddrIndex))
            {
                tr_OuterOrg.OuterOrgAddr = reader.GetString(outerOrgAddrIndex);
            }

            int outerOrgURLIndex = reader.GetOrdinal("OuterOrgURL");
            if (!reader.IsDBNull(outerOrgURLIndex))
            {
                tr_OuterOrg.OuterOrgURL = reader.GetString(outerOrgURLIndex);
            }

            int isCollaborateIndex = reader.GetOrdinal("IsCollaborate");
            if (!reader.IsDBNull(isCollaborateIndex))
            {
                tr_OuterOrg.IsCollaborate = reader.GetInt32(isCollaborateIndex);
            }

            return tr_OuterOrg;
        }
    }
}
