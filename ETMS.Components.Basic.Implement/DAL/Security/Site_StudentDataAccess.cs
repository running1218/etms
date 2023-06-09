//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-3-30 14:32:19.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.Implement.DAL.Security
{
    /// <summary>
    /// 学员信息(用户扩展表)数据访问
    /// </summary>
    public partial class Site_StudentDataAccess
	{

        /// <summary>
        /// 验证用户名是否存在(供WebService方法使用)
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="userName">用户名</param>
        /// <returns>true:存在，false:不存在</returns>
        public bool IsUserExist(string connectionString, string userName)
        {
            string commandName = string.Format("select COUNT(*) from Site_User where LoginName='{0}'", userName);
            Int32 count = (Int32)SqlHelper.ExecuteScalar(connectionString, CommandType.Text, commandName, null);
            if (count > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 注册用户账号(供WebService方法使用)
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        public void UserRegister(string connectionString, Site_Student site_Student)
        {
            //先添加用户的基本信息
            site_Student.UserID = AddUserBaseInfo(connectionString, site_Student);

            //再添加用户的扩展信息
            string commandName = "dbo.Pr_Site_Student_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(connectionString, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@WorkerNo", SqlDbType.VarChar, 20),
					new SqlParameter("@RankID", SqlDbType.Int),
					new SqlParameter("@PostID", SqlDbType.Int),
					new SqlParameter("@Superior", SqlDbType.NVarChar, 100),
					new SqlParameter("@LastEducation", SqlDbType.NVarChar, 100),
					new SqlParameter("@Specialty", SqlDbType.NVarChar, 100),
					new SqlParameter("@JoinTime", SqlDbType.DateTime)
					};
                SqlHelperParameterCache.CacheParameterSet(connectionString, commandName, parms);
            }

            parms[0].Value = site_Student.UserID;
            if (site_Student.WorkerNo != null) { parms[1].Value = site_Student.WorkerNo; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = site_Student.RankID;
            parms[3].Value = site_Student.PostID;
            if (site_Student.Superior != null) { parms[4].Value = site_Student.Superior; } else { parms[4].Value = DBNull.Value; }
            if (site_Student.LastEducation != null) { parms[5].Value = site_Student.LastEducation; } else { parms[5].Value = DBNull.Value; }
            if (site_Student.Specialty != null) { parms[6].Value = site_Student.Specialty; } else { parms[6].Value = DBNull.Value; }
            parms[7].Value = site_Student.JoinTime;
            #endregion
            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandName, parms);

        }



        /// <summary>
        /// 添加用户的基本信息(供WebService方法使用)
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="obj"></param>
        /// <returns>用户ID</returns>
        public int AddUserBaseInfo(string connectionString, object obj)
        {
            ETMS.Components.Basic.API.Entity.Security.User site_User = (ETMS.Components.Basic.API.Entity.Security.User)obj;
            string commandName = "dbo.Pr_Site_User_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(connectionString, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@PassWord", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@Telphone", SqlDbType.NVarChar, 100),
					new SqlParameter("@IsSysAccount", SqlDbType.Bit),
					new SqlParameter("@OfficeTelphone", SqlDbType.NVarChar, 40),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar, 40),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@DepartmentID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.NVarChar, 100),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 1000),
					new SqlParameter("@SexTypeID", SqlDbType.Int),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Identity", SqlDbType.NVarChar, 60),
					new SqlParameter("@PoliticsTypeID", SqlDbType.Int),
					new SqlParameter("@TitleName", SqlDbType.NVarChar, 200)
					};
                SqlHelperParameterCache.CacheParameterSet(connectionString, commandName, parms);
            }

            if (site_User.LoginName != null) { parms[1].Value = site_User.LoginName; } else { parms[1].Value = DBNull.Value; }
            if (site_User.RealName != null) { parms[2].Value = site_User.RealName; } else { parms[2].Value = DBNull.Value; }
            if (site_User.PassWord != null) { parms[3].Value = site_User.PassWord; } else { parms[3].Value = DBNull.Value; }
            if (site_User.Email != null) { parms[4].Value = site_User.Email; } else { parms[4].Value = DBNull.Value; }
            if (site_User.Telphone != null) { parms[5].Value = site_User.Telphone; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = site_User.IsSysAccount;
            if (site_User.OfficeTelphone != null) { parms[7].Value = site_User.OfficeTelphone; } else { parms[7].Value = DBNull.Value; }
            if (site_User.MobilePhone != null) { parms[8].Value = site_User.MobilePhone; } else { parms[8].Value = DBNull.Value; }
            if (site_User.Description != null) { parms[9].Value = site_User.Description; } else { parms[9].Value = DBNull.Value; }
            parms[10].Value = site_User.Status;
            parms[11].Value = site_User.OrganizationID;
            parms[12].Value = site_User.DepartmentID;
            if (site_User.Creator != null) { parms[13].Value = site_User.Creator; } else { parms[13].Value = DBNull.Value; }
            parms[14].Value = site_User.CreateTime;
            if (site_User.Modifier != null) { parms[15].Value = site_User.Modifier; } else { parms[15].Value = DBNull.Value; }
            parms[16].Value = site_User.ModifyTime;
            if (site_User.PhotoUrl != null) { parms[17].Value = site_User.PhotoUrl; } else { parms[17].Value = DBNull.Value; }
            parms[18].Value = site_User.SexTypeID;
            parms[19].Value = site_User.Birthday;
            if (site_User.Identity != null) { parms[20].Value = site_User.Identity; } else { parms[20].Value = DBNull.Value; }
            parms[21].Value = site_User.PoliticsTypeID;
            if (site_User.TitleName != null) { parms[22].Value = site_User.TitleName; } else { parms[22].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandName, parms);

            site_User.UserID = (Int32)parms[0].Value;
            return site_User.UserID;

        }







		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetManagePagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{
            string commandName = "dbo.Pr_Site_Student_GetManagePagedList";
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
			DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
			totalRecords = (int)parms[4].Value;
			return dt;
		}

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetUserStudentPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Site_UserStudent_GetPageList";
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
	}
}
