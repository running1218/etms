// File:    IUserTestPaperDao.cs
// Author:  Administrator
// Created: 2011年12月15日 15:27:10
// Purpose: Definition of Interface IUserTestPaperDao

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    ///<summary>
    /// 对考生试卷信息的数据访问的接口
    ///</summary>
    public interface IUserTestPaperDao
   {
      ///<summary>
      /// 添加一个考生考试试卷
      ///</summary>
      void AddUserTestPaper(UserTestPaper userTestPaper);
      
      ///<summary>
      /// 更新考生考试试卷
      ///</summary>
      void Update(UserTestPaper newUserTestPaper);
      
      ///<summary>
      /// 删除指定的考生试卷
      ///</summary>
      void Delete(System.Guid userTestPaperID);
      
      ///<summary>
      /// 调整考生的分数
      ///</summary>
      /// <param name="score">调整的分数。</param>
      void AdjustScore(System.Guid adjustUserID, System.Guid userTestPaperID, decimal score);
      
      ///<summary>
      /// 开始考试
      ///</summary>
      void StartTest(System.Guid userTestPaperID);
      
      ///<summary>
      /// 保存考试时间
      ///</summary>
      /// <param name="testTime">考试已使用的时间（单位：秒）</param>
      void SaveTestTime(System.Guid userTestPaperID, int testTime);
      
      ///<summary>
      /// 提交试卷
      ///</summary>
      /// <param name="testTime">考试已使用的时间（单位：秒）</param>
      void SubmitTestPaper(System.Guid userTestPaperID, int testTime);
      
      ///<summary>
      /// 得到某一考生某一试卷考试的试卷信息
      ///</summary>
      /// <param name="testPaperID">试卷ID</param>
      IList<UserTestPaper> FindTestPaperForUserAndPaperID(int userID, System.Guid testPaperID);
   
   }
}