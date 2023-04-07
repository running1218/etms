using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.Test;
using System.Data;
using System.Collections;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    public class UserExamIBatisDao : ReadWriteDataMapperDaoSupport, IUserExamDao
    {
        #region --用户试卷的状态--

        public UserTestStatusType GetTestStatusType(Guid UserExamID)
        {
            DataTable oDt = DataMapperClient_Read.QueryForDataTable(
                "Test.UserExam.GetTestStatusType", UserExamID);
            if (oDt == null || oDt.Rows == null || oDt.Rows.Count <= 0)
                return UserTestStatusType.NotStart;

            //得到值，并转换
            int nRet = Convert.ToInt32(oDt.Rows[0][0]);
            return (UserTestStatusType)nRet;
        }

        public void UpdateTestStatusType(Guid UserExamID, UserTestStatusType NewTestStatusType)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExam.UpdateTestStatusType",
                new
                {
                    UserExamID = UserExamID,
                    Status = NewTestStatusType
                }
                );
        }
        /// <summary>
        /// add 2013-8-30 hjy 修改提交试卷的状态和总分
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="NewTestStatusType"></param>
        public void UpdateUserScoreOver(Guid UserExamID, UserTestStatusType NewTestStatusType)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExam.UpdateUserScoreOver",
                new
                {
                    UserExamID = UserExamID,
                    Status = NewTestStatusType
                }
                );
        }
        #endregion

        #region IUserExamDao 成员


        public IList<UserExam> FindAllUserExamsFor(int UserID, Guid TestPaperID)
        {
            IList<UserExam> LstTestFeedbacks = DataMapperClient_Read.QueryForList<UserExam>(
                "Test.UserExam.FindAllUserExamsFor",
                new
                {
                    StudentID = UserID,
                    TestPaperID = TestPaperID
                }
                );

            return LstTestFeedbacks;
        }
        /// <summary>
        /// 得到某一个考生，指定试卷的考试的次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        public int FindAllUserExamsCountFor(int UserID, Guid TestPaperID)
        {
            int nTimes = DataMapperClient_Read.QueryForObject<int>(
                "Test.UserExam.FindAllUserExamsCountFor",
                new
                {
                    StudentID = UserID,
                    TestPaperID = TestPaperID
                }
                );

            return nTimes;
        }
        public UserExam GetUserExamByUserExamID(Guid UserExamID)
        {
            UserExam LstTestFeedbacks = DataMapperClient_Read.QueryForObject<UserExam>(
                "Test.UserExam.GetUserExamByUserExamID", UserExamID);

            return LstTestFeedbacks;
        }

        public void UpdateStartExamDateTime(Guid UserExamID)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExam.UpdateStartExamDateTime", UserExamID);
        }

        public void TestOver(Guid UserExamID)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExam.TestOver", UserExamID);
        }
        /// <summary>
        /// 更新考生成绩
        /// </summary>
        /// <param name="UserExamID"></param>
        public void UpdateUserScore(Guid UserExamID, decimal UserScore)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExam.UpdateUserScore",
                new
                {
                    UserExamID = UserExamID,
                    UserScore = UserScore
                });
        }
        public void UpdateExamElapsedTime(Guid UserExamID, int nElapsedTime, int nCurQuestionNo)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("Test.UserExam.UpdateExamElapsedTime",
                new
                {
                    UserExamID = UserExamID,
                    ElapsedTime = nElapsedTime,
                    CurrentQuestionNo = nCurQuestionNo
                });
        }
        ///<summary>
        /// 得到指定考生的测试信息
        ///</summary>
        /// <param name="studentID">考生ID</param>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testState">考试状态</param>
        public IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState,
            int pageSize, int pageIndex, out int totalCount)
        {
            //SELECT C.UserID,C.UserName,C.FirstName,C.LastName,C.Photo,
            //A.UserExamID, A.ExamScore As UserScore,A.TestPaperScore As PaperScore,
            //A.BeginExamTime As StartExamTime,A.EndExamTime,A.[Status] As TestStatus,
            //B.TestPaperID,B.TestPaperName,
            //B.TestPaperType
            //FROM KS_UserExam A JOIN KS_TestPaper B
            //ON A.TestPaperID =B.TestPaperID
            //LEFT JOIN USERS C
            //ON A.UserID=C.UserID
            //WHERE 
            //A.UserID=#USERID# AND A.TestPaperID =#TESTPAPERID#
            //AND A.[Status]=#STATUS#
            //ORDER BY A.[Status] DESC,A.BeginExamTime

            IList<StudentTestView> result = new List<StudentTestView>();
            Hashtable paraTable = new Hashtable();
            paraTable.Add("tableName", @"[KS_UserExam] A JOIN [KS_TestPaper] B
            ON A.[TestPaperID] =B.[TestPaperID]
            LEFT JOIN [USERS] C
            ON A.[UserID]=C.[UserID]");

            paraTable.Add("fields", @"C.[UserID],C.[UserName],C.[FirstName],C.[LastName],C.[Photo],
            A.[UserExamID], A.[ExamScore] As UserScore,A.[TestPaperScore] As PaperScore,
            A.[BeginExamTime] As StartExamTime,A.[EndExamTime],A.[Status] As TestStatus,
            B.[TestPaperID],B.[TestPaperName],
            B.[TestPaperType]");

            string sWhere = "1=1 AND A.[IsPreview]=0 ";
            sWhere += " AND A.[UserID]='" + studentID.ToString() + "' ";
            sWhere += " AND A.[TestPaperID]='" + testPaperID.ToString() + "' ";
            if (testState != UserTestStatusType.ALL)
            {
                sWhere += " AND A.[Status]=" + ((int)testState).ToString();
            }

            paraTable.Add("sqlWhere", sWhere);
            //paraTable.Add("groupField", "");
            paraTable.Add("orderField", "A.[Status] DESC,A.[BeginExamTime]");
            paraTable.Add("pageIndex", pageIndex);
            paraTable.Add("pageSize", pageSize);
            paraTable.Add("totalRecord", 0);
            result = DataMapperClient_Read.QueryForList<StudentTestView>
                ("Test.UserExam.FindStudentTests", paraTable);

            totalCount = (int)paraTable["totalRecord"];

            return result;
        }
        public StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview)
        {
            StudentTestView oUserExamStat = DataMapperClient_Read.QueryForObject<StudentTestView>(
                "Test.UserExam.GetUserLastTest",
                new
                {
                    UserID = studentID,
                    TestPaperID = testPaperID,
                    TestStatus = testState,
                    IsPreview = IsPreview
                }
                );

            return oUserExamStat;
        }
        public IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState)
        {
            IList<StudentTestView> LstTests = DataMapperClient_Read.QueryForList<StudentTestView>(
                "Test.UserExam.FindStudentTestsNoPaged",
                new
                {
                    StudentID = studentID,
                    TestPaperID = testPaperID,
                    TestStatus = testState
                }
                );

            return LstTests;
        }

        public UserExamStat GetUserExamStatByTestPaper(int UserID, Guid TestPaperID)
        {
            UserExamStat oUserExamStat = DataMapperClient_Read.QueryForObject<UserExamStat>(
                "Test.UserExam.GetUserExamStatByTestPaper",
                new
                {
                    UserID = UserID,
                    TestPaperID = TestPaperID
                }
                );

            return oUserExamStat;
        }

        #endregion
    }
}
