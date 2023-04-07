using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.DAL.Common;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Course
{
    public partial class Tr_ItemCourseDataAccess
    {
        //left join Tr_ItemCourseTeacher on Tr_ItemCourseTeacher.TrainingItemCourseID = Tr_ItemCourse.TrainingItemCourseID

        //培训项目课程列表查询语句，
        private static string sqlModal = @"select 
            Tr_Item.ItemCode, Tr_Item.ItemName,Tr_Item.ItemStatus,Tr_Item.ItemBeginTime,Tr_Item.ItemEndTime
            ,Res_Course.CourseCode, Res_Course.CourseName,Res_Course.CourseTypeID,Res_Course.ThumbnailURL,Res_Course.CourseModel,Res_Course.CourseIntroduction
            ,Tr_ItemCourse.*
            from Tr_ItemCourse
            inner join Res_Course on Res_Course.CourseID = Tr_ItemCourse.CourseID
            inner join Tr_Item on Tr_Item.TrainingItemID = Tr_ItemCourse.TrainingItemID
            where 1=1 {0}  ";

        string orderBySQL = "order by Tr_Item.CreateTime Desc,Tr_Item.ItemName,Tr_ItemCourse.OrderNum";


        //字段等于查询条件模板
        private static string fieldEqualModal = " AND ([{0}].[{1}] = '{2}') ";



        /// <summary>
        /// 增加项目课程，同时添加该课程应的已经启用的在线课件到项目课程的资源中
        /// </summary>
        public void AddItemCourseAndCourseware(Tr_ItemCourse tr_ItemCourse)
        {
            string commandName = "dbo.Pr_Tr_ItemCourse_AddItemCourseAndCourseware";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TeachModelID", SqlDbType.Int),
                    new SqlParameter("@CourseStatus", SqlDbType.Int),
                    new SqlParameter("@CourseBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@CourseEndTime", SqlDbType.DateTime),
                    new SqlParameter("@TrainingModelID", SqlDbType.Int),
                    new SqlParameter("@OuterOrgID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OuterOrgDutyUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@OuterOrgEMAIL", SqlDbType.NVarChar, 256),
                    new SqlParameter("@CourseAttrID", SqlDbType.Int),
                    new SqlParameter("@Score", SqlDbType.Int),
                    new SqlParameter("@CourseHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 8, 2, String.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@BudgetFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
                    new SqlParameter("@IsNeedApply", SqlDbType.Bit),
                    new SqlParameter("@IsPlanCourse", SqlDbType.Bit),
                    new SqlParameter("@IsLimit", SqlDbType.Bit),
                    new SqlParameter("@MaxNum", SqlDbType.Int),
                    new SqlParameter("@IsInputGrade", SqlDbType.Bit),
                    new SqlParameter("@IsIssueGrade", SqlDbType.Bit),
                    new SqlParameter("@GradeIssueTime", SqlDbType.DateTime),
                    new SqlParameter("@GradeIssueUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@DelFlag", SqlDbType.Bit),
                    new SqlParameter("@PassLine", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 6, 2, String.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@TotalScore", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 6, 2, String.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@IsIssueScore", SqlDbType.Bit),
                    new SqlParameter("@ScoreIssueUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@ScoreIssueTime", SqlDbType.DateTime),
                    new SqlParameter("@IsComputeScore", SqlDbType.Bit),
                    new SqlParameter("@ScoreComputeUser", SqlDbType.NVarChar, 128),
                    new SqlParameter("@ScoreComputeTime", SqlDbType.DateTime),
                    new SqlParameter("@OrderNum",SqlDbType.Int)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = tr_ItemCourse.TrainingItemCourseID;
            parms[1].Value = tr_ItemCourse.TrainingItemID;
            parms[2].Value = tr_ItemCourse.CourseID;
            parms[3].Value = tr_ItemCourse.TeachModelID;
            parms[4].Value = tr_ItemCourse.CourseStatus;
            parms[5].Value = tr_ItemCourse.CourseBeginTime;
            parms[6].Value = tr_ItemCourse.CourseEndTime;
            parms[7].Value = tr_ItemCourse.TrainingModelID;
            parms[8].Value = tr_ItemCourse.OuterOrgID;
            if (tr_ItemCourse.OuterOrgDutyUser != null) { parms[9].Value = tr_ItemCourse.OuterOrgDutyUser; } else { parms[9].Value = DBNull.Value; }
            if (tr_ItemCourse.OuterOrgEMAIL != null) { parms[10].Value = tr_ItemCourse.OuterOrgEMAIL; } else { parms[10].Value = DBNull.Value; }
            parms[11].Value = tr_ItemCourse.CourseAttrID;
            parms[12].Value = tr_ItemCourse.Score;
            parms[13].Value = tr_ItemCourse.CourseHours;
            parms[14].Value = tr_ItemCourse.BudgetFee;
            if (tr_ItemCourse.Remark != null) { parms[15].Value = tr_ItemCourse.Remark; } else { parms[15].Value = DBNull.Value; }
            parms[16].Value = tr_ItemCourse.IsNeedApply;
            parms[17].Value = tr_ItemCourse.IsPlanCourse;
            parms[18].Value = tr_ItemCourse.IsLimit;
            parms[19].Value = tr_ItemCourse.MaxNum;
            parms[20].Value = tr_ItemCourse.IsInputGrade;
            parms[21].Value = tr_ItemCourse.IsIssueGrade;
            parms[22].Value = tr_ItemCourse.GradeIssueTime;
            if (tr_ItemCourse.GradeIssueUser != null) { parms[23].Value = tr_ItemCourse.GradeIssueUser; } else { parms[23].Value = DBNull.Value; }
            parms[24].Value = tr_ItemCourse.CreateTime;
            parms[25].Value = tr_ItemCourse.CreateUserID;
            if (tr_ItemCourse.CreateUser != null) { parms[26].Value = tr_ItemCourse.CreateUser; } else { parms[26].Value = DBNull.Value; }
            parms[27].Value = tr_ItemCourse.ModifyTime;
            if (tr_ItemCourse.ModifyUser != null) { parms[28].Value = tr_ItemCourse.ModifyUser; } else { parms[28].Value = DBNull.Value; }
            parms[29].Value = tr_ItemCourse.DelFlag;
            parms[30].Value = tr_ItemCourse.PassLine;
            parms[31].Value = tr_ItemCourse.TotalScore;
            parms[32].Value = tr_ItemCourse.IsIssueScore;
            if (tr_ItemCourse.ScoreIssueUser != null) { parms[33].Value = tr_ItemCourse.ScoreIssueUser; } else { parms[33].Value = DBNull.Value; }
            parms[34].Value = tr_ItemCourse.ScoreIssueTime;
            parms[35].Value = tr_ItemCourse.IsComputeScore;
            if (tr_ItemCourse.ScoreComputeUser != null) { parms[36].Value = tr_ItemCourse.ScoreComputeUser; } else { parms[36].Value = DBNull.Value; }
            parms[37].Value = tr_ItemCourse.ScoreComputeTime;
            parms[38].Value = tr_ItemCourse.OrderNum;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }


        /// <summary>
        /// 删除项目课程，同时添加该项目课程的资源
        /// </summary>
        public void RemoveItemCourseAndCourseware(Guid trainingItemCourseID)
        {
            string commandName = "dbo.Pr_Tr_ItemCourse_DeleteItemCourseAndCourseware";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }




        /// <summary>
        /// 获取某个组织机构下的培训项目课程列表
        /// 按项目的创建时间倒序、项目名称、课程名称排序
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria">AND 打头的查询条件</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByOrgID(int orgID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            criteria += string.Format(fieldEqualModal, "Tr_Item", "OrgID", orgID);
            string sql = string.Format(sqlModal, criteria);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBySQL, out totalRecords);
        }





        /// <summary>
        /// 获取某个的培训项目课程列表
        /// </summary>
        /// <param name="TrainingItemID"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            string whereSQL = string.Format(fieldEqualModal, "Tr_ItemCourse", "TrainingItemCourseID", trainingItemCourseID);
            string sql = string.Format(sqlModal, whereSQL);
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql).Tables[0];
            return dt;
        }


        /// <summary>
        /// 获取培训项目下的课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, out int totalRecords)
        {
            string whereSQL = string.Format(fieldEqualModal, "Tr_ItemCourse", "trainingItemID", trainingItemID);
            string sql = string.Format(sqlModal, whereSQL);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBySQL, out totalRecords);
        }

        /// <summary>
        /// 获取培训项目下的课程列表，并区分必修、选修报名状态
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(int userID,Guid trainingItemID, int pageIndex, int pageSize, out int totalRecords)
        {
            string sqlModal1 = @"select 
            Tr_Item.ItemCode,Tr_Item.BudgetFee as ElectiveNumber, Tr_Item.ItemName,Tr_Item.ItemStatus,Tr_Item.ItemBeginTime,Tr_Item.ItemEndTime
            ,Res_Course.CourseCode, Res_Course.CourseName,Res_Course.CourseTypeID,Res_Course.ThumbnailURL,Res_Course.FocusCount
            ,Tr_ItemCourse.*,Sty_StudentCourse.StudentCourse,Sty_StudentSignup.StudentSignupID
            from Tr_ItemCourse
            inner join Res_Course on Res_Course.CourseID = Tr_ItemCourse.CourseID
            inner join Tr_Item on Tr_Item.TrainingItemID = Tr_ItemCourse.TrainingItemID
            inner join Sty_StudentSignup on Sty_StudentSignup.TrainingItemID=Tr_Item.TrainingItemID
            left join Sty_StudentCourse on Sty_StudentCourse.StudentSignupID=Sty_StudentSignup.StudentSignupID and Tr_ItemCourse.TrainingItemCourseID=Sty_StudentCourse.TrainingItemCourseID
            where Tr_ItemCourse.CourseStatus = 1 And Sty_StudentSignup.UserID = @UserID And Sty_StudentSignup.TrainingItemID = @TrainingItemID 
            order by Tr_Item.CreateTime Desc,Tr_Item.ItemName,Tr_ItemCourse.OrderNum ";

            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
                };

            parms[0].Value = userID;
            parms[1].Value = trainingItemID;

            //string fieldEqualModal1 = " AND ([{0}].[{1}] = '{2}') and UserID='{3}' ";
            //string whereSQL = string.Format(fieldEqualModal1, "Tr_ItemCourse", "trainingItemID", trainingItemID, userID);
            //string sql = string.Format(sqlModal1, whereSQL);
            //return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBySQL, out totalRecords);
            totalRecords = 0;
            var result = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sqlModal1, parms).Tables[0];
            totalRecords = result.Rows.Count;
            return result;
        }


        /// <summary>
        /// 获取培训项目下的课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="SortExpression">排序条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, out int totalRecords)
        {
            string commandName = "dbo.Pr_Tr_ItemCourse_GetPageList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@SortExpression", SqlDbType.NVarChar),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;
        }
        /// <summary>
        /// 根据用户ID获取所有培训项目下的课程列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemCourseLisListByUserID(int UserID)
        {
            string commandName = "dbo.Pr_Tr_Item_GetTrainingItemCourseListByUserID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 跟据项目课程ID修改排序号
        /// </summary>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByItemCourseID(Guid trainingItemCourseID, int orderNum)
        {
            string commandName = "dbo.Pr_Tr_ItemCourse_UpdateOrderNum";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrderNum",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = orderNum;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 统计培训项目下的课程数
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public int GetItemCourseCountByTrainingItemID(Guid trainingItemID)
        {
            int totalRecords = -1;
            DataTable dt = GetItemCourseListByTrainingItemID(trainingItemID, 1, 1, out totalRecords);

            return totalRecords;
        }



        /// <summary>
        /// 获取某个培训项目课程下的离线作业列表
        /// </summary>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetOffLineJobListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetOffLineJobListByTrainingItemCourseIDAndCondition(trainingItemCourseID, "", pageIndex, pageSize, out totalRecords);
        }



        /// <summary>
        /// 获取某个讲师可维护的培训项目课程列表
        /// 默认满足条件：
        /// 1.培训项目必须是：审核通过(也就是未“归档”)
        /// 2.项目是“已经发布”
        /// 3.项目和课程状态均是“启用”
        /// </summary>
        /// <param name="teacherID">讲师ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTeacherID(int teacherID, int pageIndex, int pageSize, out int totalRecords)
        {

            //培训项目课程列表查询语句，
            string sqlModal = @"select 
                Tr_Item.ItemCode, Tr_Item.ItemName,Tr_Item.ItemStatus,Tr_Item.ItemBeginTime,Tr_Item.ItemEndTime
                ,Res_Course.CourseCode, Res_Course.CourseName,Res_Course.CourseTypeID,Res_Course.ThumbnailURL
                ,Tr_ItemCourse.*
                from Tr_ItemCourse
                inner join Res_Course on Res_Course.CourseID = Tr_ItemCourse.CourseID
                inner join Tr_Item on Tr_Item.TrainingItemID = Tr_ItemCourse.TrainingItemID
                inner join Tr_ItemCourseTeacher on Tr_ItemCourseTeacher.TrainingItemCourseID = Tr_ItemCourse.TrainingItemCourseID
                where 1=1 {0}  ";

            string orderBySQL = "order by Tr_ItemCourse.CreateTime DESC";

            string whereSQL = string.Format(fieldEqualModal, "Tr_ItemCourseTeacher", "TeacherID", teacherID);
            whereSQL += " AND Tr_Item.ItemStatus = '20' AND Tr_Item.IsUse = '1' AND Tr_Item.IsIssue='1' AND Tr_ItemCourse.CourseStatus ='1'";
            string sql = string.Format(sqlModal, whereSQL);
            DataTable dt = GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBySQL, out totalRecords);
            return dt;
        }





        /// <summary>
        /// 获取某个用户ID（讲师对应的用户ID）的培训项目课程列表
        /// 默认满足条件：
        /// 1.培训项目必须是：审核通过
        /// 2.项目是“启用”
        /// 3.课程状态是“启用”
        /// 排序方式：项目开始时间倒序，课程开始时间倒序
        /// </summary>
        /// <param name="userID">用户ID(也就是TeacherID)</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByUserID(int userID, int pageIndex, int pageSize, out int totalRecords)
        {

            //培训项目课程列表查询语句，
            string sqlModal = @"select 
                Tr_Item.ItemCode, Tr_Item.ItemName,Tr_Item.ItemStatus,Tr_Item.ItemBeginTime,Tr_Item.ItemEndTime
                ,Res_Course.CourseCode, Res_Course.CourseName,Res_Course.CourseTypeID,Res_Course.ThumbnailURL
                ,Tr_ItemCourse.*
                from Tr_ItemCourse
                inner join Res_Course on Res_Course.CourseID = Tr_ItemCourse.CourseID
                inner join Tr_Item on Tr_Item.TrainingItemID = Tr_ItemCourse.TrainingItemID
                inner join Tr_ItemCourseTeacher on Tr_ItemCourseTeacher.TrainingItemCourseID = Tr_ItemCourse.TrainingItemCourseID
                where 1=1 {0}  ";

            string orderBySQL = "order by Tr_Item.ItemBeginTime desc,Tr_ItemCourse.CourseBeginTime desc";

            string whereSQL = string.Format(fieldEqualModal, "Tr_ItemCourseTeacher", "TeacherID", userID);
            whereSQL += " AND Tr_Item.ItemStatus = '20' AND Tr_Item.IsUse = '1' AND Tr_ItemCourse.CourseStatus ='1'";
            string sql = string.Format(sqlModal, whereSQL);
            DataTable dt = GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, orderBySQL, out totalRecords);
            return dt;
        }



        /// <summary>
        /// 获取某个培训项目课程下的离线作业列表
        /// </summary>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <param name="conditionSQL">以AND开头的查询条件</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetOffLineJobListByTrainingItemCourseIDAndCondition(Guid trainingItemCourseID, string conditionSQL, int pageIndex, int pageSize, out int totalRecords)
        {
            string sqlModalOffLineJob = @"
                select 
                    Tr_Item.TrainingItemID, Tr_Item.ItemCode, Tr_Item.ItemName
                    ,Res_Course.CourseID, Res_Course.CourseCode, Res_Course.CourseName
                    ,Res_ItemCourseOffLineJob.TrainingItemCourseID,Res_ItemCourseOffLineJob.ItemCourseOffLineJobID
                    , Res_e_OffLineJob.*
                from Res_ItemCourseOffLineJob
                inner join Res_e_OffLineJob on Res_e_OffLineJob.JobID = Res_ItemCourseOffLineJob.JobID
                inner join Tr_ItemCourse on Tr_ItemCourse.TrainingItemCourseID = Res_ItemCourseOffLineJob.TrainingItemCourseID
                inner join Res_Course on Res_Course.CourseID = Tr_ItemCourse.CourseID
                inner join Tr_Item on Tr_Item.TrainingItemID = Tr_ItemCourse.TrainingItemID
                where 1=1 {0}";

            conditionSQL += string.Format(fieldEqualModal, "Res_ItemCourseOffLineJob", "TrainingItemCourseID", trainingItemCourseID.ToString());
            string sql = string.Format(sqlModalOffLineJob, conditionSQL);
            return GetData.GetPagedListFromSQL(sql, pageIndex, pageSize, "order by Res_ItemCourseOffLineJob.CreateTime desc", out totalRecords);
        }
        /// <summary>
        /// 获取某个培训项目下的离线作业列表
        /// </summary>
        /// <param name="trainingItemID">项目ID</param>     
        public DataTable GetOffLineJobListByTrainingItemID(Guid trainingItemID,int UserID)
        {
            string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_GetByTrainingItemID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = trainingItemID;
            parms[1].Value = UserID;
            #endregion
            return  SqlHelper.ExecuteDataset(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
        /// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPageList(string JobName, string ItemName, int OrganizationID)
        {
            string commandName = "dbo.Pr_Tr_Res_ItemCourseOffLineJob_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@JobName", SqlDbType.NVarChar),
                    new SqlParameter("@ItemName", SqlDbType.NVarChar),            
                    new SqlParameter("@OrganizationID", SqlDbType.Int)
                   
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = JobName;
            parms[1].Value = ItemName;      
            parms[2].Value = OrganizationID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];      
            return dt;
        }


        /// <summary>
        /// 统计培训项目课程下的离线作业数
        /// </summary>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public int GetOffLineJobCountByTrainingItemCourseID(Guid trainingItemCourseID)
        {

            int totalRecords = -1;
            DataTable dt = GetOffLineJobListByTrainingItemCourseID(trainingItemCourseID, 1, 0, out totalRecords);

            return totalRecords;
        }



        /// <summary>
        /// 获取成绩发布列表，涉及的表有：Tr_Item、Res_Course、Tr_ItemCourse
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetGradeIssueList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourse_GetGradeIssueList]";
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
        /// 发布某个培训项目课程成绩
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="isIssueGrade">是否发布课程成绩（0：不发布，1：发布）</param>
        /// <param name="gradeIssueUser">课程成绩发布人</param>
        public void Tr_ItemCourse_GradeIssue(Guid trainingItemCourseID, int isIssueGrade, string gradeIssueUser)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourse_GradeIssue]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@IsIssueGrade", SqlDbType.Int),
                    new SqlParameter("@GradeIssueUser", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = isIssueGrade;
            parms[2].Value = gradeIssueUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 培训项目课程的学生成绩列表
        ///  from dbo.Sty_StudentCourse a     
        ///     INNER JOIN Sty_StudentSignup ss on ss.StudentSignupID = a.StudentSignupID
        ///     INNER JOIN Tr_ItemCourse c on c.TrainingItemCourseID = a.TrainingItemCourseID
        ///     INNER JOIN Res_Course b on b.CourseID = c.CourseID
        ///     INNER JOIN Site_User u on u.UserID = ss.UserID
        ///     INNER JOIN Site_Student s on s.UserID = u.UserID
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetItemCourseStudentScoreList(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourse_GetStudentScoreList]";
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
        /// 课程选课状态 1：本项目已经选择，2：其它项目中已经选过
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="courseID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetSignCourseStatus(Guid trainingItemID, Guid courseID, int userID)
        {
            string commandName = "dbo.[Pr_Sty_StudentCourse_GetSignCourseStatus]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = courseID;
            parms[2].Value = userID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }




        /// <summary>
        /// 查询某个培训项目课程及其对应的课程报名与否等数据
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="trainingItemSignupNumber">整个项目已经报名的学员数</param>
        /// <param name="trainingItemCourseSignupNumber">该培训项目课程已经报名的学员数(已经选课的学员数)</param>
        /// <param name="courseSignupNumber">该项目学员中，已经在别的项培训项目课程对应该课程报名的学员数</param>
        /// <param name="courseReSignupNumber">当前该课程已经重复报名的学员数</param>
        public void Tr_ItemCourse_GetStudentSignupOrNotNumber(Guid trainingItemCourseID, out int trainingItemSignupNumber, out int trainingItemCourseSignupNumber, out int courseSignupNumber, out int courseReSignupNumber)
        {
            trainingItemSignupNumber = 0;
            trainingItemCourseSignupNumber = 0;
            courseSignupNumber = 0;
            courseReSignupNumber = 0;

            string commandName = "[dbo].[Pr_Tr_ItemCourse_GetStudentSignupOrNotNumber]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemSignupNumber", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseSignupNumber", SqlDbType.Int),
                    new SqlParameter("@CourseSignupNumber", SqlDbType.Int),
                    new SqlParameter("@CourseReSignupNumber", SqlDbType.Int)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Direction = ParameterDirection.Output;
            parms[2].Direction = ParameterDirection.Output;
            parms[3].Direction = ParameterDirection.Output;
            parms[4].Direction = ParameterDirection.Output;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            trainingItemSignupNumber = (int)parms[1].Value;
            trainingItemCourseSignupNumber = (int)parms[2].Value;
            courseSignupNumber = (int)parms[3].Value;
            courseReSignupNumber = (int)parms[4].Value;


        }


        /// <summary>
        /// 跟据项目ID与讲师ID获得项目课程
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemIDAndTeacherID(Guid trainingItemID, int teacherID)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourse_GetByTrainingItemIDAndTeacherID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TeacherID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemID;
            parms[1].Value = teacherID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public void UpdateItemCourseBudgetFee(string itemCourseID, string budgetFee)
        {
            string commandName = "UPDATE Tr_ItemCourse SET BudgetFee='{0}',IsSettingPay=1 WHERE TrainingItemCourseID='{1}'";
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, string.Format(commandName, budgetFee, itemCourseID), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetNotBuyTraininItemCourseList(int userID, Guid itemID, int signupModeID, int pageIndex, int pageSize, out int totalRecords)
        {
            string commandName = "dbo.Pr_NotBuyTrainingItemCourseList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {

                   new SqlParameter("@UserID",SqlDbType.Int),
                   new SqlParameter("@ItemID",SqlDbType.UniqueIdentifier),
                   new SqlParameter("@SignupModeID",SqlDbType.Int),
                     new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = userID;
            parms[1].Value = itemID;
            parms[2].Value = signupModeID;
            parms[3].Value = pageIndex;
            parms[4].Value = pageSize;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }

        #region 项目课程学习情况分析
        /// <summary>
        /// 获取选课人数
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetChooseCourseNum(Guid trainingItemCourseID)
        {
            string sql = @"SELECT COUNT(1) AS ChooseCourseNum FROM Temp_StandardCalulate WHERE TrainingItemCourseID =@TrainingItemCourseID";
            SqlParameter[] parms = new SqlParameter[]
                {
                   new SqlParameter("@TrainingItemCourseID",SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = trainingItemCourseID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 完成人数
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetCompletedNum(Guid trainingItemCourseID)
        {
            string sql = @"SELECT COUNT(1) AS CompletedNum 
                            FROM Temp_StandardCalulate 
                            WHERE TrainingItemCourseID = @TrainingItemCourseID
	                            AND (CourseResourceNum = StudiedNum OR CourseResourceNum IS NULL) AND (JobNum = JobSubmitNum OR JobNum IS NULL)";
            SqlParameter[] parms = new SqlParameter[]
                {
                   new SqlParameter("@TrainingItemCourseID",SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = trainingItemCourseID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 未学习人数
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetUnStudyNum(Guid trainingItemCourseID)
        {
            string sql = @"SELECT COUNT(1) AS UnStudyNum 
                            FROM Temp_StandardCalulate 
                            WHERE TrainingItemCourseID = @TrainingItemCourseID
	                            AND ((CourseResourceNum IS NOT NULL AND StudiedNum IS NULL AND JobNum IS NULL)
		                            OR (CourseResourceNum IS NOT NULL AND StudiedNum IS NULL AND JobNum IS NOT NULL AND JobSubmitNum IS NULL)
		                            OR (CourseResourceNum IS NULL AND JobNum IS NOT NULL AND JobSubmitNum IS NULL)
	                            )";
            SqlParameter[] parms = new SqlParameter[]
                {
                   new SqlParameter("@TrainingItemCourseID",SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = trainingItemCourseID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 内容学习完成数
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetContentCompleteNum(Guid trainingItemCourseID)
        {
            string sql = @"SELECT COUNT(1) AS ContentCompleteNum 
                            FROM Temp_StandardCalulate 
                            WHERE TrainingItemCourseID = @TrainingItemCourseID
	                            AND 
	                            ((CourseResourceNum IS NOT NULL AND StudiedNum = CourseResourceNum)
		                            OR (CourseResourceNum IS NULL)
	                            )";
            SqlParameter[] parms = new SqlParameter[]
                {
                   new SqlParameter("@TrainingItemCourseID",SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = trainingItemCourseID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        public DataTable GetJobCompleteNum(Guid trainingItemCourseID)
        {
            string sql = @"SELECT COUNT(1) AS JobCompleteNum 
                            FROM Temp_StandardCalulate 
                            WHERE TrainingItemCourseID = @TrainingItemCourseID
	                            AND (JobNum IS NOT NULL AND JobNum = JobSubmitNum)";
            SqlParameter[] parms = new SqlParameter[]
                {
                   new SqlParameter("@TrainingItemCourseID",SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = trainingItemCourseID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取课程标准资源数、测评数
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetTop1Info(Guid trainingItemCourseID)
        {
            string sql = @"SELECT TOP 1 *
                            FROM Temp_StandardCalulate
                            WHERE 
                            TrainingItemCourseID = @TrainingItemCourseID";
            SqlParameter[] parms = new SqlParameter[]
                {
                   new SqlParameter("@TrainingItemCourseID",SqlDbType.UniqueIdentifier),
                };
            parms[0].Value = trainingItemCourseID;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
            return dt;
        }
        #endregion
    }
}
