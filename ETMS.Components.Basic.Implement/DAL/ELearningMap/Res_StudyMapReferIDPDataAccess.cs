//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-05-09 09:25:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity;

namespace ETMS.Components.Basic.Implement.DAL
{
    /// <summary>
    /// ѧϰ��ͼ��IDP�ǿγ����Ϲ�ϵ�����ݷ���
    /// </summary>
    public partial class Res_StudyMapReferIDPDataAccess
	{
        /// <summary>
        /// ��ȡѧϰ��ͼ�ǿγ̴�ѡ�����б�
        /// </summary>
        public DataTable GetMapDataChoseList(Res_StudyMapReferIDP entity)
        {
            string commandName = "dbo.Pr_Res_StudyMapReferIDP_GetMapDataChoseList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@StudyMapID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@DataCode", SqlDbType.NVarChar),
					new SqlParameter("@DataName", SqlDbType.NVarChar),
					new SqlParameter("@DataCotent", SqlDbType.NVarChar),
                    new SqlParameter("@DataOutline", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.OrgID;
            parms[1].Value = entity.StudyMapID;
            parms[2].Value = entity.DataCode;
            parms[3].Value = entity.DataName;
            parms[4].Value = entity.DataCotent;
            parms[5].Value = entity.DataOutline;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// ��ȡѧϰ��ͼ�ǿγ���ѡ�����б�
        /// </summary>
        public DataTable GetMapDataList(int organizationID, Guid studyMapID)
        {
            string commandName = "dbo.Pr_Res_StudyMapReferIDP_GetMapDataList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@StudyMapID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = studyMapID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }        
	}
}