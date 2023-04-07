// File:    StudentTestView.cs
// Author:  Administrator
// Created: 2012年1月13日 11:31:00
// Purpose: Definition of Class StudentTestView

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 考生测试信息。包括：考生基本信息、试卷基本信息、测试的状态、测试分数
    ///</summary>
    ///
    [Serializable]
   public class StudentTestView
   {
       public int UserID { get; set; }
       public string UserName { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Photo { get; set; }

       /// <summary>
       /// 采用的试卷定义ID
       /// </summary>
       public Guid TestPaperID { get; set; }
       /// <summary>
       /// 试卷的名称
       /// </summary>
       public string TestPaperName { get; set; }
       /// <summary>
       /// 试卷类型
       /// </summary>
       public TestPaperType TestPaperType { get; set; }

       /// <summary>
       /// 考生某一次考试ID
       /// </summary>
       public Guid UserExamID { get; set; }
       /// <summary>
       /// 开始考试时间
       /// </summary>
       public DateTime StartExamTime { get; set; }
       /// <summary>
       /// 结束考试时间
       /// </summary>
       public DateTime EndExamTime { get; set; }

       /// <summary>
       /// 考生考试状态
       /// </summary>
       public UserTestStatusType TestStatus { get; set; }
       /// <summary>
       /// 考生考试分数
       /// </summary>
       public decimal UserScore { get; set; }
       /// <summary>
       /// 试卷总分
       /// </summary>
       public decimal PaperScore { get; set; }
   }
}