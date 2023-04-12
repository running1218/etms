//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-05-02 11:00:37.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Mentor.Implement.DAL.Mentor
{
    /// <summary>
    /// ��ʦ�����ݷ���
    /// </summary>
    public partial class Site_MentorDataAccess
	{
		/// <summary>
		/// �Ѿ���Ϊ��ʦ���б�
		/// </summary>
        public DataTable GetMentorList(int organizationID, string realName, string department, string post, int isUse)
		{
            string commandName = "dbo.Pr_Site_Mentor_GetMentorList";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@RealName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@PostName", SqlDbType.NVarChar),
					new SqlParameter("@IsUse", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = organizationID;
			parms[1].Value = realName;
			parms[2].Value = department;
			parms[3].Value = post;
            parms[4].Value = isUse;
			#endregion
			DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
			return dt;
		}

        /// <summary>
        /// ��ȡ�����뵼ʦ��ѧԱ�б�
        /// </summary>
        public DataTable ChoseMentorList(int organizationID, string realName, string department, string post)
        {
            string commandName = "dbo.Pr_Site_Mentor_ChoseMentorList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@organizationID", SqlDbType.Int),
					new SqlParameter("@realName", SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@PostName", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = realName;
            parms[2].Value = department;
            parms[3].Value = post;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }






	}
}