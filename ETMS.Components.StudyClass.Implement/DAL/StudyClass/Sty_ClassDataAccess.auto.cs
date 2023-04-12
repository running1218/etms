//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-04-19 20:30:48.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.StudyClass.API.Entity.StudyClass;

namespace ETMS.Components.StudyClass.Implement.DAL.StudyClass
{
    /// <summary>
    /// �༶�����ݷ���
    /// </summary>
    public partial class Sty_ClassDataAccess
    {
        /// <summary>
        /// ����
        /// </summary>
        public void Add(Sty_Class sty_Class)
        {
            string commandName = "dbo.Pr_Sty_Class_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ClassID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ClassDesc", SqlDbType.NVarChar, 2048),
					new SqlParameter("@StudentNum", SqlDbType.Int),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DelFlag", SqlDbType.Bit)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = sty_Class.ClassID;
            parms[1].Value = sty_Class.TrainingItemID;
            if (sty_Class.ClassName != null) { parms[2].Value = sty_Class.ClassName; } else { parms[2].Value = DBNull.Value; }
            if (sty_Class.ClassDesc != null) { parms[3].Value = sty_Class.ClassDesc; } else { parms[3].Value = DBNull.Value; }
            parms[4].Value = sty_Class.StudentNum;
            if (sty_Class.DutyUser != null) { parms[5].Value = sty_Class.DutyUser; } else { parms[5].Value = DBNull.Value; }
            if (sty_Class.TelPhone != null) { parms[6].Value = sty_Class.TelPhone; } else { parms[6].Value = DBNull.Value; }
            if (sty_Class.Email != null) { parms[7].Value = sty_Class.Email; } else { parms[7].Value = DBNull.Value; }
            parms[8].Value = sty_Class.CreateTime;
            parms[9].Value = sty_Class.CreateUserID;
            if (sty_Class.CreateUser != null) { parms[10].Value = sty_Class.CreateUser; } else { parms[10].Value = DBNull.Value; }
            parms[11].Value = sty_Class.ModifyTime;
            if (sty_Class.ModifyUser != null) { parms[12].Value = sty_Class.ModifyUser; } else { parms[12].Value = DBNull.Value; }
            if (sty_Class.Remark != null) { parms[13].Value = sty_Class.Remark; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = sty_Class.DelFlag;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void Remove(Guid classID)
        {
            string commandName = "dbo.Pr_Sty_Class_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ClassID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = classID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save(Sty_Class sty_Class)
        {
            string commandName = "dbo.Pr_Sty_Class_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ClassID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ClassDesc", SqlDbType.NVarChar, 2048),
					new SqlParameter("@StudentNum", SqlDbType.Int),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DelFlag", SqlDbType.Bit)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = sty_Class.ClassID;
            parms[1].Value = sty_Class.TrainingItemID;
            if (sty_Class.ClassName != null) { parms[2].Value = sty_Class.ClassName; } else { parms[2].Value = DBNull.Value; }
            if (sty_Class.ClassDesc != null) { parms[3].Value = sty_Class.ClassDesc; } else { parms[3].Value = DBNull.Value; }
            parms[4].Value = sty_Class.StudentNum;
            if (sty_Class.DutyUser != null) { parms[5].Value = sty_Class.DutyUser; } else { parms[5].Value = DBNull.Value; }
            if (sty_Class.TelPhone != null) { parms[6].Value = sty_Class.TelPhone; } else { parms[6].Value = DBNull.Value; }
            if (sty_Class.Email != null) { parms[7].Value = sty_Class.Email; } else { parms[7].Value = DBNull.Value; }
            parms[8].Value = sty_Class.CreateTime;
            parms[9].Value = sty_Class.CreateUserID;
            if (sty_Class.CreateUser != null) { parms[10].Value = sty_Class.CreateUser; } else { parms[10].Value = DBNull.Value; }
            parms[11].Value = sty_Class.ModifyTime;
            if (sty_Class.ModifyUser != null) { parms[12].Value = sty_Class.ModifyUser; } else { parms[12].Value = DBNull.Value; }
            if (sty_Class.Remark != null) { parms[13].Value = sty_Class.Remark; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = sty_Class.DelFlag;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ���ݱ�ʶ��ȡ����
        /// </summary>
        public Sty_Class GetById(Guid classID)
        {
            Sty_Class sty_Class = null;

            string commandName = "dbo.Pr_Sty_Class_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ClassID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = classID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    sty_Class = PopulateSty_ClassFromDataReader(dataReader);
                }
            }

            return sty_Class;
        }

        /// <summary>
        /// ���ݲ�����ȡ�����б�����ҳ��������
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Sty_Class_GetPagedList";
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
        /// ���ݲ�����ȡ�����б�����ҳ��������
        /// </summary>
        public IList<Sty_Class> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Sty_Class> list = new List<Sty_Class>();
            string commandName = "dbo.Pr_Sty_Class_GetPagedList";
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
                    list.Add(PopulateSty_ClassFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }

        /// <summary>
        /// ��DataReader�ж�ȡ���ݵ�ҵ�����
        /// </summary>
        private Sty_Class PopulateSty_ClassFromDataReader(SqlDataReader reader)
        {
            Sty_Class sty_Class = new Sty_Class();

            int classIDIndex = reader.GetOrdinal("ClassID");
            if (!reader.IsDBNull(classIDIndex))
            {
                sty_Class.ClassID = reader.GetGuid(classIDIndex);
            }

            int trainingItemIDIndex = reader.GetOrdinal("TrainingItemID");
            if (!reader.IsDBNull(trainingItemIDIndex))
            {
                sty_Class.TrainingItemID = reader.GetGuid(trainingItemIDIndex);
            }

            int classNameIndex = reader.GetOrdinal("ClassName");
            if (!reader.IsDBNull(classNameIndex))
            {
                sty_Class.ClassName = reader.GetString(classNameIndex);
            }

            int classDescIndex = reader.GetOrdinal("ClassDesc");
            if (!reader.IsDBNull(classDescIndex))
            {
                sty_Class.ClassDesc = reader.GetString(classDescIndex);
            }

            int studentNumIndex = reader.GetOrdinal("StudentNum");
            if (!reader.IsDBNull(studentNumIndex))
            {
                sty_Class.StudentNum = reader.GetInt32(studentNumIndex);
            }

            int dutyUserIndex = reader.GetOrdinal("DutyUser");
            if (!reader.IsDBNull(dutyUserIndex))
            {
                sty_Class.DutyUser = reader.GetString(dutyUserIndex);
            }

            int telPhoneIndex = reader.GetOrdinal("TelPhone");
            if (!reader.IsDBNull(telPhoneIndex))
            {
                sty_Class.TelPhone = reader.GetString(telPhoneIndex);
            }

            int emailIndex = reader.GetOrdinal("Email");
            if (!reader.IsDBNull(emailIndex))
            {
                sty_Class.Email = reader.GetString(emailIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                sty_Class.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            int createUserIDIndex = reader.GetOrdinal("CreateUserID");
            if (!reader.IsDBNull(createUserIDIndex))
            {
                sty_Class.CreateUserID = reader.GetInt32(createUserIDIndex);
            }

            int createUserIndex = reader.GetOrdinal("CreateUser");
            if (!reader.IsDBNull(createUserIndex))
            {
                sty_Class.CreateUser = reader.GetString(createUserIndex);
            }

            int modifyTimeIndex = reader.GetOrdinal("ModifyTime");
            if (!reader.IsDBNull(modifyTimeIndex))
            {
                sty_Class.ModifyTime = reader.GetDateTime(modifyTimeIndex);
            }

            int modifyUserIndex = reader.GetOrdinal("ModifyUser");
            if (!reader.IsDBNull(modifyUserIndex))
            {
                sty_Class.ModifyUser = reader.GetString(modifyUserIndex);
            }

            int remarkIndex = reader.GetOrdinal("Remark");
            if (!reader.IsDBNull(remarkIndex))
            {
                sty_Class.Remark = reader.GetString(remarkIndex);
            }

            int delFlagIndex = reader.GetOrdinal("DelFlag");
            if (!reader.IsDBNull(delFlagIndex))
            {
                sty_Class.DelFlag = reader.GetBoolean(delFlagIndex);
            }

            return sty_Class;
        }
    }
}