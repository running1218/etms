using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.TrainingItem.Student;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Student
{
    public partial class Sty_StudentSignupDataAccess
    {

        //取数据列表
        string  sqlModalStudentInfo = @"select
            Site_Student.WorkerNo,Site_Student.PostID,Site_Student.RankID,Site_Student.JoinTime
            ,Site_User.SexTypeID, Site_User.PoliticsTypeID,Site_User.Email, Site_User.MobilePhone, Site_User.[Identity]
            ,Site_User.LoginName,Site_User.RealName,Site_User.DepartmentID,Site_User.OrganizationID
            ,Sty_StudentSignup.*,Site_Organization.OrganizationCode,Site_Organization.OrganizationName,Site_Department.DepartmentName,Dic_Post.PostName,Dic_Sys_Rank.RankName
            from Sty_StudentSignup
            inner join  Site_Student on Site_Student.UserID = Sty_StudentSignup.UserID
            inner join  Site_User on Site_User.UserID = Site_Student.UserID
            left  join  Site_Organization ON Site_User.OrganizationID=Site_Organization.OrganizationID
			left  join Site_Department ON Site_User.DepartmentID=Site_Department.DepartmentID
			left join Dic_Post ON Site_Student.PostID=Dic_Post.PostID
			left join Dic_Sys_Rank on Site_Student.RankID=Dic_Sys_Rank.RankID
            where 1=1 {0} 
             ";
        string orderBySQL = "order by Sty_StudentSignup.CreateTime DESC";


        /// <summary>
        /// 获取某个培训项目的学员列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentListByTrainingItemID(Guid trainingItemID,int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            string conditionSQL = " AND Sty_StudentSignup.TrainingItemID = '" + trainingItemID.ToString() + "'";
            string sortExpression = " Sty_StudentSignup.CreateTime DESC ";
            criteria += conditionSQL;
            return GetPagedList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目的学员列表,返回学员的基本信息
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentListALLByTrainingItemID(Guid trainingItemID,int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            //组合查询条件
            criteria += " AND Sty_StudentSignup.TrainingItemID = '" + trainingItemID.ToString() + "'";
            //先取记录数
            string sql = string.Format(sqlModalStudentInfo, criteria);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, "order by Site_User.OrganizationID, Site_User.LoginName ", out totalRecords);
        }


        /// <summary>
        /// 获取学员列表，用于项目学员添加  
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectStudentList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Tr_Item_GetNoSelectStudentList]";
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
        /// 培训项目报名
        /// </summary>
        /// <param name="signUpModeID">报名模式：</param>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <param name="userIDList">培训学员列表</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户</param>
        /// <param name="modifyTime">修改时间</param>
        /// <param name="modifyUser">修改用户</param>
        /// <param name="reMark">备注</param>
        public void AddStudentListToTrainingItem(SignupModeEnum signUpModeID, Guid trainingItemID, string userIDList, DateTime createTime, int createUserID, string createUser, DateTime modifyTime, string modifyUser, string reMark)
        {
            string commandName = "[dbo].[Pr_Tr_Item_AddStudentList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@SignupModeID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserIDList", SqlDbType.VarChar),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar, 64),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 64),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = signUpModeID;
            parms[1].Value = trainingItemID;
            parms[2].Value = userIDList;
            parms[3].Value = createTime;
            parms[4].Value = createUserID;
            parms[5].Value = createUser;
            parms[6].Value = modifyTime;
            parms[7].Value = modifyUser;
            parms[8].Value = reMark;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }


        /// <summary>
        /// 培训项目报名:按查询条件报名
        /// </summary>
        /// <param name="signUpModeID">报名模式：</param>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <param name="criteria">要添加的满足条件的学员，与AND 开头的查询条件</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户</param>
        /// <param name="reMark">备注</param>
        /// <returns>返回成功插入的学员数</returns>
        public int AddStudentListToTrainingItemBySQLCondition(SignupModeEnum signUpModeID, Guid trainingItemID, string criteria, DateTime createTime, int createUserID, string createUser, string reMark)
        {
            string sqlModal = @"
                 INSERT INTO [dbo].[Sty_StudentSignup]
                    ([StudentSignupID],[SignupModeID],[TrainingItemID],[UserID],[CreateUserID],[CreateTime],[CreateUser],[ModifyTime],[ModifyUser],[Remark])
                 SELECT NEWID(),'{0}','{1}',UserID,'{2}','{3}','{4}','{3}','{4}','{5}'
                 from [dbo].[vw_ValidStudent] " +
                " WHERE 1=1 " + criteria + " and UserID not in (select userID from dbo.Sty_StudentSignup where TrainingItemID='{1}')";
            string sql = string.Format(sqlModal, (int)signUpModeID, trainingItemID, createUserID, createTime, createUser, createTime, createUser, reMark);
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql);
        }



        /// <summary>
        /// 根据培训项目的学员总数
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns>学员总数</returns>
        public Int32 GetTrainingItemStudentTotal(Guid trainingItemID)
        {
            string commandName = string.Format("select COUNT(*) from Sty_StudentSignup where TrainingItemID='{0}'", trainingItemID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 根据学员ID，获取其报名的所有项目列表
        /// [Sty_StudentSignup] a
        /// Tr_Item b
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemListByStudentID(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetTrainingItemList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StudentID", SqlDbType.Int),
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
        /// 获取报名学员的所有选课列表
        /// FROM Sty_StudentCourse a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Tr_ItemCourse d on d.TrainingItemCourseID =a.TrainingItemCourseID
        /// INNER JOIN Res_Course e on e.CourseID = d.CourseID
        /// INNER JOIN Site_User u on u.UserID=b.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetCourseList]";
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
        /// 根据学员ID，获取其报名的所有项目的有考试记录的课程列表
        /// FROM Sty_StudentCourse a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Tr_ItemCourse d on d.TrainingItemCourseID =a.TrainingItemCourseID
        /// INNER JOIN Res_Course e on e.CourseID = d.CourseID
        /// INNER JOIN vw_ValidStudent f on f.UserID=b.UserID
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseExamList(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetCourseExamList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StudentID", SqlDbType.Int),
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
        /// 根据学员ID，获取学生某门选课的在线考试列表
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="studentCourseID">学员选课ID</param>
        /// <returns></returns>
        public DataTable GetStudentCourseOnLineTestLisByStudentID(int studentID, Guid studentCourseID)
        {

            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetStudentCourseOnLineTestList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@studentID", SqlDbType.Int),
					new SqlParameter("@studentCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            parms[1].Value = studentCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;

        }
        /// <summary>
        /// 根据学员ID，获取学生某门选课的在线考试列表
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="studentCourseID"></param>
        /// <param name="OnLineTestID"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseOnLineTestLisByTestID(int studentID, Guid studentCourseID,Guid OnLineTestID)
        {

            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetOnLineTestListByTestID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@studentID", SqlDbType.Int),
                    new SqlParameter("@studentCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OnLineTestID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            parms[1].Value = studentCourseID;
            parms[2].Value = OnLineTestID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;

        }


        /// <summary>
        /// 获取某个学员某个培训项目下，尚未选择的课程，且该课程的状态为“启用”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentNoSelectItemCourseListByTrainingItemID(int studentID, Guid trainingItemID, int pageIndex, int pageSize, string Crieria, out int totalRecords)
        {
            string sql = string.Format(@"select 
                            Tr_Item.ItemCode, Tr_Item.ItemName,Tr_Item.ItemStatus,Tr_Item.ItemBeginTime,Tr_Item.ItemEndTime
                            ,Res_Course.CourseCode, Res_Course.CourseName,Res_Course.CourseTypeID,Res_Course.ThumbnailURL
                            ,Sty_StudentSignup.StudentSignupID
                            ,Tr_ItemCourse.*
                        from Tr_ItemCourse
                            inner join Res_Course on Res_Course.CourseID = Tr_ItemCourse.CourseID
                            inner join Tr_Item on Tr_Item.TrainingItemID = Tr_ItemCourse.TrainingItemID
                            inner join Sty_StudentSignup on Sty_StudentSignup.TrainingItemID=Tr_ItemCourse.TrainingItemID
                        where 1=1 and Tr_ItemCourse.CourseStatus='1' 
                            and Tr_ItemCourse.TrainingItemID='{0}'
                            and Sty_StudentSignup.UserID='{1}'
                            and Tr_ItemCourse.TrainingItemCourseID not in (
                                select a.TrainingItemCourseID
                                from Sty_StudentCourse a
                                    inner join Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
                                where b.UserID='{1}'){2}", trainingItemID, studentID, Crieria);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, " order by Tr_ItemCourse.CourseBeginTime desc ", out totalRecords);
        }




        /// <summary>
        /// 获取所有学员项目报名的所有基本信息
        /// FROM Sty_StudentSignup b 
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Site_User u on u.UserID=b.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        /// LEFT JOIN (
        ///     select
        ///         c.ClassStudentID,c.StudentSignupID,c.UserID
        ///         ,b.ClassSubgroupID,b.ClassSubgroupName
        ///     from Sty_ClassSubgroupStudent a
        ///         inner join Sty_ClassSubgroup b on b.ClassSubgroupID = a.ClassSubgroupID
        ///         inner join Sty_ClassStudent c on c.ClassStudentID = a.ClassStudentID
        ///         inner join Sty_Class d on d.ClassID = c.ClassID
        ///         inner join Sty_StudentSignup e on e.StudentSignupID =c.StudentSignupID
        ///         ) j on j.UserID= s.UserID and j.StudentSignupID=b.StudentSignupID
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetStudentSignupAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetAllStudentInfoList]";
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
        /// 项目学员调整 启用或者停用学员状态(zhangsz Add 2012-05-14)
        /// </summary>
        /// <param name="studentSignupID">学员报名ID</param>
        /// <param name="isUse">启用或者停用</param>
        public void UpdateIsUseByStudentSignupID(Guid studentSignupID, int isUse)
        {
            string commandName = "[dbo].[Pr_Sty_StudentSignup_UpdateIsUseByStudentSignupID]";

            SqlParameter[] parms = new SqlParameter[] {
					new SqlParameter("@StudentSignupID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@IsUse", SqlDbType.Int)
				};

            parms[0].Value = studentSignupID;
            parms[1].Value = isUse;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }




        /// <summary>
        /// 获取某个培训项目课程未选课的所有学生列表
        /// from dbo.Sty_StudentSignup a
        /// INNER JOIN Site_User u on u.UserID=a.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectCourseAllInfoListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Sty_StudentSignup_GetNoSelectCourseAllInfoListByTrainingItemCourseID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
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
        /// 获取项目学员所在机构列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemOrganizationList(Guid trainingItemID)
        {
            string commandName = "Pr_Sty_StudentSignup_GetTrainingItemOrganization";
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

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, commandName, parms).Tables[0];
        }
        /// <summary>
        /// 课程中心学员选购课程时，自动创建学员报名表数据
        /// </summary>
        /// <param name="signUpModeID"></param>
        /// <param name="trainingItemID"></param>
        /// <param name="userID"></param>
        /// <param name="createTime"></param>
        /// <param name="createUserID"></param>
        /// <param name="createUser"></param>
        /// <param name="modifyTime"></param>
        /// <param name="modifyUser"></param>
        /// <param name="reMark"></param>
        public void AddStyStudentSignup(int signUpModeID, Guid trainingItemID,string trainingItemCourseID, int userID, DateTime createTime, int createUserID, string createUser, DateTime modifyTime, string modifyUser, string reMark)
        {
            string commandName = "Pr_Sty_StudentSignupAdd";

            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {               
                    new SqlParameter("@SignupModeID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemCourseIDs",SqlDbType.VarChar),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar, 64),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 64),
                    new SqlParameter("@Remark", SqlDbType.NVarChar)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = signUpModeID;
            parms[1].Value = trainingItemID;
            parms[2].Value = trainingItemCourseID;
            parms[3].Value = userID;
            parms[4].Value = createTime;
            parms[5].Value = createUserID;
            parms[6].Value = createUser;
            parms[7].Value = modifyTime;
            parms[8].Value = modifyUser;
            parms[9].Value = reMark;
            #endregion
           SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 学习档案
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetUserStudentArchives(int userID)
        {

            string commandName = "[dbo].[Pr_StudentStudyArchives]";
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
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;

        }
    }
}
