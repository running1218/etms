// File:    TestFeedback.cs
// Author:  Administrator
// Created: 2011年12月17日 11:45:25
// Purpose: Definition of Class TestFeedback

using System;


namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 试卷反馈类
    ///</summary>
    [Serializable]
   public class TestFeedback 
   {
       /// <summary>
       /// 反馈ID
       /// </summary>
       public Guid FeedbackID { get; set; }

       /// <summary>
       /// 试卷定义ID
       /// </summary>
       public Guid TestPaperID { get; set; }

       /// <summary>
       /// 分值开始值
       /// </summary>
       public Decimal BeginScore { get; set; }

       /// <summary>
       /// 分值结束值
       /// </summary>
       public Decimal EndScore { get; set; }

       /// <summary>
       /// 反馈内容
       /// </summary>
       public String Content { get; set; }

       /// <summary>
       /// 是否删除
       /// </summary>
       public bool IsDelete { get; set; }

       public TestFeedback() { }
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="testPaperID"></param>
       public TestFeedback(Guid testPaperID)
       {
            this.TestPaperID = testPaperID;
       }

       // 摘要:
       //     创建日期
       public DateTime CreatedDate { get; set; }
       //
       // 摘要:
       //     创建人
       public int CreatedUserID { get; set; }
       //
       // 摘要:
       //     最后修改日期
       public DateTime UpdatedDate { get; set; }
       //
       // 摘要:
       //     最后修改人
       public int UpdatedUserID { get; set; }
   }
}