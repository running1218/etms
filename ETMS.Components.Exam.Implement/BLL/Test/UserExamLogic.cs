using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.DAL.Test;

namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 考生答卷逻辑实现
    /// </summary>
    public class UserExamLogic : IMessageSourceAware, IInitializingObject
    {
        #region --错误代码--
        //private static string Err_TestFeedback_Not_Found = "ItemBank.OptionGroup.Not.Found";
        //private static string Err_TestFeedback_Data_Invalid = "ItemBank.OptionGroup.Data.Invalid";
        private static string Err_UserExamResult_Instance_Invalid = "Test.UserExam.Instance.Invalid";
        //private static string Err_TestFeedback_New_Invalid = "ItemBank.OptionGroup.New.Invalid";
        #endregion

        public IUserExamDao UserExamDao { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (UserExamDao == null)
            {
                throw new Exception("please set TestFeedbackDao Property First!");
            }
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

        #endregion

        #region --获取试卷信息--
        /// <summary>
        /// 得到试卷中某一试题信息
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public UserExam GetUserExamByUserExamID(Guid UserExamID)
        {
            return UserExamDao.GetUserExamByUserExamID(UserExamID);
        }
        /// <summary>
        /// 得到某个用户某一次未开始的考试信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        public IList<UserExam> FindNotStartUserExamFor(int UserID, Guid TestPaperID)
        {
            IList<UserExam> LstAll = FindAllUserExamsFor(UserID, TestPaperID);
            if (LstAll == null || LstAll.Count <= 0)
                return null;

            //得到状态中未开始的部分
            var LstTmps = from item in LstAll
                          where item.Status == UserTestStatusType.NotStart
                          select item;

            return LstTmps.ToList<UserExam>(); 
        }
        /// <summary>
        /// 得到某一个考生，指定试卷的已考试结束的考试次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        public IList<UserExam> FindCompletedUserExamFor(int UserID, Guid TestPaperID)
        {
            IList<UserExam> LstAll = FindAllUserExamsFor(UserID, TestPaperID);
            if (LstAll == null || LstAll.Count <= 0)
                return null;

            //得到状态中未开始的部分
            var LstTmps = from item in LstAll
                          where item.Status == UserTestStatusType.TestOver
                          select item;

            return LstTmps.ToList<UserExam>(); 
        }
        /// <summary>
        /// 得到某一个考生，指定试卷的所有次考试信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        public IList<UserExam> FindAllUserExamsFor(int UserID, Guid TestPaperID)
        {
            return UserExamDao.FindAllUserExamsFor(UserID, TestPaperID);
        }
        /// <summary>
        /// 得到某一个考生，指定试卷的考试的次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        public int FindAllUserExamsCountFor(int UserID, Guid TestPaperID)
        {
            return UserExamDao.FindAllUserExamsCountFor(UserID, TestPaperID);
        }
        #endregion

        /// <summary>
        /// 更新考生考试的登录时间
        /// </summary>
        /// <param name="UserExamID"></param>
        public void UpdateStartExamDateTime(Guid UserExamID)
        {
            UserExamDao.UpdateStartExamDateTime(UserExamID);
        }
        /// <summary>
        /// 考生考试结束
        /// </summary>
        /// <param name="UserExamID"></param>
        public void TestOver(Guid UserExamID)
        {
            UserExamDao.TestOver(UserExamID);
        }
        /// <summary>
        /// 更新考生成绩
        /// </summary>
        /// <param name="UserExamID"></param>
        public void UpdateUserScore(Guid UserExamID, decimal UserScore)
        {
            UserExamDao.UpdateUserScore(UserExamID, UserScore);
        }
        /// <summary>
        /// 更新考生的答题时间
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="nElapsedTime">使用总时间</param>
        public void UpdateExamElapsedTime(Guid UserExamID,int nElapsedTime,int nCurQuestionNo)
        {
            UserExamDao.UpdateExamElapsedTime(UserExamID, nElapsedTime,nCurQuestionNo);
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
            return UserExamDao.FindStudentTests(studentID, testPaperID, testState,
                pageSize, pageIndex, out totalCount);
        }
        ///<summary>
        /// 得到指定考生的测试信息
        ///</summary>
        /// <param name="studentID">考生ID</param>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testState">考试状态</param>
        public IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState)
        {
            return UserExamDao.FindStudentTests(studentID, testPaperID, testState);
        }
        internal UserExamStat GetUserExamStat(int UserID, Guid TestPaperID)
        {
            return UserExamDao.GetUserExamStatByTestPaper(UserID, TestPaperID);
        }
        private bool ValidIsInitialized()
        {
            //if (this.UserExamID == null || this.UserExamID == Guid.Empty)
            //{
            //    return false;
            //}

            return true;
        }
        /// <summary>
        /// 检查实例中数据是否完整，不完整直接抛出异常。
        /// </summary>
        private void ThrowNotInitializedExeception()
        {
            bool bIsInit = this.ValidIsInitialized();
            if (!bIsInit)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_Instance_Invalid, 
                    new Exception("未正确加载数据，请正确加载试题选项数据加载"));
            }
        }



        internal StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview)
        {
            return UserExamDao.GetUserLastTest( studentID,  testPaperID,  testState,  IsPreview);
        }
    }

    
}
