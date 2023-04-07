using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.StudentCourse
{
    public partial class Sty_StudentCourseDataAccess
    {
#region 管理端调用

        /// <summary>
        /// 获取项目报名学员中没有选择相关培训项目课程的学员
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public DataTable GetStudentForNoSelectCourse(Guid trainingItemCourseID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetStudentForNoSelectCourse]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }


        /// <summary>
        /// 获取培训项目课程下学员总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public int GetItemCourseStudentNum(Guid trainingItemCourseID)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourse_GetStudentNum]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            #endregion
            return  (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 获取项目报名学员中已经选择相关培训项目课程的学员
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public DataTable GetStudentForSelectedCourse(Guid trainingItemCourseID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetStudentForSelectedCourse]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }


        /// <summary>
        /// 增加学员选课
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <param name="studentSignupIDList">项目学员报名编号列表</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户姓名</param>
        public void AddStudentSelectCourse(Guid trainingItemCourseID, string studentSignupIDList, int createUserID, string createUser)
        {
            string commandName = "[dbo].[Pr_Tr_Item_AddStudentSelectCourse]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@StudentSignupIDList", SqlDbType.NVarChar),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = studentSignupIDList;
            parms[2].Value = createUserID;
            parms[3].Value = createUser;
            #endregion

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 删除学员选课
        /// </summary>
        /// <param name="studentCourseList">学员选课编号列表</param>
        public void DeleteStudentSelectCourse(string studentCourseList)
        {
            string commandName = "[dbo].[Pr_Tr_Item_DeleteStudentSelectCourse]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@StudentCourseList", SqlDbType.NVarChar),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = studentCourseList;

            #endregion

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
 
        }
        
        /// <summary>
        /// 跟据项目ID删除项目课程下所有的学员
        /// </summary>
        /// <param name="TrainingItemID">项目ID</param>
        public void DeleteStudentCourseByTrainingItemID(Guid TrainingItemID)
        {
            string commandName = "[dbo].[Sty_StudentCourse_DelByTrainingItemID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = TrainingItemID;
            #endregion

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);        
        }


        /// <summary>
        /// 取消学员报名（前台）
        /// </summary>
        /// <param name="studentSignupID"></param>
        /// <param name="trainingItemCourseID"></param>
        public void CancelStudentSignCourse(Guid studentSignupID, Guid trainingItemCourseID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_DeleteStudentSignCourseByTrainingCourseIDAndStudentSignID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@StudentSignupID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = studentSignupID;
            parms[1].Value = trainingItemCourseID;

            #endregion

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }



        /// <summary>
        /// 查询某个培训项目课程可选课的已经报名学员ID，为学员选课用
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="type">--范围:
        /// --0：整个项目已经报名的学员
        /// --1：未在本项目选课且在其他项目也没有选课的学员
        /// --2：未在本项目选课的整个项目已经报名学员</param>
        /// <returns></returns>
        public DataTable GetNoSelectCourseStudentFromStudentSignup(Guid trainingItemCourseID, int type)
        {
            string commandName = "dbo.[Pr_Sty_StudentCourse_GetNoSelectCourseStudentFromStudentSignup]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@Type", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = type;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }



#endregion

#region 学员端调用
        /// <summary>
        /// 学员学习课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="topNum">返回记录数量</param>
        /// <returns></returns>
        public DataTable GetStudentCourseByUserID(int userID,int topNum)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetStudentCourseByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TopNum", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = topNum;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 学员学习课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="topNum">返回记录数量</param>
        /// <returns></returns>
        public DataTable GetStudentCourseByUserID(string CourseName, int userID, int topNum)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetStudentCourseSearchByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseName", SqlDbType.VarChar),
					new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TopNum", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = CourseName;
            parms[1].Value = userID;
            parms[2].Value = topNum;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 获取学员培训项目列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetTrainingItemListByUserID(int userID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetTrainingItemListByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
      
        }

        /// <summary>
        /// 获取所有机构培训项目列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetAllOrganizationTrainingItemList()
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetAllOrganizationTrainingItem]";           
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, null).Tables[0];

        }

        /// <summary>
        /// 培训项目获取学员课程成绩
        /// </summary>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <returns></returns>
        public DataTable GetCourseScoreByTrainingItemID(Guid trainingItemID)
        {
            string commandName = "[dbo].[Pr_Tr_Item_GetCourseScoreByTrainingItemID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
      

        }




        /// <summary>
        /// 获取学生的考试成绩列表
        /// from dbo.vw_StudentCourse a 
        /// inner join dbo.vw_ValidItemCourse b on a.TrainingItemCourseID=b.TrainingItemCourseID
        /// inner join dbo.vw_ValidStudent c on c.UserID = a.UserID
        /// inner join dbo.Res_Course d on d.CourseID = b.CourseID
        /// inner join dbo.Tr_Item e on e.TrainingItemID = b.TrainingItemID
        /// </summary>
        /// <param name="studentID">学生ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetStudentCourseScoreList(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Sty_StudentCourse_GetStudentScoreList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@studentID", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            parms[4].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }


        /// <summary>
        /// 获取某个学生的某个培训项目课程在线考试的次数
        /// </summary>
        /// <param name="studentID">学生ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetStudentCourseOnLineTestNum(int studentID, Guid trainingItemCourseID)
        {
            string commandName = string.Format("select COUNT(*) AS num from Ex_StudentOnlineTest where StudentID = '{0}' and trainingItemCourseID ='{1}'", studentID, trainingItemCourseID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);

        }



        /// <summary>
        /// 获取某个学生的所有课程学习资源
        /// </summary>
        /// <param name="studentID">学生ID</param>
        public DataTable GetStudentALLCourseResList(int studentID)
        {
            string commandName = "dbo.[Pr_Sty_StudentCourse_GetStudentCourseResList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@studentID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }



        /// <summary>
        /// 获取学生的某个培训项目课程的所有学习资源列表
        /// </summary>
        /// <param name="studentID">学生ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        public DataTable GetStudentCourseResListByTrainingItemCourseID(int studentID, Guid trainingItemCourseID)
        {
            string commandName = "dbo.[Pr_Sty_StudentCourse_GetStudentCourseResListByTrainingItemCourseID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@studentID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            parms[1].Value = trainingItemCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }



        /// <summary>
        /// 获取当前项目下学员选课信息
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseByTrainingItemIDAndUserID(Guid trainingItemID, int userID)
        {
            string commandName = "dbo.[Pr_Sty_StudentCourseByTrainingItemIDAndUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = userID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }




        #endregion
        /// <summary>
        /// 学员选课人数
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        public Int32 GetStudentCourseUserTotal(Guid CourseID)
        {
            string commandName = string.Format("select COUNT(1) from[dbo].[Sty_StudentCourse] where TrainingItemCourseID in(select TrainingItemCourseID from[dbo].[Tr_ItemCourse] where CourseID = '{0}')", CourseID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }

        
    }
}
