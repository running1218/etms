using System;
using System.Linq;
using ETMS.Components.Exam.API.Entity.NewTestPaper;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;
using ETMS.Utility;

namespace ETMS.Components.Exam.Implement.DAL.NewTestPaper
{
    public class ExamDataAccess : AExamHomeWorkData
    {
        /// <summary>
        /// 查询试卷考试信息
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="onlineTestID">测试ID</param>
        /// <returns></returns>
        public override Paper GetExamTestPaper(Guid testPaperID, Guid onlineTestID)
        {
            string commandName = "Pr_KS_TestPaper_GetExamTestPaper";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OnLineTestID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = testPaperID;
            parms[1].Value = onlineTestID;

            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return PopulateKS_TestPaperFromDataRow(tab.Rows[0]);

        }

        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public override Guid StartNewTest(int studentID, Guid testPaperID, Guid trainingItemCourseID, Guid onlineTestID, Guid studentCourseID)
        {
            string commandName = "Pr_KS_UserExam_StartNewTest";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PaperID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OnlineTestID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@StudentCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ExamID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = testPaperID;
            parms[1].Value = studentID;
            parms[2].Value = trainingItemCourseID;
            parms[3].Value = onlineTestID;
            parms[4].Value = studentCourseID;
            parms[5].Value = new Guid();
            parms[5].Direction = ParameterDirection.Output;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
            return (Guid)parms[5].Value;
        }

        /// <summary>
        /// 提交试卷
        /// </summary>
        /// <param name="userExamID"></param>
        /// <param name="status"></param>
        public override void SubmitTestPaper(Guid userExamID, int status, int userID, Guid testPaperID)
        {
            string commandName = "pr_KS_UserExam_SubmitTestPaper";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TestPaperID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = userExamID;
            parms[1].Value = status;
            parms[2].Value = userID;
            parms[3].Value = testPaperID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
        }

        public override UserTestPaper GetUserTestPaper(Guid userExamID, Guid onLineTestID)
        {
            string commandName = "Pr_KS_UserExam_GetUserTestPaper";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserExamID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OnLineTestID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = userExamID;
            parms[1].Value = onLineTestID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            var p = from x in dt.AsEnumerable()
                    select new UserTestPaper
                        {
                            TestPaperID = x.Field<string>("TestPaperID"),
                            TestPaperName = x.Field<string>("TestPaperName"),
                            TestPaperScore = x.Field<decimal>("TestPaperScore"),
                            ExamScore = x.Field<decimal>("ExamScore"),
                            IsShowAnswer = x.Field<int>("IsShowAnswer")
                        };
            return p.ToList().First();
        }

    }
}
