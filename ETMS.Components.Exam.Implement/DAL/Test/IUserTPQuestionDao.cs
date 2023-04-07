// File:    IUserTPQuestionDao.cs
// Author:  Administrator
// Created: 2011年12月15日 15:50:44
// Purpose: Definition of Interface IUserTPQuestionDao

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;

namespace ETMS.Components.Exam.Implement.DAL.Test
{
    ///<summary>
    /// 对考生试卷试题表的数据访问
    ///</summary>
    public interface IUserTPQuestionDao
   {
      void AddTPQuestion(UserTPQuestion userTPQuestion);
      
      void Update(UserTPQuestion newUserTPQuestion);
      
      void Delete(System.Guid userTPQuestionID);
      
      ///<summary>
      /// 更新指定试题的分数
      ///</summary>
      /// <param name="answer">考生答案</param>
      void AnswerUpdate(System.Guid userTPQuestionID, string answer);
      
      ///<summary>
      /// 更新某一试题考生分数
      ///</summary>
      /// <param name="score">考生得分</param>
      void ScoreUpdate(System.Guid userTPQuestionID, decimal score);
      
      ///<summary>
      /// 得到某一试卷中所有试题信息
      ///</summary>
      IList<UserTPQuestion> FindAllQuestionsInUserPaper(System.Guid examID);
      
      ///<summary>
      /// 删除某一用户试卷中所有试题
      ///</summary>
      void DeleteAllInUserPaper(System.Guid examID);
   
   }
}